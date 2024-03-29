// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-16-2015
// ***********************************************************************
// <copyright file="PermissionItem.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SNC.Authorization.Management;

namespace SNC.Authorization.Entities
{
	public abstract class PermissionItem
	{
		#region Public Constructors

		public PermissionItem(string name, List<Authorizations> permissions)
			: this(name)
		{
			Permissions = permissions;
		}

		public PermissionItem(string name)
		{
			Name = name;
			Permissions = new List<Authorizations>();
		}

		#endregion

		#region Public Properties
		public string Name { get; set; }

		public List<Authorizations> Permissions { get; private set; }
		#endregion

		#region Public Methods

		public virtual XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = new XElement(manager.ItemName, new XAttribute(manager.ItemNameAttributeName, Name));
			var type = this.GetType().Name;
			XElement topElement = null;
			if(this.GetType() == typeof(User))
				topElement = new XElement(manager.UserPermissionsName);
			else
				topElement = new XElement(manager.RolePermissionsName);
			foreach (var permission in Permissions)
			{
				if (type == "User")
					topElement.Add(new XElement(manager.UserPermissionName, new XAttribute(manager.UserValueAttributeName, Convert.ToInt64(permission))));
				else
					topElement.Add(new XElement(manager.RolePermissionName, new XAttribute(manager.RoleValueAttributeName, Convert.ToInt64(permission))));
			}
			result.Add(topElement);
			return result;
		}

		#endregion
	}
}
