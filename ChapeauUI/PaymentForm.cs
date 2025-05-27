using Model;
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
        public PaymentForm()
        {
            InitializeComponent();
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

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {

        }

        private void FillListView(ListView listView, List<Bill> items)
        {
            listView.Items.Clear();

            foreach (var item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name); // first name and then sub items.
                listViewItem.SubItems.Add(item.Price.ToString("C"));
                listViewItem.SubItems.Add(item.Stock.ToString());
                listView.Items.Add(listViewItem);
            }
        }
    }
}
