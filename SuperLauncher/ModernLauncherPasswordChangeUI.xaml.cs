using System.Windows;
using System.Windows.Input;
using System;

namespace SuperLauncher
{
    /// <summary>
    /// Interaction logic for ModernLauncherPasswordChangeUI.xaml
    /// </summary>
    public partial class ModernLauncherPasswordChangeUI : Window
    {
        public ModernLauncherPasswordChangeUI()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Shared.EnableAcrylic(this);
            Shared.SetWindowColor(this);
            LBUser.Content += Environment.UserName;
            TBCurrentPassword.Focus();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {

            string result = AccountInfo.ChangePassword(TBCurrentPassword.Password, TBConfirmPassword.Password);

            if (result != "Success")
            {
                LBError.Content = result;
                LBError.Visibility = Visibility.Visible;
                return;
            }

            if (Settings.Default.RememberMe)
            {
                CredentialManager.CREDENTIAL cred = new()
                {
                    TargetName = "Super Launcher",
                    Type = CredentialManager.CredType.CRED_TYPE_GENERIC,
                    Persist = CredentialManager.CredPersist.CRED_PERSIST_LOCAL_MACHINE,
                    UserName = RunAsHelper.GetOriginalInvokerDomainWithUserName(),
                    Password = TBConfirmPassword.Password
                };
                CredentialManager.CredWriteA(cred, CredentialManager.CredWriteFlags.NONE);
            }
            Program.ModernApplicationShutdown();
        }
        private void TBPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) BtnOK_Click(null, null);

        }

        // This highlights the textbox if they didn't match
        private void TB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (TBNewPassword.Password.Length <= 1) 
            {
                BtnOK.IsEnabled = false;
                TBConfirm_Border.BorderBrush = System.Windows.Media.Brushes.Transparent;
                return; 
            }

            if (TBNewPassword.Password != TBConfirmPassword.Password)
            {
                TBConfirm_Border.BorderBrush = System.Windows.Media.Brushes.Red;
                BtnOK.IsEnabled = false;
                LBError.Content = "New and confirm passwords don't match";
                LBError.Visibility = Visibility.Visible;
            }
            else
            {
                BtnOK.IsEnabled = true;
                TBConfirm_Border.BorderBrush = System.Windows.Media.Brushes.Green;
                LBError.Visibility = Visibility.Hidden;
            }
        }
    }
}