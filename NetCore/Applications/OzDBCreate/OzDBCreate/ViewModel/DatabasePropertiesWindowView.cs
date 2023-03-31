using Common.MVVMFramework;

namespace OzDBCreate.ViewModel {
    public class DatabasePropertiesWindowView : ViewModelBase {
        public DatabasePropertiesWindowView() {

        }

        public override void Initialize() {
            base.Initialize();

        }

        #region commands

        #region OKCommand
        private DelegateCommand _OKCommand = default;
        /// <summary>Gets the OK command.</summary>
        /// <value>The OK command.</value>
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => true;
        private void OK(object state) {
            DialogResult = true;
        }
        #endregion

        #region CancelCommand
        private DelegateCommand _CancelCommand = default;
        /// <summary>Gets the Cancel command.</summary>
        /// <value>The Cancel command.</value>
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            DialogResult= false;
        }
        #endregion

        #endregion

        #region DialogResult Property
        private bool? _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DatabaseName Property
        private string _DatabaseName = default;
        /// <summary>Gets/sets the DatabaseName.</summary>
        /// <value>The DatabaseName.</value>
        public string DatabaseName {
            get => _DatabaseName;
            set {
                _DatabaseName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DatabaseDescription Property
        private string _DatabaseDescription = default;
        /// <summary>Gets/sets the DatabaseDescription.</summary>
        /// <value>The DatabaseDescription.</value>
        public string DatabaseDescription {
            get => _DatabaseDescription;
            set {
                _DatabaseDescription = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
