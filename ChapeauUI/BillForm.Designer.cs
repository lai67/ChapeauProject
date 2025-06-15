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
            btnPaySubBill = new Button();
            lstViewBill = new ListView();
            Item = new ColumnHeader();
            listViewSubBill = new ListView();
            columnHeader1 = new ColumnHeader();
            btnAddToSubBill = new Button();
            btnRemoveFromSubBill = new Button();
            btnPayBill = new Button();
            lblVatLowBill = new Label();
            lblVatHighBill = new Label();
            lblVatHighBillValue = new Label();
            lblVatLowBillValue = new Label();
            lblVatLowValueSubBill = new Label();
            lblVatHighValueSubBill = new Label();
            lblVatHighSubBill = new Label();
            lblVatLowSubBill = new Label();
            completeBillFields.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblCompleteBill
            // 
            lblCompleteBill.AutoSize = true;
            lblCompleteBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCompleteBill.Location = new Point(138, 73);
            lblCompleteBill.Name = "lblCompleteBill";
            lblCompleteBill.Size = new Size(128, 28);
            lblCompleteBill.TabIndex = 0;
            lblCompleteBill.Text = "Complete Bill";
            // 
            // lblSubBill
            // 
            lblSubBill.AutoSize = true;
            lblSubBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubBill.Location = new Point(554, 73);
            lblSubBill.Name = "lblSubBill";
            lblSubBill.Size = new Size(80, 28);
            lblSubBill.TabIndex = 1;
            lblSubBill.Text = "Sub-Bill";
            // 
            // divider1
            // 
            divider1.BackColor = SystemColors.Desktop;
            divider1.Location = new Point(398, 134);
            divider1.Name = "divider1";
            divider1.Size = new Size(3, 545);
            divider1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Red;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(804, 37);
            panel1.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(122, 43);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(2, 27);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1, 138);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(2, 27);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(147, 104);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(2, 27);
            textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(398, 81);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(2, 27);
            textBox4.TabIndex = 9;
            // 
            // lblVatTotalCompBill
            // 
            lblVatTotalCompBill.AutoSize = true;
            lblVatTotalCompBill.Location = new Point(42, 651);
            lblVatTotalCompBill.Name = "lblVatTotalCompBill";
            lblVatTotalCompBill.Size = new Size(71, 20);
            lblVatTotalCompBill.TabIndex = 10;
            lblVatTotalCompBill.Text = "VAT Total";
            // 
            // lblTotalPriceCompBill
            // 
            lblTotalPriceCompBill.AutoSize = true;
            lblTotalPriceCompBill.Location = new Point(42, 676);
            lblTotalPriceCompBill.Name = "lblTotalPriceCompBill";
            lblTotalPriceCompBill.Size = new Size(78, 20);
            lblTotalPriceCompBill.TabIndex = 12;
            lblTotalPriceCompBill.Text = "Total Price";
            // 
            // lblVatValueCompBill
            // 
            lblVatValueCompBill.AutoSize = true;
            lblVatValueCompBill.Location = new Point(317, 651);
            lblVatValueCompBill.Name = "lblVatValueCompBill";
            lblVatValueCompBill.Size = new Size(44, 20);
            lblVatValueCompBill.TabIndex = 13;
            lblVatValueCompBill.Text = "€0,00";
            lblVatValueCompBill.Click += lblVatValueCompBill_Click;
            // 
            // lblTotalPriceValueBill
            // 
            lblTotalPriceValueBill.AutoSize = true;
            lblTotalPriceValueBill.Location = new Point(317, 676);
            lblTotalPriceValueBill.Name = "lblTotalPriceValueBill";
            lblTotalPriceValueBill.Size = new Size(44, 20);
            lblTotalPriceValueBill.TabIndex = 15;
            lblTotalPriceValueBill.Text = "€0,00";
            // 
            // lblVatInfoCompBill
            // 
            lblVatInfoCompBill.BackColor = Color.Transparent;
            lblVatInfoCompBill.BorderStyle = BorderStyle.FixedSingle;
            lblVatInfoCompBill.ForeColor = Color.Transparent;
            lblVatInfoCompBill.Location = new Point(32, 588);
            lblVatInfoCompBill.Name = "lblVatInfoCompBill";
            lblVatInfoCompBill.Size = new Size(342, 120);
            lblVatInfoCompBill.TabIndex = 16;
            lblVatInfoCompBill.Text = "label1";
            // 
            // completeBillFields
            // 
            completeBillFields.BackColor = SystemColors.Desktop;
            completeBillFields.Controls.Add(lblAmtCompBill);
            completeBillFields.Controls.Add(lblPriceCompBill);
            completeBillFields.Controls.Add(lblItem);
            completeBillFields.Location = new Point(32, 134);
            completeBillFields.Name = "completeBillFields";
            completeBillFields.Size = new Size(342, 60);
            completeBillFields.TabIndex = 24;
            // 
            // lblAmtCompBill
            // 
            lblAmtCompBill.AutoSize = true;
            lblAmtCompBill.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblAmtCompBill.ForeColor = SystemColors.Control;
            lblAmtCompBill.Location = new Point(272, 17);
            lblAmtCompBill.Name = "lblAmtCompBill";
            lblAmtCompBill.Size = new Size(46, 23);
            lblAmtCompBill.TabIndex = 27;
            lblAmtCompBill.Text = "Amt.";
            // 
            // lblPriceCompBill
            // 
            lblPriceCompBill.AutoSize = true;
            lblPriceCompBill.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblPriceCompBill.ForeColor = SystemColors.Control;
            lblPriceCompBill.Location = new Point(179, 17);
            lblPriceCompBill.Name = "lblPriceCompBill";
            lblPriceCompBill.Size = new Size(47, 23);
            lblPriceCompBill.TabIndex = 26;
            lblPriceCompBill.Text = "Price";
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblItem.ForeColor = SystemColors.Control;
            lblItem.Location = new Point(16, 17);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(45, 23);
            lblItem.TabIndex = 25;
            lblItem.Text = "Item";
            // 
            // lblSubBillTotalValue
            // 
            lblSubBillTotalValue.AutoSize = true;
            lblSubBillTotalValue.Location = new Point(709, 677);
            lblSubBillTotalValue.Name = "lblSubBillTotalValue";
            lblSubBillTotalValue.Size = new Size(44, 20);
            lblSubBillTotalValue.TabIndex = 30;
            lblSubBillTotalValue.Text = "€0,00";
            // 
            // lblVatValueSubBill
            // 
            lblVatValueSubBill.AutoSize = true;
            lblVatValueSubBill.Location = new Point(709, 651);
            lblVatValueSubBill.Name = "lblVatValueSubBill";
            lblVatValueSubBill.Size = new Size(44, 20);
            lblVatValueSubBill.TabIndex = 28;
            lblVatValueSubBill.Text = "€0,00";
            // 
            // lblTotalPriceSubBill
            // 
            lblTotalPriceSubBill.AutoSize = true;
            lblTotalPriceSubBill.Location = new Point(435, 676);
            lblTotalPriceSubBill.Name = "lblTotalPriceSubBill";
            lblTotalPriceSubBill.Size = new Size(78, 20);
            lblTotalPriceSubBill.TabIndex = 27;
            lblTotalPriceSubBill.Text = "Total Price";
            // 
            // lblVatTotalSubBill
            // 
            lblVatTotalSubBill.AutoSize = true;
            lblVatTotalSubBill.Location = new Point(435, 651);
            lblVatTotalSubBill.Name = "lblVatTotalSubBill";
            lblVatTotalSubBill.Size = new Size(127, 20);
            lblVatTotalSubBill.TabIndex = 25;
            lblVatTotalSubBill.Text = "Sub-Bill VAT Total";
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.BorderStyle = BorderStyle.FixedSingle;
            label7.ForeColor = Color.Transparent;
            label7.Location = new Point(424, 588);
            label7.Name = "label7";
            label7.Size = new Size(342, 120);
            label7.TabIndex = 31;
            label7.Text = "label1";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Desktop;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(424, 134);
            panel2.Name = "panel2";
            panel2.Size = new Size(342, 60);
            panel2.TabIndex = 31;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(272, 17);
            label3.Name = "label3";
            label3.Size = new Size(46, 23);
            label3.TabIndex = 27;
            label3.Text = "Amt.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(174, 17);
            label4.Name = "label4";
            label4.Size = new Size(47, 23);
            label4.TabIndex = 26;
            label4.Text = "Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(16, 17);
            label5.Name = "label5";
            label5.Size = new Size(45, 23);
            label5.TabIndex = 25;
            label5.Text = "Item";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(539, 104);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(2, 27);
            textBox5.TabIndex = 30;
            // 
            // btnRemoveAllFromSubBill
            // 
            btnRemoveAllFromSubBill.BackColor = SystemColors.ButtonShadow;
            btnRemoveAllFromSubBill.Location = new Point(598, 523);
            btnRemoveAllFromSubBill.Name = "btnRemoveAllFromSubBill";
            btnRemoveAllFromSubBill.Size = new Size(167, 51);
            btnRemoveAllFromSubBill.TabIndex = 32;
            btnRemoveAllFromSubBill.Text = "Remove All";
            btnRemoveAllFromSubBill.UseVisualStyleBackColor = false;
            btnRemoveAllFromSubBill.Click += btnRemoveAllFromSubBill_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(401, 523);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(2, 27);
            textBox6.TabIndex = 33;
            // 
            // btnPaySubBill
            // 
            btnPaySubBill.BackColor = SystemColors.ButtonShadow;
            btnPaySubBill.Location = new Point(424, 711);
            btnPaySubBill.Name = "btnPaySubBill";
            btnPaySubBill.Size = new Size(342, 49);
            btnPaySubBill.TabIndex = 38;
            btnPaySubBill.Text = "Pay Sub-Bill";
            btnPaySubBill.UseVisualStyleBackColor = false;
            btnPaySubBill.Click += btnPaySubBill_Click;
            // 
            // lstViewBill
            // 
            lstViewBill.Columns.AddRange(new ColumnHeader[] { Item });
            lstViewBill.Location = new Point(32, 197);
            lstViewBill.Name = "lstViewBill";
            lstViewBill.Size = new Size(342, 320);
            lstViewBill.TabIndex = 71;
            lstViewBill.UseCompatibleStateImageBehavior = false;
            lstViewBill.View = View.Details;
            // 
            // listViewSubBill
            // 
            listViewSubBill.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            listViewSubBill.Location = new Point(424, 197);
            listViewSubBill.Name = "listViewSubBill";
            listViewSubBill.Size = new Size(342, 320);
            listViewSubBill.TabIndex = 72;
            listViewSubBill.UseCompatibleStateImageBehavior = false;
            listViewSubBill.View = View.Details;
            // 
            // btnAddToSubBill
            // 
            btnAddToSubBill.BackColor = SystemColors.ButtonShadow;
            btnAddToSubBill.Location = new Point(32, 523);
            btnAddToSubBill.Name = "btnAddToSubBill";
            btnAddToSubBill.Size = new Size(342, 51);
            btnAddToSubBill.TabIndex = 73;
            btnAddToSubBill.Text = "Add to Sub-Bill";
            btnAddToSubBill.UseVisualStyleBackColor = false;
            btnAddToSubBill.Click += btnAddToSubBill_Click;
            // 
            // btnRemoveFromSubBill
            // 
            btnRemoveFromSubBill.BackColor = SystemColors.ButtonShadow;
            btnRemoveFromSubBill.Location = new Point(424, 523);
            btnRemoveFromSubBill.Name = "btnRemoveFromSubBill";
            btnRemoveFromSubBill.Size = new Size(168, 51);
            btnRemoveFromSubBill.TabIndex = 74;
            btnRemoveFromSubBill.Text = "Remove";
            btnRemoveFromSubBill.UseVisualStyleBackColor = false;
            btnRemoveFromSubBill.Click += btnRemoveFromSubBill_Click;
            // 
            // btnPayBill
            // 
            btnPayBill.BackColor = SystemColors.ButtonShadow;
            btnPayBill.Location = new Point(32, 711);
            btnPayBill.Name = "btnPayBill";
            btnPayBill.Size = new Size(342, 49);
            btnPayBill.TabIndex = 75;
            btnPayBill.Text = "Pay Bill";
            btnPayBill.UseVisualStyleBackColor = false;
            btnPayBill.Click += btnPayBill_Click;
            // 
            // lblVatLowBill
            // 
            lblVatLowBill.AutoSize = true;
            lblVatLowBill.Location = new Point(42, 599);
            lblVatLowBill.Name = "lblVatLowBill";
            lblVatLowBill.Size = new Size(62, 20);
            lblVatLowBill.TabIndex = 76;
            lblVatLowBill.Text = "VAT low";
            // 
            // lblVatHighBill
            // 
            lblVatHighBill.AutoSize = true;
            lblVatHighBill.Location = new Point(42, 624);
            lblVatHighBill.Name = "lblVatHighBill";
            lblVatHighBill.Size = new Size(67, 20);
            lblVatHighBill.TabIndex = 77;
            lblVatHighBill.Text = "VAT high";
            // 
            // lblVatHighBillValue
            // 
            lblVatHighBillValue.AutoSize = true;
            lblVatHighBillValue.Location = new Point(317, 624);
            lblVatHighBillValue.Name = "lblVatHighBillValue";
            lblVatHighBillValue.Size = new Size(44, 20);
            lblVatHighBillValue.TabIndex = 78;
            lblVatHighBillValue.Text = "€0,00";
            // 
            // lblVatLowBillValue
            // 
            lblVatLowBillValue.AutoSize = true;
            lblVatLowBillValue.Location = new Point(317, 599);
            lblVatLowBillValue.Name = "lblVatLowBillValue";
            lblVatLowBillValue.Size = new Size(44, 20);
            lblVatLowBillValue.TabIndex = 79;
            lblVatLowBillValue.Text = "€0,00";
            // 
            // lblVatLowValueSubBill
            // 
            lblVatLowValueSubBill.AutoSize = true;
            lblVatLowValueSubBill.Location = new Point(709, 599);
            lblVatLowValueSubBill.Name = "lblVatLowValueSubBill";
            lblVatLowValueSubBill.Size = new Size(44, 20);
            lblVatLowValueSubBill.TabIndex = 83;
            lblVatLowValueSubBill.Text = "€0,00";
            // 
            // lblVatHighValueSubBill
            // 
            lblVatHighValueSubBill.AutoSize = true;
            lblVatHighValueSubBill.Location = new Point(709, 624);
            lblVatHighValueSubBill.Name = "lblVatHighValueSubBill";
            lblVatHighValueSubBill.Size = new Size(44, 20);
            lblVatHighValueSubBill.TabIndex = 82;
            lblVatHighValueSubBill.Text = "€0,00";
            // 
            // lblVatHighSubBill
            // 
            lblVatHighSubBill.AutoSize = true;
            lblVatHighSubBill.Location = new Point(434, 624);
            lblVatHighSubBill.Name = "lblVatHighSubBill";
            lblVatHighSubBill.Size = new Size(67, 20);
            lblVatHighSubBill.TabIndex = 81;
            lblVatHighSubBill.Text = "VAT high";
            // 
            // lblVatLowSubBill
            // 
            lblVatLowSubBill.AutoSize = true;
            lblVatLowSubBill.Location = new Point(434, 599);
            lblVatLowSubBill.Name = "lblVatLowSubBill";
            lblVatLowSubBill.Size = new Size(62, 20);
            lblVatLowSubBill.TabIndex = 80;
            lblVatLowSubBill.Text = "VAT low";
            // 
            // BillForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 772);
            Controls.Add(lblVatLowValueSubBill);
            Controls.Add(lblVatHighValueSubBill);
            Controls.Add(lblVatHighSubBill);
            Controls.Add(lblVatLowSubBill);
            Controls.Add(lblVatLowBillValue);
            Controls.Add(lblVatHighBillValue);
            Controls.Add(lblVatHighBill);
            Controls.Add(lblVatLowBill);
            Controls.Add(btnPayBill);
            Controls.Add(btnRemoveFromSubBill);
            Controls.Add(btnAddToSubBill);
            Controls.Add(listViewSubBill);
            Controls.Add(lstViewBill);
            Controls.Add(btnPaySubBill);
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
        private Button btnPaySubBill;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private ListView lstViewBill;
        private ColumnHeader Item;
        private ListView listViewSubBill;
        private ColumnHeader columnHeader1;
        private Button btnAddToSubBill;
        private Button btnRemoveFromSubBill;
        private Button btnPayBill;
        private Label lblVatLowBill;
        private Label lblVatHighBill;
        private Label lblVatHighBillValue;
        private Label lblVatLowBillValue;
        private Label lblVatLowValueSubBill;
        private Label lblVatHighValueSubBill;
        private Label lblVatHighSubBill;
        private Label lblVatLowSubBill;
    }
}