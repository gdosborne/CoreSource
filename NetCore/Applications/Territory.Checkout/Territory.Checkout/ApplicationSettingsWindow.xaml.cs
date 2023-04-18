namespace Territory.Checkout {
	using Common.OzApplication.Primitives;
	using Common.OzApplication.Windows;
	using System;
	using System.Windows;
	using Territory.Checkout.ViewModels;

	public partial class ApplicationSettingsWindow : Window {
		public ApplicationSettingsWindow() {
			InitializeComponent();

			View.Initialize();
		}

		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}

		internal ApplicationSettingsWindowView View => DataContext.As<ApplicationSettingsWindowView>();

		private void ApplicationSettingsWindowView_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {

		}

		private void ApplicationSettingsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "DialogResult") DialogResult = View.DialogResult;
		}
	}
}
