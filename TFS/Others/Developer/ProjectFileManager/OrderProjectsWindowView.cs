// -----------------------------------------------------------------------
// Copyright © Statistics & Controls, Inc 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Order Projects View
//
namespace ProjectFileManager
{
	using fDefs.ProjectService;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	internal class OrderProjectsWindowView : INotifyPropertyChanged
	{
		#region Public Methods
		public void InitView() {
		}
		public void UpdateInterface() {
			UpCommand.RaiseCanExecuteChanged();
			DownCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state) {
			DialogResult = false;
		}
		private void Down(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("MoveProjectDown", null));
		}
		private void OK(object state) {
			DialogResult = true;
		}
		private void Up(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("MoveProjectUp", null));
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateDownState(object state) {
			if (Projects == null)
				return false;
			var max = Projects.Max(x => x.Sequence);
			return SelectedProject != null && SelectedProject.Sequence < max;
		}
		private bool ValidateOKState(object state) {
			return true;
		}
		private bool ValidateUpState(object state) {
			if (Projects == null)
				return false;
			var min = Projects.Min(x => x.Sequence);
			return SelectedProject != null && SelectedProject.Sequence > 0;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private DelegateCommand _DownCommand = null;
		private DelegateCommand _OKCommand = null;
		private ObservableCollection<ProjectData> _Projects;
		private ProjectData _SelectedProject;
		private DelegateCommand _UpCommand = null;
		#endregion Private Fields

		#region Public Properties
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
		public DelegateCommand DownCommand {
			get {
				if (_DownCommand == null)
					_DownCommand = new DelegateCommand(Down, ValidateDownState);
				return _DownCommand as DelegateCommand;
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
		public DelegateCommand UpCommand {
			get {
				if (_UpCommand == null)
					_UpCommand = new DelegateCommand(Up, ValidateUpState);
				return _UpCommand as DelegateCommand;
			}
		}
		#endregion Public Properties
	}
}
