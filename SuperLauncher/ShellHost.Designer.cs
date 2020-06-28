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
            this.miNew = new System.Windows.Forms.MenuItem();
            this.miArrange = new System.Windows.Forms.MenuItem();
            this.miCascade = new System.Windows.Forms.MenuItem();
            this.miTileHoriz = new System.Windows.Forms.MenuItem();
            this.miTileVert = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miWindowList,
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
            this.miOrganize.Index = 2;
            this.miOrganize.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miArrange,
            this.miCascade,
            this.miTileHoriz,
            this.miTileVert});
            this.miOrganize.Text = "&Organize...";
            // 
            // miNew
            // 
            this.miNew.Index = 3;
            this.miNew.Text = "&New";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
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
            // ShellHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Menu = this.MainMenu;
            this.Name = "ShellHost";
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
    }
}