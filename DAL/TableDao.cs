using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
