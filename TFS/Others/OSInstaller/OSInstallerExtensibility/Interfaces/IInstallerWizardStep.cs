namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IInstallerWizardStep
	{
		#region Public Methods
		void Initialize();
		#endregion Public Methods

		#region Public Properties
		string Id { get; set; }
		bool IsInstallationStep { get; set; }
		IInstallerManager Manager { get; set; }
		int Sequence { get; set; }
		#endregion Public Properties
	}
}
