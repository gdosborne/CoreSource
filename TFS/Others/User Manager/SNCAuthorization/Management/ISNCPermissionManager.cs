// ***********************************************************************
// Assembly         : SNCAuthorization
// Author           : Greg
// Created          : 06-19-2015
//
// Last Modified By : Greg
// Last Modified On : 06-26-2015
// ***********************************************************************
// <copyright file="ISNCPermissionManager.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using SNC.Authorization.Entities;

namespace SNC.Authorization.Management
{
	public interface ISNCPermissionManager : INotifyPropertyChanged
	{
		#region Public Events
		new event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		IDictionary<string, string> CustomNames { get; set; }
		string GroupName { get; }
		string ItemFirstNameAttributeName { get; }
		string ItemLastNameAttributeName { get; }
		string ItemName { get; }
		string ItemNameAttributeName { get; }
		string ItemPasswordAttributeName { get; }
		string ItemTypeAttributeName { get; }
		Exception LastException { get; }
		string OriginalXmlData { get; }
		IList<PermissionItem> Permissions { get; }
		string ReferenceName { get; }
		string ReferenceNameAttributeName { get; }
		string ReferencesName { get; }
		string ReferenceSubTypeAttributeName { get; }
		string ReferenceTypeAttributeName { get; }
		bool RethrowException { get; set; }
		string RolePermissionName { get; }
		string RolePermissionsName { get; }
		bool RolePriority { get; set; }
		string RoleValueAttributeName { get; }
		string RootName { get; }
		string UserPermissionName { get; }
		string UserPermissionsName { get; }
		string UserValueAttributeName { get; }
		#endregion

		#region Public Methods

		bool AddItem(PermissionItem item);

		bool Authenticate(NetworkCredential credentials);

		bool Authenticate(NetworkCredential credentials, Authorizations requiredAuths);

		bool DeleteItem(PermissionItem item);

		XElement GetPermissionXElement();

		string HashString(string value);

		void Load(XElement dataElement);

		Task LoadFileAsync(string fileName);

		void Parse(string xmlData);

		Task SaveFileAsync(string fileName, string contents);

		Task SaveFileAsync(string fileName, string contents, bool isWebFile);

		#endregion
	}
}
