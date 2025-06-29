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
using System.Drawing.Text;

namespace ChapeauUI
{
    public partial class PaymentFormCompleteBill : Form
    {
        private Bill bill;
        private List<decimal> guestTips;
        private bool userCancelled = false;
        private int paymentsProcessed = 0; // Number of guests already processed
        public bool UserCancelled
        {
            get { return userCancelled; }
            set { userCancelled = value; }
        }
        public PaymentFormCompleteBill(Bill bill)
        {
            InitializeComponent();
            this.FormClosing += PaymentFormCompleteBill_FormClosing; // ensures form cannot be closed until all guests are processed
            this.bill = bill;
            rdBtnTipPct0.Checked = true;
            guestTips = new List<decimal>();
            LoadTipButtons();
            LoadPaymentButtons();
            DisplayPrices();
        }
        private void DisplayPrices()
        {
            int splitValue = GetSplitValue();
            int currentGuestNumber = GetCurrentGuestNumber();
            decimal remainingAmount = GetRemainingAmount(splitValue, currentGuestNumber);

            lblTotalPriceCompleteBill.Text = $"Total: €{remainingAmount:0.00}";

            var (splitPrice, priceWithTip) = GetCurrentGuestPrices(currentGuestNumber, splitValue, remainingAmount);

            lblTotalForGuest.Text = $"Total For Guest {currentGuestNumber}: €{priceWithTip:0.00}";
        }
        private decimal CalculatePriceWithTip(decimal baseValue, bool isLoaded = true)
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
        private (decimal splitPrice, decimal priceWithTip) GetCurrentGuestPrices(int guestNumber, int splitValue, decimal remainingAmount)
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
            int splitValue = GetSplitValue();
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
            int splitValue = GetSplitValue();
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
            try
            {
                if (!TryGetPaymentInfo(out int splitValue, out int currentGuestNumber, out decimal remainingAmount, out decimal splitPrice, out decimal priceWithTip))
                    return;

                if (!ValidatePaymentMethod())
                    return;

                if (!TryValidateAndCalculateTip(splitPrice, out decimal tipForThisGuest))
                    return;

                ProcessPayment(priceWithTip, currentGuestNumber);

                paymentsProcessed++; // Move to next guest

                DisableSplitButtonsIfNeeded(splitValue);

                HandlePaymentCompletion(splitValue, currentGuestNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally log ex.ToString() for diagnostics
            }
        }
        private bool TryGetPaymentInfo(out int splitValue, out int currentGuestNumber, out decimal remainingAmount, out decimal splitPrice, out decimal priceWithTip)
        {
            splitValue = GetSplitValue();
            currentGuestNumber = GetCurrentGuestNumber();
            remainingAmount = GetRemainingAmount(splitValue, currentGuestNumber);

            var prices = GetCurrentGuestPrices(currentGuestNumber, splitValue, remainingAmount);
            splitPrice = prices.splitPrice;
            priceWithTip = prices.priceWithTip;

            return true;
        }
        private bool TryValidateAndCalculateTip(decimal splitPrice, out decimal tipForThisGuest)
        {
            decimal tipPercentage = GetSelectedTipPercentage();
            tipForThisGuest = splitPrice * tipPercentage;
            guestTips.Add(tipForThisGuest);
            return true;
        }

        private void DisableSplitButtonsIfNeeded(int splitValue)
        {
            if (splitValue > 1 && paymentsProcessed == 1)
            {
                btnSplitIncrement.Enabled = false;
                btnSplitDecrement.Enabled = false;
            }
        }

        private void HandlePaymentCompletion(int splitValue, int currentGuestNumber)
        {
            if (currentGuestNumber == splitValue)
            {
                bill.Tip = guestTips.Sum();
                CompleteAllPayments();
            }
            else
            {
                MessageBox.Show("Payment successful!");
                AdvanceToNextGuest();
            }
        }
        private void PaymentFormCompleteBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            int splitValue = GetSplitValue();
            bool forceClose = this.Tag as string == "ForceClose";
            if (!forceClose && paymentsProcessed < splitValue)
            {
                MessageBox.Show("You must process all guests before closing the form.");
                e.Cancel = true;
            }
        }
        private void btnCancelPayment_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to cancel the payment and return to the bill?",
                "Cancel Payment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                userCancelled = true;
                this.Tag = "ForceClose"; // Set Tag to indicate force close
                bill.IsPaid = false;
                this.Close();
            }
        }
        private void ProcessPayment(decimal priceWithTip, int guestNumber)
        {
            if (rdBtnCard.Checked)
                MessageBox.Show($"Guest {guestNumber} paid €{priceWithTip:0.00} by card including tip.");
            else if (rdBtnCash.Checked)
                MessageBox.Show($"Guest {guestNumber} paid €{priceWithTip:0.00} with cash including tip.");
        }
        private void AdvanceToNextGuest()
        {
            richTextBoxFeedback.Clear();
            rdBtnTipPct0.Checked = true;
            DisplayPrices();
        }
        private void CompleteAllPayments()
        {
            MessageBox.Show("All guests have been processed.");
            richTextBoxFeedback.Clear();
            this.Tag = "ForceClose";
            bill.IsPaid = true;
            userCancelled = false;
            this.Close();
        }
        private bool ValidatePaymentMethod()
        {
            if (!rdBtnCard.Checked && !rdBtnCash.Checked)
            {
                MessageBox.Show("Please select a payment method.");
                return false;
            }
            return true;
        }
        private decimal GetSelectedTipPercentage()
        {
            if (rdBtnTipPct0.Checked) return 0m;
            if (rdBtnTipPct2.Checked) return 0.02m;
            if (rdBtnTipPct5.Checked) return 0.05m;
            if (rdBtnTipPct7.Checked) return 0.07m;
            if (rdBtnTipPct10.Checked) return 0.10m;
            if (rdBtnTipPct12.Checked) return 0.12m;
            if (rdBtnTipPct15.Checked) return 0.15m;
            if (rdBtnTipPct20.Checked) return 0.20m;
            if (rdBtnTipPct25.Checked) return 0.25m;
            return 0m;
        }
        private int GetSplitValue()
        {
            if (int.TryParse(lblSplitValue.Text, out int value))
                return value;
            return 1; // Default fallback
        }
        private decimal GetRemainingAmount(int splitValue, int currentGuestNumber)
        {
            decimal splitPrice = bill.TotalPrice / splitValue;
            return bill.TotalPrice - (splitPrice * (currentGuestNumber - 1));
        }
        private int GetCurrentGuestNumber()
        {
            int splitValue = GetSplitValue();
            // Guest numbers are 1-based
            return paymentsProcessed + 1;
        }
    }
}
