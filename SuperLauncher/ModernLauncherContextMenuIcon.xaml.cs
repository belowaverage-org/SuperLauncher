using System;
using System.Diagnostics;
using System.Windows.Controls;
using IWshRuntimeLibrary;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenuMain.xaml
    /// </summary>
    public partial class ModernLauncherContextMenuIcon : Page
    {
        private readonly ModernLauncherIcon Icon;
        public ModernLauncherContextMenuIcon(ModernLauncherIcon Icon)
        {
            this.Icon = Icon;
            InitializeComponent();
        }
        private void BtnRunAsAdmin_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ProcessStartInfo psi = new()
            {
                UseShellExecute = true,
                FileName = Icon.FilePath,
                Verb = "RunAs"
            };
            try
            {
                Process.Start(psi);
            }
            catch (Exception) { }
        }
        private void BtnOpenLocation_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {            
            new ShellHost(Icon.FilePath.Replace(Icon.FileName, "")).Show();
        }
        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isExecutable = false;
            try
            {
                WshShell shell = new();
                WshShortcut shortcut = shell.CreateShortcut(Icon.FilePath);
                isExecutable = shortcut.TargetPath.ToLower().Contains(".exe");
            }
            catch (Exception)
            {
                isExecutable = Icon.FileName.ToLower().Contains(".exe");
            }   
            if (!isExecutable)
            {
                BtnRunAsAdmin.IsEnabled = false;
                BtnRunAsAdmin.Opacity = 0.5;
            }
        }
        private void BtnUnpin_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Settings.Default.FileList.Remove(Icon.FilePath);
            Settings.Default.Save();
            ((ModernLauncher)Program.ModernApplication.MainWindow).MLI.PopulateIcons();
        }
    }
}