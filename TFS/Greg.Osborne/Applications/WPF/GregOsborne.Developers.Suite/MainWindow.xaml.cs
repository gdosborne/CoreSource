namespace GregOsborne.Developers.Suite {
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Threading;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows.Controls;
	using GregOsborne.Developers.Suite.Configuration;
	using GregOsborne.Dialogs;
	using GregOsborne.Suite.Extender;
	using GregOsborne.Suite.Extender.AppSettings;

	public partial class MainWindow : Window {
		public MainWindow() {
			this.InitializeComponent();
			Settings.ApplyWindowBounds(App.Current.As<App>().ApplicationName, this, new Rect(0, 0, 800, 600));
			this.View.Initialize();
			this.View.ExecuteUiAction += this.View_ExecuteUiAction;

			this.Closing += this.MainWindow_Closing;
			var dt = new DispatcherTimer {
				Interval = TimeSpan.FromMilliseconds(100)
			};
			dt.Tick += this.Dt_Tick;
			dt.Start();

			this.Closed += this.MainWindow_Closed;
			App.Current.As<App>().ExtensionManager.ExtensionAdded += this.ExtensionManager_ExtensionAdded;

		}

		private void ExtensionManager_ExtensionAdded(object sender, GregOsborne.Suite.Extender.ExtensionAddedEventArgs e) {
			this.AddTabFromExtension(e.Extension);
		}

		private void MainWindow_Closed(object sender, EventArgs e) {
			if (this.View.IsSaveRequired) {
				this.View.SaveConfigFileAsCommand.Execute(null);
			}
			App.Current.As<App>().ExitApplication();
		}

		private int initializationStep = 0;

		protected override void OnSourceInitialized(EventArgs e) => this.mainToolbar.RemoveOverflow();

		private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
			if (e.CommandToExecute == "ShowAboutWindow") {
				var aboutWin = new AboutWindow {
					Owner = this,
					WindowStartupLocation = WindowStartupLocation.CenterOwner
				};
				aboutWin.ShowDialog();
			} else if (e.CommandToExecute == "ShowSettingsWindow") {
				var settingsWin = new SettingsWindow {
					Owner = this,
					WindowStartupLocation = WindowStartupLocation.CenterOwner
				};
				settingsWin.View.Categories = this.View.Categories;
				var result = settingsWin.ShowDialog();
				if (!result.HasValue || !result.Value) {
					return;
				}
				this.ApplyExtensionSettings();
			} else if (e.CommandToExecute == "ShowManagerWindow") {
				var managerWin = new ExtensionManagerWindow {
					Owner = this,
					WindowStartupLocation = WindowStartupLocation.CenterOwner
				};
				var result = managerWin.ShowDialog();
				if (!result.HasValue || !result.Value) {
					return;
				}
				
			} else if(e.CommandToExecute == "ExitApplication") {
				App.Current.As<App>().ExitApplication();
			}
		}

		public void ApplyExtensionSettings() {
			while (App.Current.As<App>().ExtensionManager.HasNextExtension) {
				var ext = App.Current.As<App>().ExtensionManager.GetNextExtension();
				var generalCategory = this.View.Categories.First(x => x.Title == "General");
				generalCategory.SettingValues.ForEach(setting => {
					var wmOn = ((BoolSettingValue)setting).CurrentValue;
					ext.UpdateControlProperty(setting.Name, wmOn ? Visibility.Visible : Visibility.Hidden);
				});
			}
		}

		private void Dt_Tick(object sender, EventArgs e) {
			var dt = sender.As<DispatcherTimer>();
			dt.Stop();
			if (this.initializationStep == 0) {
				if (this.View.ConfigurationFile == null) {
					var td = new TaskDialog {
						AllowDialogCancellation = false,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						Content = $"There is no configuration file selected for use with {App.Current.As<App>().ApplicationName}.\n\n• If you have " +
							$"a configuration file, you can choose that file by selecting \"Select...\"\n• If this is the your first time using " +
							$"{App.Current.As<App>().ApplicationName} you can create a default configuration file\n• Select " +
							$"\"Exit\" if you are not ready at this time.",
						MainIcon = TaskDialogIcon.Information,
						MainInstruction = "Configuration file not set",
						MinimizeBox = false,
						Width = 300,
						WindowTitle = App.Current.As<App>().ApplicationName
					};
					td.Buttons.Add(new TaskDialogButton(ButtonType.Custom) { Text = "Select..." });
					td.Buttons.Add(new TaskDialogButton(ButtonType.Custom) { Text = "Create Default" });
					td.Buttons.Add(new TaskDialogButton(ButtonType.Custom) { Text = "Exit" });
					var tdResult = td.ShowDialog(this);
					if (tdResult.Text == "Exit") {
						Environment.Exit(0);
					}
					if (tdResult.Text == "Create Default") {
						this.View.ConfigurationFile = ConfigFile.CreateNew();
					} else {

					}
				}
				this.initializationStep++;
				dt.Start();
			} else if (this.initializationStep == 1) {
				var em = App.Current.As<App>().ExtensionManager;
				em.BeginRead();
				while (em.HasNextExtension) {
					var ext = em.GetNextExtension();
					this.AddTabFromExtension(ext);
				}
				this.initializationStep++;
				dt.Start();
			} else if (this.initializationStep == 2) {
				if (this.tabControls.Items.Count > 0) {
					this.tabControls.SelectedItem = this.tabControls.Items[0];
				}

				this.initializationStep++;
				dt.Start();
			}
		}

		private void AddTabFromExtension(IExtender extension) {
			var sp = new StackPanel {
				Orientation = Orientation.Horizontal,
				Background = extension.Control.Background
			};
			var icon = extension.GetIconTextBlock();
			icon.VerticalAlignment = VerticalAlignment.Center;
			icon.Margin = new Thickness(2, 2, 7, 2);
			sp.Children.Add(icon);
			var tb = new TextBlock {
				Text = extension.Title,
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(0, 2, 2, 2)
			};
			sp.Children.Add(tb);
			var ti = new TabItem {
				Header = sp,
				Content = extension.Control,

			};
			extension.TabItem = ti;
			this.tabControls.Items.Add(ti);
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.View.ConfigurationFile.IsSaveRequired) {
				this.View.ConfigurationFile.Save();
			}
			Settings.SaveWindowBounds(App.Current.As<App>().ApplicationName, this);
		}

		public MainWindowView View => this.DataContext.As<MainWindowView>();
	}
}
