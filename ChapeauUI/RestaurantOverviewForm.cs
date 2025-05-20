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

            foreach (var btn in TablesPanel.Controls.OfType<Button>())
            {
                if (!int.TryParse(btn.Text, out int tableNumber))
                    continue;

                var table = _tables.FirstOrDefault(t => t.TableNumber == tableNumber);
                if (table == null)
                    continue;

                btn.UseVisualStyleBackColor = false;  // enable BackColor
                switch (table.Status)
                {
                    case TableStatus.Free:
                        btn.BackColor = Color.LightGreen;
                        break;
                    case TableStatus.Booked:
                        btn.BackColor = Color.LightBlue;
                        break;
                    case TableStatus.Occupied:
                        btn.BackColor = Color.IndianRed;
                        break;
                }
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
