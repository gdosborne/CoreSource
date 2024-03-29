namespace SNC.OptiRamp.Application.Developer {

	using SNC.OptiRamp.Application.Developer.Properties;
	using SNC.OptiRamp.Application.Developer.Views;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls.Ribbon;
	using SNC.OptiRamp.Application.Developer.Classes.Management;
	using System.Threading.Tasks;
	using System.Threading;
	using System.Collections.ObjectModel;
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using System.Windows.Input;

	internal partial class DeveloperMainWindow : RibbonWindow {

		#region Public Constructors
		public DeveloperMainWindow() {
			InitializeComponent();
			if (Settings.Default.UpdateRequired) {
				Settings.Default.Upgrade();
				Settings.Default.Save();
			}
			App.ApplicationExtensions = new Composition();
			App.OpenSplashScreen();
			View.Initialize(this);
			this.Dispatcher.Invoke(() => {
				LoadExtensions();
			});
			View.UpdateInterface();
		}

		private async Task LoadExtensions() {
			App.ApplicationExtensions.ShowSplashMessage("Initializing extensions...");
			var t1 = Task.Delay(TimeSpan.FromSeconds(2));
			var t2 = Task.Factory.StartNew(() => {
				foreach (var extension in SNC.OptiRamp.Application.Developer.App.ApplicationExtensions.Extensions) {
					try {
						var ver = extension.GetType().Assembly.GetName().Version;
						App.ApplicationExtensions.ShowSplashMessage("{0} (version {1}) loaded", extension.Name, ver);
						extension.AddRibbonItems(ApplicationRibbon);
						if (extension.OptionsCategory != null)
							App.ApplicationExtensions.OptionsCategories.Add(extension.OptionsCategory);
						extension.ShowUserControl += extension_ShowUserControl;
					}
					catch (Exception) {
						return;
					}
				}
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
			await Task.WhenAll(t1, t2);
			var pos = Mouse.GetPosition(App.SplashWindow);
			if (pos.X >= 0 && pos.Y >= 0) {
				App.SplashWindow.MouseLeave += SplashWindow_MouseLeave;
				return;
			}
			App.SplashWindow.Close();
			return;
		}

		void SplashWindow_MouseLeave(object sender, MouseEventArgs e) {
			App.SplashWindow.Close();
		}
		void extension_ShowUserControl(object sender, DeveloperEntities.Management.ShowUserControlEventArgs e) {
			ExtensionBorder.Child = e.Control;
		}
		#endregion Public Constructors
		private void SetupChangedHandler() {
			foreach (var item in SNC.OptiRamp.Application.Developer.App.ApplicationExtensions.Extensions) {
				item.ProjectChanged += item_ProjectChanged;
			}
		}

		void item_ProjectChanged(object sender, EventArgs e) {
			View.UpdateInterface();
			foreach (var item in SNC.OptiRamp.Application.Developer.App.ApplicationExtensions.Extensions) {
				if (item != sender)
					item.UpdateInterface();
			}
		}
		#region Private Methods
		private void DeveloperMainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
			var yesTaskButton = new TaskDialogButton(ButtonType.Yes);
			var noTaskButton = new TaskDialogButton(ButtonType.No);
			switch (e.CommandToExecute) {
				case "ShowAboutDialog":
					var aboutDialog = new AboutBoxWindow {
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
					};
					aboutDialog.View.OutputMessage = "Loaded Extensions" + Environment.NewLine;
					foreach (var extension in SNC.OptiRamp.Application.Developer.App.ApplicationExtensions.Extensions) {
						aboutDialog.View.OutputMessage += string.Format("{0} ({1})" + Environment.NewLine, extension.Name, extension.GetType().Assembly.GetName().Version);
					}
					aboutDialog.ShowDialog();
					break;
				case "ShowSettings":
					var settingsDialog = new SettingsWindow {
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
					};
					settingsDialog.View.Categories = new ObservableCollection<Category>(App.ApplicationExtensions.OptionsCategories);
					var resultSettings = settingsDialog.ShowDialog();
					if (!resultSettings.GetValueOrDefault())
						return;
					break;
				case "ShowLocalOpenDialog":
					var localOpenDialog = new VistaOpenFileDialog {
						CheckFileExists = true,
						Filter = "Developer files|*.developer|Xml files|*.xml",
						InitialDirectory = string.IsNullOrEmpty(Settings.Default.LastOpenFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Settings.Default.LastOpenFolder,
						Multiselect = false,
						Title = "Open project..."
					};
					var openLocalResult = localOpenDialog.ShowDialog(this);
					if (!openLocalResult.GetValueOrDefault())
						return;
					Settings.Default.LastOpenFolder = System.IO.Path.GetDirectoryName(localOpenDialog.FileName);
					Settings.Default.Save();
					View.ProjectFile = ProjectFile.Create(localOpenDialog.FileName);
					View.ProjectFile.Project.CurrentPage = View.ProjectFile.Project.Pages.First();
					if (View.ProjectFile.Project.CurrentPage != null) {
						var cmd = App.ApplicationExtensions.ExtensionCommands.FirstOrDefault(x => x != null && x.Name.Equals("AddControl", StringComparison.OrdinalIgnoreCase));
						if (cmd != null) {
							foreach (var item in View.ProjectFile.Project.CurrentPage.Objects) {
								cmd.Execute(item);
								item.Control.SizeChanged += Control_SizeChanged;
								if (item.Control is uRectangle)
									item.Control.As<uRectangle>().LocationChanged += DeveloperMainWindow_LocationChanged;
							}
						}
					}
					SetupChangedHandler();
					View.UpdateInterface();
					break;
				case "ShowRemoteOpenDialog":
					var remoteOpenDialog = new OpenRemoteWindow {
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
						Address = Settings.Default.LastWebServiceAddress
					};
					var openRemoteResult = remoteOpenDialog.ShowDialog();
					if (!openRemoteResult.GetValueOrDefault())
						return;
					View.ProjectFile = ProjectFile.Create(remoteOpenDialog.View.FileName, remoteOpenDialog.View.Stream);
					View.ProjectFile.Project.CurrentPage = View.ProjectFile.Project.Pages.First();
					SetupChangedHandler();
					View.UpdateInterface();
					break;
				case "CloseProjectFile":
					var closeTaskDialog = new TaskDialog {
						AllowDialogCancellation = false,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						ExpandedByDefault = false,
						ExpandedInformation = "If you respond with no, any changes made to this project will be lost.",
						MainIcon = TaskDialogIcon.Warning,
						MainInstruction = "The currenly loaded project (" + View.ProjectFile.FileName + ") has changed. Do you really want to close the project?",
						MinimizeBox = false,
						//WindowIcon = this.Icon,
						WindowTitle = "Close project..."
					};
					closeTaskDialog.Buttons.Add(yesTaskButton);
					closeTaskDialog.Buttons.Add(noTaskButton);
					var closeProjectResult = closeTaskDialog.ShowDialog(this);
					if (closeProjectResult.ButtonType == ButtonType.No)
						return;
					View.ProjectFile = null;
					break;
				case "NewProjectFile":
					var dir = string.IsNullOrEmpty(Settings.Default.LastOpenFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Settings.Default.LastOpenFolder;
					var fileName = "noname";
					var filePath = System.IO.Path.Combine(dir, fileName + ".developer");
					var count = -1;
					while (System.IO.File.Exists(filePath)) {
						count++;
						fileName = string.Format("noname{0}", count);
						filePath = System.IO.Path.Combine(dir, fileName + ".developer");
					}
					View.ProjectFile = ProjectFile.Create(filePath);
					View.ProjectFile.Project.CurrentPage = View.ProjectFile.Project.Pages.First();
					SetupChangedHandler();
					View.UpdateInterface();
					break;
				case "SaveProjectFileAs":
					var localSaveDialog = new VistaSaveFileDialog {
						AddExtension = true,
						DefaultExt = ".developer",
						CreatePrompt = true,
						CheckPathExists = true,
						Filter = "Developer files|*.developer|Xml files|*.xml",
						InitialDirectory = string.IsNullOrEmpty(Settings.Default.LastOpenFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : Settings.Default.LastOpenFolder,
						Title = "Save project..."
					};
					var saveLocalResult = localSaveDialog.ShowDialog(this);
					if (!saveLocalResult.GetValueOrDefault())
						return;
					Settings.Default.LastOpenFolder = System.IO.Path.GetDirectoryName(localSaveDialog.FileName);
					Settings.Default.Save();
					View.ProjectFile.Save(localSaveDialog.FileName);
					break;
			}
		}

		void DeveloperMainWindow_LocationChanged(object sender, LocationChangedEventArgs e) {
			View.ProjectFile.IsChanged = true;
			View.UpdateInterface();
		}

		void Control_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e) {
			View.ProjectFile.IsChanged = true;
			View.UpdateInterface();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			View.Persist(this);
		}
		#endregion Private Methods

		#region Public Properties
		public DeveloperMainWindowView View {
			get {
				return LayoutRoot.GetView<DeveloperMainWindowView>();
			}
		}
		#endregion Public Properties
	}
}
