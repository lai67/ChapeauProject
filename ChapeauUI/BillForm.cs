using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class BillForm : Form
    {
        private readonly int orderId;
        private List<OrderItem> billItems; // Cached full bill items with VAT
        private List<OrderItem> subBillItems;
        private Bill currentBill; // keep this
        private SubBill currentSubBill; // keep this too
        public BillForm(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            subBillItems = new List<OrderItem>();
            billItems = new List<OrderItem>();
        }
        private void FillListView(ListView listView, List<OrderItem> items)
        {
            listView.Items.Clear();
            foreach (var item in items)
            {
                decimal itemTotal = item.MenuItem.Price * item.Count;
                ListViewItem listItem = new ListViewItem(item.MenuItem.Name);
                listItem.SubItems.Add(item.MenuItem.Price.ToString("C"));
                listItem.SubItems.Add(item.Count.ToString());
                listItem.SubItems.Add(itemTotal.ToString("C"));
                listView.Items.Add(listItem);
            }
        }
        private void BillForm_Load(object sender, EventArgs e)
        {
            var billService = new BillService();

            LoadColumns(lstViewBill);
            LoadColumns(listViewSubBill);

            currentBill = billService.GetBillByOrderId(orderId);

            if (currentBill == null)
            {
                currentBill = new Bill
                {
                    OrderId = orderId,
                    TotalPrice = 0, 
                    Vat = 0,
                    GuestNumber = 1,
                    Tip = 0,
                    Feedback = ""
                };
                billService.CreateBill(currentBill);

                // Reload the bill to get the BillId assigned by the DB
                currentBill = billService.GetBillByOrderId(orderId);
            }

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

            OrderItem mainItem = FindMainBillItem(name);
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

            OrderItem subItem = FindSubBillItem(name);

            if (subItem != null)
            {
                subItem.Count--;
                if (subItem.Count <= 0)
                {
                    subBillItems.Remove(subItem);
                }

                AddOrIncrementMainBillItem(name, subItem.MenuItem.Price, subItem.MenuItem.Vat);
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
            var orderService = new OrderService();
            var billService = new BillService();

            if (IsBillEmpty())
            {
                ShowBillEmptyMessage();
                return;
            }

            decimal billTotalExclVat = CalculateTotal(billItems, out decimal vatTotal);
            EnsureBillExists(billTotalExclVat, vatTotal, billService);

            if (!ShowCompleteBillPaymentForm(currentBill))
                return;

            currentBill.IsPaid = true;
            billService.UpdateBill(currentBill);

            ClearBill();
            AfterBillPaid(currentBill, orderService);
        }

        // combine methods above and below

        private void btnPaySubBill_Click(object sender, EventArgs e)
        {
            var orderService = new OrderService();
            var subBillService = new SubBillService();

            if (IsSubBillEmpty())
            {
                ShowSubBillEmptyMessage();
                return;
            }

            decimal subBillTotalExclVat = CalculateTotal(subBillItems, out decimal vatTotal);
            EnsureSubBillExists(subBillTotalExclVat, vatTotal, subBillService);

            if (!ShowSubBillPaymentForm(currentSubBill))
                return;

            currentSubBill.IsPaid = true;
            subBillService.UpdateSubBill(currentSubBill);

            ClearSubBill();
            AfterSubBillPaid(currentSubBill, orderService);
        }
        private decimal CalculateTotal(List<OrderItem> items, out decimal vatTotal)
        {
            decimal total = 0;
            vatTotal = 0;

            foreach (var item in items)
            {
                decimal itemTotal = item.MenuItem.Price * item.Count;
                decimal itemVat = (itemTotal * item.MenuItem.Vat) / 100;
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
        private decimal CalculateLowAndHighVat(List<OrderItem> items, out decimal lowVatTotal, out decimal highVatTotal)
        {
            lowVatTotal = 0;
            highVatTotal = 0;

            foreach (var item in items)
            {
                decimal itemTotal = item.MenuItem.Price * item.Count;
                // Check for low VAT (9%)
                if (item.MenuItem.Vat == 9)
                {
                    lowVatTotal += (itemTotal * item.MenuItem.Vat) / 100;
                }
                // Check for high VAT (21%)
                else if (item.MenuItem.Vat == 21)
                {
                    highVatTotal += (itemTotal * item.MenuItem.Vat) / 100;
                }
                // Optionally handle other VAT rates if needed
            }

            return lowVatTotal;
        }
        private bool BillsPaid(Bill currentBill, SubBill currentSubBill, OrderService orderService)
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
        private OrderItem FindMainBillItem(string name)
        {
            return billItems.FirstOrDefault(i => i.MenuItem.Name == name && i.Count > 0);
        }

        private void DecrementMainBillItem(OrderItem mainItem)
        {
            mainItem.Count--;
        }
        private void AddOrIncrementSubBillItem(string name, decimal price, OrderItem mainItem)
        {
            OrderItem subItem = subBillItems.FirstOrDefault(i => i.MenuItem.Name == name);
            if (subItem != null)
            {
                subItem.Count++;
            }
            else
            {
                // Create a new MenuItemModel for the sub-bill item
                var menuItem = new MenuItemModel
                {
                    Name = name,
                    Price = price,
                    Vat = mainItem.MenuItem.Vat,
                    Item_Category = mainItem.MenuItem.Item_Category,
                    Menu_Id = mainItem.MenuItem.Menu_Id
                };

                subBillItems.Add(new OrderItem(
                    menuItem,
                    mainItem.Comment,
                    mainItem.orderStatus,
                    1, // count
                    mainItem.OrderId
                ));
            }
        }
        private void RemoveMainBillItemIfZero(OrderItem mainItem)
        {
            if (mainItem.Count == 0)
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
        private OrderItem FindSubBillItem(string name)
        {
            return subBillItems.FirstOrDefault(i => i.MenuItem.Name == name);
        }

        private void AddOrIncrementMainBillItem(string name, decimal price, decimal vat, int amount = 1)
        {
            OrderItem mainItem = billItems.FirstOrDefault(i => i.MenuItem.Name == name);
            if (mainItem != null)
            {
                mainItem.Count += amount;
            }
            else
            {
                // Create a new MenuItemModel for the main bill item
                var menuItem = new MenuItemModel
                {
                    Name = name,
                    Price = price,
                    Vat = vat
                    // Optionally set other properties if needed
                };

                billItems.Add(new OrderItem(
                    menuItem,
                    comment: string.Empty,
                    orderStatus: OrderItem.OrderStatus.Placed,
                    count: amount,
                    orderId: orderId
                ));
            }
        }
        private void RestoreAllSubBillItemsToMainBill()
        {
            foreach (var subItem in subBillItems)
            {
                AddOrIncrementMainBillItem(subItem.MenuItem.Name, subItem.MenuItem.Price, subItem.MenuItem.Vat, subItem.Count);
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
        private void AfterBillPaid(Bill bill, OrderService orderService)
        {
            MessageBox.Show("bill has been paid.");
            currentBill = bill;
            currentBill.IsPaid = true; // Mark the bill as paid
            if (BillsPaid(currentBill, currentSubBill, orderService))
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
        private void AfterSubBillPaid(SubBill subBill, OrderService orderService)
        {
            MessageBox.Show("Sub-bill has been paid.");
            currentSubBill = subBill;
            currentSubBill.IsPaid = true; // Mark the sub-bill as paid
            if (BillsPaid(currentBill, currentSubBill, orderService))
            {
                this.Close(); // Close the form if both bills are paid
            }
        }
        private (decimal total, decimal vatTotal) GetTotalAndVat(List<OrderItem> items)
        {
            decimal vatTotal;
            decimal total = CalculateTotal(items, out vatTotal);
            return (total, vatTotal);
        }
        private (decimal lowVatTotal, decimal highVatTotal) GetLowAndHighVat(List<OrderItem> items)
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
        private void EnsureBillExists(decimal billTotalExclVat, decimal vatTotal, BillService billService)
        {
            if (currentBill == null)
            {
                int nextId = billService.GetNextBillId();
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
                currentBill.TotalPrice = billTotalExclVat + vatTotal;
                currentBill.Vat = vatTotal;
                billService.UpdateBill(currentBill);
            }
        }
        private void EnsureSubBillExists(decimal subBillTotalExclVat, decimal vatTotal, SubBillService subBillService)
        {
            if (currentSubBill == null)
            {
                int nextId = subBillService.GetNextSubBillId();
                currentSubBill = new SubBill
                {
                    SubBillId = nextId,
                    BillId = currentBill?.BillId ?? 0,
                    Price = subBillTotalExclVat + vatTotal,
                    Vat = vatTotal,
                    IsPaid = false
                };
                subBillService.CreateSubBill(currentSubBill);
            }
            else
            {
                currentSubBill.Price = subBillTotalExclVat + vatTotal;
                currentSubBill.Vat = vatTotal;
                subBillService.UpdateSubBill(currentSubBill);
            }
        }
    }
}
