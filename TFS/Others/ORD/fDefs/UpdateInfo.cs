namespace fDefs {
	using System;

	/// <summary>
	/// Class UpdateInfo.
	/// </summary>
	public class UpdateInfo {
		#region Public Properties
		/// <summary>
		/// Gets or sets the application.
		/// </summary>
		/// <value>The application.</value>
		public string Application {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the download URL.
		/// </summary>
		/// <value>The download URL.</value>
		public string DownloadUrl {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the error code.
		/// </summary>
		/// <value>The error code.</value>
		public string ErrorCode {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the error text.
		/// </summary>
		/// <value>The error text.</value>
		public string ErrorText {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public long Size {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the stream identifier.
		/// </summary>
		/// <value>The stream identifier.</value>
		public string StreamId {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether [update is available].
		/// </summary>
		/// <value><c>true</c> if [update is available]; otherwise, <c>false</c>.</value>
		public bool UpdateIsAvailable {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		public Version Version {
			get;
			set;
		}
		#endregion Public Properties
	}

}
