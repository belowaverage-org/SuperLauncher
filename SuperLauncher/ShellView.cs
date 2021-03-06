﻿using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;
using SuperLauncher.Properties;
using System;
using System.Windows.Forms;
using System.IO;

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
        private void btnBack_Click(object sender, EventArgs e)
        {
            Browser.NavigateLogLocation(NavigationLogDirection.Backward);
        }
        private void btnForward_Click(object sender, EventArgs e)
        {
            Browser.NavigateLogLocation(NavigationLogDirection.Forward);
        }
        private void txtNav_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btnNavUp_Click(object sender, EventArgs e)
        {
            Browser.Navigate(ShellObject.FromParsingName(Directory.GetParent(txtNav.Text).FullName));
        }
    }
}
