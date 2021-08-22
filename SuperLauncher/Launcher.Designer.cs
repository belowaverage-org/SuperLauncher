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
            this.TrayContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TcMiSuperLauncher = new System.Windows.Forms.ToolStripMenuItem();
            this.TcSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TcMiElevate = new System.Windows.Forms.ToolStripMenuItem();
            this.TcMiRunAs = new System.Windows.Forms.ToolStripMenuItem();
            this.TcSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TcMiAddShortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.TcSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TcMiOpenExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.TcSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TcMiViewConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.TcMiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.TcSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TcMiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.IconsBox = new System.Windows.Forms.ListView();
            this.RemoveItemContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RicMiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.ShieldIcon = new System.Windows.Forms.PictureBox();
            this.UserLabel = new System.Windows.Forms.Label();
            this.SuperIcon = new System.Windows.Forms.PictureBox();
            this.AppLabel = new System.Windows.Forms.Label();
            this.TrayContext.SuspendLayout();
            this.RemoveItemContext.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShieldIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipTitle = "Super Launcher";
            this.TrayIcon.ContextMenuStrip = this.TrayContext;
            this.TrayIcon.Text = "Super Launcher";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // TrayContext
            // 
            this.TrayContext.DropShadowEnabled = false;
            this.TrayContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TcMiSuperLauncher,
            this.TcSeparator1,
            this.TcMiElevate,
            this.TcMiRunAs,
            this.TcSeparator2,
            this.TcMiAddShortcut,
            this.TcSeparator3,
            this.TcMiOpenExplorer,
            this.TcSeparator4,
            this.TcMiViewConfig,
            this.TcMiSettings,
            this.TcSeparator5,
            this.TcMiExit});
            this.TrayContext.Name = "contextMenuStrip1";
            this.TrayContext.Size = new System.Drawing.Size(157, 210);
            this.TrayContext.Text = "TrayContext";
            // 
            // TcMiSuperLauncher
            // 
            this.TcMiSuperLauncher.Image = global::SuperLauncher.Resources.logo_16;
            this.TcMiSuperLauncher.Name = "TcMiSuperLauncher";
            this.TcMiSuperLauncher.Size = new System.Drawing.Size(156, 22);
            this.TcMiSuperLauncher.Text = "Super Launcher";
            // 
            // TcSeparator1
            // 
            this.TcSeparator1.Name = "TcSeparator1";
            this.TcSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // TcMiElevate
            // 
            this.TcMiElevate.Image = global::SuperLauncher.Resources.shield;
            this.TcMiElevate.Name = "TcMiElevate";
            this.TcMiElevate.Size = new System.Drawing.Size(156, 22);
            this.TcMiElevate.Text = "Elevate...";
            // 
            // TcMiRunAs
            // 
            this.TcMiRunAs.Name = "TcMiRunAs";
            this.TcMiRunAs.Size = new System.Drawing.Size(156, 22);
            this.TcMiRunAs.Text = "Run as...";
            // 
            // TcSeparator2
            // 
            this.TcSeparator2.Name = "TcSeparator2";
            this.TcSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // TcMiAddShortcut
            // 
            this.TcMiAddShortcut.Image = global::SuperLauncher.Resources.shortcut;
            this.TcMiAddShortcut.Name = "TcMiAddShortcut";
            this.TcMiAddShortcut.Size = new System.Drawing.Size(156, 22);
            this.TcMiAddShortcut.Text = "Add shortcut...";
            // 
            // TcSeparator3
            // 
            this.TcSeparator3.Name = "TcSeparator3";
            this.TcSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // TcMiOpenExplorer
            // 
            this.TcMiOpenExplorer.Image = global::SuperLauncher.Resources.explorer;
            this.TcMiOpenExplorer.Name = "TcMiOpenExplorer";
            this.TcMiOpenExplorer.Size = new System.Drawing.Size(156, 22);
            this.TcMiOpenExplorer.Text = "Open Explorer";
            // 
            // TcSeparator4
            // 
            this.TcSeparator4.Name = "TcSeparator4";
            this.TcSeparator4.Size = new System.Drawing.Size(153, 6);
            // 
            // TcMiViewConfig
            // 
            this.TcMiViewConfig.Name = "TcMiViewConfig";
            this.TcMiViewConfig.Size = new System.Drawing.Size(156, 22);
            this.TcMiViewConfig.Text = "View Config...";
            // 
            // TcMiSettings
            // 
            this.TcMiSettings.Name = "TcMiSettings";
            this.TcMiSettings.Size = new System.Drawing.Size(156, 22);
            this.TcMiSettings.Text = "Settings...";
            // 
            // TcSeparator5
            // 
            this.TcSeparator5.Name = "TcSeparator5";
            this.TcSeparator5.Size = new System.Drawing.Size(153, 6);
            // 
            // TcMiExit
            // 
            this.TcMiExit.Name = "TcMiExit";
            this.TcMiExit.Size = new System.Drawing.Size(156, 22);
            this.TcMiExit.Text = "Exit";
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
            this.IconsBox.ContextMenuStrip = this.RemoveItemContext;
            this.IconsBox.ForeColor = System.Drawing.Color.White;
            this.IconsBox.HideSelection = false;
            this.IconsBox.Location = new System.Drawing.Point(1, 35);
            this.IconsBox.Margin = new System.Windows.Forms.Padding(10);
            this.IconsBox.MultiSelect = false;
            this.IconsBox.Name = "IconsBox";
            this.IconsBox.ShowGroups = false;
            this.IconsBox.Size = new System.Drawing.Size(374, 184);
            this.IconsBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.IconsBox.TabIndex = 1;
            this.IconsBox.TileSize = new System.Drawing.Size(50, 50);
            this.IconsBox.UseCompatibleStateImageBehavior = false;
            this.IconsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IconsBox_KeyPress);
            this.IconsBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IconsBox_MouseClick);
            this.IconsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IconsBox_MouseDoubleClick);
            // 
            // RemoveItemContext
            // 
            this.RemoveItemContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RicMiRemove});
            this.RemoveItemContext.Name = "RemoveItemContext";
            this.RemoveItemContext.Size = new System.Drawing.Size(118, 26);
            // 
            // RicMiRemove
            // 
            this.RicMiRemove.Name = "RicMiRemove";
            this.RicMiRemove.Size = new System.Drawing.Size(117, 22);
            this.RicMiRemove.Text = "Remove";
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenu.Text = "Exit Super Launcher";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DereferenceLinks = false;
            this.OpenFileDialog.Multiselect = true;
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // TopPanel
            // 
            this.TopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopPanel.BackColor = System.Drawing.Color.White;
            this.TopPanel.Controls.Add(this.ShieldIcon);
            this.TopPanel.Controls.Add(this.UserLabel);
            this.TopPanel.Controls.Add(this.SuperIcon);
            this.TopPanel.Controls.Add(this.AppLabel);
            this.TopPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TopPanel.Location = new System.Drawing.Point(1, 1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(374, 26);
            this.TopPanel.TabIndex = 2;
            // 
            // ShieldIcon
            // 
            this.ShieldIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShieldIcon.Image = ((System.Drawing.Image)(resources.GetObject("ShieldIcon.Image")));
            this.ShieldIcon.Location = new System.Drawing.Point(353, 5);
            this.ShieldIcon.Name = "ShieldIcon";
            this.ShieldIcon.Size = new System.Drawing.Size(16, 16);
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
            this.UserLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserLabel.Location = new System.Drawing.Point(115, 4);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(255, 15);
            this.UserLabel.TabIndex = 2;
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SuperIcon
            // 
            this.SuperIcon.Image = global::SuperLauncher.Resources.logo_16;
            this.SuperIcon.Location = new System.Drawing.Point(6, 5);
            this.SuperIcon.Name = "SuperIcon";
            this.SuperIcon.Size = new System.Drawing.Size(16, 16);
            this.SuperIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SuperIcon.TabIndex = 1;
            this.SuperIcon.TabStop = false;
            // 
            // AppLabel
            // 
            this.AppLabel.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AppLabel.Location = new System.Drawing.Point(26, 4);
            this.AppLabel.Name = "AppLabel";
            this.AppLabel.Size = new System.Drawing.Size(90, 15);
            this.AppLabel.TabIndex = 0;
            this.AppLabel.Text = "Super Launcher";
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
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(148, 200);
            this.Name = "Launcher";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Launcher_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Launcher_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Launcher_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Launcher_MouseUp);
            this.Resize += new System.EventHandler(this.Launcher_Resize);
            this.TrayContext.ResumeLayout(false);
            this.RemoveItemContext.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShieldIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SuperIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ListView IconsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label AppLabel;
        private System.Windows.Forms.PictureBox SuperIcon;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.PictureBox ShieldIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContext;
        private System.Windows.Forms.ToolStripMenuItem TcMiSuperLauncher;
        private System.Windows.Forms.ToolStripSeparator TcSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TcMiElevate;
        private System.Windows.Forms.ToolStripMenuItem TcMiRunAs;
        private System.Windows.Forms.ToolStripSeparator TcSeparator2;
        private System.Windows.Forms.ToolStripMenuItem TcMiAddShortcut;
        private System.Windows.Forms.ToolStripSeparator TcSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TcMiOpenExplorer;
        private System.Windows.Forms.ToolStripSeparator TcSeparator4;
        private System.Windows.Forms.ToolStripMenuItem TcMiViewConfig;
        private System.Windows.Forms.ToolStripMenuItem TcMiSettings;
        private System.Windows.Forms.ToolStripSeparator TcSeparator5;
        private System.Windows.Forms.ToolStripMenuItem TcMiExit;
        private System.Windows.Forms.ContextMenuStrip RemoveItemContext;
        private System.Windows.Forms.ToolStripMenuItem RicMiRemove;
        /*
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
private System.Windows.Forms.MenuItem miSeperator4;
private System.Windows.Forms.MenuItem miExplorer;
private System.Windows.Forms.MenuItem miConfig;
private System.Windows.Forms.MenuItem miSettings;
private System.Windows.Forms.MenuItem miSeperator5;
*/
    }
}

