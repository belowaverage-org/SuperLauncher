using System;
using System.Diagnostics;
using System.Windows.Controls;

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
                FileName = Environment.ProcessPath,
                Verb = "RunAs",
                Arguments = "\"/RunAs:" + Icon.FilePath + "\""
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
        private void BtnUnpin_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Settings.Default.FileList.Remove(Icon.FilePath);
            Settings.Default.Save();
            ((ModernLauncher)Program.ModernApplication.MainWindow).MLI.PopulateIcons();
        }
        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (RunAsHelper.IsElevated()) 
            {
                BtnRunAsAdmin.IsEnabled = false;
                BtnRunAsAdmin.Opacity = 0.5;
            }
        }
    }
}