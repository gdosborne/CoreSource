// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-16-2015
// ***********************************************************************
// <copyright file="User.cs" company="Statistics and Controls, Inc.">
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
	public class User : PermissionItem
	{
		#region Public Constructors

		public User(string name)
			: base(name) { }

		public User(string name, string firstName, string lastName)
			: base(name)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public User(string name, string firstName, string lastName, List<Authorizations> permissions)
			: base(name, permissions)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		#endregion

		#region Public Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		#endregion

		#region Public Methods

		public override XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = base.ToXElement(manager);
			result.Add(new XAttribute(manager.ItemTypeAttributeName, this.GetType().Name));
			if (!string.IsNullOrEmpty(FirstName))
				result.Add(new XAttribute(manager.ItemFirstNameAttributeName, FirstName));
			if (!string.IsNullOrEmpty(LastName))
				result.Add(new XAttribute(manager.ItemLastNameAttributeName, LastName));
			if (!string.IsNullOrEmpty(Password))
				result.Add(new XAttribute(manager.ItemPasswordAttributeName, Password));
			return result;
		}

		#endregion

		#region Internal Methods

		internal static User FromReferenceXElement(XElement element, ISNCPermissionManager manager)
		{
			User result = null;
			if (element.Name.LocalName.Equals(manager.ItemName)
				&& element.Attribute(manager.ItemTypeAttributeName) != null
				&& element.Attribute(manager.ItemTypeAttributeName).Value == "User"
				&& element.Attribute(manager.ItemNameAttributeName) != null)
			{
				if (element.Attribute(manager.ItemFirstNameAttributeName) != null
					&& element.Attribute(manager.ItemLastNameAttributeName) != null)
					result = new User(element.Attribute(manager.ItemNameAttributeName).Value, element.Attribute(manager.ItemFirstNameAttributeName).Value, element.Attribute(manager.ItemLastNameAttributeName).Value);
				else
					result = new User(element.Attribute(manager.ItemNameAttributeName).Value);

				if (element.Element(manager.UserPermissionsName) != null)
				{
					foreach (var elem in element.Element(manager.UserPermissionsName).Elements())
					{
						if (elem.Attribute(manager.UserValueAttributeName) != null)
							(result.Permissions as List<Authorizations>).Add((Authorizations)long.Parse(elem.Attribute(manager.UserValueAttributeName).Value));
					}
				}
			}
			return result;
		}

		public static User FromXElement(XElement element, ISNCPermissionManager manager)
		{
			User result = null;
			if (element.Name.LocalName.Equals(manager.ItemName)
				&& element.Attribute(manager.ItemTypeAttributeName) != null
				&& element.Attribute(manager.ItemTypeAttributeName).Value == "User"
				&& element.Attribute(manager.ItemNameAttributeName) != null)
			{
				if (element.Attribute(manager.ItemFirstNameAttributeName) != null
					&& element.Attribute(manager.ItemLastNameAttributeName) != null)
					result = new User(element.Attribute(manager.ItemNameAttributeName).Value, element.Attribute(manager.ItemFirstNameAttributeName).Value, element.Attribute(manager.ItemLastNameAttributeName).Value);
				else
					result = new User(element.Attribute(manager.ItemNameAttributeName).Value);
				if (element.Attribute(manager.ItemPasswordAttributeName) != null)
					result.Password = element.Attribute(manager.ItemPasswordAttributeName).Value;

				if (element.Element(manager.UserPermissionsName) != null)
				{
					foreach (var elem in element.Element(manager.UserPermissionsName).Elements())
					{
						if (elem.Attribute(manager.UserValueAttributeName) != null)
						{
							var value = (Authorizations)long.Parse(elem.Attribute(manager.UserValueAttributeName).Value);
							if ((value & Authorizations.Spec_SystemAdmin) == Authorizations.Spec_SystemAdmin)
								value = AuthorizationDefaults.AllRights;
							(result.Permissions as List<Authorizations>).Add(value);
						}
					}
				}
			}
			return result;
		}

		#endregion
	}
}
