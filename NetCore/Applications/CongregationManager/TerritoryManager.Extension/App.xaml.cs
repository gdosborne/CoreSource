using Common.Logging;
using Common;
using CongregationManager.Data;
using System;
using static Common.Logging.ApplicationLogger;
using System.Text;
using static Common.Dialogs.Helpers;
using System.Linq;

namespace TerritoryManager.Extension {
    public partial class App : System.Windows.Application {

        internal static ApplicationLogger logger { get; set; } = default;
        internal static AppSettings AppSettings { get; set; } = default;
        internal static DataManager DataManager { get; set; } = default;

        internal static void LogMessage(string message, EntryTypes type) {
            logger.LogMessage(new StringBuilder(message), type);
        }

        internal static Territory? AddTerritory() {
            var territory = new Territory {
                ID = 0
            };

            var win = new TerritoryWindow();
            win.View.Territory = territory;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return null;
            if (territory.ID == 0) {
                territory.ID = DataManager.CurrentCongregation.Territories.Count == 0
                    ? 1 : DataManager.CurrentCongregation.Territories.Max(x => x.ID) + 1;
            }
            return territory;
        }
    }
}
