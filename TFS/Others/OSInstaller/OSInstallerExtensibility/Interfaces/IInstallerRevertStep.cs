namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IInstallerRevertStep
	{
		#region Public Methods
		void RevertInstallation();
		#endregion Public Methods
	}
}
