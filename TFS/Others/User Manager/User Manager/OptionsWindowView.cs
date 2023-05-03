// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-25-2015
//
// Last Modified By : Greg
// Last Modified On : 07-02-2015
// ***********************************************************************
// <copyright file="OptionsWindowView.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MVVMFramework;
using SNC.Authorization.Management;
using User_Manager.Classes;

namespace User_Manager
{
	public class OptionsWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private bool _AreDefaultValues;
		private RelayCommand _CancelCommand;
		private RelayCommand _CheckForNewVersionCommand;
		private string _FileName;
		private string _GroupName;
		private string _InstallationFolder;
		private string _InstallerFileName;
		private RelayCommand _InstallFolderCommand;
		private string _ItemFirstNameAttributeName;
		private string _ItemLastNameAttributeName;
		private string _ItemName;
		private string _ItemNameAttributeName;
		private string _ItemPasswordAttributeName;
		private string _ItemTypeAttributeName;
		private ISNCPermissionManager _Manager;
		private bool _ManuallyInstallUpdates;
		private RelayCommand _OKCommand;
		private string _ReferenceName;
		private string _ReferenceNameAttributeName;
		private string _ReferencesName;
		private string _ReferenceSubTypeAttributeName;
		private string _ReferenceTypeAttributeName;
		private bool _RemoteInstallAvailable;
		private RelayCommand _ResetCommand;
		private string _RolePermissionValueName;
		private string _RolesPermissionName;
		private string _RolesPermissionsName;
		private string _RootName;
		private string _UpdateMessage;
		private string _UserPermissionValueName;
		private string _UsersPermissionName;
		private string _UsersPermissionsName;
		#endregion

