using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {

        public Launcher()
        {
            InitializeComponent();
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

        private void Launcher_Shown(object sender, EventArgs e)
        {
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
            Environment.Exit(0);
        }

        private void Launcher_Deactivate(object sender, EventArgs e)
        {
            FadeOut();
        }
    }
}
