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
            new SuperLauncherCommon.Splash().Show();
        }
    }
}
