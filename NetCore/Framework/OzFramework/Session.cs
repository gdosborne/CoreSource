/* File="Session"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.IO;
using Common.Logging;
using Common.Primitives;
using System;
using System.IO;

namespace Common {
    public sealed class Session {
        private WriteStatusHandler handler = default;

        public Session(string applicationDirectory, string applicationName, ApplicationLogger.StorageTypes logType, ApplicationLogger.StorageOptions logOptions, string smtpServer = null, WriteStatusHandler writeHandler = null, int memoryTimerCheckSeconds = 10) {
            handler = writeHandler;
            ApplicationName = applicationName;
            ApplicationDirectory = applicationDirectory;
            var settingsDirectry = System.IO.Path.Combine(ApplicationDirectory, "Settings");
            var logDirectory = System.IO.Path.Combine(ApplicationDirectory, "Logs");
            var tryCount = 0;
            var maxTryCount = 5;
            while (true) {
                try {
                    tryCount++;
                    ApplicationSettings = AppSettings.CreateFromApplicationSettingsFile(applicationName, settingsDirectry);
                    Logger = new ApplicationLogger(applicationName, logType, logDirectory, logOptions, "application", logOptions.HasFlag(ApplicationLogger.StorageOptions.CreateFolderForEachDay)) {
                        SingleFileMaxSizeInMB = 15
                    };
                    break;
                } catch {
                    if (tryCount >= maxTryCount) {
                        break;
                    }
                    if (ApplicationSettings.IsNull()) {
                        var d = new DirectoryInfo(settingsDirectry);
                        d.CreateIfNotExist();
                        foreach (var file in d.GetFiles()) {
                            file.Delete();
                        }
                        continue;
                    }
                    if (Logger.IsNull()) {
                        var curLogDir = System.IO.Path.Combine(logDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
                        var d = new DirectoryInfo(curLogDir);
                        d.CreateIfNotExist();
                        foreach (var file in d.GetFiles()) {
                            file.Delete();
                        }
                    }
                }
            }
            Email = new EMailer(smtpServer.IsNull() ? EMailer.DefaultSMTPServer : smtpServer);
            Memory = new Memory(TimeSpan.FromSeconds(memoryTimerCheckSeconds), false, writeHandler);
        }

        public string ApplicationDirectory { get; private set; } = null;

        public string ApplicationName { get; private set; }

        public AppSettings ApplicationSettings { get; private set; }

        public ApplicationLogger Logger { get; private set; }

        public EMailer Email { get; private set; }

        public Memory Memory { get; private set; }

        public bool IsTesting { get; set; } = false;
    }
}
