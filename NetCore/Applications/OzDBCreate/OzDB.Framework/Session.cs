namespace OzDB.Application {
	using System;
	using System.IO;
	using System.Windows;
	using OzDB.Application.Logging;

	public sealed class Session {
		private WriteStatusHandler handler = default;

		public Session(string applicationDirectory, string applicationName, ApplicationLogger.StorageTypes logType, ApplicationLogger.StorageOptions logOptions, string smtpServer = null, WriteStatusHandler writeHandler = null, int memoryTimerCheckSeconds = 10) {
			this.handler = writeHandler;
			this.ApplicationName = applicationName;
			this.ApplicationDirectory = applicationDirectory;
			var settingsDirectry = Path.Combine(this.ApplicationDirectory, "Settings");
			var logDirectory = Path.Combine(this.ApplicationDirectory, "Logs");
			this.ApplicationSettings = Settings.CreateFromApplicationSettingsFile(applicationName, settingsDirectry);
			this.Logger = new ApplicationLogger(applicationName, logType, logDirectory, logOptions, "application", logOptions.HasFlag(ApplicationLogger.StorageOptions.CreateFolderForEachDay)) {
				SingleFileMaxSizeInMB = 15
			};

			this.Email = new EMailer(string.IsNullOrEmpty(smtpServer) ? EMailer.DefaultSMTPServer : smtpServer);
			this.Memory = new Memory(TimeSpan.FromSeconds(memoryTimerCheckSeconds), false, writeHandler);
		}
				
		public string ApplicationDirectory { get; private set; } = null;

		public string ApplicationName { get; private set; }

		public Settings ApplicationSettings { get; private set; }

		public ApplicationLogger Logger { get; private set; }

		public EMailer Email { get; private set; }

		public Memory Memory { get; private set; }

		public bool IsTesting { get; set; } = false;

		public enum EmailTypes {
			InvalidNotificationError = -5,
			CouldNotGetTemplate,
			ApplicationError,
			SqlError,
			MissingBusinessInfo,
			Unspecified,
			Approved,
			NoManufacturer,
			OnHold,
			NotificationAlreadySent,
			NotificationRequest,
			InvalidNotification,
			ContractIdInvalid,
			UpdateToDefaultOptOut,
			MissingOrInvalidEmail,
			Success
		}
	}
}
