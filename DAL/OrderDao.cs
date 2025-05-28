using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;


namespace DAL
{
    public class OrderDao : BaseDao
    {
        public int CreateOrder(Order order)
        {
            string query = @"INSERT INTO [Order] (order_time, preparation_time, 
                             isCreated, employee_id, preparation_location, table_id) 
                        OUTPUT INSERTED.id
                        VALUES (@orderTime, @preparationTime, @isCreated, @employeeId, @preparationLocation, @tableId)";

            SqlParameter[] parameters = {
            new SqlParameter("@orderTime", order.OrderTime),
            new SqlParameter("@preparationTime", order.PreparationTime),
            new SqlParameter("@isCreated", order.IsCreated),
            new SqlParameter("@employeeId", order.Employee.Id),
            new SqlParameter("@preparationLocation", order.PreparationLocation),
            new SqlParameter("@tableId", order.Table.Id)
        };
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddRange(parameters);
                int insertedId = (int)command.ExecuteScalar();
                return insertedId;
            }
        }
    }
}