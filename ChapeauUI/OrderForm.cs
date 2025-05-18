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
        private Menu_Service menuService;
        // ctor. pass the table number ( id ) to the form.
        public OrderForm()
        {
            InitializeComponent();
            menuService = new Menu_Service();


        }
        // general load method to fill lists with items.
        private void FillListView(ListView listView, List<Menu_Item_Model> items)
        {
            listView.Items.Clear();

            foreach (var item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name); // first name and then sub items.
                listViewItem.SubItems.Add(item.Price.ToString("C"));
                listViewItem.SubItems.Add(item.Stock.ToString());
                listView.Items.Add(listViewItem);
            }
        }
        private void LoadDrinkItems()
        {
            FillListView(listVSoftDrinks, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks, "SoftDrink"));
            FillListView(listVBeers, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks,"Beer"));
            FillListView(listVWines, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks,"Wine"));
            FillListView(listVSpirit, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks,"SpiritDrink"));
            FillListView(listVCoffee, menuService.GetItemsByMenuAndCategory((int)MenuType.Drinks,"Coffee"));
        }

        private void LoadLunchItems()
        {
           
            FillListView(listVStartersLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Starter"));
            FillListView(listVMainsLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "MainCourse"));
            FillListView(listVDessertsLunch, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Dessert"));
        }
        private void LoadDinnerItems()
        {
            var allDinnerItems = menuService.GetItemsByMenuId((int)MenuType.Dinner);
            FillListView(listVStartersDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Starter"));
            FillListView(listVEntremetsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Entremet"));
            FillListView(listVMainsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "MainCourse"));
            FillListView(listVDessertsDinner, menuService.GetItemsByMenuAndCategory((int)MenuType.Lunch, "Dessert"));
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
    }
}
