// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 06-26-2015
// ***********************************************************************
// <copyright file="PermissionManager.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PCLCrypto;
using SNC.Authorization.Entities;

namespace SNC.Authorization.Management
{
	public abstract class PermissionManager : ISNCPermissionManager
	{
		#region Public Methods

		public abstract Task SaveFileAsync(string fileName, string contents);

		#endregion

		#region Private Fields
		private Exception _LastException;
		private List<PermissionItem> _Permissions = null;
		private bool _RethrowException;
		private bool _RolePriority;
		#endregion

		#region Public Constructors

		public PermissionManager()
		{
			_Permissions = new List<PermissionItem>();
			CustomNames = new Dictionary<string, string>
			{
				{ "Root", "permission" },
				{ "Group", "permissions" },
				{ "Item", "permissionitem" },
				{ "ItemName", "name" },
				{ "ItemType", "type" },
				{ "ItemFirstName", "firstname" },
				{ "ItemLastName", "lastname" },
				{ "ItemPassword", "password" },
				{ "UserPermissions", "user_permissions" },
				{ "UserPermission", "itempermission" },
				{ "RolePermissions", "role_permissions" },
				{ "RolePermission", "itempermission" },
				{ "References", "references" },
				{ "Reference", "reference" },
				{ "ReferenceName", "name" },
				{ "ReferenceType", "type" },
				{ "ReferenceSubType", "subtype" },
				{ "PermissionValue", "value" }
			};
		}

