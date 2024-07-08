using System.DirectoryServices;
using System.Security.Principal;
using System.Timers;

namespace SuperLauncher
{
    public static class CredentialExpirationService
    {
        private static Timer Timer = new();
        public static void Initialize()
        {
            Timer.Elapsed += CheckExpiration;
            Timer.Enabled = true;
            Timer.Interval = 10800000; //3 hours
            Timer.Start();
            CheckExpiration();
        }
        public static void CheckExpiration(object s = null, object e = null)
        {
            DirectorySearcher ds = new();
            ds.SearchScope = SearchScope.Base;
            ds.PropertiesToLoad.Clear();
            ds.PropertiesToLoad.Add("maxPwdAge");
            ds.Filter = "";
            SearchResult root = ds.FindOne();
            ds.SearchScope = SearchScope.Subtree;
            ds.PropertiesToLoad.Clear();
            ds.PropertiesToLoad.Add("pwdLastSet");
            ds.Filter = "(objectSid=" + WindowsIdentity.GetCurrent().User.Value + ")";
            SearchResult user = ds.FindOne();
            

            //Found user and found policy, do work here
        }
    }
}
