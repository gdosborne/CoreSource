/* File="Directory"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using SysIO = System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Universal.Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Shell32;
using SHDocVw;
using OzFramework.Text;

namespace OzFramework.IO {
    public static class Directory {
        public static SysIO.FileAttributes? Attributes(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (SysIO.FileAttributes?)null : new SysIO.DirectoryInfo(directoryName).Attributes;
        public static DateTime? CreationTime(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).CreationTime;
        public static DateTime? CreationTimeUtc(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).CreationTimeUtc;
        public static string Extension(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? null : new SysIO.DirectoryInfo(directoryName).Extension;
        public static List<SysIO.FileInfo> GetFiles(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? null : new List<SysIO.FileInfo>(new SysIO.DirectoryInfo(directoryName).GetFiles());
        public static List<SysIO.FileInfo> GetFiles(string directoryName, string filter) =>
            !SysIO.Directory.Exists(directoryName) ? null : new List<SysIO.FileInfo>(new SysIO.DirectoryInfo(directoryName).GetFiles(filter));
        public static DateTime? LastWriteTime(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).LastWriteTime;
        public static DateTime? LastWriteTimeUtc(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).LastWriteTimeUtc;
        public static long? Size(string directoryName) => Size(directoryName, "*.*");
        public static long? Size(string directoryName, string filter) =>
            !SysIO.Directory.Exists(directoryName) ? (long?)null : GetFiles(directoryName, filter).ToList().Sum(x => x.Length);
        public static DateTime? LastAccessTimeUtc(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).LastAccessTimeUtc;
        public static DateTime? LastAccessTime(string directoryName) =>
            !SysIO.Directory.Exists(directoryName) ? (DateTime?)null : new SysIO.DirectoryInfo(directoryName).LastAccessTime;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr Hwnd);
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr Hwnd, int iCmdShow);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr Hwnd);

        public static void ShowInExplorer(this SysIO.DirectoryInfo directory) {
            var SW_RESTORE = 9;
            var exShell = (IShellDispatch2)Activator.CreateInstance(
                                Type.GetTypeFromProgID("Shell.Application"));
            var isDisplayed = false;
            foreach (ShellBrowserWindow w in (IShellWindows)exShell.Windows()) {
                if (w.Document is ShellFolderView) {
                    try {
                        var expPath = (string)w.Document.FocusedItem.Path;
                        if (!directory.Exists || !SysIO.Path.GetDirectoryName(expPath).EqualsIgnoreCase(directory.FullName))
                            continue;
                        if (IsIconic(new IntPtr(w.HWND))) {
                            w.Visible = false;
                            w.Visible = true;
                            ShowWindow(new IntPtr(w.HWND), SW_RESTORE);
                            isDisplayed = true;
                            break;
                        } else {
                            w.Visible = false;
                            w.Visible = true;
                            isDisplayed = true;
                            break;
                        }
                    } catch { }
                }
            }
            if (!isDisplayed) {
                var proc = new System.Diagnostics.Process {
                    StartInfo = new System.Diagnostics.ProcessStartInfo {
                        FileName = directory.FullName,
                        UseShellExecute = true,
                        Arguments = directory.FullName,
                        WindowStyle = ProcessWindowStyle.Normal
                    }
                };
                proc.Start();
            }
        }

        public static SysIO.DirectoryInfo CreateIfNotExist(this SysIO.DirectoryInfo value) {
            if (value.IsNull())
                return value;
            if (!value.Exists)
                value.Create();
            return value;
        }

        public static SysIO.DirectoryInfo CreateIfNotExist(this string value) {
            if (value.IsNull())
                return default;
            var d = new SysIO.DirectoryInfo(value);
            return d.CreateIfNotExist();
        }

        public static void DeleteAll(this IEnumerable<SysIO.FileInfo> value) => value.ToList().ForEach(x => x.Delete());
        public static void DeleteAll(this SysIO.FileInfo[] value) => value.DeleteAll();

        public static void CleanupDirectory(this SysIO.DirectoryInfo dirInfo, params string[] excludeExtensions) {
            if (dirInfo.Exists) {
                if (!excludeExtensions.Any()) {
                    dirInfo.GetFiles().DeleteAll();
                    return;
                }
                var files = dirInfo.GetFiles().Where(x => !excludeExtensions.ContainsIgnoreCase(x.Extension)).ToList();
                files.DeleteAll();
            }
        }

        public static long Size(this SysIO.DirectoryInfo value, string filter = "*.*", bool isRecursive = true) {
            if (value.IsNull() || !value.Exists)
                return 0;
            var result = value.GetFiles(filter).ToList().Sum(x => x.Length);
            if (!isRecursive || value.GetDirectories().Length == 0)
                return result;
            result += value.GetDirectories().Select(x => x.Size(filter, isRecursive)).Sum();
            return result;
        }

        private static System.Random random = default;
        public static string GetTempFileName(this SysIO.DirectoryInfo dInfo, string filePrefix, string extension, int length = 8, bool filenameOnly = false) {
            dInfo ??= new SysIO.DirectoryInfo(SysIO.Path.GetTempPath());
            return GetTempFile(dInfo.FullName, filePrefix, extension, length, filenameOnly);
        }

        private static string allowRandomCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GetTempFile(string directoryName, string filePrefix, string extension, int length = 8, bool filenameOnly = false) {
            random ??= new System.Random();
            if (SysIO.Directory.Exists(directoryName)) {
                var pfx = filePrefix.IsNull() ? string.Empty : $"{filePrefix}-";
                var val = $"{pfx}{random.NextString(length, allowRandomCharacters.ToCharArray())}";
                var ext = extension.IsNull() ? string.Empty : $".{extension}";
                var fName = $"{val}{ext}";
                var result = SysIO.Path.Combine(directoryName, fName);
                while (SysIO.File.Exists(result)) {
                    val = $"{filePrefix}-{random.NextString(length, allowRandomCharacters.ToCharArray())}";
                    result = SysIO.Path.Combine(directoryName, $"{val}{(extension.IsNull() ? string.Empty : $"{extension}")}");
                }
                return filenameOnly ? fName : result;
            }
            return null;
        }

        public static bool DeleteByAge(this SysIO.DirectoryInfo dInfo) {
            try {
                dInfo.Delete(true);
            } catch (System.Exception ex) {
                throw;
            }
            return true;
        }

        public async static Task<SysIO.DirectoryInfo[]> FindAllByNameAsync(this SysIO.DirectoryInfo parent, string directoryName, bool isRecursive = true) {
            var result = default(SysIO.DirectoryInfo[]);

            result = await SearchDirectoryAsync(parent, directoryName, isRecursive);

            result ??= Array.Empty<SysIO.DirectoryInfo>();
            return result;
        }

        private async static Task<SysIO.DirectoryInfo[]> SearchDirectoryAsync(SysIO.DirectoryInfo dir, string directoryName, bool isRecursive) {
            var result = new List<SysIO.DirectoryInfo>();

            if (dir.Name.EqualsIgnoreCase(directoryName)) {
                result.Add(dir);
            }
            //this is necessary for unc (network) paths because they can timeout or not be available
            var dirs = default(SysIO.DirectoryInfo[]);
            var st = Task.Factory.StartNew(() => {
                try {
                    dirs = dir.GetDirectories();
                } catch { }
            });
            st.Wait(TimeSpan.FromSeconds(5));
            if (dirs.IsNull()) {
                return result.ToArray();
            }

            if (isRecursive) {
                foreach (var sub in dirs) {
                    result.AddRange(await SearchDirectoryAsync(sub, directoryName, isRecursive));
                }
            }
            return result.ToArray();
        }
    }
}
