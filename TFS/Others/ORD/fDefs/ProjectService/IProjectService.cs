namespace fDefs.ProjectService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.ServiceModel;

	/// <summary>
	/// Interface IProjectService
	/// </summary>
	[ServiceContract]
	public interface IProjectService
	{
		#region Public Methods
		/// <summary>
		/// Deletes the project.
		/// </summary>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="error">The error.</param>
		/// <returns>System.String.</returns>
		[OperationContract]
		string DeleteProject(string projectName, Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Deletes the project backup.
		/// </summary>
		/// <param name="backupName">Name of the backup.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="Error">The error.</param>
		[OperationContract]
		void DeleteProjectBackup(string backupName, Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Gets all backup projects.
		/// </summary>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="Error">The error.</param>
		/// <returns>ProjectData[].</returns>
		[OperationContract]
		ProjectData[] GetAllBackupProjects(Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Gets all project names.
		/// </summary>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="error">The error.</param>
		/// <returns>System.String[].</returns>
		[OperationContract]
		string[] GetAllProjectNames(Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Gets all projects.
		/// </summary>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="includeBackups">if set to <c>true</c> [include backups].</param>
		/// <param name="Error">The error.</param>
		/// <returns>ProjectData[].</returns>
		[OperationContract]
		ProjectData[] GetAllProjects(Enumerations.ProjectTypes projectType, bool includeBackups, out string Error);
		/// <summary>
		/// Gets the project.
		/// </summary>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="error">The error.</param>
		/// <returns>System.Byte[].</returns>
		[OperationContract]
		byte[] GetProject(string projectName, Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Determines whether [is generation complete] [the specified generation identifier].
		/// </summary>
		/// <param name="generationId">The generation identifier.</param>
		/// <param name="TotalTime">The total time.</param>
		/// <param name="Completed">The completed.</param>
		/// <param name="error">The error.</param>
		/// <returns>Statuses.</returns>
		[OperationContract]
		Enumerations.Statuses IsGenerationComplete(string generationId, out TimeSpan TotalTime, out DateTime? Completed, out string Error);
		/// <summary>
		/// Gets the number of backup days.
		/// </summary>
		/// <returns>System.Int32.</returns>
		[OperationContract]
		int NumberOfBackupDays();
		/// <summary>
		/// Gets the regenerate all status.
		/// </summary>
		/// <param name="ids">The ids.</param>
		/// <param name="Error">The error.</param>
		/// <returns>Enumerations.Statuses[].</returns>
		[OperationContract]
		Enumerations.Statuses[] GetRegenerateAllStatus(string[] ids, out string Error);
		/// <summary>
		/// Regenerates all.
		/// </summary>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="Error">The error.</param>
		/// <returns>System.String[].</returns>
		[OperationContract]
		string[] RegenerateAll(Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Restores the project.
		/// </summary>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="backupName">Name of the backup.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="Error">The error.</param>
		[OperationContract]
		void RestoreProject(string projectName, string backupName, Enumerations.ProjectTypes projectType, out string Error);
		/// <summary>
		/// Sets the file sequence.
		/// </summary>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="files">The files.</param>
		/// <param name="Error">The error.</param>
		[OperationContract]
		void SetFileSequence(Enumerations.ProjectTypes projectType, string[] files, out string Error);
		/// <summary>
		/// Sets the project.
		/// </summary>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="overWrite">if set to <c>true</c> [over write].</param>
		/// <param name="data">The data.</param>
		/// <param name="error">The error.</param>
		/// <returns>System.String.</returns>
		[OperationContract]
		string SetProject(string projectName, Enumerations.ProjectTypes projectType, bool overWrite, byte[] data, out string Error);
		/// <summary>
		/// Sets the project block.
		/// </summary>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="projectType">Type of the project.</param>
		/// <param name="overWrite">if set to <c>true</c> [over write].</param>
		/// <param name="data">The data.</param>
		/// <param name="isFirstBlock">if set to <c>true</c> [is first block].</param>
		/// <param name="isLastBlock">if set to <c>true</c> [is last block].</param>
		/// <param name="Error">The error.</param>
		/// <returns>System.String.</returns>
		[OperationContract]
		string SetProjectBlock(string projectName, string id, Enumerations.ProjectTypes projectType, bool overWrite, byte[] data, bool isFirstBlock, bool isLastBlock, out string Error);
		#endregion Public Methods
	}
}