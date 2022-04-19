using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using PInvoke;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        public static DpiScale DPI;
        private WindowInteropHelper WIH;
        private Int32Animation OpenAnimation = new()
        {
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut }
        };
        private Int32Animation CloseAnimation = new()
        {
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseIn }
        };
        private bool IsVisible = false;
        public ModernLauncher()
        {
            InitializeComponent();
            WIH = new(this);
            Timeline.SetDesiredFrameRate(OpenAnimation, 300);
            Timeline.SetDesiredFrameRate(CloseAnimation, 300);
            InitializeNotifyIcon();
        }
        private void InitializeNotifyIcon()
        {
            if (ModernLauncherNotifyIcon.Icon != null) ModernLauncherNotifyIcon.Icon.Dispose();
            ModernLauncherNotifyIcon.Initialize();
            ModernLauncherNotifyIcon.Icon.Click += Icon_Click;
        }
        private void Icon_Click(object sender, EventArgs e)
        {
            OpenWindow();
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (IsVisible) CloseWindow();
        }
        private void UpdateAnimations()
        {
            OpenAnimation.From = CloseAnimation.To = (int)DPI.ScalePixelsUp(SystemParameters.PrimaryScreenHeight);
            OpenAnimation.To = CloseAnimation.From = (int)DPI.ScalePixelsUp((SystemParameters.PrimaryScreenHeight - Height) - 60);
        }
        public static readonly DependencyProperty Win32TopProperty = DependencyProperty.Register(
            "Win32Top",
            typeof(int),
            typeof(ModernLauncher),
            new FrameworkPropertyMetadata(
                new PropertyChangedCallback(WindowPositionChanged)
            )
        );
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetPosition();
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
        }
        [DllImport("User32.dll")]
        private static extern bool InvalidateRect(IntPtr Handle, IntPtr Rect, bool Erase);
        public async void OpenWindow()
        {
            IsVisible = true;
            UpdateAnimations();
            Activate();
            User32.SetWindowLong(WIH.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_LAYERED);
            BeginAnimation(Win32TopProperty, OpenAnimation);
            await Task.Delay(300);
            InvalidateRect(WIH.Handle, IntPtr.Zero, false);
        }
        public async void CloseWindow()
        {
            IsVisible = false;
            UpdateAnimations();
            Activate();
            BeginAnimation(Win32TopProperty, CloseAnimation);
            await Task.Delay(300);
            User32.SetWindowLong(WIH.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_TOOLWINDOW);
        }
        private void ToggleWindow()
        {
            if (IsVisible)
            {
                CloseWindow();
            }
            else
            {
                OpenWindow();
            }
        }
        private void SetPosition()
        {
            Top = SystemParameters.PrimaryScreenHeight;
            Left = SystemParameters.PrimaryScreenWidth - Width - 10;
        }
        public static void WindowPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ModernLauncher win = (ModernLauncher)obj;
            User32.MoveWindow(win.WIH.Handle, (int)DPI.ScalePixelsUp(win.Left), (int)e.NewValue, (int)DPI.ScalePixelsUp(win.Width), (int)DPI.ScalePixelsUp(win.Height), false);
        }
        private void Window_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            if (e.NewDpi.DpiScaleX != DPI.DpiScaleX)
            {
                DPI = e.NewDpi;
                InitializeNotifyIcon();
            }
        }
    }
}
