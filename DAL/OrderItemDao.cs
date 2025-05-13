using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class OrderItemDao : BaseDao
    {
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM Order_Item WHERE order_id = @orderId";

            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand command = CreateCommand(connection, query,
                    new SqlParameter("@orderId", orderId));
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    orderItems.Add(MapOrderItem(reader));
                }
            }

            return orderItems;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            string query = "INSERT INTO Order_Item (menu_item_id, count, order_id) VALUES (@menuItemId, @count, @orderId)";

            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand command = CreateCommand(connection, query,
                    new SqlParameter("@menuItemId", orderItem.MenuItemId),
                    new SqlParameter("@count", orderItem.Count),
                    new SqlParameter("@orderId", orderItem.OrderId));

                command.ExecuteNonQuery();
            }
        }

        private OrderItem MapOrderItem(SqlDataReader reader)
        {
            return new OrderItem
            {
                Id = reader.GetInt32(0),
                MenuItemId = reader.GetInt32(1),
                Count = reader.GetInt32(2),
                OrderId = reader.GetInt32(3)
            };
        }
    }
}