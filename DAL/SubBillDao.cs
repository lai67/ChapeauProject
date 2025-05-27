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
            SubBill subBill = new SubBill()
            {
                SubBillId = (int)dr["id"],
                BillId = (int)dr["bill_id"],
                Price = (float)dr["price"],
                Vat = (float)dr["vat"],
                Feedback = dr["feedback"].ToString(),
                Tip = (float)dr["tip"],
            };
            return subBill;
        }
        public SubBill GetBillById(int subBillId)
        {
            string query = "SELECT id, bill_id, price, vat, tip, feedback FROM [SUB_BILL] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", subBillId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);
            return ReadSubBill(table);
        }

    }
}
