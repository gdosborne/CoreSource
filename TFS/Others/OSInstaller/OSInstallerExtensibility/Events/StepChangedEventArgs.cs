namespace OSInstallerExtensibility.Events
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Linq;

	public delegate void StepChangedHandler(object sender, StepChangedEventArgs e);

	public class StepChangedEventArgs : EventArgs
	{
		#region Public Constructors
		public StepChangedEventArgs(IInstallerWizardStep step)
		{
			Step = step;
		}
		#endregion Public Constructors

		#region Public Properties
		public IInstallerWizardStep Step { get; private set; }
		#endregion Public Properties
	}
}
