using System;
using System.Runtime.InteropServices;

namespace SuperLauncher
{
    public static class ComInterop
    {
        [ComImport, Guid("71F96385-DDD6-48D3-A0C1-AE06E8B055FB")]
        public class ExplorerBrowser { }
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("dfd3b6b5-c10c-4be9-85f6-a66969f402f6")]
        public interface IExplorerBrowser
        {
            public uint Initialize([In] nint hwndParent, [In] ref Win32Interop.RECT prc, [In, Optional] ref FOLDERSETTINGS pfs);
            public uint Destroy();
            public uint SetRect([In, Out] ref nint phdwp, [In] Win32Interop.RECT rcBrowser);
            public uint SetPropertyBag([In] string pszPropertyBag);
            public uint SetEmptyText([In] string pszEmptyText);
            public uint SetFolderSettings([In] ref FOLDERSETTINGS pfs);
            public uint Advise([In] IExplorerBrowserEvents psbe, [Out] out uint pdwCookie);
            public uint Unadvise([In] uint dwCookie);
            public uint SetOptions([In] EXPLORER_BROWSER_OPTIONS dwFlags);
            public uint GetOptions([Out] out EXPLORER_BROWSER_OPTIONS pdwFlags);
            public uint BrowseToIDList([In] nint pidl, [In] BROWSETOFLAGS uFlags);
            public uint BrowseToObjects([In] nint punk, [In] BROWSETOFLAGS uflags);
            public uint FillFromObject([In] nint punk, [In] EXPLORER_BROWSER_FILL_FLAGS dwFlags);
            public uint RemoveAll();
            public uint GetCurrentView([In] Guid riid, [Out] out nint ppv);
        }
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000114-0000-0000-c000-000000000046")]
        public interface IOleWindow
        {
            public uint GetWindow([Out] out nint phwnd);
            public uint ContextSensitiveHelp([In] bool fEnterMode);
        }
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E3-0000-0000-C000-000000000046")]
        public interface IShellView
        {
            public uint GetWindow([Out] out nint phwnd);
            public uint ContextSensitiveHelp([In] bool fEnterMode);
            public uint TranslateAccelerator([In] nint pmsg);
            public uint EnableModeless([In] bool fEnable);
            public uint UIActivate([In] uint uState);
            public uint Refresh();
            public uint CreateViewWindow([In] ref IShellView psvPrevious, [In] FOLDERSETTINGS pfs, [In] nint psb, [In] ref Win32Interop.RECT prcView, [Out] out nint phWnd);
            public uint DestroyViewWindow();
            public uint GetCurrentInfo([Out] out FOLDERSETTINGS pfs);
            public uint AddPropertySheetPages([In] uint dwReserved, [In] object pfn, [In] uint lparam);
            public uint SaveViewState();
            public uint SelectItem([In] nint pidlItem, [In] uint uFlags);
            public uint GetItemObject([In] uint uItem, [In] Guid riid, [Out] out nint ppv);
        }
        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("cde725b0-ccc9-4519-917e-325d72fab4ce")]
        public interface IFolderView
        {
            public uint GetCurrentViewMode([Out] out nint pViewMode);
            public uint SetCurrentViewMode([In] FOLDERVIEWMODE ViewMode);
            public uint GetFolder([In] Guid riid, [Out] out nint ppv);
            public uint Item([In] int iItemIndex, [Out] out nint ppidl);
            public uint ItemCount([In] SVGIO uFlags, [Out] out nint pcItems);
            public uint Items([In] SVGIO uFlags, [In] Guid riid, [Out] out nint ppv);
            public uint GetSelectionMarkedItem([Out] out nint piItem);
            public uint GetFocusedItem([Out] out int piItem);
            public uint GetItemPosition([In] nint pidl, [Out] nint ppt);
            public uint GetSpacing([Out][In] nint ppt);
            public uint GetDefaultSpacing([Out] out nint ppt);
            public uint GetAutoArrange();
            public uint SelectItem([In] int iItem, [In] SVSIF dwFlags);
            public uint SelectAndPositionItems([In] uint cidl, [In] nint apidl, [In] nint apt, [In] SVSIF dwFlags);
        }
        [ComImport, Guid("361bbdc7-e6ee-4e13-be58-58e2240c810f"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IExplorerBrowserEvents
        {
            [PreserveSig]
            uint OnNavigationPending([In] nint pidlFolder);
            [PreserveSig]
            uint OnViewCreated([In] nint psv);
            [PreserveSig]
            uint OnNavigationComplete([In] nint pidlFolder);
            [PreserveSig]
            uint OnNavigationFailed([In] nint pidlFolder);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct FOLDERSETTINGS
        {
            public FOLDERVIEWMODE ViewMode;
            public FOLDERFLAGS fFlags;
        }
        public enum FOLDERVIEWMODE : int
        {
            FVM_AUTO = -1,
            FVM_ICON = 1,
            FVM_SMALLICON = 2,
            FVM_LIST = 3,
            FVM_DETAILS = 4,
            FVM_THUMBNAIL = 5,
            FVM_TILE = 6,
            FVM_THUMBSTRIP = 7,
            FVM_CONTENT = 8
        }
        public enum FOLDERFLAGS : uint
        {
            FWF_NONE = 0,
            FWF_AUTOARRANGE = 0x1,
            FWF_ABBREVIATEDNAMES = 0x2,
            FWF_SNAPTOGRID = 0x4,
            FWF_OWNERDATA = 0x8,
            FWF_BESTFITWINDOW = 0x10,
            FWF_DESKTOP = 0x20,
            FWF_SINGLESEL = 0x40,
            FWF_NOSUBFOLDERS = 0x80,
            FWF_TRANSPARENT = 0x100,
            FWF_NOCLIENTEDGE = 0x200,
            FWF_NOSCROLL = 0x400,
            FWF_ALIGNLEFT = 0x800,
            FWF_NOICONS = 0x1000,
            FWF_SHOWSELALWAYS = 0x2000,
            FWF_NOVISIBLE = 0x4000,
            FWF_SINGLECLICKACTIVATE = 0x8000,
            FWF_NOWEBVIEW = 0x10000,
            FWF_HIDEFILENAMES = 0x20000,
            FWF_CHECKSELECT = 0x40000,
            FWF_NOENUMREFRESH = 0x80000,
            FWF_NOGROUPING = 0x100000,
            FWF_FULLROWSELECT = 0x200000,
            FWF_NOFILTERS = 0x400000,
            FWF_NOCOLUMNHEADER = 0x800000,
            FWF_NOHEADERINALLVIEWS = 0x1000000,
            FWF_EXTENDEDTILES = 0x2000000,
            FWF_TRICHECKSELECT = 0x4000000,
            FWF_AUTOCHECKSELECT = 0x8000000,
            FWF_NOBROWSERVIEWSTATE = 0x10000000,
            FWF_SUBSETGROUPS = 0x20000000,
            FWF_USESEARCHFOLDER = 0x40000000,
            FWF_ALLOWRTLREADING = 0x80000000
        }
        public enum EXPLORER_BROWSER_OPTIONS : uint
        {
            EBO_NONE = 0,
            EBO_NAVIGATEONCE = 0x1,
            EBO_SHOWFRAMES = 0x2,
            EBO_ALWAYSNAVIGATE = 0x4,
            EBO_NOTRAVELLOG = 0x8,
            EBO_NOWRAPPERWINDOW = 0x10,
            EBO_HTMLSHAREPOINTVIEW = 0x20,
            EBO_NOBORDER = 0x40,
            EBO_NOPERSISTVIEWSTATE = 0x80
        }
        public enum BROWSETOFLAGS : uint
        {
            SBSP_ABSOLUTE = 0x0000,
            SBSP_RELATIVE = 0x1000,
            SBSP_PARENT = 0x2000,
            SBSP_NAVIGATEBACK = 0x4000,
            SBSP_NAVIGATEFORWARD = 0x8000,
            SBSP_KEEPWORDWHEELTEXT = 0x00040000
        }
        public enum EXPLORER_BROWSER_FILL_FLAGS : uint
        {
            EBF_NONE = 0,
            EBF_SELECTFROMDATAOBJECT = 0x100,
            EBF_NODROPTARGET = 0x200
        }
        public enum SVGIO : uint
        {
            SVGIO_BACKGROUND = 0,
            SVGIO_SELECTION = 0x1,
            SVGIO_ALLVIEW = 0x2,
            SVGIO_CHECKED = 0x3,
            SVGIO_TYPE_MASK = 0xf,
            SVGIO_FLAG_VIEWORDER = 0x80000000
        }
        public enum SVSIF : uint
        {
            SVSI_DESELECT = 0,
            SVSI_SELECT = 0x1,
            SVSI_EDIT = 0x3,
            SVSI_DESELECTOTHERS = 0x4,
            SVSI_ENSUREVISIBLE = 0x8,
            SVSI_FOCUSED = 0x10,
            SVSI_TRANSLATEPT = 0x20,
            SVSI_SELECTIONMARK = 0x40,
            SVSI_POSITIONITEM = 0x80,
            SVSI_CHECK = 0x100,
            SVSI_CHECK2 = 0x200,
            SVSI_KEYBOARDSELECT = 0x401,
            SVSI_NOTAKEFOCUS = 0x40000000
        }
    }
}
