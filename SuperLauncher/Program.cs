﻿using System;
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
            if (Settings.Default.UseLegacyUI)
            {
                Application.EnableVisualStyles();
                Application.Run(new Launcher());
            }
            else
            {
                ModernApplication = new();
                ModernApplication.Run(new ModernLauncher());
            }
        }
    }
}