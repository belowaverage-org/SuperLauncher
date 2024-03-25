using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperLauncher
{
    enum ConfigField
    {
        UserName,
        Domain,
        AutoElevate
    }
    class ConfigHelper
    {
        public string UserName { get; set; }
        public string Domain { get; set; }
        public bool AutoElevate { get; set; } = Settings.Default.AutoElevate;
        public ConfigHelper()
        {
            if (!string.IsNullOrEmpty(Settings.Default.AutoRunAsUser))
            {
                UserName = DecryptData(Settings.Default.AutoRunAsUser);
            }
            else
            {
                UserName = null;
            }
            if (!string.IsNullOrEmpty(Settings.Default.AutoRunAsDomain))
            {
                Domain = DecryptData(Settings.Default.AutoRunAsDomain);
            }
            else
            {
                Domain = null;
            }

        }
        public void UpdateSetting(dynamic configData, ConfigField configField)
        {
            switch (configField)
            {
                case ConfigField.UserName:
                    Settings.Default.AutoRunAsUser = EncryptData(configData);
                    UserName = configData;
                    break;
                case ConfigField.Domain:
                    Settings.Default.AutoRunAsDomain = EncryptData(configData);
                    Domain = configData;
                    break;
                case ConfigField.AutoElevate:
                    Settings.Default.AutoElevate = Convert.ToBoolean(configData);
                    AutoElevate = Convert.ToBoolean(configData);
                    break;
                default:
                    throw new ArgumentException("Invalid paramater specified", nameof(configField));
            }
            Settings.Default.Save();
        }
        public bool HasRunAsCredential()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Domain))
            {
                return true;
            }
            return false;
        }
        private static string EncryptData(string clearData)
        {
            byte[] encData = ProtectedData.Protect(Encoding.ASCII.GetBytes(clearData), null, DataProtectionScope.CurrentUser);
            string b64Data = Convert.ToBase64String(encData);
            return b64Data;
        }
        private static string DecryptData(string encData)
        {
            byte[] b64Data = ProtectedData.Unprotect(Convert.FromBase64String(encData), null, DataProtectionScope.CurrentUser);
            string clearData = Encoding.ASCII.GetString(b64Data);
            return clearData;
        }
    }
}