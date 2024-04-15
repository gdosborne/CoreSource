using ColorFontPickerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ManageVersioning {
    public partial class SettingsWindowView {
        public enum Actions {
            PickColor,
            PickImage,
            SelectSharedFile
        }

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

        #region SelectColorCommand Command
        private DelegateCommand _SelectColorCommand = default;
        public DelegateCommand SelectColorCommand => _SelectColorCommand ??= new DelegateCommand(SelectColor, ValidateSelectColorCommandState);
        private bool ValidateSelectColorCommandState(object state) => true;
        private void SelectColor(object state) {
            ExecuteAction(nameof(Actions.PickColor));
        }
        #endregion

        #region SelectImage Command
        private DelegateCommand _SelectImageCommand = default;
        public DelegateCommand SelectImageCommand => _SelectImageCommand ??= new DelegateCommand(SelectImage, ValidateSelectImageState);
        private bool ValidateSelectImageState(object state) => true;
        private void SelectImage(object state) {
            ExecuteAction(nameof(Actions.PickImage));
        }
        #endregion
        
        #region SelectSharedFile Command
        private DelegateCommand _SelectSharedFileCommand = default;
        public DelegateCommand SelectSharedFileCommand => _SelectSharedFileCommand ??= new DelegateCommand(SelectSharedFile, ValidateSelectSharedFileState);
        private bool ValidateSelectSharedFileState(object state) => true;
        private void SelectSharedFile(object state) {
            ExecuteAction(nameof(Actions.SelectSharedFile));
        }
        #endregion

    }
}
