namespace ChapeauUI
{
    partial class OccupiedTableActionsForm
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
            btnFree = new Button();
            btnTakeOrder = new Button();
            lblTableNumber = new Label();
            SuspendLayout();
            // 
            // btnFree
            // 
            btnFree.Location = new Point(186, 309);
            btnFree.Name = "btnFree";
            btnFree.Size = new Size(150, 46);
            btnFree.TabIndex = 0;
            btnFree.Text = "Free Table";
            btnFree.UseVisualStyleBackColor = true;
            // 
            // btnTakeOrder
            // 
            btnTakeOrder.Location = new Point(457, 309);
            btnTakeOrder.Name = "btnTakeOrder";
            btnTakeOrder.Size = new Size(150, 46);
            btnTakeOrder.TabIndex = 1;
            btnTakeOrder.Text = "Take Order";
            btnTakeOrder.UseVisualStyleBackColor = true;
            // 
            // lblTableNumber
            // 
            lblTableNumber.AutoSize = true;
            lblTableNumber.Location = new Point(376, 108);
            lblTableNumber.Name = "lblTableNumber";
            lblTableNumber.Size = new Size(54, 32);
            lblTableNumber.TabIndex = 2;
            lblTableNumber.Text = "........";
            // 
            // OccupiedTableActionsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTableNumber);
            Controls.Add(btnTakeOrder);
            Controls.Add(btnFree);
            Name = "OccupiedTableActionsForm";
            Text = "OccupiedTableActionsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnFree;
        private Button btnTakeOrder;
        private Label lblTableNumber;
    }
}