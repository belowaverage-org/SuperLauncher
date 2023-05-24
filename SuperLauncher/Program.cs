using System;
using System.Windows.Forms;
using SuperLauncher.Properties;



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
            Settings.Default.CheckUpgrade();
            Application.EnableVisualStyles();
            Application.Run(new Launcher());
        }

    }
}
