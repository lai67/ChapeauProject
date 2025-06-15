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
    public partial class RestaurantOverviewForm : Form
    {
        private readonly Employee _currentEmpoyee;
        private readonly TableService _tableService = new();
        private readonly OrderService orderService = new();

        private List<Table> _tables = new();
        private int _selectedTableNumber;


        private readonly System.Windows.Forms.Timer _refreshTimer = new System.Windows.Forms.Timer { Interval = 5000 };
        public RestaurantOverviewForm(Employee currentEmpoyee)
        {
            InitializeComponent();
            _currentEmpoyee = currentEmpoyee;

            this.Load += RestaurantOverviewForm_Load;

            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();
            lblName.Text = $"{_currentEmpoyee.FirstName} {_currentEmpoyee.LastName}";

            panelFreeActions.Visible = false;
            panelOccActions.Visible = false;
        }
        private void RestaurantOverviewForm_Load(object sender, EventArgs e)
        {
            RefreshTables();

            //register each table button click event
            for (int i = 1; i <= 10; i++)
            {
                var btn = Controls.Find($"btnTable{i}", true).FirstOrDefault() as Button;
                if (btn != null)
                {
                    btn.Click += TableButton_Click;
                }
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshTables();
        }

        private void RefreshTables()
        {
            _tables = _tableService.GetAllTables();
            var phases = orderService.GetTableLocationPhases();

            UpdateDateTime();
            UpdateTableButtonColors();
            UpdateOrderIcons(phases);

            panelFreeActions.Visible = false;
            panelOccActions.Visible = false;

            // Re-enable all controls
            foreach (Control c in Controls)
                c.Enabled = true;

        }

        // 1) Hide all action panels
        private void HideAllPanels()
        {
            panelFreeActions.Visible = false;
            panelOccActions.Visible = false;

            foreach (Control c in Controls)
                c.Enabled = true;
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Text, out int tableNumber))
            {
                var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
                if (table == null) return;

                if (table.Status == TableStatus.Free)
                {
                    ShowFreePanel(tableNumber);
                }
                else if (table.Status == TableStatus.Occupied)
                {
                    ShowOccupiedPanel(tableNumber);
                }
            }
        }

        // 2) Show the free panel for the selected table
        //--------------------------------------------------------------------------------------------------------------------
        private void ShowFreePanel(int tableNumber)
        {
            _selectedTableNumber = tableNumber;
            lblFreeHeader.Text = $"Table {tableNumber} (Free)";
            panelFreeActions.BringToFront();
            panelFreeActions.Visible = true;
            foreach (Control c in Controls)
            {
                if (c != panelFreeActions)
                    c.Enabled = false;
            }
            panelFreeActions.Enabled = true;

            // Wire panel buttons (remove old handlers first)
            btnOccupyHere.Click -= btnOccupyHere_Click;
            btnOccupyHere.Click += btnOccupyHere_Click;

            btnCancelFree.Click -= BtnCancelFree_Click;
            btnCancelFree.Click += BtnCancelFree_Click;
        }
        private void btnOccupyHere_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblFreeHeader.Text.Split(' ')[1], out int tableNumber))
            {
                _tableService.UpdateTableStatus(tableNumber, TableStatus.Occupied);
                CloseOccPanel();
                RefreshTables();
            }
        }

        private void BtnCancelFree_Click(object sender, EventArgs e)
        {
            CloseFreePanel1();
        }

        private void CloseFreePanel1()
        {
            panelFreeActions.Visible = false;
            foreach (Control c in Controls)
            {
                c.Enabled = true;
            }
        }

        private void CloseFreePanelAndRefresh()
        {
            CloseFreePanel1();
            RefreshTables();
        }

        // 3) Show the occupied panel for the selected table
        //--------------------------------------------------------------------------------------------------------------------
        private void ShowOccupiedPanel(int tableNumber)
        {
            lblOccHeader.Text = $"Table {tableNumber} (Occupied)";
            _selectedTableNumber = tableNumber;

            lstReadyItems.Items.Clear();
            var readyItems = orderService.GetReadyItemsByTableId(tableNumber);
            foreach (var item in readyItems)
            {
                lstReadyItems.Items.Add($"{item.MenuItem.Name} - {item.MenuItem.Name}");
            }

            btnMarkAllServed.Enabled = readyItems.Count > 0;

            bool canFree = orderService.HasNoRunningItems(tableNumber);
            btnFreeHere.Enabled = canFree;


            //  Bring the panel to front and show it, disabling everything behind
            panelOccActions.BringToFront();
            panelOccActions.Visible = true;
            foreach (Control c in Controls)
            {
                if (c != panelOccActions)
                    c.Enabled = false;
            }
            panelOccActions.Enabled = true;

            // Wire panel buttons (remove old handlers first)
            btnGoToOrders.Click -= BtnGoToOrders_Click;
            btnGoToOrders.Click += BtnGoToOrders_Click;

            btnMarkAllServed.Click -= BtnMarkAllServed_Click;
            btnMarkAllServed.Click += BtnMarkAllServed_Click;

            btnFreeHere.Click -= BtnFreeHere_Click;
            btnFreeHere.Click += BtnFreeHere_Click;

            btnCancelOcc.Click -= BtnCancelOcc_Click;
            btnCancelOcc.Click += BtnCancelOcc_Click;
        }

        //order button 
        private void BtnGoToOrders_Click(object sender, EventArgs e)
        {
            var table = _tables.FirstOrDefault(t => t.TableNumber == _selectedTableNumber);
            if (table == null) return;

            OrderForm ordersForm = new OrderForm(table, _currentEmpoyee);
            CloseOccPanel();
            ordersForm.ShowDialog(this);

            _tables = _tableService.GetAllTables();
            UpdateTableButtonColors();
        }
        //mark all served button click

        private void BtnMarkAllServed_Click(object sender, EventArgs e)
        {
            int tableNumber = int.Parse(lblOccHeader.Text.Split(' ')[1]);
            orderService.MarkAllReadyServedByTableId(tableNumber);
            lstReadyItems.Items.Clear();
            btnMarkAllServed.Enabled = false;
            RefreshTables();
        }
        //free table button click
        private void BtnFreeHere_Click(object sender, EventArgs e)
        {
            _tableService.UpdateTableStatus(_selectedTableNumber, TableStatus.Free);
            CloseOccPanel();
            RefreshTables();
        }
        private void BtnCancelOcc_Click(object sender, EventArgs e)
        {
            CloseOccPanel();
        }
        private void CloseOccPanel()
        {
            panelOccActions.Visible = false;
            foreach (Control c in Controls)
            {
                c.Enabled = true;
            }
        }

        private void CloseOccPanelAndRefresh()
        {
            CloseOccPanel();
            _tables = _tableService.GetAllTables();
            UpdateTableButtonColors();
        }
        //update time 
        private void UpdateDateTime()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //update Table button colore
        private void UpdateTableButtonColors()
        {
            for (int i = 1; i <= 10; i++)
            {
                var btn = Controls.Find($"btnTable{i}", true).FirstOrDefault() as Button;
                if (btn == null) continue;

                var table = _tables.FirstOrDefault(t => t.TableNumber == i);
                if (table == null) continue;


                btn.UseVisualStyleBackColor = false;
                btn.BackColor = table.Status switch
                {
                    TableStatus.Free => Color.Green,
                    TableStatus.Booked => Color.Blue,
                    TableStatus.Occupied => Color.Red,
                    _ => SystemColors.Control
                };
                btn.Tag = table;
            }
        }
        private void UpdateOrderIcons(Dictionary<int, (string BarStatus, string KitchenStatus)> phases)
        {
            foreach (int n in Enumerable.Range(1, 10))
            {
                var picBar = Controls.Find($"picBar{n}", true).FirstOrDefault() as PictureBox;
                var picKitch = Controls.Find($"picKitch{n}", true).FirstOrDefault() as PictureBox;
                if (picBar == null || picKitch == null) continue;

                phases.TryGetValue(n, out var p);

                picBar.Image = GetBarIcon(p.BarStatus);
                picKitch.Image = GetKitchenIcon(p.KitchenStatus);
            }
        }
        //bar icos
        private Image GetBarIcon(string status)
        {
            return status switch
            {
                "Placed" => Properties.Resources.NoBarIcon,
                "Preparing" => Properties.Resources.PreparingBarIcon,
                "Ready" => Properties.Resources.ReadyBarIcon,
                _ => null
            };
        }
        //kitchen icons
        private Image GetKitchenIcon(string status)
        {
            return status switch
            {
                "Placed" => Properties.Resources.NoKitchenIcon,
                "Preparing" => Properties.Resources.PreparingKitchenIcon,
                "Ready" => Properties.Resources.ReadyKitchenIcon,
                _ => null
            };
        }


        private void btnLogOutNew_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            Hide();
            loginForm.Closed += (s, args) => Close();
            loginForm.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            var payment = new PaymentFormCompleteBill();
            payment.Show();
            this.Hide(); 
        }
    }
}

