using System;
using System.Windows;

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
    }
}