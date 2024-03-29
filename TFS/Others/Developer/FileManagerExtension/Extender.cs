// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.Developer.Extensions.FileManagerExtension {
    using GregOsborne.Application.Primitives;
    using MVVMFramework;
    using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
    using SNC.OptiRamp.Application.DeveloperEntities.IO;
    using SNC.OptiRamp.Application.DeveloperEntities.Management;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Controls.Ribbon;

    [Export(typeof(IExtender))]
	public class Extender : IExtender {

		#region Public Constructors
		public Extender() {
			Name = "File Manager Extension";
			_Control = new ExtensionControl();
			OptionsCategory = new Category {
				Title = "File Manager Extension"
			};
			ExportedCommands = new List<DelegateCommand>();
		}
		#endregion Public Constructors
		public event ProjectChangedHandler ProjectChanged;
		#region Public Methods
		public void AddRibbonItems(System.Windows.Controls.Ribbon.Ribbon ribbon) {
			foreach (var tabItem in ribbon.Items) {
				if (tabItem is RibbonTab) {
					if (tabItem.As<RibbonTab>().Header is string) {
						if (((string)tabItem.As<RibbonTab>().Header).Equals("Tools")) {
							var rg = new RibbonGroup {
								Name = "FilesGroup",
								Header = "Files"
							};
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), NewFileCommand, "New File", "Adds a new ancillary file to the project file.", "newFile.png"));
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), NewFolderCommand, "New Folder", "Adds each file of the selected folder as a new ancillary file to the project file.", "newFolder.png"));
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), DeleteFileCommand, "Delete File", "Deletes an ancillary file from the project file.", "delete.png"));
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), ShowFilesControlCommand, "Show Files", "Shows the files currently a part of the project.", "files.png"));
							tabItem.As<RibbonTab>().Items.Add(rg);
							break;
						}
					}
				}
			}
		}
		public void UpdateInterface() {
			DeleteFileCommand.RaiseCanExecuteChanged();
			NewFileCommand.RaiseCanExecuteChanged();
			NewFolderCommand.RaiseCanExecuteChanged();
			ShowFilesControlCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void DeleteFile(object state) {
		}
		private void NewFile(object state) {
		}
		private void NewFolder(object state) {
		}
		private void ShowFilesControl(object state) {
			if (_Control != null && ShowUserControl != null)
				ShowUserControl(this, new ShowUserControlEventArgs(_Control));
		}
		private bool ValidateDeleteFileState(object state) {
			return ProjectFile != null;
		}
		private bool ValidateNewFileState(object state) {
			return ProjectFile != null;
		}
		private bool ValidateNewFolderState(object state) {
			return ProjectFile != null;
		}
		private bool ValidateShowFilesControlState(object state) {
			return ProjectFile != null;
		}
		#endregion Private Methods

		#region Public Events
		public event ShowUserControlHandler ShowUserControl;
		#endregion Public Events

		#region Private Fields
		private ExtensionControl _Control = null;
		private DelegateCommand _DeleteFileCommand = null;
		private IList<DelegateCommand> _ExportedCommands = null;
		private string _Name = string.Empty;
		private DelegateCommand _NewFileCommand = null;
		private DelegateCommand _NewFolderCommand = null;
		private Category _OptionsCategory = null;
		private ProjectFile _Project = null;
		private DelegateCommand _ShowFilesControlCommand = null;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand DeleteFileCommand {
			get {
				if (_DeleteFileCommand == null)
					_DeleteFileCommand = new DelegateCommand(DeleteFile, ValidateDeleteFileState);
				return _DeleteFileCommand as DelegateCommand;
			}
		}
		public IList<DelegateCommand> ExportedCommands {
			get {
				return _ExportedCommands;
			}
			private set {
				_ExportedCommands = value;
			}
		}
		public string Name {
			get {
				return _Name;
			}
			private set {
				_Name = value;
			}
		}
		public DelegateCommand NewFileCommand {
			get {
				if (_NewFileCommand == null)
					_NewFileCommand = new DelegateCommand(NewFile, ValidateNewFileState);
				return _NewFileCommand as DelegateCommand;
			}
		}
		public DelegateCommand NewFolderCommand {
			get {
				if (_NewFolderCommand == null)
					_NewFolderCommand = new DelegateCommand(NewFolder, ValidateNewFolderState);
				return _NewFolderCommand as DelegateCommand;
			}
		}
		public Category OptionsCategory {
			get {
				return _OptionsCategory;
			}
			private set {
				_OptionsCategory = value;
			}
		}
		public ProjectFile ProjectFile {
			get {
				return _Project;
			}
			set {
				_Project = value;
			}
		}
		public DelegateCommand ShowFilesControlCommand {
			get {
				if (_ShowFilesControlCommand == null)
					_ShowFilesControlCommand = new DelegateCommand(ShowFilesControl, ValidateShowFilesControlState);
				return _ShowFilesControlCommand as DelegateCommand;
			}
		}
		#endregion Public Properties
	}
}
