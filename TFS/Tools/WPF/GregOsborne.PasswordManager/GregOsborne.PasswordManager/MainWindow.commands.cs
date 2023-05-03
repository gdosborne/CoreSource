// <copyright file="MainWindow.commands.cs" company="">
// Copyright (c) 2019 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>12/3/2019</date>

namespace GregOsborne.PasswordManager {
    using System;
    using System.IO;
    using System.Linq;

    using GregOsborne.Application.Primitives;
    using GregOsborne.Dialogs;
    using GregOsborne.MVVMFramework;

    public partial class MainWindowView {
        private DelegateCommand addNewGroupCommand = default;
        private DelegateCommand addnewItemCommand = default;
        private DelegateCommand closeCommand = default;
        private DelegateCommand closeOptionsCommand = default;
        private DelegateCommand importFileCommand = default;
        private DelegateCommand maximizeRestoreCommand = default;
        private DelegateCommand minimizeCommand = default;
        private DelegateCommand saveAsCommand = default;
        private DelegateCommand saveThemeCommand = default;
        private DelegateCommand showOptionsCommand = default;
        public DelegateCommand AddNewGroupCommand => this.addNewGroupCommand ?? (this.addNewGroupCommand = new DelegateCommand(AddNewGroup, ValidateAddNewGroupState));

        public DelegateCommand AddNewItemCommand => this.addnewItemCommand ?? (this.addnewItemCommand = new DelegateCommand(AddNewItem, ValidateAddNewItemState));

        public DelegateCommand CloseCommand => this.closeCommand ?? (this.closeCommand = new DelegateCommand(Close, ValidateCloseState));

        public DelegateCommand CloseOptionsCommand => this.closeOptionsCommand ?? (this.closeOptionsCommand = new DelegateCommand(CloseOptions, ValidateCloseOptionsState));

        public DelegateCommand ImportFileCommand => this.importFileCommand ?? (this.importFileCommand = new DelegateCommand(ImportFile, ValidateImportFileState));

        public DelegateCommand MaximizeRestoreCommand => this.maximizeRestoreCommand ?? (this.maximizeRestoreCommand = new DelegateCommand(MaximizeRestore, ValidateMaximizeRestoreState));

        public DelegateCommand MinimizeCommand => this.minimizeCommand ?? (this.minimizeCommand = new DelegateCommand(Minimize, ValidateMinimizeState));

        public DelegateCommand SaveAsCommand => this.saveAsCommand ?? (this.saveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState));

        public DelegateCommand SaveThemeCommand => this.saveThemeCommand ?? (this.saveThemeCommand = new DelegateCommand(SaveTheme, ValidateSaveThemeState));

        public DelegateCommand ShowOptionsCommand => this.showOptionsCommand ?? (this.showOptionsCommand = new DelegateCommand(ShowOptions, ValidateShowOptionsState));

        private void AddNewGroup(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(AddNewSecurityGroupText));

        private void AddNewItem(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(AddNewSecurityItemText));

        private void Close(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(CloseApplicationText));

        private void CloseOptions(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(CloseOptionsText));

        private void ImportFile(object state) {
            
        }

        private void MaximizeRestore(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(MaximizeRestoreApplicationText));

        private void Minimize(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(MinimizeApplicationText));

        private void SaveAs(object state) => throw new NotImplementedException();

        private void SaveTheme(object state) {
            Theme.Save();
            var p = new UiActionParameters(MainWindowView.ShowMessageText) {
                { "Title", "Theme saved" },
                { "MainText", "" },
                { "Message", "Your theme has been saved." },
                { "Icon", Dialogs.TaskDialogIcon.Information },
                { "Button1", new TaskDialogButton(ButtonType.Ok) }
            };
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(p));
            App.Current.As<App>().ThemeManager.Themes.Add(Theme);
            Theme = App.Current.As<App>().ThemeManager.Themes.FirstOrDefault(x => x.Name == Theme.Name);
        }

        private void ShowOptions(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(OpenOptionsText));

        private bool ValidateAddNewGroupState(object state) => true;

        private bool ValidateAddNewItemState(object state) => true;

        private bool ValidateCloseOptionsState(object state) => true;

        private bool ValidateCloseState(object state) => true;

        private bool ValidateImportFileState(object state) => true;

        private bool ValidateMaximizeRestoreState(object state) => true;

        private bool ValidateMinimizeState(object state) => true;

        private bool ValidateSaveAsState(object state) => true;

        private bool ValidateSaveThemeState(object state) => Theme != null && Theme.HasChanges && IsAllowEditingEnabled;

        private bool ValidateShowOptionsState(object state) => true;
    }
}
