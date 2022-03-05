using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace SuperLauncher
{
    public partial class Run : Form
    {
        public RunMRU RunMRU = new();
        public Run()
        {
            InitializeComponent();
            pbIcon.Image = Resources.logo.ToBitmapAlpha(32, 32);
            Icon = Resources.logo;
            RunMRU.LoadMRU();
            RefreshMRU();
        }
        private void RefreshMRU()
        {
            cbInput.Items.Clear();
            cbInput.Items.AddRange(RunMRU.GetMRUList());
            if (cbInput.Items.Count > 0) cbInput.SelectedIndex = 0;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(cbInput.Text))
            {
                ShellHost sh = new(cbInput.Text);
                sh.Show();
                Close();
                return;
            }
            string[] fileParts = cbInput.Text.Split(' ');
            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = true,
                FileName = cbInput.Text
            };
            if (fileParts.Length != 0)
            {
                if (!File.Exists(cbInput.Text)) 
                {
                    string args = "";
                    for(int i = 1; i < fileParts.Length; i++)
                    {
                        args += fileParts[i];
                        if (i < fileParts.Length - 1) args += " ";
                    }
                    startInfo.FileName = fileParts[0];
                    startInfo.Arguments = args;
                }
            }
            try
            {
                Process.Start(startInfo);
                RunMRU.AddMRU(cbInput.Text);
                RunMRU.SaveMRU();
                Close();
            }
            catch
            {
                MessageBox.Show("Super Launcher cannot find '" + cbInput.Text + "'. Make sure you typed the name correctly, and then try again.", cbInput.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Run_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnOK_Click(null, null);
        }
        private void cbInput_TextUpdate(object sender, EventArgs e)
        {
            btnOK.Enabled = (cbInput.Text != "");
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.ShowDialog();
            cbInput.Text = ofd.FileName;
            cbInput_TextUpdate(null, null);
        }
    }
    public class RunMRU
    {
        private char[] MRUKeys = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private string[] MRUVals = new string[26];
        private char[] MRUList = new char[26];
        public void LoadMRU()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU");
            if (reg == null) return;
            string sMruList = (string)reg.GetValue("MRUList");
            if (sMruList == null) return;
            sMruList.ToCharArray().CopyTo(MRUList, 0);
            foreach (char item in MRUList)
            {
                if (item == '\0') continue;
                int mruKeyIndex = Array.IndexOf(MRUKeys, item);
                if (mruKeyIndex == -1) continue;
                MRUVals[mruKeyIndex] = (string)reg.GetValue(item.ToString());
            }
            reg.Close();
        }
        public void SaveMRU()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU", true);
            string sMruList = "";
            foreach (char letter in MRUList)
            {
                if (letter == '\0') continue;
                sMruList += letter;
            }
            reg.SetValue("MRUList", sMruList);
            foreach (char item in sMruList)
            {
                int mruKeyIndex = Array.IndexOf(MRUKeys, item);
                if (mruKeyIndex == -1) continue;
                reg.SetValue(item.ToString(), MRUVals[mruKeyIndex]);
            }
            reg.Close();
        }
        public void AddMRU(string Item)
        {
            int valIndex = Array.FindIndex(MRUVals, val => { return val?.ToLower() == Item.ToLower() + "\\1"; });
            if (valIndex != -1)
            {
                char letter = MRUKeys[valIndex];
                int listIndex = Array.IndexOf(MRUList, letter);
                while(listIndex-- != 0)
                {
                    MRUList[listIndex + 1] = MRUList[listIndex];
                }
                MRUList[0] = letter;
            }
            else
            {
                char freeLetter;
                int mruValIndex = Array.IndexOf(MRUVals, null);
                if (mruValIndex != -1)
                {
                    freeLetter = MRUKeys[mruValIndex];
                }
                else
                {
                    freeLetter = MRUList[25];
                }
                int mruKeyIndex = Array.IndexOf(MRUKeys, freeLetter);
                MRUVals[mruKeyIndex] = Item + "\\1";
                for (int i = MRUList.Length - 1; i > 0; i--)
                {
                    MRUList[i] = MRUList[i - 1];
                }
                MRUList[0] = freeLetter;
            }
        }
        public string GetMRUItem(char Index)
        {
            int iIndex = Array.IndexOf(MRUKeys, Index);
            if (iIndex == -1 || MRUVals[iIndex] == null) return "";
            return MRUVals[iIndex].Remove(MRUVals[iIndex].Length - 2);
        }
        public string[] GetMRUList()
        {
            int mruListLength = Array.IndexOf(MRUList, '\0');
            if (mruListLength == -1) mruListLength = 26;
            string[] ret = new string[mruListLength];
            for (int i = 0; i < ret.Length; i++) ret[i] = GetMRUItem(MRUList[i]);
            return ret;
        }
    }
}
