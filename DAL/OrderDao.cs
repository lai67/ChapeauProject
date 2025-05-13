using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    internal class OrderDao
    {
        private string connectionString = "your_connection_string_here";

        public enum OrderStatus
        {
            Running,
            Preparing,
            Ready,
            Served
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            string query = "SELECT * FROM [Order]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order
                    {
                        Id = reader.GetInt32(0),
                        OrderTime = reader.GetDateTime(1),
                        PreparationTime = reader.GetDateTime(2),
                        Status = reader.GetString(3),
                        EmployeeId = reader.GetInt32(4),
                        BillId = reader.GetInt32(5),
                        PreparationLocation = reader.GetString(6),
                        TableId = reader.GetInt32(7)
                    };
                    orders.Add(order);
                }
            }

            return orders;
        }

        public void AddOrder(Order order)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), order.Status))
            {
                throw new ArgumentException("Invalid order status.");
            }

            string query = "INSERT INTO [Order] (order_time, preparation_time, status, employee_id, bill_id, preparation_location, table_id) VALUES (@orderTime, @prepTime, @status, @employeeId, @billId, @prepLocation, @tableId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderTime", order.OrderTime);
                command.Parameters.AddWithValue("@prepTime", order.PreparationTime);
                command.Parameters.AddWithValue("@status", order.Status);
                command.Parameters.AddWithValue("@employeeId", order.EmployeeId);
                command.Parameters.AddWithValue("@billId", order.BillId);
                command.Parameters.AddWithValue("@prepLocation", order.PreparationLocation);
                command.Parameters.AddWithValue("@tableId", order.TableId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), newStatus))
            {
                throw new ArgumentException("Invalid order status.");
            }

            string query = "UPDATE [Order] SET status = @newStatus WHERE id = @orderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@newStatus", newStatus);
                command.Parameters.AddWithValue("@orderId", orderId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Order> GetRunningOrders()
        {
            List<Order> runningOrders = new List<Order>();
            string query = "SELECT * FROM [Order] WHERE status = @status";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", OrderStatus.Running.ToString());
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order
                    {
                        Id = reader.GetInt32(0),
                        OrderTime = reader.GetDateTime(1),
                        PreparationTime = reader.GetDateTime(2),
                        Status = reader.GetString(3),
                        EmployeeId = reader.GetInt32(4),
                        BillId = reader.GetInt32(5),
                        PreparationLocation = reader.GetString(6),
                        TableId = reader.GetInt32(7)
                    };
                    runningOrders.Add(order);
                }
            }

            return runningOrders;
        }
    }
}