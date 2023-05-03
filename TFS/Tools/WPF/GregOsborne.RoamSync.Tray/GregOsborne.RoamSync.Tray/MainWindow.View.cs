using System;
using System.Windows;
using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace GregOsborne.RoamSync.Tray {
	public partial class MainWindowView : ViewModelBase {
		private bool isWindowInitializing = true;

		public MainWindowView() {
			this.WindowTitle = "Sync Roaming Folder (Design)";
			this.NextSynchronizationDate = DateTime.Now.AddHours(1);
			this.TotalFileCount = 100;
			this.SyncedFileCount = 0;
			this.UnsyncedFileCount = 100;
			this.AppFontSize = 12.0;
		}

		public override void Initialize() {
			this.isWindowInitializing = false;
			this.ShowHideText = "Hide Window";
			this.WindowVisibility = Visibility.Visible;
			this.WindowTitle = "Sync Roaming Folder";
			this.IsDisableAskToExitSet = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "Disable ask to exit", false);
			this.LastSynchronizationDate = null;
			this.NextSynchronizationDate = DateTime.Now.AddHours(1);
			this.TotalFileCount = 100;
			this.SyncedFileCount = 0;
			this.UnsyncedFileCount = 100;
			this.AllowedFontSizes = new System.Collections.ObjectModel.ObservableCollection<double>();
			for (double i = 8; i < 40; i++) {
				this.AllowedFontSizes.Add(i);
			}
			this.AppFontSize = App.Current.As<App>().SyncRoamingSession.ApplicationSettings.GetValue("General", "Application Font Size", 12.0);
		}

		private bool isDisableAskToExitSet = default;
		public bool IsDisableAskToExitSet {
			get => this.isDisableAskToExitSet;
			set {
				if (this.isWindowInitializing) {
					return;
				}

				this.isDisableAskToExitSet = value;
				App.Current.As<App>().SyncRoamingSession.ApplicationSettings.AddOrUpdateSetting("General", "Disable ask to exit", this.IsDisableAskToExitSet);
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private DateTime? lastSynchronizationDate = default;
		public DateTime? LastSynchronizationDate {
			get => this.lastSynchronizationDate;
			set {
				this.lastSynchronizationDate = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private DateTime nextSynchronizationDate = default;
		public DateTime NextSynchronizationDate {
			get => this.nextSynchronizationDate;
			set {
				this.nextSynchronizationDate = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int totalFileCount = default;
		public int TotalFileCount {
			get => this.totalFileCount;
			set {
				this.totalFileCount = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int syncedFileCount = default;
		public int SyncedFileCount {
			get => this.syncedFileCount;
			set {
				this.syncedFileCount = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int unsyncedFileCount = default;
		public int UnsyncedFileCount {
			get => this.unsyncedFileCount;
			set {
				this.unsyncedFileCount = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
