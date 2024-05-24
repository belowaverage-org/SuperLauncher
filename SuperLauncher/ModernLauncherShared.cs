using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace SuperLauncher
{
    public static class Shared
    {
        public static double ScalePixelsUp(this DpiScale DPI, double Pixels)
        {
            return Pixels * DPI.DpiScaleX;
        }
        public static double ScalePixelsDown(this DpiScale DPI, double Pixels)
        {
            return Pixels / DPI.DpiScaleX;
        }
        public static string GetArugement(string ArgumentName)
        {
            string invokerArg = Array.Find(Program.Arguments, (value) => { return value.StartsWith("/" + ArgumentName + ":"); });
            if (invokerArg != null) return invokerArg.Substring(ArgumentName.Length + 2);
            return null;
        }
        public static Color GetColorizationColor()
        {
            try
            {
                RegistryKey key = Registry.Users.OpenSubKey(RunAsHelper.GetOriginalInvokerSID().ToString() + @"\Software\Microsoft\Windows\DWM");
                int themeColor = (int)key.GetValue("ColorizationColor");
                key.Close();
                byte[] colorBytes = BitConverter.GetBytes(themeColor);
                return Color.FromArgb(colorBytes[3], colorBytes[2], colorBytes[1], colorBytes[0]);
            }
            catch
            {
                return Colors.Black;
            }
        }
        public static byte AddAndClip(byte Original, byte Add)
        {
            if (Original + Add > 0xFF) return 0xFF;
            Original += Add;
            return Original;
        }
        public static Color MakeLighterNoAlpha(Color Color, byte Amount)
        {
            Color colorNoAlphaLighter = Color;
            colorNoAlphaLighter.A = 0xFF;
            colorNoAlphaLighter.R = AddAndClip(colorNoAlphaLighter.R, Amount);
            colorNoAlphaLighter.G = AddAndClip(colorNoAlphaLighter.G, Amount);
            colorNoAlphaLighter.B = AddAndClip(colorNoAlphaLighter.B, Amount);
            return colorNoAlphaLighter;
        }
        public static void SetWindowColor(Window Window)
        {
            Color color = GetColorizationColor();
            Window.Resources["AccentColor"] = MakeLighterNoAlpha(color, 0x65);
            Window.Resources["DarkerAccentColor"] = MakeLighterNoAlpha(color, 0x20);
        }
        public static void EnableAcrylic(Window Window)
        {
            Window.Background = Brushes.Transparent;
            nint handle = new WindowInteropHelper(Window).Handle;
            nint dwmDmVal = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(dwmDmVal, 1);
            Win32Interop.DwmSetWindowAttribute(handle, Win32Interop.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, dwmDmVal, 4);
            Marshal.FreeHGlobal(dwmDmVal);
            nint dwmBdVal = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(dwmBdVal, 3);
            Win32Interop.DwmSetWindowAttribute(handle, Win32Interop.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, dwmBdVal, 4);
            Marshal.FreeHGlobal(dwmBdVal);
            nint dwmTbVal = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(dwmTbVal, unchecked((int)0xFFFFFFFE));
            Win32Interop.DwmSetWindowAttribute(handle, Win32Interop.DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR, dwmTbVal, 4);
            Marshal.FreeHGlobal(dwmTbVal);
        }
        public static string ExtRemover(string FilePath)
        {
            FilePath = FilePath.Substring(FilePath.LastIndexOf('\\') + 1);
            FilePath = FilePath.Remove(FilePath.LastIndexOf('.'));
            return FilePath;
        }
        public static void StartProcess(string FilePath, string[] Arguments = null)
        {
            Task.Run(() =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo(FilePath, Arguments ?? [])
                    {
                        UseShellExecute = true,
                        WorkingDirectory = Environment.GetEnvironmentVariable("USERPROFILE")
                    });
                }
                catch { }
            });
        }

        /// <summary>
        /// Send a tost notification
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="msg">Message to user</param>
        /// 
        // This is the native notification implementation. Which doesn't work because the toast is sent to the run-as user
        // Instead of the desktop user we're actually viewing it as
        public static void SendDesktopToast_Native(string title, string msg)
        {
            // Round logo
            //Uri logoUri = new Uri(System.IO.Path.GetFullPath("48.png"));
            // Rocket
            Uri logoUri = new Uri(System.IO.Path.GetFullPath("sl_logo_big.png"));

            try
            {
                new ToastContentBuilder()
                    .AddText(title)
                    .AddText(msg)
                    .AddAppLogoOverride(logoUri, hintCrop: ToastGenericAppLogoCrop.Circle)
                    .Show(toast =>
                    {
                        toast.ExpirationTime = DateTime.Now.AddDays(1);
                    });
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.ToString());
                //throw;
            }
        }

        /// <summary>
        /// Send a tost notification
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="msg">Message to user</param>
        ///
        public static void SendDesktopToast(string title, string msg)
        {
            NotifyIcon notifyIcon = ModernLauncherNotifyIcon.Icon;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = msg;
            notifyIcon.BalloonTipIcon = ToolTipIcon.None;
            notifyIcon.ShowBalloonTip(15000);
        }
    }
}