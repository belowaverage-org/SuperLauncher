namespace SuperLauncher
{
    partial class ShellHost
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
            this.MsMiExplorer = new System.Windows.Forms.MenuStrip();
            this.MsMiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiShowFileExtensions = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiShowHiddenItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiShowSuperHiddenItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiOrganize = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiTileVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MsMiExplorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsMiExplorer
            // 
            this.MsMiExplorer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsMiFile,
            this.MsMiWindows,
            this.MsMiOptions,
            this.MsMiOrganize,
            this.MsMiNew});
            this.MsMiExplorer.Location = new System.Drawing.Point(0, 0);
            this.MsMiExplorer.MdiWindowListItem = this.MsMiWindows;
            this.MsMiExplorer.Name = "MsMiExplorer";
            this.MsMiExplorer.Size = new System.Drawing.Size(1282, 24);
            this.MsMiExplorer.TabIndex = 1;
            this.MsMiExplorer.Text = "MsMiExplorer";
            // 
            // MsMiFile
            // 
            this.MsMiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsMiExit});
            this.MsMiFile.Name = "MsMiFile";
            this.MsMiFile.Size = new System.Drawing.Size(37, 20);
            this.MsMiFile.Text = "File";
            // 
            // MsMiExit
            // 
            this.MsMiExit.Name = "MsMiExit";
            this.MsMiExit.Size = new System.Drawing.Size(93, 22);
            this.MsMiExit.Text = "Exit";
            // 
            // MsMiWindows
            // 
            this.MsMiWindows.Name = "MsMiWindows";
            this.MsMiWindows.Size = new System.Drawing.Size(68, 20);
            this.MsMiWindows.Text = "Windows";
            // 
            // MsMiOptions
            // 
            this.MsMiOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsMiShowFileExtensions,
            this.MsMiShowHiddenItems,
            this.MsMiShowSuperHiddenItems});
            this.MsMiOptions.Name = "MsMiOptions";
            this.MsMiOptions.Size = new System.Drawing.Size(70, 20);
            this.MsMiOptions.Text = "Options...";
            // 
            // MsMiShowFileExtensions
            // 
            this.MsMiShowFileExtensions.Name = "MsMiShowFileExtensions";
            this.MsMiShowFileExtensions.Size = new System.Drawing.Size(280, 22);
            this.MsMiShowFileExtensions.Tag = "1HideFileExt";
            this.MsMiShowFileExtensions.Text = "Show File Extensions";
            // 
            // MsMiShowHiddenItems
            // 
            this.MsMiShowHiddenItems.Name = "MsMiShowHiddenItems";
            this.MsMiShowHiddenItems.Size = new System.Drawing.Size(280, 22);
            this.MsMiShowHiddenItems.Tag = "0Hidden";
            this.MsMiShowHiddenItems.Text = "Show Hidden Items";
            // 
            // MsMiShowSuperHiddenItems
            // 
            this.MsMiShowSuperHiddenItems.Name = "MsMiShowSuperHiddenItems";
            this.MsMiShowSuperHiddenItems.Size = new System.Drawing.Size(280, 22);
            this.MsMiShowSuperHiddenItems.Tag = "0ShowSuperHidden";
            this.MsMiShowSuperHiddenItems.Text = "Show Protected Operating System Files";
            // 
            // MsMiOrganize
            // 
            this.MsMiOrganize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MsMiArrange,
            this.MsMiCascade,
            this.MsMiTileHorizontal,
            this.MsMiTileVertical});
            this.MsMiOrganize.Name = "MsMiOrganize";
            this.MsMiOrganize.Size = new System.Drawing.Size(75, 20);
            this.MsMiOrganize.Text = "Organize...";
            // 
            // MsMiArrange
            // 
            this.MsMiArrange.Name = "MsMiArrange";
            this.MsMiArrange.Size = new System.Drawing.Size(150, 22);
            this.MsMiArrange.Text = "Arrange";
            // 
            // MsMiCascade
            // 
            this.MsMiCascade.Name = "MsMiCascade";
            this.MsMiCascade.Size = new System.Drawing.Size(150, 22);
            this.MsMiCascade.Text = "Cascade";
            // 
            // MsMiTileHorizontal
            // 
            this.MsMiTileHorizontal.Name = "MsMiTileHorizontal";
            this.MsMiTileHorizontal.Size = new System.Drawing.Size(150, 22);
            this.MsMiTileHorizontal.Text = "Tile Horizontal";
            // 
            // MsMiTileVertical
            // 
            this.MsMiTileVertical.Name = "MsMiTileVertical";
            this.MsMiTileVertical.Size = new System.Drawing.Size(150, 22);
            this.MsMiTileVertical.Text = "Tile Vertical";
            // 
            // MsMiNew
            // 
            this.MsMiNew.Name = "MsMiNew";
            this.MsMiNew.Size = new System.Drawing.Size(43, 20);
            this.MsMiNew.Text = "New";
            // 
            // ShellHost
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 696);
            this.Controls.Add(this.MsMiExplorer);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MsMiExplorer;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ShellHost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorer";
            this.Load += new System.EventHandler(this.ShellHost_Load);
            this.MsMiExplorer.ResumeLayout(false);
            this.MsMiExplorer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip MsMiExplorer;
        private System.Windows.Forms.ToolStripMenuItem MsMiFile;
        private System.Windows.Forms.ToolStripMenuItem MsMiExit;
        private System.Windows.Forms.ToolStripMenuItem MsMiWindows;
        private System.Windows.Forms.ToolStripMenuItem MsMiOptions;
        private System.Windows.Forms.ToolStripMenuItem MsMiShowFileExtensions;
        private System.Windows.Forms.ToolStripMenuItem MsMiShowHiddenItems;
        private System.Windows.Forms.ToolStripMenuItem MsMiShowSuperHiddenItems;
        private System.Windows.Forms.ToolStripMenuItem MsMiOrganize;
        private System.Windows.Forms.ToolStripMenuItem MsMiArrange;
        private System.Windows.Forms.ToolStripMenuItem MsMiCascade;
        private System.Windows.Forms.ToolStripMenuItem MsMiTileHorizontal;
        private System.Windows.Forms.ToolStripMenuItem MsMiTileVertical;
        private System.Windows.Forms.ToolStripMenuItem MsMiNew;

        #endregion

        /*
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miWindowList;
        private System.Windows.Forms.MenuItem miNew;
        private System.Windows.Forms.MenuItem miOrganize;
        private System.Windows.Forms.MenuItem miArrange;
        private System.Windows.Forms.MenuItem miCascade;
        private System.Windows.Forms.MenuItem miTileHoriz;
        private System.Windows.Forms.MenuItem miTileVert;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miShowFileExt;
        private System.Windows.Forms.MenuItem miShowHiddenItems;
        private System.Windows.Forms.MenuItem miShowSuperHidden;
        */
    }
}