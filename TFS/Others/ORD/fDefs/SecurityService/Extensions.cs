// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:37:30
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// Extensions.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Enum Authorizations
	/// </summary>
	[Flags]
	public enum Authorizations : long
	{
		/// <summary>
		/// The none
		/// </summary>
		[Permission("No permissions", PermissionAttribute.PermissionTypes.Permission)]
		None = 0,
		/// <summary>
		/// The per_ read
		/// </summary>
		[Permission("Read", PermissionAttribute.PermissionTypes.Permission)]
		Per_Read = 1L << 0,
		/// <summary>
		/// The per_ edit
		/// </summary>
		[Permission("Edit", PermissionAttribute.PermissionTypes.Permission)]
		Per_Edit = 1L << 1,
		/// <summary>
		/// The per_ create
		/// </summary>
		[Permission("Create", PermissionAttribute.PermissionTypes.Permission)]
		Per_Create = 1L << 2,
		/// <summary>
		/// The per_ delete
		/// </summary>
		[Permission("Delete", PermissionAttribute.PermissionTypes.Permission)]
		Per_Delete = 1L << 3,
		/// <summary>
		/// The per_ update
		/// </summary>
		[Permission("Update", PermissionAttribute.PermissionTypes.Permission)]
		Per_Update = 1L << 4,
		/// <summary>
		/// The per_ admin
		/// </summary>
		[Permission("Administrator", PermissionAttribute.PermissionTypes.Permission)]
		Per_Admin = 1L << 5,
		/// <summary>
		/// The per_ upload
		/// </summary>
		[Permission("Upload", PermissionAttribute.PermissionTypes.Permission)]
		Per_Upload = 1L << 6,
		/// <summary>
		/// The per_ manage users
		/// </summary>
		[Permission("Manage Users", PermissionAttribute.PermissionTypes.Permission)]
		Per_ManageUsers = 1L << 7,
		//Permission9 = 1L << 8,
		//Permission10 = 1L << 9,
		//Permission11 = 1L << 10,
		//Permission12 = 1L << 11,
		//Permission13 = 1L << 12,
		//Permission14 = 1L << 13,
		//Permission15 = 1L << 14,
		//Permission16 = 1L << 15,
		//Application1 = 1L << 16,
		//Application2 = 1L << 17,
		//Application3 = 1L << 18,
		//Application4 = 1L << 19,
		//Application5 = 1L << 20,
		//Application6 = 1L << 21,
		//Application7 = 1L << 22,
		//Application8 = 1L << 23,
		//Application9 = 1L << 24,
		//Application10 = 1L << 25,
		//Application11 = 1L << 26,
		//Application12 = 1L << 27,
		//Application13 = 1L << 28,
		//Application14 = 1L << 29,
		//Application15 = 1L << 30,
		//Application16 = 1L << 31,
		//Application17 = 1L << 32,
		//Application18 = 1L << 33,
		//Application19 = 1L << 34,
		//Application20 = 1L << 35,
		//Application21 = 1L << 36,
		//Application22 = 1L << 37,
		//Application23 = 1L << 38,
		//Application24 = 1L << 39,
		//Application25 = 1L << 40,
		//Application26 = 1L << 41,
		//Application27 = 1L << 42,
		//Application28 = 1L << 43,
		/// <summary>
		/// The app_ rod pump
		/// </summary>
		[Permission("Rod Pump", PermissionAttribute.PermissionTypes.Application)]
		App_RodPump = 1L << 44,
		/// <summary>
		/// The app_ opc server
		/// </summary>
		[Permission("OPC Server", PermissionAttribute.PermissionTypes.Application)]
		App_OPCServer = 1L << 45,
		/// <summary>
		/// The app_ trend viewer
		/// </summary>
		[Permission("Trend Viewer", PermissionAttribute.PermissionTypes.Application)]
		App_TrendViewer = 1L << 46,
		/// <summary>
		/// The app_ offline diagnostics
		/// </summary>
		[Permission("Offline Diagnostics", PermissionAttribute.PermissionTypes.Application)]
		App_OfflineDiagnostics = 1L << 47,
		/// <summary>
		/// The app_ diagnostics
		/// </summary>
		[Permission("Diagnostics", PermissionAttribute.PermissionTypes.Application)]
		App_Diagnostics = 1L << 48,
		/// <summary>
		/// The app_ runtime
		/// </summary>
		[Permission("Runtime", PermissionAttribute.PermissionTypes.Application)]
		App_Runtime = 1L << 49,
		/// <summary>
		/// The app_ simulator
		/// </summary>
		[Permission("Simulator", PermissionAttribute.PermissionTypes.Application)]
		App_Simulator = 1L << 50,
		/// <summary>
		/// The app_ developer
		/// </summary>
		[Permission("Developer", PermissionAttribute.PermissionTypes.Application)]
		App_Developer = 1L << 51,
		/// <summary>
		/// The app_ recorder
		/// </summary>
		[Permission("Recorder", PermissionAttribute.PermissionTypes.Application)]
		App_Recorder = 1L << 52,
		/// <summary>
		/// The app_ VTS
		/// </summary>
		[Permission("Virtual Tag Server", PermissionAttribute.PermissionTypes.Application)]
		App_VTS = 1L << 53,
		/// <summary>
		/// The app_ reporting server
		/// </summary>
		[Permission("Reporting Server", PermissionAttribute.PermissionTypes.Application)]
		App_ReportingServer = 1L << 54,
		/// <summary>
		/// The app_ notification server
		/// </summary>
		[Permission("Notification Server", PermissionAttribute.PermissionTypes.Application)]
		App_NotificationServer = 1L << 55,
		/// <summary>
		/// The app_ virtual tag server configuration
		/// </summary>
		[Permission("Virtual Tag Server Configuration", PermissionAttribute.PermissionTypes.Application)]
		App_VirtualTagServerConfig = 1L << 56,
		/// <summary>
		/// The app_ web analytics
		/// </summary>
		[Permission("Web Analytics", PermissionAttribute.PermissionTypes.Application)]
		App_WebAnalytics = 1L << 57,

		//Special1 = 1L << 58,
		//Special2 = 1L << 59,
		//Special3 = 1L << 60,
		//Special4 = 1L << 61,
		/// <summary>
		/// The spec_ system admin
		/// </summary>
		[Permission("System Administrator", PermissionAttribute.PermissionTypes.Special)]
		Spec_SystemAdmin = 1L << 62,
		/// <summary>
		/// The spec_ persistent
		/// </summary>
		[Permission("User is persistent", PermissionAttribute.PermissionTypes.Special)]
		Spec_Persistent = 1L << 63
	}

	/// <summary>
	/// Enum PermissionAttributeTypes
	/// </summary>
	public enum PermissionAttributeTypes
	{
		/// <summary>
		/// The none
		/// </summary>
		None,
		/// <summary>
		/// The name
		/// </summary>
		Name,
		/// <summary>
		/// The type
		/// </summary>
		Type,
		/// <summary>
		/// The first name
		/// </summary>
		FirstName,
		/// <summary>
		/// The last name
		/// </summary>
		LastName,
		/// <summary>
		/// The password
		/// </summary>
		Password,
		/// <summary>
		/// The value
		/// </summary>
		Value,
		/// <summary>
		/// The sub type
		/// </summary>
		SubType
	}

	/// <summary>
	/// Enum PermissionElementTypes
	/// </summary>
	public enum PermissionElementTypes
	{
		/// <summary>
		/// The none
		/// </summary>
		None,
		/// <summary>
		/// The root element
		/// </summary>
		RootElement,
		/// <summary>
		/// The group element
		/// </summary>
		GroupElement,
		/// <summary>
		/// The item element
		/// </summary>
		ItemElement,
		/// <summary>
		/// The user permissions element
		/// </summary>
		UserPermissionsElement,
		/// <summary>
		/// The individual user permission element
		/// </summary>
		IndividualUserPermissionElement,
		/// <summary>
		/// The role permissions element
		/// </summary>
		RolePermissionsElement,
		/// <summary>
		/// The individual role permission element
		/// </summary>
		IndividualRolePermissionElement,
		/// <summary>
		/// The references element
		/// </summary>
		ReferencesElement,
		/// <summary>
		/// The individual reference element
		/// </summary>
		IndividualReferenceElement
	}

	/// <summary>
	/// Class AuthorizationDefaults.
	/// </summary>
	public static class AuthorizationDefaults
	{
		#region Public Methods
		/// <summary>
		/// Descriptions the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.String.</returns>
		public static string Description(this Authorizations value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			PermissionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(PermissionAttribute)) as PermissionAttribute;
			return attribute == null ? value.ToString() : attribute.Description;
		}
		/// <summary>
		/// Permissions the type.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>SNC.Authorization.Management.PermissionAttribute.PermissionTypes.</returns>
		public static SNC.OptiRamp.Security.PermissionAttribute.PermissionTypes PermissionType(this Authorizations value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			PermissionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(PermissionAttribute)) as PermissionAttribute;
			return attribute == null ? SNC.OptiRamp.Security.PermissionAttribute.PermissionTypes.Unknown : attribute.PermissionType;
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets all rights.
		/// </summary>
		/// <value>All rights.</value>
		public static Authorizations AllRights
		{
			get
			{
				var names = Enum.GetNames(typeof(Authorizations));
				Authorizations result = Authorizations.None;
				foreach (var name in names)
				{
					result = result | (Authorizations)Enum.Parse(typeof(Authorizations), name, true);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets the applications.
		/// </summary>
		/// <value>The applications.</value>
		public static List<Authorizations> Applications
		{
			get
			{
				var names = Enum.GetNames(typeof(Authorizations)).OrderBy(x => x);
				List<Authorizations> result = new List<Authorizations>();
				foreach (var name in names)
				{
					var value = (Authorizations)Enum.Parse(typeof(Authorizations), name, true);
					var type = value.PermissionType();
					if (type == PermissionAttribute.PermissionTypes.Application)
						result.Add(value);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets the permissions.
		/// </summary>
		/// <value>The permissions.</value>
		public static List<Authorizations> Permissions
		{
			get
			{
				var names = Enum.GetNames(typeof(Authorizations)).OrderBy(x => x);
				List<Authorizations> result = new List<Authorizations>();
				foreach (var name in names)
				{
					if (name == "None")
						continue;
					var value = (Authorizations)Enum.Parse(typeof(Authorizations), name, true);
					var type = value.PermissionType();
					if (type == PermissionAttribute.PermissionTypes.Permission)
						result.Add(value);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets the special flags.
		/// </summary>
		/// <value>The special flags.</value>
		public static List<Authorizations> SpecialFlags
		{
			get
			{
				var names = Enum.GetNames(typeof(Authorizations)).OrderBy(x => x);
				List<Authorizations> result = new List<Authorizations>();
				foreach (var name in names)
				{
					if (name == "Spec_Persistent")
						continue;
					var value = (Authorizations)Enum.Parse(typeof(Authorizations), name, true);
					var type = value.PermissionType();
					if (type == PermissionAttribute.PermissionTypes.Special)
						result.Add(value);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets the web analytics read permission.
		/// </summary>
		/// <value>The web analytics read permission.</value>
		public static Authorizations WebAnalyticsReadPermission
		{
			get
			{
				return Authorizations.App_WebAnalytics | Authorizations.Per_Read;
			}
		}
		#endregion Public Properties
	}

	public static class Extensions
	{
		#region Public Methods
		/// <summary>
		/// Gets the authorization for item.
		/// </summary>
		/// <param name="permItem">The perm item.</param>
		/// <param name="manager">The manager.</param>
		/// <returns>Authorizations.</returns>
		public static Authorizations GetAuthorizationForItem(this PermissionItem permItem, ISNCPermissionManager manager)
		{
			return permItem.GetAuthorizationForItem(manager, true);
		}

		/// <summary>
		/// Gets the authorization for item.
		/// </summary>
		/// <param name="permItem">The perm item.</param>
		/// <param name="manager">The manager.</param>
		/// <param name="isRecursive">if set to <c>true</c> [isRecursive].</param>
		/// <returns>Authorizations.</returns>
		public static Authorizations GetAuthorizationForItem(this PermissionItem permItem, ISNCPermissionManager manager, bool isRecursive)
		{
			return permItem.GetAuthorizationForItem(manager.Permissions.Where(x => x is Role).Cast<Role>().ToList(), isRecursive);
		}

		/// <summary>
		/// Gets the authorization for item.
		/// </summary>
		/// <param name="permItem">The perm item.</param>
		/// <param name="roles">The roles.</param>
		/// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
		/// <returns>Authorizations.</returns>
		public static Authorizations GetAuthorizationForItem(this PermissionItem permItem, IList<Role> roles, bool isRecursive)
		{
			Authorizations result = Authorizations.None;
			if (permItem == null)
				return result;
			result = permItem.Permission;
			if (!isRecursive)
				return result;
			if (!roles.Any())
				return result;
			foreach (var role in roles)
			{
				if (role.References.Any(x => x.Name.Equals(permItem.Name, StringComparison.OrdinalIgnoreCase)))
					result |= role.GetAuthorizationForItem(roles, isRecursive);
			}
			return result;
		}
		#endregion Public Methods
	}
}