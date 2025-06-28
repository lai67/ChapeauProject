using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SubBillService
    {
        private SubBillDao subBillDao;
        public SubBillService()
        {
            // initialize the DAO to access the database
            subBillDao = new SubBillDao();
        }
        // gets all sub bills
        public List<SubBill> GetAllSubBills(SubBill subBill)
        {
            return subBillDao.GetAllSubBills();
        }
        // retrieves a single bill by its ID
        public SubBill GetSubBillById(int subBillId)
        {
            return subBillDao.GetSubBillById(subBillId);
        }
        public List<SubBill> GetSubBillsByBillId(int billId)
        {
            return subBillDao.GetSubBillsByBillId(billId);
        }
        // gets the items in a SubBill
        // shows item name, price, and amount
        public List<OrderItem> GetOrderItemsBySubBillId(int subBillId)
        {
            return subBillDao.GetOrderItemsBySubBillId(subBillId);
        }
        // creates a new sub bill and saves it to the database
        public void CreateSubBill(SubBill subBill)
        {
            subBillDao.CreateSubBill(subBill);
        }
        // updates an existing sub bill in the database
        public void UpdateSubBill(SubBill subBill)
        {
            subBillDao.UpdateSubBill(subBill);
        }
        // deletes a sub bill from the database using its ID
        public void DeleteSubBill(int subBillId)
        {
            subBillDao.DeleteSubBill(subBillId);
        }
        public int GetNextSubBillId()
        {
            return subBillDao.GetNextSubBillId();
        }
        public SubBill EnsureSubBillExists(SubBill subBill, int billId)
        {
            if (subBill == null)
            {
                int nextId = GetNextSubBillId();
                subBill = new SubBill
                {
                    SubBillId = nextId,
                    BillId = billId,
                    OrderItems = new List<OrderItem>(),
                    IsPaid = false
                };
                CreateSubBill(subBill);
            }
            else
            {
                UpdateSubBill(subBill);
            }
            return subBill;
        }
    }
}
