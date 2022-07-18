using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    static class Win32Interop
    {
        [DllImport("User32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr Rect, bool Erase);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [StructLayout(LayoutKind.Sequential)]
        private struct MENUITEMINFO
        {
            public uint cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public string dwTypeData;
            public uint cch;
            public IntPtr hbmpItem;
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
        //private static Dictionary<MenuItem, Image> MenuItemIcons = new Dictionary<MenuItem, Image>();
        /// <summary>
        /// Sets an icon on a button with the FlatStyle set to System.
        /// </summary>
        /// <param name="Button">Button Object</param>
        /// <param name="Icon">Icon Object</param>
        public static void SetSystemIcon(this Button Button, Icon Icon, int Width = 16, int Height = 16)
        {
            _ = SendMessage(Button.Handle, 0xf7, (IntPtr)1, new Icon(Icon, Width, Height).Handle);
        }
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
        public static void EnableBlur(IntPtr Handle, uint BlurOpacity = 100, uint BlurBackgroundColor = 0x990000)
        {
            var accent = new AccentPolicy();
            //accent.AccentFlags = 2;
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            accent.GradientColor = (BlurOpacity << 24) | (BlurBackgroundColor & 0xFFFFFF);
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            SetWindowCompositionAttribute(Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
    }
}