using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Specialized;
using CredentialManagement;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        [DllImport("user32.dll")]
        public static extern int SetMenuItemBitmaps(IntPtr hMenu, IntPtr nPosition, int wFlags, IntPtr hBitmapUnchecked, IntPtr hBitmapChecked);

        public ImageList imageList = new ImageList();
        public bool fakeClose = true;

        private bool fileDialogOpen = false;
        private static Bitmap BorderShadowBitmap = new Bitmap(1, 1);
        private static Graphics BorderShadowGraphics = null;
        private static bool BorderMouseDown = false;
        private static Rectangle BeforeReize = new Rectangle();
        private static ResizeDynamic CurrentDynamic = new ResizeDynamic();
        private static ResizeDynamic LockedDynamic = new ResizeDynamic();
        private class ResizeDynamic
        {
            public bool Top;
            public bool Bottom;
            public bool Left;
            public bool Right;
            public ResizeDynamic(bool Top = false, bool Bottom = false, bool Left = false, bool Right = false)
            {
                this.Top = Top;
                this.Bottom = Bottom;
                this.Left = Left;
                this.Right = Right;
            }
        }

        public Launcher()
        {
            var initialWidth = Properties.Settings.Default.width;
            var initialHeight = Properties.Settings.Default.height;
            InitializeComponent();

            Bitmap test = new Bitmap(20, 20);
            IntPtr bmptr = test.GetHbitmap();
            SetMenuItemBitmaps(TrayMenu.Handle, (IntPtr)0, 0x400, bmptr, bmptr);

            TrayIcon.ContextMenu = TrayMenu;
            if (UserAccountControl.Uac.IsProcessElevated())
            {
                ShieldIcon.Visible = true;
                UserLabel.Location = new Point(152, 7);
                //elevateToolStripMenuItem.Text = "Elevated";
                //elevateToolStripMenuItem.Enabled = false;
            }
            UserLabel.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            imageList.ImageSize = new Size(32, 32);
            Width = initialWidth;
            Height = initialHeight;
            if (Properties.Settings.Default.fileList == null)
            {
                Properties.Settings.Default.fileList = new StringCollection();
            }
            foreach (string file in Properties.Settings.Default.fileList)
            {
                if (File.Exists(file))
                {
                    addIcon(file);
                }
            }
        }
        public void FadeIn()
        {
            BorderMouseDown = false;
            ResumeLayout();
            BringToFront();
            BorderShadowBitmap.Dispose();
            BorderShadowBitmap = new Bitmap(Width, Height);
            BorderShadowGraphics = Graphics.FromImage(BorderShadowBitmap);
            BorderShadowGraphics.CopyFromScreen(Left, Top, 0, 0, new Size(Width, Height));
            pbBorderShadow.Image = BorderShadowBitmap;
            BorderShadowGraphics.Dispose();
            InnerBorderPanel.Show();
            pbBorderShadow.Show();
            Show();
        }
        public void FadeOut()
        {
            Hide();
        }
        public class IconData
        {
            public string fileLocation;
            public IconData(string FileLocation)
            {
                fileLocation = FileLocation;
            }
        }
        public void addIcon(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            imageList.Images.Add(filePath, Icon.ExtractAssociatedIcon(filePath));
            IconsBox.LargeImageList = imageList;
            ListViewItem item = IconsBox.Items.Add(fileInfo.Name.Replace(fileInfo.Extension, ""));
            item.ImageKey = filePath;
            item.Tag = new IconData(filePath);
        }
        private void Launcher_Shown(object sender, EventArgs e)
        {
            FadeIn();
            FadeOut();
        }
        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fakeClose)
            {
                e.Cancel = true;
                FadeOut();
            }
            else
            {
                Properties.Settings.Default.Save();
            }
        }
        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle PScreen = Screen.PrimaryScreen.WorkingArea;
            if (e.Button == MouseButtons.Left)
            {
                Left = MousePosition.X - (Width / 2);
                if ((PScreen.Right) < (Left + Width))
                {
                    Left = PScreen.Width - Width;
                }
                Top = PScreen.Bottom - Height;
                FadeIn();
                Activate();
            }
        }
        private void exitSuperLauncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fakeClose = false;
            Close();
        }
        private void Launcher_Deactivate(object sender, EventArgs e)
        {
            FadeOut();
        }
        private void IconsBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LaunchFocusedItem();
        }
        private void LaunchFocusedItem()
        {
            FadeOut();
            Process.Start(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
        }
        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (string file in OpenFileDialog.FileNames)
            {
                addIcon(file);
                Properties.Settings.Default.fileList.Add(file);
                Properties.Settings.Default.Save();
            }
        }
        private void addShortcutStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileDialogOpen)
            {
                fileDialogOpen = true;
                OpenFileDialog.ShowDialog();
                fileDialogOpen = false;
            }
        }
        private void IconsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickMenu.Show();
                RightClickMenu.Left = MousePosition.X;
                RightClickMenu.Top = MousePosition.Y;
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.fileList.Remove(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            Properties.Settings.Default.Save();
            IconsBox.FocusedItem.Remove();
        }
        private void Launcher_Resize(object sender, EventArgs e)
        {
            Properties.Settings.Default.width = Width;
            Properties.Settings.Default.height = Height;
        }
        private void ElevateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAccountControl.Uac.ElevateAndQuit();
            Application.ExitThread();
        }
        private void RunAsStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRunAs();
        }
        private void ShowRunAs(int ErrorCode = 0)
        {
            VistaPrompt prompt = new VistaPrompt();
            prompt.ErrorCode = ErrorCode;
            prompt.Title = "Run As - Super Launcher";
            prompt.Message = "Please enter the credentials you would like Super Launcher to run as...";
            if (prompt.ShowDialog() == CredentialManagement.DialogResult.OK)
            {
                string username, domain;
                CredentialParser.ParseUserName(prompt.Username, out username, out domain);
                Process process = new Process();
                ProcessStartInfo startInfo = process.StartInfo;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.WorkingDirectory = Application.StartupPath;
                startInfo.Domain = domain;
                startInfo.UserName = username;
                startInfo.Password = prompt.SecurePassword;
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                try
                {
                    process.Start();
                    Application.ExitThread();
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    if (e.NativeErrorCode == 267)
                    {
                        TrayIcon.BalloonTipIcon = ToolTipIcon.Warning;
                        TrayIcon.BalloonTipText = "The credentials supplied does not have access to the currently running \"SuperLauncher\" executable file.";
                        TrayIcon.ShowBalloonTip(10000);
                    }
                    ShowRunAs(e.NativeErrorCode);
                }
            }
        }
        private void IconsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LaunchFocusedItem();
            }
        }
        private void pbBorderShadow_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentDynamic = new ResizeDynamic(
                e.Y <= 10,
                e.Y >= Height - 10,
                e.X <= 10,
                e.X >= Width - 10
            );
            if (CurrentDynamic.Top && CurrentDynamic.Left)
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (CurrentDynamic.Top && CurrentDynamic.Right)
            {
                Cursor = Cursors.SizeNESW;
            }
            else if (CurrentDynamic.Bottom && CurrentDynamic.Left)
            {
                Cursor = Cursors.SizeNESW;
            }
            else if (CurrentDynamic.Bottom && CurrentDynamic.Right)
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (CurrentDynamic.Top || CurrentDynamic.Bottom)
            {
                Cursor = Cursors.SizeNS;
            }
            else if (CurrentDynamic.Left || CurrentDynamic.Right)
            {
                Cursor = Cursors.SizeWE;
            }
            if (BorderMouseDown)
            {
                if (LockedDynamic.Top)
                {
                    Top = Cursor.Position.Y;
                    Height = BeforeReize.Bottom - Cursor.Position.Y;
                }
                if (LockedDynamic.Bottom)
                {
                    Height = Cursor.Position.Y - BeforeReize.Top;
                }
                if (LockedDynamic.Left)
                {
                    Left = Cursor.Position.X;
                    Width = BeforeReize.Right - Cursor.Position.X;
                }
                if (LockedDynamic.Right)
                {
                    Width = Cursor.Position.X - BeforeReize.Left;
                }
            }
        }
        private void pbBorderShadow_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void pbBorderShadow_MouseDown(object sender, MouseEventArgs e)
        {
            LockedDynamic = CurrentDynamic;
            pbBorderShadow.Hide();
            InnerBorderPanel.Hide();
            SuspendLayout();
            BorderMouseDown = true;
            BeforeReize = Bounds;
        }
        private void pbBorderShadow_MouseUp(object sender, MouseEventArgs e)
        {
            FadeOut();
            FadeIn();
            BorderMouseDown = false;
        }
    }
}