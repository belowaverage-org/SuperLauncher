using System;

namespace SuperLauncher
{
    static class Program
    {
        public static Settings Settings = new();
        public static System.Windows.Application ModernApplication;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            if (Settings.Default.UseLegacyUI)
            {
                System.Windows.Forms.Application.Run(new Launcher());
            }
            else
            {
                ModernApplication = new();
                ModernApplication.Run(new ModernLauncherCredentialUI());
                //ModernApplication.Run(new ModernLauncher());
            }
        }
    }
}