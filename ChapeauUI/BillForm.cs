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
        private List<OrderItem> billItems;
        private List<OrderItem> subBillItems;

        /* ask danka about the two global lists above. Gerwin wants every field to be local
           except bill and currentBill, but to do that you would need an increaseAmount and
           decreaseAmount method in the DAO, and the amount of calls to those methods would 
           be (in my opinion) inefficient and wasteful in terms of memory */

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
            var subBillService = new SubBillService();

            LoadColumns(lstViewBill);
            LoadColumns(listViewSubBill);

            currentBill = billService.GetBillByOrderId(orderId);

            if (currentBill == null)
            {
                currentBill = new Bill
                {
                    OrderId = orderId,
                    OrderItems = new List<OrderItem>(),
                    GuestNumber = 1,
                    Tip = 0,
                    Feedback = ""
                };
                billService.CreateBill(currentBill);

                // Reload the bill to get the BillId assigned by the DB
                currentBill = billService.GetBillByOrderId(orderId);
            }

            if (currentBill == null) // just in case there is an error creating the bill
            {
                MessageBox.Show("Error creating bill. Please try again later.");
                this.Close(); // Close the form if bill creation fails
                return;
            }

            billItems = billService.GetOrderedItemsForBill(currentBill.BillId);

            FillListView(lstViewBill, billItems);
            UpdateBillAndSubBillViews();
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

            decimal billTotal = currentBill.TotalPrice;
            decimal vatTotal = currentBill.Vat;
            decimal lowVatTotal = currentBill.LowVatTotal;
            decimal highVatTotal = currentBill.HighVatTotal;
            EnsureBillExists(billTotal, vatTotal, billService);

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

            decimal subBillTotal = currentSubBill.Price;
            decimal vatTotal = currentSubBill.Vat;
            decimal subBillLowVatTotal = currentSubBill.LowVatTotal;
            decimal subBillHighVatTotal = currentSubBill.HighVatTotal;
            EnsureSubBillExists(subBillTotal, vatTotal, subBillService);

            if (!ShowSubBillPaymentForm(currentSubBill))
                return;

            currentSubBill.IsPaid = true;
            subBillService.UpdateSubBill(currentSubBill);

            ClearSubBill();
            AfterSubBillPaid(currentSubBill, orderService);
        }
        private void UpdateSubBillVatAndTotalLabels()
        {
            if (currentSubBill != null)
            {
                SetVatAndTotalLabels(
                    lblSubBillTotalValue,
                    lblVatValueSubBill,
                    lblVatLowValueSubBill,
                    lblVatHighValueSubBill,
                    currentSubBill.Price,
                    currentSubBill.Vat,
                    currentSubBill.LowVatTotal,
                    currentSubBill.HighVatTotal
                );
            }
            else
            {
                SetVatAndTotalLabels(
                    lblSubBillTotalValue,
                    lblVatValueSubBill,
                    lblVatLowValueSubBill,
                    lblVatHighValueSubBill,
                    0, 0, 0, 0
                );
            }
        }

        private void UpdateBillVatAndTotalLabels()
        {
            if (currentBill != null)
            {
                SetVatAndTotalLabels(
                    lblTotalPriceValueBill,
                    lblVatValueCompBill,
                    lblVatLowBillValue,
                    lblVatHighBillValue,
                    currentBill.TotalPrice,
                    currentBill.Vat,
                    currentBill.LowVatTotal,
                    currentBill.HighVatTotal
                );
            }
            else
            {
                SetVatAndTotalLabels(
                    lblTotalPriceValueBill,
                    lblVatValueCompBill,
                    lblVatLowBillValue,
                    lblVatHighBillValue,
                    0, 0, 0, 0
                );
            }
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

            // Keep currentBill.OrderItems in sync with billItems
            if (currentBill != null)
                currentBill.OrderItems = billItems.ToList();

            // Same with currentSubBill
            if (currentSubBill != null)
                currentSubBill.OrderItems = subBillItems.ToList();

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
            UpdateBillAndSubBillViews();
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
        private void SetVatAndTotalLabels(
            Label totalLabel, Label vatLabel, Label lowVatLabel, Label highVatLabel,
            decimal total, decimal vatTotal, decimal lowVatTotal, decimal highVatTotal)
        {
            totalLabel.Text = $"€{(total + vatTotal):0.00}";
            vatLabel.Text = $"€{vatTotal:0.00}";
            lowVatLabel.Text = $"€{lowVatTotal:0.00}";
            highVatLabel.Text = $"€{highVatTotal:0.00}";
        }
        private void EnsureBillExists(decimal billTotal, decimal vatTotal, BillService billService)
        {
            if (currentBill == null)
            {
                int nextId = billService.GetNextBillId();
                currentBill = new Bill
                {
                    BillId = nextId,
                    OrderId = orderId,
                    OrderItems = billItems.ToList(),
                    IsPaid = false
                };
                billService.CreateBill(currentBill);
            }
            else
            {
                billService.UpdateBill(currentBill);
            }
        }
        private void EnsureSubBillExists(decimal subBillTotal, decimal vatTotal, SubBillService subBillService)
        {
            if (currentSubBill == null)
            {
                int nextId = subBillService.GetNextSubBillId();
                currentSubBill = new SubBill
                {
                    SubBillId = nextId,
                    BillId = currentBill?.BillId ?? 0,
                    OrderItems = subBillItems.ToList(),
                    IsPaid = false
                };
                subBillService.CreateSubBill(currentSubBill);
            }
            else
            {
                subBillService.UpdateSubBill(currentSubBill);
            }
        }
    }
}
