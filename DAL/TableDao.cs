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
            string sql = @" SELECT table_number, table_status FROM dbo.[Table] ";

            DataTable dt = ExecuteSelectQuery(sql);

            return dt.Rows
                .Cast<DataRow>()
                .Select(dr => 
                {
                    int tableNumber = dr.Field<int>("table_number");
                    string tableStatusString = dr.Field<string>("table_status");

                    if (!Enum.TryParse<TableStatus>(tableStatusString, ignoreCase: true, out var tableStatus))
                        throw new DataException($"Unrecognized table status '{tableStatusString}' for table {tableNumber}");
                    return new Table(tableNumber, tableStatus);
                })
                .ToList();
        }
    }
}
