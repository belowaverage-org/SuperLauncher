using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using PInvoke;
using System.Windows.Media.Animation;
using System.Timers;
//using Image = System.Drawing.Image;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        private WindowInteropHelper WIH;
        private int y = 0;
        private int ySpeed = 65;
        private int screenHeight = (int)SystemParameters.FullPrimaryScreenHeight;
        public ModernLauncher()
        {
            InitializeComponent();
            WIH = new(this);
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            Top = screenHeight;
            y = (int)Top;
        }
        private async Task OpenAnimation()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(2));
                User32.MoveWindow(WIH.Handle, (int)Left, y -= (ySpeed -= 4), (int)Width, (int)Height, true);
                if (y <= 0) break;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
            _ = OpenAnimation();
        }
    }
}
