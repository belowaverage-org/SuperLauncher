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
            txtNav = new System.Windows.Forms.ComboBox();
            MiMenu = new System.Windows.Forms.MenuStrip();
            MIOpenWith = new System.Windows.Forms.ToolStripMenuItem();
            MiMenu.SuspendLayout();
            SuspendLayout();
            // 
            // txtNav
            // 
            txtNav.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtNav.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            txtNav.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            txtNav.ItemHeight = 15;
            txtNav.Location = new System.Drawing.Point(129, 6);
            txtNav.MaxDropDownItems = 30;
            txtNav.Name = "txtNav";
            txtNav.Size = new System.Drawing.Size(441, 23);
            txtNav.TabIndex = 3;
            // 
            // MiMenu
            // 
            MiMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MIOpenWith });
            MiMenu.Location = new System.Drawing.Point(0, 0);
            MiMenu.Name = "MiMenu";
            MiMenu.Size = new System.Drawing.Size(575, 24);
            MiMenu.TabIndex = 4;
            MiMenu.Text = "MiMenu";
            MiMenu.Visible = false;
            // 
            // MIOpenWith
            // 
            MIOpenWith.Name = "MIOpenWith";
            MIOpenWith.Size = new System.Drawing.Size(74, 20);
            MIOpenWith.Text = "Open with";
            // 
            // ShellView
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.WhiteSmoke;
            ClientSize = new System.Drawing.Size(575, 321);
            Controls.Add(txtNav);
            Controls.Add(MiMenu);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI Semilight", 9F);
            KeyPreview = true;
            MainMenuStrip = MiMenu;
            Name = "ShellView";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Explorer";
            FormClosing += ShellView_FormClosing;
            Load += ShellView_Load;
            KeyDown += ShellView_KeyDown;
            Resize += ShellView_Resize;
            MiMenu.ResumeLayout(false);
            MiMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ComboBox txtNav;
        private System.Windows.Forms.MenuStrip MiMenu;
        private System.Windows.Forms.ToolStripMenuItem MIOpenWith;
    }
}