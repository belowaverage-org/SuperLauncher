using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SuperLauncher
{
    public partial class ShellView : Form
    {
        public ComInterop.IShellView ComShellView;
        public ComInterop.IFolderView ComFolderView;
        private readonly string InitialPath;
        private readonly ModernLauncherExplorerButtons MButtons = new();
        private ComInterop.IExplorerBrowser Browser;
        private uint AdviseCookie;
        private nint ParentFolder;
        private uint NavLogCount = 0;
        private uint NavLogPosition = 0;
        private bool BackPressed = false;
        private bool ForwardPressed = false;
        private string CurrentFolder = "";
        public ShellView(string InitialPath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}")
        {
            this.InitialPath = InitialPath;
            Init();
        }
        public void Init()
        {
            InitializeComponent();
            ElementHost modernButtonsEH = new()
            {
                Width = 121,
                Height = 28,
                Top = 3,
                Left = 3,
                Child = MButtons,
                Parent = this
            };
            MButtons.Back.Click += BtnBack_Click;
            MButtons.Forward.Click += BtnForward_Click;
            MButtons.Up.Click += BtnNavUp_Click;
            MButtons.Refresh.Click += Refresh_Click;
            Icon = Resources.logo;
        }
        private void Refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ComShellView.Refresh();
        }
        private void ShellView_Load(object sender, EventArgs e)
        {
            Browser = (ComInterop.IExplorerBrowser)new ComInterop.ExplorerBrowser();
            Browser.Advise(new ExplorerBrowserEvents(this), out AdviseCookie);
            Browser.SetOptions(ComInterop.EXPLORER_BROWSER_OPTIONS.EBO_NOBORDER | ComInterop.EXPLORER_BROWSER_OPTIONS.EBO_SHOWFRAMES);
            Browser.Initialize(Handle, new Win32Interop.RECT(), new ComInterop.FOLDERSETTINGS()
            {
                fFlags = ComInterop.FOLDERFLAGS.FWF_NONE,
                ViewMode = ComInterop.FOLDERVIEWMODE.FVM_AUTO
            });
            uint hresult = Win32Interop.SHParseDisplayName(InitialPath, nint.Zero, out nint ppidl, 0, out _);
            if (hresult == 0)
            {
                Browser.BrowseToIDList(ppidl, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
                Win32Interop.ILFree(ppidl);
            }
            else
            {
                Win32Interop.SHParseDisplayName("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}", nint.Zero, out ppidl, 0, out _);
                Browser.BrowseToIDList(ppidl, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
                Win32Interop.ILFree(ppidl);
            }
            ShellView_Resize(null, null);
        }
        private class ExplorerBrowserEvents : ComInterop.IExplorerBrowserEvents
        {
            public ShellView ShellView;
            public ExplorerBrowserEvents(ShellView shellView)
            {
                ShellView = shellView;
            }
            public uint OnNavigationPending([In] nint pidlFolder)
            {
                return 0;
            }
            public uint OnViewCreated([In] nint psv)
            {
                return 0;
            }
            public uint OnNavigationComplete([In] nint pidlFolder)
            {
                ShellView.Browser.GetCurrentView(Guid.Parse("000214E3-0000-0000-C000-000000000046"), out nint ppv);
                ShellView.ComShellView = (ComInterop.IShellView)Marshal.GetTypedObjectForIUnknown(ppv, typeof(ComInterop.IShellView));
                ShellView.Browser.GetCurrentView(Guid.Parse("cde725b0-ccc9-4519-917e-325d72fab4ce"), out ppv);
                ShellView.ComFolderView = (ComInterop.IFolderView)Marshal.GetTypedObjectForIUnknown(ppv, typeof(ComInterop.IFolderView));
                Win32Interop.SHGetNameFromIDList(pidlFolder, Win32Interop.SIGDN.SIGDN_NORMALDISPLAY, out string displayName);
                Win32Interop.SHGetNameFromIDList(pidlFolder, Win32Interop.SIGDN.SIGDN_DESKTOPABSOLUTEEDITING, out string absoluteName);
                Win32Interop.SHBindToParent(pidlFolder, Guid.Parse("000214E6-0000-0000-C000-000000000046"), out ppv, out _);
                ShellView.MButtons.Up.IsEnabled = !(ppv == ShellView.ParentFolder);
                ShellView.ParentFolder = ppv;
                ShellView.Text = displayName;
                ShellView.txtNav.Text = ShellView.CurrentFolder = absoluteName;
                if (ShellView.BackPressed)
                {
                    ShellView.NavLogPosition--;
                    ShellView.BackPressed = false;
                }
                else if (ShellView.ForwardPressed)
                {
                    ShellView.NavLogPosition++;
                    ShellView.ForwardPressed = false;
                }
                else
                {
                    if (ShellView.NavLogPosition != ShellView.NavLogCount)
                    {
                        ShellView.NavLogCount = ShellView.NavLogPosition + 1;
                    }
                    else
                    {
                        ShellView.NavLogCount++;
                    }
                    ShellView.NavLogPosition = ShellView.NavLogCount;
                }
                ShellView.MButtons.Back.IsEnabled = (ShellView.NavLogPosition > 1);
                ShellView.MButtons.Forward.IsEnabled = (ShellView.NavLogPosition < ShellView.NavLogCount);
                return 0;
            }
            public uint OnNavigationFailed([In] nint pidlFolder)
            {
                return 0;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            BackPressed = true;
            Browser.BrowseToIDList(nint.Zero, ComInterop.BROWSETOFLAGS.SBSP_NAVIGATEBACK);
        }
        private void BtnForward_Click(object sender, EventArgs e)
        {
            ForwardPressed = true;
            Browser.BrowseToIDList(nint.Zero, ComInterop.BROWSETOFLAGS.SBSP_NAVIGATEFORWARD);
        }
        private void BtnNavUp_Click(object sender, EventArgs e)
        {
            Browser.BrowseToObjects(ParentFolder, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
        }
        private void ShellView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                uint hresult = Win32Interop.SHParseDisplayName(txtNav.Text, nint.Zero, out nint ppidl, 0, out _);
                if (hresult == 0)
                {
                    Browser.BrowseToIDList(ppidl, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
                }
                else
                {
                    MessageBox.Show(Marshal.GetExceptionForHR((int)hresult).Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Win32Interop.ILFree(ppidl);
            }
        }
        private void ShellView_Resize(object sender, EventArgs e)
        {
            if (Browser == null) return;
            Browser.SetRect(nint.Zero, new Win32Interop.RECT()
            {
                top = 36,
                left = 0,
                bottom = Height - 39,
                right = Width - 16,
            });
        }
        private void ShellView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Browser.Unadvise(AdviseCookie);
            Browser.Destroy();
        }
        private void MIOpenWith_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string processPath = (string)e.ClickedItem.Tag;
            try
            {
                ComFolderView.GetFocusedItem(out int piItem);
                ComFolderView.Item(piItem, out nint ppidl);
                Win32Interop.SHGetNameFromIDList(ppidl, Win32Interop.SIGDN.SIGDN_FILESYSPATH, out string filePath);
                if (Directory.Exists(CurrentFolder)) filePath = Path.Combine(CurrentFolder, Path.GetFileName(filePath));
                if (filePath == null)
                {
                    Shared.StartProcess(processPath);
                }
                else
                {
                    Shared.StartProcess(processPath, [filePath]);
                }
            }
            catch
            {
                Shared.StartProcess(processPath);
            }
        }
        private void MIOpenWith_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                MIOpenWith.DropDownItems.Clear();
                foreach (string filePath in Settings.Default.FileList)
                {
                    if (!File.Exists(filePath)) continue;
                    ToolStripItem item = MIOpenWith.DropDownItems.Add(
                        Shared.ExtRemover(filePath),
                        Icon.ExtractAssociatedIcon(filePath).ToBitmap()
                    );
                    item.Tag = filePath;
                }
            }
            catch
            {
                string filePath = @"C:\Windows\System32\cmd.exe";
                ToolStripItem item = MIOpenWith.DropDownItems.Add(Shared.ExtRemover(filePath));
                item.Tag = filePath;
            }
        }
    }
}