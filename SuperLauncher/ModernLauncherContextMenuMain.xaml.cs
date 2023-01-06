using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenuMain.xaml
    /// </summary>
    public partial class ModernLauncherContextMenuMain : Page
    {
        public ModernLauncherContextMenuMain()
        {
            InitializeComponent();
        }
        private void BtnAbout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SuperLauncherCommon.Splash splash = new()
            {
                MessageText = "Original Invoker: " + RunAsHelper.GetOriginalInvokerDomainWithUserName()
            };
            splash.Show();
        }
        private void BtnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Program.ModernApplicationShutdown();
        }
        private void BtnSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new SettingsForm().ShowDialog();
        }
        private void BtnViewConfig_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("OpenWith.exe", "\"" + Settings.Default.configPath + "\"");
        }
        private void BtnElevate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RunAsHelper.Elevate();
        }
        private void BtnRunAs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new ModernLauncherCredentialUI().ShowDialog();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (RunAsHelper.IsElevated()) BtnElevate.IsEnabled = false;
        }
        private void BtnHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("OpenWith.exe", "https://github.com/belowaverage-org/SuperLauncher/wiki");
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (IsMouseOver)
            {
                ((ModernLauncher)Program.ModernApplication.MainWindow).CloseWindow();
            }
            else
            {
                Program.ModernApplication.MainWindow.Activate();
            }
        }
    }
}