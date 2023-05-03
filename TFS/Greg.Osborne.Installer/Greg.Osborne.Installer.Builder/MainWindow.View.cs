using System;
using System.Windows;
using System.Windows.Threading;
using Greg.Osborne.Installer.Support;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace Greg.Osborne.Installer.Builder {
	public partial class MainWindowView : ViewModelBase {

		public MainWindowView() {
		}

		private DispatcherTimer statusTimer = default;
		private string installationFileName = default;
		private InstallerController controller = default;
		private Visibility dirtyIndicatorVisibility = default;

		public event ExecuteUiActionHandler ExecuteUiAction;

		public override void Initialize() {
			InstallerController.FileLoaded += this.InstallerController_FileLoaded;
			this.DirtyIndicatorVisibility = Visibility.Hidden;
			this.statusTimer = new DispatcherTimer {
				Interval = TimeSpan.FromSeconds(1)
			};
			this.statusTimer.Tick += this.StatusTimer_Tick;
			this.statusTimer.Start();
		}

		private void StatusTimer_Tick(object sender, EventArgs e) {
			this.DirtyIndicatorVisibility = this.Controller == null || !this.Controller.HasChanges ? Visibility.Hidden : Visibility.Visible;
		}

		private void InstallerController_FileLoaded(object sender, System.EventArgs e) {
			if (sender != null) {
				this.InstallationFileName = sender.As<InstallerController>().Filename;
			}
		}

		public string InstallationFileName {
			get => this.Controller == null ? "No file selected" : this.Controller.Filename;
			private set {
				this.installationFileName = value;
				this.UpdateInterface();
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public InstallerController Controller {
			get => this.controller;
			set {
				if (this.Controller != null) {
					this.Controller.FilenameHasChanged -= this.Controller_FilenameHasChanged;
				}

				this.controller = value;
				if (this.Controller != null) {
					this.InstallationFileName = this.Controller.Filename;
					this.Controller.HasChanges = false;
					this.Controller.SideItems.ForEach(sideItem => sideItem.ExecuteUiAction += this.SideItem_ExecuteUiAction);
					this.Controller.FilenameHasChanged += this.Controller_FilenameHasChanged;
				}
				this.UpdateInterface();
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private void SideItem_ExecuteUiAction(object sender, ExecuteUiActionEventArgs e) {
			this.ExecuteUiAction.Invoke(this, e);
		}

		public Visibility DirtyIndicatorVisibility {
			get => this.dirtyIndicatorVisibility;
			set {
				this.dirtyIndicatorVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private void Controller_FilenameHasChanged(object sender, Support.Events.FilenameChangedEventArgs e) => this.InstallationFileName = sender.As<InstallerController>().Filename;
	}
}
