namespace GregOsborne.RoamSync.Tray {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView {
		private DelegateCommand showHideWindowCommand = default;
		public DelegateCommand ShowHideWindowCommand => showHideWindowCommand ?? (showHideWindowCommand = new DelegateCommand(ShowHideWindow, ValidateShowHideWindowState));
		private bool ValidateShowHideWindowState(object state) => true;
		private void ShowHideWindow(object state) {
			this.WindowVisibility = this.WindowVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
			this.ShowHideText = this.WindowVisibility == Visibility.Visible ? "Hide Window" : "Show Window";
		}

		private DelegateCommand exitAppCommand = default;
		public DelegateCommand ExitAppCommand => exitAppCommand ?? (exitAppCommand = new DelegateCommand(ExitApp, ValidateExitAppState));
		private bool ValidateExitAppState(object state) => true;
		private void ExitApp(object state) {
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("ExitApp"));
		}

		
	}
}
