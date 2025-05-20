namespace ChapeauUI
{
    partial class RestaurantOverviewForm
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
            lblTime = new Label();
            lblDate = new Label();
            lblName = new Label();
            legendPanel = new Panel();
            label3 = new Label();
            pnlLegendOccupied = new Panel();
            label2 = new Label();
            pnlLegendBooked = new Panel();
            label1 = new Label();
            pnlLegendFree = new Panel();
            btnTable1 = new Button();
            btnTable3 = new Button();
            btnTable5 = new Button();
            btnTable7 = new Button();
            btnTable9 = new Button();
            btnTable2 = new Button();
            btnTable10 = new Button();
            btnTable4 = new Button();
            btnTable8 = new Button();
            btnTable6 = new Button();
            btnLogOutNew = new Button();
            legendPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(907, 30);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(62, 32);
            lblTime.TabIndex = 5;
            lblTime.Text = "time";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(676, 30);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(61, 32);
            lblDate.TabIndex = 6;
            lblDate.Text = "date";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(405, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(74, 32);
            lblName.TabIndex = 7;
            lblName.Text = "name";
            // 
            // legendPanel
            // 
            legendPanel.Controls.Add(label3);
            legendPanel.Controls.Add(pnlLegendOccupied);
            legendPanel.Controls.Add(label2);
            legendPanel.Controls.Add(pnlLegendBooked);
            legendPanel.Controls.Add(label1);
            legendPanel.Controls.Add(pnlLegendFree);
            legendPanel.Location = new Point(18, 109);
            legendPanel.Name = "legendPanel";
            legendPanel.Size = new Size(1407, 88);
            legendPanel.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(905, 5);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(146, 37);
            label3.TabIndex = 13;
            label3.Text = "– occupied";
            // 
            // pnlLegendOccupied
            // 
            pnlLegendOccupied.BackColor = Color.Red;
            pnlLegendOccupied.Location = new Point(850, 10);
            pnlLegendOccupied.Margin = new Padding(5);
            pnlLegendOccupied.Name = "pnlLegendOccupied";
            pnlLegendOccupied.Size = new Size(32, 32);
            pnlLegendOccupied.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(642, 10);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(129, 37);
            label2.TabIndex = 12;
            label2.Text = "– booked";
            // 
            // pnlLegendBooked
            // 
            pnlLegendBooked.BackColor = Color.FromArgb(0, 0, 192);
            pnlLegendBooked.Location = new Point(588, 10);
            pnlLegendBooked.Margin = new Padding(5);
            pnlLegendBooked.Name = "pnlLegendBooked";
            pnlLegendBooked.Size = new Size(32, 32);
            pnlLegendBooked.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(418, 5);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 37);
            label1.TabIndex = 0;
            label1.Text = "-Free";
            // 
            // pnlLegendFree
            // 
            pnlLegendFree.BackColor = Color.FromArgb(0, 192, 0);
            pnlLegendFree.Location = new Point(358, 5);
            pnlLegendFree.Margin = new Padding(5);
            pnlLegendFree.Name = "pnlLegendFree";
            pnlLegendFree.Size = new Size(32, 32);
            pnlLegendFree.TabIndex = 10;
            // 
            // btnTable1
            // 
            btnTable1.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable1.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable1.ForeColor = Color.White;
            btnTable1.Location = new Point(81, 240);
            btnTable1.Name = "btnTable1";
            btnTable1.Size = new Size(150, 150);
            btnTable1.TabIndex = 0;
            btnTable1.Text = "1";
            btnTable1.UseVisualStyleBackColor = true;
            // 
            // btnTable3
            // 
            btnTable3.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable3.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable3.ForeColor = Color.White;
            btnTable3.Location = new Point(320, 240);
            btnTable3.Name = "btnTable3";
            btnTable3.Size = new Size(150, 150);
            btnTable3.TabIndex = 2;
            btnTable3.Text = "3";
            btnTable3.UseVisualStyleBackColor = true;
            // 
            // btnTable5
            // 
            btnTable5.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable5.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable5.ForeColor = Color.White;
            btnTable5.Location = new Point(572, 240);
            btnTable5.Name = "btnTable5";
            btnTable5.Size = new Size(150, 150);
            btnTable5.TabIndex = 4;
            btnTable5.Text = "5";
            btnTable5.UseVisualStyleBackColor = true;
            // 
            // btnTable7
            // 
            btnTable7.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable7.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable7.ForeColor = Color.White;
            btnTable7.Location = new Point(819, 240);
            btnTable7.Name = "btnTable7";
            btnTable7.Size = new Size(150, 150);
            btnTable7.TabIndex = 6;
            btnTable7.Text = "7";
            btnTable7.UseVisualStyleBackColor = true;
            // 
            // btnTable9
            // 
            btnTable9.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable9.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable9.ForeColor = Color.White;
            btnTable9.Location = new Point(1050, 240);
            btnTable9.Name = "btnTable9";
            btnTable9.Size = new Size(150, 150);
            btnTable9.TabIndex = 8;
            btnTable9.Text = "9";
            btnTable9.UseVisualStyleBackColor = true;
            // 
            // btnTable2
            // 
            btnTable2.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable2.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable2.ForeColor = Color.White;
            btnTable2.Location = new Point(81, 458);
            btnTable2.Name = "btnTable2";
            btnTable2.Size = new Size(150, 150);
            btnTable2.TabIndex = 1;
            btnTable2.Text = "2";
            btnTable2.UseVisualStyleBackColor = true;
            // 
            // btnTable10
            // 
            btnTable10.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable10.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable10.ForeColor = Color.White;
            btnTable10.Location = new Point(1050, 458);
            btnTable10.Name = "btnTable10";
            btnTable10.Size = new Size(150, 150);
            btnTable10.TabIndex = 9;
            btnTable10.Text = "10";
            btnTable10.UseVisualStyleBackColor = true;
            // 
            // btnTable4
            // 
            btnTable4.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable4.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable4.ForeColor = Color.White;
            btnTable4.Location = new Point(305, 458);
            btnTable4.Name = "btnTable4";
            btnTable4.Size = new Size(150, 150);
            btnTable4.TabIndex = 3;
            btnTable4.Text = "4";
            btnTable4.UseVisualStyleBackColor = true;
            // 
            // btnTable8
            // 
            btnTable8.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable8.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable8.ForeColor = Color.White;
            btnTable8.Location = new Point(798, 458);
            btnTable8.Name = "btnTable8";
            btnTable8.Size = new Size(150, 150);
            btnTable8.TabIndex = 7;
            btnTable8.Text = "8";
            btnTable8.UseVisualStyleBackColor = true;
            // 
            // btnTable6
            // 
            btnTable6.BackgroundImageLayout = ImageLayout.Stretch;
            btnTable6.Font = new Font("Segoe UI", 19.875F, FontStyle.Bold, GraphicsUnit.Point);
            btnTable6.ForeColor = Color.White;
            btnTable6.Location = new Point(572, 458);
            btnTable6.Name = "btnTable6";
            btnTable6.Size = new Size(150, 150);
            btnTable6.TabIndex = 5;
            btnTable6.Text = "6";
            btnTable6.UseVisualStyleBackColor = true;
            // 
            // btnLogOutNew
            // 
            btnLogOutNew.Location = new Point(81, 30);
            btnLogOutNew.Margin = new Padding(5);
            btnLogOutNew.Name = "btnLogOutNew";
            btnLogOutNew.Size = new Size(153, 46);
            btnLogOutNew.TabIndex = 19;
            btnLogOutNew.Text = "Log Out";
            btnLogOutNew.UseVisualStyleBackColor = true;
            btnLogOutNew.Click += btnLogOutNew_Click;
            // 
            // RestaurantOverviewForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1403, 817);
            Controls.Add(btnTable10);
            Controls.Add(btnTable9);
            Controls.Add(btnTable5);
            Controls.Add(btnTable3);
            Controls.Add(btnTable8);
            Controls.Add(btnTable1);
            Controls.Add(btnTable7);
            Controls.Add(btnTable6);
            Controls.Add(btnLogOutNew);
            Controls.Add(btnTable4);
            Controls.Add(legendPanel);
            Controls.Add(lblName);
            Controls.Add(btnTable2);
            Controls.Add(lblDate);
            Controls.Add(lblTime);
            Margin = new Padding(5);
            Name = "RestaurantOverviewForm";
            Text = "RestaurantOverviewForm";
            legendPanel.ResumeLayout(false);
            legendPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTime;
        private Label lblDate;
        private Label lblName;
        private Panel legendPanel;
        private Label label3;
        private Panel pnlLegendOccupied;
        private Label label2;
        private Panel pnlLegendBooked;
        private Label label1;
        private Panel pnlLegendFree;
        private Button btnTable1;
        private Button btnTable3;
        private Button btnTable5;
        private Button btnTable7;
        private Button btnTable9;
        private Button btnTable2;
        private Button btnTable10;
        private Button btnTable4;
        private Button btnTable8;
        private Button btnTable6;
        private Button btnLogOutNew;
    }
}