namespace EnableVersioning {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.MVVMFramework;
	using VersionMaster;

	public partial class MainWindowView : ViewModelBase {
		private bool hasProjectFile = default;
		private bool areFileControlsEnabled = default;
		private string consoleText = default;
		private Visibility consoleVisibility = default;
		private string errorMessage = default;
		private string projectFileName = default;
		private string projectName = default;
		private string assemblyInfoFilename = default;
		private ObservableCollection<VersionMaster.SchemaItem> schemas = default;
		private VersionMaster.SchemaItem selectedSchema = default;
		private ObservableCollection<ProjectData> projects = default;
		private bool areSettingProjectDataValues = false;
		private ProjectData projectData = default;
		private string configFileName = default;

		private void Manipulator_TargetsFileExists(object sender, TargetsFileExistsEventArgs e) {
			var p = new Dictionary<string, object> {
				{ "cancel", false },
				{ "targetsfilename", e.FileName}
			};
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("display targets exists", p));
			e.Cancel = (bool)p["cancel"];
		}

		public ProjectConfigurationReader Reader {
			get; private set;
		}

		public bool AreFileControlsEnabled {
			get => this.areFileControlsEnabled;
			set {
				this.areFileControlsEnabled = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ConsoleText {
			get => this.consoleText;
			set {
				this.consoleText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public Visibility ConsoleVisibility {
			get => this.consoleVisibility;
			set {
				this.consoleVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ErrorMessage {
			get => this.errorMessage;
			set {
				this.errorMessage = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public bool HasProjectFile {
			get => this.hasProjectFile;
			set {
				this.hasProjectFile = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string AssemblyInfoFilename {
			get => this.assemblyInfoFilename;
			set {
				this.assemblyInfoFilename = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ProjectData ProjectData {
			get => this.projectData;
			set {
				this.projectData = value;
				if (this.ProjectData == null) {
					this.AddLineToConsole($"{this.projectName} is a new project - settings do not exist");
					var p = new Dictionary<string, object> { { "cancel", false } };
					ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("ask create project", p));
					if ((bool)p["cancel"]) {
						return;
					}
					var pd = new ProjectData(this.ProjectFileName, this.ProjectName);
					this.projectData = pd;
					this.ProjectData.PropertyChanged += this.X_PropertyChanged;
					this.Projects.Add(this.ProjectData);
					this.originalSchemaNameForProject = default;
					this.SelectedSchema = this.Schemas.FirstOrDefault(x => x.Name == "default");
				} else {
					this.AddLineToConsole($"Project Name: {this.ProjectData.Name}");
					this.areSettingProjectDataValues = true;
					this.originalSchemaNameForProject = this.ProjectData.SchemaName;
					this.SelectedSchema = this.Schemas.FirstOrDefault(x => x.Name == this.ProjectData.SchemaName);
					this.HasProjectFile = this.ProjectData != null;
					this.ProjectName = this.ProjectData.Name;
					this.areSettingProjectDataValues = false;
				}
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ProjectName {
			get => this.projectName;
			set {
				this.projectName = value;
				if (!this.areSettingProjectDataValues) {
					this.ProjectData = this.Reader.Projects.FirstOrDefault(x => x.Name.Equals(this.ProjectName, StringComparison.OrdinalIgnoreCase));
				}

				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ProjectFileName {
			get => this.projectFileName;
			set {
				this.projectFileName = value;
				this.ProjectName = ProjectData.GetProjectNameFromProjectFile(this.ProjectFileName);

				if (!this.Reader.Projects.Any(x => x.Name.Equals(this.ProjectName, StringComparison.OrdinalIgnoreCase))) {
					this.ProjectData = new ProjectData(this.ProjectFileName, this.ProjectName);
					this.Projects.Add(this.ProjectData);
					this.Reader.Projects.Add(this.ProjectData);
				}

				this.HasProjectFile = !string.IsNullOrEmpty(this.ProjectFileName) && File.Exists(this.ProjectFileName);
				this.AddLineToConsole($"Project File: {this.ProjectFileName}");
				this.AssemblyInfoFilename = ProjectData.FindAssemblyInfoFile(Path.GetDirectoryName(this.ProjectFileName));
				this.AddLineToConsole($"AssemblyInfo File: {this.AssemblyInfoFilename}");

				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ConfigFileName {
			get => this.configFileName;
			set {
				this.configFileName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<VersionMaster.SchemaItem> Schemas {
			get => this.schemas;
			set {
				this.schemas = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<ProjectData> Projects {
			get => this.projects;
			set {
				this.projects = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string originalSchemaNameForProject = default;
		public VersionMaster.SchemaItem SelectedSchema {
			get => this.selectedSchema;
			set {
				this.selectedSchema = value;
				this.AddLineToConsole($"Schema Name: {this.SelectedSchema.Name}");
				if (this.SelectedSchema != null && this.ProjectData != null) {
					this.ProjectData.SelectedSchema = this.SelectedSchema;
				}

				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public void AddLineToConsole(string text) => this.ConsoleText += $"{text}{Environment.NewLine}";

		public void AddToConsole(string text) => this.ConsoleText += text;

		public override void Initialize() {
			var configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UpdateVersion");
			if (!Directory.Exists(configFolder)) {
				Directory.CreateDirectory(configFolder);
			}

			this.ConfigFileName = Path.Combine(configFolder, "UpdateVersion.Projects.xml");
			if (!File.Exists(this.ConfigFileName)) {
				var srcFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "UpdateVersion.Projects.xml");
				File.Copy(srcFileName, this.ConfigFileName);
			}
			this.Reader = new ProjectConfigurationReader(this.ConfigFileName);
			this.Reader.Initialize();
			this.Schemas = new ObservableCollection<VersionMaster.SchemaItem>(this.Reader.Schemas);
			this.Projects = new ObservableCollection<ProjectData>(this.Reader.Projects.OrderBy(x => x.Name));
			this.Projects.CollectionChanged += this.Projects_CollectionChanged;
			this.Projects.ToList().ForEach(x => x.PropertyChanged += this.X_PropertyChanged);
		}

		private void X_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.HasChanges = true;
		private void Projects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => this.HasChanges = true;

		private bool hasChanges = default;
		public bool HasChanges {
			get => this.hasChanges;
			set {
				this.hasChanges = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}