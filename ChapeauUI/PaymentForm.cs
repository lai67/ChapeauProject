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

        private void FillListView(ListView listView, List<Bill> items)
        {

        }
    }
}
