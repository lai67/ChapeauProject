namespace ChapeauUI
{
    partial class FreeTableActionsForm
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
            btnOccupy = new Button();
            lblTableNumber = new Label();
            SuspendLayout();
            // 
            // btnOccupy
            // 
            btnOccupy.Location = new Point(346, 340);
            btnOccupy.Name = "btnOccupy";
            btnOccupy.Size = new Size(150, 46);
            btnOccupy.TabIndex = 0;
            btnOccupy.Text = "Occupy";
            btnOccupy.UseVisualStyleBackColor = true;
            // 
            // lblTableNumber
            // 
            lblTableNumber.AutoSize = true;
            lblTableNumber.Location = new Point(383, 171);
            lblTableNumber.Name = "lblTableNumber";
            lblTableNumber.Size = new Size(59, 32);
            lblTableNumber.TabIndex = 1;
            lblTableNumber.Text = ".........";
            // 
            // FreeTableActionsForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(855, 484);
            Controls.Add(lblTableNumber);
            Controls.Add(btnOccupy);
            Name = "FreeTableActionsForm";
            Text = "FreeTableActionsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOccupy;
        private Label lblTableNumber;
    }
}