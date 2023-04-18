using Common.OzApplication.Logging;
using System;
using System.IO;

namespace Common.OzApplication {
    public sealed class Session {
        private WriteStatusHandler handler = default;

        public Session(string applicationDirectory, string applicationName, ApplicationLogger.StorageTypes logType, ApplicationLogger.StorageOptions logOptions, string smtpServer = null, WriteStatusHandler writeHandler = null, int memoryTimerCheckSeconds = 10) {
            handler = writeHandler;
            ApplicationName = applicationName;
            ApplicationDirectory = applicationDirectory;
            var settingsDirectry = Path.Combine(ApplicationDirectory, "Settings");
            var logDirectory = Path.Combine(ApplicationDirectory, "Logs");
            ApplicationSettings = AppSettings.CreateFromApplicationSettingsFile(applicationName, settingsDirectry);
            Logger = new ApplicationLogger(applicationName, logType, logDirectory, logOptions, "application", logOptions.HasFlag(ApplicationLogger.StorageOptions.CreateFolderForEachDay)) {
                SingleFileMaxSizeInMB = 15
            };

            Email = new EMailer(string.IsNullOrEmpty(smtpServer) ? EMailer.DefaultSMTPServer : smtpServer);
            Memory = new Memory(TimeSpan.FromSeconds(memoryTimerCheckSeconds), false, writeHandler);
        }

        public string ApplicationDirectory { get; private set; } = null;

        public string ApplicationName { get; private set; }

        public AppSettings ApplicationSettings { get; private set; }

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
