using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GregOsborne.Application.IO {
    public static class File {
        private static readonly char[] _illegalCharacters = {'/', '?', '<', '>', '\\', ':', '*', '|'};

        public static FileAttributes? Attributes(string fileName) => !System.IO.File.Exists(fileName) ? (FileAttributes?) null : new FileInfo(fileName).Attributes;
        public static DateTime? CreationTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).CreationTime;
        public static DateTime? CreationTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).CreationTimeUtc;
        public static string Extension(string fileName) => new FileInfo(fileName).Extension;
        public static bool IsLegalFileName(string fileName) => !string.IsNullOrEmpty(fileName) && !System.IO.Path.GetFileName(fileName).ToCharArray().Any(x => _illegalCharacters.Contains(x));
        public static DateTime? LastAccessTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).LastAccessTime;
        public static DateTime? LastAccessTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).LastAccessTimeUtc;
        public static DateTime? LastWriteTime(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).LastWriteTime;
        public static DateTime? LastWriteTimeUtc(string fileName) => !System.IO.File.Exists(fileName) ? (DateTime?) null : new FileInfo(fileName).LastWriteTimeUtc;
        public static FileStream OpenFile(string fileName, FileMode mode, FileAccess access, FileShare share) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode, access, share) : null);
        public static FileStream OpenFile(string fileName, FileMode mode, FileAccess access) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode, access) : null);
        public static FileStream OpenFile(string fileName, FileMode mode) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) ? null : (fileName != null ? new FileInfo(fileName).Open(mode) : null);
        public static List<string> ReadAllLines(string fileName, FileShare share) => ReadAllLines(fileName, share, true);
        public static long? Size(string fileName) => string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName) || !System.IO.File.Exists(fileName) ? null : (fileName != null ? (long?) new FileInfo(fileName).Length : null);

        public static bool IsBinary(string fileName) {
            //this test is not conclusive but it's the best that can be done at present
            if (!System.IO.File.Exists(fileName)) return false;
            var bytes = GetBytes(new FileInfo(fileName), 500);
            var consecutiveNulls = 0;
            foreach (var c in bytes) {
                if (consecutiveNulls > 3) return true;
                if (c != 0)
                    consecutiveNulls = 0;
                else consecutiveNulls++;
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
            if (IsLegalFileName(fileName))
                return fileName;
            var path = System.IO.Path.GetDirectoryName(fileName);
            var fName = System.IO.Path.GetFileName(fileName);
            var temp = string.Empty;
            fName?.ToCharArray()
                .ToList()
                .ForEach(x => {
                    if (_illegalCharacters.Contains(x))
                        temp += illegalReplacement;
                    else
                        temp += x;
                });
            return path != null ? System.IO.Path.Combine(path, temp) : null;
        }

        public static Version GetVersion(string fileName) {
            if (!System.IO.File.Exists(fileName))
                return null;
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
            if (!System.IO.File.Exists(fileName))
                return null;
            var result = new List<string>();
            using (var fs = OpenFile(fileName, FileMode.Open, FileAccess.Read, share))
            using (var sr = new StreamReader(fs)) {
                while (sr.Peek() != -1) {
                    var line = sr.ReadLine();
                    if (!includeBlankLines && string.IsNullOrEmpty(line))
                        continue;
                    result.Add(line);
                }
            }
            return result;
        }
    }
}