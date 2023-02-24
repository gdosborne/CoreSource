using Common.Application;
using Common.Application.Linq;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

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
                if (SelectedCongregation != null) {
                    SelectedCongregation.Territories.ForEach(t => {
                        t.History.ForEach(h => {
                            if (h.CheckedOutByID == 0)
                                return;
                            h.CheckedOutBy = SelectedCongregation.Members.FirstOrDefault(x => x.ID == h.CheckedOutByID);
                        });
                    });
                }
                OnPropertyChanged();
            }
        }
        #endregion

        public void SetCongregation(Congregation cong) {
            SelectedCongregation = cong;
#if DEBUG
            if (SelectedCongregation != null) 
                Debug.WriteLine(SelectedCongregation.Name);
#endif
            if (SelectedCongregation != null && SelectedCongregation.Territories != null)
                Refresh();
        }

        public void Refresh() {
            Territories.Clear();
            Territories.AddRange(SelectedCongregation.Territories);

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
        public DelegateCommand NewTerritoryCommand => _NewTerritoryCommand 
            ??= new DelegateCommand(NewTerritory, ValidateNewTerritoryState);
        private bool ValidateNewTerritoryState(object state) => true;
        private void NewTerritory(object state) {
            var item = App.AddTerritory();
            if (item != null) {
                DataManager.CurrentCongregation.Territories.Add(item);
                DataManager.SaveCongregation(DataManager.CurrentCongregation);
                Refresh();
            }
        }
        #endregion

        #region DeleteTerritoryCommand
        private DelegateCommand _DeleteTerritoryCommand = default;
        /// <summary>Gets the DeleteTerritory command.</summary>
        /// <value>The DeleteTerritory command.</value>
        public DelegateCommand DeleteTerritoryCommand => _DeleteTerritoryCommand 
            ??= new DelegateCommand(DeleteTerritory, ValidateDeleteTerritoryState);
        private bool ValidateDeleteTerritoryState(object state) => SelectedTerritory != null;
        private void DeleteTerritory(object state) => ExecuteAction(nameof(Actions.DeleteItem));
        #endregion

        #region CheckOutTerritoryCommand
        private DelegateCommand _CheckOutTerritoryCommand = default;
        /// <summary>Gets the CheckOutTerritory command.</summary>
        /// <value>The CheckOutTerritory command.</value>
        public DelegateCommand CheckOutTerritoryCommand => _CheckOutTerritoryCommand 
            ??= new DelegateCommand(CheckOutTerritory, ValidateCheckOutTerritoryState);
        private bool ValidateCheckOutTerritoryState(object state) => SelectedTerritory != null
            && (SelectedTerritory.LastHistory != null && SelectedTerritory.LastHistory.CheckInDate.HasValue);
        private void CheckOutTerritory(object state) => ExecuteAction(nameof(Actions.CheckOutTerritory));
        #endregion

        #region CheckInTerritoryCommand
        private DelegateCommand _CheckInTerritoryCommand = default;
        /// <summary>Gets the CheckInTerritory command.</summary>
        /// <value>The CheckInTerritory command.</value>
        public DelegateCommand CheckInTerritoryCommand => _CheckInTerritoryCommand 
            ??= new DelegateCommand(CheckInTerritory, ValidateCheckInTerritoryState);
        private bool ValidateCheckInTerritoryState(object state) => SelectedTerritory != null && 
            (SelectedTerritory.LastHistory != null && !SelectedTerritory.LastHistory.CheckInDate.HasValue);
        private void CheckInTerritory(object state) => ExecuteAction(nameof(Actions.CheckInTerritory));
        #endregion

        #region TerritoryNotesCommand
        private DelegateCommand _TerritoryNotesCommand = default;
        /// <summary>Gets the TerritoryNotes command.</summary>
        /// <value>The TerritoryNotes command.</value>
        public DelegateCommand TerritoryNotesCommand => _TerritoryNotesCommand 
            ??= new DelegateCommand(TerritoryNotes, ValidateTerritoryNotesState);
        private bool ValidateTerritoryNotesState(object state) => SelectedTerritory != null;
        private void TerritoryNotes(object state) => ExecuteAction(nameof(Actions.ShowNotes));
        #endregion

        #region TerritoryDoNotCallCommand
        private DelegateCommand _TerritoryDoNotCallCommand = default;
        /// <summary>Gets the TerritoryDoNotCall command.</summary>
        /// <value>The TerritoryDoNotCall command.</value>
        public DelegateCommand TerritoryDoNotCallCommand => _TerritoryDoNotCallCommand 
            ??= new DelegateCommand(TerritoryDoNotCall, ValidateTerritoryDoNotCallState);
        private bool ValidateTerritoryDoNotCallState(object state) => SelectedTerritory != null;
        private void TerritoryDoNotCall(object state) {

        }
        #endregion

        #region TerritoryBuilderCommand
        private DelegateCommand _TerritoryBuilderCommand = default;
        /// <summary>Gets the TerritoryBuilder command.</summary>
        /// <value>The TerritoryBuilder command.</value>
        public DelegateCommand TerritoryBuilderCommand => _TerritoryBuilderCommand 
            ??= new DelegateCommand(TerritoryBuilder, ValidateTerritoryBuilderState);
        private bool ValidateTerritoryBuilderState(object state) => true;
        private void TerritoryBuilder(object state) {
            var win = new BuildTerritoriesWindow();
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;
            foreach (var item in win.View.FinalResult) {
                if (!DataManager.CurrentCongregation.Territories.Any(x => x.Number == item)) {
                    var id = DataManager.CurrentCongregation.Territories.Count == 0
                        ? 1 : DataManager.CurrentCongregation.Territories.Max(x => x.ID) + 1;
                    var t = new Territory {
                        Number = item,
                        ID = id
                    };
                    DataManager.CurrentCongregation.Territories.Add(t);
                }
            }
            DataManager.SaveCongregation(DataManager.CurrentCongregation);
            Refresh();
        }
        #endregion

        #region ReverseCheckOutTerritoryCommand
        private DelegateCommand _ReverseCheckOutTerritoryCommand = default;
        /// <summary>Gets the ReverseCheckOutTerritory command.</summary>
        /// <value>The ReverseCheckOutTerritory command.</value>
        public DelegateCommand ReverseCheckOutTerritoryCommand => _ReverseCheckOutTerritoryCommand 
            ??= new DelegateCommand(ReverseCheckOutTerritory, ValidateReverseCheckOutTerritoryState);
        private bool ValidateReverseCheckOutTerritoryState(object state) => SelectedTerritory != null
            && (SelectedTerritory.LastHistory != null && !SelectedTerritory.LastHistory.CheckInDate.HasValue);
        private void ReverseCheckOutTerritory(object state) => ExecuteAction(nameof(Actions.ReverseCheckoutTerritory));
        #endregion

        #region TerritoryHistoryCommand
        private DelegateCommand _TerritoryHistoryCommand = default;
        /// <summary>Gets the TerritoryHistory command.</summary>
        /// <value>The TerritoryHistory command.</value>
        public DelegateCommand TerritoryHistoryCommand => _TerritoryHistoryCommand 
            ??= new DelegateCommand(TerritoryHistory, ValidateTerritoryHistoryState);
        private bool ValidateTerritoryHistoryState(object state) => SelectedTerritory != null
            && SelectedTerritory.History.Any();
        private void TerritoryHistory(object state) => ExecuteAction(nameof(Actions.ShowTerritoryHistory));
        #endregion

    }
}
