using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Specialized;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        public ImageList imageList = new ImageList();

        public Launcher()
        {
            InitializeComponent();
            imageList.ImageSize = new Size(32, 32);
            if(Properties.Settings.Default.fileList == null)
            {
                Properties.Settings.Default.fileList = new StringCollection();
            }
            foreach(string file in Properties.Settings.Default.fileList)
            {
                addIcon(file);
            }
        }

        public void FadeIn()
        {
            Opacity = 0;
            Show();
            while(Opacity <= .96)
            {
                Thread.Sleep(1);
                Opacity += .02;
            }
        }

        public void FadeOut()
        {
            Opacity = .96;
            while (Opacity > 0)
            {
                Thread.Sleep(1);
                Opacity -= .02;
            }
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
            e.Cancel = true;
            FadeOut();
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Left = MousePosition.X - (Width / 2);
                Top = MousePosition.Y - (Height + 20);
                FadeIn();
                Activate();
            }
        }

        private void exitSuperLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FadeOut();
            Environment.Exit(0);
        }

        private void Launcher_Deactivate(object sender, EventArgs e)
        {
            if(!keepSuperLauncherOpenToolStripMenuItem.Checked)
            {
                FadeOut();
            }
        }

        private void IconsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach(string file in OpenFileDialog.FileNames)
            {
                addIcon(file);
                Properties.Settings.Default.fileList.Add(file);
                Properties.Settings.Default.Save();
            }
        }

        private void addShortcutStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
        }

        private void IconsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
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
    }
}
 