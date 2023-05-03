namespace VersionMaster {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using static VersionMaster.Enumerations;

	public class ProjectData : INotifyPropertyChanged, ICloneable {
		private readonly Dictionary<TransformTypes, Delegate> delegates = new Dictionary<TransformTypes, Delegate>();

		public ProjectData(string filename, string name)
			: this() {
			FullPath = filename;
			Name = name;
			AssemblyInfoPath = FindAssemblyInfoFile(ProjectDirectory);
			SchemaName = "default";
			CurrentAssemblyVersion = GetAssemblyVersion(AssemblyInfoPath);
			LastBuildDate = null;
		}

		public ProjectData() {
			InitializeDelegates();
			AllTransformTypes = new ObservableCollection<TransformTypes>();
			Schemas = new ObservableCollection<SchemaItem>();
			var ttNames = Enum.GetNames(typeof(TransformTypes));
			ttNames.ToList().ForEach(x => AllTransformTypes.Add((TransformTypes)Enum.Parse(typeof(TransformTypes), x, true)));
		}

		public void UpdateLastBuildDate() {
			LastBuildDate = DateTime.Now.Date;
			SchemaElement.Attribute(SchemaItem.lastBuildDateAttribValue).Value = DateTime.Now.ToString(SchemaItem.dateFormatValue);
		}

		public override string ToString() => Name;

		public XElement ToXElement() {
			var result = new XElement(SchemaItem.projectElementName, new XAttribute(SchemaItem.nameAttribValue, Name));
			result.Add(new XElement(SchemaItem.pathElementName, Path.Combine(ProjectDirectory, ProjectFileName)));
			var schema = new XElement(SchemaItem.schemaElementName,
				new XAttribute(SchemaItem.nameAttribValue, SchemaName),
				new XAttribute(SchemaItem.lastBuildDateAttribValue, LastBuildDate.HasValue ? LastBuildDate.Value.ToString(SchemaItem.dateFormatValue) : DateTime.MinValue.ToString(SchemaItem.dateFormatValue)));
			var paramtrs = new XElement(SchemaItem.paramsElementName,
				new XElement(SchemaItem.majorValue, MajorParameter),
				new XElement(SchemaItem.minorValue, MinorParameter),
				new XElement(SchemaItem.buildValue, BuildParameter),
				new XElement(SchemaItem.revisionValue, RevisionParameter));
			schema.Add(paramtrs);
			result.Add(schema);
			return result;
		}

		public static Version GetAssemblyVersion(string assyInfoFilename) {
			var result = new Version(1, 0, 0, 0);
			if (!string.IsNullOrEmpty(assyInfoFilename)) {
				using (var fs = new FileStream(assyInfoFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var sr = new StreamReader(fs)) {
					while (sr.Peek() > -1) {
						var line = sr.ReadLine();
						if (line.StartsWith("[assembly: AssemblyVersion")) {
							line = line
								.Replace("[assembly: AssemblyVersion", string.Empty)
								.Replace("(", string.Empty)
								.Replace(")", string.Empty)
								.Replace("\"", string.Empty)
								.Replace("]", string.Empty);
							if (Version.TryParse(line, out var ver)) {
								result = ver;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		public static ProjectData FromXElement(XElement element, List<SchemaItem> schemas) {
			var result = new ProjectData {
				Name = element.Attribute(SchemaItem.nameAttribValue).Value
			};
			var fName = element.Element(SchemaItem.pathElementName).Value;
			result.FullPath = fName;
			result.AssemblyInfoPath = FindAssemblyInfoFile(result.ProjectDirectory);
			result.CurrentAssemblyVersion = GetAssemblyVersion(result.AssemblyInfoPath);
			result.SchemaElement = element.Element(SchemaItem.schemaElementName);
			result.SchemaName = result.SchemaElement.Attribute(SchemaItem.nameAttribValue).Value;
			if (DateTime.TryParse(result.SchemaElement.Attribute(SchemaItem.lastBuildDateAttribValue).Value, out var last)) {
				result.LastBuildDate = last;
			}

			result.Schemas = new ObservableCollection<SchemaItem>(schemas);
			result.SelectedSchema = schemas.FirstOrDefault(x => x.Name == result.SchemaName);
			var paramsElement = result.SchemaElement.Element(SchemaItem.paramsElementName);
			result.MajorType = result.SelectedSchema.MajorPart;
			result.MajorParameter = paramsElement.Element(SchemaItem.majorValue).Value;
			result.MinorType = result.SelectedSchema.MinorPart;
			result.MinorParameter = paramsElement.Element(SchemaItem.minorValue).Value;
			result.BuildType = result.SelectedSchema.BuildPart;
			result.BuildParameter = paramsElement.Element(SchemaItem.buildValue).Value;
			result.RevisionType = result.SelectedSchema.RevisionPart;
			result.RevisionParameter = paramsElement.Element(SchemaItem.revisionValue).Value;
			return result;
		}

		public static string FindAssemblyInfoFile(string directory) {
			var result = default(string);
			var dir = new DirectoryInfo(directory);
			var propDir = dir.GetDirectories().FirstOrDefault(x => x.Name.Equals("Properties", StringComparison.OrdinalIgnoreCase));
			if (propDir != null) {
				var assyInfo = propDir.GetFiles().FirstOrDefault(x => x.Name.Equals("assemblyinfo.cs", StringComparison.OrdinalIgnoreCase));
				if (assyInfo != null) {
					return assyInfo.FullName;
				}
			} else {
				var assyInfo = dir.GetFiles().FirstOrDefault(x => x.Name.Equals("assemblyinfo.cs", StringComparison.OrdinalIgnoreCase));
				if (assyInfo != null) {
					return assyInfo.FullName;
				}
				foreach (var dirItem in dir.GetDirectories()) {
					result = FindAssemblyInfoFile(dirItem.FullName);
					if (!string.IsNullOrEmpty(result)) {
						break;
					}
				}
			}
			return result;
		}

		public static string GetProjectNameFromProjectFile(string projectFile) => Path.GetFileNameWithoutExtension(projectFile);


		public delegate int DateTimeHandler(DateTime v1);
		public delegate int Handler();
		public delegate int IntBoolNullDateTimeHandler(int v1, bool v2, DateTime? v3);
		public delegate int IntHandler(int v1);
		public delegate int IntNullDateTimeHandler(int v1, DateTime? v2);
		private delegate void IgnoreHandler(int value);
		public event ReportProgressHandler ReportProgress;

		private string assemblyInfoPath = default;
		private Version currentAssemblyVersion = default;
		private string revisionParameter = default;
		private Version modifiedAssemblyVersion = default;
		private string name = default;
		private ObservableCollection<TransformTypes> allTransformTypes = default;
		private string projectDirectory = default;
		private string projectFileName = default;
		private string schemaName = default;
		private DateTime? lastBuildDate = default;
		private XElement schemaElement = default;
		private TransformTypes majorType = default;
		private TransformTypes minorType = default;
		private TransformTypes buildType = default;
		private TransformTypes revisionType = default;
		private ObservableCollection<SchemaItem> schemas = default;
		private string majorParameter = default;
		private string minorParameter = default;
		private string buildParameter = default;
		private SchemaItem selectedSchema = default;
        private string fullPath = default;

        public string FullPath {
            get => fullPath;
            set {
                fullPath = value;
                if (!File.Exists(value))
                    throw new FileNotFoundException($"Cannot find project file {FullPath}");
                ProjectFileName = Path.GetFileName(FullPath);
                ProjectDirectory = Path.GetDirectoryName(FullPath);
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

		public string AssemblyInfoPath {
			get => assemblyInfoPath;
			set {
				assemblyInfoPath = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public Version CurrentAssemblyVersion {
			get => currentAssemblyVersion;
			set {
				currentAssemblyVersion = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public Version ModifiedAssemblyVersion {
			get => modifiedAssemblyVersion;
			set {
				modifiedAssemblyVersion = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<SchemaItem> Schemas {
			get => schemas;
			set {
				schemas = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string Name {
			get => name;
			set {
				var previousName = Name;
				name = value;
				if (!string.IsNullOrEmpty(ProjectFileName) && !string.IsNullOrEmpty(previousName)) {
					ProjectFileName = ProjectFileName.Replace(previousName, Name);
				}
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ProjectDirectory {
			get => projectDirectory;
			set {
				projectDirectory = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ProjectFileName {
			get => projectFileName;
			set {
				projectFileName = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SchemaName {
			get => schemaName;
			set {
				schemaName = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public SchemaItem SelectedSchema {
			get => selectedSchema;
			set {
				selectedSchema = value;
				if (SelectedSchema != null) {
					SchemaName = SelectedSchema.Name;
				} else {
					SchemaName = null;
				}
				MajorType = SelectedSchema.MajorPart;
				MinorType = SelectedSchema.MinorPart;
				BuildType = SelectedSchema.BuildPart;
				RevisionType = SelectedSchema.RevisionPart;
				MajorParameter = SelectedSchema.MajorParameter;
				MinorParameter = SelectedSchema.MinorParameter;
				BuildParameter = SelectedSchema.BuildParameter;
				RevisionParameter = SelectedSchema.RevisionParameter;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public DateTime? LastBuildDate {
			get => lastBuildDate;
			set {
				lastBuildDate = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public XElement SchemaElement {
			get => schemaElement;
			set {
				schemaElement = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<TransformTypes> AllTransformTypes {
			get => allTransformTypes;
			set {
				allTransformTypes = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes MajorType {
			get => majorType;
			set {
				majorType = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes MinorType {
			get => minorType;
			set {
				minorType = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes BuildType {
			get => buildType;
			set {
				buildType = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes RevisionType {
			get => revisionType;
			set {
				revisionType = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string MajorParameter {
			get => majorParameter;
			set {
				majorParameter = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string MinorParameter {
			get => minorParameter;
			set {
				minorParameter = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string BuildParameter {
			get => buildParameter;
			set {
				buildParameter = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string RevisionParameter {
			get => revisionParameter;
			set {
				revisionParameter = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public void ModifyVersion() {
			const int padSize = 28;
			LoadSchemaData(SchemaItem.majorValue, MajorType, MajorParameter);
			LoadSchemaData(SchemaItem.minorValue, MinorType, MinorParameter);
			LoadSchemaData(SchemaItem.buildValue, BuildType, BuildParameter);
			LoadSchemaData(SchemaItem.revisionValue, RevisionType, RevisionParameter);

			ReportProgress?.Invoke(this, new ReportProgressEventArgs($"{("AssemblyInfo path:").PadLeft(padSize)} {AssemblyInfoPath}"));
			ReportProgress?.Invoke(this, new ReportProgressEventArgs($"{("Start Assembly Version:").PadLeft(padSize)} {CurrentAssemblyVersion}"));
			ReportProgress?.Invoke(this, new ReportProgressEventArgs($"{("Assembly methods:").PadLeft(padSize)} {MajorType},{MinorType},{BuildType},{RevisionType}"));

			var major = ProcessMethod(MajorType, new object[] {
				CurrentAssemblyVersion.Major, LastBuildDate, MajorParameter
			});
			var minor = ProcessMethod(MinorType, new object[] {
				CurrentAssemblyVersion.Minor, LastBuildDate, MinorParameter
			});
			var build = ProcessMethod(BuildType, new object[] {
				CurrentAssemblyVersion.Build, LastBuildDate, BuildParameter
			});
			var revision = ProcessMethod(RevisionType, new object[] {
				CurrentAssemblyVersion.Revision, LastBuildDate, RevisionParameter
			});

			ModifiedAssemblyVersion = new Version(major, minor, build, revision);
			ReportProgress?.Invoke(this, new ReportProgressEventArgs($"{("End Assembly Version:").PadLeft(padSize)} {ModifiedAssemblyVersion}"));
		}

		private void InitializeDelegates() {
			delegates.Add(TransformTypes.Ignore, new IntHandler(UpdateMethods.Ignore));
			delegates.Add(TransformTypes.Fixed, new IntHandler(UpdateMethods.Fixed));
			delegates.Add(TransformTypes.Increment, new IntBoolNullDateTimeHandler(UpdateMethods.Increment));
			delegates.Add(TransformTypes.IncrementResetEachDay, new IntNullDateTimeHandler(UpdateMethods.IncrementResetEachDay));
			delegates.Add(TransformTypes.IncrementEachDay, new IntNullDateTimeHandler(UpdateMethods.IncrementEachDay));
			delegates.Add(TransformTypes.Random, new Handler(UpdateMethods.Random));
			delegates.Add(TransformTypes.Year, new Handler(UpdateMethods.Year));
			delegates.Add(TransformTypes.TwoDigitYear, new Handler(UpdateMethods.TwoDigitYear));
			delegates.Add(TransformTypes.Month, new Handler(UpdateMethods.Month));
			delegates.Add(TransformTypes.Day, new Handler(UpdateMethods.Day));
			delegates.Add(TransformTypes.DayValue, new DateTimeHandler(UpdateMethods.DayValueFrom));
			delegates.Add(TransformTypes.DayvalueFrom, new DateTimeHandler(UpdateMethods.DayValueFrom));
			delegates.Add(TransformTypes.SecondValueFrom, new DateTimeHandler(UpdateMethods.SecondValueFrom));
			delegates.Add(TransformTypes.Second, new Handler(UpdateMethods.Second));
		}

		private void LoadSchemaData(string partName, TransformTypes transformType, string partParameter) {
			switch (partName.ToLower()) {
				case SchemaItem.majorValue:
					MajorType = transformType;
					MajorParameter = partParameter;
					break;

				case SchemaItem.minorValue:
					MinorType = transformType;
					MinorParameter = partParameter;
					break;

				case SchemaItem.buildValue:
					BuildType = transformType;
					BuildParameter = partParameter;
					break;

				case SchemaItem.revisionValue:
					RevisionType = transformType;
					RevisionParameter = partParameter;
					break;
			}

		}

		private int ProcessMethod(TransformTypes transformType, object[] paramValues) {
			var result = default(int);
			var current = (int)paramValues[0];
			var lastUpdate = DateTime.Now.AddDays(-1);
			var minDate = new DateTime(1900, 1, 1);
			var hasParam = !string.IsNullOrEmpty((string)paramValues[2]);
			var hasLastDate = ((DateTime?)paramValues[1]).HasValue;
			if (hasLastDate) {
				lastUpdate = ((DateTime?)paramValues[1]).Value;
			}

			var parameters = new List<object>();
			var dgt = delegates[transformType];
			switch (transformType) {
				case TransformTypes.Ignore:
					parameters.Add(current);
					break;

				case TransformTypes.Increment:
					parameters.Add(current);
					parameters.Add(false);
					parameters.Add(null);
					break;

				case TransformTypes.IncrementResetEachDay:
				case TransformTypes.IncrementEachDay:
					parameters.Add(current);
					if (hasLastDate) {
						parameters.Add(lastUpdate);
					} else {
						parameters.Clear();
						parameters.Add(hasParam ? int.Parse((string)paramValues[2]) : 0);
						dgt = delegates[TransformTypes.Fixed];
					}
					break;

				case TransformTypes.DayValue:
				case TransformTypes.SecondValue:
					parameters.Add(minDate);
					break;

				case TransformTypes.DayvalueFrom:
				case TransformTypes.SecondValueFrom:
					parameters.Add(hasParam ? DateTime.Parse((string)paramValues[2]) : minDate);
					break;

				case TransformTypes.Fixed:
					parameters.Add(hasParam ? int.Parse((string)paramValues[2]) : 0);
					break;

				case TransformTypes.Random:
				case TransformTypes.Year:
				case TransformTypes.TwoDigitYear:
				case TransformTypes.Month:
				case TransformTypes.Day:
					break;
			}
			result = dgt != null
				? (int)dgt.DynamicInvoke(parameters.ToArray())
				: (int)delegates[transformType].DynamicInvoke(parameters.ToArray());
			return result;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		public object Clone() => MemberwiseClone();

		public static List<ProjectData> GetClonedList(List<ProjectData> original) {
			var result = new List<ProjectData>();
			original.ForEach(x => result.Add(x.Clone().As<ProjectData>()));
			return result;
		}
	}
}
