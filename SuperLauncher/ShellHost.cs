using SuperLauncher.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperLauncher
{
    public partial class ShellHost : Form
    {
        public ShellHost()
        {
            InitializeComponent();
            Icon = Resources.logo;
        }
        private void ShellHost_Load(object sender, EventArgs e)
        {
            miNew_Click(null, null);
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void miNew_Click(object sender, EventArgs e)
        {
            ShellView sv = new ShellView();
            sv.MdiParent = this;
            Size svSize = new Size(Size.Width - 4, Size.Height);
            sv.Size = svSize;
            sv.Show();
            sv.WindowState = FormWindowState.Maximized;
        }
        private void miArrange_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void miCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void miTileHoriz_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void miTileVert_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
