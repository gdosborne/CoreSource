namespace OzDB.Application.IO {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing.Text;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using OzDB.Application.Primitives;
    using OzDB.Application.Text;

    using Microsoft.WindowsAPICodePack.Shell;

    public static class File {
        private static readonly char[] illegalCharacters = { '/', '?', '<', '>', '\\', ':', '*', '|' };

        public static bool IsFileLocked(this string fileName) => new FileInfo(fileName).IsFileLocked();

        public static bool IsFileLocked(this FileInfo file) {
            try {
                using (var fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.None)) {
                    fs.Close();
                }
            }
            catch (IOException) {
                return true;
            }
            return false;
        }

        public static Dictionary<string, object> GetExtendedFileProperties(this string fileName) => new FileInfo(fileName).GetExtendedFileProperties();

        public static Dictionary<string, object> GetExtendedFileProperties(this FileInfo file) {
            var result = new Dictionary<string, object>();
            var f = ShellFile.FromFilePath(file.FullName);
            var name = default(string);
            var value = default(object);
            var index = 0;
            foreach (var item in f.Properties.DefaultPropertyCollection) {
                name = item.CanonicalName;
                if (string.IsNullOrEmpty(name)) {
                    name = item.CanonicalName;
                }
                index = 0;
                var theName = name;
                while (result.ContainsKey(theName)) {
                    index++;
                    theName = $"{name}{index}";
                }
                name = theName;
                value = item.ValueAsObject;
                result.Add(name, value);
            }
            return result;
        }

        public static FileInfo GetFilePartialName(string partialName, out bool hasMoreFiles) {
            var dirs = new string[] {
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Microsoft\\Windows\\Fonts"
            };
            hasMoreFiles = false;
            var result = default(FileInfo);
            for (var i = 0; i < dirs.Length; i++) {
                var d = new DirectoryInfo(dirs[i]);
                if (d == null || !d.Exists) {
                    continue;
                }

                var collection = new PrivateFontCollection();
                var allFiles = d.GetFiles("*.ttf");
                allFiles.ToList().ForEach(x => {
                    if (result != null) {
                        return;
                    }

                    collection.AddFontFile(x.FullName);
                    if (collection.Families.Any(y => y.Name.ContainsIgnoreCase(partialName))) {
                        result = x;
                        return;
                    }
                });
                if (result != null) {
                    break;
                }
            }
            return result;
        }

        public static FileAttributes? Attributes(string fileName) => !System.IO.File.Exists(fileName) ? (FileAttributes?)null : new FileInfo(fileName).Attributes;
        public static DateTime? CreationTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).CreationTime;
        public static DateTime? CreationTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).CreationTimeUtc;
        public static string Extension(string fileName) => new FileInfo(fileName).Extension;
        public static bool IsLegalFileName(string fileName) => !string.IsNullOrEmpty(fileName) && !System.IO.Path.GetFileName(fileName).ToCharArray().Any(x => illegalCharacters.Contains(x));
        public static DateTime? LastAccessTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).LastAccessTime;
        public static DateTime? LastAccessTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).LastAccessTimeUtc;
        public static DateTime? LastWriteTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).LastWriteTime;
        public static DateTime? LastWriteTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?)null : new FileInfo(fileName).LastWriteTimeUtc;
        public static FileStream OpenFile(string fileName, FileMode mode, FileAccess access, FileShare share) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode, access, share) : null);
        public static FileStream OpenFile(string fileName, FileMode mode, FileAccess access) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode, access) : null);
        public static FileStream OpenFile(string fileName, FileMode mode) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode) : null);
        public static List<string> ReadAllLines(string fileName, FileShare share) => ReadAllLines(fileName, share, true);
        public static long? Size(string fileName) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) || !System.IO.File.Exists(fileName) ? null : (fileName != null ? (long?)new FileInfo(fileName).Length : null);

        public static bool IsBinary(string fileName) {
            //this test is not conclusive but it's the best that can be done at present
            if (!System.IO.File.Exists(fileName)) {
                return false;
            }

            var bytes = GetBytes(new FileInfo(fileName), 500);
            var consecutiveNulls = 0;
            foreach (var c in bytes) {
                if (consecutiveNulls > 3) {
                    return true;
                }

                if (c != 0) {
                    consecutiveNulls = 0;
                }
                else {
                    consecutiveNulls++;
                }
            }
            return false;
        }

        public static byte[] GetBytes(this FileInfo value) {
            byte[] result;
            using (var br = new BinaryReader(value.OpenRead())) {
                result = new byte[Convert.ToInt32(br.BaseStream.Length)];
                br.Read(result, 0, result.Length);
            }
            return result;
        }

        public static byte[] GetBytes(this FileInfo value, int numberOfBytes) {
            byte[] result;
            using (var br = new BinaryReader(value.OpenRead())) {
                result = new byte[numberOfBytes];
                br.Read(result, 0, numberOfBytes);
            }
            return result;
        }

        public static string GetLegalFileName(string fileName, char illegalReplacement) {
            if (IsLegalFileName(fileName)) {
                return fileName;
            }

            var path = System.IO.Path.GetDirectoryName(fileName);
            var fName = System.IO.Path.GetFileName(fileName);
            var temp = string.Empty;
            fName?.ToCharArray()
                .ToList()
                .ForEach(x => {
                    if (illegalCharacters.Contains(x)) {
                        temp += illegalReplacement;
                    }
                    else {
                        temp += x;
                    }
                });
            return path != null ? System.IO.Path.Combine(path, temp) : null;
        }

        public static Version GetVersion(string fileName) {
            if (!System.IO.File.Exists(fileName)) {
                return null;
            }

            try {
                var fvi = FileVersionInfo.GetVersionInfo(fileName);
                return new Version(fvi.FileVersion.Replace(',', '.').ToString(CultureInfo.CurrentCulture));
            }
            catch {
                try {
                    var assy = Assembly.LoadFile(fileName);
                    return assy.GetName().Version;
                }
                catch {
                    return null;
                }
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static List<string> ReadAllLines(string fileName, FileShare share, bool includeBlankLines) {
            if (!System.IO.File.Exists(fileName)) {
                return null;
            }

            var result = new List<string>();
            using (var fs = OpenFile(fileName, FileMode.Open, FileAccess.Read, share))
            using (var sr = new StreamReader(fs)) {
                while (sr.Peek() != -1) {
                    var line = sr.ReadLine();
                    if (!includeBlankLines && string.IsNullOrEmpty(line)) {
                        continue;
                    }

                    result.Add(line);
                }
            }
            return result;
        }
    }
}
