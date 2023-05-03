// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:37:53
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// ISecurityService.cs
//
namespace SNC.OptiRamp.Security
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;

	/// <summary>
	/// Interface ISecurityService
	/// </summary>
	[ServiceContract]
	public interface ISecurityService
	{
		#region Public Methods
		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="Error">The error.</param>
		/// <returns>PermissionItem.</returns>
		[OperationContract]
		PermissionItem GetUser(string userName, out string Error);
		/// <summary>
		/// Adds the permission item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="error">The error.</param>
		[OperationContract]
		void AddPermissionItem(PermissionItem item, out string Eerror);

		/// <summary>
		/// Changes the password.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="oldHashedPassword">The old hashed password.</param>
		/// <param name="newHashedPassword">The new hashed password.</param>
		/// <param name="error">The error.</param>
		[OperationContract]
		void ChangePassword(string userName, string oldHashedPassword, string newHashedPassword, out string Error);

		/// <summary>
		/// Deletes the permission item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="error">The error.</param>
		[OperationContract]
		void DeletePermissionItem(PermissionItem item, out string Error);

		/// <summary>
		/// Gets all permission items.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>IList&lt;PermissionItem&gt;.</returns>
		[OperationContract]
		IList<PermissionItem> GetAllPermissionItems(out string Error);

		/// <summary>
		/// Gets the full authorization.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="error">The error.</param>
		/// <returns>Authorizations.</returns>
		[OperationContract]
		Authorizations GetFullAuthorization(PermissionItem item, out string Error);

		/// <summary>
		/// Gets the security data.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>System.String.</returns>
		[OperationContract]
		string GetSecurityData(out string Error);

		/// <summary>
		/// Determines whether the specified user name is authenticated.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="hashedPassword">The hashed password.</param>
		/// <param name="useSpecificPermissions">if set to <c>true</c> [use specific permissions].</param>
		/// <param name="permission">The permission.</param>
		/// <param name="error">The error.</param>
		/// <returns><c>true</c> if the specified user name is authenticated; otherwise, <c>false</c>.</returns>
		[OperationContract]
		bool IsAuthenticated(string userName, string hashedPassword, bool useSpecificPermissions, Authorizations permission, out string Error);

		/// <summary>
		/// Tests the service.
		/// </summary>
		/// <returns>System.String.</returns>
		[OperationContract]
		string TestService();
		/// <summary>
		/// Updates the permission item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="error">The error.</param>
		[OperationContract]
		void UpdatePermissionItem(PermissionItem item, out string Error);

		/// <summary>
		/// Updates the security data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="nameChanges">The name changes.</param>
		/// <param name="error">The error.</param>
		[OperationContract]
		void UpdateSecurityData(string data, Dictionary<string, string> nameChanges, out string Error);

		/// <summary>
		/// Users the login.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="password">The password.</param>
		/// <param name="Error">The error.</param>
		/// <returns>System.String.</returns>
		[OperationContract]
		string UserLogin(string userName, string password, out string Error);
		#endregion Public Methods
	}
}
