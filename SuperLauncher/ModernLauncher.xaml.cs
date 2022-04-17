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
        public ModernLauncher()
        {
            InitializeComponent();
            WIH = new(this);
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
            _ = Task.Run(() => {
                Dispatcher.BeginInvoke(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(600);
                        BeginAnimation(Win32TopProperty, OpenAnimation);
                        await Task.Delay(600);
                        BeginAnimation(Win32TopProperty, CloseAnimation);
                    }
                });
            });
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
