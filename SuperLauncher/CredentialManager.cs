using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperLauncher
{
    public static class CredentialManager
    {
        [DllImport("advapi32.dll")]
        public static extern bool CredWriteA(CREDENTIAL Credential, CredWriteFlags Flags);
        [DllImport("advapi32.dll")]
        public static extern bool CredReadA(string TargetName, CredType Type, CredReadFlags Flags, out IntPtr Credential);
        public static bool CredReadA(string TargetName, CredType Type, CredReadFlags Flags, out CREDENTIAL Credential)
        {
            bool success =  CredReadA(TargetName, Type, Flags, out IntPtr credPtr);
            Credential = new();
            Marshal.PtrToStructure(credPtr, Credential);
            return success;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class CREDENTIAL
        {
            public CredFlags Flags;
            public CredType Type;
            public string TargetName;
            public string Comment;
            public FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public CredPersist Persist;
            public uint AttributeCount;
            public uint Attributes;
            public string TargetAlias;
            public string UserName;
            public string Password { 
                get 
                {
                    byte[] bytes = new byte[CredentialBlobSize];
                    for (int i = 0; i < CredentialBlobSize; i++)
                    {
                        bytes[i] = Marshal.ReadByte(CredentialBlob + i);
                    }
                    return Encoding.Unicode.GetString(bytes);
                }
                set
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(value);
                    CredentialBlobSize = (uint)bytes.Length;
                    CredentialBlob = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                }
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public class FILETIME
        {
            public uint LowDateTime;
            public uint HighDateTime;
        }
        public enum CredWriteFlags
        {
            NONE = 0x0,
            CRED_PRESERVE_CREDENTIAL_BLOB = 0x1
        }
        public enum CredReadFlags
        {
            NONE = 0x0
        }
        public enum CredFlags
        {
            CRED_FLAGS_PROMPT_NOW = 0x2,
            CRED_FLAGS_USERNAME_TARGET = 0x4
        }
        public enum CredType
        {
            CRED_TYPE_GENERIC = 0x1,
            CRED_TYPE_DOMAIN_PASSWORD = 0x2,
            CRED_TYPE_DOMAIN_CERTIFICATE = 0x3,
            CRED_TYPE_DOMAIN_VISIBLE_PASSWORD = 0x4,
            CRED_TYPE_GENERIC_CERTIFICATE = 0x5,
            CRED_TYPE_DOMAIN_EXTENDED = 0x6,
            CRED_TYPE_MAXIMUM = 0x7
        }
        public enum CredPersist
        {
            CRED_PERSIST_SESSION = 0x1,
            CRED_PERSIST_LOCAL_MACHINE = 0x2,
            CRED_PERSIST_ENTERPRISE = 0x3
        }
    }
}
