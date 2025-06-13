namespace ChapeauUI
{
    partial class BillForm
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
            lblCompleteBill = new Label();
            lblSubBill = new Label();
            divider1 = new Panel();
            panel1 = new Panel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            lblVatTotalCompBill = new Label();
            lblTotalPriceCompBill = new Label();
            lblVatValueCompBill = new Label();
            lblTotalPriceValueBill = new Label();
            lblVatInfoCompBill = new Label();
            completeBillFields = new Panel();
            lblAmtCompBill = new Label();
            lblPriceCompBill = new Label();
            lblItem = new Label();
            lblSubBillTotalValue = new Label();
            lblVatValueSubBill = new Label();
            lblTotalPriceSubBill = new Label();
            lblVatTotalSubBill = new Label();
            label7 = new Label();
            panel2 = new Panel();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox5 = new TextBox();
            btnRemoveAllFromSubBill = new Button();
            textBox6 = new TextBox();
            lblSplitEqually = new Label();
            btnPaySubBill = new Button();
            lblSplitValue = new Label();
            btnSplitDecrement = new Button();
            btnSplitIncrement = new Button();
            lstViewBill = new ListView();
            Item = new ColumnHeader();
            listViewSubBill = new ListView();
            columnHeader1 = new ColumnHeader();
            btnAddToSubBill = new Button();
            btnRemoveFromSubBill = new Button();
            btnPayBill = new Button();
            completeBillFields.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblCompleteBill
            // 
            lblCompleteBill.AutoSize = true;
            lblCompleteBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCompleteBill.Location = new Point(224, 117);
            lblCompleteBill.Margin = new Padding(5, 0, 5, 0);
            lblCompleteBill.Name = "lblCompleteBill";
            lblCompleteBill.Size = new Size(210, 45);
            lblCompleteBill.TabIndex = 0;
            lblCompleteBill.Text = "Complete Bill";
            // 
            // lblSubBill
            // 
            lblSubBill.AutoSize = true;
            lblSubBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubBill.Location = new Point(900, 117);
            lblSubBill.Margin = new Padding(5, 0, 5, 0);
            lblSubBill.Name = "lblSubBill";
            lblSubBill.Size = new Size(129, 45);
            lblSubBill.TabIndex = 1;
            lblSubBill.Text = "Sub-Bill";
            // 
            // divider1
            // 
            divider1.BackColor = SystemColors.Desktop;
            divider1.Location = new Point(647, 214);
            divider1.Margin = new Padding(5, 5, 5, 5);
            divider1.Name = "divider1";
            divider1.Size = new Size(5, 872);
            divider1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 5, 5, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1306, 59);
            panel1.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(198, 69);
            textBox1.Margin = new Padding(5, 5, 5, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0, 39);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(2, 221);
            textBox2.Margin = new Padding(5, 5, 5, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(0, 39);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(239, 166);
            textBox3.Margin = new Padding(5, 5, 5, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(0, 39);
            textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(647, 130);
            textBox4.Margin = new Padding(5, 5, 5, 5);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(0, 39);
            textBox4.TabIndex = 9;
            // 
            // lblVatTotalCompBill
            // 
            lblVatTotalCompBill.AutoSize = true;
            lblVatTotalCompBill.Location = new Point(78, 966);
            lblVatTotalCompBill.Margin = new Padding(5, 0, 5, 0);
            lblVatTotalCompBill.Name = "lblVatTotalCompBill";
            lblVatTotalCompBill.Size = new Size(112, 32);
            lblVatTotalCompBill.TabIndex = 10;
            lblVatTotalCompBill.Text = "VAT Total";
            // 
            // lblTotalPriceCompBill
            // 
            lblTotalPriceCompBill.AutoSize = true;
            lblTotalPriceCompBill.Location = new Point(78, 1034);
            lblTotalPriceCompBill.Margin = new Padding(5, 0, 5, 0);
            lblTotalPriceCompBill.Name = "lblTotalPriceCompBill";
            lblTotalPriceCompBill.Size = new Size(123, 32);
            lblTotalPriceCompBill.TabIndex = 12;
            lblTotalPriceCompBill.Text = "Total Price";
            // 
            // lblVatValueCompBill
            // 
            lblVatValueCompBill.AutoSize = true;
            lblVatValueCompBill.Location = new Point(528, 965);
            lblVatValueCompBill.Margin = new Padding(5, 0, 5, 0);
            lblVatValueCompBill.Name = "lblVatValueCompBill";
            lblVatValueCompBill.Size = new Size(40, 32);
            lblVatValueCompBill.TabIndex = 13;
            lblVatValueCompBill.Text = "€0";
            // 
            // lblTotalPriceValueBill
            // 
            lblTotalPriceValueBill.AutoSize = true;
            lblTotalPriceValueBill.Location = new Point(528, 1034);
            lblTotalPriceValueBill.Margin = new Padding(5, 0, 5, 0);
            lblTotalPriceValueBill.Name = "lblTotalPriceValueBill";
            lblTotalPriceValueBill.Size = new Size(40, 32);
            lblTotalPriceValueBill.TabIndex = 15;
            lblTotalPriceValueBill.Text = "€0";
            // 
            // lblVatInfoCompBill
            // 
            lblVatInfoCompBill.BackColor = Color.Transparent;
            lblVatInfoCompBill.BorderStyle = BorderStyle.FixedSingle;
            lblVatInfoCompBill.ForeColor = Color.Transparent;
            lblVatInfoCompBill.Location = new Point(52, 941);
            lblVatInfoCompBill.Margin = new Padding(5, 0, 5, 0);
            lblVatInfoCompBill.Name = "lblVatInfoCompBill";
            lblVatInfoCompBill.Size = new Size(554, 144);
            lblVatInfoCompBill.TabIndex = 16;
            lblVatInfoCompBill.Text = "label1";
            // 
            // completeBillFields
            // 
            completeBillFields.BackColor = SystemColors.Desktop;
            completeBillFields.Controls.Add(lblAmtCompBill);
            completeBillFields.Controls.Add(lblPriceCompBill);
            completeBillFields.Controls.Add(lblItem);
            completeBillFields.Location = new Point(52, 214);
            completeBillFields.Margin = new Padding(5, 5, 5, 5);
            completeBillFields.Name = "completeBillFields";
            completeBillFields.Size = new Size(556, 96);
            completeBillFields.TabIndex = 24;
            // 
            // lblAmtCompBill
            // 
            lblAmtCompBill.AutoSize = true;
            lblAmtCompBill.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblAmtCompBill.ForeColor = SystemColors.Control;
            lblAmtCompBill.Location = new Point(442, 27);
            lblAmtCompBill.Margin = new Padding(5, 0, 5, 0);
            lblAmtCompBill.Name = "lblAmtCompBill";
            lblAmtCompBill.Size = new Size(72, 37);
            lblAmtCompBill.TabIndex = 27;
            lblAmtCompBill.Text = "Amt.";
            // 
            // lblPriceCompBill
            // 
            lblPriceCompBill.AutoSize = true;
            lblPriceCompBill.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPriceCompBill.ForeColor = SystemColors.Control;
            lblPriceCompBill.Location = new Point(291, 27);
            lblPriceCompBill.Margin = new Padding(5, 0, 5, 0);
            lblPriceCompBill.Name = "lblPriceCompBill";
            lblPriceCompBill.Size = new Size(74, 37);
            lblPriceCompBill.TabIndex = 26;
            lblPriceCompBill.Text = "Price";
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblItem.ForeColor = SystemColors.Control;
            lblItem.Location = new Point(26, 27);
            lblItem.Margin = new Padding(5, 0, 5, 0);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(70, 37);
            lblItem.TabIndex = 25;
            lblItem.Text = "Item";
            // 
            // lblSubBillTotalValue
            // 
            lblSubBillTotalValue.AutoSize = true;
            lblSubBillTotalValue.Location = new Point(1165, 1034);
            lblSubBillTotalValue.Margin = new Padding(5, 0, 5, 0);
            lblSubBillTotalValue.Name = "lblSubBillTotalValue";
            lblSubBillTotalValue.Size = new Size(40, 32);
            lblSubBillTotalValue.TabIndex = 30;
            lblSubBillTotalValue.Text = "€0";
            // 
            // lblVatValueSubBill
            // 
            lblVatValueSubBill.AutoSize = true;
            lblVatValueSubBill.Location = new Point(1165, 965);
            lblVatValueSubBill.Margin = new Padding(5, 0, 5, 0);
            lblVatValueSubBill.Name = "lblVatValueSubBill";
            lblVatValueSubBill.Size = new Size(40, 32);
            lblVatValueSubBill.TabIndex = 28;
            lblVatValueSubBill.Text = "€0";
            // 
            // lblTotalPriceSubBill
            // 
            lblTotalPriceSubBill.AutoSize = true;
            lblTotalPriceSubBill.Location = new Point(715, 1034);
            lblTotalPriceSubBill.Margin = new Padding(5, 0, 5, 0);
            lblTotalPriceSubBill.Name = "lblTotalPriceSubBill";
            lblTotalPriceSubBill.Size = new Size(123, 32);
            lblTotalPriceSubBill.TabIndex = 27;
            lblTotalPriceSubBill.Text = "Total Price";
            // 
            // lblVatTotalSubBill
            // 
            lblVatTotalSubBill.AutoSize = true;
            lblVatTotalSubBill.Location = new Point(715, 965);
            lblVatTotalSubBill.Margin = new Padding(5, 0, 5, 0);
            lblVatTotalSubBill.Name = "lblVatTotalSubBill";
            lblVatTotalSubBill.Size = new Size(202, 32);
            lblVatTotalSubBill.TabIndex = 25;
            lblVatTotalSubBill.Text = "Sub-Bill VAT Total";
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.ForeColor = Color.Transparent;
            label7.Location = new Point(689, 941);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(554, 144);
            label7.TabIndex = 31;
            label7.Text = "label1";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Desktop;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(689, 214);
            panel2.Margin = new Padding(5, 5, 5, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(556, 96);
            panel2.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(442, 27);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(72, 37);
            label3.TabIndex = 27;
            label3.Text = "Amt.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(283, 27);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(74, 37);
            label4.TabIndex = 26;
            label4.Text = "Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(26, 27);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(70, 37);
            label5.TabIndex = 25;
            label5.Text = "Item";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(876, 166);
            textBox5.Margin = new Padding(5, 5, 5, 5);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(0, 39);
            textBox5.TabIndex = 30;
            // 
            // btnRemoveAllFromSubBill
            // 
            btnRemoveAllFromSubBill.BackColor = SystemColors.ButtonShadow;
            btnRemoveAllFromSubBill.Location = new Point(972, 837);
            btnRemoveAllFromSubBill.Margin = new Padding(5, 5, 5, 5);
            btnRemoveAllFromSubBill.Name = "btnRemoveAllFromSubBill";
            btnRemoveAllFromSubBill.Size = new Size(271, 82);
            btnRemoveAllFromSubBill.TabIndex = 32;
            btnRemoveAllFromSubBill.Text = "Remove All";
            btnRemoveAllFromSubBill.UseVisualStyleBackColor = false;
            btnRemoveAllFromSubBill.Click += btnRemoveAllFromSubBill_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(652, 837);
            textBox6.Margin = new Padding(5, 5, 5, 5);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(0, 39);
            textBox6.TabIndex = 33;
            // 
            // lblSplitEqually
            // 
            lblSplitEqually.AutoSize = true;
            lblSplitEqually.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSplitEqually.Location = new Point(250, 1090);
            lblSplitEqually.Margin = new Padding(5, 0, 5, 0);
            lblSplitEqually.Name = "lblSplitEqually";
            lblSplitEqually.Size = new Size(150, 32);
            lblSplitEqually.TabIndex = 35;
            lblSplitEqually.Text = "Split equally:";
            // 
            // btnPaySubBill
            // 
            btnPaySubBill.BackColor = SystemColors.ButtonShadow;
            btnPaySubBill.Location = new Point(970, 1104);
            btnPaySubBill.Margin = new Padding(5, 5, 5, 5);
            btnPaySubBill.Name = "btnPaySubBill";
            btnPaySubBill.Size = new Size(273, 78);
            btnPaySubBill.TabIndex = 38;
            btnPaySubBill.Text = "Pay Sub-Bill";
            btnPaySubBill.UseVisualStyleBackColor = false;
            btnPaySubBill.Click += btnPaySubBill_Click;
            // 
            // lblSplitValue
            // 
            lblSplitValue.AutoSize = true;
            lblSplitValue.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblSplitValue.Location = new Point(296, 1118);
            lblSplitValue.Margin = new Padding(5, 0, 5, 0);
            lblSplitValue.Name = "lblSplitValue";
            lblSplitValue.Size = new Size(50, 60);
            lblSplitValue.TabIndex = 50;
            lblSplitValue.Text = "0";
            // 
            // btnSplitDecrement
            // 
            btnSplitDecrement.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSplitDecrement.Location = new Point(52, 1125);
            btnSplitDecrement.Margin = new Padding(5, 5, 5, 5);
            btnSplitDecrement.Name = "btnSplitDecrement";
            btnSplitDecrement.Size = new Size(162, 58);
            btnSplitDecrement.TabIndex = 51;
            btnSplitDecrement.Text = " - ";
            btnSplitDecrement.UseVisualStyleBackColor = true;
            btnSplitDecrement.Click += btnSplitDecrement_Click;
            // 
            // btnSplitIncrement
            // 
            btnSplitIncrement.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSplitIncrement.Location = new Point(440, 1125);
            btnSplitIncrement.Margin = new Padding(5, 5, 5, 5);
            btnSplitIncrement.Name = "btnSplitIncrement";
            btnSplitIncrement.Size = new Size(162, 58);
            btnSplitIncrement.TabIndex = 52;
            btnSplitIncrement.Text = "+";
            btnSplitIncrement.UseVisualStyleBackColor = true;
            btnSplitIncrement.Click += btnSplitIncrement_Click;
            // 
            // lstViewBill
            // 
            lstViewBill.Columns.AddRange(new ColumnHeader[] { Item });
            lstViewBill.Location = new Point(52, 315);
            lstViewBill.Margin = new Padding(5, 5, 5, 5);
            lstViewBill.Name = "lstViewBill";
            lstViewBill.Size = new Size(553, 510);
            lstViewBill.TabIndex = 71;
            lstViewBill.UseCompatibleStateImageBehavior = false;
            lstViewBill.View = View.Details;
            // 
            // listViewSubBill
            // 
            listViewSubBill.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            listViewSubBill.Location = new Point(689, 315);
            listViewSubBill.Margin = new Padding(5, 5, 5, 5);
            listViewSubBill.Name = "listViewSubBill";
            listViewSubBill.Size = new Size(553, 510);
            listViewSubBill.TabIndex = 72;
            listViewSubBill.UseCompatibleStateImageBehavior = false;
            listViewSubBill.View = View.Details;
            // 
            // btnAddToSubBill
            // 
            btnAddToSubBill.BackColor = SystemColors.ButtonShadow;
            btnAddToSubBill.Location = new Point(52, 837);
            btnAddToSubBill.Margin = new Padding(5, 5, 5, 5);
            btnAddToSubBill.Name = "btnAddToSubBill";
            btnAddToSubBill.Size = new Size(556, 82);
            btnAddToSubBill.TabIndex = 73;
            btnAddToSubBill.Text = "Add to Sub-Bill";
            btnAddToSubBill.UseVisualStyleBackColor = false;
            btnAddToSubBill.Click += btnAddToSubBill_Click;
            // 
            // btnRemoveFromSubBill
            // 
            btnRemoveFromSubBill.BackColor = SystemColors.ButtonShadow;
            btnRemoveFromSubBill.Location = new Point(689, 837);
            btnRemoveFromSubBill.Margin = new Padding(5, 5, 5, 5);
            btnRemoveFromSubBill.Name = "btnRemoveFromSubBill";
            btnRemoveFromSubBill.Size = new Size(273, 82);
            btnRemoveFromSubBill.TabIndex = 74;
            btnRemoveFromSubBill.Text = "Remove";
            btnRemoveFromSubBill.UseVisualStyleBackColor = false;
            btnRemoveFromSubBill.Click += btnRemoveFromSubBill_Click;
            // 
            // btnPayBill
            // 
            btnPayBill.BackColor = SystemColors.ButtonShadow;
            btnPayBill.Location = new Point(689, 1104);
            btnPayBill.Margin = new Padding(5, 5, 5, 5);
            btnPayBill.Name = "btnPayBill";
            btnPayBill.Size = new Size(273, 78);
            btnPayBill.TabIndex = 75;
            btnPayBill.Text = "Pay Bill";
            btnPayBill.UseVisualStyleBackColor = false;
            btnPayBill.Click += btnPayBill_Click;
            // 
            // BillForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1302, 1205);
            Controls.Add(btnPayBill);
            Controls.Add(btnRemoveFromSubBill);
            Controls.Add(btnAddToSubBill);
            Controls.Add(listViewSubBill);
            Controls.Add(lstViewBill);
            Controls.Add(btnSplitIncrement);
            Controls.Add(btnSplitDecrement);
            Controls.Add(lblSplitValue);
            Controls.Add(btnPaySubBill);
            Controls.Add(lblSplitEqually);
            Controls.Add(textBox6);
            Controls.Add(btnRemoveAllFromSubBill);
            Controls.Add(panel2);
            Controls.Add(textBox5);
            Controls.Add(lblSubBillTotalValue);
            Controls.Add(lblVatValueSubBill);
            Controls.Add(lblTotalPriceSubBill);
            Controls.Add(lblVatTotalSubBill);
            Controls.Add(label7);
            Controls.Add(completeBillFields);
            Controls.Add(lblTotalPriceValueBill);
            Controls.Add(lblVatValueCompBill);
            Controls.Add(lblTotalPriceCompBill);
            Controls.Add(lblVatTotalCompBill);
            Controls.Add(lblVatInfoCompBill);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Controls.Add(divider1);
            Controls.Add(lblSubBill);
            Controls.Add(lblCompleteBill);
            Margin = new Padding(5, 5, 5, 5);
            Name = "BillForm";
            Text = "BillForm";
            Load += BillForm_Load;
            completeBillFields.ResumeLayout(false);
            completeBillFields.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCompleteBill;
        private Label lblSubBill;
        private Panel divider1;
        private Panel panel1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label lblVatTotalCompBill;
        private Label lblTotalPriceCompBill;
        private Label lblVatValueCompBill;
        private Label lblTotalPriceValueBill;
        public Label lblVatInfoCompBill;
        private Panel completeBillFields;
        private Label lblItem;
        private Label lblPriceCompBill;
        private Label lblSubBillTotalValue;
        private Label lblVatValueSubBill;
        private Label lblTotalPriceSubBill;
        private Label lblVatTotalSubBill;
        public Label label7;
        private Label lblAmtCompBill;
        private Panel panel2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox5;
        private Button btnRemoveAllFromSubBill;
        private TextBox textBox6;
        private Label lblSplitEqually;
        private Button btnPaySubBill;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Label lblSplitValue;
        private Button btnSplitDecrement;
        private Button btnSplitIncrement;
        private ListView lstViewBill;
        private ColumnHeader Item;
        private ListView listViewSubBill;
        private ColumnHeader columnHeader1;
        private Button btnAddToSubBill;
        private Button btnRemoveFromSubBill;
        private Button btnPayBill;
    }
}