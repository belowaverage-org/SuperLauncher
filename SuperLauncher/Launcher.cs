using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Specialized;

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
            if(UserAccountControl.Uac.IsProcessElevated())
            {
                elevateToolStripMenuItem.Visible = false;
            }
            elevateToolStripMenuItem.Image = Properties.Resources.shield.ToBitmap();
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
            if (e.Button == MouseButtons.Left)
            {
                Left = MousePosition.X - (Width / 2);
                Top = Screen.PrimaryScreen.WorkingArea.Bottom - Height;
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
    }
}
 