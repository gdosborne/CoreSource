using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;

namespace GregOsborne.Application.IO {
    public static class Path {
        private const string UncStart = @"\\";

        public static string GetRelativePathToFolder(this string value, string path) {
            var u1 = new Uri(value);
            var u2 = new Uri(path);
            var result = u1.MakeRelativeUri(u2).ToString();
            return result;
        }

        public static string GetRelativePathToFolder(this DirectoryInfo value, string path) {
            return value.FullName.GetRelativePathToFolder(path);
        }

        public static string GetRelativePathToFolder(this DirectoryInfo value, DirectoryInfo toDir) {
            return value.GetRelativePathToFolder(toDir.FullName);
        }

        public static DriveInfo GetDriveInfo(string value) {
            if (value.StartsWith(UncStart, StringComparison.CurrentCulture))
                return null;
            var result = DriveInfo
                .GetDrives()
                .FirstOrDefault(x => value.StartsWith(x.Name, StringComparison.CurrentCultureIgnoreCase));
            return result;
        }

        public static string GetDriveLetter(string value) {
            if (value.StartsWith(UncStart, StringComparison.CurrentCulture))
                return null;
            var di = GetDriveInfo(value);
            return di?.RootDirectory.FullName.Replace(System.IO.Path.DirectorySeparatorChar.ToString(CultureInfo.CurrentCulture), string.Empty).ToUpper(CultureInfo.CurrentCulture);
        }

        public static DriveType GetDriveType(string value) {
            var result = GetDriveInfo(value);
            return result?.DriveType ?? DriveType.Unknown;
        }

        public static bool IsNetworkDrive(string value) {
            return GetDriveType(value) == DriveType.Network;
        }

        public static string ResolveToRootUnc(string value) {
            if (value.StartsWith(UncStart))
                return System.IO.Directory.GetDirectoryRoot(value);
            var mo = new ManagementObject();
            var driveletter = GetDriveLetter(value);
            mo.Path = new ManagementPath($"Win32_LogicalDisk='{driveletter}'");
            var driveType = Convert.ToUInt32(mo["DriveType"]);
            var networkRoot = Convert.ToString(mo["ProviderName"]);
            return driveType == 4 ? networkRoot : driveletter + System.IO.Path.DirectorySeparatorChar;
        }

        public static string ResolveToUnc(string value) {
            if (value.StartsWith(UncStart))
                return value;
            var root = ResolveToRootUnc(value);
            return value.StartsWith(root) ? value : value.Replace(GetDriveLetter(value), root);
        }

    }
}