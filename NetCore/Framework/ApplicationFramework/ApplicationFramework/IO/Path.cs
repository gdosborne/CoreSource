using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.AppFramework.IO {
    public static class Path {
        public const string UncStart = @"\\";

        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);

        public static string GetRelativePathToFolder(this DirectoryInfo value, string path) => value.FullName.GetRelativePathToFolder(path);
        public static string GetRelativePathToFolder(this DirectoryInfo value, DirectoryInfo toDir) => value.GetRelativePathToFolder(toDir.FullName);
        public static DriveType GetDriveType(string value) => GetDriveInfo(value)?.DriveType ?? DriveType.Unknown;
        public static bool IsNetworkDrive(string value) => GetDriveType(value) == DriveType.Network;
        public static string ResolveToUnc(string value) => value.StartsWith(UncStart) ? value : value.StartsWith(ResolveToRootUnc(value)) ? value : value.Replace(GetDriveLetter(value), ResolveToRootUnc(value));
        public static string GetTempFile(string directory) => GetTempFile(directory, "_");
        public static string GetTempFile(string directory, string prefix) => GetTempFile(directory, prefix, "tmp");
        public static string GetTempFile(string directory, string prefix, string extension) {
            var result = default(string);
            if (!System.IO.Directory.Exists(directory)) {
                return result;
            }

            var rnd = new Random();
            var btResult = new List<byte>();

            while (string.IsNullOrEmpty(result) || System.IO.File.Exists(result)) {
                btResult.Clear();
                for (var i = 0; i < 10; i++) {
                    btResult.Add(Convert.ToByte(rnd.Next('A', 'z')));
                }
                result = System.IO.Path.Combine(directory, $"{prefix}{Convert.ToBase64String(btResult.ToArray()).TrimEnd('=')}.{extension}");
            }
            return result;
        }

        public static string GetRelativePathToFolder(this string value, string path) {
            var u1 = new Uri(value);
            var u2 = new Uri(path);
            var result = u1.MakeRelativeUri(u2).ToString();
            return result;
        }

        public static string GetUncPath(string value) {
            var sb = new StringBuilder(512);
            var size = sb.Capacity;
            var error = WNetGetConnection($"{value}:", sb, ref size);
            if (error != 0) {
                throw new Win32Exception(error, "WNetGetConnection failed");
            }

            return sb.ToString();
        }

        public static DriveInfo GetDriveInfo(string value) {
            if (value.StartsWith(UncStart, StringComparison.CurrentCulture)) {
                return null;
            }

            var result = DriveInfo
                .GetDrives()
                .FirstOrDefault(x => value.StartsWith(x.Name, StringComparison.CurrentCultureIgnoreCase));
            return result;
        }

        public static string GetDriveLetter(string value) {
            if (value.StartsWith(UncStart, StringComparison.CurrentCulture)) {
                return null;
            }

            var di = GetDriveInfo(value);
            return di?.RootDirectory.FullName.Replace(System.IO.Path.DirectorySeparatorChar.ToString(CultureInfo.CurrentCulture), string.Empty).ToUpper(CultureInfo.CurrentCulture);
        }

        public static string ResolveToRootUnc(string value) {
            if (value.StartsWith(UncStart)) {
                return System.IO.Directory.GetDirectoryRoot(value);
            }

            var mo = new ManagementObject();
            var driveletter = GetDriveLetter(value);
            mo.Path = new ManagementPath($"Win32_LogicalDisk='{driveletter}'");
            var driveType = Convert.ToUInt32(mo["DriveType"]);
            var networkRoot = Convert.ToString(mo["ProviderName"]);
            return driveType == 4 ? networkRoot : driveletter + System.IO.Path.DirectorySeparatorChar;
        }
    }
}