using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Specialized;
using CredentialManagement;
using System.Runtime.InteropServices;
using SuperLauncher.Properties;
using System.Configuration;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        public ImageList imageList = new ImageList();
        public bool fakeClose = true;
        private bool fileDialogOpen = false;
        private ResizeHandles CurrentRH = ResizeHandles.None;
        private bool MouseIsDown = false;
        private int HandleWidth = 5;
        private int BottomRightMargin = 5;
        [DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyBottomHeight;
            public int cyTopHeight;
        }
        private enum AnimateWindowFlags : uint
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }
        private enum ResizeHandles
        {
            None = 0,
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8,
            TopRight = 3,
            TopLeft = 9,
            BottomRight = 6,
            BottomLeft = 12
        }
        protected override void WndProc(ref Message m)
        {
            IntPtr lRet = (IntPtr)0;
            if (m.Msg == 0x6) //WM_ACTIVATE
            {
                MARGINS margins;
                margins.cxLeftWidth = 1;
                margins.cxRightWidth = 1;
                margins.cyBottomHeight = 1;
                margins.cyTopHeight = 1;
                DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
            if (m.Msg == 0x1) //WM_CREATE
            {
                SetWindowPos(Handle, IntPtr.Zero, Left, Top, Width, Height, 0x20); //SWP_FRAMECHANGED
            }
            if (m.Msg == 0x83 && m.WParam == (IntPtr)1) //WM_NCCALCSIZE
            {
                return;
            }
            if (m.Msg == 0x84) //WM_NCHITTEST
            {

            }
            base.WndProc(ref m);
        }
        public Launcher()
        {
            var initialWidth = Settings.Default.width;
            var initialHeight = Settings.Default.height;
            InitializeComponent();
            RegHandleEvents(Controls);
            Icon = TrayIcon.Icon = new Icon(Resources.logo, 16, 16);
            miSuperLauncher.SetMenuItemBitmap(Resources.logo_16);
            miAddShortcut.SetMenuItemBitmap(Resources.shortcut);
            miElevate.SetMenuItemBitmap(Resources.shield);
            miExplorer.SetMenuItemBitmap(Resources.explorer);
            if (UserAccountControl.Uac.IsProcessElevated())
            {
                ShieldIcon.Visible = true;
                UserLabel.Location = new Point(113, 5);
                UserLabel.Size = new Size(238, 22);
                miElevate.Text = "Elevated";
                miElevate.Enabled = false;
            }
            UserLabel.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            imageList.ImageSize = new Size(32, 32);
            Width = initialWidth;
            Height = initialHeight;
            if (Settings.Default.fileList == null)
            {
                Settings.Default.fileList = new StringCollection();
            }
            foreach (string file in Settings.Default.fileList)
            {
                if (File.Exists(file))
                {
                    addIcon(file);
                }
            }
        }
        public void FadeIn()
        {
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
                Settings.Default.Save();
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
                    Left = (PScreen.Width - Width) - BottomRightMargin;
                }
                Top = (PScreen.Bottom - Height) - BottomRightMargin;
                FadeIn();
                Activate();
            }
            if (e.Button == MouseButtons.Right)
            {
                Opacity = 0;
                Location = MousePosition;
                Show();
                Activate();
                TrayMenu.Show(this, new Point(0, 0));
                Hide();
                Opacity = 1;
            }
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
            try
            {
                Process.Start(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            }
            catch (Exception) { } //User canceled a UAC probbably...
        }
        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (string file in OpenFileDialog.FileNames)
            {
                addIcon(file);
                Settings.Default.fileList.Add(file);
                Settings.Default.Save();
            }
        }
        private void IconsBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickMenu.Show(this, e.Location);
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.fileList.Remove(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            Settings.Default.Save();
            IconsBox.FocusedItem.Remove();
        }
        private void Launcher_Resize(object sender, EventArgs e)
        {
            Settings.Default.width = Width;
            Settings.Default.height = Height;
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
        private void miExit_Click(object sender, EventArgs e)
        {
            fakeClose = false;
            Close();
        }
        private void miAddShortcut_Click(object sender, EventArgs e)
        {
            if (!fileDialogOpen)
            {
                fileDialogOpen = true;
                OpenFileDialog.ShowDialog();
                fileDialogOpen = false;
            }
        }
        private void miRunAs_Click(object sender, EventArgs e)
        {
            ShowRunAs();
        }
        private void miElevate_Click(object sender, EventArgs e)
        {
            ProcessStartInfo elevatedProcStartInfo = new ProcessStartInfo();
            elevatedProcStartInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            elevatedProcStartInfo.Verb = "RunAs";
            Process elevatedProcess = new Process();
            elevatedProcess.StartInfo = elevatedProcStartInfo;
            try
            {
                elevatedProcess.Start();
                fakeClose = false;
                Close();
            }
            catch (Exception) { }
        }
        private void TrayMenu_Popup(object sender, EventArgs e)
        {
            ((Menu)sender).DrawMenuItemBitmaps();
        }
        private void miSuperLauncher_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        private void RegHandleEvents(Control.ControlCollection ControlCollection)
        {
            foreach (Control control in ControlCollection)
            {
                Console.WriteLine(control);
                control.MouseDown += Launcher_MouseDown;
                control.MouseUp += Launcher_MouseUp;
                control.MouseMove += Launcher_MouseMove;
                RegHandleEvents(control.Controls);
            }
        }
        private void Launcher_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
        }
        private void Launcher_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }
        private void Launcher_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = Cursor.Position;
            Point posOff = Cursor.Position;
            posOff.X -= Left;
            posOff.Y -= Top;
            if (!MouseIsDown)
            {
                CurrentRH = ResizeHandles.None;
                if (posOff.Y <= HandleWidth) CurrentRH = CurrentRH | ResizeHandles.Top;
                if (posOff.X > Width - HandleWidth) CurrentRH = CurrentRH | ResizeHandles.Right;
                if (posOff.Y > Height - HandleWidth) CurrentRH = CurrentRH | ResizeHandles.Bottom;
                if (posOff.X <= HandleWidth) CurrentRH = CurrentRH | ResizeHandles.Left;
                if (CurrentRH == ResizeHandles.None) Cursor = Cursors.Arrow;
                if (CurrentRH == ResizeHandles.Left || CurrentRH == ResizeHandles.Right) Cursor = Cursors.SizeWE;
                if (CurrentRH == ResizeHandles.Top || CurrentRH == ResizeHandles.Bottom) Cursor = Cursors.SizeNS;
                if (CurrentRH == ResizeHandles.TopLeft || CurrentRH == ResizeHandles.BottomRight) Cursor = Cursors.SizeNWSE;
                if (CurrentRH == ResizeHandles.TopRight || CurrentRH == ResizeHandles.BottomLeft) Cursor = Cursors.SizeNESW;
                return;
            }
            if ((CurrentRH & ResizeHandles.Top) == ResizeHandles.Top)
            {
                int newHeight = (Top + Height) - pos.Y;
                if (newHeight > MinimumSize.Height)
                {
                    Height = newHeight;
                    Top = pos.Y;
                }
            }
            if ((CurrentRH & ResizeHandles.Right) == ResizeHandles.Right)
            {
                Width = pos.X - Left;
            }
            if ((CurrentRH & ResizeHandles.Bottom) == ResizeHandles.Bottom)
            {
                Height = pos.Y - Top;
            }
            if ((CurrentRH & ResizeHandles.Left) == ResizeHandles.Left)
            {
                int newWidth = (Left + Width) - pos.X;
                if (newWidth > MinimumSize.Width)
                {
                    Width = newWidth;
                    Left = pos.X;
                }
            }
        }
        private void miExplorer_Click(object sender, EventArgs e)
        {
            ShellHost sh = new ShellHost();
            sh.Show();
        }
        private void miConfig_Click(object sender, EventArgs e)
        {
            Process.Start(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
        }
    }
}