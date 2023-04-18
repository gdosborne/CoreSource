using global::Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.OzApplication.Logging {
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

        private ApplicationLogger Update() {
            if (Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
                if (cache != null && cache.Any()) {
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

        public ApplicationLogger LogMessage(string text, EntryTypes type) =>
            LogMessage(new StringBuilder(text), type);
        
        public ApplicationLogger LogMessage(StringBuilder text, EntryTypes type) {
            LogMessageAsync(text.ToString(), type).GetAwaiter();
            return this;
        }

        public async Task<ApplicationLogger> LogMessageAsync(string text, EntryTypes type, int indent = 0) {
            var t = Task.Factory.StartNew(() => { 
                if (Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
                    cache.Add(new InternalStorage {
                        EntryType = type,
                        TimeStamp = DateTime.Now,
                        Text = new string(' ', indent * 3) + text
                    });
                }
                else {
                    if (!string.IsNullOrEmpty(LogDirectory) && !string.IsNullOrEmpty(logger.LogPath) && !logger.LogPath.Equals(LogDirectory)) {
                        logger.LogPath = LogDirectory;
                    }
                    using var sr = new StringReader(text);
                    while (sr.Peek() > -1) {
                        logger.Write(new string(' ', indent * 3) + sr.ReadLine(), DateTime.Now, type);
                    }
                }
            });
            t.Wait();
            return this;
        }

        public ApplicationLogger Flush() {
            var hasStorageFlage = Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush);
            if (!hasStorageFlage || (hasStorageFlage && (cache == null || !cache.Any()))) {
                return this;
            }

            logger.WriteBulk(cache);
            cache.Clear();
            return this;
        }
    }
}
