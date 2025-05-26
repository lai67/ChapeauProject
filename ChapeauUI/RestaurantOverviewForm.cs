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
        private readonly TableService _tableService = new();
        private readonly OrderService orderService = new();

        private List<Table> _tables = new();

        private readonly System.Windows.Forms.Timer _refreshTimer = new System.Windows.Forms.Timer { Interval = 5000 };
        public RestaurantOverviewForm()
        {
            InitializeComponent();

            this.Load += RestaurantOverviewForm_Load;

            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();
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

            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

            foreach (var btn in this.Controls.OfType<Button>())
            {
                if (!int.TryParse(btn.Text, out int tableNumber)) //skip logo / other buttons
                    continue; 

                var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
                if (table == null)
                    continue;

                btn.UseVisualStyleBackColor = false;  // enable BackColor
                switch (table.Status)
                {
                    case TableStatus.Free:
                        btn.BackColor = Color.Green;
                        break;
                    case TableStatus.Booked:
                        btn.BackColor = Color.Blue;
                        break;
                    case TableStatus.Occupied:
                        btn.BackColor = Color.Red;
                        break;
                }
            }

            var phases = orderService.GetTableLocationPhases();   // dictionary

            foreach (int n in Enumerable.Range(1, 10))
            {
                var picBar = Controls.Find($"picBar{n}", true).FirstOrDefault() as PictureBox;
                var picKitch = Controls.Find($"picKitch{n}", true).FirstOrDefault() as PictureBox;
                if (picBar == null || picKitch == null) continue;

                phases.TryGetValue(n, out var p);   // p defaults to ("", "") if none

                // ----BAR icon----
                picBar.Image = p.BarStatus switch
                {
                    "Preparing" => Properties.Resources.PreparingBarIcon,
                    "Ready" => Properties.Resources.ReadyBarIcon,
                    _ => Properties.Resources.NoBarIcon      // Running/None
                };

                // ---- KITCHEN icon ----
                picKitch.Image = p.KitchenStatus switch
                {
                    "Preparing" => Properties.Resources.PreparingKitchenIcon,
                    "Ready" => Properties.Resources.ReadyKitchenIcon,
                    _ => Properties.Resources.NoKitchenIcon
                };
            }
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

