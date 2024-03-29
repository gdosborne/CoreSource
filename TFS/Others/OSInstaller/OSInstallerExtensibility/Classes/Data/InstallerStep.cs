namespace OSInstallerExtensibility.Classes.Data
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class InstallerStep : IInstallerWizardStep
	{
		#region Public Methods
		public void Initialize()
		{
			throw new NotImplementedException();
		}
		#endregion Public Methods

		#region Public Properties
		public string Id { get; set; }
		public bool IsInstallationStep { get; set; }
		public IInstallerManager Manager { get; set; }
		public int Sequence { get; set; }
		#endregion Public Properties
	}
}
