using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateVersioning {
    public partial class MainWindowView {
        #region SaveProjects Command
        private DelegateCommand _SaveProjectsCommand = default;
        public DelegateCommand SaveProjectsCommand => _SaveProjectsCommand ??= new DelegateCommand(SaveProjects, ValidateSaveProjectsState);
        private bool ValidateSaveProjectsState(object state) => true;
        private void SaveProjects(object state) {
            ExAction(Actions.SaveProjects);
        }
        #endregion

        #region NewProject Command
        private DelegateCommand _NewProjectCommand = default;
        public DelegateCommand NewProjectCommand => _NewProjectCommand ??= new DelegateCommand(NewProject, ValidateNewProjectState);
        private bool ValidateNewProjectState(object state) => true;
        private void NewProject(object state) {
            ExAction(Actions.NewProject);
        }
        #endregion

        #region OpenProject Command
        private DelegateCommand _OpenProjectCommand = default;
        public DelegateCommand OpenProjectCommand => _OpenProjectCommand ??= new DelegateCommand(OpenProject, ValidateOpenProjectState);
        private bool ValidateOpenProjectState(object state) => true;
        private void OpenProject(object state) {
            ExAction(Actions.OpenProject);
        }
        #endregion
    }
}
