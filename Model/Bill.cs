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
        // Calculated property for total price (including VAT)
        public decimal TotalPrice => OrderItems.Sum(item => item.MenuItem.Price * item.Count * (1 + item.MenuItem.Vat / 100));
        // Calculated property for VAT total
        public decimal Vat => OrderItems.Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        // Calculated property for low VAT (9%)
        public decimal LowVatTotal => OrderItems
            .Where(item => item.MenuItem.Vat == 9)
            .Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        // Calculated property for high VAT (21%)
        public decimal HighVatTotal => OrderItems
            .Where(item => item.MenuItem.Vat == 21)
            .Sum(item => (item.MenuItem.Price * item.Count * item.MenuItem.Vat) / 100);
        public int GuestNumber { get; set; }
        public int OrderId { get; set; }
        public decimal Tip { get; set; }
        public string Feedback { get; set; }


        public bool IsPaid { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
