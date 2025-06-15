namespace ChapeauUI
{
    partial class PaymentFormSubBill
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
            lblTotalPerPerson = new Label();
            btnSplitIncrement = new Button();
            btnSplitDecrement = new Button();
            lblSplitValue = new Label();
            lblSplitEqually = new Label();
            lblTotalPriceValueBill = new Label();
            lblTotalPriceSubBill = new Label();
            btnFinalizePayment = new Button();
            lblTipPct0 = new Label();
            lblTipPct7 = new Label();
            lblTipPct5 = new Label();
            lblTipPct10 = new Label();
            lblTipPct12 = new Label();
            lblTipPct25 = new Label();
            lblTipPct20 = new Label();
            lblTipPct15 = new Label();
            lblTipPct2 = new Label();
            rdBtnTipPct2 = new RadioButton();
            rdBtnTipPct5 = new RadioButton();
            rdBtnTipPct7 = new RadioButton();
            rdBtnTipPct10 = new RadioButton();
            rdBtnTipPct12 = new RadioButton();
            rdBtnTipPct15 = new RadioButton();
            rdBtnTipPct20 = new RadioButton();
            rdBtnTipPct25 = new RadioButton();
            rdBtnTipPct0 = new RadioButton();
            richTextBoxFeedback = new RichTextBox();
            lblFeedbackFromCustomer = new Label();
            lblTipPercent = new Label();
            rdBtnCash = new RadioButton();
            rdBtnCard = new RadioButton();
            SuspendLayout();
            // 
            // lblTotalPerPerson
            // 
            lblTotalPerPerson.AutoSize = true;
            lblTotalPerPerson.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalPerPerson.Location = new Point(165, 128);
            lblTotalPerPerson.Name = "lblTotalPerPerson";
            lblTotalPerPerson.Size = new Size(180, 28);
            lblTotalPerPerson.TabIndex = 133;
            lblTotalPerPerson.Text = "Total Per Person: €0";
            // 
            // btnSplitIncrement
            // 
            btnSplitIncrement.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSplitIncrement.Location = new Point(331, 71);
            btnSplitIncrement.Name = "btnSplitIncrement";
            btnSplitIncrement.Size = new Size(100, 36);
            btnSplitIncrement.TabIndex = 132;
            btnSplitIncrement.Text = "+";
            btnSplitIncrement.UseVisualStyleBackColor = true;
            btnSplitIncrement.Click += btnSplitIncrement_Click;
            // 
            // btnSplitDecrement
            // 
            btnSplitDecrement.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSplitDecrement.Location = new Point(92, 71);
            btnSplitDecrement.Name = "btnSplitDecrement";
            btnSplitDecrement.Size = new Size(100, 36);
            btnSplitDecrement.TabIndex = 131;
            btnSplitDecrement.Text = " - ";
            btnSplitDecrement.UseVisualStyleBackColor = true;
            btnSplitDecrement.Click += btnSplitDecrement_Click;
            // 
            // lblSplitValue
            // 
            lblSplitValue.AutoSize = true;
            lblSplitValue.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblSplitValue.Location = new Point(242, 67);
            lblSplitValue.Name = "lblSplitValue";
            lblSplitValue.Size = new Size(33, 38);
            lblSplitValue.TabIndex = 130;
            lblSplitValue.Text = "1";
            // 
            // lblSplitEqually
            // 
            lblSplitEqually.AutoSize = true;
            lblSplitEqually.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSplitEqually.Location = new Point(214, 49);
            lblSplitEqually.Name = "lblSplitEqually";
            lblSplitEqually.Size = new Size(94, 20);
            lblSplitEqually.TabIndex = 129;
            lblSplitEqually.Text = "Split equally:";
            // 
            // lblTotalPriceValueBill
            // 
            lblTotalPriceValueBill.AutoSize = true;
            lblTotalPriceValueBill.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalPriceValueBill.Location = new Point(285, 133);
            lblTotalPriceValueBill.Name = "lblTotalPriceValueBill";
            lblTotalPriceValueBill.Size = new Size(0, 23);
            lblTotalPriceValueBill.TabIndex = 128;
            // 
            // lblTotalPriceSubBill
            // 
            lblTotalPriceSubBill.AutoSize = true;
            lblTotalPriceSubBill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalPriceSubBill.Location = new Point(196, 17);
            lblTotalPriceSubBill.Name = "lblTotalPriceSubBill";
            lblTotalPriceSubBill.Size = new Size(132, 28);
            lblTotalPriceSubBill.TabIndex = 127;
            lblTotalPriceSubBill.Text = "Total Price: €0";
            // 
            // btnFinalizePayment
            // 
            btnFinalizePayment.BackColor = SystemColors.ButtonShadow;
            btnFinalizePayment.Location = new Point(90, 585);
            btnFinalizePayment.Name = "btnFinalizePayment";
            btnFinalizePayment.Size = new Size(339, 49);
            btnFinalizePayment.TabIndex = 126;
            btnFinalizePayment.Text = "Finalize Payment";
            btnFinalizePayment.UseVisualStyleBackColor = false;
            btnFinalizePayment.Click += btnFinalizePayment_Click_1;
            // 
            // lblTipPct0
            // 
            lblTipPct0.AutoSize = true;
            lblTipPct0.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct0.Location = new Point(97, 193);
            lblTipPct0.Name = "lblTipPct0";
            lblTipPct0.Size = new Size(19, 23);
            lblTipPct0.TabIndex = 125;
            lblTipPct0.Text = "0";
            // 
            // lblTipPct7
            // 
            lblTipPct7.AutoSize = true;
            lblTipPct7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct7.Location = new Point(212, 193);
            lblTipPct7.Name = "lblTipPct7";
            lblTipPct7.Size = new Size(19, 23);
            lblTipPct7.TabIndex = 124;
            lblTipPct7.Text = "7";
            // 
            // lblTipPct5
            // 
            lblTipPct5.AutoSize = true;
            lblTipPct5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct5.Location = new Point(174, 193);
            lblTipPct5.Name = "lblTipPct5";
            lblTipPct5.Size = new Size(19, 23);
            lblTipPct5.TabIndex = 123;
            lblTipPct5.Text = "5";
            // 
            // lblTipPct10
            // 
            lblTipPct10.AutoSize = true;
            lblTipPct10.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct10.Location = new Point(247, 193);
            lblTipPct10.Name = "lblTipPct10";
            lblTipPct10.Size = new Size(28, 23);
            lblTipPct10.TabIndex = 122;
            lblTipPct10.Text = "10";
            // 
            // lblTipPct12
            // 
            lblTipPct12.AutoSize = true;
            lblTipPct12.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct12.Location = new Point(285, 193);
            lblTipPct12.Name = "lblTipPct12";
            lblTipPct12.Size = new Size(28, 23);
            lblTipPct12.TabIndex = 121;
            lblTipPct12.Text = "12";
            // 
            // lblTipPct25
            // 
            lblTipPct25.AutoSize = true;
            lblTipPct25.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct25.Location = new Point(401, 193);
            lblTipPct25.Name = "lblTipPct25";
            lblTipPct25.Size = new Size(28, 23);
            lblTipPct25.TabIndex = 120;
            lblTipPct25.Text = "25";
            // 
            // lblTipPct20
            // 
            lblTipPct20.AutoSize = true;
            lblTipPct20.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct20.Location = new Point(361, 193);
            lblTipPct20.Name = "lblTipPct20";
            lblTipPct20.Size = new Size(28, 23);
            lblTipPct20.TabIndex = 119;
            lblTipPct20.Text = "20";
            // 
            // lblTipPct15
            // 
            lblTipPct15.AutoSize = true;
            lblTipPct15.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct15.Location = new Point(322, 193);
            lblTipPct15.Name = "lblTipPct15";
            lblTipPct15.Size = new Size(28, 23);
            lblTipPct15.TabIndex = 118;
            lblTipPct15.Text = "15";
            // 
            // lblTipPct2
            // 
            lblTipPct2.AutoSize = true;
            lblTipPct2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPct2.Location = new Point(134, 193);
            lblTipPct2.Name = "lblTipPct2";
            lblTipPct2.Size = new Size(19, 23);
            lblTipPct2.TabIndex = 117;
            lblTipPct2.Text = "2";
            // 
            // rdBtnTipPct2
            // 
            rdBtnTipPct2.AutoSize = true;
            rdBtnTipPct2.Location = new Point(135, 219);
            rdBtnTipPct2.Name = "rdBtnTipPct2";
            rdBtnTipPct2.Size = new Size(17, 16);
            rdBtnTipPct2.TabIndex = 116;
            rdBtnTipPct2.TabStop = true;
            rdBtnTipPct2.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct5
            // 
            rdBtnTipPct5.AutoSize = true;
            rdBtnTipPct5.Location = new Point(175, 219);
            rdBtnTipPct5.Name = "rdBtnTipPct5";
            rdBtnTipPct5.Size = new Size(17, 16);
            rdBtnTipPct5.TabIndex = 115;
            rdBtnTipPct5.TabStop = true;
            rdBtnTipPct5.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct7
            // 
            rdBtnTipPct7.AutoSize = true;
            rdBtnTipPct7.Location = new Point(213, 219);
            rdBtnTipPct7.Name = "rdBtnTipPct7";
            rdBtnTipPct7.Size = new Size(17, 16);
            rdBtnTipPct7.TabIndex = 114;
            rdBtnTipPct7.TabStop = true;
            rdBtnTipPct7.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct10
            // 
            rdBtnTipPct10.AutoSize = true;
            rdBtnTipPct10.Location = new Point(253, 219);
            rdBtnTipPct10.Name = "rdBtnTipPct10";
            rdBtnTipPct10.Size = new Size(17, 16);
            rdBtnTipPct10.TabIndex = 113;
            rdBtnTipPct10.TabStop = true;
            rdBtnTipPct10.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct12
            // 
            rdBtnTipPct12.AutoSize = true;
            rdBtnTipPct12.Location = new Point(292, 219);
            rdBtnTipPct12.Name = "rdBtnTipPct12";
            rdBtnTipPct12.Size = new Size(17, 16);
            rdBtnTipPct12.TabIndex = 112;
            rdBtnTipPct12.TabStop = true;
            rdBtnTipPct12.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct15
            // 
            rdBtnTipPct15.AutoSize = true;
            rdBtnTipPct15.Location = new Point(328, 219);
            rdBtnTipPct15.Name = "rdBtnTipPct15";
            rdBtnTipPct15.Size = new Size(17, 16);
            rdBtnTipPct15.TabIndex = 111;
            rdBtnTipPct15.TabStop = true;
            rdBtnTipPct15.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct20
            // 
            rdBtnTipPct20.AutoSize = true;
            rdBtnTipPct20.Location = new Point(366, 219);
            rdBtnTipPct20.Name = "rdBtnTipPct20";
            rdBtnTipPct20.Size = new Size(17, 16);
            rdBtnTipPct20.TabIndex = 110;
            rdBtnTipPct20.TabStop = true;
            rdBtnTipPct20.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct25
            // 
            rdBtnTipPct25.AutoSize = true;
            rdBtnTipPct25.Location = new Point(406, 219);
            rdBtnTipPct25.Name = "rdBtnTipPct25";
            rdBtnTipPct25.Size = new Size(17, 16);
            rdBtnTipPct25.TabIndex = 109;
            rdBtnTipPct25.TabStop = true;
            rdBtnTipPct25.UseVisualStyleBackColor = true;
            // 
            // rdBtnTipPct0
            // 
            rdBtnTipPct0.AutoSize = true;
            rdBtnTipPct0.Location = new Point(98, 219);
            rdBtnTipPct0.Name = "rdBtnTipPct0";
            rdBtnTipPct0.Size = new Size(17, 16);
            rdBtnTipPct0.TabIndex = 108;
            rdBtnTipPct0.TabStop = true;
            rdBtnTipPct0.UseVisualStyleBackColor = true;
            // 
            // richTextBoxFeedback
            // 
            richTextBoxFeedback.ForeColor = SystemColors.WindowFrame;
            richTextBoxFeedback.Location = new Point(90, 264);
            richTextBoxFeedback.Name = "richTextBoxFeedback";
            richTextBoxFeedback.Size = new Size(339, 234);
            richTextBoxFeedback.TabIndex = 107;
            richTextBoxFeedback.Text = "placeholder";
            // 
            // lblFeedbackFromCustomer
            // 
            lblFeedbackFromCustomer.AutoSize = true;
            lblFeedbackFromCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblFeedbackFromCustomer.Location = new Point(90, 238);
            lblFeedbackFromCustomer.Name = "lblFeedbackFromCustomer";
            lblFeedbackFromCustomer.Size = new Size(202, 23);
            lblFeedbackFromCustomer.TabIndex = 106;
            lblFeedbackFromCustomer.Text = "Feedback from customer:";
            // 
            // lblTipPercent
            // 
            lblTipPercent.AutoSize = true;
            lblTipPercent.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipPercent.Location = new Point(90, 166);
            lblTipPercent.Name = "lblTipPercent";
            lblTipPercent.Size = new Size(100, 23);
            lblTipPercent.TabIndex = 105;
            lblTipPercent.Text = "Tip percent:";
            // 
            // rdBtnCash
            // 
            rdBtnCash.BackColor = SystemColors.ButtonShadow;
            rdBtnCash.Location = new Point(271, 512);
            rdBtnCash.Name = "rdBtnCash";
            rdBtnCash.Size = new Size(158, 51);
            rdBtnCash.TabIndex = 104;
            rdBtnCash.TabStop = true;
            rdBtnCash.Text = "Cash";
            rdBtnCash.TextAlign = ContentAlignment.MiddleCenter;
            rdBtnCash.UseVisualStyleBackColor = false;
            // 
            // rdBtnCard
            // 
            rdBtnCard.BackColor = SystemColors.ButtonShadow;
            rdBtnCard.Location = new Point(90, 512);
            rdBtnCard.Name = "rdBtnCard";
            rdBtnCard.Size = new Size(158, 51);
            rdBtnCard.TabIndex = 103;
            rdBtnCard.TabStop = true;
            rdBtnCard.Text = "Card";
            rdBtnCard.TextAlign = ContentAlignment.MiddleCenter;
            rdBtnCard.UseVisualStyleBackColor = false;
            // 
            // PaymentFormSubBill
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 651);
            Controls.Add(lblTotalPerPerson);
            Controls.Add(btnSplitIncrement);
            Controls.Add(btnSplitDecrement);
            Controls.Add(lblSplitValue);
            Controls.Add(lblSplitEqually);
            Controls.Add(lblTotalPriceValueBill);
            Controls.Add(lblTotalPriceSubBill);
            Controls.Add(btnFinalizePayment);
            Controls.Add(lblTipPct0);
            Controls.Add(lblTipPct7);
            Controls.Add(lblTipPct5);
            Controls.Add(lblTipPct10);
            Controls.Add(lblTipPct12);
            Controls.Add(lblTipPct25);
            Controls.Add(lblTipPct20);
            Controls.Add(lblTipPct15);
            Controls.Add(lblTipPct2);
            Controls.Add(rdBtnTipPct2);
            Controls.Add(rdBtnTipPct5);
            Controls.Add(rdBtnTipPct7);
            Controls.Add(rdBtnTipPct10);
            Controls.Add(rdBtnTipPct12);
            Controls.Add(rdBtnTipPct15);
            Controls.Add(rdBtnTipPct20);
            Controls.Add(rdBtnTipPct25);
            Controls.Add(rdBtnTipPct0);
            Controls.Add(richTextBoxFeedback);
            Controls.Add(lblFeedbackFromCustomer);
            Controls.Add(lblTipPercent);
            Controls.Add(rdBtnCash);
            Controls.Add(rdBtnCard);
            Name = "PaymentFormSubBill";
            Text = "PaymentFormSubBill";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTotalPerPerson;
        private Button btnSplitIncrement;
        private Button btnSplitDecrement;
        private Label lblSplitValue;
        private Label lblSplitEqually;
        private Label lblTotalPriceValueBill;
        private Label lblTotalPriceSubBill;
        private Button btnFinalizePayment;
        private Label lblTipPct0;
        private Label lblTipPct7;
        private Label lblTipPct5;
        private Label lblTipPct10;
        private Label lblTipPct12;
        private Label lblTipPct25;
        private Label lblTipPct20;
        private Label lblTipPct15;
        private Label lblTipPct2;
        private RadioButton rdBtnTipPct2;
        private RadioButton rdBtnTipPct5;
        private RadioButton rdBtnTipPct7;
        private RadioButton rdBtnTipPct10;
        private RadioButton rdBtnTipPct12;
        private RadioButton rdBtnTipPct15;
        private RadioButton rdBtnTipPct20;
        private RadioButton rdBtnTipPct25;
        private RadioButton rdBtnTipPct0;
        private RichTextBox richTextBoxFeedback;
        private Label lblFeedbackFromCustomer;
        private Label lblTipPercent;
        private RadioButton rdBtnCash;
        private RadioButton rdBtnCard;
    }
}