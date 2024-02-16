namespace ManageVersioning {
    public partial class SchemaWindowView {

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

        #region OK Command
        private DelegateCommand _OKCommand = default;
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => true;
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

        #region Delete Command
        private DelegateCommand _DeleteCommand = default;
        public DelegateCommand DeleteCommand => _DeleteCommand ??= new DelegateCommand(Delete, ValidateDeleteState);
        private bool ValidateDeleteState(object state) => true;
        private void Delete(object state) {
            IsDelete = true;
            DialogResult = true;
        }
        #endregion

    }
}
