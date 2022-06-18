using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        public static void RunAs()
        {

        }
        public static string GetOriginalInvoker()
        {
            string invokerArg = Array.Find(Program.Arguments, (value) => { return value.StartsWith(InvokerArg); });
            if (invokerArg != null) return invokerArg.Substring(InvokerArg.Length);
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }
    }
}