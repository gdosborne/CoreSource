using Common.OzApplication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Common.OzApplication.Logging.ApplicationLogger;

namespace Application {
    public struct StackEntry {
        public int Id;
        public string Text;
        public EntryTypes Type;
        public DateTime Written;
        public bool IsComplete;
        public bool IsPicked;
    }

    internal interface ILogger {
        void ProcessEntry(StackEntry entry);
        void Write(string text, DateTime written, EntryTypes type);
        void WriteBulk(IList<string> items, IList<DateTime> itemsWitten, IList<EntryTypes> itemsType);
        void WriteBulk(IList<InternalStorage> items);
        string ApplicationName { get; }
        string LogPath { get; set; }
        bool ReverseOrderLogging { get; }
        string LogFileName { get; }
        double SingleLogFileMaxSize { get; set; }
        bool IsWriteComplete { get; }
        bool IsImmediateWrite { get; }
    }

    internal abstract class LoggerBase : ILogger {

        public LoggerBase(string applicationName, string logPath, bool reverseOrder, string extension, string logFileBaseName = null, bool createNewFolderEachDay = false, bool isFileStorage = true, bool isWriteImmediate = true) {
            ApplicationName = applicationName;
            MessageStack = new List<StackEntry>();
            SingleLogFileMaxSize = 10;
            IsImmediateWrite = isWriteImmediate;
            this.logPath = logPath;
            CreateNewFolderEachDay = createNewFolderEachDay;
            ReverseOrderLogging = reverseOrder;
            logExtension = extension;

            if (isFileStorage) {
                if (string.IsNullOrEmpty(LogPath)) {
                    LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName, "Logs");
                }

                if (!Directory.Exists(LogPath)) {
                    Directory.CreateDirectory(LogPath);
                }

                this.logFileBaseName = string.IsNullOrWhiteSpace(logFileBaseName) ? ApplicationName : logFileBaseName;
                LogFileName = Path.Combine(LogPath, $"{this.logFileBaseName}.{logExtension}");
                if (File.Exists(LogFileName)) {
                    while (new FileInfo(LogFileName).Length >= Convert.ToInt64(ByteSize.BytesInMegaByte * 10)) {
                        IncrementFileName();
                    }
                }
            }
            if (isWriteImmediate) {
                return;
            }

            stackTimer = new System.Timers.Timer {
                Interval = 500
            };
            stackTimer.Elapsed += StackTimer_Elapsed;
            stackTimer.Start();
        }

        public bool IsWriteComplete { get; protected set; } = true;

        private void StackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (!MessageStack.Any()) {
                return;
            }

