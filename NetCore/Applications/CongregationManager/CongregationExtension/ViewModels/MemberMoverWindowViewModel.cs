using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.ObjectModel;

namespace CongregationExtension.ViewModels {
    public class MemberMoverWindowViewModel : LocalBase {
        public MemberMoverWindowViewModel()
            : base() {

            Title = "Member Mover [design]";
            Congregations = new ObservableCollection<Congregation>();
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Member Mover";
        }

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        /// <summary>Gets/sets the Congregations.</summary>
        /// <value>The Congregations.</value>
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
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

        #region AcceptCongregationCommand
        private DelegateCommand _AcceptCongregationCommand = default;
        /// <summary>Gets the AcceptCongregation command.</summary>
        /// <value>The AcceptCongregation command.</value>
        public DelegateCommand AcceptCongregationCommand => _AcceptCongregationCommand ?? (_AcceptCongregationCommand = new DelegateCommand(AcceptCongregation, ValidateAcceptCongregationState));
        private bool ValidateAcceptCongregationState(object state) => SelectedCongregation != null;
        private void AcceptCongregation(object state) {
            ExecuteAction(nameof(Actions.AcceptData));
        }
        #endregion

    }
}