		#region Public Constructors
		private RelayCommand _ClearRecentCommand;
		public RelayCommand ClearRecentCommand
		{
			get
			{
				if (_ClearRecentCommand == null)
					_ClearRecentCommand = new RelayCommand(ClearRecent) { IsEnabled = true };
				return _ClearRecentCommand;
			}
		}
		private void ClearRecent()
		{
			ApplicationSettings.ClearRecentItems();
		}
		public OptionsWindowView()
		{
			InstallationFolder = ApplicationSettings.GetValue<string>("Application", "RemoteInstallFolder", App.DefaultInstallFolder);
			InstallerFileName = ApplicationSettings.GetValue<string>("Application", "RemoteInstallerFileName", App.DefaultInstaller);
			ManuallyInstallUpdates = ApplicationSettings.GetValue<bool>("Application", "ManuallyInstallUpdates", true);
		}

		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteCommand;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public bool AreDefaultValues
		{
			get { return _AreDefaultValues; }
			set
			{
				_AreDefaultValues = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AreDefaultValues"));
			}
		}
		public RelayCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new RelayCommand(Cancel) { IsEnabled = true };
				return _CancelCommand;
			}
		}
		public RelayCommand CheckForNewVersionCommand
		{
			get
			{
				if (_CheckForNewVersionCommand == null)
					_CheckForNewVersionCommand = new RelayCommand(CheckForNewVersion) { IsEnabled = true };
				return _CheckForNewVersionCommand;
			}
		}
		public string FileName
		{
			get { return _FileName; }
			set
			{
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public string GroupName
		{
			get { return _GroupName; }
			set
			{
				_GroupName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("GroupName"));
			}
		}
		public string InstallationFolder
		{
			get { return _InstallationFolder; }
			set
			{
				_InstallationFolder = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InstallationFolder"));
			}
		}
		public string InstallerFileName
		{
			get { return _InstallerFileName; }
			set
			{
				_InstallerFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InstallerFileName"));
			}
		}
		public RelayCommand InstallFolderCommand
		{
			get
			{
				if (_InstallFolderCommand == null)
					_InstallFolderCommand = new RelayCommand(InstallFolder) { IsEnabled = true };
				return _InstallFolderCommand;
			}
		}
		public string ItemFirstNameAttributeName
		{
			get { return _ItemFirstNameAttributeName; }
			set
			{
				_ItemFirstNameAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemFirstNameAttributeName"));
			}
		}
		public string ItemLastNameAttributeName
		{
			get { return _ItemLastNameAttributeName; }
			set
			{
				_ItemLastNameAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemLastNameAttributeName"));
			}
		}
		public string ItemName
		{
			get { return _ItemName; }
			set
			{
				_ItemName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
			}
		}
		public string ItemNameAttributeName
		{
			get { return _ItemNameAttributeName; }
			set
			{
				_ItemNameAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemNameAttributeName"));
			}
		}
		public string ItemPasswordAttributeName
		{
			get { return _ItemPasswordAttributeName; }
			set
			{
				_ItemPasswordAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemPasswordAttributeName"));
			}
		}
		public string ItemTypeAttributeName
		{
			get { return _ItemTypeAttributeName; }
			set
			{
				_ItemTypeAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemTypeAttributeName"));
			}
		}
		public ISNCPermissionManager Manager
		{
			get { return _Manager; }
			set
			{
				if (value == null)
				{
					value = new Manager();
					value.CustomNames = ApplicationSettings.GetCustomNames();
				}
				_Manager = value;
				RootName = _Manager.RootName;
				GroupName = _Manager.GroupName;
				ItemName = _Manager.ItemName;
				ItemNameAttributeName = _Manager.ItemNameAttributeName;
				ItemTypeAttributeName = _Manager.ItemTypeAttributeName;
				ItemFirstNameAttributeName = _Manager.ItemFirstNameAttributeName;
				ItemLastNameAttributeName = _Manager.ItemLastNameAttributeName;
				ItemPasswordAttributeName = _Manager.ItemPasswordAttributeName;
				UsersPermissionsName = _Manager.UserPermissionsName;
				UsersPermissionName = _Manager.UserPermissionName;
				UserPermissionValueName = _Manager.UserValueAttributeName;
				RolesPermissionsName = _Manager.RolePermissionsName;
				RolesPermissionName = _Manager.RolePermissionName;
				RolePermissionValueName = _Manager.RoleValueAttributeName;
				ReferencesName = _Manager.ReferencesName;
				ReferenceName = _Manager.ReferenceName;
				ReferenceNameAttributeName = _Manager.ReferenceNameAttributeName;
				ReferenceTypeAttributeName = _Manager.ReferenceTypeAttributeName;
				ReferenceSubTypeAttributeName = _Manager.ReferenceSubTypeAttributeName;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
			}
		}
		public bool ManuallyInstallUpdates
		{
			get { return _ManuallyInstallUpdates; }
			set
			{
				_ManuallyInstallUpdates = value;
				RemoteInstallAvailable = !_ManuallyInstallUpdates;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ManuallyInstallUpdates"));
			}
		}
		public RelayCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new RelayCommand(OK) { IsEnabled = true };
				return _OKCommand;
			}
		}
		public string ReferenceName
		{
			get { return _ReferenceName; }
			set
			{
				_ReferenceName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ReferenceName"));
			}
		}
		public string ReferenceNameAttributeName
		{
			get { return _ReferenceNameAttributeName; }
			set
			{
				_ReferenceNameAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ReferenceNameAttributeName"));
			}
		}
		public string ReferencesName
		{
			get { return _ReferencesName; }
			set
			{
				_ReferencesName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ReferencesName"));
			}
		}
		public string ReferenceSubTypeAttributeName
		{
			get { return _ReferenceSubTypeAttributeName; }
			set
			{
				_ReferenceSubTypeAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ReferenceSubTypeAttributeName"));
			}
		}
		public string ReferenceTypeAttributeName
		{
			get { return _ReferenceTypeAttributeName; }
			set
			{
				_ReferenceTypeAttributeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ReferenceTypeAttributeName"));
			}
		}
		public bool RemoteInstallAvailable
		{
			get { return _RemoteInstallAvailable; }
			set
			{
				_RemoteInstallAvailable = value;
				//CheckForNewVersionCommand.IsEnabled = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoteInstallAvailable"));
			}
		}
		public RelayCommand ResetCommand
		{
			get
			{
				if (_ResetCommand == null)
					_ResetCommand = new RelayCommand(Reset) { IsEnabled = true };
				return _ResetCommand;
			}
		}
		public string RolePermissionValueName
		{
			get { return _RolePermissionValueName; }
			set
			{
				_RolePermissionValueName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RolePermissionValueName"));
			}
		}
		public string RolesPermissionName
		{
			get { return _RolesPermissionName; }
			set
			{
				_RolesPermissionName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RolesPermissionName"));
			}
		}
		public string RolesPermissionsName
		{
			get { return _RolesPermissionsName; }
			set
			{
				_RolesPermissionsName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RolesPermissionsName"));
			}
		}
		public string RootName
		{
			get { return _RootName; }
			set
			{
				_RootName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RootName"));
			}
		}
		public string UpdateMessage
		{
			get { return _UpdateMessage; }
			set
			{
				_UpdateMessage = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UpdateMessage"));
			}
		}
		public string UserPermissionValueName
		{
			get { return _UserPermissionValueName; }
			set
			{
				_UserPermissionValueName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UserPermissionValueName"));
			}
		}

		public string UsersPermissionName
		{
			get { return _UsersPermissionName; }
			set
			{
				_UsersPermissionName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UsersPermissionName"));
			}
		}

		public string UsersPermissionsName
		{
			get { return _UsersPermissionsName; }
			set
			{
				_UsersPermissionsName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UsersPermissionsName"));
			}
		}
		#endregion

		#region Private Methods

		private void Cancel()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", false } }));
		}

		private void CheckForNewVersion()
		{
			UpdateMessage = string.Empty;
			if (!App.AppRequiresUpdate)
				UpdateMessage = "The application version is up-to-date";
			else
				App.InstallNewApplicationVersion();
		}

		private void InstallFolder()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowFolderBrowseDialog", null));
		}

		private void OK()
		{
			ApplicationSettings.SetValue<string>("Application", "RemoteInstallFolder", InstallationFolder);
			ApplicationSettings.SetValue<string>("Application", "RemoteInstallerFileName", InstallerFileName);
			ApplicationSettings.SetValue<bool>("Application", "ManuallyInstallUpdates", ManuallyInstallUpdates);

			if (Manager != null)
			{
				Manager.CustomNames["Root"] = RootName;
				Manager.CustomNames["Group"] = GroupName;
				Manager.CustomNames["Item"] = ItemName;
				Manager.CustomNames["ItemName"] = ItemNameAttributeName;
				Manager.CustomNames["ItemType"] = ItemTypeAttributeName;
				Manager.CustomNames["ItemFirstName"] = ItemFirstNameAttributeName;
				Manager.CustomNames["ItemLastName"] = ItemLastNameAttributeName;
				Manager.CustomNames["Password"] = ItemPasswordAttributeName;
				Manager.CustomNames["UserPermissions"] = UsersPermissionsName;
				Manager.CustomNames["UserPermission"] = UsersPermissionName;
				Manager.CustomNames["PermissionValue"] = UserPermissionValueName;
				Manager.CustomNames["RolePermissions"] = RolesPermissionsName;
				Manager.CustomNames["RolePermission"] = RolesPermissionName;
				Manager.CustomNames["PermissionValue"] = RolePermissionValueName;
				Manager.CustomNames["References"] = ReferencesName;
				Manager.CustomNames["Reference"] = ReferenceName;
				Manager.CustomNames["ReferenceName"] = ReferenceNameAttributeName;
				Manager.CustomNames["ReferenceType"] = ReferenceTypeAttributeName;
				Manager.CustomNames["ReferenceSubType"] = ReferenceSubTypeAttributeName;
			}
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", true } }));
		}

		private void Reset()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ResetToDefaultValues", null));
		}

		#endregion
	}
}
