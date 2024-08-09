/* File="ApplicationLogger"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using global::Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging {
    public sealed class ApplicationLogger {
        public class InternalStorage {
            public EntryTypes EntryType { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Text { get; set; }
        }

        public enum EntryTypes {
            Information,
            Warning,
            Error
        }

        public enum StorageTypes {
            FlatFile,
            CsvFile,
            Xml,
            WindowsLog
        }

        [Flags]
        public enum StorageOptions {
            None = 0,
            CreateFolderForEachDay = 1,
            StoreInMemoryUntilFlush = 2,
            NewestFirstLogEntry = 4
        }

        ~ApplicationLogger() {
            Flush();
        }

        public ApplicationLogger(string applicationName, string logDirectory)
            : this(applicationName, logDirectory, StorageTypes.FlatFile) {
        }

        public ApplicationLogger(string applicationName, string logDirectory, StorageTypes storageType)
            : this(applicationName, storageType, logDirectory, StorageOptions.None, "AppLog") {
        }

        public ApplicationLogger(string applicationName, StorageTypes storageType, string logDirectory, StorageOptions options, string logFileBaseName, bool createNewFolderEachDay = false) {
            ApplicationName = applicationName;
            CreateNewFolderEachDay = createNewFolderEachDay;
            StorageType = storageType;
            LogFileExtension = StorageType == StorageTypes.FlatFile ? ".log" : StorageType == StorageTypes.CsvFile ? ".csv" : ".xml";
            Options = options;
            LogDirectory = logDirectory;
            Update();
            var isImmediate = !options.HasFlag(StorageOptions.StoreInMemoryUntilFlush);
            // 
            //flat file is default
            switch (StorageType) {
                case StorageTypes.CsvFile:
                    logger = new CsvLogger(ApplicationName, LogDirectory, Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
                    break;

                case StorageTypes.Xml:
                    logger = new XmlLogger(ApplicationName, LogDirectory, Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
                    break;

                case StorageTypes.WindowsLog:
                    logger = new WindowsLogLogger(ApplicationName);
                    break;

                default:
                    logger = new FlatFileLogger(ApplicationName, LogDirectory, Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
                    break;
            }
            FileName = logger.LogFileName;
        }
        public string FileName { get; set; } = default;

        public ApplicationLogger UpdateStorageOptions(StorageOptions options) {
            Options = options;
            Update();
            return this;
        }

        public StringBuilder ReadAll() {
            var result = new StringBuilder();
            using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using var sr = new StreamReader(fs);
                while (sr.Peek() > -1) {
                    result.AppendLine(sr.ReadLine());
                }
            }
            return result;
        }

        private ApplicationLogger Update() {
            if (Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
                if (!cache.IsNull() && cache.Any()) {
                    Flush();
                }

                cache = new List<InternalStorage>();
            }
            if (Options.HasFlag(StorageOptions.CreateFolderForEachDay)) {
                if (!Directory.Exists(LogDirectory)) {
                    Directory.CreateDirectory(LogDirectory);
                }

                LogDirectory = Path.Combine(LogDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(LogDirectory)) {
                    Directory.CreateDirectory(LogDirectory);
                }
            }
            return this;
        }

        private IList<InternalStorage> cache = default;
        private ILogger logger = default;

        public bool IsWriteComplete => logger.IsWriteComplete;

        public string ApplicationName { get; private set; }
        public StorageTypes StorageType { get; set; }
        public double SingleFileMaxSizeInMB { get => logger.SingleLogFileMaxSize; set => logger.SingleLogFileMaxSize = value; }
        public string LogFileExtension { get; private set; }
        public string LogDirectory { get; set; }
        public StorageOptions Options { get; private set; }
        public bool CreateNewFolderEachDay { get; private set; }

        //public ApplicationLogger LogMessage(string text, EntryTypes type) =>
        //    LogMessage(new StringBuilder(text), type);
        
        public ApplicationLogger LogMessage(StringBuilder text, EntryTypes type) {
            LogMessage(text.ToString(), type);
            return this;
        }

        public void LogMessage(string text, EntryTypes type, int indent = 0) {
            if (Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
                cache.Add(new InternalStorage {
                    EntryType = type,
                    TimeStamp = DateTime.Now,
                    Text = new string(' ', indent * 3) + text
                });
            }
            else {
                if (!LogDirectory.IsNull() && !logger.LogPath.IsNull() && !logger.LogPath.Equals(LogDirectory)) {
                    logger.LogPath = LogDirectory;
                }
                using var sr = new StringReader(text);
                while (sr.Peek() > -1) {
                    var line = sr.ReadLine();
                    if (line.IsNull()) continue;
                    logger.Write(new string(' ', indent * 3) + line, DateTime.Now, type);
                }
            }
        }

        public ApplicationLogger Flush() {
            var hasStorageFlage = Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush);
            if (!hasStorageFlage || (hasStorageFlage && (cache.IsNull() || !cache.Any()))) {
                return this;
            }

            logger.WriteBulk(cache);
            cache.Clear();
            return this;
        }
    }
}
