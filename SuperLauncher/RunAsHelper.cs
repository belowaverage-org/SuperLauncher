using System;
using System.Diagnostics;
using System.Security;
using System.Security.Principal;
using System.Windows;

namespace SuperLauncher
{
    public static class RunAsHelper
    {
        public const string InvokerArg = "/OriginalInvoker:";
        public static string SelfPath = Process.GetCurrentProcess().MainModule.FileName;
        public static void Elevate()
        {
            ProcessStartInfo psi = new()
            {
                FileName = SelfPath,
                Arguments = InvokerArg + GetOriginalInvoker(),
                UseShellExecute = true,
                Verb = "RunAs"
            };
            try { Process.Start(psi); } catch { return; }
            Program.ModernApplication.Shutdown();
        }
        public static void RunAs(string DomainWithUserName, string Password)
        {
            string[] split = DomainWithUserName.Split('\\');
            string UserName = split[1];
            string Domain = split[0];
            ProcessStartInfo psi = new()
            {
                FileName = SelfPath,
                Arguments = InvokerArg + GetOriginalInvoker(),
                UseShellExecute = false,
                UserName = UserName,
                PasswordInClearText = Password,
                Domain = Domain
            };
            try { Process.Start(psi); } catch (Exception e) {
                MessageBoxResult result = MessageBox.Show(
                    e.Message,
                    "Super Launcher failed to switch users.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return; 
            }
            Program.ModernApplication.Shutdown();
        }
        public static string GetOriginalInvoker()
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
            return GetOriginalInvoker().Split('\\')[0];
        }
        public static string GetOriginalInvokerUserName()
        {
            return GetOriginalInvoker().Split('\\')[1];
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