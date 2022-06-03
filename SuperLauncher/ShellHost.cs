using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperLauncher
{
    public partial class ShellHost : Form
    {
        private string InitialPath;
        public ShellHost(string InitialPath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}")
        {
            FormHandle = Handle;
            InitializeComponent();
            MsMiExit.Click += MsMiExit_Click;
            MsMiNew.Click += MsMiNew_Click;
            MsMiArrange.Click += MsMiArrange_Click;
            MsMiCascade.Click += MsMiCascade_Click;
            MsMiTileVertical.Click += MsMiTileVertical_Click;
            MsMiTileHorizontal.Click += MsMiTileHorizontal_Click;
            MsMiOptions.DropDownOpening += MsMiOptions_DropDownOpening;
            MsMiShowFileExtensions.Click += MsMiOptionsSubItem_Click;
            MsMiShowHiddenItems.Click += MsMiOptionsSubItem_Click;
            MsMiShowSuperHiddenItems.Click += MsMiOptionsSubItem_Click;
            Icon = Resources.logo;
            this.InitialPath = InitialPath;
        }
        private void ShellHost_Load(object sender, EventArgs e)
        {
            MsMiNew_Click(null, null);
            foreach(Control ctrl in Controls)
            {
                if (ctrl.GetType() == typeof(MdiClient))
                {
                    ctrl.BackColor = Color.WhiteSmoke;
                    ctrl.BackgroundImage = Resources.logo_explorer_divot;
                    PInvoke.User32.SetWindowLong(ctrl.Handle, PInvoke.User32.WindowLongIndexFlags.GWL_EXSTYLE, 0);
                }
            }
            Width += 1; Height += 1;
        }
        private void MsMiExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MsMiNew_Click(object sender, EventArgs e)
        {
            ShellView sv = new(InitialPath);
            sv.MdiParent = this;
            Size svSize = new(Size.Width - 4, Size.Height);
            sv.Size = svSize;
            sv.Show();
            sv.WindowState = FormWindowState.Maximized;
            InitialPath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
        }
        private void MsMiArrange_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void MsMiCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void MsMiTileHorizontal_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void MsMiTileVertical_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void MsMiOptionsSubItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked = !mi.Checked;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            string tag = (string)mi.Tag;
            string name = tag.Substring(1);
            int set = 0;
            if (tag[0] == '0' && mi.Checked) set = 1;
            if (tag[0] == '1' && !mi.Checked) set = 1;
            key.SetValue(name, set);
            key.Close();
            MessageBox.Show("In order for these changes to take effect, right click anywhere in the file explorer window and select \"Refresh\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void MsMiOptions_DropDownOpening(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", false);
            foreach (ToolStripMenuItem mi in MsMiOptions.DropDownItems)
            {
                string tag = (string)mi.Tag;
                string name = tag.Substring(1);
                object set = key.GetValue(name);
                if (set == null) continue;
                if (tag[0] == '0')
                {
                    mi.Checked = ((int)set == 1);
                }
                else
                {
                    mi.Checked = ((int)set == 0);
                }
            }
            key.Close();
        }
    }
}