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
        int X, Y = 0;
        string Text;
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetCursorPos(out Win32Point point);
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
                GetCursorPos(out Win32Point point);
                Top = point.Y;
                Left = point.X;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public uint X;
            public uint Y;
        }
    }
}
