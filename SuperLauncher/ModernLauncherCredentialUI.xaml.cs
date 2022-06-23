﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherCredentialUI.xaml
    /// </summary>
    public partial class ModernLauncherCredentialUI : Window
    {
        public ModernLauncherCredentialUI()
        {
            InitializeComponent();
        }
        private bool ShouldDisableUserInput()
        {
            return (RunAsHelper.GetOriginalInvokerDomainWithUserName() != RunAsHelper.GetCurrentDomainWithUserName());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper WIH = new(this);
            Win32Interop.EnableBlur(WIH.Handle, 200, 0);
            TBUserName.Focus();
            CBRememberMe.IsChecked = Settings.Default.RememberMe;
            CBElevate.IsChecked = Settings.Default.AutoElevate;
            if(CredentialManager.CredReadA(
                "Super Launcher", 
                CredentialManager.CredType.CRED_TYPE_GENERIC, 
                CredentialManager.CredReadFlags.NONE, 
                out CredentialManager.CREDENTIAL cred
            )) {
                TBUserName.Text = cred.UserName;
                TBPassword.Password = cred.Password;
            }
            if (ShouldDisableUserInput())
            {
                TBUserName.IsEnabled = TBPassword.IsEnabled = false;
                TBUserName.Text = RunAsHelper.GetCurrentDomainWithUserName();
                TBPassword.Password = "************";
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.RememberMe = CBRememberMe.IsChecked.Value;
            Settings.Default.AutoElevate = CBElevate.IsChecked.Value;
            if (!ShouldDisableUserInput() || !Settings.Default.RememberMe)
            {
                if (TBUserName.Text != "" && !TBUserName.Text.Contains('\\')) TBUserName.Text = Environment.UserDomainName + "\\" + TBUserName.Text;
                CredentialManager.CREDENTIAL cred = new()
                {
                    TargetName = "Super Launcher",
                    Type = CredentialManager.CredType.CRED_TYPE_GENERIC,
                    Persist = CredentialManager.CredPersist.CRED_PERSIST_LOCAL_MACHINE,
                    UserName = TBUserName.Text,
                    Password = TBPassword.Password
                };
                CredentialManager.CredWriteA(cred, CredentialManager.CredWriteFlags.NONE);
            }
            Settings.Default.Save();
            Close();
        }
        private void TBPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnOK_Click(null, null);
        }
    }
}