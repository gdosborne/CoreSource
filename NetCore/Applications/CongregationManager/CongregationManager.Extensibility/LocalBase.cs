using Common;
using Common.MVVMFramework;
using CongregationManager.Data;

namespace CongregationExtension.ViewModels {
    public abstract class LocalBase : ViewModelBase {
        public virtual void Initialize(AppSettings appSettings, DataManager dataManager) {
            base.Initialize();

            AppSettings = appSettings;
            DataManager = dataManager;
        }

        public AppSettings AppSettings { get; set; }

        public DataManager DataManager { get; set; }

        public enum Actions {
            CloseWindow,
            AcceptData,
            AddMember,
            GroupSelected,
            ShowDoNotCall,
            ShowHistory,
            Generate,
            DeleteItem,
            CheckOutTerritory,
            CheckInTerritory,
            ReverseCheckoutTerritory,
            ShowTerritoryHistory,
            ShowNotes
        }

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand
            ??= new DelegateCommand(CloseWindow, ValidateCloseWindowState);
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) => ExecuteAction(nameof(Actions.CloseWindow));
        #endregion
    }
}
