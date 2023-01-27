using Common.Applicationn;
using Common.MVVMFramework;
using CongregationManager.Data;

namespace CongregationExtension.ViewModels {
    public class CongregationWindowViewModel : ViewModelBase {
        public CongregationWindowViewModel() {
            Title = "Congregation [design]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Congregation";
        }

        public enum Actions {
            CloseWindow,
            AcceptData
        }

        public Settings AppSettings { get; set; }
        public DataManager DataManager { get; set; }

        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

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

        #region AcceptDataCommand
        private DelegateCommand _AcceptDataCommand = default;
        /// <summary>Gets the AcceptData command.</summary>
        /// <value>The AcceptData command.</value>
        public DelegateCommand AcceptDataCommand => _AcceptDataCommand ?? (_AcceptDataCommand = new DelegateCommand(AcceptData, ValidateAcceptDataState));
        private bool ValidateAcceptDataState(object state) {
            var result = true;
            result &= !string.IsNullOrEmpty(Name);
            return result;
        }
        private void AcceptData(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion
    }
}
