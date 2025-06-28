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
        private Bill currentBill; // keep this
        private SubBill currentSubBill; // keep this too
        public BillForm(Bill bill)
        {
            InitializeComponent();
            currentBill = bill;
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
                listItem.Tag = item;
            }
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            LoadColumns(lstViewBill);
            LoadColumns(listViewSubBill);

            FillListView(lstViewBill, currentBill.OrderItems);
            UpdateBillAndSubBillViews();
        }
        private void btnAddToSubBill_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedBillItem(out OrderItem mainItem))
                return;

            if (mainItem == null)
            {
                MessageBox.Show("No more of this item left to add.");
                return;
            }

            mainItem.Count--;
            AddOrIncrementSubBillItem(mainItem);
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
            OrderItem subItem = (OrderItem)selectedItem.Tag;
            string name = selectedItem.SubItems[0].Text;

            // subItem not needed
            // OrderItem subItem = subBillItems.FirstOrDefault(i => i.MenuItem.Name == name);

            if (subItem != null)
            {
                subItem.Count--;
                if (subItem.Count <= 0)
                {
                    currentSubBill.OrderItems.Remove(subItem);
                }

                AddOrIncrementMainBillItem(subItem);
                UpdateBillAndSubBillViews();
            }
        }

        private void btnRemoveAllFromSubBill_Click(object sender, EventArgs e)
        {
            foreach (var subItem in currentSubBill.OrderItems)
            {
                AddOrIncrementMainBillItem(subItem, subItem.Count);
            }
            currentSubBill.OrderItems.Clear();
            UpdateBillAndSubBillViews();
        }

        // share a method with paySubBill?
        private void btnPayBill_Click(object sender, EventArgs e)
        {
            var orderService = new OrderService();
            var billService = new BillService();

            if (currentBill.OrderItems.Count == 0)
            {
                MessageBox.Show("Bill is empty. Please add items before paying.");
                return;
            }

            currentBill = billService.EnsureBillExists(currentBill, currentBill.OrderId);

            if (!HasUserCancelledBill(currentBill))
                return;

            currentBill.IsPaid = true;
            billService.UpdateBill(currentBill);

            ClearBill();
            AfterBillPaid(currentBill, orderService);
        }

        private void btnPaySubBill_Click(object sender, EventArgs e)
        {
            var orderService = new OrderService();
            var subBillService = new SubBillService();

            if (currentSubBill.OrderItems.Count == 0)
            {
                MessageBox.Show("Sub-bill is empty. Please add items before paying.");
                return;
            }

            // Ensure currentSubBill exists before accessing its properties
            currentSubBill = subBillService.EnsureSubBillExists(currentSubBill, currentBill?.BillId ?? 0);

            if (!HasUserCancelledSubBill(currentSubBill))
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
                orderService.SetOrderCreated(currentBill.OrderId, true);

                var tableService = new TableService();

                var order = orderService.GetOrdersForAlreadyOrderedTable(currentBill.OrderId);
                if (order?.Table != null)
                {
                    tableService.UpdateTableStatus(order.Table.Id, TableStatus.Free);
                }

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
        private void AddOrIncrementSubBillItem(OrderItem mainItem)
        {
            var subBillService = new SubBillService();
            currentSubBill = subBillService.EnsureSubBillExists(currentSubBill, currentBill?.BillId ?? 0);

            OrderItem subItem = currentSubBill.OrderItems.FirstOrDefault(i => i.MenuItem.Name == mainItem.MenuItem.Name);
            if (subItem != null)
            {
                subItem.Count++;
            }
            else
            {
                // Create a new MenuItemModel for the sub-bill item
                var menuItem = new MenuItemModel
                {
                    Name = mainItem.MenuItem.Name,
                    Price = mainItem.MenuItem.Price,
                    Vat = mainItem.MenuItem.Vat,
                    Item_Category = mainItem.MenuItem.Item_Category,
                    Menu_Id = mainItem.MenuItem.Menu_Id
                };

                currentSubBill.OrderItems.Add(new OrderItem(
                    menuItem,
                    mainItem.Comment,
                    mainItem.orderStatus,
                    1, // count
                    mainItem.OrderId
                ));
            }
            UpdateBillAndSubBillViews();
        }
        private void RemoveMainBillItemIfZero(OrderItem mainItem)
        {
            if (mainItem.Count == 0)
            {
                currentBill.OrderItems.Remove(mainItem);
            }
        }
        private bool TryGetSelectedBillItem(out OrderItem selectedOrderItem)
        {
            selectedOrderItem = null;
            if (lstViewBill.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to add.");
                return false;
            }
            selectedOrderItem = lstViewBill.SelectedItems[0].Tag as OrderItem;
            return selectedOrderItem != null;
        }
        private void UpdateBillAndSubBillViews()
        {
            if (currentSubBill != null && currentSubBill.OrderItems != null)
            {
                FillListView(listViewSubBill, currentSubBill.OrderItems);
            }
            else
            {
                // Handling a null null case, e.g., clear the ListView or log an error  
                listViewSubBill.Items.Clear();
            }

            FillListView(lstViewBill, currentBill.OrderItems);
            UpdateBillVatAndTotalLabels();
            UpdateSubBillVatAndTotalLabels();
        }
        private void AddOrIncrementMainBillItem(OrderItem subItem, int amount = 1)
        {
            OrderItem mainItem = currentBill.OrderItems.FirstOrDefault(i => i.MenuItem.Name == subItem.MenuItem.Name);
            if (mainItem != null)
            {
                mainItem.Count += amount;
            }
            else
            {
                // Clone the MenuItemModel for the main bill item
                var menuItem = new MenuItemModel
                {
                    Name = subItem.MenuItem.Name,
                    Price = subItem.MenuItem.Price,
                    Vat = subItem.MenuItem.Vat,
                    Item_Category = subItem.MenuItem.Item_Category,
                    Menu_Id = subItem.MenuItem.Menu_Id
                };

                currentBill.OrderItems.Add(new OrderItem(
                    menuItem,
                    comment: subItem.Comment,
                    orderStatus: OrderItem.OrderStatus.Placed,
                    count: amount,
                    orderId: currentBill.OrderId
                ));
            }
        }
        private bool HasUserCancelledBill(Bill bill)
        {
            PaymentFormCompleteBill paymentForm = new PaymentFormCompleteBill(bill);
            paymentForm.ShowDialog();
            return !paymentForm.UserCancelled;
        }
        private void ClearBill()
        {
            currentBill.OrderItems.Clear();
            FillListView(lstViewBill, currentBill.OrderItems);
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
        private bool HasUserCancelledSubBill(SubBill subBill)
        {
            PaymentFormSubBill paymentForm = new PaymentFormSubBill(subBill);
            paymentForm.ShowDialog();
            return !paymentForm.UserCancelled;
        }
        private void ClearSubBill()
        {
            currentSubBill.OrderItems.Clear();
            FillListView(listViewSubBill, currentSubBill.OrderItems);
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
    }
}
