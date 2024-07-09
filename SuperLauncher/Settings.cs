using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace SuperLauncher
{
    class Settings
    {
        public static SettingsDefault Default = new();
    }
    class SettingsDefault
    {
        private readonly string configDir = Path.Combine(@"C:\Users\Public\Documents\Below Average\Super Launcher\", RunAsHelper.GetOriginalInvokerDomain(), RunAsHelper.GetOriginalInvokerUserName());
        public string configPath = Path.Combine(@"C:\Users\Public\Documents\Below Average\Super Launcher\", RunAsHelper.GetOriginalInvokerDomain(), RunAsHelper.GetOriginalInvokerUserName(), "SuperLauncherConfig.xml");
        public XmlDocument XDoc = new();
        public XmlDocument XDocDefault = new();
        private string DefaultXML = 
        "<!-- Super Launcher Config File -->" +
        "<SuperLauncher>" +
        "   <AutoElevate>false</AutoElevate>" +
        "   <AutoRunAsDomain></AutoRunAsDomain>" +
        "   <AutoRunAsUser></AutoRunAsUser>" +
        "   <RememberMe>false</RememberMe>" +
        "   <UseLegacyUI>false</UseLegacyUI>" +
        "   <AppList>" +
        "       <App>C:\\Windows\\System32\\cmd.exe</App>" +
        "   </AppList>" +
        "   <Width>390</Width>" +
        "   <Height>230</Height>" +
        "   <CredentialExpirationWarningDays>7</CredentialExpirationWarningDays>" +
        "</SuperLauncher>";
        public bool AutoElevate
        {
            get
            {
                return ReadBool("AutoElevate");
            }
            set
            {
                Write("AutoElevate", value);
            }
        }
        public bool RememberMe
        {
            get
            {
                return ReadBool("RememberMe");
            }
            set
            {
                Write("RememberMe", value);
            }
        }
        public string AutoRunAsDomain
        {
            get
            {
                return Read("AutoRunAsDomain");
            }
            set
            {
                Write("AutoRunAsDomain", value);
            }
        }
        public string AutoRunAsUser
        {
            get
            {
                return Read("AutoRunAsUser");
            }
            set
            {
                Write("AutoRunAsUser", value);
            }
        }
        public bool UseLegacyUI
        {
            get
            {
                return ReadBool("UseLegacyUI");
            }
            set
            {
                Write("UseLegacyUI", value);
            }
        }
        public int Height
        {
            get
            {
                return ReadInt("Height");
            }
            set
            {
                Write("Height", value);
            }
        }
        public int Width
        {
            get
            {
                return ReadInt("Width");
            }
            set
            {
                Write("Width", value);
            }
        }
        public int CredentialExpirationWarningDays
        {
            get
            {
                return ReadInt("CredentialExpirationWarningDays");
            }
            set
            {
                Write("CredentialExpirationWarningDays", value);
            }
        }
        public string Read(string NodeName)
        {
            XmlNode node = XDoc.SelectSingleNode("/SuperLauncher/" + NodeName);
            if (node == null)
            {
                node = XDocDefault.SelectSingleNode("/SuperLauncher/" + NodeName);
                XmlNode newNode = XDoc.CreateElement(NodeName);
                newNode.InnerXml = node.InnerXml;
                XDoc.SelectSingleNode("/SuperLauncher").AppendChild(newNode);
                if (node == null) return null;
            }
            return node.InnerText;
        }
        public bool ReadBool(string NodeName)
        {
            if (!bool.TryParse(Read(NodeName), out bool val)) val = false;
            return val;
        }
        public int ReadInt(string NodeName)
        {
            if (!int.TryParse(Read(NodeName), out int val)) val = 0;
            return val;
        }
        public void Write(string NodeName, string Value)
        {
            XmlNode node = XDoc.SelectSingleNode("/SuperLauncher/" + NodeName);
            if (node == null)
            {
                node = XDoc.CreateElement(NodeName);
                XDoc.SelectSingleNode("/SuperLauncher").AppendChild(node);
            }
            node.InnerText = Value;
        }
        public void Write(string NodeName, bool Value)
        {
            Write(NodeName, Value.ToString());
        }
        public void Write(string NodeName, int Value)
        {
            Write(NodeName, Value.ToString());
        }
        public AppListStringCollection FileList
        {
            get
            {
                AppListStringCollection sc = new();
                XmlNodeList apps = XDoc.SelectNodes("/SuperLauncher/AppList/App");
                foreach (XmlNode app in apps)
                {
                    ((StringCollection)sc).Add(app.InnerText);
                }
                return sc;
            }
            set { }
        }
        public SettingsDefault()
        {
            XDocDefault.InnerXml = DefaultXML;
            if (File.Exists(configPath))
            {
                XDoc.Load(configPath);
            }
            else
            {
                XDoc.InnerXml = XDocDefault.InnerXml;
            }
        }
        public void Save()
        {
            if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);
            if (!File.Exists(configPath)) File.Create(configPath).Close();
            XDoc.Save(configPath);
        }
    }
    class AppListStringCollection : StringCollection
    {
        private readonly XmlNode Apps = Settings.Default.XDoc.SelectSingleNode("/SuperLauncher/AppList");
        public new int Add(string value)
        {
            XmlNode app = Settings.Default.XDoc.CreateElement("App");
            app.AppendChild(Settings.Default.XDoc.CreateTextNode(value));
            Apps.AppendChild(app);
            return base.Add(value);
        }
        public new void Remove(string value)
        {
            foreach (XmlNode app in Apps.ChildNodes)
            {
                if (app.InnerText == value)
                {
                    Apps.RemoveChild(app);
                    break;
                }
            }
            base.Remove(value);
        }
        public new void Clear()
        {
            Apps.RemoveAll();
            base.Clear();
        }
    }
}