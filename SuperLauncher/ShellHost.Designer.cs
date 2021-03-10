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
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miWindowList = new System.Windows.Forms.MenuItem();
            this.miOrganize = new System.Windows.Forms.MenuItem();
            this.miArrange = new System.Windows.Forms.MenuItem();
            this.miCascade = new System.Windows.Forms.MenuItem();
            this.miTileHoriz = new System.Windows.Forms.MenuItem();
            this.miTileVert = new System.Windows.Forms.MenuItem();
            this.miNew = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miShowFileExt = new System.Windows.Forms.MenuItem();
            this.miShowHiddenItems = new System.Windows.Forms.MenuItem();
            this.miShowSuperHidden = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miWindowList,
            this.miOptions,
            this.miOrganize,
            this.miNew});
            // 
            // miFile
            // 
            this.miFile.Index = 0;
            this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miExit});
            this.miFile.Text = "&File";
            // 
            // miExit
            // 
            this.miExit.Index = 0;
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miWindowList
            // 
            this.miWindowList.Index = 1;
            this.miWindowList.MdiList = true;
            this.miWindowList.Text = "&Windows";
            // 
            // miOrganize
            // 
            this.miOrganize.Index = 3;
            this.miOrganize.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miArrange,
            this.miCascade,
            this.miTileHoriz,
            this.miTileVert});
            this.miOrganize.Text = "&Organize...";
            // 
            // miArrange
            // 
            this.miArrange.Index = 0;
            this.miArrange.Text = "&Arrange";
            this.miArrange.Click += new System.EventHandler(this.miArrange_Click);
            // 
            // miCascade
            // 
            this.miCascade.Index = 1;
            this.miCascade.Text = "&Cascade";
            this.miCascade.Click += new System.EventHandler(this.miCascade_Click);
            // 
            // miTileHoriz
            // 
            this.miTileHoriz.Index = 2;
            this.miTileHoriz.Text = "Tile &Horizontal";
            this.miTileHoriz.Click += new System.EventHandler(this.miTileHoriz_Click);
            // 
            // miTileVert
            // 
            this.miTileVert.Index = 3;
            this.miTileVert.Text = "Tile &Vertical";
            this.miTileVert.Click += new System.EventHandler(this.miTileVert_Click);
            // 
            // miNew
            // 
            this.miNew.Index = 4;
            this.miNew.Text = "&New";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // miOptions
            // 
            this.miOptions.Index = 2;
            this.miOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miShowFileExt,
            this.miShowHiddenItems,
            this.miShowSuperHidden});
            this.miOptions.Text = "Options...";
            this.miOptions.Popup += new System.EventHandler(this.miOptions_Popup);
            // 
            // miShowFileExt
            // 
            this.miShowFileExt.Index = 0;
            this.miShowFileExt.Tag = "1HideFileExt";
            this.miShowFileExt.Text = "Show File Extensions";
            this.miShowFileExt.Click += new System.EventHandler(this.miAdvancedShowSettings_Click);
            // 
            // miShowHiddenItems
            // 
            this.miShowHiddenItems.Index = 1;
            this.miShowHiddenItems.Tag = "0Hidden";
            this.miShowHiddenItems.Text = "Show Hidden Items";
            this.miShowHiddenItems.Click += new System.EventHandler(this.miAdvancedShowSettings_Click);
            // 
            // miShowSuperHidden
            // 
            this.miShowSuperHidden.Index = 2;
            this.miShowSuperHidden.Tag = "0ShowSuperHidden";
            this.miShowSuperHidden.Text = "Show Super Hidden Items";
            this.miShowSuperHidden.Click += new System.EventHandler(this.miAdvancedShowSettings_Click);
            // 
            // ShellHost
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 603);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Menu = this.MainMenu;
            this.Name = "ShellHost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorer";
            this.Load += new System.EventHandler(this.ShellHost_Load);
            this.ResumeLayout(false);

        }

        #endregion

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
    }
}