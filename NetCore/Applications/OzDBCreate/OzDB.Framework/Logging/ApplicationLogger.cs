namespace OzDB.Application.Logging {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using global::Application;

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
			this.Flush();
		}

		public ApplicationLogger(string applicationName, string logDirectory)
			: this(applicationName, logDirectory, StorageTypes.FlatFile) {
		}

		public ApplicationLogger(string applicationName, string logDirectory, StorageTypes storageType)
			: this(applicationName, storageType, logDirectory, StorageOptions.None, "AppLog") {
		}

		public ApplicationLogger(string applicationName, StorageTypes storageType, string logDirectory, StorageOptions options, string logFileBaseName, bool createNewFolderEachDay = false) {
			this.ApplicationName = applicationName;
			this.CreateNewFolderEachDay = createNewFolderEachDay;
			this.StorageType = storageType;
			this.LogFileExtension = this.StorageType == StorageTypes.FlatFile ? ".log" : this.StorageType == StorageTypes.CsvFile ? ".csv" : ".xml";
			this.Options = options;
			this.LogDirectory = logDirectory;
			this.Update();
			var isImmediate = !options.HasFlag(StorageOptions.StoreInMemoryUntilFlush);
			// 
			//flat file is default
			switch (this.StorageType) {
				case StorageTypes.CsvFile:
					this.logger = new CsvLogger(this.ApplicationName, this.LogDirectory, this.Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
					break;

				case StorageTypes.Xml:
					this.logger = new XmlLogger(this.ApplicationName, this.LogDirectory, this.Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
					break;

				case StorageTypes.WindowsLog:
					this.logger = new WindowsLogLogger(this.ApplicationName);
					break;

				default:
					this.logger = new FlatFileLogger(this.ApplicationName, this.LogDirectory, this.Options.HasFlag(StorageOptions.NewestFirstLogEntry), logFileBaseName, createNewFolderEachDay, isImmediate);
					break;
			}
			this.FileName = this.logger.LogFileName;
		}
        public string FileName { get; set; } = default;

		public ApplicationLogger UpdateStorageOptions(StorageOptions options) {
			this.Options = options;
			this.Update();
			return this;
		}

		private ApplicationLogger Update() {
			if (this.Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
				if (this.cache != null && this.cache.Any()) {
					this.Flush();
				}

				this.cache = new List<InternalStorage>();
			}
			if (this.Options.HasFlag(StorageOptions.CreateFolderForEachDay)) {
				if (!Directory.Exists(this.LogDirectory)) {
					Directory.CreateDirectory(this.LogDirectory);
				}

				this.LogDirectory = Path.Combine(this.LogDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
				if (!Directory.Exists(this.LogDirectory)) {
					Directory.CreateDirectory(this.LogDirectory);
				}
			}
			return this;
		}

		private IList<InternalStorage> cache = default;
		private ILogger logger = default;

		public bool IsWriteComplete => this.logger.IsWriteComplete;

		public string ApplicationName { get; private set; }
		public StorageTypes StorageType { get; set; }
		public double SingleFileMaxSizeInMB { get => this.logger.SingleLogFileMaxSize; set => this.logger.SingleLogFileMaxSize = value; }
		public string LogFileExtension { get; private set; }
		public string LogDirectory { get; set; }
		public StorageOptions Options { get; private set; }
		public bool CreateNewFolderEachDay { get; private set; }

		public ApplicationLogger LogMessage(StringBuilder text, EntryTypes type) {
			this.LogMessage(text.ToString(), type);
			return this;
		}

		public ApplicationLogger LogMessage(string text, EntryTypes type) {
			if (this.Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush)) {
				this.cache.Add(new InternalStorage {
					EntryType = type,
					TimeStamp = DateTime.Now,
					Text = text
				});
			} else {
				if (!string.IsNullOrEmpty(this.LogDirectory) && !string.IsNullOrEmpty(this.logger.LogPath) && !this.logger.LogPath.Equals(this.LogDirectory)) {
					this.logger.LogPath = this.LogDirectory;
				}

				this.logger.Write(text, DateTime.Now, type);

			}
			return this;
		}

		public ApplicationLogger Flush() {
			var hasStorageFlage = this.Options.HasFlag(StorageOptions.StoreInMemoryUntilFlush);
			if (!hasStorageFlage || (hasStorageFlage && (this.cache == null || !this.cache.Any()))) {
				return this;
			}

			this.logger.WriteBulk(this.cache);
			this.cache.Clear();
			return this;
		}
	}
}
