using GregOsborne.MVVMFramework;

namespace ManageVersioning {
    public partial class ProjectWindowView {
        public enum UIActions {
            SelectProjectPath,
            EditSchema
        }

        #region ProjectPath Command
        private DelegateCommand _ProjectPathCommand = default;
        public DelegateCommand ProjectPathCommand => _ProjectPathCommand ??= new DelegateCommand(ProjectPath, ValidateProjectPathState);
        private bool ValidateProjectPathState(object state) => Project != null;
        private void ProjectPath(object state) {
            ExecuteAction(nameof(UIActions.SelectProjectPath));
        }
        #endregion

        #region OK Command
        private DelegateCommand _OKCommand = default;
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => Project != null && !string.IsNullOrEmpty(Project.FullPath)
            && System.IO.File.Exists(Project.FullPath) && Project.SelectedSchema != null;
        private void OK(object state) {
            DialogResult = true;
        }
        #endregion

        #region Cancel Command
        private DelegateCommand _CancelCommand = default;
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            DialogResult = false;
        }
        #endregion

        #region EditName Command
        private DelegateCommand _EditNameCommand = default;
        public DelegateCommand EditNameCommand => _EditNameCommand ??= new DelegateCommand(EditName, ValidateEditNameState);
        private bool ValidateEditNameState(object state) => true;
        private void EditName(object state) {
            EditControlVisibility = System.Windows.Visibility.Visible;
            TitleControlVisibility = System.Windows.Visibility.Collapsed;
            IsOKDefault = false;
        }
        #endregion

        #region EditSchema Command
        private DelegateCommand _EditSchemaCommand = default;
        public DelegateCommand EditSchemaCommand => _EditSchemaCommand ??= new DelegateCommand(EditSchema, ValidateEditSchemaState);
        private bool ValidateEditSchemaState(object state) => Project != null && Project.SelectedSchema != null;
        private void EditSchema(object state) {
            ExecuteAction(nameof(UIActions.EditSchema));
        }
        #endregion

    }
}
