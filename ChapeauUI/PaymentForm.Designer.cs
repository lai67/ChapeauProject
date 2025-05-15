namespace ChapeauUI
{
    partial class PaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelCompleteBill = new Label();
            labelSubBill = new Label();
            labelPaymentOptions = new Label();
            divider1 = new Panel();
            panel1 = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            divider2 = new Panel();
            labelVatLowCompBill = new Label();
            labelVatHighCompBill = new Label();
            labelTotalPriceCompBill = new Label();
            labelVatLowValueCompBill = new Label();
            labelVatHighValueCompBill = new Label();
            labelTotalPriceValueCompBill = new Label();
            lblVatInfoCompBill = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            labelTotalPriceSubBill = new Label();
            labelVatHighSubBill = new Label();
            labelVatLowSubBill = new Label();
            lblVatInfoSubBill = new Label();
            SuspendLayout();
            // 
            // labelCompleteBill
            // 
            labelCompleteBill.AutoSize = true;
            labelCompleteBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelCompleteBill.Location = new Point(138, 73);
            labelCompleteBill.Name = "labelCompleteBill";
            labelCompleteBill.Size = new Size(128, 28);
            labelCompleteBill.TabIndex = 0;
            labelCompleteBill.Text = "Complete Bill";
            // 
            // labelSubBill
            // 
            labelSubBill.AutoSize = true;
            labelSubBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubBill.Location = new Point(554, 73);
            labelSubBill.Name = "labelSubBill";
            labelSubBill.Size = new Size(80, 28);
            labelSubBill.TabIndex = 1;
            labelSubBill.Text = "Sub-Bill";
            // 
            // labelPaymentOptions
            // 
            labelPaymentOptions.AutoSize = true;
            labelPaymentOptions.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelPaymentOptions.Location = new Point(908, 73);
            labelPaymentOptions.Name = "labelPaymentOptions";
            labelPaymentOptions.Size = new Size(162, 28);
            labelPaymentOptions.TabIndex = 2;
            labelPaymentOptions.Text = "Payment Options";
            // 
            // divider1
            // 
            divider1.BackColor = SystemColors.Desktop;
            divider1.Location = new Point(398, 134);
            divider1.Name = "divider1";
            divider1.Size = new Size(3, 586);
            divider1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1180, 37);
            panel1.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(122, 43);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0, 27);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1, 138);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(0, 27);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(147, 104);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(0, 27);
            textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(398, 81);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(0, 27);
            textBox4.TabIndex = 9;
            // 
            // divider2
            // 
            divider2.BackColor = SystemColors.Desktop;
            divider2.Location = new Point(796, 134);
            divider2.Name = "divider2";
            divider2.Size = new Size(3, 586);
            divider2.TabIndex = 5;
            // 
            // labelVatLowCompBill
            // 
            labelVatLowCompBill.AutoSize = true;
            labelVatLowCompBill.Location = new Point(48, 611);
            labelVatLowCompBill.Name = "labelVatLowCompBill";
            labelVatLowCompBill.Size = new Size(99, 20);
            labelVatLowCompBill.TabIndex = 10;
            labelVatLowCompBill.Text = "VAT Low (9%)";
            // 
            // labelVatHighCompBill
            // 
            labelVatHighCompBill.AutoSize = true;
            labelVatHighCompBill.Location = new Point(48, 648);
            labelVatHighCompBill.Name = "labelVatHighCompBill";
            labelVatHighCompBill.Size = new Size(112, 20);
            labelVatHighCompBill.TabIndex = 11;
            labelVatHighCompBill.Text = "VAT High (21%)";
            // 
            // labelTotalPriceCompBill
            // 
            labelTotalPriceCompBill.AutoSize = true;
            labelTotalPriceCompBill.Location = new Point(48, 690);
            labelTotalPriceCompBill.Name = "labelTotalPriceCompBill";
            labelTotalPriceCompBill.Size = new Size(78, 20);
            labelTotalPriceCompBill.TabIndex = 12;
            labelTotalPriceCompBill.Text = "Total Price";
            // 
            // labelVatLowValueCompBill
            // 
            labelVatLowValueCompBill.AutoSize = true;
            labelVatLowValueCompBill.Location = new Point(306, 611);
            labelVatLowValueCompBill.Name = "labelVatLowValueCompBill";
            labelVatLowValueCompBill.Size = new Size(25, 20);
            labelVatLowValueCompBill.TabIndex = 13;
            labelVatLowValueCompBill.Text = "€0";
            // 
            // labelVatHighValueCompBill
            // 
            labelVatHighValueCompBill.AutoSize = true;
            labelVatHighValueCompBill.Location = new Point(306, 648);
            labelVatHighValueCompBill.Name = "labelVatHighValueCompBill";
            labelVatHighValueCompBill.Size = new Size(25, 20);
            labelVatHighValueCompBill.TabIndex = 14;
            labelVatHighValueCompBill.Text = "€0";
            // 
            // labelTotalPriceValueCompBill
            // 
            labelTotalPriceValueCompBill.AutoSize = true;
            labelTotalPriceValueCompBill.Location = new Point(306, 690);
            labelTotalPriceValueCompBill.Name = "labelTotalPriceValueCompBill";
            labelTotalPriceValueCompBill.Size = new Size(25, 20);
            labelTotalPriceValueCompBill.TabIndex = 15;
            labelTotalPriceValueCompBill.Text = "€0";
            // 
            // lblVatInfoCompBill
            // 
            lblVatInfoCompBill.BackColor = Color.Transparent;
            lblVatInfoCompBill.BorderStyle = BorderStyle.FixedSingle;
            lblVatInfoCompBill.ForeColor = Color.Transparent;
            lblVatInfoCompBill.Location = new Point(32, 588);
            lblVatInfoCompBill.Name = "lblVatInfoCompBill";
            lblVatInfoCompBill.Size = new Size(318, 139);
            lblVatInfoCompBill.TabIndex = 16;
            lblVatInfoCompBill.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(714, 690);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 22;
            label1.Text = "€0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(714, 648);
            label2.Name = "label2";
            label2.Size = new Size(25, 20);
            label2.TabIndex = 21;
            label2.Text = "€0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(714, 611);
            label3.Name = "label3";
            label3.Size = new Size(25, 20);
            label3.TabIndex = 20;
            label3.Text = "€0";
            // 
            // labelTotalPriceSubBill
            // 
            labelTotalPriceSubBill.AutoSize = true;
            labelTotalPriceSubBill.Location = new Point(456, 690);
            labelTotalPriceSubBill.Name = "labelTotalPriceSubBill";
            labelTotalPriceSubBill.Size = new Size(78, 20);
            labelTotalPriceSubBill.TabIndex = 19;
            labelTotalPriceSubBill.Text = "Total Price";
            // 
            // labelVatHighSubBill
            // 
            labelVatHighSubBill.AutoSize = true;
            labelVatHighSubBill.Location = new Point(456, 648);
            labelVatHighSubBill.Name = "labelVatHighSubBill";
            labelVatHighSubBill.Size = new Size(112, 20);
            labelVatHighSubBill.TabIndex = 18;
            labelVatHighSubBill.Text = "VAT High (21%)";
            // 
            // labelVatLowSubBill
            // 
            labelVatLowSubBill.AutoSize = true;
            labelVatLowSubBill.Location = new Point(456, 611);
            labelVatLowSubBill.Name = "labelVatLowSubBill";
            labelVatLowSubBill.Size = new Size(99, 20);
            labelVatLowSubBill.TabIndex = 17;
            labelVatLowSubBill.Text = "VAT Low (9%)";
            // 
            // lblVatInfoSubBill
            // 
            lblVatInfoSubBill.BackColor = Color.Transparent;
            lblVatInfoSubBill.BorderStyle = BorderStyle.FixedSingle;
            lblVatInfoSubBill.ForeColor = Color.Transparent;
            lblVatInfoSubBill.Location = new Point(440, 588);
            lblVatInfoSubBill.Name = "lblVatInfoSubBill";
            lblVatInfoSubBill.Size = new Size(318, 139);
            lblVatInfoSubBill.TabIndex = 23;
            lblVatInfoSubBill.Text = "label1";
            // 
            // PaymentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1176, 753);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(labelTotalPriceSubBill);
            Controls.Add(labelVatHighSubBill);
            Controls.Add(labelVatLowSubBill);
            Controls.Add(lblVatInfoSubBill);
            Controls.Add(labelTotalPriceValueCompBill);
            Controls.Add(labelVatHighValueCompBill);
            Controls.Add(labelVatLowValueCompBill);
            Controls.Add(labelTotalPriceCompBill);
            Controls.Add(labelVatHighCompBill);
            Controls.Add(labelVatLowCompBill);
            Controls.Add(lblVatInfoCompBill);
            Controls.Add(divider2);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Controls.Add(divider1);
            Controls.Add(labelPaymentOptions);
            Controls.Add(labelSubBill);
            Controls.Add(labelCompleteBill);
            Name = "PaymentForm";
            Text = "PaymentForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCompleteBill;
        private Label labelSubBill;
        private Label labelPaymentOptions;
        private Panel divider1;
        private Panel panel1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Panel divider2;
        private Label labelVatLowCompBill;
        private Label labelVatHighCompBill;
        private Label labelTotalPriceCompBill;
        private Label labelVatLowValueCompBill;
        private Label labelVatHighValueCompBill;
        private Label labelTotalPriceValueCompBill;
        public Label lblVatInfoCompBill;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label labelTotalPriceSubBill;
        private Label labelVatHighSubBill;
        private Label labelVatLowSubBill;
        public Label lblVatInfoSubBill;
    }
}