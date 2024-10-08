namespace VersionEngine.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Engine : IVersionEngine
	{
		#region Public Constructors
		public Engine(ProjectTypes projectType, string projectFileName, string assemblyInfoFileName) {
			ProjectType = projectType;
			ProjectFileName = projectFileName;
			AssemblyInfoFileName = assemblyInfoFileName;
		}
		#endregion Public Constructors

		#region Public Methods
		public void UpdateVersion(VersionSchema assemblySchema, VersionSchema fileSchema, Version assemblyVersion, Version fileVersion, DateTime? lastUpdate) {
			var helper = new AssemblyInfoHelper(AssemblyInfoFileName);
			var pd = new ProjectData(ProjectFileName, AssemblyInfoFileName, assemblyVersion, fileVersion);
			pd.ModifyVersion(assemblySchema, fileSchema, lastUpdate);
			helper.SetAssemblyVersion(AssemblyInfoFileName, pd, ProjectType);
			helper.SetFileVersion(AssemblyInfoFileName, pd, ProjectType);
		}
		#endregion Public Methods

		#region Public Properties
		public string AssemblyInfoFileName { get; private set; }
		public string ProjectFileName { get; private set; }
		public ProjectTypes ProjectType { get; private set; }
		#endregion Public Properties
	}
}
