// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-17-2015
// ***********************************************************************
// <copyright file="AddUserWindowView.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MVVMFramework;
using User_Manager.Classes;

namespace User_Manager
{
	public class AddUserWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private RelayCommand _CancelCommand;
		private string _FirstName;
		private Visibility _FullNameVisibility;
		private string _LastName;
		private string _Name;
		private RelayCommand _OKCommand;
		private string _Password;
		private string _Type;
		private List<string> _Types;
		#endregion

		#region Public Constructors

		public AddUserWindowView()
		{
			FullNameVisibility = Visibility.Collapsed;
			MembersVisibility = Visibility.Visible;
			Types = new List<string> { "Role", "User" };
			Type = "Role";
		}

		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteCommand;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
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
		public Visibility FullNameVisibility
		{
			get { return _FullNameVisibility; }
			set
			{
				_FullNameVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FullNameVisibility"));
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
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		public RelayCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new RelayCommand(OK) { IsEnabled = false };
				return _OKCommand;
			}
		}
		public string Password
		{
			get { return _Password; }
			set
			{
				_Password = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Password"));
			}
		}

		public string Type
		{
			get { return _Type; }
			set
			{
				_Type = value;
				FullNameVisibility = value == "Role" ? Visibility.Collapsed : Visibility.Visible;
				MembersVisibility = value == "Role" ? Visibility.Visible : Visibility.Collapsed;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Type"));
			}
		}

		public List<string> Types
		{
			get { return _Types; }
			set
			{
				_Types = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Types"));
			}
		}
		private Visibility _MembersVisibility;
		public Visibility MembersVisibility
		{
			get { return _MembersVisibility; }
			set
			{
				_MembersVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MembersVisibility"));
			}
		}
		private List<PermissionItemSelector> _Items;
		public List<PermissionItemSelector> Items
		{
			get { return _Items; }
			set
			{
				_Items = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Items"));
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

		private void UpdateInterface()
		{
			OKCommand.IsEnabled = !string.IsNullOrEmpty(Type)
				&& !string.IsNullOrEmpty(Name)
				&& (Type == "Role" || (Type == "User" && !string.IsNullOrEmpty(Password)));
		}

		#endregion
	}
}
