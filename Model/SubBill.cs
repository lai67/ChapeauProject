using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SubBill
    {
        public int SubBillId { get; set; }
        public int BillId { get; set; } // Foreign key
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Tip { get; set; }
        public string Feedback { get; set; }

        // Navigation property
        public Bill Bill { get; set; }
    }
}
