using Common.Applicationn;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryManager.Extension.ViewModels {
    public class TerritoryWindowViewModel : LocalBase {
        public TerritoryWindowViewModel() {
            Title = "Territory [design]";
        }

        public override void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Territory";
        }
    }
}
