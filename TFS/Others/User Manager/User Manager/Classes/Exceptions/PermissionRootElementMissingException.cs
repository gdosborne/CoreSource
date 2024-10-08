// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 07-15-2015
//
// Last Modified By : Greg
// Last Modified On : 07-15-2015
// ***********************************************************************
// <copyright file="PermissionRootElementMissingException.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace User_Manager.Classes.Exceptions
{
	public class PermissionRootElementMissingException : Exception
	{
		#region Public Constructors

		public PermissionRootElementMissingException()
			: base() { }

		public PermissionRootElementMissingException(string message)
			: base(message) { }

		public PermissionRootElementMissingException(string message, Exception innerException)
			: base(message, innerException) { }

		#endregion
	}
}
