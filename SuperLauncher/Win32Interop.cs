using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    public static class Win32Interop
    {
        [DllImport("Shell32.dll")]
        public static extern uint SHBindToParent(IntPtr pidl, Guid riid, out IntPtr ppv, out IntPtr ppidlLast);
        [DllImport("Shell32.dll")]
        public static extern void SHGetNameFromIDList(IntPtr pidl, SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        [DllImport("Shell32.dll")]
        public static extern void ILFree(IntPtr pidl);
        [DllImport("Shell32.dll")]
        public static extern uint SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string pszName, [Optional] IntPtr pbc, out IntPtr ppidl, uint sfgaoIn, out IntPtr psfgaoOut);
        [DllImport("User32.dll")]
        public static extern short GetKeyState(int nVirtKey);
        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("User32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, out MONITORINFO lpmi);
        [DllImport("User32.dll")]
        public static extern bool ChangeWindowMessageFilter(uint message, uint action);
        [DllImport("User32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, MonitorFromWindowFlags dwFlags);
        [DllImport("User32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr Rect, bool Erase);
        [DllImport("User32.dll")]
        public static extern uint RegisterWindowMessage(string lpString);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern long SetWindowLong(IntPtr hWnd, SetWindowLongIndex nIndex, ExtendedWindowStyles dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public uint cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public MonitoInfoFlags dwFlags;
        }
        public enum MonitoInfoFlags
        {
            MONITORINFOF_PRIMARY = 1
        }
        public enum MonitorFromWindowFlags
        {
            MONITOR_DEFAULTTONULL = 0x0,
            MONITOR_DEFAULTTOPRIMARY = 0x1,
            MONITOR_DEFAULTTONEAREST = 0x2
        }
        public enum SetWindowLongIndex
        {
            GWL_EXSTYLE = -20,
            GWL_HINSTANCE = -6,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4
        }
        public enum ExtendedWindowStyles
        {
            WS_EX_TRANSPARENT = 0x20,
            WS_EX_LAYERED = 0x80000,
            WS_EX_TOOLWINDOW = 0x80
        }
        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public uint AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        public enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }
        public enum SIGDN : uint
        {
            SIGDN_NORMALDISPLAY = 0,
            SIGDN_PARENTRELATIVEPARSING = 0x80018001,
            SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,
            SIGDN_PARENTRELATIVEEDITING = 0x80031001,
            SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,
            SIGDN_FILESYSPATH = 0x80058000,
            SIGDN_URL = 0x80068000,
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
            SIGDN_PARENTRELATIVE = 0x80080001,
            SIGDN_PARENTRELATIVEFORUI = 0x80094001
        }
        //private static Dictionary<MenuItem, Image> MenuItemIcons = new Dictionary<MenuItem, Image>();
        /*
        public static void SetMenuItemBitmap(this MenuItem MenuItem, Image Image)
        {
            if (MenuItemIcons.ContainsKey(MenuItem))
            {
                MenuItemIcons[MenuItem] = Image;
            }
            else
            {
                MenuItemIcons.Add(MenuItem, Image);
            }
        }
        public static void DrawMenuItemBitmaps(this Menu Menu)
        {
            foreach (MenuItem mi in Menu.MenuItems)
            {
                if (!MenuItemIcons.ContainsKey(mi)) continue;
                Image image = MenuItemIcons[mi];
                Bitmap bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                MENUITEMINFO mii = new MENUITEMINFO();
                mii.cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO));
                mii.fMask = 0x80u;
                mii.hbmpItem = bitmap.GetHbitmap(Color.FromArgb(0, 0, 0, 0));
                SetMenuItemInfo(mi.Parent.Handle, (uint)mi.Index, true, ref mii);
            }
        }
        */
        public static Bitmap ToBitmapAlpha(this Icon Icon, int Width, int Height, Color Background)
        {
            Icon rsIcon = new(Icon, Width, Height);
            Bitmap bmIcon = new(Width, Height);
            Graphics g = Graphics.FromImage(bmIcon);
            IntPtr hdc = g.GetHdc();
            DrawIconEx(hdc, 0, 0, rsIcon.Handle, Width, Height, 0, CreateSolidBrush((uint)ColorTranslator.ToWin32(Background)), 0x3);
            g.ReleaseHdc(hdc);
            return bmIcon;
        }
        public static Bitmap ToBitmapAlpha(this Icon Icon, int Width, int Height)
        {
            return ToBitmapAlpha(Icon, Width, Height, Color.White);
        }
        /*
        public static void EnableBlur(IntPtr Handle, uint BlurOpacity = 100, uint BlurBackgroundColor = 0x990000)
        {
            var accent = new AccentPolicy
            {
                //accent.AccentFlags = 2;
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                GradientColor = (BlurOpacity << 24) | (BlurBackgroundColor & 0xFFFFFF)
            };
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            SetWindowCompositionAttribute(Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
        */
    }
}