            var entry = default(StackEntry);
            stackTimer.Stop();
            var t = Task.Factory.StartNew(() => {
                var trys = 0;
                while (true) {
                    IsWriteComplete = false;
                    if (!entry.IsPicked || entry.IsComplete) {
                        entry = MessageStack.OrderBy(x => x.Id).First();
                        MessageStack.Remove(entry);
                        entry.IsPicked = true;
                        trys = 0;
                    }
                    try {
                        ProcessEntry(entry);
                        entry.IsComplete = true;
                    }
                    catch {
                        System.Threading.Thread.Sleep(100);
                        trys++;
                        if (trys > 9) {
                            entry.IsComplete = false;
                            entry.IsPicked = false;
                            MessageStack.Add(entry);
                            break;
                        }
                    }
                    if (entry.IsComplete && !MessageStack.Any()) {
                        break;
                    }
                }
            });
            t.Wait();
            IsWriteComplete = true;
            stackTimer.Start();
        }

        private readonly System.Timers.Timer stackTimer = default;
        public abstract void ProcessEntry(StackEntry entry);
        protected void ValidateFileSize() {
            if (!string.IsNullOrEmpty(LogFileName) && File.Exists(LogFileName)) {
                var mbs = new ByteSize(new FileInfo(LogFileName).Length).MegaBytes;
                if (mbs >= SingleLogFileMaxSize) {
                    IncrementFileName();
                }
            }
        }

        private int lastFileIndex = -1;
        private readonly string logFileBaseName = default;
        private readonly string logExtension = default;

        protected void IncrementFileName() {
            lastFileIndex++;
            if (!string.IsNullOrEmpty(LogFileName)) {
                LogFileName = Path.Combine(LogPath, $"{logFileBaseName}.{lastFileIndex:00}.{logExtension}");
            }
        }

        protected int FileInUseError => -2147024864;
        protected List<StackEntry> MessageStack = default;

        public string ApplicationName { get; private set; }
        public string LogPath {
            get => logPath;
            set {
                logPath = value;
                if (!string.IsNullOrEmpty(logPath)) {
                    var patFrameworkarts = LogPath.Split('\\');
                    if (!patFrameworkarts.Contains(ApplicationName)) {
                        logPath = Path.Combine(LogPath, ApplicationName);
                        if (!Directory.Exists(logPath)) {
                            Directory.CreateDirectory(logPath);
                        }
                    }
                    if (CreateNewFolderEachDay) {
                        var dt = DateTime.Now.ToString("yyyy-MM-dd");
                        if (!patFrameworkarts.Contains(dt)) {
                            logPath = Path.Combine(LogPath, dt);
                            if (!Directory.Exists(logPath)) {
                                Directory.CreateDirectory(logPath);
                            }
                        }
                    }
                    LogFileName = Path.Combine(LogPath, $"{logFileBaseName}.{logExtension}");
                }
            }
        }
        private string logPath = default;
        public bool ReverseOrderLogging { get; private set; }
        public string LogFileName { get; private set; }
        public virtual void Write(string text, DateTime written, EntryTypes type) {
            ValidateFileSize();
            var id = !MessageStack.Any() ? 0 : MessageStack.Max(x => x.Id) + 1;
            var se = new StackEntry {
                Id = id,
                Text = text,
                Written = written,
                Type = type
            };
            if (IsImmediateWrite) {
                ProcessEntry(se);
            }
            else {
                MessageStack.Add(se);
            }
        }
        public bool CreateNewFolderEachDay { get; private set; }

        public virtual void WriteBulk(IList<InternalStorage> items) {
            var strings = items.Select(x => x.Text).ToList();
            var dateTimes = items.Select(x => x.TimeStamp).ToList();
            var types = items.Select(x => x.EntryType).ToList();
            WriteBulk(strings, dateTimes, types);
        }

        public virtual void WriteBulk(IList<string> items, IList<DateTime> itemsWitten, IList<EntryTypes> itemsType) {
            if (items.Count != itemsWitten.Count || items.Count != itemsType.Count || itemsWitten.Count != itemsType.Count) {
                throw new ApplicationException("Item count for all lists must match.");
            }

            for (var i = 0; i < items.Count; i++) {
                Write(items[i], itemsWitten[i], itemsType[i]);
            }
        }

        protected bool DeleteFile(string fileName) {
            var result = false;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName)) {
                var tryCount = default(byte);
                while (tryCount < 10) {
                    try {
                        File.Delete(fileName);
                        result = true;
                        break;
                    }
                    catch {
                        tryCount++;
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(50));
                    }
                }
            }
            return result;
        }

        protected string GetDateValueForLog(DateTime value, int padSize = 0) => $"{value:yyyy-MM-dd hh:mm:ss tt}".PadRight(padSize);

        protected string GetSplitDateValueForLog(DateTime value, int padSize = 0) => $"{value:yyyy-MM-dd},{value:hh:mm:ss tt}".PadRight(padSize);

        protected string GetTypesValueForLog(EntryTypes value, int padSize = 0) => $"{value}".PadRight(padSize);

        protected void WriteReverseTextFile(string newEntry, EntryTypes type, DateTime written, bool returnOnError = false) {
            if (string.IsNullOrEmpty(LogFileName)) {
                return;
            }

            var previousData = default(string);
            ValidateFileSize();
            try {
                using (var fs = new FileStream(LogFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                using (var sr = new StreamReader(fs))
                using (var sw = new StreamWriter(fs)) {
                    previousData = sr.ReadToEnd();
                    fs.Seek(0, SeekOrigin.Begin);
                    sw.Write(newEntry);
                    sw.Write(previousData);
                }
            }
            catch (System.Exception) {
                throw;
            }
        }

        protected string GetMultiLineText(string value, int paddingSpace) {
            if (string.IsNullOrEmpty(value)) {
                return string.Empty;
            }

            var firstLine = true;
            var result = default(string);
            using (var sReader = new StringReader(value)) {
                while (sReader.Peek() > -1) {
                    var line = sReader.ReadLine();
                    if (!firstLine) {
                        result += new string(' ', paddingSpace);
                    }

                    result += $"{line}{Environment.NewLine}";
                    firstLine = false;
                }
            }
            return result;
        }

        public double SingleLogFileMaxSize { get; set; }
        public bool IsImmediateWrite { get; private set; }
    }

    internal class CsvLogger : LoggerBase {
        public CsvLogger(string applicationName, string logPath, bool reverseOrder, string logFileBaseName, bool createNewFolderEacDay, bool isImmediateWrite)
            : base(applicationName, logPath, reverseOrder, "csv", logFileBaseName, createNewFolderEacDay, isWriteImmediate: isImmediateWrite) { }

        public override void ProcessEntry(StackEntry entry) {
            var newEntry = $"{GetSplitDateValueForLog(entry.Written)},{GetTypesValueForLog(entry.Type)},{GetMultiLineText(entry.Text, 0).Replace(",", " ")}";
            if (ReverseOrderLogging && File.Exists(LogFileName)) {
                WriteReverseTextFile(newEntry, entry.Type, entry.Written);
            }
            else {
                try {
                    ValidateFileSize();
                    using (var writer = File.AppendText(LogFileName)) {
                        writer.Write(newEntry);
                    }
                }
                catch (System.Exception) {
                    throw;
                }
            }
        }
    }

    internal class WindowsLogLogger : LoggerBase {
        public WindowsLogLogger(string applicationName)
            : base(applicationName, null, false, null, null, false, isFileStorage: false) { }

        public override void ProcessEntry(StackEntry entry) {
            /* 
            ╔════════════════════════════════════════════════════════════════════════╗
            ║ To use eventlog you must make sure that the user the app runs as is    ║
            ║  allowed to read/write the following registry key                      ║
            ║                                                                        ║
            ║  HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog         ║
            ║                                                                        ║
            ║ They must also have read access in the following registry key          ║
            ║                                                                        ║
            ║  HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\EventLog\Security║
            ║                                                                        ║
            ║ Normally if you give access to the parent key in a registry, children  ║ 
            ║  will inherit those permissions. The child Security key DOES NOT       ║
            ║  inherit permissions from parent, so therefore must be set separately  ║   
            ╚════════════════════════════════════════════════════════════════════════╝
            */

            try {
                EventLog.CreateEventSource(ApplicationName, "Greg.Osborne");
            }
            catch { }

            //if (!EventLog.SourceExists(ApplicationName))
            //    EventLog.CreateEventSource(ApplicationName, "Greg.Osborne");

            var type = EventLogEntryType.Information;
            if (entry.Type == EntryTypes.Error) {
                type = EventLogEntryType.Error;
            }
            else if (entry.Type == EntryTypes.Warning) {
                type = EventLogEntryType.Warning;
            }

            EventLog.WriteEntry(ApplicationName, entry.Text, type);
        }
    }

    internal class FlatFileLogger : LoggerBase {
        private readonly object fileLock = new object();
        public FlatFileLogger(string applicationName, string logPath, bool reverseOrder, string logFileBaseName, bool createNewFolderEacDay, bool isImmediateWrite)
            : base(applicationName, logPath, reverseOrder, "log", logFileBaseName, createNewFolderEacDay, isWriteImmediate: isImmediateWrite) { }

        public override void ProcessEntry(StackEntry entry) {
            var newEntry = $"{GetDateValueForLog(entry.Written, 24)}{GetTypesValueForLog(entry.Type, EntryTypes.Information.ToString().Length + 2)}";
            newEntry += $"{GetMultiLineText(entry.Text, newEntry.Length + 3)}";
            if (ReverseOrderLogging && File.Exists(LogFileName)) {
                WriteReverseTextFile(newEntry, entry.Type, entry.Written);
                return;
            }

            try {
                ValidateFileSize();
                lock (fileLock) {
                    if (!File.Exists(LogFileName)) {
                        using (var fs = new FileStream(LogFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                        using (var writer = new StreamWriter(fs)) {
                            writer.Write(newEntry);
                        }
                    }
                    else {
                        using (var fs = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        using (var writer = new StreamWriter(fs)) {
                            writer.Write(newEntry);
                        }
                    }
                }
            }
            catch (System.Exception) {
                throw;
            }
        }
    }

    internal class XmlLogger : LoggerBase {
        public XmlLogger(string applicationName, string logPath, bool reverseOrder, string logFileBaseName, bool createNewFolderEacDay, bool isImmediateWrite)
            : base(applicationName, logPath, reverseOrder, "xml", logFileBaseName, createNewFolderEacDay, isWriteImmediate: isImmediateWrite) { }

        public override void ProcessEntry(StackEntry entry) {
            var doc = default(XDocument);
            if (!File.Exists(LogFileName)) {
                doc = new XDocument(new XElement("log",
                    new XElement("entries")));
            }
            else {
                doc = XDocument.Load(LogFileName);
            }

            var newElement = new XElement("item",
                new XAttribute("timestamp", entry.Written),
                new XAttribute("type", entry.Type));
            newElement.Add(new XCData(entry.Text));
            var entriesRoot = doc.Root.Element("entries");
            if (ReverseOrderLogging && File.Exists(LogFileName)) {
                var reverseElements = entriesRoot.Elements().OrderByDescending(x => DateTime.Parse(x.Attribute("timestamp").Value)).ToList();
                reverseElements.Insert(0, newElement);
                entriesRoot.RemoveAll();
                reverseElements.ForEach(x => entriesRoot.Add(x));
            }
            else {
                entriesRoot.Add(newElement);
            }
            doc.Save(LogFileName);
        }
    }
}
