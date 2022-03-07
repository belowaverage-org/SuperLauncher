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
//using Image = System.Drawing.Image;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        public ModernLauncher()
        {
            InitializeComponent();
        }
        public void SetBackground()
        {
            /*User32.SafeDCHandle screen = User32.GetDC(IntPtr.Zero);
            User32.SafeDCHandle memory = Gdi32.CreateCompatibleDC(screen);
            IntPtr bitmap = Gdi32.CreateCompatibleBitmap(screen, (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight);
            Gdi32.SelectObject(memory, bitmap);
            Gdi32.BitBlt(memory.HWnd, 0, 0, 100, 100, screen.HWnd, 0, 0, 0x00CC0020);
            BitmapSource bitmaSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Background = new ImageBrush(bitmaSource);*/
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            SetBackground();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Win32Interop.EnableBlur(new WindowInteropHelper(this).Handle, 200, 0);
        }
    }
}
