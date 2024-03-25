using SuperLauncherCommon;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace SuperLauncherBootstrap
{
    public static class Bootstrap
    {
        private static readonly Splash Splash = new();
        private static readonly int SleepTime = 2000;
        private static readonly string TargetPath = "C:\\Users\\Public\\below average\\Super Launcher\\";
        private static readonly string TargetWindowName = "Super Launcher";
        private static readonly string TargetItem = "SuperLauncher.exe";
        private static readonly string TargetMessage = "ShowSuperLauncher";
        private static readonly string SelfPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!;
        [DllImport("User32.dll")]
        private static extern uint SendNotifyMessage(nint hWnd, uint Msg, uint wParam, uint lParam);
        [DllImport("User32.dll")]
        private static extern uint RegisterWindowMessage(string lpString);
        [DllImport("User32.dll")]
        private static extern nint FindWindow(string lpClassName, string lpWindowName);
        [STAThread]
        public static void Main()
        {
            nint existingWindow = FindWindow(null, TargetWindowName);
            if (existingWindow != nint.Zero)
            {
                SendNotifyMessage(existingWindow, RegisterWindowMessage(TargetMessage), 0x0, 0x0);
                return;
            }
            Splash.AllowClose = false;
            Application app = new();
            Task bsThread = Task.Run(() =>
            {
                DateTime start = DateTime.Now;
                BootstrapStart();
                DateTime after = DateTime.Now;
                TimeSpan difference = after - start;
                if (difference < TimeSpan.FromMilliseconds(SleepTime)) Thread.Sleep(SleepTime - difference.Milliseconds);
                if (!app.Dispatcher.HasShutdownStarted)
                {
                    app.Dispatcher.Invoke(() =>
                    {
                        Splash.Close();
                    });
                }
            });
            app.Run(Splash);
            bsThread.Wait();
        }
        public static void BootstrapStart()
        {
            Splash.MessageText = "Starting...";
            if (!File.Exists(Path.Combine(SelfPath, TargetItem)))
            {
                MessageBox.Show("Could not start, the main executable was not found.", "Failed to bootstrap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!File.Exists(Path.Combine(TargetPath, TargetItem)))
            {
                Copy();
            }
            FileVersionInfo SelfExecutable = FileVersionInfo.GetVersionInfo(Path.Combine(SelfPath, TargetItem));
            FileVersionInfo TargetExecutable = FileVersionInfo.GetVersionInfo(Path.Combine(TargetPath, TargetItem));
            if (SelfExecutable.ProductVersion != TargetExecutable.ProductVersion)
            {
                Copy();
            }
            Splash.MessageText = "Starting...";
            Process.Start(Path.Combine(TargetPath, TargetItem));
            Splash.MessageText = "Started.";
        }
        public static void Copy()
        {
            Splash.MessageText = "Purging old files...";
            if (Directory.Exists(TargetPath)) Directory.Delete(TargetPath, true);
            Copy(SelfPath, TargetPath);
        }
        public static void Copy(string SourceDir, string TargetDir)
        {
            if (!Directory.Exists(TargetDir)) Directory.CreateDirectory(TargetDir);
            foreach (DirectoryInfo childDir in new DirectoryInfo(SourceDir).GetDirectories())
            {
                Copy(Path.Combine(SourceDir, childDir.Name), Path.Combine(TargetDir, childDir.Name));
            }
            foreach (FileInfo childFile in new DirectoryInfo(SourceDir).GetFiles())
            {
                Splash.MessageText = "Copying: " + childFile.Name + "...";
                File.Copy(Path.Combine(SourceDir, childFile.Name), Path.Combine(TargetDir, childFile.Name));
            }
        }
    }
}
