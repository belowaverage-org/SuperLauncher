using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        public ImageList imageList = new ImageList();

        public Launcher()
        {
            InitializeComponent();
            imageList.ImageSize = new Size(32, 32);
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

        private void Launcher_Shown(object sender, EventArgs e)
        {
            FadeIn();
            //FadeOut();
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

        private void IconsBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData("FileDrop", true);
            foreach(string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                imageList.Images.Add(file, Icon.ExtractAssociatedIcon(file));
                IconsBox.LargeImageList = imageList;

                ListViewItem item = IconsBox.Items.Add(fileInfo.Name.Replace(fileInfo.Extension, ""));
                item.ImageKey = file;
                
                
                Console.WriteLine(file);
                item.Tag = new IconData(file);
            }
        }

        private void IconsBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void IconsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            Process.Start(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
        }
    }
}