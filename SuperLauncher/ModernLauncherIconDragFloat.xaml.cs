using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Timers;
using System;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherIconDragFloat.xaml
    /// </summary>
    public partial class ModernLauncherIconDragFloat : Window
    {
        readonly ImageSource ImageSource;
        Timer Timer;
        public ModernLauncherIconDragFloat(ImageSource ImageSource)
        {
            InitializeComponent();
            SetPos();
            this.ImageSource = ImageSource;
        }
        private void SetPos()
        {
            Win32Interop.GetCursorPos(out Win32Interop.POINT point);
            Left = point.x - (Width / 2);
            Top = point.y - (Height / 2);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wih = new(this);
            HwndSource hwndSource = HwndSource.FromHwnd(wih.Handle);
            Win32Interop.SetWindowLong(wih.Handle, Win32Interop.SetWindowLongIndex.GWL_EXSTYLE, Win32Interop.ExtendedWindowStyles.WS_EX_TRANSPARENT);
            Icon.Source = ImageSource;
            Timer = new()
            {
                Enabled = true,
                Interval = 6,
                AutoReset = true
            };
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(SetPos);
            if ((Win32Interop.GetKeyState(0x1) & 0x80) == 0) Dispatcher.Invoke(Close); //Close if mouse down.
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Timer.Stop();
        }
    }
}