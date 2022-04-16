using System;
using System.Windows;
using PInvoke;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherBadge.xaml
    /// </summary>
    public partial class ModernLauncherBadge : Window
    {
        int X, Y = 0;
        string Text;
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
                User32.GetCursorPos(out POINT point);
                Top = point.y;
                Left = point.x;
            }
        }
    }
}
