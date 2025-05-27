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
        // retrieves a single bill by it's ID
        public List<Bill> GetAllBills()
        {
            return billDao.GetAllBills();
        }
        // creates a new bill and saves it to the database
        public void CreateBill(Bill bill)
        {
            billDao.CreateBill(bill);
        }
        // updates an existing bill in the database
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
