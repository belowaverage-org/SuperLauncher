using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace SuperLauncher
{
    public static class RunAsHelper
    {
        public const string InvokerArg = "/OriginalInvoker:";
        public static string SelfPath = Process.GetCurrentProcess().MainModule.FileName;
        public static void Restart()
        {
            ProcessStartInfo psi = new()
            {
                FileName = SelfPath,
                Arguments = InvokerArg + GetOriginalInvokerDomainWithUserName(),
                UseShellExecute = true
            };
            try { Process.Start(psi); } catch { return; }
            Exit();
        }
        public static void Elevate()
        {
            ProcessStartInfo psi = new()
            {
                FileName = SelfPath,
                Arguments = InvokerArg + GetOriginalInvokerDomainWithUserName(),
                UseShellExecute = true,
                Verb = "RunAs"
            };
            try { Process.Start(psi); } catch { return; }
            Exit();
        }
        public static void RunAs(string DomainWithUserName, string Password)
        {
            string[] split = DomainWithUserName.Split('\\');
            string UserName = split[1];
            string Domain = split[0];
            ProcessStartInfo psi = new()
            {
                FileName = SelfPath,
                Arguments = InvokerArg + GetOriginalInvokerDomainWithUserName(),
                UseShellExecute = false,
                UserName = UserName,
                PasswordInClearText = Password,
                Domain = Domain
            };
            try { Process.Start(psi); } catch (Exception e) {
                MessageBox.Show(
                    e.Message,
                    "Super Launcher failed to switch users.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return; 
            }
            Exit();
        }
        public static void StartupProcedure()
        {
            if (Settings.Default.RememberMe)
            {
                bool success = CredentialManager.CredReadA(
                    "Super Launcher",
                    CredentialManager.CredType.CRED_TYPE_GENERIC,
                    CredentialManager.CredReadFlags.NONE,
                    out CredentialManager.CREDENTIAL cred
                );
                if (
                    success &&
                    cred.UserName != null &&
                    GetCurrentDomainWithUserName().ToLower() != cred.UserName.ToLower() &&
                    GetOriginalInvokerDomainWithUserName().ToLower() != cred.UserName.ToLower()
                )
                {
                    RunAs(cred.UserName, cred.Password);
                }
            }
            else
            {
                CredentialManager.CredDeleteA("Super Launcher", CredentialManager.CredType.CRED_TYPE_GENERIC, CredentialManager.CredDeleteFlags.NONE);
            }
            if (Settings.Default.AutoElevate && !IsElevated()) Elevate();
        }
        public static string GetOriginalInvokerDomainWithUserName()
        {
            string invokerArg = GetOriginalInvokerArg();
            if (invokerArg != null) return invokerArg;
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }
        public static string GetOriginalInvokerArg()
        {
            string invokerArg = Array.Find(Program.Arguments, (value) => { return value.StartsWith(InvokerArg); });
            if (invokerArg != null) return invokerArg.Substring(InvokerArg.Length);
            return null;
        }
        public static string GetOriginalInvokerDomain()
        {
            return GetOriginalInvokerDomainWithUserName().Split('\\')[0];
        }
        public static string GetOriginalInvokerUserName()
        {
            return GetOriginalInvokerDomainWithUserName().Split('\\')[1];
        }
        public static string GetCurrentDomainWithUserName()
        {
            return Environment.UserDomainName + @"\" + Environment.UserName;
        }
        public static void Exit()
        {
            if (Program.ModernApplication != null)
            {
                Program.ModernApplication.Shutdown();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public static bool IsElevated()
        {
            foreach (IdentityReference ir in WindowsIdentity.GetCurrent().Groups)
            {
                if (ir.Value == "S-1-5-32-544") return true;
            }
            return false;
        }
    }
}