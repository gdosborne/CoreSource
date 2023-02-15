using Common.Applicationn;
using CongregationExtension.ViewModels;
using CongregationManager.Data;

namespace TerritoryManager.Extension.ViewModels {
    public class ExtensionControlViewModel : LocalBase {
        public ExtensionControlViewModel()
            : base() {

            Title = "Territory Manager [design]";
        }

        public override void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Territory Manager";
        }
    }
}
