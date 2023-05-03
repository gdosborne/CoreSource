namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IInstallerSettingsController
	{
		#region Public Methods
		void Reset();
		#endregion Public Methods

		#region Public Events
		event EventHandler SettingsChanged;
		#endregion Public Events
	}
}
