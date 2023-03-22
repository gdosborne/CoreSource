using Common.MVVMFramework;
using System;
using System.Linq;

namespace MakeCompositeIcon {
    internal partial class RecycleBinWindowView {

        public enum Actions {
            ClearRecycleBin,
            RestoreSelected
        }

        #region CloseCommand
        private DelegateCommand _CloseCommand = default;
        /// <summary>Gets the Close command.</summary>
        /// <value>The Close command.</value>
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => true;
        private void Close(object state) {
            DialogResult = false;
        }
        #endregion

        #region ClearCommand
        private DelegateCommand _ClearCommand = default;
        /// <summary>Gets the Clear command.</summary>
        /// <value>The Clear command.</value>
        public DelegateCommand ClearCommand => _ClearCommand ??= new DelegateCommand(Clear, ValidateClearState);
        private bool ValidateClearState(object state) => App.RecyleBinHasFiles;
        private void Clear(object state) {
            ExecuteAction(nameof(Actions.ClearRecycleBin));
        }
        #endregion

        #region RestoreCommand
        private DelegateCommand _RestoreCommand = default;
        /// <summary>Gets the Restore command.</summary>
        /// <value>The Restore command.</value>
        public DelegateCommand RestoreCommand => _RestoreCommand ??= new DelegateCommand(Restore, ValidateRestoreState);
        private bool ValidateRestoreState(object state) => SelectedIcons != null && SelectedIcons.Any();
        private void Restore(object state) {
            ExecuteAction(nameof(Actions.RestoreSelected));
        }
        #endregion
    }
}
