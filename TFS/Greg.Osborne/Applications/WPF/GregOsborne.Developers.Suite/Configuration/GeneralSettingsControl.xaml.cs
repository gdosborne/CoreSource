using System.Windows.Controls;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Developers.Suite.Configuration {
	/// <summary>
	/// Interaction logic for GeneralSettingsControl.xaml
	/// </summary>
	public partial class GeneralSettingsControl : UserControl, IViewParent {
		public GeneralSettingsControl() {
			this.InitializeComponent();
			this.View.Initialize();
		}

		public ViewModelBase View {
			get { return this.DataContext.As<GeneralSettingsControlView>(); }
		}

		//public GeneralSettingsControlView View => this.DataContext.As<GeneralSettingsControlView>();

	}
}
