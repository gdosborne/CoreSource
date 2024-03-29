// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-16-2015
// ***********************************************************************
// <copyright file="Reference.cs" company="Statistics and Controls, Inc.">
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
	public class Reference : PermissionItem
	{
		#region Public Constructors

		public Reference(string name)
			: base(name)
		{
		}

		#endregion

		#region Public Properties
		public string SubType { get; set; }
		#endregion

		#region Public Methods

		public override XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = new XElement(manager.ReferenceName, new XAttribute(manager.ReferenceNameAttributeName, Name));
			result.Add(new XAttribute(manager.ReferenceTypeAttributeName, this.GetType().Name));
			result.Add(new XAttribute(manager.ReferenceSubTypeAttributeName, SubType));
			return result;
		}

		#endregion

		#region Internal Methods

		internal static Reference FromXElement(XElement element)
		{
			Reference result = null;
			if (element.Name.LocalName.Equals("reference")
				&& element.Attribute("type") != null
				&& element.Attribute("type").Value == "Reference"
				&& element.Attribute("subtype").Value != null
				&& (element.Attribute("subtype").Value == "User" || element.Attribute("subtype").Value == "Role")
				&& element.Attribute("name") != null)
			{
				result = new Reference(element.Attribute("name").Value);
				result.SubType = element.Attribute("subtype").Value;
			}
			return result;
		}

		#endregion
	}
}
