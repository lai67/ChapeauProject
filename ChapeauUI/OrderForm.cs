using Model;
using Service;
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
    public partial class OrderForm : Form
    {
        private OrderService orderService;
        private OrderItemService orderItemService;
        private Order currentOrder;
        private List<OrderItem> currentOrderItems;
        private Menu_Service menuService;
        private Table table;
        private Employee employee;
        public OrderForm(Table table, Employee employee)
        {
            InitializeComponent();
            ClickEventForAllMenuListItems();
            menuService = new Menu_Service();
            orderService = new OrderService();
            orderItemService = new OrderItemService();
            this.table = table;
            this.employee = employee;
            lblTableNr.Text = "Table: " + table.TableNumber;
            CreateOrder();
            RefreshOrderItemsList();
        }

        // Create or load the current order for the table.
        private void CreateOrder()
        {
            currentOrder = orderService.GetOrdersForAlreadyOrderedTable(table.Id);
            if (currentOrder == null)
            {
                currentOrder = new Order(DateTime.Now, 0, false, employee, table);
            }
            // Always start with an empty list for new items to be added in this session
            currentOrderItems = new List<OrderItem>();
        }

        // general load method to fill lists with items.
        private void FillListView(ListView listView, List<MenuItemModel> items)
        {
            listView.Items.Clear();

            foreach (var item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name); // first name and then sub items.
                listViewItem.SubItems.Add(item.Price.ToString("C"));
                listViewItem.SubItems.Add(item.Stock.ToString());
                listViewItem.Tag = item;
                listView.Items.Add(listViewItem);
            }
        }
        private void LoadDrinkItems()
        {
            FillListView(listVSoftDrinks, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "SoftDrink"));
            FillListView(listVBeers, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "Beer"));
            FillListView(listVWines, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "Wine"));
            FillListView(listVSpirit, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "SpiritDrink"));
            FillListView(listVCoffee, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "Coffee"));
        }

        private void LoadLunchItems()
        {
            FillListView(listVStartersLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Starter"));
            FillListView(listVMainsLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Main"));
            FillListView(listVDessertsLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Dessert"));
        }
        private void LoadDinnerItems()
        {
            
            FillListView(listVStartersDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Dinner, "Starter"));
            FillListView(listVEntremetsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Dinner, "Entremet"));
            FillListView(listVMainsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Dinner, "Main"));
            FillListView(listVDessertsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Dinner, "Dessert"));
        }


        // hide all panels.
        private void hideAllPanels()
        {
            pnlLunch.Visible = false;
            pnlDinner.Visible = false;
            pnlDrinks.Visible = false;
        }

        // button drink.
        private void btnDrinksM_Click(object sender, EventArgs e)
        {
            hideAllPanels();
            pnlDrinks.Visible = true;
            LoadDrinkItems();
        }
        // button lunch.
        private void btnLunchM_Click(object sender, EventArgs e)
        {
            hideAllPanels();
            pnlLunch.Visible = true;
            LoadLunchItems();

        }
        // button dinner.
        private void btnDinnerM_Click(object sender, EventArgs e)
        {
            hideAllPanels();
            pnlDinner.Visible = true;
            LoadDinnerItems();
        }
        private void RefreshOrderItemsList()
        {
            listVOrder.Items.Clear();
            foreach (var item in currentOrderItems)
            {
                var lvi = new ListViewItem(item.MenuItem.Name);
                lvi.SubItems.Add(item.Count.ToString());
                decimal total = item.MenuItem.Price * item.Count;
                lvi.SubItems.Add(total.ToString("C"));
                lvi.SubItems.Add(item.Comment ?? "");
                listVOrder.Items.Add(lvi);
            }
        }
        // double click event for menu items in the list view.
        private void MenuListView_DoubleClick(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null || listView.SelectedItems.Count == 0) return;

            var selectedItem = listView.SelectedItems[0];
            var menuItem = (MenuItemModel)selectedItem.Tag;

            var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Id == menuItem.Id);
            if (orderItem != null)
            {
                orderItem.Count++;
            }
            else
            {
                orderItem = new OrderItem(menuItem, "", OrderItem.OrderStatus.Placed, 1, currentOrder.Id);
                currentOrderItems.Add(orderItem);
            }
            RefreshOrderItemsList();
        }
        // all menu list items have the double click event attached.
        private void ClickEventForAllMenuListItems()
        {
            // Lunch
            listVStartersLunch.DoubleClick += MenuListView_DoubleClick;
            listVMainsLunch.DoubleClick += MenuListView_DoubleClick;
            listVDessertsLunch.DoubleClick += MenuListView_DoubleClick;

            // Dinner
            listVStartersDinner.DoubleClick += MenuListView_DoubleClick;
            listVEntremetsDinner.DoubleClick += MenuListView_DoubleClick;
            listVMainsDinner.DoubleClick += MenuListView_DoubleClick;
            listVDessertsDinner.DoubleClick += MenuListView_DoubleClick;

            // Drinks
            listVSoftDrinks.DoubleClick += MenuListView_DoubleClick;
            listVBeers.DoubleClick += MenuListView_DoubleClick;
            listVWines.DoubleClick += MenuListView_DoubleClick;
            listVSpirit.DoubleClick += MenuListView_DoubleClick;
            listVCoffee.DoubleClick += MenuListView_DoubleClick;
        }
        private bool AddSelectedMenuItemToOrder(ListView menuListView)
        {
            if (menuListView.SelectedItems.Count == 0) return false;
            var selectedItem = menuListView.SelectedItems[0];
            var menuItem = (MenuItemModel)selectedItem.Tag;

            var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Id == menuItem.Id);
            if (orderItem != null)
            {
                orderItem.Count++;
            }
            else
            {
                orderItem = new OrderItem(menuItem, "", OrderItem.OrderStatus.Placed, 1, currentOrder.Id);
                currentOrderItems.Add(orderItem);
            }
            RefreshOrderItemsList();
            ClearAllMenuSelections();
            return true;
        }
        private void ClearAllMenuSelections()
        {
            listVStartersLunch.SelectedItems.Clear();
            listVMainsLunch.SelectedItems.Clear();
            listVDessertsLunch.SelectedItems.Clear();
            listVStartersDinner.SelectedItems.Clear();
            listVEntremetsDinner.SelectedItems.Clear();
            listVMainsDinner.SelectedItems.Clear();
            listVDessertsDinner.SelectedItems.Clear();
            listVSoftDrinks.SelectedItems.Clear();
            listVBeers.SelectedItems.Clear();
            listVWines.SelectedItems.Clear();
            listVSpirit.SelectedItems.Clear();
            listVCoffee.SelectedItems.Clear();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count > 0)
            {
                var selectedItem = listVOrder.SelectedItems[0];
                var menuItemName = selectedItem.Text;
                var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Name == menuItemName);
                if (orderItem != null)
                {
                    orderItem.Count++;
                    RefreshOrderItemsList();
                    return;
                }
            }

            // Lunch
            if (AddSelectedMenuItemToOrder(listVStartersLunch)) return;
            if (AddSelectedMenuItemToOrder(listVMainsLunch)) return;
            if (AddSelectedMenuItemToOrder(listVDessertsLunch)) return;

            // Dinner
            if (AddSelectedMenuItemToOrder(listVStartersDinner)) return;
            if (AddSelectedMenuItemToOrder(listVEntremetsDinner)) return;
            if (AddSelectedMenuItemToOrder(listVMainsDinner)) return;
            if (AddSelectedMenuItemToOrder(listVDessertsDinner)) return;

            // Drinks
            if (AddSelectedMenuItemToOrder(listVSoftDrinks)) return;
            if (AddSelectedMenuItemToOrder(listVBeers)) return;
            if (AddSelectedMenuItemToOrder(listVWines)) return;
            if (AddSelectedMenuItemToOrder(listVSpirit)) return;
            if (AddSelectedMenuItemToOrder(listVCoffee)) return;

            // error message
            MessageBox.Show("Please select a menu item to add.");

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count == 0) return;
            var selectedItem = listVOrder.SelectedItems[0];
            var menuItemName = selectedItem.Text;
            var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Name == menuItemName);
            if (orderItem != null)
            {
                orderItem.Count--;
                if (orderItem.Count <= 0)
                    currentOrderItems.Remove(orderItem);
            }
            ClearAllMenuSelections();
            RefreshOrderItemsList();
        }
       
        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            if (currentOrderItems == null || currentOrderItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item before sending the order.");
                return;
            }

            currentOrder.Items = currentOrderItems;

            if (currentOrder.Id == 0)
            {
                int orderId = orderService.CreateOrder(currentOrder);
                currentOrder.Id = orderId;
                foreach (var item in currentOrderItems)
                    item.OrderId = orderId;
            }
            else
            {
                orderService.UpdateOrder(currentOrder);

                foreach (var item in currentOrderItems)
                {
                    if (item.Id == 0)
                    {
                        orderItemService.CreateOrderItem(item);
                        menuService.DecreaseMenuItemStock(item.MenuItem.Id, item.Count);
                    }
                    else
                        orderItemService.UpdateOrderItemCount(item);
                }
            }
            MessageBox.Show("Order sent!");;
            this.Close();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRemoveCom_Click(object sender, EventArgs e)
        {

            if (listVOrder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order item to remove its comment.");
                return;
            }
            var selectedItem = listVOrder.SelectedItems[0];
            var menuItemName = selectedItem.Text;
            var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Name == menuItemName);
            if (orderItem != null)
            {
                orderItem.Comment = string.Empty;
                RefreshOrderItemsList();
            }
        }

        private void btnAddCom_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order item to add a comment.");
                return;
            }
            var selectedItem = listVOrder.SelectedItems[0];
            var menuItemName = selectedItem.Text;
            var orderItem = currentOrderItems.FirstOrDefault(i => i.MenuItem.Name == menuItemName);
            if (orderItem != null)
            {
                orderItem.Comment = textBoxComment.Text;
                RefreshOrderItemsList();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            currentOrderItems.RemoveAll(item => item.Id == 0); // Remove only items that are not yet saved to the database.
            RefreshOrderItemsList();
        }
    }
}
