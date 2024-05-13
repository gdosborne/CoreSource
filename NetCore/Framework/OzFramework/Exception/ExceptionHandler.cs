/* File="ExceptionHandler"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/30/2024" */

using OzFramework.Logging;
using OzFramework.Primitives;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogHelper = OzFramework.Dialogs.Helpers;
using OD = Ookii.Dialogs.Wpf;

namespace OzFramework.Exception {
    public class ExceptionHandler {
        public ExceptionHandler(System.Exception exception, ApplicationLogger logger, string applicationName) {
            Exception = exception;
            this.logger = logger;
            ApplicationName = applicationName;
        }

        public string ApplicationName { get; private set; }
        public System.Exception Exception { get; private set; }
        private ApplicationLogger logger = default;

        public bool Handle(System.Windows.Window win) {
            var exceptionData = Exception.ToStringRecurse(isProcessDataIncluded: false);
            if (!exceptionData.IsNull()) {
                logger.LogMessage(exceptionData, ApplicationLogger.EntryTypes.Error);
                var msg = $"For the particulars of the error, look in the logs located at {logger.LogDirectory}. If " +
                    $"this error continues, please submit a bug report via the application. \n\n" +
                    $"Would you like to send this error information to the {ApplicationName} application team?";
                return DialogHelper.ShowYesNoDialogNew(win, $"{ApplicationName} Error",
                    $"An exception has occurred in the {ApplicationName} application", msg, OD.TaskDialogIcon.Error, 250).Value;
            }
            return false;
        }

        public bool Handle(System.Windows.Window win, string message) {
            if (!Exception.IsNull()) {
                var exceptionData = Exception.ToStringRecurse(isProcessDataIncluded: false);
                logger.LogMessage(exceptionData, ApplicationLogger.EntryTypes.Error);
            }
            if (!message.IsNull()) {
                return DialogHelper.ShowYesNoDialogNew(win, $"{ApplicationName} Error",
                    $"An exception has occurred in the {ApplicationName} application", message, OD.TaskDialogIcon.Error, 250).Value;
            }
            return false;
        }

    }
}
