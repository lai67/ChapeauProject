using System;
using ChapeauUI;
using DAL;
using Model;
using Service;
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
    public partial class PaymentFormCompleteBill : Form
    {
        private decimal totalPrice;
        private int splitValue = 1; // Default split value
        public PaymentFormCompleteBill(decimal totalPrice)
        {
            InitializeComponent();
            this.totalPrice = totalPrice;
            lblTotalPriceCompBill.Text = $"€Total Price: €{totalPrice:0.00}";
        }
        private void btnFinalizePayment_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment successful!");
        }
        private void btnSplitDecrement_Click_1(object sender, EventArgs e)
        {
            if (splitValue > 1)
            {
                splitValue--;
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Split cannot be less than 1.");
            }
        }
        private void btnSplitIncrement_Click_1(object sender, EventArgs e)
        {
            if (splitValue < 10)
            {
                splitValue++;
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Maximum split value reached.");
            }
        }
    }
}
