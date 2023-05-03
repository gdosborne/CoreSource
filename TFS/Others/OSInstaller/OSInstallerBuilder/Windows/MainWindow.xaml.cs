namespace OSInstallerBuilder.Windows
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using MVVMFramework;
	using MyApplication;
	using MyApplication.Primitives;
	using MyApplication.Windows.Controls;
	using OSInstallerBuilder.Controls;
	using OSInstallerCommands.Classes.Events;
	using OSInstallerExtensibility.Classes.Managers;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Input;
	using System.Windows.Interop;
	using MyApplication.Windows;
	using MyApplication.Media;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			MyToolBar.RemoveOverflow();
		}
		#endregion Protected Methods

		#region Private Methods
		private void Copy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}
		private void Copy_Execute(object sender, ExecutedRoutedEventArgs e)
		{
		}
		private void Cut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}
		private void Cut_Execute(object sender, ExecutedRoutedEventArgs e)
		{
		}
		private void InstallerGlobalSettings_SettingsChanged(object sender, EventArgs e)
		{
			if (View.IsInitializing)
				return;
			if (View.Manager != null)
				View.Manager.IsDirty = true;
			View.UpdateInterface();
		}
		private void MainWindowView_CommandStatusUpdate(object sender, CommandStatusUpdateEventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				if (e.IsComplete)
				{
					this.IsEnabled = true;
					View.BuildProgressVisibility = Visibility.Collapsed;
					return;
				}
				this.IsEnabled = false;
				View.BuildProgressVisibility = Visibility.Visible;
				View.BuildProgressMaximum = e.Max;
				View.BuildProgressValue = e.Value;
				View.BuildProgressText = ((e.Value + 1) / e.Max).ToString("0%");
			}
			else
				Dispatcher.BeginInvoke(new CommandStatusUpdateHandler(MainWindowView_CommandStatusUpdate), new object[] { sender, e });
		}
		private void MainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("OpenFile"))
			{
				var lastFileName = Settings.GetValue<string>(App.ApplicationName, "History", "LastInstallerFile", string.Empty);
				var dialog = new CommonOpenFileDialog
				{
					AddToMostRecentlyUsedList = false,
					AllowNonFileSystemItems = true,
					DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					EnsureFileExists = true,
					EnsurePathExists = true,
					InitialDirectory = string.IsNullOrEmpty(lastFileName) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : System.IO.Path.GetDirectoryName(lastFileName),
					Multiselect = false,
					Title = "Select installer file..."
				};
				dialog.Filters.Add(new CommonFileDialogFilter("Installer Files", "*.installer"));
				dialog.IsFolderPicker = false;
				CommonFileDialogResult result = dialog.ShowDialog();
				if (result == CommonFileDialogResult.Cancel)
					return;
				if (View.Manager != null && View.Manager.IsDirty)
				{
					var td = App.GetTaskDialog(
						"File has changed",
						"The installer file has changes.",
						"Would you like to save the installer?",
						View.Manager.FileName,
						TaskDialogStandardIcon.Information, new WindowInteropHelper(this).Handle, TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel);
					var tdResult = td.Show();
					if (tdResult == TaskDialogResult.Cancel)
						return;
					else if (tdResult == TaskDialogResult.Yes)
						View.SaveFileCommand.Execute(null);

					(GlobalSettings.As<IInstallerSettingsController>()).Reset();
					(InstallerData.As<IInstallerSettingsController>()).Reset();
					(VisualData.As<IInstallerSettingsController>()).Reset();
					(FilesData.As<IInstallerSettingsController>()).Reset();
				}
				var temp = new Manager();
				temp.LoadComplete += Manager_LoadComplete;
				temp.Load(dialog.FileName);
				temp.Items.ToList().ForEach(x =>
				{
					if (x.ItemType == OSInstallerExtensibility.Interfaces.ItemTypes.File)
						x.TypeSource = temp.GetImageSourceFromResource("OSInstallerBuilder", File.Exists(x.Path) ? "Assets/file.png" : "Assets/file1.png");
					else if (x.ItemType == OSInstallerExtensibility.Interfaces.ItemTypes.Folder)
						x.TypeSource = temp.GetImageSourceFromResource("OSInstallerBuilder", Directory.Exists(x.Path) ? "Assets/folder.png" : "Assets/folder1.png");
				});
				Settings.SetValue<string>(App.ApplicationName, "History", "LastInstallerFile", dialog.FileName);
				View.Manager = temp;
				View.Manager.IsDirty = false;
				InstallerData.View.RequiredNames = View.Manager.RequiredDataNames;
				View.UpdateInterface();
			}
			else if (e.CommandToExecute.Equals("NewFile"))
			{
				var lastFileName = Settings.GetValue<string>(App.ApplicationName, "History", "LastInstallerFile", string.Empty);
				var dialog = new CommonSaveFileDialog
				{
					AddToMostRecentlyUsedList = false,
					DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					EnsureFileExists = false,
					EnsurePathExists = true,
					InitialDirectory = string.IsNullOrEmpty(lastFileName) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : System.IO.Path.GetDirectoryName(lastFileName),
					Title = "Create installer file..."
				};
				dialog.Filters.Add(new CommonFileDialogFilter("Installer Files", (string)View.OptionCategories.GetItem("General", "Installer Settings", "Build", "Installer extension").Value));
				CommonFileDialogResult result = dialog.ShowDialog();
				if (result == CommonFileDialogResult.Cancel)
					return;

				Settings.SetValue<string>(App.ApplicationName, "History", "LastInstallerFile", dialog.FileName);
				View.Manager = new Manager();
				View.Manager.CreateNew(dialog.FileName);
				View.Manager.IsDirty = true;
				InstallerData.View.RequiredNames = View.Manager.RequiredDataNames;
				View.UpdateInterface();
			}
			else if (e.CommandToExecute.Equals("ShowOptionsWindow"))
			{
				var optionWin = new OptionsWindow
				{
					WindowStartupLocation = WindowStartupLocation.CenterOwner,
					Owner = this
				};
				optionWin.View.OptionCategories = View.OptionCategories;
				var optWinResult = optionWin.ShowDialog();
			}
		}

		private void Manager_LoadComplete(object sender, EventArgs e)
		{
			sender.As<IInstallerManager>().IsDirty = false;
		}
		private void Paste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}
		private void Paste_Execute(object sender, ExecutedRoutedEventArgs e)
		{
		}
		private void Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}
		private void Redo_Execute(object sender, ExecutedRoutedEventArgs e)
		{
		}
		private void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
		}
		private void Undo_Execute(object sender, ExecutedRoutedEventArgs e)
		{
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Left", RestoreBounds.Left);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Top", RestoreBounds.Top);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Width", RestoreBounds.Width);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Height", RestoreBounds.Height);

			if (View.Manager != null && View.Manager.IsDirty)
			{
				var td = App.GetTaskDialog(
					"File has changed",
					"The installer file has changes.",
					"Would you like to save the installer?",
					View.Manager.FileName,
					TaskDialogStandardIcon.Information, (new WindowInteropHelper(Window.GetWindow(this))).Handle, TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel);
				var result = td.Show();
				if (result == TaskDialogResult.Yes)
					View.Manager.Save();
				if (result == TaskDialogResult.Cancel)
					e.Cancel = true;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var item = View.OptionCategories.GetItem("General", "General", "Startup", "Save Window Position");
			if (item != null && (bool)item.Value)
			{
				Left = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Left", Left);
				Top = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Top", Top);
				Width = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Width", Width);
				Height = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Height", Height);
			}
		}
		#endregion Private Methods

		#region Public Properties
		public MainWindowView View
		{
			get { return LayoutRoot.GetView<MainWindowView>(); }
		}
		#endregion Public Properties

	}
}
