using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SuperLauncher.Classes
{
    public class RegWatcher
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int RegNotifyChangeKeyValue(IntPtr hKey, bool bWatchSubtree, uint dwNotifyFilter, IntPtr hEvent, bool fAsynchronous);
        /// <summary>
        /// This event is fired whenever the specified registry key changes.
        /// </summary>
        public event EventHandler Event;
        /// <summary>
        /// This class constructs an event that fires whenever the registry changes.
        /// </summary>
        /// <param name="RegistryHive">Registry hive to watch.</param>
        /// <param name="RegistryView">Registry view to watch.</param>
        /// <param name="Key">Registry key to watch.</param>
        /// <param name="NotifyFilter">Registry events the watch.</param>
        /// <param name="WatchSubtree">Should the RegWatcher class watch sub-keys as well?</param>
        public RegWatcher(RegistryHive RegistryHive, RegistryView RegistryView, string Key, NotifyFilter NotifyFilter = NotifyFilter.LastSet, bool WatchSubtree = false)
        {
            RegistryKey registry = RegistryKey.OpenBaseKey(RegistryHive, RegistryView);
            RegistryKey subKey = registry.OpenSubKey(Key, RegistryRights.Notify);
            Task.Run(() => {
                while(true)
                {
                    RegNotifyChangeKeyValue(subKey.Handle.DangerousGetHandle(), WatchSubtree, (uint)NotifyFilter, IntPtr.Zero, false);
                    Event(this, null);
                }
            });
        }
        public enum NotifyFilter : uint
        {
            /// <summary>
            /// Notify the caller if a subkey is added or deleted.
            /// </summary>
            Name = 0x00000001,
            /// <summary>
            /// Notify the caller of changes to the attributes of the key, such as the security descriptor information.
            /// </summary>
            Attributes = 0x00000002,
            /// <summary>
            /// Notify the caller of changes to a value of the key. This can include adding or deleting a value, or changing an existing value.
            /// </summary>
            LastSet = 0x00000004,
            /// <summary>
            /// Notify the caller of changes to the security descriptor of the key.
            /// </summary>
            Security = 0x00000008,
            /// <summary>
            /// Indicates that the lifetime of the registration must not be tied to the lifetime of the thread issuing the RegNotifyChangeKeyValue call.
            /// </summary>
            ThreadAgnostic = 0x10000000
        }
    }
}