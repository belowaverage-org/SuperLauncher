namespace SuperLauncher
{
    partial class Launcher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.IconsBox = new System.Windows.Forms.ListView();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.ShieldIcon = new System.Windows.Forms.PictureBox();
            this.UserLabel = new System.Windows.Forms.Label();
            this.SuperIcon = new System.Windows.Forms.PictureBox();
            this.AppLabel = new System.Windows.Forms.Label();
            this.RightClickMenu = new System.Windows.Forms.ContextMenu();
            this.miRemove = new System.Windows.Forms.MenuItem();
            this.miSuperLauncher = new System.Windows.Forms.MenuItem();
            this.miSeperator1 = new System.Windows.Forms.MenuItem();
            this.miElevate = new System.Windows.Forms.MenuItem();
            this.miRunAs = new System.Windows.Forms.MenuItem();
            this.miSeperator2 = new System.Windows.Forms.MenuItem();
            this.miAddShortcut = new System.Windows.Forms.MenuItem();
            this.miSeperator3 = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.TrayMenu = new System.Windows.Forms.ContextMenu();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShieldIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipTitle = "Super Launcher";
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Super Launcher";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // IconsBox
            // 
            this.IconsBox.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.IconsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IconsBox.BackColor = System.Drawing.Color.Black;
            this.IconsBox.BackgroundImageTiled = true;
            this.IconsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IconsBox.ForeColor = System.Drawing.Color.White;
            this.IconsBox.HideSelection = false;
            this.IconsBox.Location = new System.Drawing.Point(0, 29);
            this.IconsBox.Margin = new System.Windows.Forms.Padding(10);
            this.IconsBox.MultiSelect = false;
            this.IconsBox.Name = "IconsBox";
            this.IconsBox.ShowGroups = false;
            this.IconsBox.Size = new System.Drawing.Size(376, 191);
            this.IconsBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.IconsBox.TabIndex = 1;
            this.IconsBox.TileSize = new System.Drawing.Size(50, 50);
            this.IconsBox.UseCompatibleStateImageBehavior = false;
            this.IconsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IconsBox_KeyPress);
            this.IconsBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IconsBox_MouseClick);
            this.IconsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IconsBox_MouseDoubleClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem1.Text = "Exit Super Launcher";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DereferenceLinks = false;
            this.OpenFileDialog.Multiselect = true;
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.White;
            this.TopPanel.Controls.Add(this.ShieldIcon);
            this.TopPanel.Controls.Add(this.UserLabel);
            this.TopPanel.Controls.Add(this.SuperIcon);
            this.TopPanel.Controls.Add(this.AppLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(376, 20);
            this.TopPanel.TabIndex = 2;
            // 
            // ShieldIcon
            // 
            this.ShieldIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShieldIcon.Image = ((System.Drawing.Image)(resources.GetObject("ShieldIcon.Image")));
            this.ShieldIcon.Location = new System.Drawing.Point(357, 0);
            this.ShieldIcon.Name = "ShieldIcon";
            this.ShieldIcon.Size = new System.Drawing.Size(13, 13);
            this.ShieldIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ShieldIcon.TabIndex = 3;
            this.ShieldIcon.TabStop = false;
            this.ShieldIcon.Visible = false;
            // 
            // UserLabel
            // 
            this.UserLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserLabel.AutoEllipsis = true;
            this.UserLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.Location = new System.Drawing.Point(115, -2);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(255, 22);
            this.UserLabel.TabIndex = 2;
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SuperIcon
            // 
            this.SuperIcon.Image = ((System.Drawing.Image)(resources.GetObject("SuperIcon.Image")));
            this.SuperIcon.Location = new System.Drawing.Point(7, 0);
            this.SuperIcon.Name = "SuperIcon";
            this.SuperIcon.Size = new System.Drawing.Size(13, 13);
            this.SuperIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SuperIcon.TabIndex = 1;
            this.SuperIcon.TabStop = false;
            // 
            // AppLabel
            // 
            this.AppLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppLabel.Location = new System.Drawing.Point(25, -2);
            this.AppLabel.Name = "AppLabel";
            this.AppLabel.Size = new System.Drawing.Size(90, 22);
            this.AppLabel.TabIndex = 0;
            this.AppLabel.Text = "Super Launcher";
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miRemove});
            // 
            // miRemove
            // 
            this.miRemove.Index = 0;
            this.miRemove.Text = "Remove";
            this.miRemove.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // miSuperLauncher
            // 
            this.miSuperLauncher.Index = 0;
            this.miSuperLauncher.Text = "Super Launcher";
            this.miSuperLauncher.Click += new System.EventHandler(this.miSuperLauncher_Click);
            // 
            // miSeperator1
            // 
            this.miSeperator1.Index = 1;
            this.miSeperator1.Text = "-";
            // 
            // miElevate
            // 
            this.miElevate.Index = 2;
            this.miElevate.Text = "Elevate...";
            this.miElevate.Click += new System.EventHandler(this.miElevate_Click);
            // 
            // miRunAs
            // 
            this.miRunAs.Index = 3;
            this.miRunAs.Text = "Run as...";
            this.miRunAs.Click += new System.EventHandler(this.miRunAs_Click);
            // 
            // miSeperator2
            // 
            this.miSeperator2.Index = 4;
            this.miSeperator2.Text = "-";
            // 
            // miAddShortcut
            // 
            this.miAddShortcut.Index = 5;
            this.miAddShortcut.Text = "Add shortcut...";
            this.miAddShortcut.Click += new System.EventHandler(this.miAddShortcut_Click);
            // 
            // miSeperator3
            // 
            this.miSeperator3.Index = 6;
            this.miSeperator3.Text = "-";
            // 
            // miExit
            // 
            this.miExit.Index = 7;
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // TrayMenu
            // 
            this.TrayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miSuperLauncher,
            this.miSeperator1,
            this.miElevate,
            this.miRunAs,
            this.miSeperator2,
            this.miAddShortcut,
            this.miSeperator3,
            this.miExit});
            this.TrayMenu.Popup += new System.EventHandler(this.TrayMenu_Popup);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(376, 220);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.IconsBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(148, 200);
            this.Name = "Launcher";
            this.Opacity = 0.96D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Launcher_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            this.Resize += new System.EventHandler(this.Launcher_Resize);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShieldIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ListView IconsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label AppLabel;
        private System.Windows.Forms.PictureBox SuperIcon;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.PictureBox ShieldIcon;
        private System.Windows.Forms.ContextMenu RightClickMenu;
        private System.Windows.Forms.MenuItem miRemove;
        private System.Windows.Forms.MenuItem miSuperLauncher;
        private System.Windows.Forms.MenuItem miSeperator1;
        private System.Windows.Forms.MenuItem miElevate;
        private System.Windows.Forms.MenuItem miRunAs;
        private System.Windows.Forms.MenuItem miSeperator2;
        private System.Windows.Forms.MenuItem miAddShortcut;
        private System.Windows.Forms.MenuItem miSeperator3;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.ContextMenu TrayMenu;
    }
}

