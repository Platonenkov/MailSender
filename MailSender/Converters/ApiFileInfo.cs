using System;
using System.Runtime.InteropServices;

namespace MailSender.Converters
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ApiFileInfo
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };
}