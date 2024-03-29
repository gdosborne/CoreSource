namespace SNC.OptiRamp.Application.Developer {

	using MVVMFramework;
	using SNC.OptiRamp.Application.Developer.Views;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	internal partial class SplashWindow : Window {

		#region Public Constructors
		public SplashWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private void SplashWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					Close();
					break;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			View.InitView();
			this.BringIntoView();
		}
		#endregion Private Methods

		#region Public Properties
		public SplashWindowView View {
			get {
				return LayoutRoot.GetView<SplashWindowView>();
			}
		}
		#endregion Public Properties
	}
}
