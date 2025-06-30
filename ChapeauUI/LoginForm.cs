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


            foreach (Button btn in panel1.Controls.OfType<Button>().Where(b => b.Tag?.ToString() == "NUM"))
            {
                btn.Click += btnNumber_Click;
            }

            btnDelete.Click += btnDelete_Click;
        }

        //Login button

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryGetCredentials(out int userId, out string password))
                    return;

                Employee employee = employeeService.Authenticate(userId, password);
                if (employee is null)
                {
                    ShowLoginError("Invalid ID or password. Please try again.");
                    return;
                }

                RedirectEmployeeRole(employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // role-based redirection
        private void RedirectEmployeeRole(Employee employee)
        {
            Form next;
            switch (employee.Role)
            {
                case Role.Waiter:
                    next = new RestaurantOverviewForm(employee);
                    break;
                case Role.Barman:
                    next = new BarOrders(employee);
                    break;
                case Role.Chef:
                    next = new KitchenOrders(employee);
                    break;

                default:
                    MessageBox.Show("Unknown role. Cannot redirect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            this.Hide();
            next.Closed += (s, _) => this.Close();
            next.Show();
        }

        //Number buttons

        private void TextBox_Click(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
            activeTextBox.Focus();  
        }
        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            activeTextBox.Text += btn.Text;
        }

        //Delete button

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string text = activeTextBox.Text;
            if (text.Length > 0)
                activeTextBox.Text = text[..^1];
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

    }

}
