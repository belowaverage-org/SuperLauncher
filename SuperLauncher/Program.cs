using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SuperLauncher
{
    static class Program
    {
        public static Settings Settings = new();
        public static System.Windows.Application ModernApplication;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            if (Settings.Default.UseLegacyUI)
            {
                Application.Run(new Launcher());
            }
            else
            {
                ModernApplication = new();
                //ModernApplication.Run(new ModernLauncherCredentialUI());
                //ModernApplication.Run(new ModernLauncher());

                CredentialManager.CREDENTIAL cred = new()
                {
                    Type = CredentialManager.CredType.CRED_TYPE_GENERIC,
                    TargetName = "Super Launcher",
                    Comment = "Super Launcher - Run As",
                    Persist = CredentialManager.CredPersist.CRED_PERSIST_LOCAL_MACHINE,
                    UserName = "SUPERLAUNCHER",
                    Password = "HELLOWORLD"
                };

                //CredentialManager.CredWriteA(cred, CredentialManager.CredWriteFlags.NONE);

                CredentialManager.CredReadA("Super Launcher", CredentialManager.CredType.CRED_TYPE_GENERIC, CredentialManager.CredReadFlags.NONE, out CredentialManager.CREDENTIAL OutCred);
                _ = OutCred;
            }
        }
    }
}