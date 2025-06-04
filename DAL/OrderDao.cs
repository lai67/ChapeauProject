using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;


namespace DAL
{
    public class OrderDao : BaseDao
    {
        // it appears that we dont have the location prepratioin but we have a menu type where it states there lunch,diner ,and drink 
        //however we hava a meneu type where it states there lunch,diner ,and drink
        public Dictionary<int, (string BarStatus, string KitchenStatus)> GetTableLocationPhases()
        {
            const string sql = @"
    SELECT
        t.table_number,
        
        -- Determine “location” from the menu_name: drinks ? Bar, else ? Kitchen
        CASE 
            WHEN m.menu_name = 'Drink' THEN 'Bar'
            ELSE 'Kitchen'
        END AS item_location,

        MAX(
            CASE oi.status
                WHEN 'Placed'    THEN 1
                WHEN 'Preparing' THEN 2
                WHEN 'Ready'     THEN 3
                ELSE 0
            END
        ) AS phase_code

    FROM dbo.[Table] AS t
    LEFT JOIN dbo.[Order]      AS o  ON o.table_id       = t.id
    LEFT JOIN dbo.Order_Item   AS oi ON oi.order_id      = o.id
    LEFT JOIN dbo.Menu_Item    AS mi ON mi.id            = oi.menu_item_id
    LEFT JOIN dbo.Menu         AS m  ON m.id             = mi.menu_id

    WHERE oi.status IN ('Placed','Preparing','Ready')
    GROUP BY
        t.table_number,
        CASE 
            WHEN m.menu_name = 'Drink' THEN 'Bar'
            ELSE 'Kitchen'
        END;
    ";

            DataTable dt = ExecuteSelectQuery(sql);
            return BuildTableLocationPhaseDictionary(dt);
        }
        private Dictionary<int, (string Bar, string Kitch)> BuildTableLocationPhaseDictionary(DataTable dt)
        {
            var dict = new Dictionary<int, (string Bar, string Kitch)>();

            foreach (DataRow row in dt.Rows)
            {
                int tableNumber = row.Field<int>("table_number");
                string itemLocation = row.Field<string>("item_location");  // was “preparation_location”
                int code = row.Field<int>("phase_code");

                // Convert numeric code back to a textual status
                string status = code switch
                {
                    1 => "Placed",
                    2 => "Preparing",
                    3 => "Ready",
                    _ => "None"
                };

                // If this table already has an entry, get its current tuple.
                dict.TryGetValue(tableNumber, out var current);

                if (itemLocation.Equals("Bar", StringComparison.OrdinalIgnoreCase))
                {
                    // Update BarStatus slot, keep current.Kitch unchanged
                    dict[tableNumber] = (status, current.Kitch);
                }
                else
                {
                    // Update KitchenStatus slot, keep current.Bar unchanged
                    dict[tableNumber] = (current.Bar, status);
                }
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

        // update order
        public void UpdateOrderPreparationInfo(int orderId, int preparationTime, string preparationLocation)
        {
            string query = @"UPDATE [Order]
                     SET preparation_time = @preparationTime,
                         preparation_location = @preparationLocation
                     WHERE id = @orderId";
            SqlParameter[] parameters = {
        new SqlParameter("@preparationTime", preparationTime),
        new SqlParameter("@preparationLocation", preparationLocation),
        new SqlParameter("@orderId", orderId)
    };
            ExecuteEditQuery(query, parameters);
        }

        public Order GetOrdersForAlreadyOrderedTable(int tableId)
        {
            string query = @"SELECT TOP 1 o.*, t.table_number, t.table_status
                    FROM [Order] o
               JOIN [Table] t ON o.table_id = t.id
               WHERE o.table_id = @tableId AND o.isCreated = 0
               ORDER BY o.order_time DESC";
            SqlParameter[] parameters = {new SqlParameter("@tableId", tableId)
            };
            DataTable dt = ExecuteSelectQuery(query, parameters);
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Order(
       id: row.Field<int>("id"),
       orderTime: row.Field<DateTime>("order_time"),
       preparationTime: row.Field<int>("preparation_time"),
       isCreated: row.Field<bool>("isCreated"),
       employee: new Employee { Id = row.Field<int>("employee_id") },
       bill: null,
       preparationLocation: row.Field<string>("preparation_location"),
       table: new Table(
           row.Field<int>("table_id"),
           row.Field<int>("table_number"),
           Enum.Parse<TableStatus>(row.Field<string>("table_status"), true)
       )
   );
        }

    }
}