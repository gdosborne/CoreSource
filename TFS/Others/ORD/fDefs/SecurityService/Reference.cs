// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:38:27
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// Reference.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.Xml.Linq;

	/// <summary>
	/// Class Reference.
	/// </summary>
	public class Reference : PermissionItem
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Reference" /> class.
		/// </summary>
		public Reference()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Role" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public Reference(string name)
			: base(name)
		{
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the type of the sub.
		/// </summary>
		/// <value>The type of the sub.</value>
		public string SubType { get; set; }
		#endregion

		#region Public Methods

		/// <summary>
		/// Serializes this instance to an XElement
		/// </summary>
		/// <param name="manager">The manager.</param>
		/// <returns>XElement.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public override XElement ToXElement(ISNCPermissionManager manager)
		{
			var result = new XElement(manager.ReferenceName, new XAttribute(manager.ReferenceNameAttributeName, Name));
			result.Add(new XAttribute(manager.ReferenceTypeAttributeName, this.GetType().Name));
			result.Add(new XAttribute(manager.ReferenceSubTypeAttributeName, SubType));
			return result;
		}

		#endregion

		#region Internal Methods

		/// <summary>
		/// Gets the reference from the XElement
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>Role.</returns>
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