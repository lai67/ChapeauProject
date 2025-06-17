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
        /*public List<Bill> GetAllBills()
        {
            string querySelect = "SELECT id, total_price, vat, guest_number, order_id, tip, feedback FROM [Bill] ORDER BY order_id;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(querySelect, sqlParameters));
        }*/
        /*private List<Bill> ReadTables(DataTable dataTable)
        {
            List<Bill> bills = new List<Bill>();
            SubBillDao subBillDao = new SubBillDao();

            foreach (DataRow dr in dataTable.Rows)
            {
                int billId = (int)dr["id"];

                Bill bill = new Bill()
                {
                    BillId = billId,
                    TotalPrice = (decimal)dr["total_price"],
                    Vat = (decimal)dr["vat"],
                    GuestNumber = (int)dr["guest_number"],
                    OrderId = (int)dr["order_id"],
                    Feedback = dr["feedback"].ToString(),
                    Tip = (decimal)dr["tip"],
                    SubBills = subBillDao.GetSubBillsByBillId(billId)
                };
                bills.Add(bill);
            }
            return bills;
        }*/
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
                TotalPrice = (decimal)dr["total_price"],
                Vat = (decimal)dr["vat"],
                GuestNumber = (int)dr["guest_number"],
                OrderId = (int)dr["order_id"],
                Feedback = dr["feedback"].ToString(),
                Tip = (decimal)dr["tip"],
                SubBills = subBillDao.GetSubBillsByBillId(billId)
            };
            return bill;
        }
        public Bill GetBillById(int billId)
        {
            string query = "SELECT id, total_price, vat, guest_number, order_id, tip, feedback FROM [Bill] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", billId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);
            return ReadBill(table);
        }
        public Bill GetBillByOrderId(int orderId)
        {
            string query = @"
                            SELECT b.id, b.total_price, b.vat, b.guest_number, b.order_id, b.tip, b.feedback
                            FROM [Bill] b
                            JOIN [ORDER] o ON b.order_id = o.id
                            WHERE b.order_id = @orderId AND o.isCreated = 0;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@orderId", orderId)
            };

            DataTable table = ExecuteSelectQuery(query, parameters);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return ReadBill(table); // Reuses your existing method
        }
        public List<OrderItem> GetOrderItemsByBillId(int billId)
        {
            string query = @"
                SELECT oi.id AS order_item_id, oi.count, oi.order_id, oi.comment, oi.status,
                mi.id AS menu_item_id, mi.name, mi.price, mi.vat, mi.item_category, mi.stock, mi.preparation_time, mi.menu_id
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

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (DataRow row in table.Rows)
            {
                // Build MenuItemModel first
                MenuItemModel menuItem = new MenuItemModel
                {
                    Id = Convert.ToInt32(row["menu_item_id"]),
                    Name = row["name"].ToString(),
                    Price = Convert.ToDecimal(row["price"]),
                    Vat = Convert.ToDecimal(row["vat"]),
                    Item_Category = row["item_category"].ToString(),
                    Stock = Convert.ToInt32(row["stock"]),
                    Preperation_Time = Convert.ToInt32(row["preparation_time"]),
                    Menu_Id = Convert.ToInt32(row["menu_id"])
                };

                // Then build OrderItem
                OrderItem orderItem = new OrderItem(
                    id: Convert.ToInt32(row["order_item_id"]),
                    menuItem: menuItem,
                    comment: row["comment"].ToString(),
                    orderStatus: Enum.Parse<OrderItem.OrderStatus>(row["status"].ToString()),
                    count: Convert.ToInt32(row["count"]),
                    orderId: Convert.ToInt32(row["order_id"])
                );

                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        public void CreateBill(Bill bill)
        {
            int nextId = GetNextBillId();
            string query = @"INSERT INTO [Bill] (id, total_price, vat, guest_number, order_id, tip, feedback)
                     VALUES (@id, @total_price, @vat, @guest_number, @order_id, @tip, @feedback);";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@id", nextId),
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
            string query = @"UPDATE Bill
                     SET total_price = @TotalPrice,
                         vat = @Vat,
                         guest_number = @GuestNumber,
                         feedback = @Feedback,
                         tip = @Tip
                     WHERE order_id = @OrderId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TotalPrice", bill.TotalPrice),
        new SqlParameter("@Vat", bill.Vat),
        new SqlParameter("@GuestNumber", bill.GuestNumber),
        new SqlParameter("@Feedback", bill.Feedback ?? (object)DBNull.Value),
        new SqlParameter("@Tip", bill.Tip),
        new SqlParameter("@OrderId", bill.OrderId)
            };
            ExecuteEditQuery(query, parameters);
        }

        /*public void DeleteBill(int billId)
        {
            string query = "DELETE FROM [Bill] WHERE id = @id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", billId)
            };

            ExecuteEditQuery(query, parameters);
        }*/
        public int GetNextBillId()
        {
            string query = "SELECT ISNULL(MAX(id), 0) + 1 FROM [Bill]";
            using (SqlCommand command = new SqlCommand(query, OpenConnection()))
            {
                int nextId = (int)command.ExecuteScalar();
                return nextId;
            }
        }
    }
}
