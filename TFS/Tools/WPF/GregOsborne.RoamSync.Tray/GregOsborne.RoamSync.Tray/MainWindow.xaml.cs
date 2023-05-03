namespace GregOsborne.RoamSync.Tray {
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows.Dialog;

	public partial class MainWindow : Window {
		public MainWindow() {
			this.InitializeComponent();
			this.View.Initialize();
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
			this.Closing += this.MainWindow_Closing;

			this.Left = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "MainWindow Left", 0.0);
			this.Top = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "MainWindow Top", 0.0);
			this.Width = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "MainWindow Width", 575.0);
			this.Height = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "MainWindow Height", 200.0);
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.ExitApplication()) {
				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "MainWindow Left", this.RestoreBounds.Left);
				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "MainWindow Top", this.RestoreBounds.Top);
				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "MainWindow Width", this.RestoreBounds.Width);
				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "MainWindow Height", this.RestoreBounds.Height);
				this.taskBarIcon.Dispose();
				return;
			}

			e.Cancel = true;
		}

		private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
			if (e.CommandToExecute == "ExitApp") {
				this.Close();
			}
		}

		private bool ExitApplication() {
			var isAskToEditDisabled = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "Disable ask to exit", false);
			if (!isAskToEditDisabled) {
				var yesButton = new Dialogs.TaskDialogButton(Dialogs.ButtonType.Yes);
				var noButton = new Dialogs.TaskDialogButton(Dialogs.ButtonType.No);
				var result = this.ShowTaskDialog("Exit application", "Exit application",
					$"You are about to exit the application. Doing so will disable the syncronization " +
					$"of the Roaming folder.\n\nDo you want to quit?", Dialogs.TaskDialogIcon.Warning, null,
					"Do not ask if I want to quit again", out var isChecked, yesButton, noButton);

				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "Disable ask to exit", isChecked);
				if (result == noButton) {
					return false;
				}
			}
			return true;

		}

		public MainWindowView View => this.DataContext.As<MainWindowView>();
	}
}
