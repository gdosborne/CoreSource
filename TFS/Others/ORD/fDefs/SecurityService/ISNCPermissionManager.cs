// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 
//		06/21/2016		Greg	Original
//		07/19/2016		Greg	Added ItemSSIDAttributeName property
// -----------------------------------------------------------------------
//
// ISNCPermissionManager.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Net;
	using System.Threading.Tasks;
	using System.Xml.Linq;

	public interface ISNCPermissionManager : INotifyPropertyChanged
	{
		#region Public Methods
		bool AddItem(PermissionItem item);
		User Authenticate(NetworkCredential credentials);
		bool Authenticate(NetworkCredential credentials, Authorizations requiredAuths);
		void ChangePassword(string userName, string oldHashedPassword, string newHashedPassword);
		bool DeleteItem(PermissionItem item);
		int GetPasswordStrength(string password);
		XElement GetPermissionXElement();
		string HashString(string value);
		void Load(XElement dataElement);
		void LoadFile(string fileName);
		Task LoadFileAsync(string fileName);
		void Parse(string xmlData);
		void ResetPassword(string itemName);
		void SaveFile(string fileName);
		void SaveFile(string fileName, string contents);
		void SaveFile(string fileName, string contents, bool isWebFile);
		Task SaveFileAsync(string fileName);
		Task SaveFileAsync(string fileName, string contents);
		Task SaveFileAsync(string fileName, string contents, bool isServiceFile);
		#endregion Public Methods

		#region Public Events
		new event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Public Properties
		IDictionary<string, string> CustomNames { get; set; }
		string GroupName { get; }
		bool HasChanges { get; set; }
		string ItemFirstNameAttributeName { get; }
		string ItemLastNameAttributeName { get; }
		string ItemName { get; }
		string ItemNameAttributeName { get; }
		string ItemPasswordAttributeName { get; }
		string ItemSSIDAttributeName { get; }
		string ItemTypeAttributeName { get; }
		Exception LastException { get; }
		IDictionary<string, string> Names { get; set; }
		string OriginalXmlData { get; }
		string OtherDataAttributeName { get; }
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
		#endregion Public Properties
	}
}