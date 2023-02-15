using Common.Applicationn;
using Common.MVVMFramework;
using CongregationManager.Data;
using Controls.Core;

namespace CongregationExtension.ViewModels {
    public abstract class LocalBase : ViewModelBase {
        public virtual void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize();

            AppSettings = appSettings;
            DataManager = dataManager;
        }

        public Settings AppSettings { get; set; }
        public DataManager DataManager { get; set; }

        public enum Actions {
            CloseWindow,
            AcceptData,
            AddMember,
            GroupSelected
        }

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand ?? (_CloseWindowCommand = new DelegateCommand(CloseWindow, ValidateCloseWindowState));
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) {
            ExecuteAction(nameof(Actions.CloseWindow));
        }
        #endregion
    }
}
