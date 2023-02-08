using Common.Applicationn;
using Common.MVVMFramework;
using CongregationManager.Data;

namespace CongregationExtension.ViewModels {
    public abstract class LocalBase : ViewModelBase {
        public LocalBase() {
            AppSettings = App.AppSettings;
            DataManager = App.DataManager;
        }

        public Settings AppSettings { get; set; }
        public DataManager DataManager { get; set; }

        public enum Actions {
            CloseWindow,
            AcceptData,
            AddMember
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
