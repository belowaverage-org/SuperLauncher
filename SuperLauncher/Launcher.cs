using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using CredentialManagement;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SuperLauncher
{
    public partial class Launcher : Form
    {
        public ImageList imageList = new();
        public bool fakeClose = true;
        public bool saving = true;
        private bool fileDialogOpen = false;
        private ResizeHandles CurrentRH = ResizeHandles.None;
        private bool MouseIsDown = false;
        private readonly int HandleWidth = 5;
        private readonly int BottomRightMargin = 5;
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
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 0) //WM_HOTKEY //ALT + S
            {
                CenterToScreen();
                FadeIn();
            }
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 1) //WM_HOTKEY //ALT + E
            {
                TcMiOpenExplorer_Click(null, null);
            }
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 2) //WM_HOTKEY //ALT + R
            {
                TcMIOpenRun_Click(null, null);
            }
            if (m.Msg == 0x0100 && m.WParam.ToInt32() == 0x1B) //WM_KEYDOWN //ESC
            {
                FadeOut();
            }
            if (m.Msg == 0x6) //WM_ACTIVATE
            {
                MARGINS margins;
                margins.cxLeftWidth = 1;
                margins.cxRightWidth = 1;
                margins.cyBottomHeight = 1;
                margins.cyTopHeight = 1;
                _ = DwmExtendFrameIntoClientArea(Handle, ref margins);
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
            bool isElevated = UserAccountControl.Uac.IsProcessElevated();
            ConfigHelper configHelper = new();
            var initialWidth = Settings.Default.Width;
            var initialHeight = Settings.Default.Height;
            InitializeComponent();
            RegHandleEvents(Controls);
            Icon = Resources.logo;
            TrayIcon.Icon = new Icon(Resources.logo, 16, 16);
            TcMiSuperLauncher.Click += TcMiSuperLauncher_Click;
            TcMiElevate.Click += TcMiElevate_Click;
            TcMiRunAs.Click += TcMiRunAs_Click;
            TcMiAddShortcut.Click += TcMiAddShortcut_Click;
            TcMiOpenExplorer.Click += TcMiOpenExplorer_Click;
            TcMiViewConfig.Click += TcMiConfig_Click;
            TcMiSettings.Click += TcMiSettings_Click;
            TcMiExit.Click += TcMiExit_Click;
            RicMiRemove.Click += RicMiRemove_Click;
            if (isElevated)
            {
                ShieldIcon.Visible = true;
                UserLabel.Location = new Point(113, 5);
                UserLabel.Size = new Size(238, 22);
                TcMiElevate.Text = "Elevated";
                TcMiElevate.Enabled = false;
            }
            UserLabel.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            imageList.ImageSize = new Size(32, 32);
            Width = initialWidth;
            Height = initialHeight;
            if (Settings.Default.FileList == null)
            {
                Settings.Default.FileList = new AppListStringCollection();
            }
            foreach (string file in Settings.Default.FileList)
            {
                if (File.Exists(file))
                {
                    AddIcon(file);
                }
            }
            if (!isElevated)
            {
                if (configHelper.HasRunAsCredential())
                {
                    // I can't directly start an elevated session as a different user. Something about "security"
                    // Whatever
                    // So if we're starting elevated skip checking for an alternate user
                    ShowRunAs(0, configHelper);
                }
                if (configHelper.AutoElevate)
                {
                    // Funny story, if you have autoElevate on and don't check if you're already elevated
                    // You get this awesome infinite loop of it restarting until you restart the machine
                    TcMiElevate_Click(null, null);
                }
            }
            Win32Interop.RegisterHotKey(Handle, 0, 0x1 | 0x4000, 0x53); //Register Hot Key ALT + S
            Win32Interop.RegisterHotKey(Handle, 1, 0x1 | 0x4000, 0x45); //Register Hot Key ALT + E
            Win32Interop.RegisterHotKey(Handle, 2, 0x1 | 0x4000, 0x52); //Register Hot Key ALT + R
        }
        public void FadeIn()
        {
            Show();
            Activate();
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
        public void AddIcon(string filePath)
        {
            FileInfo fileInfo = new(filePath);
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
            else if(saving)
            {
                e.Cancel = true;
                Settings.Default.Save();
                saving = false;
                Close();
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
                ProcessStartInfo psi = new()
                {
                    FileName = ((IconData)IconsBox.FocusedItem.Tag).fileLocation,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception) { } //User canceled a UAC probbably...
        }
        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            foreach (string file in OpenFileDialog.FileNames)
            {
                AddIcon(file);
                Settings.Default.FileList.Add(file);
                Settings.Default.Save();
            }
        }
        private void RicMiRemove_Click(object sender, EventArgs e)
        {
            if (IconsBox.FocusedItem == null) return;
            Settings.Default.FileList.Remove(((IconData)IconsBox.FocusedItem.Tag).fileLocation);
            Settings.Default.Save();
            IconsBox.FocusedItem.Remove();
        }
        private void Launcher_Resize(object sender, EventArgs e)
        {
            Settings.Default.Width = Width;
            Settings.Default.Height = Height;
        }
        private void ShowRunAs(int ErrorCode = 0, ConfigHelper configHelper = null)
        {
            Task.Run(() => {
                VistaPrompt prompt = new();
                prompt.ErrorCode = ErrorCode;
                prompt.Title = "Super Launcher - Run-As";
                prompt.Message = "Please enter the credentials you would like Super Launcher to run as...";
                if (configHelper != null && configHelper.HasRunAsCredential())
                {
                    prompt.Username = configHelper.UserName;
                    prompt.Domain = configHelper.Domain;
                }
                if (prompt.ShowDialog() == CredentialManagement.DialogResult.OK)
                {
                    CredentialParser.ParseUserName(prompt.Username, out string username, out string domain);
                    Process process = new();
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
                        fakeClose = false;
                        Invoke(new Action(() => {
                            Close();
                        }));
                    }
                    catch (Win32Exception e)
                    {
                        if (e.NativeErrorCode == 267)
                        {
                            int result = (int)MessageBox.Show("The credentials supplied do not have access to the currently running \"Super Launcher\" executable file.\n\nConsider moving Super Launcher to a location that this account has access too.", "Super Launcher: Permission Error.", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            if (result == 2) return; //If result equals "Cancel". https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.dialogresult?view=net-5.0
                        }
                        ShowRunAs(e.NativeErrorCode);
                    }
                }
            });
        }
        private void IconsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LaunchFocusedItem();
            }
        }
        private void TcMiExit_Click(object sender, EventArgs e)
        {
            fakeClose = false;
            Close();
        }
        private void TcMiAddShortcut_Click(object sender, EventArgs e)
        {
            if (!fileDialogOpen)
            {
                fileDialogOpen = true;
                OpenFileDialog.ShowDialog();
                fileDialogOpen = false;
            }
        }
        private void TcMiRunAs_Click(object sender, EventArgs e)
        {
            ShowRunAs();
        }
        private void TcMiElevate_Click(object sender, EventArgs e)
        {
            Task.Run(() => {
                ProcessStartInfo elevatedProcStartInfo = new();
                elevatedProcStartInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
                elevatedProcStartInfo.Verb = "RunAs";
                elevatedProcStartInfo.UseShellExecute = true;
                Process elevatedProcess = new();
                elevatedProcess.StartInfo = elevatedProcStartInfo;
                try
                {
                    elevatedProcess.Start();
                    fakeClose = false;
                    Invoke(new Action(() => {
                        Close();
                    }));
                }
                catch (Exception) { }
            });
        }
        private void TcMiSuperLauncher_Click(object sender, EventArgs e)
        {
            new SuperLauncherCommon.Splash().ShowDialog();
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
                if (posOff.Y <= HandleWidth) CurrentRH |= ResizeHandles.Top;
                if (posOff.X > Width - HandleWidth) CurrentRH |= ResizeHandles.Right;
                if (posOff.Y > Height - HandleWidth) CurrentRH |= ResizeHandles.Bottom;
                if (posOff.X <= HandleWidth) CurrentRH |= ResizeHandles.Left;
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
        private void TcMiOpenExplorer_Click(object sender, EventArgs e)
        {
            ShellHost sh = new();
            sh.Show();
        }
        private void TcMiConfig_Click(object sender, EventArgs e)
        {
            Process.Start("OpenWith.exe", "\"" + Settings.Default.configPath + "\"");
        }
        private void TcMiSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new();
            form.ShowDialog();
        }
        private void Launcher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) FadeOut();
        }
        private void TcMIOpenRun_Click(object sender, EventArgs e)
        {
            Run run = new();
            run.ShowDialog();
        }
    }
}