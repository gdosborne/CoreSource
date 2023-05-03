namespace GregOsborne.RoamSync.Tray {
	using System.Collections.ObjectModel;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView {
		public event ExecuteUiActionHandler ExecuteUiAction;

		private string showHideText = default;
		public string ShowHideText {
			get => this.showHideText;
			set {
				this.showHideText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility windowVisibility = default;
		public Visibility WindowVisibility {
			get => this.windowVisibility;
			set {
				this.windowVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string windowTitle = default;
		public string WindowTitle {
			get => this.windowTitle;
			set {
				this.windowTitle = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private double appFontSize = default;
		public double AppFontSize {
			get => this.appFontSize;
			set {
				this.appFontSize = value;
				if (this.isWindowInitializing) {
					return;
				}

				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "Application Font Size", this.AppFontSize);
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<double> allowedFontSizes = default;
		public ObservableCollection<double> AllowedFontSizes {
			get => this.allowedFontSizes;
			set {
				this.allowedFontSizes = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
