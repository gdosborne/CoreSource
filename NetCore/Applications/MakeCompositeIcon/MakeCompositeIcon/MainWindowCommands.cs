using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System.Collections.Generic;
using System.Diagnostics;

namespace MakeCompositeIcon {
    internal partial class MainWindowView {
        public new enum Actions {
            FileOpen,
            FileNew,
            FileSave,
            FileSaveAs,
            Exit,
            OpenSettings,
            SelectColor,
            Delete,
            ViewXaml,
            ShowSettingType,
            RenameIcon,
            ShowRecycleBin,
            ShowCharacterWindow
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

        #region RenameIconCommand
        private DelegateCommand _RenameIconCommand = default;
        /// <summary>Gets the RenameIcon command.</summary>
        /// <value>The RenameIcon command.</value>
        public DelegateCommand RenameIconCommand => _RenameIconCommand ??= new DelegateCommand(RenameIcon, ValidateRenameIconState);
        private bool ValidateRenameIconState(object state) => SelectedIcon != null;
        private void RenameIcon(object state) {
            ExecuteAction(nameof(Actions.RenameIcon));
        }
        #endregion

        #region ShowRecycleCommand
        private DelegateCommand _ShowRecycleCommand = default;
        /// <summary>Gets the ShowRecycle command.</summary>
        /// <value>The ShowRecycle command.</value>
        public DelegateCommand ShowRecycleCommand => _ShowRecycleCommand ??= new DelegateCommand(ShowRecycle, ValidateShowRecycleState);
        private bool ValidateShowRecycleState(object state) => App.RecyleBinHasFiles;
        private void ShowRecycle(object state) {
            ExecuteAction(nameof(Actions.ShowRecycleBin));
        }
        #endregion

        #region CenterVerticallyCommand
        private DelegateCommand _CenterVerticallyCommand = default;
        /// <summary>Gets the CenterVertically command.</summary>
        /// <value>The CenterVertically command.</value>
        public DelegateCommand CenterVerticallyCommand => _CenterVerticallyCommand ??= new DelegateCommand(CenterVertically, ValidateCenterVerticallyState);
        private bool ValidateCenterVerticallyState(object state) => true;
        private void CenterVertically(object state) {
            SelectedIcon.SecondaryVerticalOffset = 0;
        }
        #endregion

        #region CenterHorizontallyCommand
        private DelegateCommand _CenterHorizontallyCommand = default;
        /// <summary>Gets the CenterHorizontally command.</summary>
        /// <value>The CenterHorizontally command.</value>
        public DelegateCommand CenterHorizontallyCommand => _CenterHorizontallyCommand ??= new DelegateCommand(CenterHorizontally, ValidateCenterHorizontallyState);
        private bool ValidateCenterHorizontallyState(object state) => true;
        private void CenterHorizontally(object state) {
            SelectedIcon.SecondaryHorizontalOffset = 0;
        }
        #endregion

        #region ShowCharWindowCommand
        private DelegateCommand _ShowCharWindowCommand = default;
        /// <summary>Gets the ShowCharWindow command.</summary>
        /// <value>The ShowCharWindow command.</value>
        public DelegateCommand ShowCharWindowCommand => _ShowCharWindowCommand ??= new DelegateCommand(ShowCharWindow, ValidateShowCharWindowState);
        private bool ValidateShowCharWindowState(object state) => true;
        private void ShowCharWindow(object state) {
            var p = new Dictionary<string, object> {
                { "IsPrimary", (string)state == "IsPrimary" }
            };
            ExecuteAction(nameof(Actions.ShowCharacterWindow), p);
        }
        #endregion

        #region GoToFilesDirCommand
        private DelegateCommand _GoToFilesDirCommand = default;
        /// <summary>Gets the GoToFilesDir command.</summary>
        /// <value>The GoToFilesDir command.</value>
        public DelegateCommand GoToFilesDirCommand => _GoToFilesDirCommand ??= new DelegateCommand(GoToFilesDir, ValidateGoToFilesDirState);
        private bool ValidateGoToFilesDirState(object state) => true;
        private void GoToFilesDir(object state) {
            var p = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "explorer.exe",
                    Arguments = App.ThisApp.FilesDirectory,
                    UseShellExecute = true                    
                }
            };
            p.Start();
        }
        #endregion

        #region GoToFileCommand
        private DelegateCommand _GoToFileCommand = default;
        /// <summary>Gets the GoToFile command.</summary>
        /// <value>The GoToFile command.</value>
        public DelegateCommand GoToFileCommand => _GoToFileCommand ??= new DelegateCommand(GoToFile, ValidateGoToFileState);
        private bool ValidateGoToFileState(object state) => SelectedIcon != null;
        private void GoToFile(object state) {
            var p = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{SelectedIcon.FullPath}\"",
                    UseShellExecute = true
                }
            };
            p.Start();
        }
        #endregion
    }

}
