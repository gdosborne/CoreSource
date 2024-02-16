using GregOsborne.Application.Linq;
using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using VersionMaster;
using file = GregOsborne.Application.IO.File;

namespace UpdateVersioning {
    public partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = "Update Versioning [designer]";
            Schemas = new ObservableCollection<SchemaItem>();
            Projects = new ObservableCollection<ProjectData>();
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Update Versioning";
            if (!File.Exists(schemasFilename)) {
                fullSchemaPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Data", "UpdateVersion.Projects.xml");
                File.Copy(fullSchemaPath, schemasFilename);
            }
            var root = XDocument.Load(schemasFilename).Root;
            if (root != null) {
                var schemasRoot = root.Element("schemas");
                var tt = Enumerations.GetTransformTypes();
                schemasRoot.Elements().ToList().ForEach(e => {
                    Schemas.Add(SchemaItem.FromXElement(e, tt));
                });
                var projectsRoot = root.Element("projects");
                projectsRoot.Elements().ToList().ForEach(e => {
                    Projects.Add(ProjectData.FromXElement(e, Schemas.ToList()));
                });
            }
            if (Projects.Any()) {
                SelectedProject = Projects.First();
            }
        }

        private string schemasFilename = Path.Combine(App.ApplicationDirectory, "UpdateVersion.Projects.xml");
        private string fullSchemaPath = default;

        private ProjectConfigurationReader projectReader = default;
        private readonly string csAssyInfoFileName = "assemblyinfo.cs";
        private readonly string vbAssyInfoFileName = "assemblyinfo.vb";
        private string assemblyInfoFilename = default;

        #region Title Property
        private string _Title = default;
        public string Title {
            get => _Title;
            set {
                _Title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedProject Property
        private ProjectData _SelectedProject = default;
        public ProjectData SelectedProject {
            get => _SelectedProject;
            set {
                _SelectedProject = value;
                if (SelectedProject != null) {

                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Projects Property
        private ObservableCollection<ProjectData> _Projects = default;
        public ObservableCollection<ProjectData> Projects {
            get => _Projects;
            set {
                _Projects = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Schemas Property
        private ObservableCollection<SchemaItem> _Schemas = default;
        public ObservableCollection<SchemaItem> Schemas {
            get => _Schemas;
            set {
                _Schemas = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private string GetAssemblyInfoPath(string projectPath) {
            var dirInfo = new DirectoryInfo(projectPath);
            return GetAssemblyInfoPath(dirInfo);
        }

        private string GetAssemblyInfoPath(DirectoryInfo dirInfo) {
            var file = dirInfo.GetFiles().FirstOrDefault(x => x.Name.Equals(assemblyInfoFilename, StringComparison.OrdinalIgnoreCase));
            if (file != null) return file.FullName;
            foreach (var dir in dirInfo.GetDirectories()) {
                var value = GetAssemblyInfoPath(dir);
                if (string.IsNullOrEmpty(value)) continue;
                return value;
            }
            return default;
        }

        public enum Actions {
            SaveProjects,
            NewProject,
            OpenProject
        }

        private void ExAction(Actions action) => ExecuteAction(action.ToString());

    }
}
