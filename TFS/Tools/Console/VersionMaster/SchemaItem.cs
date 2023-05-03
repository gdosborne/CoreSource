namespace VersionMaster {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Xml.Linq;
	using GregOsborne.Application;
	using static VersionMaster.Enumerations;

	public class SchemaItem {
		public static SchemaItem Create(string name,
			IEnumerable<TransformTypes> methods,
			TransformTypes majorMethod,
			string majorParameter,
			TransformTypes minorMethod,
			string minorParameter,
			TransformTypes buildMethod,
			string buildParameter,
			TransformTypes revisionMethod,
			string revisionParameter) {
			var result = new SchemaItem() {
				Name = name,
				MajorPart = majorMethod,
				MajorParameter = majorParameter,
				MinorPart = minorMethod,
				MinorParameter = minorParameter,
				BuildPart = buildMethod,
				BuildParameter = buildParameter,
				RevisionPart = revisionMethod,
				RevisionParameter = revisionParameter,
				TransformMethods = new ObservableCollection<TransformTypes>(methods)
			};
			return result;
		}

		public static SchemaItem FromXElement(XElement element, IEnumerable<TransformTypes> methods) {
			var name = element.Attribute(nameAttribValue).Value;
			element = element.Elements().FirstOrDefault();
			var majorElement = element.Elements().FirstOrDefault(x => x.Attribute(nameAttribValue).Value.Equals(majorValue, StringComparison.OrdinalIgnoreCase));
			var minorElement = element.Elements().FirstOrDefault(x => x.Attribute(nameAttribValue).Value.Equals(minorValue, StringComparison.OrdinalIgnoreCase));
			var buildElement = element.Elements().FirstOrDefault(x => x.Attribute(nameAttribValue).Value.Equals(buildValue, StringComparison.OrdinalIgnoreCase));
			var revisionElement = element.Elements().FirstOrDefault(x => x.Attribute(nameAttribValue).Value.Equals(revisionValue, StringComparison.OrdinalIgnoreCase));

			var majorMethod = (TransformTypes)Enum.Parse(typeof(TransformTypes), majorElement.Attribute(methodAttribValue).Value, true);
			var minorMethod = (TransformTypes)Enum.Parse(typeof(TransformTypes), minorElement.Attribute(methodAttribValue).Value, true);
			var buildMethod = (TransformTypes)Enum.Parse(typeof(TransformTypes), buildElement.Attribute(methodAttribValue).Value, true);
			var revisionMethod = (TransformTypes)Enum.Parse(typeof(TransformTypes), revisionElement.Attribute(methodAttribValue).Value, true);

			var majorParameter = majorElement.Attribute(parameterAttribValue) == null ? string.Empty : majorElement.Attribute(parameterAttribValue).Value;
			var minorParameter = minorElement.Attribute(parameterAttribValue) == null ? string.Empty : minorElement.Attribute(parameterAttribValue).Value;
			var buildParameter = buildElement.Attribute(parameterAttribValue) == null ? string.Empty : buildElement.Attribute(parameterAttribValue).Value;
			var revisionParameter = revisionElement.Attribute(parameterAttribValue) == null ? string.Empty : revisionElement.Attribute(parameterAttribValue).Value;
			return Create(name, methods, majorMethod, majorParameter, minorMethod, minorParameter, buildMethod, buildParameter, revisionMethod, revisionParameter);
		}

		public const string dateFormatValue = "yyyy-MM-dd";
		public const string majorValue = "major";
		public const string minorValue = "minor";
		public const string buildValue = "build";
		public const string revisionValue = "revision";
		public const string nameAttribValue = "name";
		public const string versionAttribValue = "version";
		public const string methodAttribValue = "method";
		public const string parameterAttribValue = "parameter";
		public const string lastBuildDateAttribValue = "lastbuilddate";
		public const string updateVersionProjectsElementValue = "updateversion.projects";
		public const string levelElementValue = "level";
		public const string detailElementValue = "detail";
		public const string schemaElementName = "schema";
		public const string projectElementName = "project";
		public const string pathElementName = "path";
		public const string paramsElementName = "parameters";
		public const string schemasElementName = "schemas";
		public const string methodsElementName = "methods";
		public const string projectsElementName = "projects";

		private ObservableCollection<TransformTypes> transformMethods = default;
		private string name = default;
		private TransformTypes majorPart = default;
		private TransformTypes minorPart = default;
		private TransformTypes buildPart = default;
		private TransformTypes revisionPart = default;
		private string majorParameter = default;
		private string minorParameter = default;
		private string buildParameter = default;
		private string revisionParameter = default;

		public ObservableCollection<TransformTypes> TransformMethods {
			get => transformMethods;
			set {
				transformMethods = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string Name {
			get => name;
			set {
				name = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes MajorPart {
			get => majorPart;
			set {
				majorPart = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes MinorPart {
			get => minorPart;
			set {
				minorPart = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes BuildPart {
			get => buildPart;
			set {
				buildPart = value;
				InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public TransformTypes RevisionPart {
			get => revisionPart;
			set {
				revisionPart = value;
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

		public override string ToString() {
			var result = new StringBuilder(1024);
			result.Append($"{Name} => (");
			result.Append($"{MajorPart},");
			result.Append($"{MinorPart},");
			result.Append($"{BuildPart},");
			result.Append($"{RevisionPart}");
			result.Append(")");
			return result.ToString();
		}

		public XElement ToXElement() {
			var result = new XElement(schemaElementName,
				new XAttribute(nameAttribValue, Name),
				new XElement(detailElementValue,
					new XElement(levelElementValue,
						new XAttribute(nameAttribValue, majorValue),
						new XAttribute(methodAttribValue, MajorPart),
						new XAttribute(parameterAttribValue, MajorParameter)),
					new XElement(levelElementValue,
						new XAttribute(nameAttribValue, minorValue),
						new XAttribute(methodAttribValue, MinorPart),
						new XAttribute(parameterAttribValue, MinorParameter)),
					new XElement(levelElementValue,
						new XAttribute(nameAttribValue, buildValue),
						new XAttribute(methodAttribValue, BuildPart),
						new XAttribute(parameterAttribValue, BuildParameter)),
					new XElement(levelElementValue,
						new XAttribute(nameAttribValue, revisionValue),
						new XAttribute(methodAttribValue, RevisionPart),
						new XAttribute(parameterAttribValue, RevisionParameter))));
			return result;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
