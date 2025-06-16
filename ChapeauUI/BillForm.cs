using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class BillForm : Form
    {
        private int orderId;
        private BillService billService;
        private List<BillItem> billItems; // Cached full bill items with VAT
        private List<BillItem> subBillItems;
        private Bill currentBill;
        private SubBill currentSubBill;
        public BillForm(int orderId)
        {
            InitializeComponent();
            billService = new BillService();
            subBillItems = new List<BillItem>();
            this.orderId = orderId;
        }
        private void FillListView(ListView listView, List<BillItem> billItems)
        {
            listView.Items.Clear();

            decimal vatTotal;
            decimal vatHighTotal;
            decimal billTotal = CalculateTotal(billItems, out vatTotal);
            decimal vatLowTotal = CalculateLowAndHighVat(billItems, out vatLowTotal, out vatHighTotal);

            foreach (var item in billItems)
            {
                decimal itemTotal = item.Price * item.Amount;
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(item.Amount.ToString());
                listView.Items.Add(listItem);
            }

            lblTotalPriceValueBill.Text = $"€{(billTotal + vatTotal):0.00}"; // Grand total (including VAT)
            lblVatValueCompBill.Text = $"€{vatTotal:0.00}";
            lblVatLowBillValue.Text = $"€{vatLowTotal:0.00}";
            lblVatHighBillValue.Text = $"€{vatHighTotal:0.00}";
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            LoadBillColumns();
            LoadSubBillColumns();
            // Get the bill related to this order
            currentBill = billService.GetBillByOrderId(orderId);

            if (currentBill != null)
            {
                billItems = billService.GetOrderedItemsForBill(currentBill.BillId);
                FillListView(lstViewBill, billItems);
            }
            else
            {
                MessageBox.Show("No bill found for this order.");
            }
        }
        private void LoadBillColumns()
        {
            // Setup ListView columns
            lstViewBill.View = View.Details;
            lstViewBill.FullRowSelect = true;
            lstViewBill.Columns.Clear();
            lstViewBill.Columns.Add("Name", 171);
            lstViewBill.Columns.Add("Price", 85);
            lstViewBill.Columns.Add("Amount", 85);
            lstViewBill.Columns.Add("Total", 85);
        }

        // create method LoadColumns with a listView parameter

        private void LoadSubBillColumns()
        {
            // Setup ListView columns for sub-bill
            listViewSubBill.View = View.Details;
            listViewSubBill.FullRowSelect = true;
            listViewSubBill.Columns.Clear();
            listViewSubBill.Columns.Add("Name", 171);
            listViewSubBill.Columns.Add("Price", 85);
            listViewSubBill.Columns.Add("Amount", 85);
            listViewSubBill.Columns.Add("Total", 85);
        }
        private void btnAddToSubBill_Click(object sender, EventArgs e)
        {
            if (lstViewBill.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to add.");
                return;
            }

            var selectedItem = lstViewBill.SelectedItems[0];

            string name = selectedItem.SubItems[0].Text;
            decimal price = decimal.Parse(selectedItem.SubItems[1].Text, NumberStyles.Currency);

            // Check if the item already exists in the sub-bill
            BillItem mainItem = billItems.FirstOrDefault(i => i.Name == name && i.Amount > 0);

            if (mainItem == null)
            {
                MessageBox.Show("No more of this item left to add.");
                return;
            }

            // decrement the amount in the main bill
            mainItem.Amount--;

            // Add or increment in sub-bill
            BillItem subItem = subBillItems.FirstOrDefault(i => i.Name == name);

            if (subItem != null)
            {
                // If item already exists in sub-bill, increment the amount
                subItem.Amount++;
            }
            else
            {
                // If item does not exist, create a new one
                BillItem newItem = new BillItem
                {
                    Name = name,
                    Price = price,
                    Vat = mainItem.Vat,  // ✅ Correct VAT
                    Amount = 1
                };
                subBillItems.Add(newItem);
            }

            // Remove from main bill if amount is zero
            if (mainItem.Amount == 0)
            {
                billItems.Remove(mainItem);
            }

            FillListView(lstViewBill, billItems);
            UpdateSubBillListView();
            UpdateSubBillVatAndTotalLabels();
        }
        private void UpdateSubBillListView()
        {
            listViewSubBill.Items.Clear();

            foreach (var item in subBillItems)
            {
                decimal itemTotal = item.Price * item.Amount; // Exclude VAT here
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(item.Amount.ToString());
                listViewSubBill.Items.Add(listItem);
            }
        }
        private void UpdateBillListView()
        {
            lstViewBill.Items.Clear();

            foreach (var item in billItems)
            {
                decimal itemTotal = item.Price * item.Amount; // Exclude VAT here
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(item.Amount.ToString());
                listViewSubBill.Items.Add(listItem);
            }
        }
        private void btnRemoveFromSubBill_Click(object sender, EventArgs e)
        {
            if (listViewSubBill.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.");
                return;
            }

            var selectedItem = listViewSubBill.SelectedItems[0];
            string name = selectedItem.SubItems[0].Text;

            BillItem subItem = subBillItems.FirstOrDefault(i => i.Name == name);

            if (subItem != null)
            {
                subItem.Amount--;
                if (subItem.Amount <= 0)
                {
                    subBillItems.Remove(subItem);
                }

                // increment in main bill
                BillItem mainItem = billItems.FirstOrDefault(i => i.Name == name);
                if (mainItem != null)
                {
                    mainItem.Amount++;
                }
                else
                {
                    billItems.Add(new BillItem
                    {
                        Name = name,
                        Price = subItem.Price,
                        Vat = subItem.Vat,
                        Amount = 1 // Add back with 1 amount
                    });
                }
                FillListView(lstViewBill, billItems);
                UpdateSubBillListView();
                UpdateSubBillVatAndTotalLabels();
            }
        }

        private void btnRemoveAllFromSubBill_Click(object sender, EventArgs e)
        {
            // Restore all sub-bill items to the main bill
            foreach (var subItem in subBillItems)
            {
                var mainItem = billItems.FirstOrDefault(i => i.Name == subItem.Name);
                if (mainItem != null)
                {
                    mainItem.Amount += subItem.Amount;
                }
                else
                {
                    // Add back as a new item if it doesn't exist in the main bill
                    billItems.Add(new BillItem
                    {
                        Name = subItem.Name,
                        Price = subItem.Price,
                        Vat = subItem.Vat,
                        Amount = subItem.Amount
                    });
                }
            }

            // After payment, clear the sub-bill
            subBillItems.Clear();

            FillListView(lstViewBill, billItems);
            UpdateSubBillListView();

            UpdateSubBillVatAndTotalLabels();
        }

        private void btnPayBill_Click(object sender, EventArgs e)
        {
            decimal billTotalExclVat = CalculateTotal(billItems, out decimal vatTotal);

            if (billItems.Count == 0)
            {
                MessageBox.Show("Bill is empty. Please add items before paying.");
                return;
            }

            // Create and show the bill payment form
            Bill bill = new Bill
            {
                TotalPrice = billTotalExclVat + vatTotal
            };

            PaymentFormCompleteBill paymentForm = new PaymentFormCompleteBill(bill);
            paymentForm.ShowDialog();

            if (paymentForm.UserCancelled)
            {
                return;
            }

            // ✅ After payment, clear the sub-bill
            billItems.Clear();
            UpdateBillListView();
            UpdateBillVatAndTotalLabels();
            MessageBox.Show("bill has been paid.");
            currentBill = bill;
            currentBill.IsPaid = true; // Mark the bill as paid
            if (BillsPaid(currentBill, currentSubBill))
            {
                this.Close(); // Close the form if both bills are paid
            }
        }

        // combine methods above and below

        private void btnPaySubBill_Click(object sender, EventArgs e)
        {
            decimal subBillTotalExclVat = CalculateTotal(subBillItems, out decimal vatTotal);

            if (subBillItems.Count == 0)
            {
                MessageBox.Show("Sub-bill is empty. Please add items before paying.");
                return;
            }

            // Create and show the sub-bill payment form
            SubBill subBill = new SubBill
            {
                Price = subBillTotalExclVat + vatTotal
            };

            PaymentFormSubBill paymentForm = new PaymentFormSubBill(subBill);
            paymentForm.ShowDialog();

            // Check if the user canceled the payment
            if (paymentForm.UserCancelled)
            {
                return;
            }

            // After payment, clear the sub-bill
            subBillItems.Clear();
            UpdateSubBillListView();
            UpdateSubBillVatAndTotalLabels();

            MessageBox.Show("Sub-bill has been paid.");
            currentSubBill = subBill;
            currentSubBill.IsPaid = true; // Mark the bill as paid
            if (BillsPaid(currentBill, currentSubBill))
            {
                this.Close(); // Close the form if both bills are paid
            }
        }
        private decimal CalculateTotal(List<BillItem> items, out decimal vatTotal)
        {
            decimal total = 0;
            vatTotal = 0;

            foreach (var item in items)
            {
                decimal itemTotal = item.Price * item.Amount;
                decimal itemVat = (itemTotal * item.Vat) / 100;
                total += itemTotal;
                vatTotal += itemVat;
            }

            return total;
        }
        private void UpdateSubBillVatAndTotalLabels()
        {
            decimal vatTotal;
            decimal highVatTotal;
            decimal subBillTotal = CalculateTotal(subBillItems, out vatTotal);
            decimal lowVatTotal = CalculateLowAndHighVat(subBillItems, out lowVatTotal, out highVatTotal);
            lblSubBillTotalValue.Text = $"€{(subBillTotal + vatTotal):0.00}";
            lblVatValueSubBill.Text = $"€{vatTotal:0.00}";
            lblVatLowValueSubBill.Text = $"€{lowVatTotal:0.00}"; // Low VAT total (9%)
            lblVatHighValueSubBill.Text = $"€{highVatTotal:0.00}"; // High VAT total (21%)
        }
        private void UpdateBillVatAndTotalLabels()
        {
            decimal vatTotal;
            decimal highVatTotal;
            decimal billTotal = CalculateTotal(billItems, out vatTotal);
            decimal lowVatTotal = CalculateLowAndHighVat(billItems, out lowVatTotal, out highVatTotal);
            lblTotalPriceValueBill.Text = $"€{(billTotal + vatTotal):0.00}";
            lblVatValueCompBill.Text = $"€{vatTotal:0.00}";
            lblVatLowBillValue.Text = $"€{lowVatTotal:0.00}"; // Low VAT total (9%)
            lblVatHighBillValue.Text = $"€{highVatTotal:0.00}"; // High VAT total (21%)
        }
        private decimal CalculateLowAndHighVat(List<BillItem> items, out decimal lowVatTotal, out decimal highVatTotal)
        {
            lowVatTotal = 0;
            highVatTotal = 0;

            foreach (var item in items)
            {
                decimal itemTotal = item.Price * item.Amount;
                // Check for low VAT (9%)
                if (item.Vat == 9)
                {
                    lowVatTotal += (itemTotal * item.Vat) / 100;
                }
                // Check for high VAT (21%)
                else if (item.Vat == 21)
                {
                    highVatTotal += (itemTotal * item.Vat) / 100;
                }
                // Optionally handle other VAT rates if needed
            }

            return lowVatTotal;
        }
        private bool BillsPaid(Bill currentBill, SubBill currentSubBill)
        {
            if (currentBill.IsPaid && currentSubBill.IsPaid)
            {
                MessageBox.Show("Both bills are paid. Form is closing.");
                return true; // Both bills are paid, allow closing
            }
            else
            {
                return false; // Prevent closing if bills are not paid
            }
        }
    }
}
