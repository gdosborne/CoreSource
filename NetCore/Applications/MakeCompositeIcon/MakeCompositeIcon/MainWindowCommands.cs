using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System.Collections.Generic;

namespace MakeCompositeIcon {
    internal partial class MainWindowView {
        public new enum Actions {
            FileOpen,
            FileNew,
            FileSave,
            FileSaveAs,
            Exit,
            UndoChanges,
            RedoChanges,
            Cut,
            Copy,
            Paste,
            CloseIcon,
            OpenSettings,
            SelectColor
        }

        #region FileOpenCommand
        private DelegateCommand _FileOpenCommand = default;
        /// <summary>Gets the FileOpen command.</summary>
        /// <value>The FileOpen command.</value>
        public DelegateCommand FileOpenCommand => _FileOpenCommand ??= new DelegateCommand(FileOpen, ValidateFileOpenState);
        private bool ValidateFileOpenState(object state) => true;
        private void FileOpen(object state) {
            ExecuteAction(nameof(Actions.FileOpen));
        }
        #endregion

        #region FileNewCommand
        private DelegateCommand _FileNewCommand = default;
        /// <summary>Gets the FileNew command.</summary>
        /// <value>The FileNew command.</value>
        public DelegateCommand FileNewCommand => _FileNewCommand ??= new DelegateCommand(FileNew, ValidateFileNewState);
        private bool ValidateFileNewState(object state) => true;
        private void FileNew(object state) {
            ExecuteAction(nameof(Actions.FileNew));
        }
        #endregion

        #region FileSaveCommand
        private DelegateCommand _FileSaveCommand = default;
        /// <summary>Gets the FileSave command.</summary>
        /// <value>The FileSave command.</value>
        public DelegateCommand FileSaveCommand => _FileSaveCommand ??= new DelegateCommand(FileSave, ValidateFileSaveState);
        private bool ValidateFileSaveState(object state) => SelectedIcon != null;
        private void FileSave(object state) {
            ExecuteAction(nameof(Actions.FileSave));
        }
        #endregion

        #region FileSaveAsCommand
        private DelegateCommand _FileSaveAsCommand = default;
        /// <summary>Gets the FileSaveAs command.</summary>
        /// <value>The FileSaveAs command.</value>
        public DelegateCommand FileSaveAsCommand => _FileSaveAsCommand ??= new DelegateCommand(FileSaveAs, ValidateFileSaveAsState);
        private bool ValidateFileSaveAsState(object state) => SelectedIcon != null;
        private void FileSaveAs(object state) {
            ExecuteAction(nameof(Actions.FileSaveAs));
        }
        #endregion

        #region ExitCommand
        private DelegateCommand _ExitCommand = default;
        /// <summary>Gets the Exit command.</summary>
        /// <value>The Exit command.</value>
        public DelegateCommand ExitCommand => _ExitCommand ??= new DelegateCommand(Exit, ValidateExitState);
        private bool ValidateExitState(object state) => true;
        private void Exit(object state) {
            ExecuteAction(nameof(Actions.Exit));
        }
        #endregion

        #region UndoCommand
        private DelegateCommand _UndoCommand = default;
        /// <summary>Gets the Undo command.</summary>
        /// <value>The Undo command.</value>
        public DelegateCommand UndoCommand => _UndoCommand ??= new DelegateCommand(Undo, ValidateUndoState);
        private bool ValidateUndoState(object state) => true;
        private void Undo(object state) {
            ExecuteAction(nameof(Actions.UndoChanges));
        }
        #endregion

        #region RedoCommand
        private DelegateCommand _RedoCommand = default;
        /// <summary>Gets the Redo command.</summary>
        /// <value>The Redo command.</value>
        public DelegateCommand RedoCommand => _RedoCommand ??= new DelegateCommand(Redo, ValidateRedoState);
        private bool ValidateRedoState(object state) => true;
        private void Redo(object state) {
            ExecuteAction(nameof(Actions.RedoChanges));
        }
        #endregion

        #region CutCommand
        private DelegateCommand _CutCommand = default;
        /// <summary>Gets the Cut command.</summary>
        /// <value>The Cut command.</value>
        public DelegateCommand CutCommand => _CutCommand ??= new DelegateCommand(Cut, ValidateCutState);
        private bool ValidateCutState(object state) => true;
        private void Cut(object state) {
            ExecuteAction(nameof(Actions.Cut));
        }
        #endregion

        #region CopyCommand
        private DelegateCommand _CopyCommand = default;
        /// <summary>Gets the Copy command.</summary>
        /// <value>The Copy command.</value>
        public DelegateCommand CopyCommand => _CopyCommand ??= new DelegateCommand(Copy, ValidateCopyState);
        private bool ValidateCopyState(object state) => true;
        private void Copy(object state) {
            ExecuteAction(nameof(Actions.Copy));
        }
        #endregion

        #region PasteCommand
        private DelegateCommand _PasteCommand = default;
        /// <summary>Gets the Paste command.</summary>
        /// <value>The Paste command.</value>
        public DelegateCommand PasteCommand => _PasteCommand ??= new DelegateCommand(Paste, ValidatePasteState);
        private bool ValidatePasteState(object state) => true;
        private void Paste(object state) {
            ExecuteAction(nameof(Actions.Paste));
        }
        #endregion

        #region CloseIconCommand
        private DelegateCommand _CloseIconCommand = default;
        /// <summary>Gets the CloseIcon command.</summary>
        /// <value>The CloseIcon command.</value>
        public DelegateCommand CloseIconCommand => _CloseIconCommand ??= new DelegateCommand(CloseIcon, ValidateCloseIconState);
        private bool ValidateCloseIconState(object state) => true;
        private void CloseIcon(object state) {
            ExecuteAction(nameof(Actions.CloseIcon));
        }
        #endregion

        #region OpenSettingsCommand
        private DelegateCommand _OpenSettingsCommand = default;
        /// <summary>Gets the OpenSettings command.</summary>
        /// <value>The OpenSettings command.</value>
        public DelegateCommand OpenSettingsCommand => _OpenSettingsCommand ??= new DelegateCommand(OpenSettings, ValidateOpenSettingsState);
        private bool ValidateOpenSettingsState(object state) => true;
        private void OpenSettings(object state) {
            ExecuteAction(nameof(Actions.OpenSettings));
        }
        #endregion

        #region SelectedColorCommand
        private DelegateCommand _SelectedColorCommand = default;
        /// <summary>Gets the SelectedColor command.</summary>
        /// <value>The SelectedColor command.</value>
        public DelegateCommand SelectedColorCommand => _SelectedColorCommand ??= new DelegateCommand(SelectedColor, ValidateSelectedColorState);
        private bool ValidateSelectedColorState(object state) => true;
        private void SelectedColor(object state) {
            var pars = new Dictionary<string, object> {
                { "IsPrimary", state.As<string>().Equals("Primary") },
                { "IsSurface", state.As<string>().Equals("Surface") }
            };
            ExecuteAction(nameof(Actions.SelectColor), pars);
            IsSingleColorSelected = SelectedIcon.PrimaryBrush.Color.ToHexValue() == SelectedIcon.SecondaryBrush.Color.ToHexValue();
        }
        #endregion

    }
}
