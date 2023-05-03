namespace GregOsborne.Developers.Suite {
	using System;
	using System.Diagnostics;
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;

	public partial class AboutWindow : Window {
		public AboutWindow() {
			this.InitializeComponent();

			this.View.Initialize();
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
		}

		private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
			switch (e.CommandToExecute) {
				case "DisplaySystemInfoBox":
					new Process {
						StartInfo = new ProcessStartInfo {
							FileName = @"msinfo32.exe",
							UseShellExecute = true,
							WindowStyle = ProcessWindowStyle.Normal
						}
					}.Start();
					break;
				case "CloseAboutBox":
					this.Close();
					break;
			}
		}

		public AboutWindowView View => this.DataContext.As<AboutWindowView>();

		protected override void OnSourceInitialized(EventArgs e) => this.HideMinimizeAndMaximizeButtons();
	}
}
