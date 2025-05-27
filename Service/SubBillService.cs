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
    }
}
