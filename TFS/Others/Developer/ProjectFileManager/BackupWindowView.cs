// ----------------------------------------------------------------------- Copyright © Statistics & Controls, Inc 2016 Created by: Greg Osborne -----------------------------------------------------------------------
//
// Backup Window View
namespace ProjectFileManager {

	//using fDefs.ProjectService;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	internal class BackupWindowView : INotifyPropertyChanged {

		#region Public Constructors
		public BackupWindowView() {
			BackupDays = 10;
		}
		#endregion Public Constructors

		#region Public Methods
		public void InitView() {
		}
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state) {
			DialogResult = false;
		}
		private void OK(object state) {
			DialogResult = true;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateOKState(object state) {
			return SelectedBackup != null;
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private int _BackupDays;
		private ObservableCollection<ProjectData> _Backups;
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private DelegateCommand _OKCommand = null;
		private ObservableCollection<ProjectData> _Projects;
		private ProjectData _SelectedBackup;
		private ProjectData _SelectedProject;
		#endregion Private Fields

		#region Public Properties
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
		public ObservableCollection<ProjectData> Backups {
			get {
				return _Backups;
			}
			set {
				_Backups = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Backups"));
			}
		}
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
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
		public ProjectData SelectedBackup {
			get {
				return _SelectedBackup;
			}
			set {
				_SelectedBackup = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedBackup"));
			}
		}
		public ProjectData SelectedProject {
			get {
				return _SelectedProject;
			}
			set {
				_SelectedProject = value;
				Backups = null;
				if (_SelectedProject != null)
					Backups = new ObservableCollection<ProjectData>(_SelectedProject.Backups.OrderByDescending(x => x.LastModifyTimeUtc));
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedProject"));
			}
		}
		#endregion Public Properties
	}
}
