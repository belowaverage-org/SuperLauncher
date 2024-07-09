using System;
using System.Windows;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherBadge.xaml
    /// </summary>
    public partial class ModernLauncherBadge : Window
    {
        public ModernLauncherBadge(string Text)
        {
            InitializeComponent();
            LabelText.Content = Text;
        }
        private void Label_Initialized(object sender, EventArgs e)
        {
            Win32Interop.GetCursorPos(out Win32Interop.POINT point);
            Top = ModernLauncher.DPI.ScalePixelsDown(point.y);
            Left = ModernLauncher.DPI.ScalePixelsDown(point.x) - Width;
        }
    }
}
