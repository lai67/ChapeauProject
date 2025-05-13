namespace ChapeauUI
{
    partial class table
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableInstance = new Button();
            SuspendLayout();
            // 
            // tableInstance
            // 
            tableInstance.Location = new Point(3, 3);
            tableInstance.Name = "tableInstance";
            tableInstance.Size = new Size(219, 188);
            tableInstance.TabIndex = 0;
            tableInstance.Text = "Table ... ";
            tableInstance.UseVisualStyleBackColor = true;
            // 
            // table
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableInstance);
            Name = "table";
            Size = new Size(226, 191);
            ResumeLayout(false);
        }

        #endregion

        private Button tableInstance;
    }
}
