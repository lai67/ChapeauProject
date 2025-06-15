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
    public partial class PaymentFormSubBill : Form
    {
        private SubBill subBill;
        private int splitValue = 1; // Default split value
        public PaymentFormSubBill(SubBill subBill)
        {
            InitializeComponent();
            this.subBill = subBill;
            DisplayPricePerPerson();
        }
        private void btnSplitDecrement_Click(object sender, EventArgs e)
        {
            if (splitValue > 1)
            {
                splitValue--;
                DisplayPricePerPerson();
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Split cannot be less than 1.");
            }
        }
        private void btnSplitIncrement_Click(object sender, EventArgs e)
        {
            if (splitValue < 10)
            {
                splitValue++;
                DisplayPricePerPerson();
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Maximum split value reached.");
            }
        }
        private void DisplayPricePerPerson()
        {
            decimal pricePerPerson = subBill.Price / splitValue; // Adjust the price based on the split value
            lblTotalPriceSubBill.Text = $"Total Price: €{pricePerPerson:0.00}";
        }
        private void btnFinalizePayment_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Payment successful!");
        }
    }
}
