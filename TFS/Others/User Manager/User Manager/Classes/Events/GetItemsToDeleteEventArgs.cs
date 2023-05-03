// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-19-2015
//
// Last Modified By : Greg
// Last Modified On : 06-19-2015
// ***********************************************************************
// <copyright file="DeleteItemsEventArgs.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace User_Manager.Classes.Events
{
	public delegate void GetItemsToDeleteHandler(object sender, GetItemsToDeleteEventArgs e);

	public class GetItemsToDeleteEventArgs : EventArgs
	{
		#region Public Constructors

		public GetItemsToDeleteEventArgs()
		{
			Items = new List<PermissionItemSelector>();
		}

		#endregion

		#region Public Properties
		public IList<PermissionItemSelector> Items { get; set; }
		#endregion
	}
}
