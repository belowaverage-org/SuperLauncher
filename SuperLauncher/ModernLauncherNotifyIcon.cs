using System.Windows.Forms;

namespace SuperLauncher
{
    public static class ModernLauncherNotifyIcon
    {
        public static NotifyIcon Icon;
        public static void Initialize()
        {
            Icon = new();
            Icon.Icon = Resources.logo;
            Icon.Visible = true;
        }
    }
}
