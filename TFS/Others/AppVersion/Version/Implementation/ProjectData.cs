namespace VersionEngine.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ProjectData
	{
		#region Public Constructors
		public ProjectData(string projectFileName, string assemblyInfoPath, Version currentAssemblyVersion, Version currentFileVersion) {
			ProjectFileName = projectFileName;
			AssemblyInfoPath = assemblyInfoPath;
			CurrentAssemblyVersion = currentAssemblyVersion;
			CurrentFileVersion = currentFileVersion;
			InitializeDelegates();
		}
		#endregion Public Constructors

		#region Internal Delegates
		internal delegate int DateTimeHandler(DateTime v1);
		internal delegate int Handler();
		internal delegate int IntBoolNullDateTimeHandler(int v1, bool v2, DateTime? v3);
		internal delegate int IntHandler(int v1);
		internal delegate int IntNullDateTimeHandler(int v1, DateTime? v2);
		#endregion Internal Delegates

		#region Private Delegates
		private delegate void IgnoreHandler(int value);
		#endregion Private Delegates

		#region Public Methods
		public void ModifyVersion(VersionSchema assemblySchema, VersionSchema fileSchema, DateTime? lastUpdate) {
			int major = ProcessMethod(assemblySchema.Major, CurrentAssemblyVersion.Major, assemblySchema.MajorFixed, lastUpdate, VersionParts.Major);
			int minor = ProcessMethod(assemblySchema.Minor, CurrentAssemblyVersion.Minor, assemblySchema.MinorFixed, lastUpdate, VersionParts.Minor);
			int build = ProcessMethod(assemblySchema.Build, CurrentAssemblyVersion.Build, assemblySchema.BuildFixed, lastUpdate, VersionParts.Build);
			int revision = ProcessMethod(assemblySchema.Revision, CurrentAssemblyVersion.Revision, assemblySchema.RevisionFixed, lastUpdate, VersionParts.Revision);
			ModifiedAssemblyVersion = new Version(major, minor, build, revision);
			major = ProcessMethod(fileSchema.Major, CurrentFileVersion.Major, fileSchema.MajorFixed, lastUpdate, VersionParts.Major);
			minor = ProcessMethod(fileSchema.Minor, CurrentFileVersion.Minor, fileSchema.MinorFixed, lastUpdate, VersionParts.Minor);
			build = ProcessMethod(fileSchema.Build, CurrentFileVersion.Build, fileSchema.BuildFixed, lastUpdate, VersionParts.Build);
			revision = ProcessMethod(fileSchema.Revision, CurrentFileVersion.Revision, fileSchema.RevisionFixed, lastUpdate, VersionParts.Revision);
			ModifiedFileVersion = new Version(major, minor, build, revision);
		}
		#endregion Public Methods

		#region Private Methods
		private void InitializeDelegates() {
			_Delegates.Add(VersionMethods.Ignore, new IntHandler(UpdateMethods.Ignore));
			_Delegates.Add(VersionMethods.Fixed, new IntHandler(UpdateMethods.Fixed));
			_Delegates.Add(VersionMethods.Increment, new IntHandler(UpdateMethods.Increment));
			_Delegates.Add(VersionMethods.Day, new Handler(UpdateMethods.Day));
			_Delegates.Add(VersionMethods.Month, new Handler(UpdateMethods.Month));
			_Delegates.Add(VersionMethods.Year, new Handler(UpdateMethods.Year));
			_Delegates.Add(VersionMethods.Year2Digit, new Handler(UpdateMethods.Year2Digit));
			_Delegates.Add(VersionMethods.Second, new Handler(UpdateMethods.Second));
			_Delegates.Add(VersionMethods.IncrementResetEachDay, new IntNullDateTimeHandler(UpdateMethods.IncrementResetEachDay));
		}
		private int ProcessMethod(VersionMethods method, params object[] paramValues) {
			int result = 0;
			int current = (int)paramValues[0];
			bool hasParam = paramValues.Length > 0;
			int paramValue = (int)paramValues[1];
			DateTime? paramValue1 = (DateTime?)paramValues[2];
			VersionParts part = (VersionParts)paramValues[3];
			List<object> parameters = new List<object>();
			Delegate dgt = _Delegates[method];
			switch (method) {
				case VersionMethods.Ignore:
					parameters.Add(current);
					break;
				case VersionMethods.Increment:
					parameters.Add(current);
					break;
				case VersionMethods.IncrementResetEachDay:
					parameters.Add(current);
					parameters.Add(paramValue1);
					break;
				case VersionMethods.Day:
				case VersionMethods.Month:
				case VersionMethods.Year:
				case VersionMethods.Year2Digit:
				case VersionMethods.Second:
					break;
				case VersionMethods.Fixed:
					parameters.Add(hasParam ? paramValue : 0);
					break;
			}
			result = (int)dgt.DynamicInvoke(parameters.ToArray());
			if (part == VersionParts.Revision && method == VersionMethods.Increment)
				result = int.MaxValue;
			return result;
		}
		#endregion Private Methods

		#region Private Fields
		private Dictionary<VersionMethods, Delegate> _Delegates = new Dictionary<VersionMethods, Delegate>();
		#endregion Private Fields

		#region Public Properties
		public string AssemblyInfoPath { get; private set; }
		public Version CurrentAssemblyVersion { get; private set; }
		public Version CurrentFileVersion { get; private set; }
		public Version ModifiedAssemblyVersion { get; set; }
		public Version ModifiedFileVersion { get; set; }
		public string ProjectFileName { get; private set; }
		#endregion Public Properties
	}
}
