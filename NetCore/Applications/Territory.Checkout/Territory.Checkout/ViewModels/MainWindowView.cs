namespace Territory.Checkout.ViewModels {
	using Common.MVVMFramework;
	using Common.OzApplication.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using Territory.Checkout.Data;
	using Universal.Common;

	internal class MainWindowView : ViewModelBase {
		public MainWindowView() {
			Title = $"{App.AppName} [designer]";
			SpinnerVisibility = Visibility.Collapsed;
		}

		public async override void Initialize() {
			base.Initialize();
			SpinnerVisibility = Visibility.Visible;
			Title = App.AppName;
			ErrorVisibility = Visibility.Hidden;
			var tempAreas = new List<AreaItem>(await AreaItem.GetAllAsync());
			PersonCheckoutItems = new ObservableCollection<CheckoutItem>();

			if (!tempAreas.Any(x => x.Name == "Unassigned")) {
				tempAreas.Add(await AreaItem.Add("Unassigned", "General purpose area"));
			}
			Areas = new ObservableCollection<AreaItem>(tempAreas.OrderBy(x => x.Name));

			var tempTerritories = new List<TerritoryItem>(await TerritoryItem.GetAllAsync());
			Territories = new ObservableCollection<TerritoryItem>(tempTerritories.OrderBy(x => x.Number.PadLeft(4, '0')));

			var tempPersons = new List<PersonItem>(await PersonItem.GetAllAsync());
			Persons = new ObservableCollection<PersonItem>(tempPersons.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));

			Checkouts = new ObservableCollection<CheckoutItem>(await CheckoutItem.GetAllAsync());
			var tempNeedsWorked = new List<NeedsWorkedItem>();
			Territories.ForEach(x => {
				var coItems = Checkouts.Where(y => y.Territory.ID == x.ID);
				var nw = NeedsWorkedItem.Create(x, coItems.ToList());
				if (nw != null) tempNeedsWorked.Add(nw);
			});
			ItemsNeedingWorked = new ObservableCollection<NeedsWorkedItem>(tempNeedsWorked.OrderByDescending(x => x.NumberOfDays));
			
			Areas.ForEach(item => item.PropertyChanged += Item_PropertyChanged);
			Territories.ForEach(item => item.PropertyChanged += Item_PropertyChanged);
			Persons.ForEach(item => item.PropertyChanged += Item_PropertyChanged);
			Checkouts.ForEach(item => item.PropertyChanged += Item_PropertyChanged);

			IsTerritoryExpanded = true;
		}

		private void Item_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			InvokePropertyChanged(e.PropertyName);
		}

		#region Areas Property
		private ObservableCollection<AreaItem> _Areas = default;
		public ObservableCollection<AreaItem> Areas {
			get => _Areas;
			set {
				_Areas = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Territories Property
		private ObservableCollection<TerritoryItem> _Territories = default;
		public ObservableCollection<TerritoryItem> Territories {
			get => _Territories;
			set {
				_Territories = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Persons Property
		private ObservableCollection<PersonItem> _Persons = default;
		public ObservableCollection<PersonItem> Persons {
			get => _Persons;
			set {
				_Persons = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Checkouts Property
		private ObservableCollection<CheckoutItem> _Checkouts = default;
		public ObservableCollection<CheckoutItem> Checkouts {
			get => _Checkouts;
			set {
				_Checkouts = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SpinnerVisibility Property
		private Visibility _SpinnerVisibility = default;
		public Visibility SpinnerVisibility {
			get => _SpinnerVisibility;
			set {
				_SpinnerVisibility = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region ErrorText Property
		private string _ErrorText = default;
		public string ErrorText {
			get => _ErrorText;
			set {
				_ErrorText = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsAreaExpanded Property
		private bool _IsAreaExpanded = default;
		public bool IsAreaExpanded {
			get => _IsAreaExpanded;
			set {
				_IsAreaExpanded = value;
				if (IsAreaExpanded) {
					IsTerritoryExpanded = false;
					IsPersonExpanded = false;
					IsCheckoutExpanded = false;

					SetDataVisibility(typeof(AreaItem));
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsTerritoryExpanded Property
		private bool _IsTerritoryExpanded = default;
		public bool IsTerritoryExpanded {
			get => _IsTerritoryExpanded;
			set {
				_IsTerritoryExpanded = value;
				if (IsTerritoryExpanded) {
					IsAreaExpanded = false;
					IsPersonExpanded = false;
					IsCheckoutExpanded = false;

					SetDataVisibility(typeof(TerritoryItem));
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsPersonExpanded Property
		private bool _IsPersonExpanded = default;
		public bool IsPersonExpanded {
			get => _IsPersonExpanded;
			set {
				_IsPersonExpanded = value;
				if (IsPersonExpanded) {
					IsAreaExpanded = false;
					IsTerritoryExpanded = false;
					IsCheckoutExpanded = false;

					SetDataVisibility(typeof(PersonItem));
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsCheckoutExpanded Property
		private bool _IsCheckoutExpanded = default;
		public bool IsCheckoutExpanded {
			get => _IsCheckoutExpanded;
			set {
				_IsCheckoutExpanded = value;
				if (IsCheckoutExpanded) {
					IsAreaExpanded = false;
					IsTerritoryExpanded = false;
					IsPersonExpanded = false;

					SetDataVisibility(typeof(CheckoutItem));
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region CheckoutInfo Property
		private string _CheckoutInfo = default;
		public string CheckoutInfo {
			get => _CheckoutInfo;
			set {
				_CheckoutInfo = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SelectedNeedsWorked Property
		private NeedsWorkedItem _SelectedNeedsWorked = default;
		public NeedsWorkedItem SelectedNeedsWorked {
			get => _SelectedNeedsWorked;
			set {
				_SelectedNeedsWorked = value;
				if (SelectedNeedsWorked != null) {
					SelectedTerritoryItem = SelectedNeedsWorked.Territory;
					ExecuteAction(nameof(Actions.MoveToSelectedTerritory));
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		private void SetDataVisibility(Type theType) {
			AreaDataVisibility = Visibility.Collapsed;
			TerritoryDataVisibility = Visibility.Collapsed;
			PersonDataVisibility = Visibility.Collapsed;
			if (theType != null) {
				if (theType == typeof(AreaItem)) AreaDataVisibility = Visibility.Visible;
				else if (theType == typeof(TerritoryItem)) TerritoryDataVisibility = Visibility.Visible;
				else if (theType == typeof(PersonItem)) PersonDataVisibility = Visibility.Visible;
			}
		}

		#region PersonDataVisibility Property
		private Visibility _PersonDataVisibility = default;
		public Visibility PersonDataVisibility {
			get => _PersonDataVisibility;
			set {
				var prevValue = PersonDataVisibility;
				_PersonDataVisibility = value;
				if (PersonDataVisibility == Visibility.Collapsed) {
					var hasChanges = Persons.Any(x => x.Status != DataItemBase.Statuses.Unchanged);
					if (hasChanges) {
						ErrorText = "Changes have been detected in the Persons - save changes before collapsing the area";
						ErrorVisibility = Visibility.Visible;
						IsAreaExpanded = true;
						return;
					}
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region AreaDataVisibility Property
		private Visibility _AreaDataVisibility = default;
		public Visibility AreaDataVisibility {
			get => _AreaDataVisibility;
			set {
				var prevValue = AreaDataVisibility;
				_AreaDataVisibility = value;
				if (AreaDataVisibility == Visibility.Collapsed) {
					var hasChanges = Areas.Any(x => x.Status != DataItemBase.Statuses.Unchanged);
					if (hasChanges) {
						ErrorText = "Changes have been detected in the Areas - save changes before collapsing the area";
						ErrorVisibility = Visibility.Visible;
						IsAreaExpanded = true;
						return;
					}
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region TerritoryDataVisibility Property
		private Visibility _TerritoryDataVisibility = default;
		public Visibility TerritoryDataVisibility {
			get => _TerritoryDataVisibility;
			set {
				var prevValue = TerritoryDataVisibility;
				_TerritoryDataVisibility = value;
				if (TerritoryDataVisibility == Visibility.Collapsed) {
					var hasChanges = Territories.Any(x => x.Status != DataItemBase.Statuses.Unchanged);
					if (hasChanges) {
						ErrorText = "Changes have been detected in the Territories - save changes before collapsing the area";
						ErrorVisibility = Visibility.Visible;
						IsTerritoryExpanded = true;
						return;
					}
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region ErrorVisibility Property
		private Visibility _ErrorVisibility = default;
		public Visibility ErrorVisibility {
			get => _ErrorVisibility;
			set {
				_ErrorVisibility = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SelectedAreaItem Property
		private AreaItem _SelectedAreaItem = default;
		public AreaItem SelectedAreaItem {
			get => _SelectedAreaItem;
			set {
				_SelectedAreaItem = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SelectedTerritoryItem Property
		private TerritoryItem _SelectedTerritoryItem = default;
		public TerritoryItem SelectedTerritoryItem {
			get => _SelectedTerritoryItem;
			set {
				_SelectedTerritoryItem = value;
				CheckoutInfo = string.Empty;
				if (SelectedTerritoryItem != null) {
					var tItem = Checkouts.FirstOrDefault(x => x.Territory.ID == SelectedTerritoryItem.ID
						&& !x.CheckedIn.HasValue);
					if (tItem != null) CheckoutInfo = $"This territory is checked out to " +
						$"{tItem.Person.FullName} since {tItem.CheckedOut.ToShortDateString()} " +
						$"({DateTime.Now.Subtract(tItem.CheckedOut).Days} days)";
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SelectedPersonItem Property
		private PersonItem _SelectedPersonItem = default;
		public PersonItem SelectedPersonItem {
			get => _SelectedPersonItem;
			set {
				_SelectedPersonItem = value;
				PersonCheckoutItems.Clear();
				var items = Checkouts.Where(x => x.Person.ID == SelectedPersonItem.ID).OrderByDescending(x => x.CheckedOut);
				PersonCheckoutItems.AddRange(items);
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsAreaItemEnabled Property
		private bool _IsAreaItemEnabled = default;
		public bool IsAreaItemEnabled {
			get => _IsAreaItemEnabled;
			set {
				_IsAreaItemEnabled = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsTerritoryItemEnabled Property
		private bool _IsTerritoryItemEnabled = default;
		public bool IsTerritoryItemEnabled {
			get => _IsTerritoryItemEnabled;
			set {
				_IsTerritoryItemEnabled = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsPersonItemEnabled Property
		private bool _IsPersonItemEnabled = default;
		public bool IsPersonItemEnabled {
			get => _IsPersonItemEnabled;
			set {
				_IsPersonItemEnabled = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region PersonCheckoutItems Property
		private ObservableCollection<CheckoutItem> _PersonCheckoutItems = default;
		public ObservableCollection<CheckoutItem> PersonCheckoutItems {
			get => _PersonCheckoutItems;
			set {
				_PersonCheckoutItems = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region ItemsNeedingWorked Property
		private ObservableCollection<NeedsWorkedItem> _ItemsNeedingWorked = default;
		public ObservableCollection<NeedsWorkedItem> ItemsNeedingWorked {
			get => _ItemsNeedingWorked;
			set {
				_ItemsNeedingWorked = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		internal enum Actions {
			AskDeleteArea,
			AskDeleteTerritory,
			AskDeletePerson,
			ShowCheckout,
			AskCheckIn,
			ShowNeedsWork,
			MoveToSelectedTerritory,
			ShowReports,
			ShowSettings
		}

		#region AddArea Command
		private DelegateCommand _AddAreaCommand = default;
		public DelegateCommand AddAreaCommand => _AddAreaCommand ??= new DelegateCommand(AddArea, ValidateAddAreaState);
		private bool ValidateAddAreaState(object state) => true;
		private async void AddArea(object state) {
			var name = "Unnamed";
			var index = 0;
			while (Areas.Any(x => x.Name == name)) {
				index++;
				name = $"Unnamed_{index}";
			}
			var description = "The new territory area";
			var result = await AreaItem.Add(name, description);
			result.IsLoading = false;
			result.PropertyChanged += Item_PropertyChanged;
			Areas.Add(result);
			SelectedAreaItem = result;
		}
		#endregion

		#region RemoveArea Command
		private DelegateCommand _RemoveAreaCommand = default;
		public DelegateCommand RemoveAreaCommand => _RemoveAreaCommand ??= new DelegateCommand(RemoveArea, ValidateRemoveAreaState);
		private bool ValidateRemoveAreaState(object state) => SelectedAreaItem != null;
		private void RemoveArea(object state) {
			if (SelectedAreaItem != null) {
				var p = new Dictionary<string, object> {
					{ "Cancel", false },
				};
				ExecuteAction(nameof(Actions.AskDeleteArea), p);
				if (!(bool)p["Cancel"]) return;
				if (SelectedAreaItem.Name != "Unassigned" && Territories.Any(x => x.Area.ID == SelectedAreaItem.ID)) {
					var unassignedArea = Areas.FirstOrDefault(x => x.Name == "Unassigned");
					Territories
						.Where(x => x.Area.ID == SelectedAreaItem.ID)
						.ToList()
						.ForEach(x => x.Area = unassignedArea);
				}
				SelectedAreaItem.Delete();
				Areas.Remove(SelectedAreaItem);
				IsAreaItemEnabled = false;
			}
		}
		#endregion

		#region UpdateArea Command
		private DelegateCommand _UpdateAreaCommand = default;
		public DelegateCommand UpdateAreaCommand => _UpdateAreaCommand ??= new DelegateCommand(UpdateArea, ValidateUpdateAreaState);
		private bool ValidateUpdateAreaState(object state) {
			return SelectedAreaItem != null &&
				(SelectedAreaItem.Status == DataItemBase.Statuses.Changed
				|| SelectedAreaItem.Status == DataItemBase.Statuses.NewlyAdded);
		}
		private void UpdateArea(object state) {
			SelectedAreaItem?.Update();
			ErrorVisibility = Visibility.Hidden;
		}
		#endregion

		#region AddTerritory Command
		private DelegateCommand _AddTerritoryCommand = default;
		public DelegateCommand AddTerritoryCommand => _AddTerritoryCommand ??= new DelegateCommand(AddTerritory, ValidateAddTerritoryState);
		private bool ValidateAddTerritoryState(object state) => true;
		private async void AddTerritory(object state) {
			var unassignedArea = Areas.FirstOrDefault(x => x.Name == "Unassigned");
			var lastNumber = Territories.OrderBy(x => x.Number.PadLeft(3, '0')).Last().Number.PadLeft(3, '0');
			var number = "0";
			if (int.TryParse(lastNumber, out var value)) {
				number = (value + 1).ToString();
			}
			else {
				number = number + "X";
			}
			var result = await TerritoryItem.Add(number, unassignedArea.ID.CastTo<int>());
			result.IsLoading = false;
			result.PropertyChanged += Item_PropertyChanged;
			Territories.Add(result);
			_SelectedTerritoryItem = result;
		}
		#endregion

		#region RemoveTerritory Command
		private DelegateCommand _RemoveTerritoryCommand = default;
		public DelegateCommand RemoveTerritoryCommand => _RemoveTerritoryCommand ??= new DelegateCommand(RemoveTerritory, ValidateRemoveTerritoryState);
		private bool ValidateRemoveTerritoryState(object state) => SelectedTerritoryItem != null;
		private void RemoveTerritory(object state) {
			if (SelectedTerritoryItem != null) {
				var p = new Dictionary<string, object> {
					{ "Cancel", false },
				};
				ExecuteAction(nameof(Actions.AskDeleteTerritory), p);
				if (!(bool)p["Cancel"]) return;
				SelectedTerritoryItem.Delete();
				if (Checkouts.Any(x => x.Territory.ID == SelectedTerritoryItem.ID)) {
					Checkouts.Where(x => x.Territory.ID == SelectedTerritoryItem.ID)
						.ToList()
						.ForEach(x => {
							x.Delete();
							Checkouts.Remove(x);
						});
				}
				Territories.Remove(SelectedTerritoryItem);
				IsTerritoryItemEnabled = false;
			}
		}
		#endregion

		#region UpdateTerritory Command
		private DelegateCommand _UpdateTerritoryCommand = default;
		public DelegateCommand UpdateTerritoryCommand => _UpdateTerritoryCommand ??= new DelegateCommand(UpdateTerritory, ValidateUpdateTerritoryState);
		private bool ValidateUpdateTerritoryState(object state) {
			return SelectedTerritoryItem != null &&
				(SelectedTerritoryItem.Status == DataItemBase.Statuses.Changed
				|| SelectedTerritoryItem.Status == DataItemBase.Statuses.NewlyAdded);
		}
		private void UpdateTerritory(object state) {
			SelectedTerritoryItem?.Update();
		}
		#endregion

		#region AddPerson Command
		private DelegateCommand _AddPersonCommand = default;
		public DelegateCommand AddPersonCommand => _AddPersonCommand ??= new DelegateCommand(AddPerson, ValidateAddPersonState);
		private bool ValidateAddPersonState(object state) => true;
		private async void AddPerson(object state) {
			var firstName = "FirstName";
			var lastName = "LastName";
			var result = await PersonItem.Add(lastName, firstName);
			result.IsLoading = false;
			result.PropertyChanged += Item_PropertyChanged;
			Persons.Add(result);
			SelectedPersonItem = result;
		}
		#endregion

		#region RemovePerson Command
		private DelegateCommand _RemovePersonCommand = default;
		public DelegateCommand RemovePersonCommand => _RemovePersonCommand ??= new DelegateCommand(RemovePerson, ValidateRemovePersonState);
		private bool ValidateRemovePersonState(object state) => _SelectedPersonItem != null;
		private void RemovePerson(object state) {
			if (SelectedPersonItem != null) {
				var p = new Dictionary<string, object> {
					{ "Cancel", false },
				};
				ExecuteAction(nameof(Actions.AskDeletePerson), p);
				if (!(bool)p["Cancel"]) return;
				SelectedPersonItem.Delete();
				if (Checkouts.Any(x => x.Person.ID == SelectedPersonItem.ID)) {
					Checkouts.Where(x => x.Person.ID == SelectedPersonItem.ID)
						.ToList()
						.ForEach(x => {
							x.Delete();
							Checkouts.Remove(x);
						});
				}
				Persons.Remove(SelectedPersonItem);
				IsPersonItemEnabled = false;
			}
		}
		#endregion

		#region UpdatePerson Command
		private DelegateCommand _UpdatePersonCommand = default;
		public DelegateCommand UpdatePersonCommand => _UpdatePersonCommand ??= new DelegateCommand(UpdatePerson, ValidateUpdatePersonState);
		private bool ValidateUpdatePersonState(object state) {
			return SelectedPersonItem != null &&
				(SelectedPersonItem.Status == DataItemBase.Statuses.Changed
				|| SelectedPersonItem.Status == DataItemBase.Statuses.NewlyAdded);
		}
		private void UpdatePerson(object state) {
			SelectedPersonItem?.Update();
			var saved = SelectedPersonItem;
			SelectedPersonItem = null;
			Persons = new ObservableCollection<PersonItem>(Persons.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));
			SelectedPersonItem = saved;
		}
		#endregion

		#region ShowCheckout Command
		private DelegateCommand _ShowCheckoutCommand = default;
		public DelegateCommand ShowCheckoutCommand => _ShowCheckoutCommand ??= new DelegateCommand(ShowCheckout, ValidateShowCheckoutState);
		private bool ValidateShowCheckoutState(object state) => true;
		private void ShowCheckout(object state) {
			var p = new Dictionary<string, object> {
				{ "screen", state }
			};
			ExecuteAction(nameof(Actions.ShowCheckout), p);
		}
		#endregion

		#region ShowCheckout1 Command
		private DelegateCommand _ShowCheckout1Command = default;
		public DelegateCommand ShowCheckout1Command => _ShowCheckout1Command ??= new DelegateCommand(ShowCheckout1, ValidateShowCheckout1State);
		private bool ValidateShowCheckout1State(object state) {
			if (SelectedTerritoryItem != null) {
				var tItem = Checkouts.FirstOrDefault(x => x.Territory.ID == SelectedTerritoryItem.ID
					&& !x.CheckedIn.HasValue);
				return tItem == null;
			}
			return true;
		}
		private void ShowCheckout1(object state) {
			var p = new Dictionary<string, object> {
				{ "screen", state }
			};
			ExecuteAction(nameof(Actions.ShowCheckout), p);
		}
		#endregion

		#region CheckIn Command
		private DelegateCommand _CheckInCommand = default;
		public DelegateCommand CheckInCommand => _CheckInCommand ??= new DelegateCommand(CheckIn, ValidateCheckInState);
		private bool ValidateCheckInState(object state) {
			if (Checkouts == null || SelectedTerritoryItem == null) return true;
			var cot = Checkouts.FirstOrDefault(x => x.Territory.ID == SelectedTerritoryItem.ID && !x.CheckedIn.HasValue);
			return cot != null;
		}
		private void CheckIn(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", true }
			};
			ExecuteAction(nameof(Actions.AskCheckIn), p);
			if ((bool)p["cancel"]) return;
			if (TerritoryDataVisibility == Visibility.Visible && SelectedTerritoryItem != null) {
				var cot = Checkouts.FirstOrDefault(x => x.Territory.ID == SelectedTerritoryItem.ID && !x.CheckedIn.HasValue);
				if (cot == null) return;
				cot.CheckedIn = DateTime.Now;
				cot.Update();
				var temp = SelectedTerritoryItem;
				SelectedTerritoryItem = null;
				SelectedTerritoryItem = temp;
			}
		}
		#endregion

		#region ShowNeedsWork Command
		private DelegateCommand _ShowNeedsWorkCommand = default;
		public DelegateCommand ShowNeedsWorkCommand => _ShowNeedsWorkCommand ??= new DelegateCommand(ShowNeedsWork, ValidateShowNeedsWorkState);
		private bool ValidateShowNeedsWorkState(object state) => true;
		private void ShowNeedsWork(object state) {
			ExecuteAction(nameof(Actions.ShowNeedsWork));
		}
		#endregion

		#region Report Command
		private DelegateCommand _ReportCommand = default;
		public DelegateCommand ReportCommand => _ReportCommand ??= new DelegateCommand(Report, ValidateReportState);
		private bool ValidateReportState(object state) => true;
		private void Report(object state) {
			ExecuteAction(nameof(Actions.ShowReports));
		}
		#endregion

		#region ShowSettings Command
		private DelegateCommand _ShowSettingsCommand = default;
		public DelegateCommand ShowSettingsCommand => _ShowSettingsCommand ??= new DelegateCommand(ShowSettings, ValidateShowSettingsState);
		private bool ValidateShowSettingsState(object state) => true;
		private void ShowSettings(object state) {
			ExecuteAction(nameof(Actions.ShowSettings));
		}
		#endregion
	}
}
