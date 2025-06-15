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
                listItem.SubItems.Add(itemTotal.ToString("C"));
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
            Bill bill = billService.GetBillByOrderId(orderId);

            if (bill != null)
            {
                billItems = billService.GetOrderedItemsForBill(bill.BillId);
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
                listItem.SubItems.Add(itemTotal.ToString("C"));
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

            subBillItems.Clear();

            FillListView(lstViewBill, billItems);
            UpdateSubBillListView();

            UpdateSubBillVatAndTotalLabels();
        }

        private void btnPayBill_Click(object sender, EventArgs e)
        {
            decimal billTotal = CalculateTotal(billItems, out decimal vatTotal);

            PaymentFormCompleteBill paymentForm = new PaymentFormCompleteBill(billTotal);
            paymentForm.ShowDialog();
        }

        // combine methods above and below

        private void btnPaySubBill_Click(object sender, EventArgs e)
        {
            decimal subBillTotalExclVat = CalculateTotal(subBillItems, out decimal vatTotal);

            // Create a Bill object with the sub-bill total
            SubBill subBill = new SubBill();

            subBill.Price = subBillTotalExclVat + vatTotal;

            // Pass the Bill object to the payment form
            PaymentFormSubBill paymentForm = new PaymentFormSubBill(subBill);
            paymentForm.ShowDialog();
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

        private void lblVatValueCompBill_Click(object sender, EventArgs e)
        {

        }
    }
}
