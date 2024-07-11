using System;
using System.Windows;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherBadge.xaml
    /// </summary>
    public partial class ModernLauncherBadge : Window
    {
        private string Text = "Super Launcher";
        public ModernLauncherBadge(string Text)
        {
            this.Text = Text;
            InitializeComponent();
        }
        private void Label_Initialized(object sender, EventArgs e)
        {
            LabelText.Content = Text;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Win32Interop.GetCursorPos(out Win32Interop.POINT point);
            Top = ModernLauncher.DPI.ScalePixelsDown(point.y);
            Left = ModernLauncher.DPI.ScalePixelsDown(point.x) - Width;
        }
    }
}
