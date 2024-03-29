namespace OSInstallerExtensibility.Events
{
	using OSInstallerCommands;
	using System;
	using System.Linq;

	public delegate void InstallCompleteHandler(object sender, InstallCompleteEventArgs e);

	public class InstallCompleteEventArgs : EventArgs
	{
		#region Public Constructors
		public InstallCompleteEventArgs(CommandStatuses status)
		{
			Status = status;
		}
		#endregion Public Constructors

		#region Public Properties
		public CommandStatuses Status { get; private set; }
		#endregion Public Properties
	}
}
