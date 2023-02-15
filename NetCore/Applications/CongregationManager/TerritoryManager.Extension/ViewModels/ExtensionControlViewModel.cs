using Common.Applicationn;
using Common.Applicationn.Linq;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TerritoryManager.Extension.ViewModels {
    public class ExtensionControlViewModel : LocalBase {
        public ExtensionControlViewModel()
            : base() {

            Title = "Territory Manager [design]";
            Territories = new ObservableCollection<Territory>();
        }

        public override void Initialize(Settings appSettings, DataManager dataManager) {
            base.Initialize(appSettings, dataManager);

            Title = "Territory Manager";
        }

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

        public void SetCongregation(Congregation cong) {
            SelectedCongregation = cong;
#if DEBUG
            if(SelectedCongregation != null) {
                Debug.WriteLine(SelectedCongregation.Name);
            }
#endif
            if (SelectedCongregation != null && SelectedCongregation.Territories != null) {
                Territories.Clear();
                Territories.AddRange(SelectedCongregation.Territories);
            }
        }

        #region SelectedTerritory Property
        private Territory _SelectedTerritory = default;
        /// <summary>Gets/sets the SelectedTerritory.</summary>
        /// <value>The SelectedTerritory.</value>
        public Territory SelectedTerritory {
            get => _SelectedTerritory;
            set {
                _SelectedTerritory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Territories Property
        private ObservableCollection<Territory> _Territories = default;
        /// <summary>Gets/sets the Territories.</summary>
        /// <value>The Territories.</value>
        public ObservableCollection<Territory> Territories {
            get => _Territories;
            set {
                _Territories = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NewTerritoryCommand
        private DelegateCommand _NewTerritoryCommand = default;
        /// <summary>Gets the NewTerritory command.</summary>
        /// <value>The NewTerritory command.</value>
        public DelegateCommand NewTerritoryCommand => _NewTerritoryCommand ?? (_NewTerritoryCommand = new DelegateCommand(NewTerritory, ValidateNewTerritoryState));
        private bool ValidateNewTerritoryState(object state) => true;
        private void NewTerritory(object state) {
            var item = App.AddTerritory();
            if (item != null) {

            }
        }
        #endregion

        #region DeleteTerritoryCommand
        private DelegateCommand _DeleteTerritoryCommand = default;
        /// <summary>Gets the DeleteTerritory command.</summary>
        /// <value>The DeleteTerritory command.</value>
        public DelegateCommand DeleteTerritoryCommand => _DeleteTerritoryCommand ?? (_DeleteTerritoryCommand = new DelegateCommand(DeleteTerritory, ValidateDeleteTerritoryState));
        private bool ValidateDeleteTerritoryState(object state) => SelectedTerritory != null;
        private void DeleteTerritory(object state) {
            
        }
        #endregion

        #region CheckOutTerritoryCommand
        private DelegateCommand _CheckOutTerritoryCommand = default;
        /// <summary>Gets the CheckOutTerritory command.</summary>
        /// <value>The CheckOutTerritory command.</value>
        public DelegateCommand CheckOutTerritoryCommand => _CheckOutTerritoryCommand ?? (_CheckOutTerritoryCommand = new DelegateCommand(CheckOutTerritory, ValidateCheckOutTerritoryState));
        private bool ValidateCheckOutTerritoryState(object state) => SelectedTerritory != null;
        private void CheckOutTerritory(object state) {
            
        }
        #endregion

        #region CheckInTerritoryCommand
        private DelegateCommand _CheckInTerritoryCommand = default;
        /// <summary>Gets the CheckInTerritory command.</summary>
        /// <value>The CheckInTerritory command.</value>
        public DelegateCommand CheckInTerritoryCommand => _CheckInTerritoryCommand ?? (_CheckInTerritoryCommand = new DelegateCommand(CheckInTerritory, ValidateCheckInTerritoryState));
        private bool ValidateCheckInTerritoryState(object state) => SelectedTerritory != null;
        private void CheckInTerritory(object state) {
            
        }
        #endregion

        #region TerritoryNotesCommand
        private DelegateCommand _TerritoryNotesCommand = default;
        /// <summary>Gets the TerritoryNotes command.</summary>
        /// <value>The TerritoryNotes command.</value>
        public DelegateCommand TerritoryNotesCommand => _TerritoryNotesCommand ?? (_TerritoryNotesCommand = new DelegateCommand(TerritoryNotes, ValidateTerritoryNotesState));
        private bool ValidateTerritoryNotesState(object state) => SelectedTerritory != null;
        private void TerritoryNotes(object state) {
            
        }
        #endregion

        #region TerritoryDoNotCallCommand
        private DelegateCommand _TerritoryDoNotCallCommand = default;
        /// <summary>Gets the TerritoryDoNotCall command.</summary>
        /// <value>The TerritoryDoNotCall command.</value>
        public DelegateCommand TerritoryDoNotCallCommand => _TerritoryDoNotCallCommand ?? (_TerritoryDoNotCallCommand = new DelegateCommand(TerritoryDoNotCall, ValidateTerritoryDoNotCallState));
        private bool ValidateTerritoryDoNotCallState(object state) => SelectedTerritory != null;
        private void TerritoryDoNotCall(object state) {
            
        }
        #endregion

    }
}
