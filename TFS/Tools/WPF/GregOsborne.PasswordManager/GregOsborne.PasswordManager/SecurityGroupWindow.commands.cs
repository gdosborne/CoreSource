// <copyright file="SecurityGroupWindow.commands.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>2/26/2020</date>

namespace GregOsborne.PasswordManager {
    using GregOsborne.MVVMFramework;

    public partial class SecurityGroupWindowView {
        private DelegateCommand closeWindowCommand = default;
        private DelegateCommand okCommand = default;
        public event ExecuteUiActionHandler ExecuteUiAction;
        public DelegateCommand CloseWindowCommand => this.closeWindowCommand ?? (this.closeWindowCommand = new DelegateCommand(CloseWindow, ValidateCloseWindowState));

        public DelegateCommand OKCommand => this.okCommand ?? (this.okCommand = new DelegateCommand(OK, ValidateOKState));

        private void CloseWindow(object state) => DialogResult = false;

        private void OK(object state) => DialogResult = true;

        private bool ValidateCloseWindowState(object state) => true;

        private bool ValidateOKState(object state) => !string.IsNullOrEmpty(GroupName) && !string.IsNullOrEmpty(Description);
    }
}
