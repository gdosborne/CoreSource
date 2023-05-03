// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:38:21
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// PermissionItem.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.Xml.Linq;
	using SNC.OptiRamp.Security;

	/// <summary>
	/// Class PermissionItem.
	/// </summary>
	/// <remarks>Base for all items needing permissions</remarks>
	public abstract class PermissionItem
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionItem" /> class.
		/// </summary>
		public PermissionItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionItem" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="permissions">The permissions.</param>
		public PermissionItem(string name, List<Authorizations> permissions)
			: this(name)
		{
			Permissions = permissions;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionItem" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public PermissionItem(string name)
		{
			Name = name;
			Permissions = new List<Authorizations>();
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the permission.
		/// </summary>
		/// <value>The permission.</value>
		public Authorizations Permission 
		{ 
			get
			{
				Authorizations result = Authorizations.None;
				foreach (var p in Permissions)
				{
					result = result |= p;
				}
				return result;
			}
			set {
				_Permission = value;
			} 
		}
		private Authorizations _Permission = Authorizations.None;
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the permissions.
		/// </summary>
		/// <value>The permissions.</value>
		public List<Authorizations> Permissions { get; set; }
		#endregion

		#region Public Methods

		/// <summary>
		/// Serializes this instance to an XElement
		/// </summary>
		/// <param name="manager">The manager.</param>
		/// <returns>XElement.</returns>
		public virtual XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = new XElement(manager.ItemName, new XAttribute(manager.ItemNameAttributeName, Name));
			var type = this.GetType().Name;
			XElement topElement = null;
			if (this.GetType() == typeof(User))
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