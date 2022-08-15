namespace SuperLauncher
{
    partial class ShellView
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
            this.txtNav = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtNav
            // 
            this.txtNav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNav.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtNav.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.txtNav.ItemHeight = 15;
            this.txtNav.Location = new System.Drawing.Point(99, 5);
            this.txtNav.MaxDropDownItems = 30;
            this.txtNav.Name = "txtNav";
            this.txtNav.Size = new System.Drawing.Size(472, 23);
            this.txtNav.TabIndex = 3;
            // 
            // ShellView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(575, 321);
            this.Controls.Add(this.txtNav);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.Name = "ShellView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorer";
            this.Load += new System.EventHandler(this.ShellView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShellView_KeyDown);
            this.Resize += new System.EventHandler(this.ShellView_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox txtNav;
    }
}