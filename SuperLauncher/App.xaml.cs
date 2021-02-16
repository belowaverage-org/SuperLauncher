using Microsoft.Win32;
using SuperLauncher.Classes;
using System;
using System.Windows;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Launcher LauncherWindow = null;
        public App()
        {
            new RegWatcher(RegistryHive.CurrentUser, RegistryView.Default, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize").Event += SystemThemingChanged;
            new RegWatcher(RegistryHive.CurrentUser, RegistryView.Default, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent").Event += SystemThemingChanged;
        }
        private void SystemThemingChanged(object sender, EventArgs e)
        {
            Theming.Update();
        }
    }
}
