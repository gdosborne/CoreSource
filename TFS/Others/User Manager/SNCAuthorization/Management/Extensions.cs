// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-19-2015
//
// Last Modified By : Greg
// Last Modified On : 06-19-2015
// ***********************************************************************
// <copyright file="Extensions.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using SNC.Authorization.Entities;

namespace SNC.Authorization.Management
{
	public static class Extensions
	{
		#region Public Methods

		public static Authorizations GetAuthorizationForItem(this PermissionItem permItem, PermissionManager manager)
		{
			return permItem.GetAuthorizationForItem(manager, true);
		}

		public static Authorizations GetAuthorizationForItem(this PermissionItem permItem, PermissionManager manager, bool isRecursive)
		{
			Authorizations result = Authorizations.None;
			if (permItem == null)
				return result;
			result = permItem.Permissions.FirstOrDefault();
			if (!isRecursive)
				return result;
			var roles = manager.Permissions.Where(x => x is Role).Cast<Role>().ToList();
			if (!roles.Any())
				return result;
			foreach (var role in roles)
			{
				if (role.References.Any(x => x.Name.Equals(permItem.Name, StringComparison.OrdinalIgnoreCase)))
					result = result | role.GetAuthorizationForItem(manager, isRecursive);
			}
			return result;
		}

		#endregion
	}
}
