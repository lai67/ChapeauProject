using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;


namespace DAL
{
    public class OrderDao : BaseDao
    {

        public Dictionary<int, (string BarStatus, string KitchenStatus)> GetTableLocationPhases()
        {
            const string sql = @"
            SELECT 
                t.table_number,
                o.preparation_location,
                MAX(
                    CASE oi.status
                        WHEN 'Placed'    THEN 1    
                        WHEN 'Preparing' THEN 2
                        WHEN 'Ready'     THEN 3
                        ELSE 0
                    END
                ) AS phase_code
            FROM dbo.[Table]      AS t
            LEFT JOIN dbo.[Order] AS o  ON o.table_id = t.id
            LEFT JOIN dbo.Order_Item AS oi ON oi.order_id = o.id
            WHERE oi.status IN ('Placed','Preparing','Ready')
            GROUP BY 
                t.table_number, 
                o.preparation_location;
        ";
            var dt = ExecuteSelectQuery(sql);
            return BuildTableLocationPhaseDictionary(dt);
        }
        private Dictionary<int, (string BarStatus, string KitchenStatus)> BuildTableLocationPhaseDictionary(DataTable dt)
        {
            var dict = new Dictionary<int, (string Bar, string Kitch)>();

            foreach (DataRow row in dt.Rows)
            {
                int tableNumber = row.Field<int>("table_number");
                string location = row.Field<string>("preparation_location"); 
                int code = row.Field<int>("phase_code");

                string status = code switch
                {
                    1 => "Placed",     
                    2 => "Preparing",
                    3 => "Ready",
                    _ => "None"
                };

                // If we already have an entry for this table, retrieve it; otherwise default to (None,None)
                dict.TryGetValue(tableNumber, out var current);

                if (location.Equals("Bar", StringComparison.OrdinalIgnoreCase))
                    dict[tableNumber] = (status, current.Kitch);
                else
                    dict[tableNumber] = (current.Bar, status);
            }

            return dict;
        }

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