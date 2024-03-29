// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-16-2015
// ***********************************************************************
// <copyright file="Role.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SNC.Authorization.Management;

namespace SNC.Authorization.Entities
{
	public class Role : PermissionItem
	{
		#region Public Constructors

		public Role(string name, List<Reference> references)
			: base(name)
		{
			References = references;
		}

		public Role(string name)
			: base(name)
		{
			References = new List<Reference>();
		}

		public Role(string name, List<Authorizations> permissions)
			: base(name, permissions) { }

		#endregion

		#region Public Properties
		public List<Reference> References { get; private set; }
		#endregion

		#region Public Methods

		public override XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = base.ToXElement(manager);
			result.Add(new XAttribute(manager.ItemTypeAttributeName, this.GetType().Name));
			var refElement = new XElement(manager.ReferencesName);
			foreach (var refnce in References)
			{
				refElement.Add(refnce.ToXElement(manager));
			}
			result.Add(refElement);
			return result;
		}

		#endregion

		#region Internal Methods

		public static Role FromXElement(XElement element, ISNCPermissionManager manager)
		{
			Role result = null;
			if (element.Name.LocalName.Equals(manager.ItemName)
				&& element.Attribute(manager.ItemTypeAttributeName) != null
				&& element.Attribute(manager.ItemTypeAttributeName).Value == "Role"
				&& element.Attribute(manager.ItemNameAttributeName) != null)
			{
				result = new Role(element.Attribute(manager.ItemNameAttributeName).Value);
				if (element.Element(manager.ReferencesName) != null)
				{
					foreach (var elem in element.Element(manager.ReferencesName).Elements())
					{
						if (elem.Attribute(manager.ReferenceNameAttributeName) != null)
						{
							if (!(result.References as List<Reference>).Any(x => x.Name.Equals(elem.Attribute(manager.ReferenceNameAttributeName).Value)))
								(result.References as List<Reference>).Add(Reference.FromXElement(elem));
						}
					}
				}
				if (element.Element(manager.RolePermissionsName) != null)
				{
					foreach (var elem in element.Element(manager.RolePermissionsName).Elements())
					{
						if (elem.Attribute(manager.RoleValueAttributeName) != null)
							(result.Permissions as List<Authorizations>).Add((Authorizations)long.Parse(elem.Attribute(manager.RoleValueAttributeName).Value));
					}
				}
			}
			return result;
		}

		#endregion
	}
}
