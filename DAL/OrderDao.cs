//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using Model;

//namespace DAL
//{
//    internal class OrderDao : BaseDao
//    {
//        public enum OrderStatus
//        {
//            Running,
//            Preparing,
//            Ready,
//            Served
//        }

//        public List<Order> GetAllOrders()
//        {
//            List<Order> orders = new List<Order>();
//            string query = "SELECT * FROM [Order]";

//            using (SqlConnection connection = OpenConnection())
//            {
//                SqlCommand command = CreateCommand(connection, query);
//                SqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    orders.Add(MapOrder(reader));
//                }
//            }

//            return orders;
//        }

//        public void AddOrder(Order order)
//        {
//            if (!Enum.IsDefined(typeof(OrderStatus), order.Status))
//            {
//                throw new ArgumentException("Invalid order status.");
//            }

//            string query = "INSERT INTO [Order] (order_time, preparation_time, status, employee_id, bill_id, preparation_location, table_id) " +
//                           "VALUES (@orderTime, @prepTime, @status, @employeeId, @billId, @prepLocation, @tableId)";

//            using (SqlConnection connection = OpenConnection())
//            {
//                SqlCommand command = CreateCommand(connection, query,
//                    new SqlParameter("@orderTime", order.OrderTime),
//                    new SqlParameter("@prepTime", order.PreparationTime),
//                    new SqlParameter("@status", order.Status),
//                    new SqlParameter("@employeeId", order.EmployeeId),
//                    new SqlParameter("@billId", order.BillId),
//                    new SqlParameter("@prepLocation", order.PreparationLocation),
//                    new SqlParameter("@tableId", order.TableId));

//                command.ExecuteNonQuery();
//            }
//        }

//        public void UpdateOrderStatus(int orderId, string newStatus)
//        {
//            if (!Enum.IsDefined(typeof(OrderStatus), newStatus))
//            {
//                throw new ArgumentException("Invalid order status.");
//            }

//            string query = "UPDATE [Order] SET status = @newStatus WHERE id = @orderId";

//            using (SqlConnection connection = OpenConnection())
//            {
//                SqlCommand command = CreateCommand(connection, query,
//                    new SqlParameter("@newStatus", newStatus),
//                    new SqlParameter("@orderId", orderId));

//                command.ExecuteNonQuery();
//            }
//        }

//        public List<Order> GetRunningOrders()
//        {
//            List<Order> runningOrders = new List<Order>();
//            string query = "SELECT * FROM [Order] WHERE status = @status";

//            using (SqlConnection connection = OpenConnection())
//            {
//                SqlCommand command = CreateCommand(connection, query,
//                    new SqlParameter("@status", OrderStatus.Running.ToString()));
//                SqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    runningOrders.Add(MapOrder(reader));
//                }
//            }

//            return runningOrders;
//        }

//        private Order MapOrder(SqlDataReader reader)
//        {
//            return new Order
//            {
//                Id = reader.GetInt32(0),
//                OrderTime = reader.GetDateTime(1),
//                PreparationTime = reader.GetDateTime(2),
//                Status = reader.GetString(3),
//                EmployeeId = reader.GetInt32(4),
//                BillId = reader.GetInt32(5),
//                PreparationLocation = reader.GetString(6),
//                TableId = reader.GetInt32(7)
//            };
//        }
//    }
//}