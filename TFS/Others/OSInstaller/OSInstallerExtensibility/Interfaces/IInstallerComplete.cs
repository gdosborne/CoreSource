namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IInstallerComplete
	{
		#region Public Methods
		void ModifyCompleteMessage(string message);
		#endregion Public Methods
	}
}
