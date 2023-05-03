namespace GregOsborne.Item.Templates {
	using System.Windows;

	public partial class ViewModelWindow : Window {
		public ViewModelWindow() {
			this.InitializeComponent();
			this.View.Initialize();
		}

		public ViewModelWindowView View => this.DataContext as ViewModelWindowView;

		private void ViewModelWindowView_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) { }

		private void ViewModelWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) { }
	}
}
