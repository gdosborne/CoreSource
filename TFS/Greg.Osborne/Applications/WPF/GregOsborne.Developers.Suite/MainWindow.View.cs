namespace GregOsborne.Developers.Suite {
	using System;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Developers.Suite.Configuration;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Suite.Extender.AppSettings;

	public partial class MainWindowView : ViewModelBase {
		public event ExecuteUiActionHandler ExecuteUiAction;
		public MainWindowView() {
			//any initialization you want displayed in the designer must be placed here in the constructor
			this.Title = "Developer's Suite";
		}

		public override void Initialize() {
			//runtime intiaialization
			this.Title = $"Developer's Suite - {Environment.UserName}";
			var cfgFileName = App.Current.As<App>().CurrentConfigurationFileName;
			this.ConfigurationFile = !string.IsNullOrEmpty(cfgFileName) && File.Exists(cfgFileName)
				? ConfigFile.Open(cfgFileName) : null;
			var generalCategory = new Category("General") {
				Control = new GeneralSettingsControl()
			};

			var val = GregOsborne.Application.Settings.GetSetting(App.Current.As<App>().ApplicationName, generalCategory.Path, Utilities.Shared.ShowatermarksOnExtensions, true);
			var b = new BoolSettingValue(Utilities.Shared.ShowatermarksOnExtensions, "ShowWatermarks") {
				DefaultValue = true,
				CurrentValue = val
			};
			generalCategory.SettingValues.Add(b);
			var extensionsCategory = new Category(null, "Extensions");
			while (App.Current.As<App>().ExtensionManager.HasNextExtension) {
				var ext = App.Current.As<App>().ExtensionManager.GetNextExtension();
				ext.InitializeSettings(extensionsCategory);
			}
			Utilities.Shared.UpdateExtensionSettings(generalCategory, extensionsCategory);
			this.Categories = new ObservableCollection<Category> {
				generalCategory,
				extensionsCategory
			};
			this.IsSaveRequired = false;
		}

		public bool IsSaveRequired {
			get => this.ConfigurationFile.IsSaveRequired;
			set {
				this.ConfigurationFile.IsSaveRequired = value;
				this.NeedsSavingVisibility = this.IsSaveRequired ? Visibility.Visible : Visibility.Hidden;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string title = default;
		public string Title {
			get => this.title;
			private set {
				this.title = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility needsSavingVisibility = default;
		public Visibility NeedsSavingVisibility {
			get => this.needsSavingVisibility;
			private set {
				this.needsSavingVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}


		private ConfigFile configurationFile = default;
		public ConfigFile ConfigurationFile {
			get => this.configurationFile;
			set {
				this.configurationFile = value;
				this.ConfigfileName = this.ConfigurationFile == null ? "Unknown" : this.ConfigurationFile.FileName;
				if (this.ConfigurationFile != null) {
					GregOsborne.Application.Settings.SetSetting(App.Current.As<App>().ApplicationName, "Application", "Configuration File Name", this.ConfigurationFile.FileName);
				}

				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string configfileName = default;
		public string ConfigfileName {
			get => this.configfileName;
			set {
				this.configfileName = value;
				App.Current.As<App>().CurrentConfigurationFileName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<Category> categories = default;
		public ObservableCollection<Category> Categories {
			get => this.categories;
			set {
				this.categories = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
