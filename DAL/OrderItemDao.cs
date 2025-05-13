using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class OrderItemDao
    {
        private string connectionString = "your_connection_string_here";

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM Order_Item WHERE order_id = @orderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderItem orderItem = new OrderItem
                    {
                        Id = reader.GetInt32(0),
                        MenuItemId = reader.GetInt32(1),
                        Count = reader.GetInt32(2),
                        OrderId = reader.GetInt32(3)
                    };
                    orderItems.Add(orderItem);
                }
            }

            return orderItems;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            string query = "INSERT INTO Order_Item (menu_item_id, count, order_id) VALUES (@menuItemId, @count, @orderId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@menuItemId", orderItem.MenuItemId);
                command.Parameters.AddWithValue("@count", orderItem.Count);
                command.Parameters.AddWithValue("@orderId", orderItem.OrderId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}