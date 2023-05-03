// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace SNC.Applications.Developer.Views
{
	using MVVMFramework;
	using ORDModel;
	using SNC.Applications.Developer.Controls;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Drawing;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Xml.Linq;

	/// <summary>
	/// Class MainWindowView.
	/// </summary>
	public class MainWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindowView"/> class.
		/// </summary>
		public MainWindowView() {
			TreeItems = new ObservableCollection<TreeViewItem>();
			ProjectFileNameVisibility = Visibility.Collapsed;
			NoFileNameVisibility = Visibility.Visible;
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Creates the project.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void CreateProject(string fileName) {
			ProjectFileName = fileName;
		}
		/// <summary>
		/// Displays the project changed notification.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool? DisplayProjectChangedNotification(string action) {
			if (ExecuteUIAction != null) {
				var parameters = new Dictionary<string, object> { { "action", action } };
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("DisplayProjectChangedMessage", parameters));
				if (parameters.ContainsKey("result"))
					return (bool?)parameters["result"];
			}
			return null;
		}
		/// <summary>
		/// Initializes the view.
		/// </summary>
		public void InitView() {
		}
		/// <summary>
		/// Opens the project.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void OpenProject(string fileName) {
			var doc = XDocument.Load(fileName);
			var root = doc.Root;
			if (root.Name.LocalName.Equals("project", StringComparison.Ordinal)) {
				ReadProject(doc, fileName);
				ProjectFileName = fileName;
				LoadTree();
			}
			else {
				if (ExecuteUIAction != null) {
					var parameters = new Dictionary<string, object> { { "message", "The xml document specified is not an OptiRamp project file." } };
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowErrorMessage", parameters));
				}
			}
		}
		/// <summary>
		/// Saves the project.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool SaveProject(string fileName) {
			var result = false;
			try {
				CurrentProject.SaveAs(fileName);
				result = true;
			}
			catch (Exception ex) {
				LastException = ex;
				result = false;
				throw;
			}
			return result;
		}
		/// <summary>
		/// Saves the project.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool SaveProject() {
			var result = false;
			try {
				CurrentProject.Save();
				result = true;
			}
			catch (Exception ex) {
				LastException = ex;
				result = false;
				throw;
			}
			return result;
		}
		/// <summary>
		/// Updates the interface.
		/// </summary>
		public void UpdateInterface() {
			SaveProjectCommand.RaiseCanExecuteChanged();
			SaveProjectAsCommand.RaiseCanExecuteChanged();
			ShowObjectPropertiesCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void CurrentProject_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			UpdateInterface();
		}
		private byte[] GetImage(Bitmap bitmap) {
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
		}
		private void LoadTree() {
			var tvi = new TreeViewItem
			{
				Header = new TreeViewItemHeader
				{
					Text = CurrentProject.Name,
					Style = (Style)App.Current.FindResource("ProjectTreeItem")
				},
				Tag = CurrentProject
			};
			tvi.Selected += tvi_Selected;
			tvi.Expanded += tvi_Expanded;
			tvi.Items.Add(new TreeViewItem { Header = "_JUNK_" });
			TreeItems.Add(tvi);
		}
		private void LoadTreeChildren(TreeViewItem parent) {
			if (parent.Tag is Project) {
				var project = parent.Tag as Project;
				project.Computers.ToList().ForEach(x =>
				{
					var tvi = new TreeViewItem
					{
						Header = new TreeViewItemHeader
						{
							Text = x.Name,
							Style = (Style)App.Current.FindResource("ComputerTreeItem")
						},
						Tag = x
					};
					tvi.Selected += tvi_Selected;
					tvi.Expanded += tvi_Expanded;
					tvi.Items.Add(new TreeViewItem { Header = "_JUNK_" });
					parent.Items.Add(tvi);
				});
			}
		}
		private void NewProject(object state) {
			if (IsProjectChanged) {
				var result = (bool?)DisplayProjectChangedNotification("new");
				return;
			}
			if (ExecuteUIAction != null) {
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("CreateNewProject"));
			}
		}
		private void OpenProject(object state) {
			if (IsProjectChanged) {
				DisplayProjectChangedNotification("open");
				return;
			}
			if (ExecuteUIAction != null) {
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("OpenProject"));
			}
		}
		private void ReadProject(XDocument doc, string fileName) {
			CurrentProject = new Project(doc, App.CurrentProjectImagesFolder, fileName);
		}
		private void SaveProject(object state) {
		}
		private void SaveProjectAs(object state) {
		}
		private void ShowObjectProperties(object state) {
			if (ExecuteUIAction != null) {
				var parameters = new Dictionary<string, object> { { "object", SelectedTreeViewItem.Tag } };
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowObjectProperties", parameters));
			}
		}
		private void tvi_Expanded(object sender, RoutedEventArgs e) {
			if ((sender as TreeViewItem).Items.Count == 1 && ((sender as TreeViewItem).Items[0] as TreeViewItem).Header.Equals("_JUNK_")) {
				(sender as TreeViewItem).Items.Clear();
				LoadTreeChildren(sender as TreeViewItem);
			}
		}
		private void tvi_Selected(object sender, RoutedEventArgs e) {
			if ((sender as TreeViewItem).Items.Count == 1 && ((sender as TreeViewItem).Items[0] as TreeViewItem).Header.Equals("_JUNK_")) {
				(sender as TreeViewItem).Items.Clear();
				LoadTreeChildren(sender as TreeViewItem);
			}
			SelectedTreeViewItem = sender as TreeViewItem;
		}
		private bool ValidateNewProjectState(object state) {
			return true;
		}
		private bool ValidateOpenProjectState(object state) {
			return true;
		}
		private bool ValidateSaveProjectAsState(object state) {
			return !string.IsNullOrEmpty(ProjectFileName);
		}
		private bool ValidateSaveProjectState(object state) {
			return IsProjectChanged;
		}
		private bool ValidateShowObjectPropertiesState(object state) {
			return CurrentProject != null && SelectedTreeViewItem != null;
		}
		#endregion Private Methods

		#region Public Events
		/// <summary>
		/// Occurs when [execute UI action].
		/// </summary>
		public event ExecuteUIActionHandler ExecuteUIAction;
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Project _CurrentProject;
		private bool _IsProjectChanged;
		private Exception _LastException;
		private DelegateCommand _NewProjectCommand = null;
		private Visibility _NoFileNameVisibility;
		private DelegateCommand _OpenProjectCommand = null;
		private string _ProjectFileName;
		private Visibility _ProjectFileNameVisibility;
		private DelegateCommand _SaveProjectAsCommand = null;
		private DelegateCommand _SaveProjectCommand = null;
		private TreeViewItem _SelectedTreeViewItem;
		private DelegateCommand _ShowObjectPropertiesCommand = null;
		private ObservableCollection<TreeViewItem> _TreeItems;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the current project.
		/// </summary>
		/// <value>The current project.</value>
		public Project CurrentProject {
			get {
				return _CurrentProject;
			}
			set {
				_CurrentProject = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentProject"));
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is project changed.
		/// </summary>
		/// <value><c>true</c> if this instance is project changed; otherwise, <c>false</c>.</value>
		public bool IsProjectChanged {
			get {
				return _IsProjectChanged;
			}
			set {
				_IsProjectChanged = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsProjectChanged"));
			}
		}
		public Exception LastException {
			get {
				return _LastException;
			}
			set {
				_LastException = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastException"));
			}
		}
		/// <summary>
		/// Gets the new project command.
		/// </summary>
		/// <value>The new project command.</value>
		public DelegateCommand NewProjectCommand {
			get {
				if (_NewProjectCommand == null)
					_NewProjectCommand = new DelegateCommand(NewProject, ValidateNewProjectState);
				return _NewProjectCommand as DelegateCommand;
			}
		}
		/// <summary>
		/// Gets or sets the no file name visibility.
		/// </summary>
		/// <value>The no file name visibility.</value>
		public Visibility NoFileNameVisibility {
			get {
				return _NoFileNameVisibility;
			}
			set {
				_NoFileNameVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("NoFileNameVisibility"));
			}
		}
		/// <summary>
		/// Gets the open project command.
		/// </summary>
		/// <value>The open project command.</value>
		public DelegateCommand OpenProjectCommand {
			get {
				if (_OpenProjectCommand == null)
					_OpenProjectCommand = new DelegateCommand(OpenProject, ValidateOpenProjectState);
				return _OpenProjectCommand as DelegateCommand;
			}
		}
		/// <summary>
		/// Gets or sets the name of the project file.
		/// </summary>
		/// <value>The name of the project file.</value>
		public string ProjectFileName {
			get {
				return _ProjectFileName;
			}
			set {
				_ProjectFileName = value;
				ProjectFileNameVisibility = string.IsNullOrEmpty(value) ? Visibility.Collapsed : Visibility.Visible;
				NoFileNameVisibility = string.IsNullOrEmpty(value) ? Visibility.Visible : Visibility.Collapsed;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectFileName"));
			}
		}
		/// <summary>
		/// Gets or sets the project file name visibility.
		/// </summary>
		/// <value>The project file name visibility.</value>
		public Visibility ProjectFileNameVisibility {
			get {
				return _ProjectFileNameVisibility;
			}
			set {
				_ProjectFileNameVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectFileNameVisibility"));
			}
		}
		/// <summary>
		/// Gets the save project as command.
		/// </summary>
		/// <value>The save project as command.</value>
		public DelegateCommand SaveProjectAsCommand {
			get {
				if (_SaveProjectAsCommand == null)
					_SaveProjectAsCommand = new DelegateCommand(SaveProjectAs, ValidateSaveProjectAsState);
				return _SaveProjectAsCommand as DelegateCommand;
			}
		}
		/// <summary>
		/// Gets the save project command.
		/// </summary>
		/// <value>The save project command.</value>
		public DelegateCommand SaveProjectCommand {
			get {
				if (_SaveProjectCommand == null)
					_SaveProjectCommand = new DelegateCommand(SaveProject, ValidateSaveProjectState);
				return _SaveProjectCommand as DelegateCommand;
			}
		}
		public TreeViewItem SelectedTreeViewItem {
			get {
				return _SelectedTreeViewItem;
			}
			set {
				_SelectedTreeViewItem = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedTreeViewItem"));
			}
		}
		public DelegateCommand ShowObjectPropertiesCommand {
			get {
				if (_ShowObjectPropertiesCommand == null)
					_ShowObjectPropertiesCommand = new DelegateCommand(ShowObjectProperties, ValidateShowObjectPropertiesState);
				return _ShowObjectPropertiesCommand as DelegateCommand;
			}
		}
		public ObservableCollection<TreeViewItem> TreeItems {
			get {
				return _TreeItems;
			}
			set {
				_TreeItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TreeItems"));
			}
		}
		#endregion Public Properties
	}
}