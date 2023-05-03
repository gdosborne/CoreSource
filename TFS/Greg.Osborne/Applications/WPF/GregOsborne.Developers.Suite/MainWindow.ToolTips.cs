namespace GregOsborne.Developers.Suite {
	using GregOsborne.Application.Primitives;

	public partial class MainWindowView {

		private string openConfigFileTip = "Open configuration file";
		private string newConfigFileTip = "New configuration file";
		private string saveConfigFileTip = "Save configuration file";
		private string saveConfigFileAsTip = "Save configuration file as";
		private string exitApplicationTip = "Exit";
		private string undoTip = "Undo";
		private string redoTip = "Redo";
		private string cutTip = "Cut";
		private string copyTip = "Copy";
		private string settingsTip = "Settings";
		private string pasteTip = "Paste";
		private string managerTip = "Extension Manager";
		private string aboutTip = $"About { App.Current.As<App>().ApplicationName}";
		private string requiresSavingTip = "Configuration file requires saving";

		public string RequiresSavingTip {
			get => this.requiresSavingTip;
			set {
				this.requiresSavingTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string NewConfigFileTip {
			get => this.newConfigFileTip;
			private set {
				this.newConfigFileTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string OpenConfigFileTip {
			get => this.openConfigFileTip;
			private set {
				this.openConfigFileTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SaveConfigFileTip {
			get => this.saveConfigFileTip;
			set {
				this.saveConfigFileTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SaveConfigFileAsTip {
			get => this.saveConfigFileAsTip;
			set {
				this.saveConfigFileAsTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ExitApplicationTip {
			get => this.exitApplicationTip;
			set {
				this.exitApplicationTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string UndoTip {
			get => this.undoTip;
			set {
				this.undoTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string RedoTip {
			get => this.redoTip;
			set {
				this.redoTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string CutTip {
			get => this.cutTip;
			set {
				this.cutTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string CopyTip {
			get => this.copyTip;
			set {
				this.copyTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string PasteTip {
			get => this.pasteTip;
			set {
				this.pasteTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ManagerTip {
			get => this.managerTip;
			set {
				this.managerTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string AboutTip {
			get => this.aboutTip;
			set {
				this.aboutTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SettingsTip {
			get => this.settingsTip;
			set {
				this.settingsTip = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
