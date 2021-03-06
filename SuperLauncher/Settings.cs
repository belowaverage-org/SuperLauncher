﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace SuperLauncher.Properties {
    class Settings {
        public static SettingsDefault Default = new SettingsDefault();
    }
    class SettingsDefault
    {
        private string configDir = Path.Combine(@"C:\Users\Public\Documents\Below Average\Super Launcher\", Environment.UserDomainName, Environment.UserName);
        public string configPath = Path.Combine(@"C:\Users\Public\Documents\Below Average\Super Launcher\", Environment.UserDomainName, Environment.UserName, "SuperLauncherConfig.xml");
        public XmlDocument XDoc = new XmlDocument();
        public bool autoElevate {
            get
            {
                if (!bool.TryParse(XDoc.SelectSingleNode("/SuperLauncher/AutoElevate").InnerText, out bool val)) val = false;
                return val;
            }
            set
            {
                XDoc.SelectSingleNode("/SuperLauncher/AutoElevate").InnerText = value.ToString();
            }
        }
        public string autoRunAsDomain
        {
            get
            {
                return XDoc.SelectSingleNode("/SuperLauncher/AutoRunAsDomain").InnerText;
            }
            set
            {
                XDoc.SelectSingleNode("/SuperLauncher/AutoRunAsDomain").InnerText = value;
            }
        }
        public string autoRunAsUser
        {
            get
            {
                return XDoc.SelectSingleNode("/SuperLauncher/AutoRunAsUser").InnerText;
            }
            set
            {
                XDoc.SelectSingleNode("/SuperLauncher/AutoRunAsUser").InnerText = value;
            }
        }
        public AppListStringCollection fileList
        {
            get
            {
                AppListStringCollection sc = new AppListStringCollection();
                XmlNodeList apps = XDoc.SelectNodes("/SuperLauncher/AppList/App");
                foreach(XmlNode app in apps)
                {
                    ((StringCollection)sc).Add(app.InnerText);
                }
                return sc;
            }
            set { }
        }
        public int height
        {
            get
            {
                if (!int.TryParse(XDoc.SelectSingleNode("/SuperLauncher/Height").InnerText, out int val)) val = 0;
                return val;
            }
            set
            {
                XDoc.SelectSingleNode("/SuperLauncher/Height").InnerText = value.ToString();
            }
        }
        public int width
        {
            get
            {
                if (!int.TryParse(XDoc.SelectSingleNode("/SuperLauncher/Width").InnerText, out int val)) val = 0;
                return val;
            }
            set
            {
                XDoc.SelectSingleNode("/SuperLauncher/Width").InnerText = value.ToString();
            }
        }
        public SettingsDefault()
        {
            if(File.Exists(configPath))
            {
                XDoc.Load(configPath);
            }
            else
            {
                XDoc.InnerXml =
                "<!-- Super Launcher Config File -->" +
                "<SuperLauncher>" +
                "   <AutoElevate>false</AutoElevate>" +
                "   <AutoRunAsDomain></AutoRunAsDomain>" +
                "   <AutoRunAsUser></AutoRunAsUser>" +
                "   <AppList>" +
                "       <App>C:\\Windows\\System32\\cmd.exe</App>" +
                "   </AppList>" +
                "   <Width>390</Width>" +
                "   <Height>230</Height>" +
                "</SuperLauncher>";
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
        private XmlNode Apps = Settings.Default.XDoc.SelectSingleNode("/SuperLauncher/AppList");
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
    } 
}