namespace Territory.Checkout {
	using Common.OzApplication.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Shapes;
	using Territory.Checkout.ViewModels;

	/// <summary>
	/// Interaction logic for CheckoutWindow.xaml
	/// </summary>
	public partial class CheckoutWindow : Window {
		public CheckoutWindow() {
			InitializeComponent();
			View.Initialize();
		}

		internal CheckoutWindowView View => DataContext.As<CheckoutWindowView>();

		private void CheckoutWindowView_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {

		}

		private void CheckoutWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName.Equals("dialogresult", StringComparison.OrdinalIgnoreCase))
				DialogResult = View.DialogResult;
		}
	}
}
