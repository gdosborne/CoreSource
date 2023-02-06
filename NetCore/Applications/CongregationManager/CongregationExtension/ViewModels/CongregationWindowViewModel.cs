using Common.Applicationn;
using Common.Applicationn.Primitives;
using Common.Applicationn.Text;
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

        #region Congregation Property
        private Congregation _Congregation = default;
        /// <summary>Gets/sets the Congregation.</summary>
        /// <value>The Congregation.</value>
        public Congregation Congregation {
            get => _Congregation;
            set {
                _Congregation = value;
                if(Congregation != null) {
                    Congregation.PropertyChanged += Congregation_PropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        private void Congregation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (Congregation != null && Congregation.IsNew && e.PropertyName == "Name")
                Congregation.Filename = $"{Congregation.Name}.congregation";
            UpdateInterface();
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
            result &= Congregation != null && !string.IsNullOrEmpty(Congregation.Name);
            return result;
        }
        private void AcceptData(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion
    }
}