		public PermissionManager(string xmlData)
			: this()
		{
			try
			{
				Parse(xmlData);
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
		}

		public PermissionManager(XElement dataElement)
			: this()
		{
			try
			{
				Load(dataElement);
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
		}

		#endregion

		#region Public Events
		public event ItemAddedHandler ItemAdded;

		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public IDictionary<string, string> CustomNames { get; set; }

		public string GroupName { get { return CustomNames["Group"]; } }

		public string ItemFirstNameAttributeName { get { return CustomNames["ItemFirstName"]; } }

		public string ItemLastNameAttributeName { get { return CustomNames["ItemLastName"]; } }

		public string ItemName { get { return CustomNames["Item"]; } }

		public string ItemNameAttributeName { get { return CustomNames["ItemName"]; } }

		public string ItemPasswordAttributeName { get { return CustomNames["ItemPassword"]; } }

		public string ItemTypeAttributeName { get { return CustomNames["ItemType"]; } }

		public Exception LastException
		{
			get { return _LastException; }
			set
			{
				_LastException = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastException"));
			}
		}

		public string OriginalXmlData { get; private set; }

		public IList<PermissionItem> Permissions { get { return _Permissions; } }

		public string ReferenceName { get { return CustomNames["Reference"]; } }

		public string ReferenceNameAttributeName { get { return CustomNames["ReferenceName"]; } }

		public string ReferencesName { get { return CustomNames["References"]; } }

		public string ReferenceSubTypeAttributeName { get { return CustomNames["ReferenceSubType"]; } }

		public string ReferenceTypeAttributeName { get { return CustomNames["ReferenceType"]; } }

		public bool RethrowException
		{
			get { return _RethrowException; }
			set
			{
				_RethrowException = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RethrowException"));
			}
		}

		public string RolePermissionName { get { return CustomNames["RolePermission"]; } }

		public string RolePermissionsName { get { return CustomNames["RolePermissions"]; } }

		public bool RolePriority
		{
			get { return _RolePriority; }
			set
			{
				_RolePriority = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RolePriority"));
			}
		}

		public string RoleValueAttributeName { get { return CustomNames["PermissionValue"]; } }

		public string RootName { get { return CustomNames["Root"]; } }

		public string UserPermissionName { get { return CustomNames["UserPermission"]; } }

		public string UserPermissionsName { get { return CustomNames["UserPermissions"]; } }

		public string UserValueAttributeName { get { return CustomNames["PermissionValue"]; } }
		#endregion

		public bool AddItem(PermissionItem item)
		{
			var count = 0;
			var baseName = item.Name;
			while (Permissions.Any(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
			{
				count++;
				item.Name = string.Format("{0}_{1}", baseName, count);
			}
			Permissions.Add(item);
			if (ItemAdded != null)
				ItemAdded(this, new ItemAddedEventArgs(item.Name));
			return true;
		}

		public bool Authenticate(NetworkCredential credentials)
		{
			try
			{
				PermissionItem authItem = null;
				authItem = Permissions.FirstOrDefault(x => x.Name.Equals(credentials.UserName, StringComparison.OrdinalIgnoreCase));
				if (authItem == null)
					return false;
				if ((authItem as User).Password.Equals(credentials.Password, StringComparison.Ordinal))
					return true;
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
			return false;
		}

		public bool Authenticate(NetworkCredential credentials, Authorizations requiredAuths)
		{
			if (!Authenticate(credentials))
				return false;

			try
			{
				var authItem = Permissions.FirstOrDefault(x => x.Name.Equals(credentials.UserName, StringComparison.OrdinalIgnoreCase));
				var auths = authItem.GetAuthorizationForItem(this, true);
				if ((auths & requiredAuths) == requiredAuths)
					return true;
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
			return false;
		}

		public bool DeleteItem(PermissionItem item)
		{
			var result = false;
			try
			{
				var roles = Permissions.Where(x => x is Role).Cast<Role>().ToList();
				foreach (var role in roles)
				{
					var refnc = role.References.FirstOrDefault(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
					if (refnc != null)
						role.References.Remove(refnc);
				}
				Permissions.Remove(item);
				result = true;
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
			return result;
		}

		public XElement GetPermissionXElement()
		{
			var result = new XElement(RootName);
			try
			{
				var permissionsElement = new XElement(GroupName);
				foreach (var perm in Permissions)
				{
					permissionsElement.Add(perm.ToXElement(this));
				}
				result.Add(permissionsElement);
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
			return result;
		}

		public string HashString(string value)
		{
			var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
			byte[] inputBytes = Encoding.UTF8.GetBytes(value);
			byte[] hash = hasher.HashData(inputBytes);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}

		public virtual void Load(XElement dataElement)
		{
			try
			{
				var metaElement = dataElement.Element("permission_metadata");
				if (metaElement != null)
				{
					foreach (var nameElem in metaElement.Elements())
					{
						if (CustomNames.ContainsKey(nameElem.Attribute("key").Value))
							CustomNames[nameElem.Attribute("key").Value] = nameElem.Attribute("value").Value;
					}
				}
				if (!dataElement.Name.LocalName.Equals(RootName))
					throw new Exception("Invalid permissions XElement");
				var permissionsElement = dataElement.Element(GroupName);
				foreach (var pElement in permissionsElement.Elements(ItemName))
				{
					if (pElement.Attribute(ItemTypeAttributeName) != null)
					{
						if (pElement.Attribute(ItemTypeAttributeName).Value.Equals("User"))
						{
							var val = User.FromXElement(pElement, this);
							if (val != null && (!_Permissions.Any(x => x.Name.Equals(val.Name)) || (_Permissions.Any(x => x.Name.Equals(val.Name)) && !RolePriority)))
								_Permissions.Add(val);
						}
						else if (pElement.Attribute(ItemTypeAttributeName).Value.Equals("Role"))
						{
							var val = Role.FromXElement(pElement, this);
							if (val != null && (!_Permissions.Any(x => x.Name.Equals(val.Name)) || (_Permissions.Any(x => x.Name.Equals(val.Name)) && RolePriority)))
								_Permissions.Add(val);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
		}

		public abstract Task LoadFileAsync(string fileName);

		public virtual void Parse(string xmlData)
		{
			OriginalXmlData = xmlData;
			try
			{
				Load(XElement.Parse(xmlData));
			}
			catch (Exception ex)
			{
				LastException = ex;
				if (RethrowException)
					throw;
			}
		}

		public abstract Task SaveFileAsync(string fileName, string contents, bool isWebFile);
	}
}
