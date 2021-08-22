using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Windows.Forms;
using System.IO;
using SuperLauncherNET5;

namespace SuperLauncher
{
    public partial class ShellView : Form
    {
        public ShellView()
        {
            InitializeComponent();
            Icon = Resources.logo;
            Browser.Navigate(ShellObject.FromParsingName("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"));
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Browser.NavigateLogLocation(NavigationLogDirection.Backward);
        }
        private void BtnForward_Click(object sender, EventArgs e)
        {
            Browser.NavigateLogLocation(NavigationLogDirection.Forward);
        }
        private void TxtNav_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                try
                {
                    Browser.Navigate(ShellObject.FromParsingName(txtNav.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a valid path / directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void Browser_NavigationComplete(object sender, NavigationCompleteEventArgs e)
        {
            Text = e.NewLocation.Name;
            if (e.NewLocation.IsFileSystemObject)
            {
                txtNav.Text = e.NewLocation.ParsingName;
            }
            else
            {
                txtNav.Text = e.NewLocation.GetDisplayName(DisplayNameType.Default);
            }
            btnNavUp.Enabled = Directory.Exists(txtNav.Text) && Directory.GetParent(txtNav.Text) != null;
            btnBack.Enabled = Browser.NavigationLog.CanNavigateBackward;
            btnForward.Enabled = Browser.NavigationLog.CanNavigateForward;
        }
        private void BtnNavUp_Click(object sender, EventArgs e)
        {
            Browser.Navigate(ShellObject.FromParsingName(Directory.GetParent(txtNav.Text).FullName));
        }
    }
}
