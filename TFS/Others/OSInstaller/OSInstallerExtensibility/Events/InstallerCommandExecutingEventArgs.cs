namespace OSInstallerExtensibility.Events
{
	using OSInstallerCommands;
	using System;
	using System.Linq;

	public delegate void InstallerCommandExecutingHandler(object sender, InstallerCommandExecutingEventArgs e);

	public class InstallerCommandExecutingEventArgs : EventArgs
	{
		#region Public Constructors
		public InstallerCommandExecutingEventArgs(BaseCommand command, string message, double primaryProgressBarMaximum, double primaryProgressBarValue)
		{
			Command = command;
			Message = message;
			PrimaryProgressBarMaximum = primaryProgressBarMaximum;
			PrimaryProgressBarValue = primaryProgressBarValue;
		}
		#endregion Public Constructors

		#region Public Properties
		public BaseCommand Command { get; private set; }
		public string Message { get; private set; }
		public double PrimaryProgressBarMaximum { get; private set; }
		public double PrimaryProgressBarValue { get; private set; }
		#endregion Public Properties
	}
}
