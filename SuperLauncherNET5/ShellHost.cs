using SuperLauncherNET5;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

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
            MiNew_Click(null, null);
        }
        private void MiExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MiNew_Click(object sender, EventArgs e)
        {
            ShellView sv = new();
            sv.MdiParent = this;
            Size svSize = new(Size.Width - 4, Size.Height);
            sv.Size = svSize;
            sv.Show();
            sv.WindowState = FormWindowState.Maximized;
        }
        private void MiArrange_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void MiCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void MiTileHoriz_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void MiTileVert_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        /*
        private void miAdvancedShowSettings_Click(object sender, EventArgs e)
        {
            //MenuItem mi = (MenuItem)sender;
            mi.Checked = !mi.Checked;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            string tag = (string)mi.Tag;
            string name = tag.Substring(1);
            int set = 0;
            if (tag[0] == '0' && mi.Checked) set = 1;
            if (tag[0] == '1' && !mi.Checked) set = 1;
            key.SetValue(name, set);
            key.Close();
        }
        */
        /*
        private void miOptions_Popup(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", false);
            foreach (MenuItem mi in miOptions.MenuItems)
            {
                string tag = (string)mi.Tag;
                string name = tag.Substring(1);
                int set = (int)key.GetValue(name);
                if (tag[0] == '0')
                {
                    mi.Checked = (set == 1);
                }
                else
                {
                    mi.Checked = (set == 0);
                }
            }
            key.Close();
        }
        */
    }
}