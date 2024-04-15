using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BuildInno {
    public partial class MainWindowView {
        public enum Actions {
            Close,
            Minimize,
            CreateNew,
            OpenFile,
            SaveFile,
            ShowSettings,
            CopyGuid,
            ShowToolsContentMenu,
            Build,
            CutText,
            CopyText,
            PasteText,
            IsTextSelected
        }

        #region Close Command
        private DelegateCommand _CloseCommand = default;
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => true;
        private void Close(object state) {
            ExecuteAction(nameof(Actions.Close));
        }
        #endregion

        #region Minimize Command
        private DelegateCommand _MinimizeCommand = default;
        public DelegateCommand MinimizeCommand => _MinimizeCommand ??= new DelegateCommand(Minimize, ValidateMinimizeState);
        private bool ValidateMinimizeState(object state) => true;
        private void Minimize(object state) {
            ExecuteAction(nameof(Actions.Minimize));
        }
        #endregion

        #region CreateNew Command
        private DelegateCommand _CreateNewCommand = default;
        public DelegateCommand CreateNewCommand => _CreateNewCommand ??= new DelegateCommand(CreateNew, ValidateCreateNewState);
        private bool ValidateCreateNewState(object state) => true;
        private void CreateNew(object state) {
            ExecuteAction(nameof(Actions.CreateNew));
        }
        #endregion

        #region OpenFile Command
        private DelegateCommand _OpenFileCommand = default;
        public DelegateCommand OpenFileCommand => _OpenFileCommand ??= new DelegateCommand(OpenFile, ValidateOpenFileState);
        private bool ValidateOpenFileState(object state) => true;
        private void OpenFile(object state) {
            ExecuteAction(nameof(Actions.OpenFile));
        }
        #endregion

        #region SaveFile Command
        private DelegateCommand _SaveFileCommand = default;
        public DelegateCommand SaveFileCommand => _SaveFileCommand ??= new DelegateCommand(SaveFile, ValidateSaveFileState);
        private bool ValidateSaveFileState(object state) => SelectedProject != null;// && SelectedProject.Data != SelectedProject.OriginalData;
        private void SaveFile(object state) {
            ExecuteAction(nameof(Actions.SaveFile));
        }
        #endregion

        #region ShowSettings Command
        private DelegateCommand _ShowSettingsCommand = default;
        public DelegateCommand ShowSettingsCommand => _ShowSettingsCommand ??= new DelegateCommand(ShowSettings, ValidateShowSettingsState);
        private bool ValidateShowSettingsState(object state) => true;
        private void ShowSettings(object state) {
            ExecuteAction(nameof(Actions.ShowSettings));
        }
        #endregion

        #region CopyGuid Command
        private DelegateCommand _CopyGuidCommand = default;
        public DelegateCommand CopyGuidCommand => _CopyGuidCommand ??= new DelegateCommand(CopyGuid, ValidateCopyGuidState);
        private bool ValidateCopyGuidState(object state) => SelectedProject != null;
        private void CopyGuid(object state) {
            ExecuteAction(nameof(Actions.CopyGuid));
        }
        #endregion

        #region ShowToolsContentMenu Command
        private DelegateCommand _ShowToolsContentMenuCommand = default;
        public DelegateCommand ShowToolsContentMenuCommand => _ShowToolsContentMenuCommand ??= new DelegateCommand(ShowToolsContentMenu, ValidateShowToolsContentMenuState);
        private bool ValidateShowToolsContentMenuState(object state) => true;
        private void ShowToolsContentMenu(object state) {
            ExecuteAction(nameof(Actions.ShowToolsContentMenu));
        }
        #endregion

        #region Build Command
        private DelegateCommand _BuildCommand = default;
        public DelegateCommand BuildCommand => _BuildCommand ??= new DelegateCommand(Build, ValidateBuildState);
        private bool ValidateBuildState(object state) => SelectedProject != null;
        private void Build(object state) {
            ExecuteAction(nameof(Actions.Build));
        }
        #endregion

        #region CutText Command
        private DelegateCommand _CutTextCommand = default;
        public DelegateCommand CutTextCommand => _CutTextCommand ??= new DelegateCommand(CutText, ValidateCutTextState);
        private bool ValidateCutTextState(object state) => SelectedProject != null && IsTextSelected;
        private void CutText(object state) {
            ExecuteAction(nameof(Actions.CutText));
        }
        #endregion

        #region CopyText Command
        private DelegateCommand _CopyTextCommand = default;
        public DelegateCommand CopyTextCommand => _CopyTextCommand ??= new DelegateCommand(CopyText, ValidateCopyTextState);
        private bool ValidateCopyTextState(object state) => SelectedProject != null && IsTextSelected;
        private void CopyText(object state) {
            ExecuteAction(nameof(Actions.CopyText));
        }
        #endregion

        #region PasteText Command
        private DelegateCommand _PasteTextCommand = default;
        public DelegateCommand PasteTextCommand => _PasteTextCommand ??= new DelegateCommand(PasteText, ValidatePasteTextState);
        private bool ValidatePasteTextState(object state) => SelectedProject != null && 
            Clipboard.GetText(TextDataFormat.Text).Length > 0;
        private void PasteText(object state) {
            ExecuteAction(nameof(Actions.PasteText));
        }
        #endregion

    }
}
