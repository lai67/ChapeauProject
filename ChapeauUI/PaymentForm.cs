using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class PaymentForm : Form
    {
        private BillService billService;
        private List<Bill> bills;
        public PaymentForm()
        {
            InitializeComponent();
            billService = new BillService();
            bills = billService.GetAllBills();
        }
        private int splitValue = 0;
        private void btnSplitIncrement_Click(object sender, EventArgs e)
        {
            splitValue++;
            lblSplitValue.Text = splitValue.ToString();
        }

        private void btnSplitDecrement_Click(object sender, EventArgs e)
        {
            splitValue--;
            lblSplitValue.Text = splitValue.ToString();
        }

        private void FillListView(ListView listView, List<Bill> bills)
        {
            listView.Items.Clear();

            foreach (var bill in bills)
            {
                // Get ordered items for this bill
                List<OrderedMenuItemDTO> orderedItems = billService.GetOrderedItemsForBill(bill.BillId);

                foreach (var item in orderedItems)
                {
                    ListViewItem listItem = new ListViewItem(item.Name);
                    listItem.SubItems.Add(item.Price.ToString("C")); // "C" formats as currency
                    listItem.SubItems.Add(item.Amount.ToString());
                    listItem.SubItems.Add((item.Price * item.Amount).ToString("C")); // total for that item

                    listView.Items.Add(listItem);
                }
            }
        }
    }
}
