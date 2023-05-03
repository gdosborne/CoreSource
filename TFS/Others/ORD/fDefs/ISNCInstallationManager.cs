// ***********************************************************************
// Assembly         : fDefs
// Author           : Greg
// Created          : 07-16-2015
//
// Last Modified By : Greg
// Last Modified On : 07-16-2015
// ***********************************************************************
// <copyright file="ISNCInstallationManager.cs" company="Statistics and Controls, Inc.">
//     Copyright (c) Statistics and Controls, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace SNC.OptiRamp.Installation.Management
{
    /// <summary>
    /// Interface ISNCInstallationManager
    /// </summary>
    public interface ISNCInstallationManager
    {
        #region Public Properties
		/// <summary>
		/// Occurs when [update complete].
		/// </summary>
		event EventHandler UpdateComplete;
		/// <summary>
		/// Occurs when [preparing to close].
		/// </summary>
		event EventHandler PreparingToClose;
		/// <summary>
		/// Occurs when [download ended].
		/// </summary>
		event EventHandler DownloadEnded;
        /// <summary>
        /// Gets or sets the current version.
        /// </summary>
        /// <value>The current version.</value>
        Version CurrentVersion { get; set; }

        /// <summary>
        /// Gets the download progress.
        /// </summary>
        /// <value>The download progress.</value>
        long DownloadProgress { get; }

        /// <summary>
        /// Gets the size of the download.
        /// </summary>
        /// <value>The size of the download.</value>
        long DownloadSize { get; }

        /// <summary>
        /// Gets or sets the name of the installation file.
        /// </summary>
        /// <value>The name of the installation file.</value>
        string InstallationFileName { get; set; }

        /// <summary>
        /// Gets the installation folder.
        /// </summary>
        /// <value>The installation folder.</value>
        string InstallationFolder { get; }

        /// <summary>
        /// Gets the install version.
        /// </summary>
        /// <value>The install version.</value>
        Version InstallVersion { get; }

        /// <summary>
        /// Gets a value indicating whether the application requires update.
        /// </summary>
        /// <value><c>true</c> if the application requires update; otherwise, <c>false</c>.</value>
        bool RequiresUpdate { get; }

        /// <summary>
        /// Gets or sets the temporary path.
        /// </summary>
        /// <value>The temporary path.</value>
        string TempPath { get; set; }

        /// <summary>
        /// Occurs when [download progress changed].
        /// </summary>
        event EventHandler DownloadProgressChanged;

		/// <summary>
		/// Gets all versions.
		/// </summary>
		/// <value>All versions.</value>
		Version[] AllVersions { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Begins the installation.
        /// </summary>
        /// <param name="forceClose">if set to <c>true</c> [force close].</param>
        void BeginInstallation(bool forceClose);

        /// <summary>
        /// Gets the latest install version.
        /// </summary>
        /// <returns>Version.</returns>
        Version GetLatestInstallVersion();

        /// <summary>
        /// Gets the latest install version.
        /// </summary>
        /// <param name="installationFolder">The installation folder.</param>
        /// <returns>Version.</returns>
        Version GetLatestInstallVersion(string installationFolder);

        /// <summary>
        /// Installations the file exists.
        /// </summary>
        /// <param name="installerFileName">Name of the installer file.</param>
        /// <param name="version">The version.</param>
        /// <returns><c>true</c> if the installation file exists, <c>false</c> otherwise.</returns>
        bool InstallationFileExists(string installerFileName, Version version);

        #endregion Public Methods
    }
}