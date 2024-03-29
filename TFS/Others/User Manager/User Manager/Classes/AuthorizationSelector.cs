// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-17-2015
// ***********************************************************************
// <copyright file="AuthorizationSelector.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SNC.Authorization;

namespace User_Manager.Classes
{
	public class AuthorizationSelector : INotifyPropertyChanged
	{
		#region Private Fields
		private Authorizations _Authorization;
		private bool _IsSelected;
		#endregion

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public Authorizations Authorization
		{
			get { return _Authorization; }
			set
			{
				_Authorization = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Authorization"));
			}
		}
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
		#endregion
	}
}
