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
using System.Diagnostics;
using System.Runtime.InteropServices;
//using Image = System.Drawing.Image;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncher.xaml
    /// </summary>
    public partial class ModernLauncher : Window
    {
        private WindowInteropHelper WIH;
        private Int32Animation OpenAnimation;
        private Int32Animation CloseAnimation;
        private bool IsVisible = false;
        public ModernLauncher()
        {
            InitializeComponent();
            WIH = new(this);
            ModernLauncherNotifyIcon.Initialize();
            ModernLauncherNotifyIcon.Icon.Click += Icon_Click;
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            ToggleWindow();
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
            CalculateAnimations();
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
            /*_ = Task.Run(() => {
                Dispatcher.BeginInvoke(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(600);
                        OpenWindow();
                        await Task.Delay(600);
                        CloseWindow();
                    }
                });
            });*/
        }
        [DllImport("User32.dll")]
        private static extern bool InvalidateRect(IntPtr Handle, IntPtr Rect, bool Erase);
        private async void OpenWindow()
        {
            IsVisible = true;
            User32.SetWindowLong(WIH.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_LAYERED);
            BeginAnimation(Win32TopProperty, OpenAnimation);
            await Task.Delay(300);
            InvalidateRect(WIH.Handle, IntPtr.Zero, false);
        }
        private async void CloseWindow()
        {
            IsVisible = false;
            BeginAnimation(Win32TopProperty, CloseAnimation);
            await Task.Delay(300);
            User32.SetWindowLong(WIH.Handle, User32.WindowLongIndexFlags.GWL_EXSTYLE, User32.SetWindowLongFlags.WS_EX_TOOLWINDOW);
        }
        private void ToggleWindow()
        {
            CalculateAnimations(); //Do on resize instead
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
        private void CalculateAnimations()
        {
            OpenAnimation = new()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                From = (int)SystemParameters.PrimaryScreenHeight,
                To = (int)(SystemParameters.PrimaryScreenHeight - Height) - 60,
                EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut }
            };
            CloseAnimation = new()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                To = (int)SystemParameters.PrimaryScreenHeight,
                From = (int)(SystemParameters.PrimaryScreenHeight - Height) - 60,
                EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseIn }
            };
            Timeline.SetDesiredFrameRate(OpenAnimation, 300);
            Timeline.SetDesiredFrameRate(CloseAnimation, 300);
        }
        public static void WindowPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ModernLauncher win = (ModernLauncher)obj;
            User32.MoveWindow(win.WIH.Handle, (int)win.Left, (int)e.NewValue, (int)win.Width, (int)win.Height, false);
        }
    }
}
