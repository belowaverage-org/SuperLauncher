using Microsoft.Win32;
using System;
using System.Windows;
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
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");
            int themeColor = (int)key.GetValue("ColorizationColor");
            key.Close();
            byte[] colorBytes = BitConverter.GetBytes(themeColor);
            return Color.FromArgb(colorBytes[3], colorBytes[2], colorBytes[1], colorBytes[0]);
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
            Window.Background = new SolidColorBrush(color);
        }
    }
}