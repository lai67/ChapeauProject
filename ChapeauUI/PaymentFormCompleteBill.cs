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
        private Bill bill;
        private int splitValue = 1; // Default split value
        private bool isLoaded = false;
        private bool forceClose = false; // Flag to allow form closure
        private int guestNumber = 1;
        private decimal remainingAmount;
        public PaymentFormCompleteBill(Bill bill)
        {
            InitializeComponent();
            this.FormClosing += PaymentFormCompleteBill_FormClosing; // ensures form cannot be closed until all guests are processed
            this.bill = bill;
            rdBtnTipPct0.Checked = true;
            remainingAmount = bill.TotalPrice;
            LoadTipButtons();
            LoadPaymentButtons();
            DisplayPrices();
            isLoaded = true; // Set isLoaded to true after initialization
        }
        private void DisplayPrices()
        {
            lblTotalPriceCompleteBill.Text = $"Total: €{remainingAmount:0.00}";

            var (splitPrice, priceWithTip) = GetCurrentGuestPrices();

            lblTotalForGuest.Text = $"Total For Guest {guestNumber}: €{priceWithTip:0.00}";
        }
        private decimal CalculatePriceWithTip(decimal baseValue)
        {
            decimal tipPercentage = 1;
            if (rdBtnTipPct0.Checked) tipPercentage = 1;
            else if (rdBtnTipPct2.Checked) tipPercentage = 1.02m;
            else if (rdBtnTipPct5.Checked) tipPercentage = 1.05m;
            else if (rdBtnTipPct7.Checked) tipPercentage = 1.07m;
            else if (rdBtnTipPct10.Checked) tipPercentage = 1.1m;
            else if (rdBtnTipPct12.Checked) tipPercentage = 1.12m;
            else if (rdBtnTipPct15.Checked) tipPercentage = 1.15m;
            else if (rdBtnTipPct20.Checked) tipPercentage = 1.2m;
            else if (rdBtnTipPct25.Checked) tipPercentage = 1.25m;
            else if (isLoaded)
            {
                MessageBox.Show("Please select a tip amount.");
            }
            return tipPercentage * baseValue;
        }
        private void LoadTipButtons()
        {
            // Attach CheckedChanged event handlers for all tip radio buttons
            rdBtnTipPct0.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct2.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct5.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct7.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct10.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct12.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct15.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct20.CheckedChanged += TipRadioButton_CheckedChanged;
            rdBtnTipPct25.CheckedChanged += TipRadioButton_CheckedChanged;
        }
        private void LoadPaymentButtons()
        {
            rdBtnCard.CheckedChanged += PaymentMethod_CheckedChanged;
            rdBtnCash.CheckedChanged += PaymentMethod_CheckedChanged;
        }
        private void TipRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DisplayPrices();
        }
        private void PaymentMethod_CheckedChanged(object sender, EventArgs e) { }
        private (decimal splitPrice, decimal priceWithTip) GetCurrentGuestPrices()
        {
            decimal splitPrice = remainingAmount / (splitValue - guestNumber + 1);
            decimal priceWithTip = splitPrice * CalculatePriceWithTip(1); // Tip multiplier

            return (splitPrice, priceWithTip);
        }
        private bool IsTipSelected()
        {
            return rdBtnTipPct0.Checked || rdBtnTipPct2.Checked || rdBtnTipPct5.Checked ||
                   rdBtnTipPct7.Checked || rdBtnTipPct10.Checked || rdBtnTipPct12.Checked ||
                   rdBtnTipPct15.Checked || rdBtnTipPct20.Checked || rdBtnTipPct25.Checked;
        }

        private void btnSplitDecrement_Click_1(object sender, EventArgs e)
        {
            if (splitValue > 1)
            {
                splitValue--;
                DisplayPrices();
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Split cannot be less than 1.");
            }
        }

        private void btnSplitIncrement_Click_1(object sender, EventArgs e)
        {
            if (!IsTipSelected())
            {
                MessageBox.Show("Please select a tip percentage before changing the split.");
                return;
            }
            if (splitValue < 10)
            {
                splitValue++;
                DisplayPrices();
                lblSplitValue.Text = splitValue.ToString();
            }
            else
            {
                MessageBox.Show("Maximum split value reached.");
            }
        }

        private void btnFinalizePayment_Click(object sender, EventArgs e)
        {
            var (splitPrice, priceWithTip) = GetCurrentGuestPrices();

            if (rdBtnCard.Checked)
            {
                MessageBox.Show($"Guest {guestNumber} paid {priceWithTip:0.00} by card including tip.");
            }
            else if (rdBtnCash.Checked)
            {
                MessageBox.Show($"Guest {guestNumber} paid {priceWithTip:0.00} with cash including tip.");
            }
            else
            {
                MessageBox.Show("Please select a payment method.");
                return;
            }

            remainingAmount -= splitPrice;

            if (guestNumber == 1)
            {
                btnSplitIncrement.Enabled = false;
                btnSplitDecrement.Enabled = false;
            }

            if (guestNumber == splitValue)
            {
                MessageBox.Show("All guests have been processed.");
                richTextBoxFeedback.Clear();
                forceClose = true; // Allow form to close
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Payment successful!");
                guestNumber++;
                richTextBoxFeedback.Clear();
                rdBtnTipPct0.Checked = true; // Reset tip selection for next guest
                DisplayPrices();
            }
        }
        private void PaymentFormCompleteBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose && guestNumber <= splitValue)
            {
                MessageBox.Show("You must process all guests before closing the form.");
                e.Cancel = true;
            }
        }
    }
}
