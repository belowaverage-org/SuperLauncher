using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherBadge.xaml
    /// </summary>
    public partial class ModernLauncherBadge : Window
    {
        private readonly int X, Y = 0;
        private readonly string Text;
        public ModernLauncherBadge(string Text, int X = 0, int Y = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Text = Text;
            InitializeComponent();
        }
        private void Label_Initialized(object sender, EventArgs e)
        {
            LabelText.Content = Text;
            if (X == 0 && Y == 0)
            {
                Win32Interop.GetCursorPos(out Win32Interop.POINT point);
                Top = ModernLauncher.DPI.ScalePixelsDown(point.y);
                Left = ModernLauncher.DPI.ScalePixelsDown(point.x);
            }
        }
    }
}
