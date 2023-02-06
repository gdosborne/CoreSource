using Common.Applicationn.Logging;
using System;
using System.Threading.Tasks;
using static Common.Applicationn.Logging.ApplicationLogger;

namespace CongregationExtension {
    internal class App {
        public static void Main(string[] args) { }

        internal ApplicationLogger logger = default;
        public static void LogMessage(string message, EntryTypes type) {
            switch (type) {
                case EntryTypes.Information:
                case EntryTypes.Warning:
                    Logger.LogMessage(message);
                    break;
                case EntryTypes.Error:
                    Logger.LogException(new Exception(message));
                    break;
            }
        }
    }
}
