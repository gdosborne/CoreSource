using ApplicationFramework.Media;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            SelectColor,
            Delete,
            ViewXaml,
            ShowSettingType
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
        private bool ValidateUndoState(object state) {
            if (SelectedIcon == null)
                return false;

            var result = false;
            return result;
        }
        private void Undo(object state) {
            if (SelectedIcon == null)
                return;
            var result = false;

            //var value = SelectedIcon.Clipboard.GetPrevious(true, ref propertyName);
            //if (!string.IsNullOrEmpty(propertyName)) {
            //    var prop = SelectedIcon.GetType().GetProperty(propertyName);
            //    prop?.SetValue(SelectedIcon, value);
            //}
        }
        #endregion

        #region RedoCommand
        private DelegateCommand _RedoCommand = default;
        /// <summary>Gets the Redo command.</summary>
        /// <value>The Redo command.</value>
        public DelegateCommand RedoCommand => _RedoCommand ??= new DelegateCommand(Redo, ValidateRedoState);
        private bool ValidateRedoState(object state) {
            if (SelectedIcon == null)
                return false;

            var result = false;
            return result;
        }
    
        private void Redo(object state) {
            if (SelectedIcon == null)
                return;
            var result = false;
            //if (!string.IsNullOrEmpty(propertyName)) {
            //    var prop = SelectedIcon.GetType().GetProperty(propertyName);
            //    prop?.SetValue(SelectedIcon, value);
            //}
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

        #region DeleteIconCommand
        private DelegateCommand _DeleteIconCommand = default;
        /// <summary>Gets the DeleteIcon command.</summary>
        /// <value>The DeleteIcon command.</value>
        public DelegateCommand DeleteIconCommand => _DeleteIconCommand ??= new DelegateCommand(DeleteIcon, ValidateDeleteIconState);
        private bool ValidateDeleteIconState(object state) => SelectedIcon != null;
        private void DeleteIcon(object state) {
            ExecuteAction(nameof(Actions.Delete));
        }
        #endregion

        #region ViewXamlCommand
        private DelegateCommand _ViewXamlCommand = default;
        /// <summary>Gets the ViewXaml command.</summary>
        /// <value>The ViewXaml command.</value>
        public DelegateCommand ViewXamlCommand => _ViewXamlCommand ??= new DelegateCommand(ViewXaml, ValidateViewXamlState);
        private bool ValidateViewXamlState(object state) => SelectedIcon != null;
        private void ViewXaml(object state) {
            ExecuteAction(nameof(Actions.ViewXaml));
        }
        #endregion

        #region ShowSettingTypeCommand
        private DelegateCommand _ShowSettingTypeCommand = default;
        /// <summary>Gets the ShowSettingType command.</summary>
        /// <value>The ShowSettingType command.</value>
        public DelegateCommand ShowSettingTypeCommand => _ShowSettingTypeCommand ??= new DelegateCommand(ShowSettingType, ValidateShowSettingTypeState);
        private bool ValidateShowSettingTypeState(object state) => true;
        private void ShowSettingType(object state) {
            var p = new Dictionary<string, object> {
                { "Type", (string)state }
            };
            ExecuteAction(nameof(Actions.ShowSettingType), p);
        }
        #endregion


    }
    
}
