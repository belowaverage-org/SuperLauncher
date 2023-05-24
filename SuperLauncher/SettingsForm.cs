using SuperLauncher.Properties;
using System;
using System.Drawing;
using System.Diagnostics;

using System.Windows.Forms;

namespace SuperLauncher
{
    public partial class SettingsForm : Form
    {
        ConfigHelper configHelper = new ConfigHelper();
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Icon icon = new Icon(Resources.logo, 16, 16);
            this.Icon = icon;

            txtUsername.Text = configHelper.UserName;
            txtDomain.Text = configHelper.Domain;

            lblWho.Text = $"{Environment.UserDomainName}\\{Environment.UserName}";
        }

        private void chkAutoElevate_CheckedChanged(object sender, EventArgs e)
        {
            configHelper.UpdateSetting(chkAutoElevate.Checked, ConfigField.AutoElevate);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            configHelper.UpdateSetting(txtUsername.Text, ConfigField.UserName);
            configHelper.UpdateSetting(txtDomain.Text, ConfigField.Domain);
            configHelper.UpdateSetting(chkAutoElevate.Checked, ConfigField.AutoElevate);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
