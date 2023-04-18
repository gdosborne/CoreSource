namespace Territory.Checkout.ViewModels {
	using Common.MVVMFramework;
	using Common.OzApplication.Linq;
	using Common.OzApplication.OSSystem;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Territory.Checkout.Data;

	internal class CheckoutWindowView : ViewModelBase {
		public CheckoutWindowView() {
			Title = "Check Out Territory [designer]";
		}

		public override void Initialize() {
			base.Initialize();

			Title = "Check Out Territory";
			UpdateInterface();
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

		#region OK Command
		private DelegateCommand _OKCommand = default;
		public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
		private bool ValidateOKState(object state) => CheckoutDate.HasValue 
			&& Territory != null && CheckoutBy != null;
		private void OK(object state) {
			DialogResult = true;
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

		#region CheckoutDate Property
		private System.DateTime? _CheckoutDate = default;
		public System.DateTime? CheckoutDate {
			get => _CheckoutDate;
			set {
				_CheckoutDate = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region CheckoutBy Property
		private PersonItem _CheckoutBy = default;
		public PersonItem CheckoutBy {
			get => _CheckoutBy;
			set {
				_CheckoutBy = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Territory Property
		private TerritoryItem _Territory = default;
		public TerritoryItem Territory {
			get => _Territory;
			set {
				_Territory = value;
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
	}
}
