using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GregOsborne.Application.IO {
    public static class Directory {
        public static FileAttributes? Attributes(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? (FileAttributes?) null : new DirectoryInfo(directoryName).Attributes;
        }

        public static DateTime? CreationTime(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).CreationTime;
        }

        public static DateTime? CreationTimeUtc(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).CreationTimeUtc;
        }

        public static string Extension(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? null : new DirectoryInfo(directoryName).Extension;
        }

        public static List<FileInfo> GetFiles(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? null : new List<FileInfo>(new DirectoryInfo(directoryName).GetFiles());
        }

        public static List<FileInfo> GetFiles(string directoryName, string filter) {
            return !System.IO.Directory.Exists(directoryName) ? null : new List<FileInfo>(new DirectoryInfo(directoryName).GetFiles(filter));
        }

        public static string GetTempFile(this DirectoryInfo dirInfo) {
            var fileName = new Random().Next().ToString("X") + ".tmp";
            while (System.IO.File.Exists(System.IO.Path.Combine(dirInfo.FullName, fileName)))
                fileName = new Random().Next().ToString("X") + ".tmp";
            return System.IO.Path.Combine(dirInfo.FullName, fileName);
        }

        public static DateTime? LastAccessTime(string directoryName) {
            if (!System.IO.Directory.Exists(directoryName))
                return null;
            return new DirectoryInfo(directoryName).LastAccessTime;
        }

        public static DateTime? LastAccessTimeUtc(string directoryName) {
            if (!System.IO.Directory.Exists(directoryName))
                return null;
            return new DirectoryInfo(directoryName).LastAccessTimeUtc;
        }

        public static DateTime? LastWriteTime(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).LastWriteTime;
        }

        public static DateTime? LastWriteTimeUtc(string directoryName) {
            return !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).LastWriteTimeUtc;
        }

        public static long? Size(string directoryName, string filter) {
            if (!System.IO.Directory.Exists(directoryName))
                return null;
            var result = GetFiles(directoryName, filter).ToList().Sum(x => x.Length);
            return result;
        }

        public static long? Size(string directoryName) {
            return Size(directoryName, "*.*");
        }
    }
}