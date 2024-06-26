// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-17-2015
// ***********************************************************************
// <copyright file="PermissionItemSelector.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SNC.Authorization;
using SNC.Authorization.Entities;

namespace User_Manager.Classes
{
	public class PermissionItemSelector : INotifyPropertyChanged
	{
		#region Private Fields
		private bool _IsReadOnly;
		private string _Name;

		private string _Type;
		#endregion

		#region Public Constructors

		public PermissionItemSelector(PermissionItem item)
		{
			Item = item;
			Name = item.Name;
			Type = item.GetType().Name;
			foreach (var p in item.Permissions)
			{
				if((p & Authorizations.Spec_Persistent) == Authorizations.Spec_Persistent)
				{
					IsReadOnly = true;
					break;
				}
			}
		}

		#endregion

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public List<AuthorizationSelector> Applications
		{
			get
			{
				return GetList(AuthorizationDefaults.Applications);
			}
		}
		public bool IsReadOnly
		{
			get { return _IsReadOnly; }
			set
			{
				_IsReadOnly = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsReadOnly"));
			}
		}
		public PermissionItem Item { get; private set; }
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
		public List<AuthorizationSelector> Permissions
		{
			get
			{
				return GetList(AuthorizationDefaults.Permissions);
			}
		}
		private bool _IsSelected;
		public bool IsSelected
		{
			get { return _IsSelected; }
			set
			{
				_IsSelected = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
			}
		}
		public List<AuthorizationSelector> SpecialFlags
		{
			get
			{
				return GetList(AuthorizationDefaults.SpecialFlags);
			}
		}
		public string Type
		{
			get { return _Type; }
			set
			{
				_Type = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Type"));
			}
		}
		#endregion

		#region Private Methods

		private void a_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var auth = (AuthorizationSelector)sender;
			if (auth.IsSelected)
				Item.Permissions[0] |= auth.Authorization;
			else
				Item.Permissions[0] &= ~auth.Authorization;
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}

		private List<AuthorizationSelector> GetList(List<Authorizations> compare)
		{
			var result = new List<AuthorizationSelector>();
			compare.ForEach(x =>
			{
				bool isSelected = false;
				foreach (var p in Item.Permissions)
				{
					if ((p & x) == x)
					{
						isSelected = true;
						break;
					}
				}
				var a = new AuthorizationSelector { Authorization = x, IsSelected = isSelected };
				a.PropertyChanged += a_PropertyChanged;
				result.Add(a);
			});
			return result;
		}

		#endregion
	}
}
