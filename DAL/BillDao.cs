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
            string querySelect = "SELECT id, total_price, vat, guest_number, order_id, tip, feedback FROM [BILL] ORDER BY order_id;";
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
        public Bill GetBillById(int billId)
        {
            string query = "SELECT id, total_price, vat, guest_number, order_id, tip, feedback FROM [BILL] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", billId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);
            return ReadBill(table);
        }
        public void CreateBill(Bill bill)
        {
            string query = @"INSERT INTO [BILL] (total_price, vat, guest_number, order_id, tip, feedback)
                     VALUES (@total_price, @vat, @guest_number, @order_id, @tip, @feedback);";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@total_price", bill.TotalPrice),
                new SqlParameter("@vat", bill.Vat),
                new SqlParameter("@guest_number", bill.GuestNumber),
                new SqlParameter("@order_id", bill.OrderId),
                new SqlParameter("@tip", bill.Tip),
                new SqlParameter("@feedback", bill.Feedback ?? (object)DBNull.Value)
            };

            ExecuteEditQuery(query, parameters);
        }
        public void UpdateBill(Bill bill)
        {
            string query = @"UPDATE [BILL]
                     SET total_price = @total_price,
                         vat = @vat,
                         guest_number = @guest_number,
                         order_id = @order_id,
                         tip = @tip,
                         feedback = @feedback
                     WHERE id = @id;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@total_price", bill.TotalPrice),
                new SqlParameter("@vat", bill.Vat),
                new SqlParameter("@guest_number", bill.GuestNumber),
                new SqlParameter("@order_id", bill.OrderId),
                new SqlParameter("@tip", bill.Tip),
                new SqlParameter("@feedback", bill.Feedback ?? (object)DBNull.Value),
                new SqlParameter("@id", bill.BillId)
            };

            ExecuteEditQuery(query, parameters);
        }
        public void DeleteBill(int billId)
        {
            string query = "DELETE FROM [BILL] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", billId)
            };

            ExecuteEditQuery(query, parameters);
        }
    }
}
