namespace OSInstallerBuilder.Controls
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Classes;
	using OSInstallerExtensibility.Classes.Data;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Interop;

	public partial class InstallerFilesData : UserControl, IInstallerSettingsController
	{
		#region Public Constructors
		public InstallerFilesData()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Reset()
		{
			TheListView.ItemsSource = null;
			View.Manager = null;
			View.UpdateInterface();
		}
		#endregion Public Methods

		#region Private Methods
		private static void onManagerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (InstallerFilesData)source;
			if (src == null)
				return;
			var value = (IInstallerManager)e.NewValue;
			src.View.Manager = value;
		}
		private void InstallerFilesDataView_ExecuteUIAction(object sender, ExecuteUiActionEventArgs e)
		{
			var lastAddDir = default(string);// Settings.GetValue<string>(App.ApplicationName, "History", "LastAddDirectory", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
			if (e.CommandToExecute.Equals("DeleteItems"))
			{
			}
			else if (e.CommandToExecute.Equals("AddFileItems") || e.CommandToExecute.Equals("AddFolderItems"))
			{
				var dialog = new CommonOpenFileDialog
				{
					AddToMostRecentlyUsedList = false,
					AllowNonFileSystemItems = true,
					EnsureFileExists = true,
					EnsurePathExists = true,
					InitialDirectory = lastAddDir,
					Multiselect = true,
					Title = "Select " + (e.CommandToExecute.Equals("AddFileItems") ? "files" : "folder") + "..."
				};
				if (e.CommandToExecute.Equals("AddFileItems"))
				{
					dialog.Filters.Add(new CommonFileDialogFilter("All files", "*.*"));
				}
				dialog.IsFolderPicker = e.CommandToExecute.Equals("AddFolderItems");
				CommonFileDialogResult result = dialog.ShowDialog();
				if (result == CommonFileDialogResult.Cancel)
					return;
				//Settings.SetValue<string>(App.ApplicationName, "History", "LastAddDirectory", System.IO.Path.GetDirectoryName(dialog.FileNames.FirstOrDefault()));
				if (e.CommandToExecute.Equals("AddFileItems"))
				{
					foreach (var fileName in dialog.FileNames)
					{
						View.AddItem(new InstallerItem(Guid.NewGuid().ToString())
						{
							ItemType = ItemTypes.File,
							Path = fileName,
							TypeSource = Helpers.GetImageSourceFromResource("OSInstallerBuilder", "Assets/file.png")
						});
					}
				}
				else
				{
					var td = App.GetTaskDialog(
						"Folder selected",
						string.Format("Adding {0} folders to the installation.", dialog.FileNames.Count()),
						"Include all sub-folders?",
						string.Join(",", dialog.FileNames.Select(x => System.IO.Path.GetFileName(x))),
						TaskDialogStandardIcon.Information, (new WindowInteropHelper(Window.GetWindow(this))).Handle, TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel);
					var dialogResult = td.Show();
					bool includeSubDirs = dialogResult == TaskDialogResult.Yes;
					if (dialogResult == TaskDialogResult.Cancel)
						return;
					foreach (var folderName in dialog.FileNames)
					{
						View.AddItem(new InstallerItem(Guid.NewGuid().ToString())
						{
							ItemType = ItemTypes.Folder,
							Path = folderName,
							IncludeSubFolders = includeSubDirs,
							TypeSource = Helpers.GetImageSourceFromResource("OSInstallerBuilder", "Assets/folder.png")
						});
					}
				}
				(TheListView.View as GridView).Columns[0].Width = TheListView.ActualWidth - SystemParameters.VerticalScrollBarWidth - 10;
				if (SettingsChanged != null)
					SettingsChanged(this, EventArgs.Empty);
			}
		}
		private void InstallerFilesDataView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
		}
		private void TheListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			View.SelectedItems.Clear();
			foreach (var item in (sender as ListView).SelectedItems)
			{
				View.SelectedItems.Add((IInstallerItem)item);
			}
		}
		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (double.IsNaN(TheListView.ActualWidth) || TheListView.ActualWidth == 0.0)
				return;
			(TheListView.View as GridView).Columns[0].Width = TheListView.ActualWidth - SystemParameters.VerticalScrollBarWidth - 10;
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler SettingsChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty ManagerProperty = DependencyProperty.Register("Manager", typeof(IInstallerManager), typeof(InstallerFilesData), new PropertyMetadata(null, onManagerChanged));
		#endregion Public Fields

		#region Public Properties
		public IInstallerManager Manager
		{
			get { return (IInstallerManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public InstallerFilesDataView View
		{
			get { return LayoutRoot.GetView<InstallerFilesDataView>(); }
		}
		#endregion Public Properties
	}
}
