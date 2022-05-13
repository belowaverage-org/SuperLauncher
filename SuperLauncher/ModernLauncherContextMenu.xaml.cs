using System.Windows;
using System.Windows.Interop;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenu.xaml
    /// </summary>
    public partial class ModernLauncherContextMenu : Window
    {
        private WindowInteropHelper WIH;
        public ModernLauncherContextMenu()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WIH = new(this);
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
        }
    }
}
