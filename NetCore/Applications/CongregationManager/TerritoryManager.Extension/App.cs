using Common.Applicationn.Logging;
using Common.Applicationn;
using CongregationManager.Data;
using System;
using static Common.Applicationn.Logging.ApplicationLogger;
using System.Text;

namespace TerritoryManager.Extension {
    internal static class App {
        public static void Main(string[] args) { }

        internal static ApplicationLogger logger { get; set; } = default;
        internal static Settings AppSettings { get; set; } = default;
        internal static DataManager DataManager { get; set; } = default;

        internal static void LogMessage(string message, EntryTypes type) {
            logger.LogMessage(new StringBuilder(message), type);
        }
    }
}
