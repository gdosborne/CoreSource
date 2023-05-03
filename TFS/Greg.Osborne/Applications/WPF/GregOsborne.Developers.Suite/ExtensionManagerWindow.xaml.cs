namespace GregOsborne.Developers.Suite {
	using System;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;
	using GregOsborne.Dialogs;

	public partial class ExtensionManagerWindow : Window {
		public ExtensionManagerWindow() {
			this.InitializeComponent();
			this.View.Initialize();
			this.View.PropertyChanged += this.View_PropertyChanged;
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
		}

		private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
			var lastDir = Settings.GetSetting(App.Current.As<App>().ApplicationName, "General", "LastAssemblyDirectory", Path.GetDirectoryName(this.GetType().Assembly.Location));
			if (e.CommandToExecute == "AskDeletePermanent") {
				var permButton = new TaskDialogButton(ButtonType.Custom) { Text = "Permanent" };
				var sessionButton = new TaskDialogButton(ButtonType.Custom) { Text = "Session" };
				var cancelButton = new TaskDialogButton(ButtonType.Cancel);

				var result = App.Current.As<App>().DisplayMessage("How long would you like to keep this deletion in effect? If you select \"Permanent\" the extension assembly will be removed and the extension(s) in that assembly will need to be reinstalled.", App.MessageTypes.Warning, "Deletion Scope", permButton, sessionButton, cancelButton);
				var isPermanent = result == permButton.Text;
				if (result == "Cancel") {
					return;
				}
				var removed = App.Current.As<App>().ExtensionManager.RemoveExtension(this.View.SelectedExtension, isPermanent);
				for (var i = 0; i < removed.Count; i++) {
					this.View.Extensions.Remove(this.View.Extensions.ToList().FirstOrDefault(x => x.Id == removed[i].Id));
				}
			} else if (e.CommandToExecute == "BrowseForAssembly") {
				var dlg = new VistaOpenFileDialog {
					CheckFileExists = true,
					DefaultExt = ".dll",
					Filter = "Assemblies|*.dll|All files|*.*",
					InitialDirectory = lastDir,
					RestoreDirectory = true,
					Title = "Select extension assembly..."
				};
				var result = dlg.ShowDialog(this);
				if (!result.HasValue || !result.Value) {
					e.Parameters["iscancel"] = true;
					return;
				}
				e.Parameters["assemblyfilename"] = dlg.FileName;
				Settings.SetSetting(App.Current.As<App>().ApplicationName, "General", "LastAssemblyDirectory", Path.GetDirectoryName(dlg.FileName));
			}
		}

		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "DialogResult") {
				this.DialogResult = this.View.DialogResult;
			}
		}

		public ExtensionManagerWindowView View => this.DataContext.As<ExtensionManagerWindowView>();

		protected override void OnSourceInitialized(EventArgs e) => this.HideMinimizeAndMaximizeButtons();
	}
}
