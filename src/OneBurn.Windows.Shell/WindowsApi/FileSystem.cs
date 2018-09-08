using System;
using System.Runtime.InteropServices;

namespace OneBurn.Windows.Shell.WindowsApi
{
    public static class FileSystem
    {
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public static string GetTypeFriendlyName(string filePath)
        {
            var info = new SHFILEINFO();
            const uint dwFileAttributes = FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
            const uint uFlags = SHGFI.SHGFI_TYPENAME | SHGFI.SHGFI_USEFILEATTRIBUTES;

            SHGetFileInfo(filePath, dwFileAttributes, ref info, (uint) Marshal.SizeOf(info), uFlags);

            return info.szTypeName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public readonly IntPtr hIcon;
            public readonly int iIcon;
            public readonly uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public readonly string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public readonly string szTypeName;
        }

        private static class FILE_ATTRIBUTE
        {
            public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        }

        private static class SHGFI
        {
            public const uint SHGFI_TYPENAME = 0x000000400;
            public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        }
    }
}