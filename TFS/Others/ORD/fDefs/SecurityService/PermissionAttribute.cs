// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:38:09
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// PermissionAttribute.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Class PermissionAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class PermissionAttribute : Attribute
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionAttribute" /> class.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="permissionType">Type of the permission.</param>
		public PermissionAttribute(string description, PermissionTypes permissionType)
		{
			Description = description;
			PermissionType = permissionType;
		}

		#endregion

		#region Public Enums

		/// <summary>
		/// Enum PermissionTypes
		/// </summary>
		public enum PermissionTypes
		{
			/// <summary>
			/// The unknown
			/// </summary>
			Unknown,
			/// <summary>
			/// The permission
			/// </summary>
			Permission,
			/// <summary>
			/// The application
			/// </summary>
			Application,
			/// <summary>
			/// The special
			/// </summary>
			Special
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Gets or sets the type of the permission.
		/// </summary>
		/// <value>The type of the permission.</value>
		public PermissionTypes PermissionType { get; set; }
		#endregion
	}
}