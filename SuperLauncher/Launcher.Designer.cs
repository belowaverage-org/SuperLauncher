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
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.superLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitSuperLauncherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.keepSuperLauncherOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IconsBox = new System.Windows.Forms.ListView();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DragLogo = new System.Windows.Forms.PictureBox();
            this.TrayMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DragLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipTitle = "Super Launcher";
            this.TrayIcon.ContextMenuStrip = this.TrayMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Super Launcher";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.superLauncherToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitSuperLauncherToolStripMenuItem,
            this.toolStripSeparator1,
            this.keepSuperLauncherOpenToolStripMenuItem});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TrayMenu.Size = new System.Drawing.Size(157, 88);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // superLauncherToolStripMenuItem
            // 
            this.superLauncherToolStripMenuItem.Enabled = false;
            this.superLauncherToolStripMenuItem.Name = "superLauncherToolStripMenuItem";
            this.superLauncherToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.superLauncherToolStripMenuItem.Text = "Super Launcher";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // exitSuperLauncherToolStripMenuItem
            // 
            this.exitSuperLauncherToolStripMenuItem.Name = "exitSuperLauncherToolStripMenuItem";
            this.exitSuperLauncherToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitSuperLauncherToolStripMenuItem.Text = "Exit...";
            this.exitSuperLauncherToolStripMenuItem.Click += new System.EventHandler(this.exitSuperLauncherToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // keepSuperLauncherOpenToolStripMenuItem
            // 
            this.keepSuperLauncherOpenToolStripMenuItem.Checked = true;
            this.keepSuperLauncherOpenToolStripMenuItem.CheckOnClick = true;
            this.keepSuperLauncherOpenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepSuperLauncherOpenToolStripMenuItem.Name = "keepSuperLauncherOpenToolStripMenuItem";
            this.keepSuperLauncherOpenToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.keepSuperLauncherOpenToolStripMenuItem.Text = "Pinned";
            // 
            // IconsBox
            // 
            this.IconsBox.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.IconsBox.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.IconsBox.AllowDrop = true;
            this.IconsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.IconsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IconsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconsBox.ForeColor = System.Drawing.Color.White;
            this.IconsBox.Location = new System.Drawing.Point(0, 69);
            this.IconsBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IconsBox.MultiSelect = false;
            this.IconsBox.Name = "IconsBox";
            this.IconsBox.ShowGroups = false;
            this.IconsBox.Size = new System.Drawing.Size(238, 229);
            this.IconsBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.IconsBox.TabIndex = 1;
            this.IconsBox.UseCompatibleStateImageBehavior = false;
            this.IconsBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.IconsBox_DragDrop);
            this.IconsBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.IconsBox_DragEnter);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem1.Text = "Exit Super Launcher";
            // 
            // DragLogo
            // 
            this.DragLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.DragLogo.Image = ((System.Drawing.Image)(resources.GetObject("DragLogo.Image")));
            this.DragLogo.Location = new System.Drawing.Point(0, 0);
            this.DragLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DragLogo.Name = "DragLogo";
            this.DragLogo.Size = new System.Drawing.Size(238, 69);
            this.DragLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DragLogo.TabIndex = 0;
            this.DragLogo.TabStop = false;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 298);
            this.ControlBox = false;
            this.Controls.Add(this.IconsBox);
            this.Controls.Add(this.DragLogo);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Launcher";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Launcher_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            this.TrayMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DragLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.PictureBox DragLogo;
        private System.Windows.Forms.ListView IconsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem exitSuperLauncherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem keepSuperLauncherOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem superLauncherToolStripMenuItem;
    }
}

