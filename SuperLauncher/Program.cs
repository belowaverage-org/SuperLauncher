using System;
using System.Diagnostics;
using System.Windows;

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
                ModernApplication.DispatcherUnhandledException += ModernApplication_DispatcherUnhandledException;
                ModernApplication.Run(new ModernLauncher());
            }
        }
        private static void ModernApplication_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\n\n" + e.Exception.StackTrace, "Super Launcher", MessageBoxButton.OK, MessageBoxImage.Error);
            ModernApplicationShutdown();
        }
        public static void ModernApplicationShutdown()
        {
            ModernApplicationShuttingDown = true;
            ModernApplication.Shutdown();
        }
    }
}