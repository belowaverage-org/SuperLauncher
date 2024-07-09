using System;
using System.Diagnostics;

namespace SuperLauncher
{
    static class Program
    {
        public static bool ModernApplicationShuttingDown = false;
        public static System.Windows.Application ModernApplication;
        public static string[] Arguments;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] arguments)
        {
            Arguments = arguments;
            string runAs = Shared.GetArugement("RunAs");
            if (runAs != null)
            {
                ProcessStartInfo psi = new()
                {
                    FileName = runAs,
                    UseShellExecute = true
                };
                Process.Start(psi);
                return;
            }
            if (Settings.Default.UseLegacyUI)
            {
                System.Windows.Forms.Application.Run(new Launcher());
            }
            else
            {
                RunAsHelper.StartupProcedure();
                ModernApplication = new();
                ModernApplication.Run(new ModernLauncher());
            }
        }
        public static void ModernApplicationShutdown()
        {
            ModernApplicationShuttingDown = true;
            ModernApplication.Shutdown();
        }
    }
}