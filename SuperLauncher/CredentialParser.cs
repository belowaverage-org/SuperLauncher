using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperLauncher
{
    class CredentialParser
    {
        [DllImport("credui.dll", EntryPoint = "CredUIParseUserNameW", CharSet = CharSet.Unicode)]
        private static extern CredUIReturnCodes CredUIParseUserName(
            string userName,
            StringBuilder user,
            int userMaxChars,
            StringBuilder domain,
            int domainMaxChars
        );
        public enum CredUIReturnCodes
        {
            NO_ERROR = 0,
            ERROR_CANCELLED = 1223,
            ERROR_NO_SUCH_LOGON_SESSION = 1312,
            ERROR_NOT_FOUND = 1168,
            ERROR_INVALID_ACCOUNT_NAME = 1315,
            ERROR_INSUFFICIENT_BUFFER = 122,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_INVALID_FLAGS = 1004,
            ERROR_BAD_ARGUMENTS = 160
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        public static bool ParseUserName(string userName, out string user, out string domain)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            StringBuilder userBuilder = new StringBuilder();
            StringBuilder domainBuilder = new StringBuilder();

            CredUIReturnCodes returnCode = CredUIParseUserName(userName, userBuilder, int.MaxValue, domainBuilder, int.MaxValue);
            switch (returnCode)
            {
                case CredUIReturnCodes.NO_ERROR: // The username is valid.
                    user = userBuilder.ToString();
                    domain = domainBuilder.ToString();
                    return true;

                case CredUIReturnCodes.ERROR_INVALID_ACCOUNT_NAME: // The username is not valid.
                    user = userName;
                    domain = null;
                    return false;

                case CredUIReturnCodes.ERROR_INVALID_PARAMETER: // ulUserMaxChars or ulDomainMaxChars is zero OR userName, user, or domain is NULL.
                    throw new ArgumentNullException("userName");

                default:
                    user = null;
                    domain = null;
                    return false;
            }
        }
    }
}