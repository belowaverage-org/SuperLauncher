using System;
using System.DirectoryServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Timers;

namespace SuperLauncher
{
    public class CredentialExpirationService
    {
        private readonly Timer CheckTimer = new();
        private readonly Timer NotifyTimer = new();
        private DateTime PasswordLastSet = DateTime.MaxValue;
        private TimeSpan MaxPasswordAge = TimeSpan.MaxValue;
        private ExpStat Status = ExpStat.Unknown;
        private readonly SecurityIdentifier SID;
        public string PasswordExpirationMessage
        {
            get
            {
                if (Status == ExpStat.NeverExpires) return "Password never expires.";
                if (Status == ExpStat.DCNotResponding) return "Could not determine password expiration date, Active Directory is offline.";
                if (Status == ExpStat.Expires) return
                "Password expires in " +
                ExpirationTimeSpan.Days +
                " day(s), " +
                ExpirationTimeSpan.Hours +
                " hour(s) and " +
                ExpirationTimeSpan.Seconds +
                " second(s).";
                return "An un-known error occured when determining your password expiration date.";
            }
        }
        public DateTime PasswordExpirationDate
        {
            get
            {
                if (MaxPasswordAge == TimeSpan.MaxValue) return DateTime.MaxValue;
                return PasswordLastSet.Add(MaxPasswordAge);
            }
        }
        public TimeSpan ExpirationTimeSpan
        {
            get
            {
                return PasswordExpirationDate.Subtract(DateTime.Now);
            }
        }
        public CredentialExpirationService(SecurityIdentifier SID)
        {
            this.SID = SID;
            Task.Run(() =>
            {
                CheckTimer.Elapsed += CheckTimer_Elapsed;
                CheckTimer.Enabled = true;
                CheckTimer.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds;
                CheckTimer.Start();
                CheckTimer_Elapsed(null, null);
                if (Settings.Default.CredentialExpirationWarningIntervalMinutes <= 0) return;
                NotifyTimer.Elapsed += NotifyTimer_Elapsed;
                NotifyTimer.Enabled = true;
                NotifyTimer.Interval = TimeSpan.FromMinutes(Settings.Default.CredentialExpirationWarningIntervalMinutes).TotalMilliseconds;
                NotifyTimer.Start();
                NotifyTimer_Elapsed(null, null);
            });
        }
        private void NotifyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (PasswordExpirationDate.CompareTo(DateTime.Now.AddDays(Settings.Default.CredentialExpirationWarningDays)) <= 0)
            {
                ModernLauncherNotifyIcon.Icon.BalloonTipTitle = RunAsHelper.GetCurrentDomainWithUserName();
                ModernLauncherNotifyIcon.Icon.BalloonTipText = PasswordExpirationMessage;
                ModernLauncherNotifyIcon.Icon.ShowBalloonTip(0);
            }
        }
        private void CheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DirectorySearcher ds = new()
                {
                    SearchScope = SearchScope.Base
                };
                ds.PropertiesToLoad.Clear();
                ds.PropertiesToLoad.Add("maxPwdAge");
                ds.Filter = "";
                SearchResult root = ds.FindOne();
                MaxPasswordAge = TimeSpan.FromMicroseconds((long)root.Properties["maxPwdAge"][0] / 10 * -1);
                if (MaxPasswordAge == TimeSpan.MaxValue || MaxPasswordAge == TimeSpan.Zero) { Status = ExpStat.NeverExpires; return; }
                ds.SearchScope = SearchScope.Subtree;
                ds.PropertiesToLoad.Clear();
                ds.PropertiesToLoad.Add("userAccountControl");
                ds.PropertiesToLoad.Add("pwdLastSet");
                ds.Filter = "(objectSid=" + SID + ")";
                SearchResult user = ds.FindOne();
                PasswordLastSet = DateTime.FromFileTime((long)user.Properties["pwdLastSet"][0]);
                bool pwdNeverExpires = (((int)user.Properties["userAccountControl"][0]) & 0x00010000) == 0x00010000; //https://learn.microsoft.com/en-us/windows/win32/api/iads/ne-iads-ads_user_flag_enum
                if (pwdNeverExpires == true) { Status = ExpStat.NeverExpires; return; }
                Status = ExpStat.Expires;
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.DirectoryServices")
                {
                    Status = ExpStat.DCNotResponding;
                    return;
                }
                Status = ExpStat.Unknown;
            }
        }
        private enum ExpStat
        {
            Expires = 0,
            NeverExpires = 1,
            DCNotResponding = 2,
            Unknown = -1
        }
    }
}