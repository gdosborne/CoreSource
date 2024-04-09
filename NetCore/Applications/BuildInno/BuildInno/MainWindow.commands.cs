using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ShowToolsContentMenu
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
        private bool ValidateSaveFileState(object state) => true;
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

    }
}
