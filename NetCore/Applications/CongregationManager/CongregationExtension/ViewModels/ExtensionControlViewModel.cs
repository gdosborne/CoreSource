using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CongregationExtension.ViewModels {
    public class ExtensionControlViewModel : ViewModelBase {
        public ExtensionControlViewModel() {

        }

        public override void Initialize() {
            base.Initialize();

            ErrorVisibility = Visibility.Collapsed;
            Congregations = new ObservableCollection<Congregation>();
            Congregations.CollectionChanged += Congregations_CollectionChanged;
        }

        private void X_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsLocal") {
                ProcessLocalItems();
            }
        }

        private void Congregations_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                foreach (Congregation item in e.NewItems) {
                    item.PropertyChanged += X_PropertyChanged;
                }
            }
            ProcessLocalItems();

        }

        private void ProcessLocalItems() {
            if (Congregations != null && Congregations.Any()) {
                var isLocalCount = Congregations.Count(x => x.IsLocal);
                ErrorMessage = isLocalCount == 0 ? "You do not have a congregation marked as Local"
                    : isLocalCount > 1 ? "You have more than one congregation marked as Local" : default;
                SelectedCongregation ??= Congregations.FirstOrDefault(x => x.IsLocal);
            }
        }

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        /// <summary>Gets/sets the Congregations.</summary>
        /// <value>The Congregations.</value>
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
                ProcessLocalItems();
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedCongregation Property
        private Congregation _SelectedCongregation = default;
        /// <summary>Gets/sets the SelectedCongregation.</summary>
        /// <value>The SelectedCongregation.</value>
        public Congregation SelectedCongregation {
            get => _SelectedCongregation;
            set {
                _SelectedCongregation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ErrorMessage Property
        private string _ErrorMessage = default;
        /// <summary>Gets/sets the ErrorMessage.</summary>
        /// <value>The ErrorMessage.</value>
        public string ErrorMessage {
            get => _ErrorMessage;
            set {
                _ErrorMessage = value;
                ErrorVisibility = string.IsNullOrEmpty(ErrorMessage) ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ErrorVisibility Property
        private Visibility _ErrorVisibility = default;
        /// <summary>Gets/sets the ErrorVisibility.</summary>
        /// <value>The ErrorVisibility.</value>
        public Visibility ErrorVisibility {
            get => _ErrorVisibility;
            set {
                _ErrorVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
