using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;

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
            /*Int32Animation fadeIn = new()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut }
            };
            Frame.BeginAnimation(TopProperty, fadeIn);*/
        }
        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
