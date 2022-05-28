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
            this.Browser = new Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser();
            this.panelBorderCover = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtNav
            // 
            this.txtNav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNav.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtNav.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.txtNav.ItemHeight = 15;
            this.txtNav.Location = new System.Drawing.Point(98, 4);
            this.txtNav.MaxDropDownItems = 30;
            this.txtNav.Name = "txtNav";
            this.txtNav.Size = new System.Drawing.Size(472, 23);
            this.txtNav.TabIndex = 3;
            // 
            // Browser
            // 
            this.Browser.AllowDrop = true;
            this.Browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Browser.Location = new System.Drawing.Point(-1, 32);
            this.Browser.Name = "Browser";
            this.Browser.PropertyBagName = "Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser";
            this.Browser.Size = new System.Drawing.Size(577, 290);
            this.Browser.TabIndex = 5;
            this.Browser.NavigationComplete += new System.EventHandler<Microsoft.WindowsAPICodePack.Controls.NavigationCompleteEventArgs>(this.Browser_NavigationComplete);
            // 
            // panelBorderCover
            // 
            this.panelBorderCover.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBorderCover.Location = new System.Drawing.Point(0, 31);
            this.panelBorderCover.Name = "panelBorderCover";
            this.panelBorderCover.Size = new System.Drawing.Size(575, 3);
            this.panelBorderCover.TabIndex = 7;
            // 
            // ShellView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(575, 321);
            this.Controls.Add(this.panelBorderCover);
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.txtNav);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.Name = "ShellView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShellView_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox txtNav;
        private Microsoft.WindowsAPICodePack.Controls.WindowsForms.ExplorerBrowser Browser;
        private System.Windows.Forms.Panel panelBorderCover;
    }
}