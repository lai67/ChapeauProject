using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Bill
    {
        public int BillId { get; set; }
        public float TotalPrice { get; set; }
        public decimal Vat { get; set; }
        public int GuestNumber { get; set; }
        public int OrderId { get; set; }
        public float Tip { get; set; }
        public string Feedback { get; set; }
        //navigation property
        public List<SubBill> SubBills { get; set; }
    }
}
