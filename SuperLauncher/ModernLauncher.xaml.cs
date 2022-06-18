using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using PInvoke;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Controls;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        public static DpiScale DPI;
        private bool IgnoreDeactivation = false;
        private WindowInteropHelper WIH;
        private HwndSource HWND;
        private DoubleAnimation RenderBoostAnimation = new()
        {
            Duration = TimeSpan.FromSeconds(0.3),
            From = 1,
            To = 0
        };
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
        }
        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x0312 && wParam.ToInt32() == 0) //WM_HOTKEY //ALT + S
            {
                OpenWindow();
            }
            if (msg == 0x0312 && wParam.ToInt32() == 1) //WM_HOTKEY //ALT + E
            {
                new ShellHost().Show();
            }
            if (msg == 0x0312 && wParam.ToInt32() == 2) //WM_HOTKEY //ALT + R
            {
                new Run().Show();
            }
            return IntPtr.Zero;
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
            if (IgnoreDeactivation) return;
            if (IsVisible) CloseWindow();
        }
        private void UpdateAnimations()
        {
            User32.MONITORINFO mi = new User32.MONITORINFO();
            mi.cbSize = Marshal.SizeOf(mi);
            IntPtr hMonitor = User32.MonitorFromWindow(WIH.Handle, User32.MonitorOptions.MONITOR_DEFAULTTONEAREST);
            User32.GetMonitorInfo(hMonitor, ref mi);
            OpenAnimation.From = CloseAnimation.From = (int)DPI.ScalePixelsUp(Top);
            CloseAnimation.To = mi.rcMonitor.bottom;
            OpenAnimation.To = (mi.rcWork.bottom - (int)DPI.ScalePixelsUp(Height) - (int)DPI.ScalePixelsUp(10));
        }
        public static readonly DependencyProperty Win32TopProperty = DependencyProperty.Register(
            "Win32Top",
            typeof(int),
            typeof(ModernLauncher),
            new FrameworkPropertyMetadata(
                new PropertyChangedCallback(WindowPositionChanged)
            )
        );
        private void SetElevateLabels()
        {
            ElevateUser.Content = Environment.UserDomainName + @"\" + Environment.UserName;
            if (UserAccountControl.Uac.IsProcessElevated())
            {
                ElevateIcon.Content = "";
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetPosition();
            WIH = new(this);
            HWND = HwndSource.FromHwnd(WIH.Handle);
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
            InitializeNotifyIcon();
            Win32Interop.RegisterHotKey(WIH.Handle, 0, 0x1 | 0x4000, 0x53); //Register Hot Key ALT + S
            Win32Interop.RegisterHotKey(WIH.Handle, 1, 0x1 | 0x4000, 0x45); //Register Hot Key ALT + E
            Win32Interop.RegisterHotKey(WIH.Handle, 2, 0x1 | 0x4000, 0x52); //Register Hot Key ALT + R
            HWND.AddHook(HwndSourceHook);
            SetElevateLabels();
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
            RenderBoost.BeginAnimation(OpacityProperty, RenderBoostAnimation);
            Filter.Text = "";
            Filter.Focus();
            await Task.Delay(300);
            InvalidateRect(WIH.Handle, IntPtr.Zero, false);
        }
        public async void CloseWindow()
        {
            IsVisible = false;
            UpdateAnimations();
            Activate();
            BeginAnimation(Win32TopProperty, CloseAnimation);
            RenderBoost.BeginAnimation(OpacityProperty, RenderBoostAnimation);
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
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            MLI.Filter = Filter.Text;
        }
        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) MoveFocus(new(FocusNavigationDirection.Next));
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsVisible && e.Key == Key.Escape) CloseWindow();
        }
        private void BtnExplorer_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
            new ShellHost().Show();
        }
        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
            new Run().Show();
        }
        private void BtnMore_Click(object sender, RoutedEventArgs e)
        {
            IgnoreDeactivation = true;
            ModernLauncherContextMenu menu = new();
            menu.Frame.Content = new ModernLauncherContextMenuMain();
            menu.Closed += Menu_Closed;
            menu.MouseUp += Menu_MouseUp;
            menu.Top = Top + Height - 255;
            menu.Left = Left + Width - 180;
            menu.Show();
            IgnoreDeactivation = false;
        }
        private void Menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CloseWindow();
        }
        private void Menu_Closed(object sender, EventArgs e)
        {
            Activate();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ModernLauncherNotifyIcon.Icon != null) ModernLauncherNotifyIcon.Icon.Dispose();
        }
    }
} 