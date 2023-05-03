namespace EnableVersioning {
	using System;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Xml.Linq;

	internal class ProjectVersionManipulator {
		private bool isUpdateVersionInProjectFile = default;

		private void GetUpdateValuesFromFile(string updateVersionFileName, out string schemaName, out DateTime lastDate, out bool skipUpdate) {
			schemaName = "default";
			lastDate = DateTime.Parse("01/01/2000");
			skipUpdate = default;
			var doc = updateVersionFileName.GetXDocument();
			if (doc == null) {
				return;
			}

			var tempSchemaName = schemaName;
			var tempLastDate = lastDate;
			var tempSkipUpdate = skipUpdate;
			doc.Root.Elements().ToList().ForEach(x => {
				if (x.LocalName().Equals("propertygroup", StringComparison.OrdinalIgnoreCase)) {
					x.Elements().ToList().ForEach(y => {
						if (y.Attribute("Label") != null) {
							if (y.Attribute("Label").Value.Equals("schemaname", StringComparison.OrdinalIgnoreCase)) {
								tempSchemaName = y.Value;
							} else if (y.Attribute("Label").Value.Equals("lastupdate", StringComparison.OrdinalIgnoreCase)) {
								tempLastDate = y.Value.CastTo<DateTime>();
							} else if (y.Attribute("Label").Value.Equals("skip", StringComparison.OrdinalIgnoreCase)) {
								tempSkipUpdate = y.Value.CastTo<bool>();
							}
						}
					});
					return;
				}
			});
			schemaName = tempSchemaName;
			lastDate = tempLastDate;
			skipUpdate = tempSkipUpdate;
		}

		private XElement GetValueElement(string name, object value) => new XElement("AppData", value.ToString(),
				new XAttribute("Label", name));

		public ProjectVersionManipulator() {
		}

		public ProjectVersionManipulator(string projectFileName)
			: this() => this.SetProjectFileName(projectFileName);

		public event TargetsFileExistsHandler TargetsFileExists;

		public bool IsProjectPreparedForUpdateVersion => this.UpdateVersionFileExists && this.IsUpdateVersionInProjectFile;

		public bool IsSkipUpdateEnabled { get; set; }

		public bool IsUpdateVersionInProjectFile {
			get {
				var doc = this.ProjectFileName.GetXDocument();
				if (doc == null) {
					return false;
				}

				var result = this.isUpdateVersionInProjectFile;
				doc.Root.Elements().ToList().ForEach(x => {
					if (result != this.isUpdateVersionInProjectFile) {
						return;
					}

					if (x.LocalName().Equals("itemgroup", StringComparison.OrdinalIgnoreCase)) {
						x.Elements().ToList().ForEach(y => {
							if (result != this.isUpdateVersionInProjectFile) {
								return;
							}

							if (y.LocalName().Equals("None", StringComparison.OrdinalIgnoreCase) && y.Attribute("Include") != null) {
								result = y.Attribute("Include").Value.Equals(Path.GetFileName(this.UpdateVersionFileName), StringComparison.OrdinalIgnoreCase);
							}
						});
					}
				});
				if (result != this.isUpdateVersionInProjectFile) {
					this.isUpdateVersionInProjectFile = result;
				}

				return this.isUpdateVersionInProjectFile && File.Exists(this.UpdateVersionFileName);
			}
		}

		public bool IsValidTargetsEntry { get; set; } = false;

		public DateTime LastUpdate { get; set; } = DateTime.Parse("01/01/2000");

		public string ProjectFileName { get; private set; } = null;

		public string ProjectUpdateVersionFileName { get; private set; } = null;

		public string SchemaName { get; set; } = null;

		public bool UpdateVersionFileExists => File.Exists(this.UpdateVersionFileName);

		public string UpdateVersionFileName => $"{this.ProjectFileName}.updateversion";

		public void CreateTargetsFile(string targetsFileName) {
		}

		public void CreateUpdateVersionFile() => this.CreateUpdateVersionFile(this.SchemaName);

		public void CreateUpdateVersionFile(string schemaName) {
			var doc = new XDocument(
						new XElement("Project",
							new XElement("PropertyGroup",
								this.GetValueElement("SchemaName", schemaName),
								this.GetValueElement("LastUpdate", this.LastUpdate.ToString("yyyy-MM-dd")),
								this.GetValueElement("Skip", this.IsSkipUpdateEnabled)
			)));
			doc.Save(this.ProjectUpdateVersionFileName);
			if (!this.IsUpdateVersionInProjectFile || !this.IsValidTargetsEntry) {
				var projDoc = this.ProjectFileName.GetXDocument();
				var root = projDoc.Root;
				var itemGroup = new XElement("ItemGroup",
					new XElement("None",
						new XAttribute("Include", Path.GetFileName(this.ProjectUpdateVersionFileName))));
				root.Add(itemGroup);
				projDoc.Save(this.ProjectFileName);
			}
		}

		public bool HasUpdateTaragetEntry() => this.HasUpdateTaragetEntry(this.ProjectFileName);

		public bool HasUpdateTaragetEntry(string projectFileName) {
			var doc = projectFileName.GetXDocument();
			if (doc == null) {
				return false;
			}

			var result = false;
			doc.Root.Elements().ToList().ForEach(x => {
				if (result) {
					return;
				}

				if (x.LocalName().Equals("import", StringComparison.OrdinalIgnoreCase) && x.Attribute("Project") != null) {
					var targetFilePath = x.Attribute("Project").Value;
					var info = new FileInfo(Path.GetFileName(targetFilePath));
					if (info.Name.Equals("updateversion.targets", StringComparison.OrdinalIgnoreCase)) {
						result = true;
						return;
					}
				}
			});
			return result;
		}

		public void ModifyUpdateVersionFile() => this.ModifyUpdateVersionFile(this.SchemaName);

		public void ModifyUpdateVersionFile(string schemaName) {
			var doc = this.ProjectUpdateVersionFileName.GetXDocument();
			var proGroupElement = doc.Root.Element("PropertyGroup");
			var schemaElement = proGroupElement.Elements().FirstOrDefault(x => x.LocalName().Equals("appdata", StringComparison.OrdinalIgnoreCase) && x.Attribute("Label").Value.Equals("SchemaName", StringComparison.OrdinalIgnoreCase));
			var lastUpdateElement = proGroupElement.Elements().FirstOrDefault(x => x.LocalName().Equals("appdata", StringComparison.OrdinalIgnoreCase) && x.Attribute("Label").Value.Equals("LastUpdate", StringComparison.OrdinalIgnoreCase));
			var skipElement = proGroupElement.Elements().FirstOrDefault(x => x.LocalName().Equals("appdata", StringComparison.OrdinalIgnoreCase) && x.Attribute("Label").Value.Equals("Skip", StringComparison.OrdinalIgnoreCase));
			schemaElement.Value = schemaName;
			lastUpdateElement.Value = DateTime.Now.ToString("yyyy-MM-dd");
			skipElement.Value = this.IsSkipUpdateEnabled.ToString();
			doc.Save(this.ProjectUpdateVersionFileName);
		}

		public void SetProjectFileName(string projectFileName) {
			this.ProjectFileName = projectFileName;
			var ext = GregOsborne.Application.IO.File.Extension(this.ProjectFileName);
			if (ext.Equals(".csproj", StringComparison.OrdinalIgnoreCase)) {
				this.ProjectUpdateVersionFileName = $"{this.ProjectFileName}.updateversion";
			} else if (ext.Equals(".updateversion", StringComparison.OrdinalIgnoreCase)) {
				this.ProjectUpdateVersionFileName = this.ProjectFileName;
				this.ProjectFileName = this.ProjectFileName.Replace(ext, string.Empty);
			} else {
				throw new FileNotFoundException("You must select either a project file (.csproj) or an update version file (.updateversion).");
			}

			this.IsValidTargetsEntry = this.HasUpdateTaragetEntry(this.ProjectFileName);
			this.GetUpdateValuesFromFile(this.ProjectUpdateVersionFileName, out var schemaName, out var lastUpdate, out var skipUpdate);
			this.SchemaName = schemaName;
			this.LastUpdate = lastUpdate;
			this.IsSkipUpdateEnabled = skipUpdate;
		}

		public void WriteTargetsFile(string projectFileName) {
			if (!File.Exists(projectFileName)) {
				throw new FileNotFoundException("Cannot find project file name", projectFileName);
			}

			var projectDir = Path.GetDirectoryName(projectFileName);
			var targetsFileName = Path.Combine(projectDir, $"{Path.GetFileNameWithoutExtension(projectFileName)}.targets");
			if (File.Exists(targetsFileName)) {
				var e = new TargetsFileExistsEventArgs(targetsFileName);
				TargetsFileExists?.Invoke(this, e);
				if (e.Cancel) {
					return;
				}
			}
			this.CreateTargetsFile(targetsFileName);
		}
	}
}