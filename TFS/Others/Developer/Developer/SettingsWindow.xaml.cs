namespace SNC.OptiRamp.Application.Developer {
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
	using GregOsborne.MVVMFramework;
	using SNC.OptiRamp.Application.Developer.Views;
	using GregOsborne.Application.Windows;

	internal partial class SettingsWindow : Window {
		public SettingsWindow() {
			InitializeComponent();
			View.Initialize(this);
			View.InitView();
		}

		public SettingsWindowView View {
			get {
				return LayoutRoot.GetView<SettingsWindowView>();
			}
		}
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
	}
}
