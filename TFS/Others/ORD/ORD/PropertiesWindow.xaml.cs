namespace SNC.Applications.Developer
{
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
	using SNC.Applications.Developer.Views;
	using SNC.OptiRamp.Application.Extensions.Windows;

	public partial class PropertiesWindow : Window
	{
		public PropertiesWindow() {
			InitializeComponent();
		}
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
		public PropertiesWindowView View {
			get {
				return LayoutRoot.GetView<PropertiesWindowView>();
			}
		}

		private void PropertiesWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}
	}
}
