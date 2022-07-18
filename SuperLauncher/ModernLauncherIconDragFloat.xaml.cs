using System;
using PInvoke;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherIconDragFloat.xaml
    /// </summary>
    public partial class ModernLauncherIconDragFloat : Window
    {
        ImageSource ImageSource;
        public ModernLauncherIconDragFloat(ImageSource ImageSource)
        {
            InitializeComponent();
            SetPos();
            this.ImageSource = ImageSource;
        }
        private void SetPos()
        {
            User32.GetCursorPos(out POINT Point);
            Left = Point.x - (Width / 2);
            Top = Point.y - (Height / 2);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x200) //Mouse Move
            {
                SetPos();
            }
            if (msg == 0x202) //Mouse Up
            {
                Close();
            }
            return IntPtr.Zero;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            HwndSource hwndSource = HwndSource.FromHwnd(wih.Handle);
            hwndSource.AddHook(WndProc);
            Win32Interop.SetCapture(wih.Handle);
            User32.SetWindowLong(wih.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_TRANSPARENT);
            Icon.Source = ImageSource;
        }
    }
}
