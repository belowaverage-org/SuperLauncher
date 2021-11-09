using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SuperLauncher
{
    public partial class Run : Form
    {
        public Run()
        {
            InitializeComponent();
            pbIcon.Image = Resources.run_ico.ToBitmap();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(cbInput.Text))
            {
                ShellHost sh = new(cbInput.Text);
                sh.Show();
                Close();
                return;
            }
            string[] fileParts = cbInput.Text.Split(' ');
            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = true,
                FileName = cbInput.Text
            };
            if (fileParts.Length != 0)
            {
                if (!File.Exists(cbInput.Text)) 
                {
                    string args = "";
                    for(int i = 1; i < fileParts.Length; i++)
                    {
                        args += fileParts[i];
                        if (i < fileParts.Length - 1) args += " ";
                    }
                    startInfo.FileName = fileParts[0];
                    startInfo.Arguments = args;
                }
            }
            try
            {
                Process.Start(startInfo);
                Close();
            }
            catch
            {
                MessageBox.Show("Super Launcher cannot find '" + cbInput.Text + "'. Make sure you typed the name correctly, and then try again.", cbInput.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Run_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnOK_Click(null, null);
        }
        private void cbInput_TextUpdate(object sender, EventArgs e)
        {
            btnOK.Enabled = (cbInput.Text != "");
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.ShowDialog();
            cbInput.Text = ofd.FileName;
            cbInput_TextUpdate(null, null);
        }
    }
}
