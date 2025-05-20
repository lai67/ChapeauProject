using Microsoft.IdentityModel.Tokens;
using Model;
using Service;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class LoginForm : Form
    {
        private EmployeeService employeeService = new();

        private TextBox activeTextBox;
        public LoginForm()
        {
            InitializeComponent();

            txtUserId.UseSystemPasswordChar = false;
            txtPassword.UseSystemPasswordChar = true;

            activeTextBox = txtUserId;

            txtUserId.Click += TextBox_Click;
            txtPassword.Click += TextBox_Click;


            foreach (var btn in panel1.Controls.OfType<Button>().Where(b => b.Tag?.ToString() == "NUM"))
            {
                btn.Click += btnNumber_Click;
            }

            btnDelete.Click += btnDelete_Click;
            btnLogin.Click += btnLogin_Click;
        }

        //Login button

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!TryGetCredentials(out int userId, out string password))
                return;

            var employee = employeeService.Authenticate(userId, password);
            if (employee is null)
            {
                ShowLoginError("Invalid ID or password. Please try again.");
                return;
            }

            GlobalVariables.CurrentEmployee = employee;

            // 2) Instantiate the overview with the employee
            var overview = new RestaurantOverviewForm();
            overview.Show();

            // 3) Hide login
            this.Hide();
        }

        //GlobalVariables.CurrentEmployee = employee;
        //RedirectEmployeeRole();


        //private void RedirectEmployeeRole()
        //{
        //    Form next;
        //    switch (employee.Role)
        //    {
        //        case Role.Waiter:
        //            next = new RestaurantOverviewForm(employee);
        //            break;
        //        case Role.Barman:
        //            next = new BarOrdersForm(employee);
        //            break;
        //        case Role.Chef:
        //            next = new KitchenOrdersForm(employee);
        //            break;
        //        case Role.Manager:
        //            next = new ManagerForm(employee);
        //            break;
        //        default:
        //            next = new RestaurantOverviewForm(employee);
        //            break;
        //           
        //    }

        //    this.Hide();
        //    next.Closed += (s, _) => this.Close();
        //    next.Show();
        //}

        //Number buttons

        private void TextBox_Click(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
            activeTextBox.Focus();  // Gives visual focus to the active field
        }
        private void btnNumber_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            activeTextBox.Text += btn.Text;
        }

        //Delete button

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var text = activeTextBox.Text;
            if (text.Length > 0)
                activeTextBox.Text = text[..^1];  // C# 8 range syntax
        }

        private bool TryGetCredentials(out int userId, out string password)
        {
            userId = 0;
            password = null!;

            if (!int.TryParse(txtUserId.Text, out userId))
            {
                ShowLoginError("Please enter a valid  User ID.");
                txtUserId.Focus();
                return false;
            }

            password = txtPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                ShowLoginError("Please enter your PIN Code.");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void ShowLoginError(string message)
        {
            MessageBox.Show(message,
                            "Login Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            txtPassword.Clear();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        public static class GlobalVariables
        {
            public static Model.Employee CurrentEmployee { get; set; }
        }

    }
}
