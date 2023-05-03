namespace OSInstallerExtensibility.Classes.Data
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class InstallerExtension : IInstallerExtension
	{
		#region Public Constructors
		public InstallerExtension(string path, Assembly assy)
		{
			Path = path;
			Assembly = assy;
			Steps = new List<IInstallerWizardStep>();
			var types = Assembly.GetTypes().ToList();
			types.ForEach(y =>
			{
				if (y.GetInterfaces().Contains(typeof(IInstallerWizardStep)))
				{
					var obj = (IInstallerWizardStep)Activator.CreateInstance(y);
					Steps.Add(new InstallerStep { Id = y.Name, IsInstallationStep = obj.IsInstallationStep });
				}
			});
		}
		#endregion Public Constructors

		#region Public Properties
		public Assembly Assembly { get; private set; }
		public string Path { get; private set; }
		public IList<IInstallerWizardStep> Steps { get; private set; }
		#endregion Public Properties
	}
}
