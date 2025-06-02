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
            SubBillDao subBillDao = new SubBillDao();

            foreach (DataRow dr in dataTable.Rows)
            {
                int billId = (int)dr["id"];

                Bill bill = new Bill()
                {
                    BillId = billId,
                    TotalPrice = (float)dr["total_price"],
                    Vat = (float)dr["vat"],
                    GuestNumber = (int)dr["guest_number"],
                    OrderId = (int)dr["order_id"],
                    Feedback = dr["feedback"].ToString(),
                    Tip = (float)dr["tip"],
                    SubBills = subBillDao.GetSubBillsByBillId(billId)
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
            SubBillDao subBillDao = new SubBillDao();
            DataRow dr = dataTable.Rows[0];
            int billId = (int)(dr["id"]);
            Bill bill = new Bill()
            {
                BillId = billId,
                TotalPrice = (float)dr["total_price"],
                Vat = (float)dr["vat"],
                GuestNumber = (int)dr["guest_number"],
                OrderId = (int)dr["order_id"],
                Feedback = dr["feedback"].ToString(),
                Tip = (float)dr["tip"],
                SubBills = subBillDao.GetSubBillsByBillId(billId)
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
        public List<OrderedMenuItemDTO> GetMenuItemsByBillId(int billId)
        {
            string query = @"
                SELECT mi.name, mi.price, oi.amount
                FROM Bill b
                JOIN [Order] o ON b.order_id = o.id
                JOIN Order_Item oi ON o.id = oi.order_id
                JOIN Menu_Item mi ON oi.menu_item_id = mi.id
                WHERE b.id = @billId;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@billId", billId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);

            List<OrderedMenuItemDTO> items = new List<OrderedMenuItemDTO>();

            foreach (DataRow row in table.Rows)
            {
                OrderedMenuItemDTO item = new OrderedMenuItemDTO
                {
                    Name = row["name"].ToString(),
                    Price = Convert.ToDecimal(row["price"]),
                    Amount = Convert.ToInt32(row["amount"])
                };
                items.Add(item);
            }

            return items;
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
