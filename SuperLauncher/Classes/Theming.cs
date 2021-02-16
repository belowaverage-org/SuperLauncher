using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperLauncher.Classes
{
    class Theming
    {
        public static bool SysEnableTransparency;
        public static bool SysColorPrevalence;
        public static bool SysAppsUseLightTheme;
        public static byte[] SysAccentColorMenu;
        public static void Update()
        {
            if (App.LauncherWindow != null)
            {
                
                int accentColorMenu = (int)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent").GetValue("AccentColorMenu");
                byte a, r, g, b = 0;
                a = (byte)((accentColorMenu & 0xff000000) >> 24);
                b = (byte)((accentColorMenu & 0x00ff0000) >> 16);
                g = (byte)((accentColorMenu & 0x0000ff00) >> 8);
                r = (byte)(accentColorMenu & 0x000000ff);
                SysAccentColorMenu = new byte[4] { a, r, g, b };
                App.LauncherWindow.Dispatcher.Invoke(new Action(() => {
                    byte[] c = SysAccentColorMenu;
                    App.LauncherWindow.Background = new SolidColorBrush(Color.FromArgb(c[0], c[1], c[2], c[3]));
                }));
            }
        }
    }
}
