// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-18-2015
// ***********************************************************************
// <copyright file="EditUserWindowView.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MVVMFramework;
using SNC.Authorization;
using SNC.Authorization.Entities;
using User_Manager.Classes;

namespace User_Manager
{
	public class EditUserWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private List<AuthorizationSelector> _Applications = null;
		private RelayCommand _CancelCommand;
		private string _FirstName;
		private PermissionItemSelector _Item = null;
		private List<PermissionItemSelector> _Items;
		private string _LastName;
		private string _Name;
		private RelayCommand _OKCommand;
		private List<AuthorizationSelector> _Permissions = null;
		private List<PermissionItemSelector> _Roles;
		private Visibility _RoleVisibility;
		private List<AuthorizationSelector> _SpecialFlags = null;
		private Visibility _UserVisibility;
		#endregion

		#region Public Constructors

		public EditUserWindowView()
		{
			RoleVisibility = Visibility.Visible;
			UserVisibility = Visibility.Collapsed;
		}

		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteCommand;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public List<AuthorizationSelector> Applications
		{
			get
			{
				if (_Applications == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.Applications;
					values.ForEach(x => result.Add(new AuthorizationSelector { Authorization = x, IsSelected = false }));
					_Applications = result;
				}
				return _Applications;
			}
			set
			{
				_Applications = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Applications"));
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
		public string FirstName
		{
			get { return _FirstName; }
			set
			{
				_FirstName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
			}
		}
		public PermissionItemSelector Item
		{
			get { return _Item; }
			set
			{
				_Item = value;
				Name = _Item.Item.Name;
				if (_Item.Type.Equals("User"))
				{
					FirstName = (_Item.Item as User).FirstName;
					LastName = (_Item.Item as User).LastName;
				}
				RoleVisibility = value.Item is Role ? Visibility.Visible : Visibility.Collapsed;
				UserVisibility = value.Item is User ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
			}
		}
		public List<PermissionItemSelector> Items
		{
			get { return _Items; }
			set
			{
				_Items = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Items"));
			}
		}
		public string LastName
		{
			get { return _LastName; }
			set
			{
				_LastName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
			}
		}
		public string Name
		{
			get { return _Name; }
			set
			{
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
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
		public List<AuthorizationSelector> Permissions
		{
			get
			{
				if (_Permissions == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.Permissions;
					values.ForEach(x => result.Add(new AuthorizationSelector { Authorization = x, IsSelected = false }));
					_Permissions = result;
				}
				return _Permissions;
			}
			set
			{
				_Permissions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Permissions"));
			}
		}

		public List<PermissionItemSelector> Roles
		{
			get { return _Roles; }
			set
			{
				_Roles = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Roles"));
			}
		}

		public Visibility RoleVisibility
		{
			get { return _RoleVisibility; }
			set
			{
				_RoleVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RoleVisibility"));
			}
		}

		public List<AuthorizationSelector> SpecialFlags
		{
			get
			{
				if (_SpecialFlags == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.SpecialFlags;
					values.ForEach(x => result.Add(new AuthorizationSelector { Authorization = x, IsSelected = false }));
					_SpecialFlags = result;
				}
				return _SpecialFlags;
			}
			set
			{
				_SpecialFlags = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SpecialFlags"));
			}
		}

		public Visibility UserVisibility
		{
			get { return _UserVisibility; }
			set
			{
				_UserVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UserVisibility"));
			}
		}
		#endregion

		#region Private Methods

		private void Cancel()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", false } }));
		}

		private void OK()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", true } }));
		}

		#endregion
	}
}
