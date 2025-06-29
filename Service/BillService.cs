using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace Service
{
    public class BillService
    {
        private BillDao billDao;
        public BillService()
        {
            // initalize the DAO to access the database
            billDao = new BillDao();
        }
        /*public List<Bill> GetAllBills()
        {
            return billDao.GetAllBills();
        }*/

        // retrieves a single bill by its ID
        /*public Bill GetBillById(int billId)
        {
            var bill = billDao.GetBillById(billId);
            if (bill != null)
                bill.SubBills = subBillService.GetSubBillsByBillId(bill.BillId);
            return bill;
        }*/
        public Bill GetBillByOrderId(int orderId)
        {
            return billDao.GetBillByOrderId(orderId);
        }
        // gets the items ordered in a bill
        // shows item name, price, and amount
        /*public List<OrderItem> GetOrderedItemsForBill(int billId)
        {
            return billDao.GetOrderItemsByBillId(billId);
        }*/
        // creates a new bill and saves it to the database
        public void CreateBill(Bill bill)
        {
            billDao.CreateBill(bill);
        }
        public void UpdateBill(Bill bill)
        { 
            billDao.UpdateBill(bill);
        }
        // deletes a bill from the database using its ID
        /*public void DeleteBill(int billId)
        {
            billDao.DeleteBill(billId);
        }*/
        public int GetNextBillId()
        {
            return billDao.GetNextBillId();
        }

        // not needed as the bill is already validated upon the form loading

        /*public Bill EnsureBillExists(Bill bill, int orderId)
        {
            if (bill == null)
            {
                int nextId = GetNextBillId();
                bill = new Bill
                {
                    BillId = nextId,
                    OrderId = orderId,
                    OrderItems = new List<OrderItem>(),
                    IsPaid = false
                };
                CreateBill(bill);
            }
            else
            {
                UpdateBill(bill);
            }
            return bill;
        }*/
    }
}
