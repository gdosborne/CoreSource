namespace Application {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using OzDB.Application;
	using static OzDB.Application.Logging.ApplicationLogger;

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
			this.ApplicationName = applicationName;
			this.MessageStack = new List<StackEntry>();
			this.SingleLogFileMaxSize = 10;
			this.IsImmediateWrite = isWriteImmediate;
			this.logPath = logPath;
			this.CreateNewFolderEachDay = createNewFolderEachDay;
			this.ReverseOrderLogging = reverseOrder;
			this.logExtension = extension;

			if (isFileStorage) {
				if (string.IsNullOrEmpty(this.LogPath)) {
					this.LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.ApplicationName, "Logs");
				}

				if (!Directory.Exists(this.LogPath)) {
					Directory.CreateDirectory(this.LogPath);
				}

				this.logFileBaseName = string.IsNullOrWhiteSpace(logFileBaseName) ? this.ApplicationName : logFileBaseName;
				this.LogFileName = Path.Combine(this.LogPath, $"{this.logFileBaseName}.{this.logExtension}");
				if (File.Exists(this.LogFileName)) {
					while (new FileInfo(this.LogFileName).Length >= Convert.ToInt64(ByteSize.BytesInMegaByte * 10)) {
						this.IncrementFileName();
					}
				}
			}
			if (isWriteImmediate) {
				return;
			}

			this.stackTimer = new System.Timers.Timer {
				Interval = 500
			};
			this.stackTimer.Elapsed += this.StackTimer_Elapsed;
			this.stackTimer.Start();
		}

		public bool IsWriteComplete { get; protected set; } = true;

		private void StackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			if (!this.MessageStack.Any()) {
				return;
			}

			var entry = default(StackEntry);
			this.stackTimer.Stop();
			var t = Task.Factory.StartNew(() => {
				var trys = 0;
				while (true) {
					this.IsWriteComplete = false;
					if (!entry.IsPicked || entry.IsComplete) {
						entry = this.MessageStack.OrderBy(x => x.Id).First();
						this.MessageStack.Remove(entry);
						entry.IsPicked = true;
						trys = 0;
					}
					try {
						this.ProcessEntry(entry);
						entry.IsComplete = true;
					}
					catch {
						System.Threading.Thread.Sleep(100);
						trys++;
						if (trys > 9) {
							entry.IsComplete = false;
							entry.IsPicked = false;
							this.MessageStack.Add(entry);
							break;
						}
					}
					if (entry.IsComplete && !this.MessageStack.Any()) {
						break;
					}
				}
			});
			t.Wait();
			this.IsWriteComplete = true;
			this.stackTimer.Start();
		}

		private readonly System.Timers.Timer stackTimer = default;
		public abstract void ProcessEntry(StackEntry entry);
		protected void ValidateFileSize() {
			if (!string.IsNullOrEmpty(this.LogFileName) && File.Exists(this.LogFileName)) {
				var mbs = new ByteSize(new FileInfo(this.LogFileName).Length).MegaBytes;
				if (mbs >= this.SingleLogFileMaxSize) {
					this.IncrementFileName();
				}
			}
		}

		private int lastFileIndex = -1;
		private readonly string logFileBaseName = default;
		private readonly string logExtension = default;

		protected void IncrementFileName() {
			this.lastFileIndex++;
			if (!string.IsNullOrEmpty(this.LogFileName)) {
				this.LogFileName = Path.Combine(this.LogPath, $"{this.logFileBaseName}.{this.lastFileIndex.ToString("00")}.{this.logExtension}");
			}
		}

		protected int FileInUseError => -2147024864;
		protected List<StackEntry> MessageStack = default;

		public string ApplicationName { get; private set; }
		public string LogPath {
			get => this.logPath;
			set {
				this.logPath = value;
				if (!string.IsNullOrEmpty(this.logPath)) {
					var pathParts = this.LogPath.Split('\\');
					if (!pathParts.Contains(this.ApplicationName)) {
						this.logPath = Path.Combine(this.LogPath, this.ApplicationName);
						if (!Directory.Exists(this.logPath)) {
							Directory.CreateDirectory(this.logPath);
						}
					}
					if (this.CreateNewFolderEachDay) {
						var dt = DateTime.Now.ToString("yyyy-MM-dd");
						if (!pathParts.Contains(dt)) {
							this.logPath = Path.Combine(this.LogPath, dt);
							if (!Directory.Exists(this.logPath)) {
								Directory.CreateDirectory(this.logPath);
							}
						}
					}
					this.LogFileName = Path.Combine(this.LogPath, $"{this.logFileBaseName}.{this.logExtension}");
				}
			}
		}
		private string logPath = default;
		public bool ReverseOrderLogging { get; private set; }
		public string LogFileName { get; private set; }
		public virtual void Write(string text, DateTime written, EntryTypes type) {
			this.ValidateFileSize();
			var id = !this.MessageStack.Any() ? 0 : this.MessageStack.Max(x => x.Id) + 1;
			var se = new StackEntry {
				Id = id,
				Text = text,
				Written = written,
				Type = type
			};
			if (this.IsImmediateWrite) {
				this.ProcessEntry(se);
			} else {
				this.MessageStack.Add(se);
			}
		}
		public bool CreateNewFolderEachDay { get; private set; }

		public virtual void WriteBulk(IList<InternalStorage> items) {
			var strings = items.Select(x => x.Text).ToList();
			var dateTimes = items.Select(x => x.TimeStamp).ToList();
			var types = items.Select(x => x.EntryType).ToList();
			this.WriteBulk(strings, dateTimes, types);
		}

		public virtual void WriteBulk(IList<string> items, IList<DateTime> itemsWitten, IList<EntryTypes> itemsType) {
			if (items.Count != itemsWitten.Count || items.Count != itemsType.Count || itemsWitten.Count != itemsType.Count) {
				throw new ApplicationException("Item count for all lists must match.");
			}

			for (var i = 0; i < items.Count; i++) {
				this.Write(items[i], itemsWitten[i], itemsType[i]);
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

		protected string GetDateValueForLog(DateTime value, int padSize = 0) => $"{value.ToString("yyyy-MM-dd hh:mm:ss tt")}".PadRight(padSize);

		protected string GetSplitDateValueForLog(DateTime value, int padSize = 0) => $"{value.ToString("yyyy-MM-dd")},{value.ToString("hh:mm:ss tt")}".PadRight(padSize);

		protected string GetTypesValueForLog(EntryTypes value, int padSize = 0) => $"{value.ToString()}".PadRight(padSize);

		protected void WriteReverseTextFile(string newEntry, EntryTypes type, DateTime written, bool returnOnError = false) {
			if (string.IsNullOrEmpty(this.LogFileName)) {
				return;
			}

			var previousData = default(string);
			this.ValidateFileSize();
			try {
				using (var fs = new FileStream(this.LogFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
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
			var newEntry = $"{this.GetSplitDateValueForLog(entry.Written)},{this.GetTypesValueForLog(entry.Type)},{this.GetMultiLineText(entry.Text, 0).Replace(",", " ")}";
			if (this.ReverseOrderLogging && File.Exists(this.LogFileName)) {
				this.WriteReverseTextFile(newEntry, entry.Type, entry.Written);
			} else {
				try {
					this.ValidateFileSize();
					using (var writer = File.AppendText(this.LogFileName)) {
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
				EventLog.CreateEventSource(this.ApplicationName, "Greg.Osborne");
			}
			catch { }

			//if (!EventLog.SourceExists(ApplicationName))
			//    EventLog.CreateEventSource(ApplicationName, "Greg.Osborne");

			var type = EventLogEntryType.Information;
			if (entry.Type == EntryTypes.Error) {
				type = EventLogEntryType.Error;
			} else if (entry.Type == EntryTypes.Warning) {
				type = EventLogEntryType.Warning;
			}

			EventLog.WriteEntry(this.ApplicationName, entry.Text, type);
		}
	}

	internal class FlatFileLogger : LoggerBase {
		private readonly object fileLock = new object();
		public FlatFileLogger(string applicationName, string logPath, bool reverseOrder, string logFileBaseName, bool createNewFolderEacDay, bool isImmediateWrite)
			: base(applicationName, logPath, reverseOrder, "log", logFileBaseName, createNewFolderEacDay, isWriteImmediate: isImmediateWrite) { }

		public override void ProcessEntry(StackEntry entry) {
			var newEntry = $"{this.GetDateValueForLog(entry.Written, 24)}{this.GetTypesValueForLog(entry.Type, EntryTypes.Information.ToString().Length + 2)}";
			newEntry += $"{this.GetMultiLineText(entry.Text, newEntry.Length + 3)}";
			if (this.ReverseOrderLogging && File.Exists(this.LogFileName)) {
				this.WriteReverseTextFile(newEntry, entry.Type, entry.Written);
				return;
			}

			try {
				this.ValidateFileSize();
				lock (this.fileLock) {
					if (!File.Exists(this.LogFileName)) {
						using (var fs = new FileStream(this.LogFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
						using (var writer = new StreamWriter(fs)) {
							writer.Write(newEntry);
						}
					} else {
						using (var fs = new FileStream(this.LogFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
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
			if (!File.Exists(this.LogFileName)) {
				doc = new XDocument(new XElement("log",
					new XElement("entries")));
			} else {
				doc = XDocument.Load(this.LogFileName);
			}

			var newElement = new XElement("item",
				new XAttribute("timestamp", entry.Written),
				new XAttribute("type", entry.Type));
			newElement.Add(new XCData(entry.Text));
			var entriesRoot = doc.Root.Element("entries");
			if (this.ReverseOrderLogging && File.Exists(this.LogFileName)) {
				var reverseElements = entriesRoot.Elements().OrderByDescending(x => DateTime.Parse(x.Attribute("timestamp").Value)).ToList();
				reverseElements.Insert(0, newElement);
				entriesRoot.RemoveAll();
				reverseElements.ForEach(x => entriesRoot.Add(x));
			} else {
				entriesRoot.Add(newElement);
			}
			doc.Save(this.LogFileName);
		}
	}
}
