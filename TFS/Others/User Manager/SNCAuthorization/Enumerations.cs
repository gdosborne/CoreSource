using System;
using System.Collections.Generic;
using System.Reflection;
using SNC.Authorization.Management;
using System.Linq;

namespace SNC.Authorization
{
	public enum PermissionElementTypes
	{
		None,
		RootElement,
		GroupElement,
		ItemElement,
		UserPermissionsElement,
		IndividualUserPermissionElement,
		RolePermissionsElement,
		IndividualRolePermissionElement,
		ReferencesElement,
		IndividualReferenceElement
	}
	public enum PermissionAttributeTypes
	{
		None,
		Name,
		Type,
		FirstName,
		LastName,
		Password,
		Value,
		SubType
	}
	
	[Flags]
	public enum Authorizations : long
	{
		[Permission("No permissions", PermissionAttribute.PermissionTypes.Permission)]
		None = 0,
		[Permission("Read", PermissionAttribute.PermissionTypes.Permission)]
		Per_Read = 1L << 0,
		[Permission("Edit", PermissionAttribute.PermissionTypes.Permission)]
		Per_Edit = 1L << 1,
		[Permission("Create", PermissionAttribute.PermissionTypes.Permission)]
		Per_Create = 1L << 2,
		[Permission("Delete", PermissionAttribute.PermissionTypes.Permission)]
		Per_Delete = 1L << 3,
		[Permission("Update", PermissionAttribute.PermissionTypes.Permission)]
		Per_Update = 1L << 4,
		[Permission("Administrator", PermissionAttribute.PermissionTypes.Permission)]
		Per_Admin = 1L << 5,
		[Permission("Upload", PermissionAttribute.PermissionTypes.Permission)]
		Per_Upload = 1L << 6,
		//Permission8 = 1L << 7,
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
		//Application29 = 1L << 44,
		//Application30 = 1L << 45,
		//Application31 = 1L << 46,
		//Application32 = 1L << 47,
		//Application33 = 1L << 48,
		//Application34 = 1L << 49,
		//Application35 = 1L << 50,
		[Permission("Developer", PermissionAttribute.PermissionTypes.Application)]
		App_Developer = 1L << 51,
		[Permission("Recorder", PermissionAttribute.PermissionTypes.Application)]
		App_Recorder = 1L << 52,
		[Permission("Virtual Tag Server", PermissionAttribute.PermissionTypes.Application)]
		App_VTS = 1L << 53,
		[Permission("Reporting Server", PermissionAttribute.PermissionTypes.Application)]
		App_ReportingServer = 1L << 54,
		[Permission("Notification Server", PermissionAttribute.PermissionTypes.Application)]
		App_NotificationServer = 1L << 55,
		[Permission("Virtual Tag Server Configuration", PermissionAttribute.PermissionTypes.Application)]
		App_VirtualTagServerConfig = 1L << 56,
		[Permission("Web Analytics", PermissionAttribute.PermissionTypes.Application)]
		App_WebAnalytics = 1L << 57,
		
		//Special1 = 1L << 58,
		//Special2 = 1L << 59,
		//Special3 = 1L << 60,
		//Special4 = 1L << 61,
		[Permission("System Administrator", PermissionAttribute.PermissionTypes.Special)]
		Spec_SystemAdmin = 1L << 62,
		[Permission("User is persistent", PermissionAttribute.PermissionTypes.Special)]
		Spec_Persistent = 1L << 63
	}

	public static class AuthorizationDefaults
	{
		#region Public Properties
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
		#endregion

		#region Public Methods

		public static string Description(this Authorizations value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			PermissionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(PermissionAttribute)) as PermissionAttribute;
			return attribute == null ? value.ToString() : attribute.Description;
		}

		public static SNC.Authorization.Management.PermissionAttribute.PermissionTypes PermissionType(this Authorizations value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			PermissionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(PermissionAttribute)) as PermissionAttribute;
			return attribute == null ? SNC.Authorization.Management.PermissionAttribute.PermissionTypes.Unknown : attribute.PermissionType;
		}

		#endregion
	}
}
