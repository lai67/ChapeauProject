using System;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillDao : BaseDao
    {
        public List<Bill> GetAllBills()
        {
            string querySelect = "SELECT id, total_price, vat, guest_number, order_id, tip_amount, feedback FROM [BILL] ORDER BY order_id;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(querySelect, sqlParameters));
        }
        private List<Bill> ReadTables(DataTable dataTable)
        {
            List<Bill> bills = new List<Bill>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Bill bill = new Bill()
                {
                    BillId = (int)dr["id"],
                    TotalPrice = (float)dr["total_price"],
                    Vat = (float)dr["vat"],
                    GuestNumber = (int)dr["guest_number"],
                    OrderId = (int)dr["order_id"],
                    Feedback = dr["feedback"].ToString(),
                    Tip = (float)dr["tip"],
                };
                bills.Add(bill);
            }
            return bills;
        }
        private Bill ReadBill(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            DataRow dr = dataTable.Rows[0];
            Bill bill = new Bill()
            {
                BillId = (int)dr["id"],
                TotalPrice = (float)dr["total_price"],
                Vat = (float)dr["vat"],
                GuestNumber = (int)dr["guest_number"],
                OrderId = (int)dr["order_id"],
                Feedback = dr["feedback"].ToString(),
                Tip = (float)dr["tip"],
            };
            return bill;
        }
    }
}
