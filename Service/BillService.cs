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
        public List<Bill> GetAllBills()
        {
            return billDao.GetAllBills();
        }
        // retrieves a single bill by its ID
        public Bill GetBillById(int billId)
        {
            var bill = billDao.GetBillById(billId);
            if (bill != null)
                bill.SubBills = subBillService.GetSubBillsByBillId(bill.BillId);
            return bill;
        }
        public Bill GetBillByOrderId(int orderId)
        {
            return billDao.GetBillByOrderId(orderId);
        }
        // gets the items ordered in a bill
        // shows item name, price, and amount
        public List<OrderedMenuItemDTO> GetOrderedItemsForBill(int billId)
        {
            return billDao.GetMenuItemsByBillId(billId);
        }
        // creates a new bill and saves it to the database
        public void CreateBill(Bill bill)
        {
            billDao.CreateBill(bill);
        }
        // updates an existing bill in the databasecr
        public void UpdateBill(Bill bill)
        { 
            billDao.UpdateBill(bill);
        }
        // deletes a bill from the database using its ID
        public void DeleteBill(int billId)
        {
            billDao.DeleteBill(billId);
        } 
    }
}
