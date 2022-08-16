using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.IO;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    public partial class ShellView : Form
    {
        private readonly string InitialPath;
        private readonly ModernLauncherExplorerButtons MButtons = new();
        private ComInterop.IExplorerBrowser Browser;
        private uint AdviseCookie;
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
                Width = 90,
                Height = 30,
                Top = 2,
                Left = 3,
                Child = MButtons,
                Parent = this
            };
            MButtons.Back.Click += BtnBack_Click;
            MButtons.Forward.Click += BtnForward_Click;
            MButtons.Up.Click += BtnNavUp_Click;
            Icon = Resources.logo;
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
            uint hresult = Win32Interop.SHParseDisplayName(InitialPath, IntPtr.Zero, out IntPtr ppidl, 0, out _);
            if (hresult == 0)
            {
                Browser.BrowseToIDList(ppidl, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
                Win32Interop.ILFree(ppidl);
            }
            else
            {
                Win32Interop.SHParseDisplayName("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}", IntPtr.Zero, out ppidl, 0, out _);
                Browser.BrowseToIDList(ppidl, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
                Win32Interop.ILFree(ppidl);
            }
        }
        private class ExplorerBrowserEvents : ComInterop.IExplorerBrowserEvents
        {
            public ShellView ShellView;
            public ExplorerBrowserEvents(ShellView shellView)
            {
                ShellView = shellView;
            }
            public uint OnNavigationPending([In] IntPtr pidlFolder)
            {
                return 0;
            }
            public uint OnViewCreated([In] IntPtr psv)
            {
                return 0;
            }
            public uint OnNavigationComplete([In] IntPtr pidlFolder)
            {
                Win32Interop.SHGetNameFromIDList(pidlFolder, Win32Interop.SIGDN.SIGDN_DESKTOPABSOLUTEEDITING, out string ppszName);

                ShellView.txtNav.Text = ppszName;

                ShellView.MButtons.Back.IsEnabled = true;
                ShellView.MButtons.Forward.IsEnabled = true;
                
                return 0;
            }
            public uint OnNavigationFailed([In] IntPtr pidlFolder)
            {
                return 0;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Browser.BrowseToIDList(IntPtr.Zero, ComInterop.BROWSETOFLAGS.SBSP_NAVIGATEBACK);
        }
        private void BtnForward_Click(object sender, EventArgs e)
        {
            Browser.BrowseToIDList(IntPtr.Zero, ComInterop.BROWSETOFLAGS.SBSP_NAVIGATEFORWARD);
        }
        private void BtnNavUp_Click(object sender, EventArgs e)
        {
            //Browser.Navigate(ShellObject.FromParsingName(Directory.GetParent(txtNav.Text).FullName));
        }
        private void ShellView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                try
                {
                    //Browser.Navigate(ShellObject.FromParsingName(txtNav.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Not a valid path / directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void ShellView_Resize(object sender, EventArgs e)
        {
            if (Browser == null) return;
            Browser.SetRect(IntPtr.Zero, new Win32Interop.RECT() 
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
    }
}
