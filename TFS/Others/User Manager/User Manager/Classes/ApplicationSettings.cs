// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-25-2015
//
// Last Modified By : Greg
// Last Modified On : 07-15-2015
// ***********************************************************************
// <copyright file="ApplicationSettings.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace User_Manager.Classes
{
	public static class ApplicationSettings
	{
		#region Public Fields
		public static readonly string ServiceName = "RemoteSecurityService";
		#endregion

		#region Public Methods

		public static List<string> AddRecentItem(string fileName)
		{
			var result = GetRecentItems();
			while (result.Contains(fileName))
			{
				result.Remove(fileName);
			}
			result.Insert(0, fileName);
			result = result.Take(10).ToList();
			ClearRecentItems();
			for (int i = 0; i < result.Count; i++)
			{
				SetValue<string>("Recent", i.ToString(), result[i]);
			}
			return result;
		}

		public static void ClearRecentItems()
		{
			for (int i = 0; i < 10; i++)
			{
				RemoveValue("Recent", i.ToString());
			}
		}

		public static Dictionary<string, string> GetCustomNames()
		{
			var result = new Dictionary<string, string>
			{
				{ "Root", GetValue<string>("CustomNames", "Root", "permission") },
				{ "Group", GetValue<string>("CustomNames", "Group","permissions") },
				{ "Item", GetValue<string>("CustomNames", "Item","permissionitem") },
				{ "ItemName", GetValue<string>("CustomNames", "ItemName","name") },
				{ "ItemType", GetValue<string>("CustomNames", "ItemType","type") },
				{ "ItemFirstName", GetValue<string>("CustomNames", "ItemFirstName","firstname") },
				{ "ItemLastName", GetValue<string>("CustomNames", "ItemLastName","lastname") },
				{ "ItemPassword", GetValue<string>("CustomNames", "ItemPassword","password") },
				{ "UserPermissions", GetValue<string>("CustomNames", "UserPermissions","user_permissions") },
				{ "UserPermission", GetValue<string>("CustomNames", "UserPermission","itempermission") },
				{ "RolePermissions", GetValue<string>("CustomNames", "RolePermissions","role_permissions") },
				{ "RolePermission", GetValue<string>("CustomNames", "RolePermission","itempermission") },
				{ "References", GetValue<string>("CustomNames", "References","references") },
				{ "Reference", GetValue<string>("CustomNames", "Reference","reference") },
				{ "ReferenceName", GetValue<string>("CustomNames", "ReferenceName","name") },
				{ "ReferenceType", GetValue<string>("CustomNames", "ReferenceType","type") },
				{ "ReferenceSubType", GetValue<string>("CustomNames", "ReferenceSubType","subtype") },
				{ "PermissionValue", GetValue<string>("CustomNames", "PermissionValue","value") }
			};
			return result;
		}

		public static Dictionary<string, string> GetDefaultCustomNames()
		{
			var man = new Manager();
			return (Dictionary<string, string>)man.CustomNames;
		}

		public static List<string> GetRecentItems()
		{
			var result = new List<string>();
			var itemNumber = 0;
			var item = GetValue<string>("Recent", itemNumber.ToString(), string.Empty);
			while (!string.IsNullOrEmpty(item))
			{
				result.Add(item);
				itemNumber++;
				item = GetValue<string>("Recent", itemNumber.ToString(), string.Empty);
			}
			result = result.Distinct().ToList();
			return result;
		}

		public static T GetValue<T>(string key, string name, T defaultValue) where T : IConvertible
		{
			var regKey = Registry.CurrentUser.OpenSubKey(@"Software\User Manager", true);
			if (regKey == null)
				return defaultValue;
			regKey = regKey.CreateSubKey(key);
			if (typeof(T) == typeof(string))
				return (T)regKey.GetValue(name, defaultValue.ToString());
			else if (typeof(T).IsEnum)
				return (T)Enum.Parse(typeof(T), (string)regKey.GetValue(name, defaultValue.ToString()), true);
			else
				return (T)Convert.ChangeType((string)regKey.GetValue(name, defaultValue.ToString()), typeof(T));
		}

		public static void RemoveValue(string key, string name)
		{
			var regKey = Registry.CurrentUser.OpenSubKey(@"Software\User Manager", true);
			if (!regKey.GetSubKeyNames().Contains(key))
				return;
			regKey = regKey.OpenSubKey(key, true);
			if (regKey.GetValueNames().Contains(name))
				regKey.DeleteValue(name);
		}

		public static void SetValue<T>(string key, string name, T value)
		{
			var regKey = Registry.CurrentUser.OpenSubKey(@"Software", true);
			regKey = regKey.CreateSubKey(@"User Manager");
			regKey = regKey.CreateSubKey(key);
			regKey.SetValue(name, value.ToString());
		}

		#endregion
	}
}
