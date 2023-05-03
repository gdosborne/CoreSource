namespace Greg.Osborne.Installer.Builder {
	using System;
	using System.Windows;
	using System.Windows.Threading;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;

	public partial class SelectIconWindow : Window {
		public SelectIconWindow() {
			this.InitializeComponent();
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
			this.View.PropertyChanged += this.View_PropertyChanged;
			var t = new DispatcherTimer {
				Interval = TimeSpan.FromMilliseconds(100)
			};
			t.Tick += this.T_Tick;
			t.Start();
		}

		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName.Equals("DialogResult")) {
				this.DialogResult = this.View.DialogResult;
			}
		}

		private void T_Tick(object sender, EventArgs e) {
			sender.As<DispatcherTimer>().Stop();
			this.View.Initialize();
		}

		private void View_ExecuteUiAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
			if (e.CommandToExecute.Equals("GetIconAreaWidth")) {
				e.Parameters["areawidth"] = this._primaryBorder.ActualWidth;
			} else if (e.CommandToExecute.Equals("CenterParent")) {
				this.CenterOwner();
			} else if (e.CommandToExecute.Equals("SelectImage")) {
				this.View.CurrentIndex = (int)e.Parameters["index"];
				this.DialogResult = true;
			}
		}

		public SelectIconWindowView View => this.DataContext.As<SelectIconWindowView>();

		protected override void OnSourceInitialized(EventArgs e) => this.HideWindowParts(GregOsborne.Application.Windows.Extension.WindowParts.MaximizeButton | GregOsborne.Application.Windows.Extension.WindowParts.MinimizeButton);
	}
}
