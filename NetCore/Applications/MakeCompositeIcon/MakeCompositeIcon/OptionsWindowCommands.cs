using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeCompositeIcon {
    internal partial class OptionsWindowView {

        #region OKCommand
        private DelegateCommand _OKCommand = default;
        /// <summary>Gets the OK command.</summary>
        /// <value>The OK command.</value>
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => true;
        private void OK(object state) {
            DialogResult = true;
        }
        #endregion

        #region CancelCommand
        private DelegateCommand _CancelCommand = default;
        /// <summary>Gets the Cancel command.</summary>
        /// <value>The Cancel command.</value>
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            DialogResult = false;
        }
        #endregion

        public enum Actions {
            ClearRecycleBin
        }

        #region ClearRecycleCommand
        private DelegateCommand _ClearRecycleCommand = default;
        /// <summary>Gets the ClearRecycle command.</summary>
        /// <value>The ClearRecycle command.</value>
        public DelegateCommand ClearRecycleCommand => _ClearRecycleCommand ??= new DelegateCommand(ClearRecycle, ValidateClearRecycleState);
        private bool ValidateClearRecycleState(object state) => App.RecyleBinHasFiles;
        private void ClearRecycle(object state) {
            ExecuteAction(nameof(Actions.ClearRecycleBin));
        }
        #endregion

    }
}
