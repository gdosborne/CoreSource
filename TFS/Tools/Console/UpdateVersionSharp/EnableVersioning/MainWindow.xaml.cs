namespace EnableVersioning {
	using System;
	using System.IO;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;
	using GregOsborne.Dialogs;

	public partial class MainWindow : Window {

		private void ConsoleTextBox_TextChanged(object sender, TextChangedEventArgs e) {
			sender.As<TextBox>().CaretIndex = sender.As<TextBox>().Text.Length;
			sender.As<TextBox>().ScrollToEnd();
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

		private void View_ExecuteUIAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e) {
			switch (e.CommandToExecute) {
				case "display file saved": {
					var isContinueNotifySaving = App.Session.ApplicationSettings.GetValue("General", "isContinueNotifySaving", false);
					if (isContinueNotifySaving) {
						var td = new TaskDialog {
							WindowTitle = $"File saved",
							AllowDialogCancellation = true,
							ButtonStyle = TaskDialogButtonStyle.Standard,
							CenterParent = true,
							MainIcon = TaskDialogIcon.Information,
							Content = "The schema project configuration file has been saved.",
							MainInstruction = "File saved",
							Width = 200,
							VerificationText = "Continue to be notified of save",
							IsVerificationChecked = isContinueNotifySaving
						};
						td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
						td.ShowDialog(App.Current.MainWindow);
						App.Session.ApplicationSettings.AddOrUpdateSetting("General", "isContinueNotifySaving", td.IsVerificationChecked);
					}
					break;
				}

				case "ask to exit": {
					break;
				}
				case "get project file": {
					var initialDir = (string)e.Parameters["initialdirectory"];
					var directoryName = string.IsNullOrEmpty(initialDir) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : initialDir;
					var fileName = "*.csproj";
					if (!string.IsNullOrEmpty(this.View.ProjectFileName)) {
						directoryName = Path.GetDirectoryName(this.View.ProjectFileName);
						fileName = Path.GetFileName(this.View.ProjectFileName);
					}
					var dlg = new VistaOpenFileDialog {
						CheckFileExists = true,
						FileName = "*.csproj",
						Filter = "CSharp project files|*.csproj",
						InitialDirectory = directoryName,
						Multiselect = false,
						ShowReadOnly = false,
						RestoreDirectory = true,
						Title = "Select CSharp project file..."
					};
					var result1 = dlg.ShowDialog(this);
					e.Parameters["cancel"] = !result1.HasValue || !result1.Value;
					e.Parameters["filename"] = dlg.FileName;
					break;
				}
				case "ask to change schema": {
					if (!string.IsNullOrEmpty(this.View.ProjectFileName) && this.View.SelectedSchema != null) {
						var projectName = Path.GetFileNameWithoutExtension(this.View.ProjectFileName);
						var msg = $"You are about to make the following changes to the {projectName} project:{Environment.NewLine}";
						msg += $"{new string(' ', 4)}Schema: {this.View.SelectedSchema.Name}{Environment.NewLine}";
						msg += $"Are you sure you want to make these changes?";
						var td = new TaskDialog {
							WindowTitle = $"Change schema",
							AllowDialogCancellation = true,
							ButtonStyle = TaskDialogButtonStyle.Standard,
							CenterParent = true,
							MainIcon = TaskDialogIcon.Information,
							Content = msg,
							MainInstruction = "Change schema?",
							Width = 200
						};
						td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
						td.Buttons.Add(new TaskDialogButton(ButtonType.No));
						var result = td.ShowDialog(App.Current.MainWindow);
						e.Parameters["cancel"] = result.ButtonType != ButtonType.Yes;
					}
					break;
				}
				case "ask create project": {
					var td1 = new TaskDialog {
						WindowTitle = $"Project is undefined",
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Warning,
						Content = $"The project {this.View.ProjectName} has no settings. Would you like to create a project reference for {this.View.ProjectName}?",
						MainInstruction = $"Project is undefined",
						Width = 200
					};
					td1.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
					td1.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
					e.Parameters["cancel"] = td1.ShowDialog(App.Current.MainWindow).Text == "Cancel";
					break;
				}
				case "display targets exists": {
					var td1 = new TaskDialog {
						WindowTitle = "The targets file exists",
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Warning,
						Content = $"The file {e.Parameters["targetsfilename"]} exists. Responding with Yes " +
							$"causes this file to be overwritten.\n\nAre you sure you would like to do this?",
						MainInstruction = "The .targets file exists",
						Width = 200
					};
					td1.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
					td1.Buttons.Add(new TaskDialogButton(ButtonType.No) { Default = true });
					var result = td1.ShowDialog(App.Current.MainWindow);
					e.Parameters["cancel"] = result.ButtonType == ButtonType.No;
					break;
				}

				case "show projects window": {
					this.Visibility = Visibility.Hidden;
					var win = new ProjectsWindow {
						Owner = this,
						WindowStartupLocation = WindowStartupLocation.CenterScreen,
						Reader = this.View.Reader
					};
					win.View.Projects = this.View.Projects;
					var result = win.ShowDialog();
					this.View.Projects = win.View.Projects;
					this.Visibility = Visibility.Visible;
					break;
				}
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			this.Dispatcher.BeginInvoke((Action)(() => {
				this.View.ExitAppCommand.Execute(null);
			}));
			e.Cancel = true;
		}

		protected override void OnSourceInitialized(EventArgs e) => this.HideMinimizeAndMaximizeButtons();

		public MainWindow() {
			this.InitializeComponent();
			this.View.Initialize();
			this.View.ExecuteUIAction += this.View_ExecuteUIAction;
		}

		public MainWindowView View => this.DataContext.As<MainWindowView>();
	}
}