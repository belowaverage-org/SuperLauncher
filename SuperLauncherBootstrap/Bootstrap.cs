using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using SuperLauncherCommon;

namespace SuperLauncherBootstrap
{
    public static class Bootstrap
    {
        private static readonly Splash Splash = new();
        private static readonly int SleepTime = 2000;
        private static readonly string TargetPath = "C:\\Users\\Public\\below average\\Super Launcher\\";
        private static readonly string TargetItem = "SuperLauncher.exe";
        private static readonly string SelfPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!;
        [STAThread]
        public static void Main()
        {
            Splash.AllowClose = false;
            Application app = new();
            Task bsThread = Task.Run(() => {
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
            foreach(DirectoryInfo childDir in new DirectoryInfo(SourceDir).GetDirectories())
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
