namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IInstallerView
	{
		#region Public Properties
		double ProgressMaximum { get; set; }
		string ProgressMessage { get; set; }
		double ProgressValue { get; set; }
		#endregion Public Properties
	}
}
