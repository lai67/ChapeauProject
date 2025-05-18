namespace ChapeauUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            Button openOrderFormBtn = new Button
            {
                Text = "Open Order Form",
                Location = new Point(10, 10),
                AutoSize = true
            };

            openOrderFormBtn.Click += (s, e) =>
            {
                OrderForm orderForm = new OrderForm();
                orderForm.ShowDialog(); // veya .Show()
            };

            Controls.Add(openOrderFormBtn);
        }
    }

}
