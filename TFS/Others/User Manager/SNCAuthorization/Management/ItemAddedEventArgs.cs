// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-19-2015
//
// Last Modified By : Greg
// Last Modified On : 06-19-2015
// ***********************************************************************
// <copyright file="ItemAddedEventArgs.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace SNC.Authorization.Management
{
	public delegate void ItemAddedHandler(object sender, ItemAddedEventArgs e);

	public class ItemAddedEventArgs : EventArgs
	{
		#region Public Constructors

		public ItemAddedEventArgs(string name)
		{
			Name = name;
		}

		#endregion

		#region Public Properties
		public string Name { get; private set; }
		#endregion
	}
}
