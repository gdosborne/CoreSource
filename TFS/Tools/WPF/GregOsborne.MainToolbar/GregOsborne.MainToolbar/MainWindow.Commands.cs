// <copyright file="MainWindow.Commands.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace GregOsborne.MainToolbar {
    using GregOsborne.MVVMFramework;

    public partial class MainWindowView {
        private DelegateCommand helpCommand = default;
        private DelegateCommand optionsCommand = default;
        public DelegateCommand HelpCommand => this.helpCommand ?? (this.helpCommand = new DelegateCommand(Help, ValidateHelpState));

        public DelegateCommand OptionsCommand => this.optionsCommand ?? (this.optionsCommand = new DelegateCommand(Options, ValidateOptionsState));

        private void Help(object state) {
        }

        private void Options(object state) {
        }

        private bool ValidateHelpState(object state) => true;

        private bool ValidateOptionsState(object state) => true;
    }
}
