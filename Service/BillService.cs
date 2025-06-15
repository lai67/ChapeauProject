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
        private SubBillService subBillService;
        public BillService()
        {
            // initalize the DAO to access the database
            billDao = new BillDao();
            subBillService = new SubBillService();
        }
        // gets all bills
        /* public List<Bill> GetAllBills()
        {
            return billDao.GetAllBills();
        }*/

        // retrieves a single bill by its ID
        /*ublic Bill GetBillById(int billId)
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
        public List<BillItem> GetOrderedItemsForBill(int billId)
        {
            List<OrderItem> orderItems = billDao.GetOrderItemsByBillId(billId);

            return orderItems.Select(o => new BillItem
            {
                Name = o.MenuItem.Name,
                Price = o.MenuItem.Price,
                Vat = o.MenuItem.Vat,
                Amount = o.Count
            }).ToList();
        }
        // creates a new bill and saves it to the database
        public void CreateBill(Bill bill)
        {
            billDao.CreateBill(bill);
        }
        // updates an existing bill in the databasecr
        /*public void UpdateBill(Bill bill)
        { 
            billDao.UpdateBill(bill);
        }*/
        // deletes a bill from the database using its ID
        /*public void DeleteBill(int billId)
        {
            billDao.DeleteBill(billId);
        }*/ 
    }
}
