namespace Counties {
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using Data.Classes;

	public partial class MainWindow : Window {
		public MainWindow() {
			this.InitializeComponent();
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
			this.View.Initialize();
		}

		private void View_ExecuteUiAction(object sender, MVVM.ExecuteUiActionEventArgs e) {
			if(e.CommandToExecute == "findcounty") {
				Task.Factory.StartNew(() => {
					this.Dispatcher.Invoke(new Action(() => this.View.SelectedCounty = this.View.AllCounties.FirstOrDefault(x => x.ID == (int)e.Parameters["value"])));
				});
			}
			this.firstControl.Focus();
			this.firstControl.SelectAll();
		}

		public MainWindowView View => this.DataContext as MainWindowView;

		private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			Task.Factory.StartNew(() => {
				this.Dispatcher.Invoke(new Action(() => this.View.SelectedCounty = ((sender as ListBox).SelectedItem as County)));
			});
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) => (sender as TextBox).SelectAll();
	}
}
