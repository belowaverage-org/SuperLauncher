using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.Timers;


namespace SuperLauncher
{
    static class Program
    {
        public static bool ModernApplicationShuttingDown = false;
        public static System.Windows.Application ModernApplication;
        public static Timer accountMonitorTimer = new Timer(TimeSpan.FromMinutes(30));
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

                // Clear any old notifications
                ToastNotificationManagerCompat.History.Clear();
                // Start the timer to handle monitoring password expiration
                accountMonitorTimer.Elapsed += AccountInfo.AccountMonitorTask;
                accountMonitorTimer.AutoReset = true;
                accountMonitorTimer.Enabled = true;

                ModernApplication.Run(new ModernLauncher());
            }
        }
        public static void ModernApplicationShutdown()
        {
            ModernApplicationShuttingDown = true;
            accountMonitorTimer.Stop();
            accountMonitorTimer.Dispose();
            ModernApplication.Shutdown();
        }

    }
}