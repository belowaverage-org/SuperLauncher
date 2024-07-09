using System.Windows.Forms;

namespace SuperLauncher
{
    public static class ModernLauncherNotifyIcon
    {
        public static NotifyIcon Icon;
        public static void Initialize()
        {
            Icon = new()
            {
                Text = "Super Launcher",
                Icon = Resources.logo,
                Visible = true
            };
        }
    }
}
