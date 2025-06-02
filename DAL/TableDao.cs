using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL;
using Model;
using System.Data;

namespace DAL
{
    public class TableDao : BaseDao
    {
        public List<Table> GetAllTables()
        {
            string sql = @" SELECT id, table_number, table_status FROM dbo.[Table]  ORDER  BY table_number";

            DataTable dt = ExecuteSelectQuery(sql);

            return dt.Rows
                .Cast<DataRow>()
                .Select(dr => 
                {
                    int tableId = dr.Field<int>("id");
                    int tableNumber = dr.Field<int>("table_number");
                    string tableStatusString = dr.Field<string>("table_status");

                    if (!Enum.TryParse<TableStatus>(tableStatusString, ignoreCase: true, out var tableStatus))
                        throw new DataException($"Unrecognized table status '{tableStatusString}' for table {tableNumber}");
                    return new Table(tableId,tableNumber, tableStatus);
                })
                .ToList();
        }
        public Table GetTableById(int tableId)
        {
            string sql = @"SELECT id, table_number, table_status FROM dbo.[Table] WHERE id = @tableId";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@tableId", tableId)
            };
            DataTable dt = ExecuteSelectQuery(sql, sqlParameters);
            if (dt.Rows.Count == 0)
            {
                throw new DataException($"No table found with ID {tableId}");
            }
            DataRow dr = dt.Rows[0];
            int tableNumber = dr.Field<int>("table_number");
            string tableStatusString = dr.Field<string>("table_status");
            if (!Enum.TryParse<TableStatus>(tableStatusString, ignoreCase: true, out var tableStatus))
                throw new DataException($"Unrecognized table status '{tableStatusString}' for table {tableNumber}");
            return new Table(tableId, tableNumber, tableStatus);
        }

        //update tabel status
        public void UpdateTableStatus(int tableId, TableStatus status)
        {
            string sql = @"UPDATE dbo.[Table] SET table_status = @status WHERE id = @tableId";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@status", status.ToString()),
                new SqlParameter("@tableId", tableId)
            };
            ExecuteEditQuery(sql, sqlParameters);
        }

    }
}
