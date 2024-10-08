namespace VersionEngine
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public enum VersionParts
	{
		Major,
		Minor,
		Build,
		Revision
	}
	public enum ProjectTypes
	{
		VBProject,
		CSProject,
		CPPProject
	}
	public enum VersionMethods
	{
		Fixed,
		Increment,
		IncrementResetEachDay,
		Year,
		Year2Digit,
		Month,
		Day,
		Second,
		Ignore
	}

	public interface IVersionEngine
	{
		#region Public Methods
		void UpdateVersion(VersionSchema assemblySchema, VersionSchema fileSchema, Version assemblyVersion, Version fileVersion, DateTime? lastUpdate);
		#endregion Public Methods

		#region Public Properties
		ProjectTypes ProjectType { get; }
		#endregion Public Properties
	}
}
