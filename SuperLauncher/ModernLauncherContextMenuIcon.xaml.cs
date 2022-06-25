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
                FileName = Icon.FileName,
                Verb = "RunAs"
            };
            Process.Start(psi);
        }
        private void BtnOpenLocation_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {            
            new ShellHost(Icon.FilePath.Replace(Icon.FileName, "")).Show();
        }
        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (
                !Icon.FileName.ToLower().Contains(".exe")
            ) {
                BtnRunAsAdmin.IsEnabled = false;
                BtnRunAsAdmin.Opacity = 0.5;
            }
        }
    }
}