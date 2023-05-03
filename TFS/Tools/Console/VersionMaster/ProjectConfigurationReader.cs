namespace VersionMaster {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Xml.Linq;
	using Microsoft.VisualBasic.FileIO;
	using static VersionMaster.Enumerations;
	using OzIO = GregOsborne.Application.IO;

	public class ProjectConfigurationReader : IDisposable {

		public event DIsplayMessageHandler DisplayMessage;

		private bool disposedValue = false;
		private XDocument document = default;

		public ProjectConfigurationReader(string configFilename) {
			ConfigurationFilename = configFilename;
			ConfiurationFileVersion = null;
			IsVersionAvailable = false;
			Schemas = new List<SchemaItem>();
			Projects = new List<ProjectData>();
			Methods = new List<TransformTypes>();
		}

		public string ConfigurationFilename {
			get; private set;
		}

		public Version ConfiurationFileVersion {
			get; private set;
		}

		public bool IsVersionAvailable {
			get; private set;
		}

		public List<SchemaItem> Schemas {
			get; private set;
		}

		public List<ProjectData> Projects {
			get; private set;
		}

		public List<TransformTypes> Methods {
			get; private set;
		}

		public SchemaItem SelectedSchema {
			get; set;
		}

		public void Initialize() => Refresh();

		private void Refresh() {
			if (!File.Exists(ConfigurationFilename)) {
				throw new ApplicationException($"Cannot find {ConfigurationFilename}");
			}
			Schemas.Clear();
			Projects.Clear();
			Methods.Clear();

			document = XDocument.Load(ConfigurationFilename);
			if (!document.Root.LocalName().Equals(SchemaItem.updateVersionProjectsElementValue)) {
				throw new ApplicationException($"Invalid project configuration file - missing root ({ConfigurationFilename})");
			}

			if (document.Root.Attribute(SchemaItem.versionAttribValue) == null || !Version.TryParse(document.Root.Attribute(SchemaItem.versionAttribValue).Value, out var ver) || ver.Major < 2) {
				IsVersionAvailable = false;
				ConfiurationFileVersion = null;
				DisplayMessage?.Invoke(this, new DisplayMessageEventArgs("The project configuration file is missing the version attribute or it is " +
					"not a version 2.0 file. Will treat file as version 2.0."));
			} else {
				IsVersionAvailable = true;
				ConfiurationFileVersion = ver;
			}

			var schemasRoot = document.Root.Element(SchemaItem.schemasElementName);
			var methodsRoot = document.Root.Element(SchemaItem.methodsElementName);
			var projectsRoot = document.Root.Element(SchemaItem.projectsElementName);
			if (methodsRoot == null) {
				throw new ApplicationException($"Invalid project configuration file - missing methods ({ConfigurationFilename})");
			}
			methodsRoot.Elements().ToList().ForEach(methodElement => Methods.Add((TransformTypes)Enum.Parse(typeof(TransformTypes), methodElement.Attribute(SchemaItem.nameAttribValue).Value, true)));

			if (schemasRoot == null) {
				throw new ApplicationException($"Invalid project configuration file - missing schemas ({ConfigurationFilename})");
			}
			schemasRoot.Elements().ToList().ForEach(schemaElement => Schemas.Add(SchemaItem.FromXElement(schemaElement, Methods)));

			if (projectsRoot == null) {
				throw new ApplicationException($"Invalid project configuration file - missing projects ({ConfigurationFilename})");
			}
			projectsRoot.Elements().ToList().ForEach(projectElement => {
				var pData = ProjectData.FromXElement(projectElement, Schemas);
				if (!Projects.Any(x => x.Name == pData.Name)) {
					Projects.Add(pData);
				}
			});
		}

		public void Save() {
			document = new XDocument(
				new XElement(SchemaItem.updateVersionProjectsElementValue, new XAttribute(SchemaItem.versionAttribValue, ConfiurationFileVersion)));
			var schema = new XElement(SchemaItem.schemasElementName);
			Schemas.ForEach(x => {
				schema.Add(x.ToXElement());
			});
			document.Root.Add(schema);

			var method = new XElement(SchemaItem.methodsElementName);
			Methods.ForEach(x => {
				method.Add(new XElement(SchemaItem.methodAttribValue, new XAttribute(SchemaItem.nameAttribValue, x)));
			});
			document.Root.Add(method);

			var project = new XElement(SchemaItem.projectsElementName);
			Projects.ForEach(x => {
				project.Add(x.ToXElement());
			});
			document.Root.Add(project);

			var breakOut = false;
			var writeFile = OzIO.Path.GetTempFile(Path.GetDirectoryName(ConfigurationFilename));
			document.Save(writeFile);
			while (!breakOut) {
				try {
					FileSystem.CopyFile(writeFile, ConfigurationFilename, true);
					breakOut = true;
				}
				catch { System.Threading.Thread.Sleep(100); }
			}

		}

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					document = null;
				}
				disposedValue = true;
			}
		}

		public void Dispose() => Dispose(true);
	}
}
