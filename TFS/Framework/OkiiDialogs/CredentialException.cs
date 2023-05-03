namespace GregOsborne.Dialogs {
	using System;
	using System.Security.Permissions;

	[Serializable()]
	public class CredentialException : System.ComponentModel.Win32Exception {
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException()
			: base(Ookii.Dialogs.Wpf.Properties.Resources.CredentialError) {
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(int error)
			: base(error) {
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(string message)
			: base(message) {
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(int error, string message)
			: base(error, message) {
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(string message, Exception innerException)
			: base(message, innerException) {
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected CredentialException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context) {
		}
	}
}
