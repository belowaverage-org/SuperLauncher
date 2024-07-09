using System;
using System.DirectoryServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Timers;

namespace SuperLauncher
{
    public static class CredentialExpirationService
    {
        private static Timer Timer = new();
        public static DateTime ExpirationDate = DateTime.MaxValue;
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
            Task.Run(() => {
                try
                {
                    DirectorySearcher ds = new();
                    ds.SearchScope = SearchScope.Base;
                    ds.PropertiesToLoad.Clear();
                    ds.PropertiesToLoad.Add("maxPwdAge");
                    ds.Filter = "";
                    SearchResult root = ds.FindOne();
                    ds.SearchScope = SearchScope.Subtree;
                    ds.PropertiesToLoad.Clear();
                    ds.PropertiesToLoad.Add("userAccountControl");
                    ds.PropertiesToLoad.Add("pwdLastSet");
                    ds.Filter = "(objectSid=" + WindowsIdentity.GetCurrent().User.Value + ")";
                    SearchResult user = ds.FindOne();
                    bool pwdNeverExpires = (((int)user.Properties["userAccountControl"][0]) & 0x00010000) == 0x00010000; //https://learn.microsoft.com/en-us/windows/win32/api/iads/ne-iads-ads_user_flag_enum
                    DateTime ExpirationDate = DateTime.FromFileTime((long)user.Properties["pwdLastSet"][0]);
                    TimeSpan maxPwdAge = TimeSpan.FromMicroseconds((long)root.Properties["maxPwdAge"][0] / 10 * -1);
                    
                }
                catch
                {
                    //Just give up
                }
            });
        }
        //private static datetime convertfromadtime()
        //{

        //}
    }
}
