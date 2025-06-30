using Microsoft.Identity.Client;
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
        public int BillId { get; set; }
        public decimal Price => OrderItems.Sum(item => item.MenuItem.Price * item.Count * (1 + item.MenuItem.Vat / 100));
        public decimal Vat => OrderItems.Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        public decimal LowVatTotal => OrderItems
            .Where(item => item.MenuItem.Vat == 9)
            .Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        public decimal HighVatTotal => OrderItems
            .Where(item => item.MenuItem.Vat == 21)
            .Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        public decimal Tip { get; set; }
        public string Feedback { get; set; }
        public Bill Bill { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
