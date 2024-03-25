using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace SuperLauncher
{
    public static class AccountInfo
    {
        // When was last notification sent?
        private static DateTime lastExpirationNotify = DateTime.MinValue;
        // Save this to avoid polling every time the tooltip is opened
        public static DateTime ExpirationDate;

        /// <summary>
        /// Get date from Active Directory when password expires
        /// </summary>
        /// <returns></returns>
        public static DateTime GetPasswordExpirationDate()
        {
            using (var userEntry = new DirectoryEntry("WinNT://" + Environment.UserDomainName + '/' + Environment.UserName + ",user"))
            {
                
                DateTime time = (DateTime)userEntry.InvokeGet("PasswordExpirationDate");
                return time.ToLocalTime();
            }
        }

        // This gets the account expiration, not the password expiration
        //public static DateTime GetPasswordExpirationDate()
        //{
        //    using PrincipalContext pc = new PrincipalContext(ContextType.Domain);
        //    using UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, Environment.UserName);

        //    DateTime time = (DateTime)user.AccountExpirationDate;
        //    return time.ToLocalTime();
        //}

        /// <summary>
        /// Get the number of whole days until the password of the user running the application expires
        /// </summary>
        public static int GetPasswordExpirationDays()
        {
            return (GetPasswordExpirationDate() - DateTime.Now).Days;
        }

        /// <summary>
        /// Check whether the provided password is correct for the current user
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        // Unneeded when using System.DirectoryServices.AccountManagement.ChangePassword below
        public static bool ValidatePassword(string password)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
            {
                // validate the credentials
                bool isValid = pc.ValidateCredentials(Environment.UserName, password);
                return isValid;
            }
        }

        public static string ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                using PrincipalContext pc = new PrincipalContext(ContextType.Domain);
                using UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, Environment.UserName);
                user.ChangePassword(currentPassword, newPassword);
                user.Save();

                return "Success";
            }
            catch (PasswordException ep)
            {
                return ep.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void AccountMonitorTask(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Update saved expiration date
            ExpirationDate = GetPasswordExpirationDate();
  
            NotifyExpiration(GetPasswordExpirationDays());
        }

        /// <summary>
        /// Notify at startup and every 12 hours
        /// </summary>
        /// <param name="daysRemain">number of days before password expires</param>
        public static void NotifyExpiration(int daysRemain)
        {
            //(daysRemain <= 10) &&
            if ((daysRemain <= 10) && (lastExpirationNotify.AddHours(12) < DateTime.Now))
            {
                Shared.SendDesktopToast("Password Expiration", $"The password for {Environment.UserName} expires in {daysRemain} days");
                lastExpirationNotify = DateTime.Now;
            }
        }
    }
}
