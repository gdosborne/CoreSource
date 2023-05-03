using System;
using System.ServiceModel;
namespace fDefs {
	[ServiceContract]
	public interface IUpdateService {
		#region Public Methods
		/// <summary>
		/// Closes the stream.
		/// </summary>
		/// <param name="streamId">The stream identifier.</param>
		[OperationContract]
		void CloseStream(string streamId);
		/// <summary>
		/// Downloads the version.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="version">The version.</param>
		/// <param name="installationFileName">Name of the installation file.</param>
		/// <returns>UpdateInfo.</returns>
		[OperationContract]
		UpdateInfo DownloadVersion(string application, Version version, string installationFileName);
		/// <summary>
		/// Gets all versions.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns>Version[].</returns>
		[OperationContract]
		Version[] GetAllVersions(string application);
		/// <summary>
		/// Gets the batch.
		/// </summary>
		/// <param name="streamId">The stream identifier.</param>
		/// <param name="isLastBatch">if set to <c>true</c> [is last batch].</param>
		/// <returns>System.Byte[].</returns>
		[OperationContract]
		byte[] GetBatch(string streamId, out bool isLastBatch);

		/// <summary>
		/// Gets the update.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="currentVersion">The current version.</param>
		/// <param name="installationFileName">Name of the installation file.</param>
		/// <returns>UpdateInfo.</returns>
		[OperationContract]
		UpdateInfo GetUpdate(string application, Version currentVersion, string installationFileName);

		/// <summary>
		/// Installations the file exists.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="currentVersion">The current version.</param>
		/// <param name="installationFileName">Name of the installation file.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		[OperationContract]
		bool InstallationFileExists(string application, Version currentVersion, string installationFileName);
		/// <summary>
		/// Queries the update.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <param name="currentVersion">The current version.</param>
		/// <param name="installationFileName">Name of the installation file.</param>
		/// <returns>UpdateInfo.</returns>
		[OperationContract]
		UpdateInfo QueryUpdate(string application, Version currentVersion, string installationFileName);
		#endregion Public Methods
	}
}
