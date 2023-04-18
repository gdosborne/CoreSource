namespace Territory.Checkout.ViewModels {
	using Common.MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Common.OzApplication.Primitives;

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
