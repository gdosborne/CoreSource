using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GregOsborne.Application.Primitives;
using GregOsborne.Dialogs;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Developers.Suite {
	public partial class ExtensionManagerWindowView {
		public event ExecuteUiActionHandler ExecuteUiAction;

		private DelegateCommand okCommand = default;
		private DelegateCommand removeCommand = default;
		private DelegateCommand expandContractCommand = default;
		private DelegateCommand browseCommand = default;

		public DelegateCommand OKCommand => this.okCommand ?? (this.okCommand = new DelegateCommand(this.OK, this.ValidateOKState));
		public DelegateCommand RemoveCommand => this.removeCommand ?? (this.removeCommand = new DelegateCommand(this.Remove, this.ValidateRemoveState));
		public DelegateCommand BrowseCommand => this.browseCommand ?? (this.browseCommand = new DelegateCommand(this.Browse, this.ValidateBrowseState));
		public DelegateCommand ExpandContractCommand => this.expandContractCommand ?? (this.expandContractCommand = new DelegateCommand(this.ExpandContract, this.ValidateExpandContractState));


		private bool ValidateOKState(object state) => true;
		private bool ValidateRemoveState(object state) => this.SelectedExtension != null;
		private bool ValidateBrowseState(object state) => true;
		private bool ValidateExpandContractState(object state) => true;

		private void OK(object state) => this.DialogResult = true;

		private void Remove(object state) {
			try {
				ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("AskDeletePermanent"));
			}
			catch (Exception ex) {
				App.Current.As<App>().DisplayMessage(ex.Message, App.MessageTypes.Error, "Error", ButtonType.Ok);
			}
		}

		private void Browse(object state) {
			var p = new Dictionary<string, object> {
				{ "iscancel", false },
				{ "assemblyfilename", default(string) }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("BrowseForAssembly", p));
			if ((bool)p["iscancel"]) {
				return;
			}
			try {
				var dirName = Path.GetDirectoryName((string)p["assemblyfilename"]);
				var newDirName = Path.Combine(App.Current.As<App>().ApplicationDirectory, "Extensions", Path.GetFileNameWithoutExtension((string)p["assemblyfilename"]));
				var newName = this.CopyDir(Path.GetFileName((string)p["assemblyfilename"]), dirName, newDirName);
				App.Current.As<App>().ExtensionManager.AddExtension(newName, true);
			}
			catch (Exception ex) {
				App.Current.As<App>().DisplayMessage(ex.Message, App.MessageTypes.Error, "Error", ButtonType.Ok);
			}
		}

		private string CopyDir(string extensionFileName, string fromDir, string toDir) {
			if (!Directory.Exists(fromDir)) {
				return default;
			}

			var d1 = new DirectoryInfo(fromDir);
			var files = d1.GetFiles();
			if (files.Length == 0 || !files.Any(x => x.Name == extensionFileName)) {
				return default;
			}
			if (!Directory.Exists(toDir)) {
				Directory.CreateDirectory(toDir);
			}
			files.ToList().ForEach(x => {
				try {
					x.CopyTo(Path.Combine(toDir, x.Name), true);
				}
				catch { }
			});
			var newFiles = Directory.GetFiles(toDir);
			return newFiles.FirstOrDefault(x => Path.GetFileName(x) == extensionFileName);
		}

		private void ExpandContract(object state) => this.SecondLineVisibility = this.SecondLineVisibility == System.Windows.Visibility.Visible ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;

	}
}
