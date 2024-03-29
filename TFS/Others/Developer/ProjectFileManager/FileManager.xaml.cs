// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// FileManager.xaml.cs
//
namespace ProjectFileManager {

	using fDefs.ProjectService;
	using MVVMFramework;
	using SNC.OptiRamp.Application.Dialog;
	using SNC.OptiRamp.Application.Extensions.Primitives;
	using SNC.OptiRamp.Application.Extensions.Windows;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class FileManager : UserControl {

		#region Public Constructors
		public FileManager() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void OpenProject(string fileName) {
			_ClientFileName = fileName;
			if (View.Projects != null && View.Projects.Any(x => x.Name.Equals(fileName))) {
				View.SelectedProject = View.Projects.First(x => x.Name.Equals(fileName));
				View.OKCommand.Execute(null);
			}
		}
		public void Refresh() {
			View.Refresh();
		}
		#endregion Public Methods

		#region Private Methods
		private static void onIsRuntimeProjectsChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (FileManager)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
			src.View.ProjectType = value ? Enumerations.ProjectTypes.Runtime : Enumerations.ProjectTypes.Vts;
		}
		private static void onWebServicUriChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (FileManager)source;
			if (src == null)
				return;
			var value = (Uri)e.NewValue;
			src.View.WebServiceUri = value;
		}
		private void FileListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			View.OKCommand.Execute(null);
		}

		private void FileManagerView_ExecuteAction(object sender, ExecuteUIActionEventArgs e) {
			var form = this.FindForm();
			switch (e.CommandToExecute) {
				case "DisplayBackups":
					View.GetBackupProjectNames();
					if (View.BackupProjects == null)
						return;
					var backupWin = new BackupWindow();
					backupWin.SetOwner(form);
					backupWin.Left = form.Left + ((form.Width - backupWin.Width) / 2);
					backupWin.Top = form.Top + ((form.Height - backupWin.Height) / 2);
					backupWin.View.BackupDays = View.BackupDays;
					backupWin.View.Projects = View.BackupProjects;
					var result1 = backupWin.ShowDialog();
					if (!result1.GetValueOrDefault())
						return;
					View.RestoreProjectFile(backupWin.View.SelectedProject, backupWin.View.SelectedBackup);
					break;
				case "DeleteProject":
					var td1 = new TaskDialog {
						AllowClose = false,
						Width = 400,
						Image = ImagesTypes.Question,
						MessageText = (string)e.Parameters["message"],
						AdditionalInformation = (string)e.Parameters["additional"],
						IsAdditionalInformationExpanded = true,
						Title = (string)e.Parameters["title"]
					};
					td1.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
					e.Parameters["cancel"] = (td1.ShowDialog(Window.GetWindow(this)) == (int)ButtonTypes.No);
					break;
				case "ReorderFiles":
					if (View.Projects == null) {
						View.ShowError("No projects loaded");
						return;
					}
					var reorderWin = new OrderProjectsWindow();
					reorderWin.SetOwner(form);
					reorderWin.Left = form.Left + ((form.Width - reorderWin.Width) / 2);
					reorderWin.Top = form.Top + ((form.Height - reorderWin.Height) / 2);
					reorderWin.View.Projects = new ObservableCollection<ProjectData>(View.Projects.OrderBy(x => x.Sequence));
					reorderWin.ShowDialog();
					if (!reorderWin.DialogResult.GetValueOrDefault())
						return;
					var td2 = new TaskDialog {
						AllowClose = false,
						Width = 400,
						Image = ImagesTypes.Question,
						MessageText = (string)e.Parameters["message"],
						Title = (string)e.Parameters["title"]
					};
					td2.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
					if (td2.ShowDialog(Window.GetWindow(this)) == (int)ButtonTypes.No)
						return;
					View.UpdateSequenceFile();
					break;
			}
		}
		private void FileManagerView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					if (!View.DialogResult.GetValueOrDefault()) {
						if (DialogCancelled != null)
							DialogCancelled(this, EventArgs.Empty);
						return;
					}
					if (DialogCompleted != null)
						DialogCompleted(this, new FileSelectedEventArgs(View.SelectedProject.Name, View.ProjectStream, View.Address));
					break;
			}
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			sender.As<TextBox>().SelectAll();
		}
		#endregion Private Methods

		#region Public Events
		public event CancelHandler DialogCancelled;
		public event OKHandler DialogCompleted;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty IsRuntimeProjectsProperty = DependencyProperty.Register("IsRuntimeProjects", typeof(bool), typeof(FileManager), new PropertyMetadata(true, onIsRuntimeProjectsChanged));
		public static readonly DependencyProperty WebServicUriProperty = DependencyProperty.Register("WebServicUri", typeof(Uri), typeof(FileManager), new PropertyMetadata(null, onWebServicUriChanged));
		#endregion Public Fields

		#region Private Fields
		private string _ClientFileName = null;
		#endregion Private Fields

		#region Public Properties
		public bool IsRuntimeProjects {
			get {
				return (bool)GetValue(IsRuntimeProjectsProperty);
			}
			set {
				SetValue(IsRuntimeProjectsProperty, value);
			}
		}
		public Uri WebServicUri {
			get {
				return (Uri)GetValue(WebServicUriProperty);
			}
			set {
				SetValue(WebServicUriProperty, value);
				try {
					View.InitView();
				}
				catch (Exception) {
				}
			}
		}
		#endregion Public Properties

		#region Internal Properties
		internal FileManagerView View {
			get {
				return LayoutRoot.GetView<FileManagerView>();
			}
		}
		#endregion Internal Properties
	}
}
