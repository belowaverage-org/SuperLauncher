using SuperLauncher.Properties;
using System;
using System.Windows.Forms;

namespace SuperLauncher
{
    public partial class ShellView : Form
    {
        public ShellView()
        {
            InitializeComponent();
            Icon = Resources.logo;
            Browser.DocumentTitleChanged += Browser_DocumentTitleChanged;
        }
        private void Browser_DocumentTitleChanged(object sender, EventArgs e)
        {
            Text = Browser.DocumentTitle;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Browser.GoBack();
        }
        private void btnForward_Click(object sender, EventArgs e)
        {
            Browser.GoForward();
        }
        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtNav.Text = Browser.Url.ToString();
            btnBack.Enabled = Browser.CanGoBack;
            btnForward.Enabled = Browser.CanGoForward;
        }
        private void txtNav_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                try
                {
                    Browser.Url = new Uri(txtNav.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Browser.Refresh();
        }
    }
}
