namespace SNC.OptiRamp.Application.Developer {
	using SNC.OptiRamp.Application.Developer.Views;
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
	using MVVMFramework;
	using GregOsborne.Application.Windows;

	internal partial class AboutBoxWindow : Window {
		public AboutBoxWindow() {
			InitializeComponent();
		}

		private void SplashWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
		}
		public SplashWindowView View {
			get {
				return LayoutRoot.GetView<SplashWindowView>();
			}
		}
	}
}
