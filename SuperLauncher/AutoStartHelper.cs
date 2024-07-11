using Microsoft.Win32;
using System.Linq;

namespace SuperLauncher
{
    public static class AutoStartHelper
    {
        private const string SuperLauncherPath = "C:\\Users\\Public\\below average\\Super Launcher\\SuperLauncherBootstrap.exe";
        private const string RelativeRunKeyPath = "\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public static AutoStartStatus Status {
            get => GetStatus();
            set
            {
                if (value == AutoStartStatus.Enabled) Enable();
                if (value == AutoStartStatus.Disabled) Disable();
            }
        }
        private static string RunKeyPath
        {
            get
            {
                return RunAsHelper.GetOriginalInvokerSID() + RelativeRunKeyPath;
            }
        }
        private static void Enable()
        {
            if (Status == AutoStartStatus.Enabled) return;
            RegistryKey key = Registry.Users.OpenSubKey(RunKeyPath, true);
            try
            {
                key.SetValue("Super Launcher", SuperLauncherPath);
            }
            finally
            {
                key.Close();
            }
        }
        private static void Disable() 
        {
            if (Status == AutoStartStatus.Disabled) return;
            RegistryKey key = Registry.Users.OpenSubKey(RunKeyPath, true);
            try
            {
                key.DeleteValue("Super Launcher");
            }
            finally
            {
                key.Close();
            }
        }
        private static AutoStartStatus GetStatus()
        {
            RegistryKey key = Registry.Users.OpenSubKey(RunKeyPath);
            try
            {
                if (key.GetValueNames().Contains("Super Launcher")) return AutoStartStatus.Enabled;
                return AutoStartStatus.Disabled;
            }
            finally
            {
                key.Close();
            }
        }
        public enum AutoStartStatus
        {
            Unknown = -1,
            Disabled = 0,
            Enabled = 1
        }
    }
}