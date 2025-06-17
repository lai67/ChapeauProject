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
        private OrderService orderService;
        private BillService billService;
        private List<BillItem> billItems; // Cached full bill items with VAT
        private List<BillItem> subBillItems;
        private Bill currentBill;
        private SubBill currentSubBill;
        private SubBillService subBillService;
        public BillForm(int orderId)
        {
            InitializeComponent();
            billService = new BillService();
            subBillItems = new List<BillItem>();
            subBillService = new SubBillService();
            this.orderId = orderId;
        }
        private void FillListView(ListView listView, List<BillItem> items)
        {
            listView.Items.Clear();
            foreach (var item in items)
            {
                decimal itemTotal = item.Price * item.Amount;
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(item.Amount.ToString());
                listItem.SubItems.Add(itemTotal.ToString("C"));
                listView.Items.Add(listItem);
            }
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            LoadColumns(lstViewBill);
            LoadColumns(listViewSubBill);

            // Only load the bill if it exists
            currentBill = billService.GetBillByOrderId(orderId);

            if (currentBill != null)
            {
                billItems = billService.GetOrderedItemsForBill(currentBill.BillId);
            }
            else
            {
                // Bill does not exist yet, order items are being loaded for display
                // I may need a method to get order items by orderId if not present
                billItems = billService.GetOrderedItemsForBill(orderId);
            }

            FillListView(lstViewBill, billItems);
            UpdateBillVatAndTotalLabels();
        }
        private void btnAddToSubBill_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedBillItem(out string name, out decimal price))
                return;

            BillItem mainItem = FindMainBillItem(name);
            if (mainItem == null)
            {
                MessageBox.Show("No more of this item left to add.");
                return;
            }

            DecrementMainBillItem(mainItem);
            AddOrIncrementSubBillItem(name, price, mainItem);
            RemoveMainBillItemIfZero(mainItem);
            UpdateBillAndSubBillViews();
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

            BillItem subItem = FindSubBillItem(name);

            if (subItem != null)
            {
                subItem.Amount--;
                if (subItem.Amount <= 0)
                {
                    subBillItems.Remove(subItem);
                }

                AddOrIncrementMainBillItem(name, subItem.Price, subItem.Vat);
                UpdateBillAndSubBillViews();
            }
        }

        private void btnRemoveAllFromSubBill_Click(object sender, EventArgs e)
        {
            RestoreAllSubBillItemsToMainBill();
            subBillItems.Clear();
            UpdateBillAndSubBillViews();
        }

        private void btnPayBill_Click(object sender, EventArgs e)
        {
            if (IsBillEmpty())
            {
                ShowBillEmptyMessage();
                return;
            }

            decimal billTotalExclVat = CalculateTotal(billItems, out decimal vatTotal);
            // If the bill does not exist, create it now with correct values
            if (currentBill == null)
            {
                int nextId = billService.GetNextBillId(); // Assuming you have a method to get the next ID
                currentBill = new Bill
                {
                    BillId = nextId,
                    OrderId = orderId,
                    TotalPrice = billTotalExclVat + vatTotal,
                    Vat = vatTotal,
                    IsPaid = false
                };
                billService.CreateBill(currentBill);
            }
            else
            {
                // Update the existing bill with correct values
                currentBill.TotalPrice = billTotalExclVat + vatTotal;
                currentBill.Vat = vatTotal;
                billService.UpdateBill(currentBill);
            }

            if (!ShowCompleteBillPaymentForm(currentBill))
                return;

            // After payment, mark as paid and update in DB
            currentBill.IsPaid = true;
            billService.UpdateBill(currentBill); // Uncomment if you have this method

            ClearBill();
            AfterBillPaid(currentBill);
        }

        // combine methods above and below

        private void btnPaySubBill_Click(object sender, EventArgs e)
        {
            if (IsSubBillEmpty())
            {
                ShowSubBillEmptyMessage();
                return;
            }

            decimal subBillTotalExclVat = CalculateTotal(subBillItems, out decimal vatTotal);

            // If the sub-bill does not exist, create it now with correct values
            if (currentSubBill == null)
            {
                int nextId = subBillService.GetNextSubBillId(); // Assuming you have a method to get the next ID
                currentSubBill = new SubBill
                {
                    SubBillId = nextId,
                    BillId = currentBill?.BillId ?? 0, // Make sure to set the correct BillId
                    Price = subBillTotalExclVat + vatTotal,
                    Vat = vatTotal,
                    IsPaid = false
                };
                // You need to have a SubBillService instance, similar to BillService
                subBillService.CreateSubBill(currentSubBill);
            }
            else
            {
                // Update the existing sub-bill with correct values
                currentSubBill.Price = subBillTotalExclVat + vatTotal;
                currentSubBill.Vat = vatTotal;
                subBillService.UpdateSubBill(currentSubBill);
            }

            if (!ShowSubBillPaymentForm(currentSubBill))
                return;

            // After payment, mark as paid and update in DB
            currentSubBill.IsPaid = true;
            var subBillServiceFinal = new SubBillService();
            subBillServiceFinal.UpdateSubBill(currentSubBill);

            ClearSubBill();
            AfterSubBillPaid(currentSubBill);
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
            var (subBillTotal, vatTotal) = GetTotalAndVat(subBillItems);
            var (lowVatTotal, highVatTotal) = GetLowAndHighVat(subBillItems);
            SetVatAndTotalLabels(
                lblSubBillTotalValue, lblVatValueSubBill, lblVatLowValueSubBill, lblVatHighValueSubBill,
                subBillTotal, vatTotal, lowVatTotal, highVatTotal);
        }
        private void UpdateBillVatAndTotalLabels()
        {
            var (billTotal, vatTotal) = GetTotalAndVat(billItems);
            var (lowVatTotal, highVatTotal) = GetLowAndHighVat(billItems);
            SetVatAndTotalLabels(
                lblTotalPriceValueBill, lblVatValueCompBill, lblVatLowBillValue, lblVatHighBillValue,
                billTotal, vatTotal, lowVatTotal, highVatTotal);
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
            if (currentBill != null && currentBill.IsPaid &&
                currentSubBill != null && currentSubBill.IsPaid)
            {
                orderService.SetOrderCreated(orderId, true);

                MessageBox.Show("Both bills are paid. Form is closing.");
                return true; // Both bills are paid, allow closing
            }
            else
            {
                return false; // Prevent closing if bills are not paid
            }
        }
        private void LoadColumns(ListView listView)
        {
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.Columns.Clear();
            listView.Columns.Add("Name", 171);
            listView.Columns.Add("Price", 85);
            listView.Columns.Add("Amount", 85);
            listView.Columns.Add("Total", 85);
        }
        private BillItem FindMainBillItem(string name)
        {
            return billItems.FirstOrDefault(i => i.Name == name && i.Amount > 0);
        }

        private void DecrementMainBillItem(BillItem mainItem)
        {
            mainItem.Amount--;
        }

        private void AddOrIncrementSubBillItem(string name, decimal price, BillItem mainItem)
        {
            BillItem subItem = subBillItems.FirstOrDefault(i => i.Name == name);
            if (subItem != null)
            {
                subItem.Amount++;
            }
            else
            {
                subBillItems.Add(new BillItem
                {
                    Name = name,
                    Price = price,
                    Vat = mainItem.Vat,
                    Amount = 1
                });
            }
        }

        private void RemoveMainBillItemIfZero(BillItem mainItem)
        {
            if (mainItem.Amount == 0)
            {
                billItems.Remove(mainItem);
            }
        }
        private bool TryGetSelectedBillItem(out string name, out decimal price)
        {
            name = null;
            price = 0;
            if (lstViewBill.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to add.");
                return false;
            }
            var selectedItem = lstViewBill.SelectedItems[0];
            name = selectedItem.SubItems[0].Text;
            price = decimal.Parse(selectedItem.SubItems[1].Text, NumberStyles.Currency);
            return true;
        }
        private void UpdateBillAndSubBillViews()
        {
            FillListView(lstViewBill, billItems);
            FillListView(listViewSubBill, subBillItems);
            UpdateBillVatAndTotalLabels();
            UpdateSubBillVatAndTotalLabels();
        }
        private BillItem FindSubBillItem(string name)
        {
            return subBillItems.FirstOrDefault(i => i.Name == name);
        }

        private void AddOrIncrementMainBillItem(string name, decimal price, decimal vat, int amount = 1)
        {
            BillItem mainItem = billItems.FirstOrDefault(i => i.Name == name);
            if (mainItem != null)
            {
                mainItem.Amount += amount;
            }
            else
            {
                billItems.Add(new BillItem
                {
                    Name = name,
                    Price = price,
                    Vat = vat,
                    Amount = amount
                });
            }
        }
        private void RestoreAllSubBillItemsToMainBill()
        {
            foreach (var subItem in subBillItems)
            {
                AddOrIncrementMainBillItem(subItem.Name, subItem.Price, subItem.Vat, subItem.Amount);
            }
        }
        private bool IsBillEmpty()
        {
            return billItems.Count == 0;
        }

        private void ShowBillEmptyMessage()
        {
            MessageBox.Show("Bill is empty. Please add items before paying.");
        }

        private Bill CreateBillForPayment(decimal total)
        {
            return new Bill
            {
                TotalPrice = total
            };
        }

        private bool ShowCompleteBillPaymentForm(Bill bill)
        {
            PaymentFormCompleteBill paymentForm = new PaymentFormCompleteBill(bill);
            paymentForm.ShowDialog();
            return !paymentForm.UserCancelled;
        }

        private void ClearBill()
        {
            billItems.Clear();
            FillListView(lstViewBill, billItems);
            UpdateBillVatAndTotalLabels();
        }

        private void AfterBillPaid(Bill bill)
        {
            MessageBox.Show("bill has been paid.");
            currentBill = bill;
            currentBill.IsPaid = true; // Mark the bill as paid
            if (BillsPaid(currentBill, currentSubBill))
            {
                LoadColumns(lstViewBill);
                LoadColumns(listViewSubBill);
                this.Close(); // Close the form if both bills are paid
            }
        }
        private bool IsSubBillEmpty()
        {
            return subBillItems.Count == 0;
        }

        private void ShowSubBillEmptyMessage()
        {
            MessageBox.Show("Sub-bill is empty. Please add items before paying.");
        }

        private SubBill CreateSubBillForPayment(decimal total)
        {
            return new SubBill
            {
                Price = total
            };
        }

        private bool ShowSubBillPaymentForm(SubBill subBill)
        {
            PaymentFormSubBill paymentForm = new PaymentFormSubBill(subBill);
            paymentForm.ShowDialog();
            return !paymentForm.UserCancelled;
        }

        private void ClearSubBill()
        {
            subBillItems.Clear();
            FillListView(listViewSubBill, subBillItems);
            UpdateSubBillVatAndTotalLabels();
        }

        private void AfterSubBillPaid(SubBill subBill)
        {
            MessageBox.Show("Sub-bill has been paid.");
            currentSubBill = subBill;
            currentSubBill.IsPaid = true; // Mark the sub-bill as paid
            if (BillsPaid(currentBill, currentSubBill))
            {
                this.Close(); // Close the form if both bills are paid
            }
        }
        private (decimal total, decimal vatTotal) GetTotalAndVat(List<BillItem> items)
        {
            decimal vatTotal;
            decimal total = CalculateTotal(items, out vatTotal);
            return (total, vatTotal);
        }

        private (decimal lowVatTotal, decimal highVatTotal) GetLowAndHighVat(List<BillItem> items)
        {
            decimal lowVatTotal, highVatTotal;
            CalculateLowAndHighVat(items, out lowVatTotal, out highVatTotal);
            return (lowVatTotal, highVatTotal);
        }
        private void SetVatAndTotalLabels(
            Label totalLabel, Label vatLabel, Label lowVatLabel, Label highVatLabel,
            decimal total, decimal vatTotal, decimal lowVatTotal, decimal highVatTotal)
        {
            totalLabel.Text = $"€{(total + vatTotal):0.00}";
            vatLabel.Text = $"€{vatTotal:0.00}";
            lowVatLabel.Text = $"€{lowVatTotal:0.00}";
            highVatLabel.Text = $"€{highVatTotal:0.00}";
        }
    }
}
