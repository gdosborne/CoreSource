// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// FileManagerView.cs
//
namespace ProjectFileManager {

	using fDefs.ProjectService;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.ServiceModel;
	using System.Windows;

	public class FileManagerView : INotifyPropertyChanged {

		#region Public Constructors
		public FileManagerView() {
			AddressWidth = 250;
			ControlVersion = string.Format("Project File Manager\nVersion {0}", this.GetType().Assembly.GetName().Version);
			ErrorVisibility = Visibility.Collapsed;
			ErrorText = "Testing";
			ProjectType = ProjectType;
		}
		#endregion Public Constructors

		#region Public Methods
		public void DeleteProjectFile(string name) {
			var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
			string Error = null;
			client.DeleteProject(SelectedProject.Name, ProjectType, out Error);
			if (!string.IsNullOrEmpty(Error))
				ShowError(Error);
			else
				Refresh(null);
		}
		public void GetBackupProjectNames() {
			InitializeService();
			string Error = null;
			try {
				var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
				BackupProjects = new ObservableCollection<ProjectData>(client.GetAllBackupProjects(ProjectType, out Error));
				BackupDays = client.NumberOfBackupDays();
				HideError();
			}
			catch (Exception) {
				ShowError("Cannot connect to service");
			}
		}
		public void InitView() {
			if (WebServiceUri == null || string.IsNullOrEmpty(Address)) {
				ShowError("Address is missing");
				return;
			}
			GetProjectNames();
		}
		public void Refresh() {
			Refresh(null);
		}
		public void Refresh(object state) {
			GetProjectNames();
		}
		public void RestoreProjectFile(ProjectData original, ProjectData backup) {
			var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
			string Error = null;
			client.RestoreProject(original.Name, backup.Name, ProjectType, out Error);
			if (!string.IsNullOrEmpty(Error))
				ShowError(Error);
			else
				Refresh(null);
		}
		public void ShowError(string message) {
			ErrorText = message;
			ErrorVisibility = Visibility.Visible;
		}
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
			DeleteCommand.RaiseCanExecuteChanged();
			SetServerCommand.RaiseCanExecuteChanged();
			ReorderCommand.RaiseCanExecuteChanged();
			GetBackupsCommand.RaiseCanExecuteChanged();
		}
		public void UpdateSequenceFile() {
			var ordered = Projects.OrderBy(x => x.Sequence).Select(x => x.Name).ToArray();
			var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
			string Error = null;
			client.SetFileSequence(ProjectType, ordered, out Error);
			if (!string.IsNullOrEmpty(Error))
				ShowError(Error);
			else
				Refresh(null);
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state) {
			DialogResult = false;
		}
		private void Delete(object state) {
			if (ExecuteAction != null) {
				var parameters = new Dictionary<string, object> { 
					{ "message", string.Format("You are about to delete the project \"{1}\" from \"{0}\".\n\nAre you sure you want to do this?", Address, SelectedProject.Name) },
					{ "additional", "This is irreversible, but you can restore a previous backup, if available." },
					{ "title", "Delete project" },
					{ "cancel", false } 
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("DeleteProject", parameters));
				if ((bool)parameters["cancel"])
					return;
				DeleteProjectFile(SelectedProject.Name);
			}
		}
		private void GetBackups(object state) {
			if (ExecuteAction != null)
				ExecuteAction(this, new ExecuteUIActionEventArgs("DisplayBackups", null));
		}
		private void GetProjectNames() {
			InitializeService();
			string Error = null;
			try {
				var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
				Projects = new ObservableCollection<ProjectData>(client.GetAllProjects(ProjectType, false, out Error));
				BackupDays = client.NumberOfBackupDays();
				HideError();
			}
			catch (Exception) {
				ShowError("Cannot connect to service");
			}
		}
		private void HideError() {
			ErrorVisibility = Visibility.Collapsed;
		}
		private void InitializeService() {
			if (WebServiceUri == null)
				return;
			if (binding != null)
				binding = null;
			if (epa != null)
				epa = null;
			binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
			binding.MaxBufferSize = int.MaxValue;
			binding.MaxReceivedMessageSize = int.MaxValue;
			epa = new EndpointAddress(WebServiceUri);
		}
		private void OK(object state) {
			if (SelectedProject == null)
				return;
			var client = ChannelFactory<IProjectService>.CreateChannel(binding, epa);
			string Error = null;
			ProjectStream = null;
			var data = client.GetProject(SelectedProject.Name, ProjectType, out Error);
			if (!string.IsNullOrEmpty(Error)) {
				ShowError(Error);
				return;
			}
			if (data != null)
				ProjectStream = new MemoryStream(data);
			DialogResult = true;
		}
		private void Reorder(object state) {
			if (ExecuteAction != null) {
				var parameters = new Dictionary<string, object> { 
					{ "message", "You are about to change the display order of the projects.\n\nAre you sure you want to do this?" },
					{ "title", "Reorder projects" }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("ReorderFiles", parameters));
			}
		}
		private void SetServer(object state) {
			Address = ValidateAddress(Address);
			WebServiceUri = new Uri(Address);
			Refresh(state);
		}
		private string ValidateAddress(string original) {
			try {
				var tempUri = new UriBuilder(original).Uri;
				//var tempUri = new Uri(original);
				int port = tempUri.Port;
				string scheme = tempUri.Scheme;
				string hostName = tempUri.Host;
				string path = tempUri.PathAndQuery;
				if (!scheme.Equals("http", StringComparison.OrdinalIgnoreCase))
					scheme = "http";
				if (string.IsNullOrEmpty(path) || path.Equals("/"))
					path = "ProjectService";
				if (port == 0 || port == 80)
					port = 9002;
				return new UriBuilder(scheme, hostName, port, path).Uri.ToString();
			}
			catch {
				return original;
			}
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateDeleteState(object state) {
			return SelectedProject != null;
		}
		private bool ValidateGetBackupsState(object state) {
			return ProjectType == Enumerations.ProjectTypes.Runtime;
		}
		private bool ValidateOKState(object state) {
			return SelectedProject != null;
		}
		private bool ValidateRefreshState(object state) {
			return true;
		}
		private bool ValidateReorderState(object state) {
			return ProjectType == Enumerations.ProjectTypes.Runtime;
		}
		private bool ValidateSetServerState(object state) {
			return !string.IsNullOrEmpty(Address);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _Address;
		private double _AddressWidth;
		private int _BackupDays;
		private ObservableCollection<ProjectData> _BackupProjects;
		private DelegateCommand _CancelCommand = null;
		private string _ControlVersion;
		private DelegateCommand _DeleteCommand = null;
		private bool? _DialogResult;
		private string _ErrorText;
		private Visibility _ErrorVisibility;
		private string _FileName;
		private DelegateCommand _GetBackupsCommand = null;
		private DelegateCommand _OKCommand = null;
		private ObservableCollection<ProjectData> _Projects;
		private Stream _ProjectStream;
		private Enumerations.ProjectTypes _ProjectType;
		private DelegateCommand _RefreshCommand = null;
		private DelegateCommand _ReorderCommand = null;
		private ProjectData _SelectedProject;
		private DelegateCommand _SetServerCommand = null;
		private Uri _WebServiceUri;
		private BasicHttpBinding binding = null;
		private EndpointAddress epa = null;
		#endregion Private Fields

		#region Public Properties
		public string Address {
			get {
				return _Address;
			}
			set {
				_Address = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Address"));
			}
		}
		public double AddressWidth {
			get {
				return _AddressWidth;
			}
			set {
				_AddressWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AddressWidth"));
			}
		}
		public int BackupDays {
			get {
				return _BackupDays;
			}
			set {
				_BackupDays = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BackupDays"));
			}
		}
		public ObservableCollection<ProjectData> BackupProjects {
			get {
				return _BackupProjects;
			}
			set {
				_BackupProjects = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BackupProjects"));
			}
		}
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public string ControlVersion {
			get {
				return _ControlVersion;
			}
			set {
				_ControlVersion = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ControlVersion"));
			}
		}
		public DelegateCommand DeleteCommand {
			get {
				if (_DeleteCommand == null)
					_DeleteCommand = new DelegateCommand(Delete, ValidateDeleteState);
				return _DeleteCommand as DelegateCommand;
			}
		}
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public string ErrorText {
			get {
				return _ErrorText;
			}
			set {
				_ErrorText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorText"));
			}
		}
		public Visibility ErrorVisibility {
			get {
				return _ErrorVisibility;
			}
			set {
				_ErrorVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorVisibility"));
			}
		}
		public string FileName {
			get {
				return _FileName;
			}
			set {
				_FileName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public DelegateCommand GetBackupsCommand {
			get {
				if (_GetBackupsCommand == null)
					_GetBackupsCommand = new DelegateCommand(GetBackups, ValidateGetBackupsState);
				return _GetBackupsCommand as DelegateCommand;
			}
		}
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public ObservableCollection<ProjectData> Projects {
			get {
				return _Projects;
			}
			set {
				_Projects = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Projects"));
			}
		}
		public Stream ProjectStream {
			get {
				return _ProjectStream;
			}
			set {
				_ProjectStream = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectStream"));
			}
		}
		public Enumerations.ProjectTypes ProjectType {
			get {
				return _ProjectType;
			}
			set {
				_ProjectType = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectType"));
			}
		}
		public DelegateCommand RefreshCommand {
			get {
				if (_RefreshCommand == null)
					_RefreshCommand = new DelegateCommand(Refresh, ValidateRefreshState);
				return _RefreshCommand as DelegateCommand;
			}
		}
		public DelegateCommand ReorderCommand {
			get {
				if (_ReorderCommand == null)
					_ReorderCommand = new DelegateCommand(Reorder, ValidateReorderState);
				return _ReorderCommand as DelegateCommand;
			}
		}
		public ProjectData SelectedProject {
			get {
				return _SelectedProject;
			}
			set {
				_SelectedProject = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedProject"));
			}
		}
		public DelegateCommand SetServerCommand {
			get {
				if (_SetServerCommand == null)
					_SetServerCommand = new DelegateCommand(SetServer, ValidateSetServerState);
				return _SetServerCommand as DelegateCommand;
			}
		}
		public Uri WebServiceUri {
			get {
				return _WebServiceUri;
			}
			set {
				_WebServiceUri = value;
				Address = value != null ? value.ToString() : string.Empty;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WebServiceUri"));
			}
		}
		#endregion Public Properties
	}
}
