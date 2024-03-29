// ***********************************************************************
// Assembly         : SNCInstallation
// Author           : Greg
// Created          : 07-02-2015
//
// Last Modified By : Greg
// Last Modified On : 07-02-2015
// ***********************************************************************
// <copyright file="InstallationManager.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SNC.Installation.Management
{
	public class InstallationManager : ISNCInstallationManager
	{
		#region Public Methods

		public Version GetLatestInstallVersion()
		{
			if (string.IsNullOrEmpty(InstallationFolder) || !Directory.Exists(InstallationFolder))
				return null;
			Version result = new Version();
			var baseDir = new DirectoryInfo(InstallationFolder);
			foreach (var dir in baseDir.GetDirectories())
			{
				var ver = new Version(dir.Name);
				if (ver > result)
					result = ver;
			}
			return result;
		}

		public Version GetLatestInstallVersion(string installationFolder)
		{
			InstallationFolder = installationFolder;
			return GetLatestInstallVersion();
		}

		public bool InstallationFileExists(string installerFileName, Version version)
		{
			bool result = false;
			try
			{
				InstallationFileName = Path.Combine(Path.Combine(InstallationFolder, version.ToString()), installerFileName);
				result = File.Exists(InstallationFileName);
			}
			catch { }
			return result;
		}

		#endregion

		#region Public Constructors

		public InstallationManager(string installationFolder, string tempPath, Version currentVersion, string installationFileName)
		{
			InstallationFolder = installationFolder;
			TempPath = tempPath;
			CurrentVersion = currentVersion;
			InstallationFileName = installationFileName;
		}

		#endregion

		#region Public Properties
		public Version CurrentVersion { get; set; }

		public string InstallationFileName { get; set; }

		public string InstallationFolder { get; private set; }

		public Version InstallVersion
		{
			get
			{
				if (string.IsNullOrEmpty(InstallationFolder) || string.IsNullOrEmpty(InstallationFileName))
					return null;
				return GetLatestInstallVersion();
			}
		}

		public bool RequiresUpdate
		{
			get
			{
				if (CurrentVersion == null || !InstallationFileExists(InstallationFileName, InstallVersion) || InstallVersion == null)
					return false;
				return InstallVersion > CurrentVersion;
			}
		}

		public string TempPath { get; set; }
		#endregion

		public void BeginInstallation(bool forceClose)
		{
			var tempPath = Path.Combine(TempPath, Path.GetFileName(InstallationFileName));
			File.Copy(Path.Combine(Path.Combine(InstallationFolder, InstallVersion.ToString()), InstallationFileName), tempPath, true);
			var proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = tempPath,
					WindowStyle = ProcessWindowStyle.Normal,
					WorkingDirectory = Path.GetDirectoryName(tempPath)
				}
			};
			proc.Start();
			if (forceClose)
				Environment.Exit(-1);
		}
	}
}
