using Common.MVVMFramework;

namespace OzMiniDB.Builder {
    public partial class NewTableWindowView {

        #region OK Command
        private DelegateCommand _OKCommand = default;
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => !string.IsNullOrWhiteSpace(TableName);
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

    }
}
