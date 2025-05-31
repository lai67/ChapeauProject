using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
    }
}