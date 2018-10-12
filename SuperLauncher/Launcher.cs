using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Left = MousePosition.X - (Width / 2);
                Show();
            }
        }

        private void exitSuperLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
