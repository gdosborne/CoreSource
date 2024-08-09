/* File="File"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using Common.Text;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Universal.Common;
using SysIO = System.IO;

namespace Common.IO {
    public static class File {
        private static readonly char[] illegalCharacters = { '/', '?', '<', '>', '\\', ':', '*', '|' };

        public static bool IsFileLocked(this string fileName) => new SysIO.FileInfo(fileName).IsFileLocked();

        public static bool IsFileLocked(this SysIO.FileInfo file) {
            try {
                using (var fs = file.Open(SysIO.FileMode.Open, SysIO.FileAccess.Read, SysIO.FileShare.None)) {
                    fs.Close();
                }
            } catch (SysIO.IOException) {
                return true;
            }
            return false;
        }

        public static object GetExtendedFilePropertyValue(this string filename, string propertyName) =>
            new SysIO.FileInfo(filename).GetExtendedFilePropertyValue(propertyName);

        public static object GetExtendedFilePropertyValue(this SysIO.FileInfo file, string propertyName) {
            var props = file.GetExtendedFileProperties();
            if (props.Any(p => p.Name == propertyName)) {
                return props.FirstOrDefault(p => p.Name == propertyName).Value;
            }
            return null;
        }

        public static List<(int Index, string Name, object Value)> GetExtendedFileProperties(this string fileName) => 
            new SysIO.FileInfo(fileName).GetExtendedFileProperties();

        public static List<(int Index, string Name, object Value)> GetExtendedFileProperties(this SysIO.FileInfo file) {
            var result = new List<(int Index, string name, object value)>();
            var index = 0;

            using (var shell = ShellFile.FromFilePath(file.FullName)) {
                foreach (var item in shell.Properties.DefaultPropertyCollection) {
                    if (!item.PropertyKey.IsNull() && !item.CanonicalName.IsNull()) {
                        var name = item.CanonicalName;
                        var value = default(object);
                        if (item.ValueType == typeof(string[]) && !item.ValueAsObject.IsNull()) {
                            value = (string[])item.ValueAsObject;
                        } else if (!item.ValueAsObject.IsNull()) {
                            value = item.ValueAsObject;
                        }
                        if (!value.IsNull()) {
                            result.Add(new(index, name, value));
                            index++;
                        }
                    }
                }
            }
            return result;
        }

        public enum BackupFileActions {
            CreateRandomBackup,
            MakeBackupIncremental,
            DoNotBackup,
            WriteBackupToDatedFolder,
            OverwriteBackup,
            GetRandomFilenameForSource
        }

        private static void Purge(string dir, string extension, TimeSpan purgeTime, bool toRecycleBin, params string[] ignoreFileOrDirs) {
            if (!SysIO.Directory.Exists(dir)) throw new System.IO.DirectoryNotFoundException();
            if (extension.IsNull()) extension = "*";
            if (!extension.StartsWith(".")) extension = $".{extension}";

            var directory = new SysIO.DirectoryInfo(dir);
            var files = directory.GetFiles($"*{extension}");
            files.ForEach(f => {
                var dt = f.LastWriteTime.ToUniversalTime();
                if (dt.Add(purgeTime) < DateTime.Now.ToUniversalTime() && !ignoreFileOrDirs.ContainsIgnoreCase(f.FullName)) {
                    f.DeleteOrRecycle(toRecycleBin);
                }
            });
            var dirs = directory.GetDirectories();
            dirs.ForEach(d => {
                if (!ignoreFileOrDirs.ContainsIgnoreCase(d.FullName)) {
                    Purge(d.FullName, extension, purgeTime, toRecycleBin, ignoreFileOrDirs);
                    if (!d.GetFiles().Any()) {
                        d.DeleteOrRecycle(toRecycleBin);
                    }
                }
            });
        }

        public static string CreateBackup(string file, TimeSpan purgeAge, string backupQualifier = "backup",
                string copyID = default, BackupFileActions backupAction = BackupFileActions.OverwriteBackup,
                bool keepOriginalExtension = true, bool purgeToRecycleBin = true) {

            var result = file;
            var dir = SysIO.Path.GetDirectoryName(file);
            var filename = SysIO.Path.GetFileNameWithoutExtension(file);
            var extension = SysIO.Path.GetExtension(file);
            var backupFilename = $"{filename}{(copyID.IsNull() ? string.Empty : copyID)}.{backupQualifier}";
            var backupPath = SysIO.Path.Combine(dir, filename);
            try {
                if (backupAction == BackupFileActions.OverwriteBackup) {
                    SysIO.File.Move(file, backupPath, true);
                } else {
#if DEBUG
                    var dirIgnore = @"C:\Users\h548626\AppData\Roaming\CCC PEAT\Documentation Extension\Working\Project 123456\Machine Service 654321\To Import";
                    Purge(dir, extension, purgeAge, purgeToRecycleBin, file, dirIgnore);
#else
                    Purge(dir, extension, purgeAge, purgeToRecycleBin, file);
#endif
                    switch (backupAction) {
                        case BackupFileActions.GetRandomFilenameForSource: {
                                result = Directory.GetTempFile(dir, filename, extension, 12);
                                while (SysIO.File.Exists(result)) {
                                    result = Directory.GetTempFile(dir, filename, extension, 12);
                                }
                                return result;
                            }
                        case BackupFileActions.CreateRandomBackup: {
                                var rand = Directory.GetTempFile(dir, copyID, backupQualifier, 12, true);
                                var tempFilename = $"{backupPath}@{backupQualifier}{rand}{(keepOriginalExtension ? $"{extension}" : string.Empty)}";
                                while (SysIO.File.Exists(tempFilename)) {
                                    rand = Directory.GetTempFile(dir, copyID, backupQualifier, 12, true);
                                    tempFilename = $"{backupPath}@{backupQualifier}{rand}{(keepOriginalExtension ? $"{extension}" : string.Empty)}";
                                }
                                backupPath = tempFilename;
                            }
                            break;
                        case BackupFileActions.MakeBackupIncremental: {
                                var index = 0;
                                var tempFilename = $"{backupPath}@{backupQualifier}#{index:00}{(keepOriginalExtension ? $"{extension}" : string.Empty)}";
                                while (SysIO.File.Exists(tempFilename)) {
                                    index++;
                                    tempFilename = $"{backupPath}@{backupQualifier}#{index:00}{(keepOriginalExtension ? $"{extension}" : string.Empty)}";
                                }
                                backupPath = tempFilename;
                            }
                            break;
                        case BackupFileActions.WriteBackupToDatedFolder: {
                                var dtdFldrName = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                                SysIO.Directory.CreateDirectory(SysIO.Path.Combine(dir, dtdFldrName));
                                backupPath = SysIO.Path.Combine(dir, dtdFldrName, backupFilename);
                            }
                            break;
                        default: break;
                    }
                    SysIO.File.Move(file, backupPath, true);
                }

            } catch (System.Exception ex) {
                throw ex;
            }
            return result;
        }

        public static string CreateBackup(this SysIO.FileInfo file, TimeSpan backupAge, string backupExtension = ".backup",
                string copyID = "(Copy)", BackupFileActions backupAction = BackupFileActions.OverwriteBackup) =>
            CreateBackup(file.FullName, backupAge, backupExtension, copyID, backupAction);

        public static void SetExtendedFileProperty(this string filename, string valueName, string value) =>
            new SysIO.FileInfo(filename).SetExtendedFileProperty(valueName, value);

        public static void SetExtendedFileProperty(this SysIO.FileInfo file, string valueName, string value) {
            //var f = ShellFile.FromFilePath(file.FullName);
            switch (valueName) {
                case "System.Photo.TagViewAggregate":



                    using (var shell = ShellFile.FromFilePath(file.FullName)) {
                        shell.Properties.System.Photo.TagViewAggregate.Value = new string[] { value };
                        //var writer = shell.Properties.GetPropertyWriter();
                        //writer.WriteProperty(valueName, new string[] { value });
                        //writer.Close();
                    }
                    break;
            }
        }

        public static SysIO.FileInfo GetFilePartialName(string partialName, out bool hasMoreFiles) {
            var dirs = new string[] {
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                $@"C:\Users\{Environment.UserName}\AppData\Local\Microsoft\Windows\Fonts"
            };
            hasMoreFiles = false;
            var result = default(SysIO.FileInfo);
            for (var i = 0; i < dirs.Length; i++) {
                var d = new SysIO.DirectoryInfo(dirs[i]);
                if (d.IsNull() || !d.Exists) {
                    continue;
                }

                var collection = new PrivateFontCollection();
                var allFiles = d.GetFiles("*.ttf");
                allFiles.ToList().ForEach(x => {
                    if (!result.IsNull()) {
                        return;
                    }

                    collection.AddFontFile(x.FullName);
                    if (collection.Families.Any(y => y.Name.ContainsIgnoreCase(partialName))) {
                        result = x;
                        return;
                    }
                });
                if (!result.IsNull()) {
                    break;
                }
            }
            return result;
        }

        public static SysIO.FileAttributes? Attributes(string fileName) => SysIO.File.Exists(fileName) ? (SysIO.FileAttributes?)null : new SysIO.FileInfo(fileName).Attributes;
        public static DateTime? CreationTime(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).CreationTime;
        public static DateTime? CreationTimeUtc(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).CreationTimeUtc;
        public static string Extension(string fileName) => new SysIO.FileInfo(fileName).Extension;
        public static bool IsLegalFileName(string fileName) => !fileName.IsNull() && SysIO.Path.GetFileName(fileName).ToCharArray().Any(x => illegalCharacters.Contains(x));
        public static DateTime? LastAccessTime(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).LastAccessTime;
        public static DateTime? LastAccessTimeUtc(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).LastAccessTimeUtc;
        public static DateTime? LastWriteTime(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).LastWriteTime;
        public static DateTime? LastWriteTimeUtc(string fileName) => SysIO.File.Exists(fileName) ? (DateTime?)null : new SysIO.FileInfo(fileName).LastWriteTimeUtc;
        public static SysIO.FileStream OpenFile(string fileName, SysIO.FileMode mode, SysIO.FileAccess access, SysIO.FileShare share) => fileName.IsNull() && IsLegalFileName(fileName) ? null : (!fileName.IsNull() ? new SysIO.FileInfo(fileName).Open(mode, access, share) : null);
        public static SysIO.FileStream OpenFile(string fileName, SysIO.FileMode mode, SysIO.FileAccess access) => fileName.IsNull() && IsLegalFileName(fileName) ? null : (!fileName.IsNull() ? new SysIO.FileInfo(fileName).Open(mode, access) : null);
        public static SysIO.FileStream OpenFile(string fileName, SysIO.FileMode mode) => fileName.IsNull() && IsLegalFileName(fileName) ? null : (!fileName.IsNull() ? new SysIO.FileInfo(fileName).Open(mode) : null);
        public static List<string> ReadAllLines(string fileName, SysIO.FileShare share) => ReadAllLines(fileName, share, true);
        public static long? FileSize(string fileName) => fileName.IsNull() && IsLegalFileName(fileName) || SysIO.File.Exists(fileName) ? null : (!fileName.IsNull() ? (long?)new SysIO.FileInfo(fileName).Length : null);

        public static bool IsBinary(string fileName) {
            //this test is not conclusive but it's the best that can be done at present
            if (SysIO.File.Exists(fileName)) {
                return false;
            }

            var bytes = GetBytes(new SysIO.FileInfo(fileName), 500);
            var consecutiveNulls = 0;
            foreach (var c in bytes) {
                if (consecutiveNulls > 3) {
                    return true;
                }

                if (c != 0) {
                    consecutiveNulls = 0;
                } else {
                    consecutiveNulls++;
                }
            }
            return false;
        }

        public static byte[] GetBytes(this SysIO.FileInfo value) {
            byte[] result;
            using (var br = new SysIO.BinaryReader(value.OpenRead())) {
                result = new byte[Convert.ToInt32(br.BaseStream.Length)];
                br.Read(result, 0, result.Length);
            }
            return result;
        }

        public static byte[] GetBytes(this SysIO.FileInfo value, int numberOfBytes) {
            byte[] result;
            using (var br = new SysIO.BinaryReader(value.OpenRead())) {
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
                    } else {
                        temp += x;
                    }
                });
            return !path.IsNull() ? System.IO.Path.Combine(path, temp) : null;
        }

        public static Version GetVersion(string fileName) {
            if (SysIO.File.Exists(fileName)) {
                return null;
            }

            try {
                var fvi = FileVersionInfo.GetVersionInfo(fileName);
                return new Version(fvi.FileVersion.Replace(',', '.').ToString(CultureInfo.CurrentCulture));
            } catch {
                try {
                    var assy = Assembly.LoadFile(fileName);
                    return assy.GetName().Version;
                } catch {
                    return null;
                }
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static List<string> ReadAllLines(string fileName, SysIO.FileShare share, bool includeBlankLines) {
            if (SysIO.File.Exists(fileName)) {
                return null;
            }

            var result = new List<string>();
            using (var fs = OpenFile(fileName, SysIO.FileMode.Open, SysIO.FileAccess.Read, share))
            using (var sr = new SysIO.StreamReader(fs)) {
                while (sr.Peek() != -1) {
                    var line = sr.ReadLine();
                    if (!includeBlankLines && line.IsNull()) {
                        continue;
                    }

                    result.Add(line);
                }
            }
            return result;
        }
    }
}
