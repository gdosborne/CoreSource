// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:38:38
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// User.cs
//
namespace SNC.OptiRamp.Security
{
	using SNC.OptiRamp.ProjectService;
	using SNC.OptiRamp.Security;
	using System;
	using System.Collections.Generic;
	using System.Xml.Linq;

	/// <summary>
	/// Class User.
	/// </summary>
	public class User : PermissionItem
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="User" /> class.
		/// </summary>
		public User()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionItem" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public User(string name)
			: base(name) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="User" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		public User(string name, string firstName, string lastName)
			: base(name)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="firstName">The first name.</param>
		/// <param name="lastName">The last name.</param>
		/// <param name="permissions">The permissions.</param>
		public User(string name, string firstName, string lastName, List<Authorizations> permissions)
			: base(name, permissions)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; set; }
		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; set; }
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get; set; }
		#endregion

		#region Public Methods

		/// <summary>
		/// Froms the x element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="manager">The manager.</param>
		/// <returns>User.</returns>
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
				result.Permission = result.GetAuthorizationForItem(manager);
			}
			return result;
		}

		/// <summary>
		/// Serializes this instance to an XElement
		/// </summary>
		/// <param name="manager">The manager.</param>
		/// <returns>XElement.</returns>
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

		/// <summary>
		/// Froms the reference x element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="manager">The manager.</param>
		/// <returns>User.</returns>
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

		#endregion
	}
}