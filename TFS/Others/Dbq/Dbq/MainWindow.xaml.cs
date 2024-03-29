using Dbq.Views;
using GregOsborne.Application.Primitives;
using System.Windows;

namespace Dbq {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			//this.DataContext.As<MainWindowView>().ShowView += MainWindow_ShowView;
			this.DataContext.As<MainWindowView>().PropertyChanged += MainWindow_PropertyChanged;
		}

		private void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {

		}

		//private void MainWindow_ShowView(object sender, MVVMFramework.ShowViewEventArgs e) {
		//	if (e.View is SplashWindowView) {
		//		var aboutBoxWindow = new AboutBoxWindow {
		//			Owner = this,
		//			WindowStartupLocation = WindowStartupLocation.CenterOwner,
		//			DataContext = e.View
		//		};
		//		aboutBoxWindow.ShowDialog();
		//	}
		//}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Dbq.App.SetSettingValue<Point>("MainWindow", "Location", new Point(this.RestoreBounds.Left, this.RestoreBounds.Top));
			Dbq.App.SetSettingValue<Size>("MainWindow", "Size", new Size(this.RestoreBounds.Width, this.RestoreBounds.Height));
			Dbq.App.SetSettingValue<WindowState>("MainWindow", "WindowState", this.WindowState);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			var loc = Dbq.App.GetSettingValue<Point>("MainWindow", "Location", new Point(this.Left, this.Top));
			var siz = Dbq.App.GetSettingValue<Size>("MainWindow", "Size", new Size(this.Width, this.Height));
			var wstate = Dbq.App.GetSettingValue<WindowState>("MainWindow", "WindowState", this.WindowState);

			this.Left = loc.X;
			this.Top = loc.Y;
			this.Width = siz.Width;
			this.Height = siz.Height;
			this.WindowState = wstate;

		}


	}
}
