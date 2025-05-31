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

        private readonly System.Windows.Forms.Timer _refreshTimer = new System.Windows.Forms.Timer { Interval = 5000 };
        public RestaurantOverviewForm(Employee currentEmpoyee)
        {
            InitializeComponent();
            _currentEmpoyee = currentEmpoyee;
            this.Load += RestaurantOverviewForm_Load;

            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();
            lblName.Text =$"{_currentEmpoyee.FirstName}{_currentEmpoyee.LastName}";
        }
        private void RestaurantOverviewForm_Load(object sender, EventArgs e)
        {
            RefreshTables();
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
            foreach (var btn in Controls.OfType<Button>())
            {
                if (!int.TryParse(btn.Text, out int tblNbr)) continue;
                var table = _tables.FirstOrDefault(t => t.TableNumber == tblNbr);
                if (table == null) continue;

                btn.UseVisualStyleBackColor = false;
                btn.BackColor = table.Status switch
                {
                    TableStatus.Free => Color.Green,
                    TableStatus.Booked => Color.Blue,
                    TableStatus.Occupied => Color.Red,
                    _ => SystemColors.Control
                };
            }
        }
        private void UpdateOrderIcons(Dictionary<int,(string BarStatus,string KitchenStatus)> phases)
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

    }
}

