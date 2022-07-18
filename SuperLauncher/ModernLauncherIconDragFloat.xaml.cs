using PInvoke;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Timers;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherIconDragFloat.xaml
    /// </summary>
    public partial class ModernLauncherIconDragFloat : Window
    {
        ImageSource ImageSource;
        Timer Timer;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            HwndSource hwndSource = HwndSource.FromHwnd(wih.Handle);
            User32.SetWindowLong(wih.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_TRANSPARENT);
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
            if ((User32.GetKeyState(0x1) & 0x80) == 0) Dispatcher.Invoke(Close); //Close if mouse down.
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Timer.Stop();
        }
    }
}