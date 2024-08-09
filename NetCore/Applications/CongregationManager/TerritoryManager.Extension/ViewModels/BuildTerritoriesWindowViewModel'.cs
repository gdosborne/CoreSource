using Common;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System.Collections.Generic;
using System.Transactions;
using System.Windows;

namespace TerritoryManager.Extension.ViewModels {
    public class BuildTerritoriesWindowViewModel : LocalBase {
        public BuildTerritoriesWindowViewModel() =>
            Title = "Territory Builder [design]";

        public override void Initialize(AppSettings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            FinalMessageVisibility = Visibility.Hidden;
            HasRunThrough = false;

            AppSettings = appSettings;
            DataManager = dataManager;

            Title = "Territory Builder";
        }

        #region Groupings Property
        private string _Groupings = default;
        /// <summary>Gets/sets the Groupings.</summary>
        /// <value>The Groupings.</value>
        public string Groupings {
            get => _Groupings;
            set {
                _Groupings = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FinalMessageVisibility Property
        private Visibility _FinalMessageVisibility = default;
        /// <summary>Gets/sets the FinalMessageVisibility.</summary>
        /// <value>The FinalMessageVisibility.</value>
        public Visibility FinalMessageVisibility {
            get => _FinalMessageVisibility;
            set {
                _FinalMessageVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AcceptCommand
        private DelegateCommand _AcceptCommand = default;
        /// <summary>Gets the Accept command.</summary>
        /// <value>The Accept command.</value>
        public DelegateCommand AcceptCommand => _AcceptCommand 
            ??= new DelegateCommand(Accept, ValidateAcceptState);
        private bool ValidateAcceptState(object state) => HasRunThrough;
        private void Accept(object state) => ExecuteAction(nameof(Actions.AcceptData));
        #endregion

        #region CancelCommand
        private DelegateCommand _CancelCommand = default;
        /// <summary>Gets the Cancel command.</summary>
        /// <value>The Cancel command.</value>
        public DelegateCommand CancelCommand => _CancelCommand 
            ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) => ExecuteAction(nameof(Actions.CloseWindow));
        #endregion

        #region GenerateCommand
        private DelegateCommand _GenerateCommand = default;
        /// <summary>Gets the Generate command.</summary>
        /// <value>The Generate command.</value>
        public DelegateCommand GenerateCommand => _GenerateCommand 
            ??= new DelegateCommand(Generate, ValidateGenerateState);
        private bool ValidateGenerateState(object state) => !string.IsNullOrEmpty(Groupings);
        private void Generate(object state) {
            ExecuteAction(nameof(Actions.Generate));
            FinalMessageVisibility = Visibility.Visible;
            HasRunThrough = true;
        }
        #endregion

        #region HasRunThrough Property
        private bool _HasRunThrough = default;
        /// <summary>Gets/sets the HasRunThrough.</summary>
        /// <value>The HasRunThrough.</value>
        public bool HasRunThrough {
            get => _HasRunThrough;
            set {
                _HasRunThrough = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FinalResult Property
        private List<string> _FinalResult = default;
        /// <summary>Gets/sets the FinalResult.</summary>
        /// <value>The FinalResult.</value>
        public List<string> FinalResult {
            get => _FinalResult;
            set {
                _FinalResult = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
