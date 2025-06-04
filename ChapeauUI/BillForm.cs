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
        private List<Bill> bills;
        private List<OrderedMenuItemDTO> subBillItems;
        public BillForm(int orderId)
        {
            InitializeComponent();
            billService = new BillService();
            bills = billService.GetAllBills();
            subBillItems = new List<OrderedMenuItemDTO>();
            this.orderId = orderId;
        }
        private int splitValue = 0;
        private void btnSplitIncrement_Click(object sender, EventArgs e)
        {
            splitValue++;
            DisplaySplitValue();
        }
        private void btnSplitDecrement_Click(object sender, EventArgs e)
        {
            if (splitValue > 0)
            {
                splitValue--;
                DisplaySplitValue();
            }
            else
            {
                MessageBox.Show("Split value cannot be negative.");
            }
        }
        private void DisplaySplitValue()
        {
            lblSplitValue.Text = splitValue.ToString();
        }
        private void FillListView(ListView listView, Bill bill)
        {
            listView.Items.Clear();
            decimal billTotal = 0;
            decimal vatTotal = 0;

            List<OrderedMenuItemDTO> orderedItems = billService.GetOrderedItemsForBill(bill.BillId);

            foreach (var item in orderedItems)
            {
                decimal itemTotal = item.Price * item.Amount;
                vatTotal = item.Vat * vatTotal;
                billTotal += itemTotal;
                vatTotal += vatTotal;
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C")); // "C" formats as currency
                listItem.SubItems.Add(item.Amount.ToString());
                listItem.SubItems.Add((item.Price * item.Amount).ToString("C")); // total for that item

                listView.Items.Add(listItem);
            }

            lblTotalPriceValueBill.Text = $"Total: €{billTotal.ToString("0.##")}";
            lblVatTotalCompBill.Text = $"Total: €{vatTotal.ToString("0.##")}";
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            // Setup ListView columns
            lstViewBill.View = View.Details;
            lstViewBill.FullRowSelect = true;
            lstViewBill.Columns.Clear();
            lstViewBill.Columns.Add("Name", 171);
            lstViewBill.Columns.Add("Price", 85);
            lstViewBill.Columns.Add("Amount", 85);

            // Get the bill related to this order
            Bill bill = billService.GetBillByOrderId(orderId);

            if (bill != null)
            {
                FillListView(lstViewBill, bill);
            }
            else
            {
                MessageBox.Show("No bill found for this order.");
            }
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
            int amount = int.Parse(selectedItem.SubItems[2].Text);

            OrderedMenuItemDTO existingItem = null;
            foreach (var item in subBillItems) // checks if item has already been added to sub-bill
            {
                if (item.Name == name)
                {
                    existingItem = item;
                    break;
                }
            }
            if (existingItem != null)
            {
                existingItem.Amount++;
            }
            else
            {
                OrderedMenuItemDTO newItem = new OrderedMenuItemDTO
                {
                    Name = name,
                    Price = price,
                    Amount = 1 // starting with 1 unit
                };

                subBillItems.Add(newItem);
            }
            UpdateSubBillListView();
            decimal subBillTotal = CalculateSubBillTotal();
            lblSubBillTotalValue.Text = $"Total: €{subBillTotal.ToString("0.##")}";
        }
        private void UpdateSubBillListView()
        {
            listViewSubBill.Items.Clear();

            foreach (var item in subBillItems)
            {
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(item.Amount.ToString());
                listItem.SubItems.Add((item.Price * item.Amount).ToString("C"));

                listViewSubBill.Items.Add(listItem);
            }
        }
        private decimal CalculateSubBillTotal()
        {
            decimal total = 0;

            foreach (var item in subBillItems)
            {
                total += item.Price * item.Amount;
            }

            return total;
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

            OrderedMenuItemDTO itemToRemove = null;
            foreach (var item in subBillItems)
            {
                if (item.Name == name)
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                itemToRemove.Amount--;
                if (itemToRemove.Amount <= 0)
                {
                    subBillItems.Remove(itemToRemove);
                }

                UpdateSubBillListView();
                decimal total = CalculateSubBillTotal();
                lblSubBillTotalValue.Text = $"Total: {total:C}";
            }
        }

        private void btnRemoveAllFromSubBill_Click(object sender, EventArgs e)
        {
            subBillItems.Clear();

            UpdateSubBillListView();

            decimal total = CalculateSubBillTotal();
            lblSubBillTotalValue.Text = $"Total: {total:C}";
        }

        private void btnFinalizePayment_Click(object sender, EventArgs e)
        {

        }

        private void lblSplitValue_Click(object sender, EventArgs e)
        {

        }
    }
}
