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
    public class SubBillDao : BaseDao
    {
        public List<SubBill> GetAllSubBills()
        {
            string querySelect = "SELECT id, bill_id, price, vat, tip, feedback FROM [SUB_BILL] ORDER BY bill_id;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(querySelect, sqlParameters));
        }
        private List<SubBill> ReadTables(DataTable dataTable)
        {
            List<SubBill> subBills = new List<SubBill>();

            foreach (DataRow dr in dataTable.Rows)
            {
                SubBill subBill = new SubBill()
                {
                    SubBillId = (int)dr["id"],
                    BillId = (int)dr["bill_id"],
                    Price = (float)dr["price"],
                    Vat = (float)dr["vat"],
                    Feedback = dr["feedback"].ToString(),
                    Tip = (float)dr["tip"],
                };
                subBills.Add(subBill);
            }
            return subBills;
        }
        private SubBill ReadSubBill(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            DataRow dr = dataTable.Rows[0];
            int billId = (int)dr["bill_id"];
            SubBill subBill = new SubBill()
            {
                SubBillId = (int)dr["id"],
                BillId = billId,
                Price = (float)dr["price"],
                Vat = (float)dr["vat"],
                Feedback = dr["feedback"].ToString(),
                Tip = (float)dr["tip"],
                Bill = new BillDao().GetBillById(billId) // only if parent Bill needs to be loaded for SubBill
            };
            return subBill;
        }
        public SubBill GetSubBillById(int subBillId)
        {
            string query = "SELECT id, bill_id, price, vat, tip, feedback FROM [SUB_BILL] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", subBillId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);
            return ReadSubBill(table);
        }
        public List<SubBill> GetSubBillsByBillId(int billId)
        {
            string query = "SELECT id, bill_id, price, vat, tip, feedback FROM [SUB_BILL] WHERE bill_id = @bill_id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@bill_id", billId)
            };

            return ReadTables(ExecuteSelectQuery(query, parameters));
        }
        public List<OrderedMenuItemDTO> GetMenuItemsBySubBillId(int subBillId)
        {
            string query = @"
                SELECT mi.name, mi.price, oi.amount
                FROM Sub_Bill sb
                JOIN Bill b ON sb.bill_id = b.id
                JOIN [Order] o ON b.order_id = o.id
                JOIN Order_Item oi ON o.id = oi.order_id
                JOIN Menu_Item mi ON oi.menu_item_id = mi.id
                WHERE sb.id = @subBillId;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@subBillId", subBillId)
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

        public void CreateSubBill(SubBill subBill)
        {
            string query = @"INSERT INTO [SUB_BILL] (bill_id, price, vat, tip, feedback)
                     VALUES (@bill_id, @price, @vat, @tip, @feedback);";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@bill_id", subBill.BillId),
                new SqlParameter("@price", subBill.Price),
                new SqlParameter("@vat", subBill.Vat),
                new SqlParameter("@tip", subBill.Tip),
                new SqlParameter("@feedback", subBill.Feedback ?? (object)DBNull.Value)
            };
            ExecuteEditQuery(query, parameters);
        }
        public void UpdateSubBill(SubBill subBill)
        {
            string query = @"UPDATE [SUB_BILL]
                           SET bill_id = @bill_id
                           price = @price,
                           vat = @vat,
                           tip = @tip,
                           feedback = @feedback
                           WHERE id = @id;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@price", subBill.Price),
                new SqlParameter("@vat", subBill.Vat),
                new SqlParameter("@tip", subBill.Tip),
                new SqlParameter("@feedback", subBill.Feedback ?? (object)DBNull.Value),
                new SqlParameter("@id", subBill.SubBillId),
                new SqlParameter("@bill_id", subBill.BillId)
            };
            ExecuteEditQuery(query, parameters);
        }
        public void DeleteSubBill(int subBillId)
        {
            string query = "DELETE FROM [SUB_BILL] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", subBillId)
            };

            ExecuteEditQuery(query, parameters);
        }
    }
}
