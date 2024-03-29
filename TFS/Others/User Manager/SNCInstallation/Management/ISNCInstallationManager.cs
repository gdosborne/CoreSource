// ***********************************************************************
// Assembly         : SNCInstallation
// Author           : Greg
// Created          : 07-02-2015
//
// Last Modified By : Greg
// Last Modified On : 07-02-2015
// ***********************************************************************
// <copyright file="ISNCInstallationManager.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace SNC.Installation.Management
{
	public interface ISNCInstallationManager
	{
		#region Public Properties
		Version CurrentVersion { get; set; }
		string InstallationFileName { get; set; }
		string InstallationFolder { get; }
		Version InstallVersion { get; }
		bool RequiresUpdate { get; }
		string TempPath { get; set; }
		#endregion

		#region Public Methods

		void BeginInstallation(bool forceClose);

		Version GetLatestInstallVersion();

		Version GetLatestInstallVersion(string installationFolder);

		bool InstallationFileExists(string installerFileName, Version version);

		#endregion
	}
}
