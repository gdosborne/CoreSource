namespace Territory.Checkout.ViewModels {
	using Common.MVVMFramework;
	using Common.OzApplication.Primitives;
	using System.Collections.ObjectModel;

	internal class ApplicationSettingsWindowView : ViewModelBase {
		public ApplicationSettingsWindowView() {
			Title = "Application Settings [designer]";
		}

		public override void Initialize() {
			base.Initialize();

			Title = "Application Settings";
			FontSizes = new ObservableCollection<double>();
			for (double i = 10; i < 41; i++) {
				FontSizes.Add(i);
			}
			SelectedTextFontSize = App.AppSettings.GetValue("Application", "Font.Size", 13.0);
			var startup = App.AppSettings.GetValue("Application", "Startup", "Territory");
			IsAreaStartup = startup == "Area";
			IsTerritoryStartup = startup == "Territory";
			IsPersonStartup = startup == "Person";
			NeedsWorkedSelection = new ObservableCollection<int> {
				90, 180, 270, 365
			};
			NeedsWorkedSelected = App.NumberOfDaysNeedsWorked;
		}

		#region DialogResult Property
		private bool _DialogResult = default;
		public bool DialogResult {
			get => _DialogResult;
			set {
				_DialogResult = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region FontSizes Property
		private ObservableCollection<double> _FontSizes = default;
		public ObservableCollection<double> FontSizes {
			get => _FontSizes;
			set {
				_FontSizes = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region SelectedTextFontSize Property
		private double _SelectedTextFontSize = default;
		public double SelectedTextFontSize {
			get => _SelectedTextFontSize;
			set {
				_SelectedTextFontSize = value;
				App.AppSettings.AddOrUpdateSetting("Application", "Font.Size", SelectedTextFontSize);
				App.Current.As<App>().Resources["TextFontSize"] = SelectedTextFontSize;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsAreaStartup Property
		private bool _IsAreaStartup = default;
		public bool IsAreaStartup {
			get => _IsAreaStartup;
			set {
				_IsAreaStartup = value;
				if (IsAreaStartup) {
					App.AppSettings.AddOrUpdateSetting("Application", "Startup", "Area");
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsTerritoryStartup Property
		private bool _IsTerritoryStartup = default;
		public bool IsTerritoryStartup {
			get => _IsTerritoryStartup;
			set {
				_IsTerritoryStartup = value;
				if (IsTerritoryStartup) {
					App.AppSettings.AddOrUpdateSetting("Application", "Startup", "Territory");
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region IsPersonStartup Property
		private bool _IsPersonStartup = default;
		public bool IsPersonStartup {
			get => _IsPersonStartup;
			set {
				_IsPersonStartup = value;
				if (IsPersonStartup) {
					App.AppSettings.AddOrUpdateSetting("Application", "Startup", "Person");
				}
				InvokePropertyChanged();
			}
		}
		#endregion

		#region NeedsWorkedSelection Property
		private ObservableCollection<int> _NeedsWorkedSelection = default;
		public ObservableCollection<int> NeedsWorkedSelection {
			get => _NeedsWorkedSelection;
			set {
				_NeedsWorkedSelection = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region NeedsWorkedSelected Property
		private int _NeedsWorkedSelected = default;
		public int NeedsWorkedSelected {
			get => _NeedsWorkedSelected;
			set {
				NeedsWorkRequiresRefresh = value != NeedsWorkedSelected;
				_NeedsWorkedSelected = value;
				App.NumberOfDaysNeedsWorked = NeedsWorkedSelected;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region NeedsWorkRequiresRefresh Property
		private bool _NeedsWorkRequiresRefresh = default;
		public bool NeedsWorkRequiresRefresh {
			get => _NeedsWorkRequiresRefresh;
			set {
				_NeedsWorkRequiresRefresh = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Cancel Command
		private DelegateCommand _CancelCommand = default;
		public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
		private bool ValidateCancelState(object state) => true;
		private void Cancel(object state) {
			DialogResult = false;
		}
		#endregion
	}
}
