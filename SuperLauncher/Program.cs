using System;
using System.Windows.Forms;

namespace SuperLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new ModernLauncher().Show();
            Application.EnableVisualStyles();
            Application.Run(new Launcher());
        }
    }
}