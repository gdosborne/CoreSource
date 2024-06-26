namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public interface IInstallerExtension
	{
		#region Public Properties
		Assembly Assembly { get; }
		string Path { get; }
		IList<IInstallerWizardStep> Steps { get; }
		#endregion Public Properties
	}
}
