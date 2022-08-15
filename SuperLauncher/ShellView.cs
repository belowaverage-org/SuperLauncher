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

            try
            {
                //Browser.Navigate(ShellObject.FromParsingName(InitialPath));
            }
            catch
            {
                //Browser.Navigate(ShellObject.FromParsingName("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"));
            }
        }
        private void ShellView_Load(object sender, EventArgs e)
        {
            Browser = (ComInterop.IExplorerBrowser)new ComInterop.ExplorerBrowser();
            Browser.Advise(new ExplorerBrowserEvents(), out uint test);
            Browser.SetOptions(ComInterop.EXPLORER_BROWSER_OPTIONS.EBO_NOBORDER | ComInterop.EXPLORER_BROWSER_OPTIONS.EBO_SHOWFRAMES);
            Browser.Initialize(Handle, new Win32Interop.RECT(), new ComInterop.FOLDERSETTINGS()
            {
                fFlags = ComInterop.FOLDERFLAGS.FWF_NONE,
                ViewMode = ComInterop.FOLDERVIEWMODE.FVM_AUTO
            });
            Win32Interop.SHGetDesktopFolder(out IntPtr ppshf);
            Browser.BrowseToObjects(ppshf, ComInterop.BROWSETOFLAGS.SBSP_ABSOLUTE);
            
        }
        private class ExplorerBrowserEvents : ComInterop.IExplorerBrowserEvents
        {
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
                return 0;
            }
            public uint OnNavigationFailed([In] IntPtr pidlFolder)
            {
                return 0;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Browser.NavigateLogLocation(NavigationLogDirection.Backward);
        }
        private void BtnForward_Click(object sender, EventArgs e)
        {
            //Browser.NavigateLogLocation(NavigationLogDirection.Forward);
        }
        /*
        private void Browser_NavigationComplete(object sender, NavigationCompleteEventArgs e)
        {
            Text = e.NewLocation.Name;
            if (e.NewLocation.IsFileSystemObject)
            {
                txtNav.Text = e.NewLocation.ParsingName;
            }
            else
            {
                txtNav.Text = e.NewLocation.GetDisplayName(DisplayNameType.Default);
            }
            MButtons.Up.IsEnabled = Directory.Exists(txtNav.Text) && Directory.GetParent(txtNav.Text) != null;
            MButtons.Back.IsEnabled = Browser.NavigationLog.CanNavigateBackward;
            MButtons.Forward.IsEnabled = Browser.NavigationLog.CanNavigateForward;
        }
        */
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
    }
}
