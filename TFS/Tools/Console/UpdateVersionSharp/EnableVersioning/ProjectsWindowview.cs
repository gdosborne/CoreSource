namespace EnableVersioning {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using GregOsborne.Application;
	using GregOsborne.MVVMFramework;
	using VersionMaster;

	public partial class SchemaWindowview : ViewModelBase {
		public override void Initialize() {

		}

		private void Project_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "Name") {
				this.HasChanges = true;
			}
		}

		public List<ProjectData> Copy { get; private set; } = default;
		private ObservableCollection<ProjectData> projects = default;
		public ObservableCollection<ProjectData> Projects {
			get => this.projects;
			set {
				if (value != null && this.Copy == null) {
					this.Copy = ProjectData.GetClonedList(value.ToList());
				}
				this.projects = value;

				if (this.Projects != null) {
					this.Projects.ToList().ForEach(project => project.PropertyChanged += this.Project_PropertyChanged);
				}

				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ProjectData selectedProject = default;
		public ProjectData SelectedProject {
			get => this.selectedProject;
			set {
				this.selectedProject = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

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
