// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 
//		06/21/2016		Greg	Original
//		07/19/2016		Greg	Added SSID Property for Domain Group
// -----------------------------------------------------------------------
// 
// Role.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
	using SNC.OptiRamp.Security;

	public class Role : PermissionItem
	{
		#region Public Constructors
		public Role()
		{
		}

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
		public string OtherData { get; set; }
		public string SSID { get; set; }
		#endregion

		#region Public Methods
		public static Role FromXElement(XElement element, ISNCPermissionManager manager)
		{
			Role result = null;
			if (element.Name.LocalName.Equals(manager.ItemName)
				&& element.Attribute(manager.ItemTypeAttributeName) != null
				&& element.Attribute(manager.ItemTypeAttributeName).Value == "Role"
				&& element.Attribute(manager.ItemNameAttributeName) != null)
			{
				result = new Role(element.Attribute(manager.ItemNameAttributeName).Value);
				if (element.Attribute(manager.ItemSSIDAttributeName) != null)
					result.SSID = element.Attribute(manager.ItemSSIDAttributeName).Value;
				if (element.Attribute(manager.OtherDataAttributeName) != null)
				{
					result.OtherData = element.Attribute(manager.OtherDataAttributeName).Value;
				}
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
				result.Permission = result.GetAuthorizationForItem(manager);
			}
			return result;
		}
		public override XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = base.ToXElement(manager);
			result.Add(new XAttribute(manager.ItemTypeAttributeName, this.GetType().Name));
			result.Add(new XAttribute(manager.ItemSSIDAttributeName, SSID != null ? SSID : string.Empty));
			result.Add(new XAttribute(manager.OtherDataAttributeName, this.OtherData == null ? string.Empty : this.OtherData));
			var refElement = new XElement(manager.ReferencesName);
			foreach (var refnce in References)
			{
				refElement.Add(refnce.ToXElement(manager));
			}
			result.Add(refElement);
			return result;
		}

		#endregion
	}
}