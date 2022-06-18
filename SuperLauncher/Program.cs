using System;

namespace SuperLauncher
{
    static class Program
    {
        public static Settings Settings = new();
        public static System.Windows.Application ModernApplication;
        public static string[] Arguments;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Arguments = arguments;
            if (Settings.Default.UseLegacyUI)
            {
                System.Windows.Forms.Application.Run(new Launcher());
            }
            else
            {
                ModernApplication = new();
                ModernApplication.Run(new ModernLauncher());
            }
        }
    }
}