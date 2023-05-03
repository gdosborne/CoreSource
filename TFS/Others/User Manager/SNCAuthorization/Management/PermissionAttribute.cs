// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-16-2015
// ***********************************************************************
// <copyright file="PermissionAttribute.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace SNC.Authorization.Management
{
	[AttributeUsage(AttributeTargets.Field)]
	public class PermissionAttribute : Attribute
	{
		#region Public Constructors

		public PermissionAttribute(string description, PermissionTypes permissionType)
		{
			Description = description;
			PermissionType = permissionType;
		}

		#endregion

		#region Public Enums

		public enum PermissionTypes
		{
			Unknown,
			Permission,
			Application,
			Special
		}

		#endregion

		#region Public Properties
		public string Description { get; set; }
		public PermissionTypes PermissionType { get; set; }
		#endregion
	}
}
