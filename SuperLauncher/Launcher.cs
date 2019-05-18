using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Specialized;
using CredentialManagement;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        public ImageList imageList = new ImageList();
        public bool fakeClose = true;

        private bool fileDialogOpen = false;

        public Launcher()
        {
            var initialWidth = Properties.Settings.Default.width;
            var initialHeight = Properties.Settings.Default.height;
            InitializeComponent();
            if (UserAccountControl.Uac.IsProcessElevated())
            {
                ShieldIcon.Visible = true;
                UserLabel.Location = new Point(115, -2);
                UserLabel.Size = new Size(238, 22);
                elevateToolStripMenuItem.Text = "Elevated";
                elevateToolStripMenuItem.Enabled = false;
            }
            UserLabel.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            imageList.ImageSize = new Size(32, 32);
            Width = initialWidth;
            Height = initialHeight;
            if (Properties.Settings.Default.fileList == null)
            {
                Properties.Settings.Default.fileList = new StringCollection();
            }
            foreach (string file in Properties.Settings.Default.fileList)
            {
                if (File.Exists(file))
                {
                    addIcon(file);
                }
            }
        }

        public void FadeIn()
        {
            BringToFront();
            Show();
        }

        public void FadeOut()
        {
            Hide();
        }

        public class IconData
        {
            public string fileLocation;
            public IconData(string FileLocation)
            {
                fileLocation = FileLocation;
            }
        }

        public void addIcon(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            imageList.Images.Add(filePath, Icon.ExtractAssociatedIcon(filePath));
            IconsBox.LargeImageList = imageList;
            ListViewItem item = IconsBox.Items.Add(fileInfo.Name.Replace(fileInfo.Extension, ""));
            item.ImageKey = filePath;
            item.Tag = new IconData(filePath);
        }

        private void Launcher_Shown(object sender, EventArgs e)
        {
            FadeIn();
            FadeOut();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fakeClose)
            {
                e.Cancel = true;
                FadeOut();
            }
            else
            {
                Properties.Settings.Default.Save();
            }
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle PScreen = Screen.PrimaryScreen.WorkingArea;
            if (e.Button == MouseButtons.Left)
            {
                Left = MousePosition.X - (Width / 2);
                if((PScreen.Right - 5) < (Left + Width))
                {
                    Left = PScreen.Width - Width - 5;
                }
                Top = PScreen.Bottom - Height;
                FadeIn();
                Activate();
            }
        }

        private void exitSuperLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fakeClose = false;
            Close();
        }

        private void Launcher_Deactivate(object sender, EventArgs e)
        {
            FadeOut();
        }

        private void IconsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LaunchFocusedItem();
        }

        private void LaunchFocusedItem()
        {
            FadeOut();
            Process.Start(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (string file in OpenFileDialog.FileNames)
            {
                addIcon(file);
                Properties.Settings.Default.fileList.Add(file);
                Properties.Settings.Default.Save();
            }
        }

        private void addShortcutStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileDialogOpen)
            {
                fileDialogOpen = true;
                OpenFileDialog.ShowDialog();
                fileDialogOpen = false;
            }
        }

        private void IconsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickMenu.Show();
                RightClickMenu.Left = MousePosition.X;
                RightClickMenu.Top = MousePosition.Y;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.fileList.Remove(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            Properties.Settings.Default.Save();
            IconsBox.FocusedItem.Remove();
        }

        private void Launcher_Resize(object sender, EventArgs e)
        {
            Properties.Settings.Default.width = Width;
            Properties.Settings.Default.height = Height;
        }

        private void ElevateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAccountControl.Uac.ElevateAndQuit();
            Application.ExitThread();
        }

        private void RunAsStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRunAs();
        }

        private void ShowRunAs(int ErrorCode = 0)
        {
            VistaPrompt prompt = new VistaPrompt();
            prompt.ErrorCode = ErrorCode;
            prompt.Title = "Run As - Super Launcher";
            prompt.Message = "Please enter the credentials you would like Super Launcher to run as...";
            if (prompt.ShowDialog() == CredentialManagement.DialogResult.OK)
            {
                string username, domain;
                CredentialParser.ParseUserName(prompt.Username, out username, out domain);
                Process process = new Process();
                ProcessStartInfo startInfo = process.StartInfo;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.WorkingDirectory = Application.StartupPath;
                startInfo.Domain = domain;
                startInfo.UserName = username;
                startInfo.Password = prompt.SecurePassword;
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                try
                {
                    process.Start();
                    Application.ExitThread();
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    if(e.NativeErrorCode == 267)
                    {
                        TrayIcon.BalloonTipIcon = ToolTipIcon.Warning;
                        TrayIcon.BalloonTipText = "The credentials supplied does not have access to the currently running \"SuperLauncher\" executable file.";
                        TrayIcon.ShowBalloonTip(10000);
                    }
                    ShowRunAs(e.NativeErrorCode);
                }
            }
        }

        private void IconsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                LaunchFocusedItem();
            }
        }
    }
}
 