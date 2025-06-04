using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using Model;


namespace DAL
{

    public class OrderItemDao : BaseDao
    {
        // Create Order Item
        public void CreateOrderItem(OrderItem orderItem)
        {
            string query = @"INSERT INTO Order_Item (menu_item_id, count, order_id, comment, status) 
                        VALUES (@menuItemId, @count, @orderId, @comment, @status)";

            SqlParameter[] parameters = {
            new SqlParameter("@menuItemId", orderItem.MenuItem.Id),
            new SqlParameter("@count", orderItem.Count),
            new SqlParameter("@orderId", orderItem.OrderId),
            new SqlParameter("@comment", orderItem.Comment ?? ""),
            new SqlParameter("@status", orderItem.orderStatus.ToString())
            };

            ExecuteEditQuery(query, parameters);
        }
        // get order items by order
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            string query = @"SELECT oi.*, mi.* FROM [Order_Item] oi
                     JOIN [Menu_Item] mi ON oi.menu_item_id = mi.id
                     WHERE oi.order_id = @orderId";
            SqlParameter[] parameters = { new SqlParameter("@orderId", orderId) };
            DataTable dt = ExecuteSelectQuery(query, parameters);

            List<OrderItem> items = new List<OrderItem>();
            foreach (DataRow row in dt.Rows)
            {
               
                var menuItem = new MenuItemModel
                {
                    Id = (int)row["menu_item_id"],
                    Name = row["name"].ToString(),
                    Item_Category = row["item_category"].ToString(),
                    Stock = (int)row["stock"],
                    Vat = Convert.ToDouble(row["vat"]),
                    Price = (decimal)row["price"],
                    PreparationTime = (int)row["preparation_time"],
                    Menu_Id = (int)row["menu_id"]
                };

                var orderItem = new OrderItem(
                    (int)row["id"],
                    menuItem,
                    row["comment"].ToString(),
                    (OrderItem.OrderStatus)Enum.Parse(typeof(OrderItem.OrderStatus), row["status"].ToString()),
                    (int)row["count"],
                    (int)row["order_id"]
                );
                items.Add(orderItem);
            }
            return items;
        }




        // Update Order Item Count
        public void UpdateOrderItemCount(OrderItem orderItem)
        {
            if (IfOrderItemExists(orderItem.OrderId, orderItem.MenuItem.Id))
            {
                UpdateOrderItemCount(orderItem.OrderId, orderItem.MenuItem.Id, orderItem.Count);
            }
        }

        // Helper method. 
        public bool IfOrderItemExists(int orderId, int menuItemId)
        {
            string query = "SELECT COUNT(*) FROM Order_Item WHERE order_id = @orderId AND menu_item_id = @menuItemId";
            SqlParameter[] parameters = {
            new SqlParameter("@orderId", orderId),
            new SqlParameter("@menuItemId", menuItemId)
          };
            var dt = ExecuteSelectQuery(query, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        // Helper method.
        public void UpdateOrderItemCount(int orderId, int menuItemId, int newCount)
        {
            if (newCount > 0)
            {
                string query = "UPDATE Order_Item SET count = @count WHERE order_id = @orderId AND menu_item_id = @menuItemId";
                SqlParameter[] parameters = {
                    new SqlParameter("@count", newCount),
                    new SqlParameter("@orderId", orderId),
                    new SqlParameter("@menuItemId", menuItemId)
                };
                ExecuteEditQuery(query, parameters);
            }
            else
            {
                string query = "DELETE FROM Order_Item WHERE order_id = @orderId AND menu_item_id = @menuItemId";
                SqlParameter[] parameters = {
                    new SqlParameter("@orderId", orderId),
                    new SqlParameter("@menuItemId", menuItemId)
                };
                ExecuteEditQuery(query, parameters);
            }
        }

        // count how many items at a given table are still "placed" or "preparing
        // so we can be able to free the table or not possible 

        public int CountRunningItemsByTableId(int tableId)
        {
            string query = @"SELECT COUNT(*) FROM Order_Item oi
                             JOIN [Order] o ON oi.order_id = o.id
                             WHERE o.table_id = @tableId AND oi.status IN ('Placed', 'Preparing')";
            SqlParameter[] parameters = {
                new SqlParameter("@tableId", tableId)
            };
            var dt = ExecuteSelectQuery(query, parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        //give all orderItem that are status is Ready

        public List<OrderItem> GetReadyItemsByTableId(int tableId)
        {
            string query = @"SELECT oi.id, oi.menu_item_id, oi.count, oi.order_id, oi.comment, oi.status 
                             FROM Order_Item oi
                             JOIN [Order] o ON oi.order_id = o.id
                             WHERE o.table_id = @tableId AND oi.status = 'Ready'";
            SqlParameter[] parameters = {
                new SqlParameter("@tableId", tableId)
            };
            var dt = ExecuteSelectQuery(query, parameters);
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                int menuItemId = Convert.ToInt32(row["menu_item_id"]);
                int count = Convert.ToInt32(row["count"]);
                int orderId = Convert.ToInt32(row["order_id"]);
                string comment = row["comment"].ToString();
                OrderItem.OrderStatus status = (OrderItem.OrderStatus)Enum.Parse(typeof(OrderItem.OrderStatus), row["status"].ToString());
                MenuItemModel menuItem = new MenuItemModel { Menu_Id = menuItemId };
                OrderItem orderItem = new OrderItem(id, menuItem, comment, status, count, orderId);
                orderItems.Add(orderItem);
            }
            return orderItems;
        }

        //marking all ready items as served 

        public void MarkAllReadyServedByTableId(int tableId)
        {
            string query = @"UPDATE Order_Item SET status = 'Served' 
                             WHERE order_id IN (SELECT id FROM [Order] WHERE table_id = @tableId) 
                             AND status = 'Ready'";
            SqlParameter[] parameters = {
                new SqlParameter("@tableId", tableId)
            };
            ExecuteEditQuery(query, parameters);
        }
    }
}