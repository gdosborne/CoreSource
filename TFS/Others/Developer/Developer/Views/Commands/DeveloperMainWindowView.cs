// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.Developer.Views {
	using GregOsborne.MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal partial class DeveloperMainWindowView {
		private DelegateCommand _AboutCommand = null;
		public DelegateCommand AboutCommand {
			get {
				if (_AboutCommand == null)
					_AboutCommand = new DelegateCommand(About, ValidateAboutState);
				return _AboutCommand as DelegateCommand;
			}
		}
		private void About(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("ShowAboutDialog"));
		}
		private bool ValidateAboutState(object state) {
			return true;
		}

		#region Private Methods
		private void CloseProject(object state) {
			if (ProjectFile.IsChanged) {
				if (ExecuteUIAction != null)
					ExecuteUIAction(this, new ExecuteUiActionEventArgs("CloseProjectFile"));
				return;
			}
			ProjectFile = null;
		}
		private void NewProject(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("NewProjectFile"));
		}
		private void OpenLocal(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("ShowLocalOpenDialog"));
		}
		private void OpenRemote(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("ShowRemoteOpenDialog"));
		}
		private void SaveProject(object state) {
			Save();
		}
		private void SaveProjectAs(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("SaveProjectFileAs"));
		}
		private bool ValidateCloseProjectState(object state) {
			return ProjectFile != null && !ProjectFile.IsChanged;
		}
		private bool ValidateNewProjectState(object state) {
			return ProjectFile == null || !ProjectFile.IsChanged;
		}
		private bool ValidateOpenLocalState(object state) {
			return ProjectFile == null || !ProjectFile.IsChanged;
		}
		private bool ValidateOpenRemoteState(object state) {
			return ProjectFile == null || !ProjectFile.IsChanged;
		}
		private bool ValidateSaveProjectAsState(object state) {
			return ProjectFile != null;
		}
		private bool ValidateSaveProjectState(object state) {
			return ProjectFile != null && ProjectFile.IsChanged;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUiActionHandler ExecuteUIAction;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CloseProjectCommand = null;
		private DelegateCommand _NewProjectCommand = null;
		private DelegateCommand _OpenLocalCommand = null;
		private DelegateCommand _OpenRemoteCommand = null;
		private DelegateCommand _SaveProjectAsCommand = null;
		private DelegateCommand _SaveProjectCommand = null;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CloseProjectCommand {
			get {
				if (_CloseProjectCommand == null)
					_CloseProjectCommand = new DelegateCommand(CloseProject, ValidateCloseProjectState);
				return _CloseProjectCommand as DelegateCommand;
			}
		}
		public DelegateCommand NewProjectCommand {
			get {
				if (_NewProjectCommand == null)
					_NewProjectCommand = new DelegateCommand(NewProject, ValidateNewProjectState);
				return _NewProjectCommand as DelegateCommand;
			}
		}
		public DelegateCommand OpenLocalCommand {
			get {
				if (_OpenLocalCommand == null)
					_OpenLocalCommand = new DelegateCommand(OpenLocal, ValidateOpenLocalState);
				return _OpenLocalCommand as DelegateCommand;
			}
		}
		public DelegateCommand OpenRemoteCommand {
			get {
				if (_OpenRemoteCommand == null)
					_OpenRemoteCommand = new DelegateCommand(OpenRemote, ValidateOpenRemoteState);
				return _OpenRemoteCommand as DelegateCommand;
			}
		}
		public DelegateCommand SaveProjectAsCommand {
			get {
				if (_SaveProjectAsCommand == null)
					_SaveProjectAsCommand = new DelegateCommand(SaveProjectAs, ValidateSaveProjectAsState);
				return _SaveProjectAsCommand as DelegateCommand;
			}
		}
		public DelegateCommand SaveProjectCommand {
			get {
				if (_SaveProjectCommand == null)
					_SaveProjectCommand = new DelegateCommand(SaveProject, ValidateSaveProjectState);
				return _SaveProjectCommand as DelegateCommand;
			}
		}
		private DelegateCommand _SettingsXCommand = null;
		public DelegateCommand SettingsXCommand {
			get {
				if (_SettingsXCommand == null)
					_SettingsXCommand = new DelegateCommand(SettingsX, ValidateSettingsXState);
				return _SettingsXCommand as DelegateCommand;
			}
		}
		private void SettingsX(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("ShowSettings"));
		}
		private bool ValidateSettingsXState(object state) {
			return true;
		}

		#endregion Public Properties
	}
}
