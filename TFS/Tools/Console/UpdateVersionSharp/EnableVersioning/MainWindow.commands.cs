namespace EnableVersioning {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView {
		private DelegateCommand exitAppCommand = default;
		private DelegateCommand saveFilesCommand = default;
		private DelegateCommand selectProjectCommand = default;
		private DelegateCommand runTestCommand = default;
		private DelegateCommand showProjectsCommand = default;

		private void ShowProjects(object state) {
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("show projects window"));
		}

		private void ExitApp(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", false }
			};
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("ask to exit", p));
			if ((bool)p["cancel"]) {
				return;
			}

			App.Current.Shutdown(0);
		}

		private void SelectProject(object state) {
			var p = new Dictionary<string, object> {
				{ "cancel", false },
				{ "filename", null },
				{ "initialdirectory",  App.Session.ApplicationSettings.GetValue("General", "LastDirectory", string.Empty) }
			};
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("get project file", p));
			if ((bool)p["cancel"]) {
				return;
			}

			this.ProjectFileName = (string)p["filename"];
			App.Session.ApplicationSettings.AddOrUpdateSetting("General", "LastDirectory", Path.GetDirectoryName(this.ProjectFileName));
		}

		private void SaveFiles(object state) {
			this.Reader.Save();
			this.originalSchemaNameForProject = this.ProjectData.SchemaName;
			base.UpdateInterface();
			this.HasChanges = false;
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("display file saved"));
		}

		private Dictionary<string, Version> testVersions = default;
		private Dictionary<string, DateTime> testDates = default;

		private void RunTest(object state) {
			if (this.testVersions == null) {
				this.testVersions = new Dictionary<string, Version>();
				this.testDates = new Dictionary<string, DateTime>();
			}

			if (this.testVersions.ContainsKey(this.ProjectName)) {
				this.ProjectData.CurrentAssemblyVersion = this.testVersions[this.ProjectName];
				this.ProjectData.LastBuildDate = this.testDates[this.ProjectName];
			}

			this.AddLineToConsole("Beginning version update test.\n");
			this.ProjectData.ReportProgress += this.ProjectData_ReportProgress;
			this.ProjectData.ModifyVersion();
			this.ProjectData.ReportProgress -= this.ProjectData_ReportProgress;

			if (this.testVersions.ContainsKey(this.ProjectName)) {
				this.testVersions[this.ProjectName] = this.ProjectData.ModifiedAssemblyVersion;
				this.testDates[this.ProjectName] = DateTime.Now.Date;
			} else {
				this.testVersions.Add(this.ProjectName, this.ProjectData.ModifiedAssemblyVersion);
				this.testDates.Add(this.ProjectName, DateTime.Now.Date);
			}
		}

		private void ProjectData_ReportProgress(object sender, VersionMaster.ReportProgressEventArgs e) => this.AddLineToConsole(e.Message);

		private bool ValidateShowProjectsState(object state) => true;
		private bool ValidateExitAppState(object state) => true;
		private bool ValidateSaveFilesState(object state) => this.HasChanges;
		private bool ValidateSelectProjectState(object state) => true;
		private bool ValidateRunTestState(object state) => this.HasProjectFile;

		public event ExecuteUiActionHandler ExecuteUIAction;

		public DelegateCommand ShowProjectsCommand => this.showProjectsCommand ?? (this.showProjectsCommand = new DelegateCommand(this.ShowProjects, this.ValidateShowProjectsState));
		public DelegateCommand ExitAppCommand => this.exitAppCommand ?? (this.exitAppCommand = new DelegateCommand(this.ExitApp, this.ValidateExitAppState));
		public DelegateCommand SaveFilesCommand => this.saveFilesCommand ?? (this.saveFilesCommand = new DelegateCommand(this.SaveFiles, this.ValidateSaveFilesState));
		public DelegateCommand SelectProjectCommand => this.selectProjectCommand ?? (this.selectProjectCommand = new DelegateCommand(this.SelectProject, this.ValidateSelectProjectState));
		public DelegateCommand RunTestCommand => this.runTestCommand ?? (this.runTestCommand = new DelegateCommand(this.RunTest, this.ValidateRunTestState));
	}
}