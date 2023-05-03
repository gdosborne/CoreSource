namespace EnableVersioning {
	using System;
	using System.Linq;
	using System.Text;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Threading;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;
	using GregOsborne.Dialogs;
	using VersionMaster;

	public partial class ProjectsWindow : Window {
		public ProjectsWindow() {
			this.InitializeComponent();
			this.View.Initialize();
			this.View.ExecuteUIAction += this.View_ExecuteUIAction;
			this.Closing += this.SchemaWindow_Closing;
		}

		private void SchemaWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.View.HasChanges) {
				var td = new TaskDialog {
					WindowTitle = $"Schema list has changed",
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Warning,
					Content = $"The schema list has changed. If you close this window now those changes will be lost." +
						$"\n\nAre you sure you want to Close this window?",
					MainInstruction = "Schema list has changed",
					Width = 200
				};
				td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
				td.Buttons.Add(new TaskDialogButton(ButtonType.No));
				var result = td.ShowDialog(App.Current.MainWindow);
				if (result.ButtonType == ButtonType.No) {
					e.Cancel = true;
					return;
				}
				this.View.Projects = new System.Collections.ObjectModel.ObservableCollection<ProjectData>(this.View.Copy);
			}
		}

		private void View_ExecuteUIAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
			switch (e.CommandToExecute) {
				case "close window": {
					this.Close();
					break;
				}
				case "remove schema": {
					var td = new TaskDialog {
						WindowTitle = $"Remove schema",
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Warning,
						Content = $"You are about to remove the selected schema. Any projects that are using that schema " +
							$"will be set to the default schema.\n\nAre you sure you want to remove this schema?",
						MainInstruction = "Remove schema?",
						Width = 200
					};
					td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
					td.Buttons.Add(new TaskDialogButton(ButtonType.No));
					var result = td.ShowDialog(App.Current.MainWindow);
					e.Parameters["cancel"] = result.ButtonType == ButtonType.No;
					break;
				}
				case "save projects": {
					try {
						this.Reader.Save();
						var isPrompt = App.Session.ApplicationSettings.GetValue("General", "isContinueNotifySaving", true);
						App.Session.Logger.LogMessage("Schema file saved", GregOsborne.Application.Logging.ApplicationLogger.EntryTypes.Information);
						var sb = new StringBuilder();
						this.View.Projects.ToList().ForEach(x => {
							var tmp = $"  {x.Name} - {x.SchemaName}";
							sb.AppendLine(tmp);
						});
						App.Session.Logger.LogMessage(sb, GregOsborne.Application.Logging.ApplicationLogger.EntryTypes.Information);
						if (isPrompt) {
							var td = new TaskDialog {
								WindowTitle = $"File saved",
								AllowDialogCancellation = true,
								ButtonStyle = TaskDialogButtonStyle.Standard,
								CenterParent = true,
								MainIcon = TaskDialogIcon.Information,
								Content = "The schema project configuration file has been saved.",
								MainInstruction = "File saved",
								VerificationText = "Continue to notify me about saving the schema file",
								IsVerificationChecked = true,
								Width = 200
							};
							td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
							td.ShowDialog(App.Current.MainWindow);
							App.Session.ApplicationSettings.AddOrUpdateSetting("General", "isContinueNotifySaving", td.IsVerificationChecked);
						}
					}
					catch (Exception ex) {
						var td = new TaskDialog {
							WindowTitle = $"File not saved",
							AllowDialogCancellation = true,
							ButtonStyle = TaskDialogButtonStyle.Standard,
							CenterParent = true,
							MainIcon = TaskDialogIcon.Warning,
							Content = $"The schema project configuration file has not been saved. The error was:\n\n{ex.ToString()}",
							MainInstruction = "File not saved",
							Width = 200
						};
						td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
						td.ShowDialog(App.Current.MainWindow);
					}
					break;
				}
			}
		}

		public ProjectConfigurationReader Reader { get; set; }

		public SchemaWindowview View => this.DataContext.As<SchemaWindowview>();

		protected override void OnSourceInitialized(EventArgs e) => this.HideMinimizeAndMaximizeButtons();

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			sender.As<TextBox>().BorderThickness = new Thickness(1);
			this.theLastTextBox = sender.As<TextBox>();
			var t = new DispatcherTimer {
				Interval = TimeSpan.FromMilliseconds(100)
			};
			t.Tick += this.T_Tick;
			t.Start();
		}

		private TextBox theLastTextBox = default;

		private void T_Tick(object sender, EventArgs e) {
			sender.As<DispatcherTimer>().Stop();
			if (this.theLastTextBox != null) {
				this.theLastTextBox.SelectAll();
			}
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().BorderThickness = new Thickness(0);

		private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			var item = ItemsControl.ContainerFromElement(sender.As<ListBox>(), e.OriginalSource as DependencyObject) as ListBoxItem;
			this.View.SelectedProject = item.As<ListBoxItem>().DataContext.As<ProjectData>();
		}
	}
}
