using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherContextMenu.xaml
    /// </summary>
    public partial class ModernLauncherContextMenu : Window
    {
        private bool ActuallyClose = false;
        private DoubleAnimation FadeIn = new()
        {
            To = 1,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut }
        };
        public ModernLauncherContextMenu()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Shared.SetWindowColor(this);
            Frame.BeginAnimation(OpacityProperty, FadeIn);
        }
        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            ActuallyClose = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ActuallyClose) e.Cancel = true;
        }
    }
}