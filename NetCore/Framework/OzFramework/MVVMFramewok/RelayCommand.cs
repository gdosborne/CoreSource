/* File="RelayCommand"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Windows.Input;

namespace Common.MVVMFramework {
    public class RelayCommand : ICommand {
        #region Private Fields

        private readonly Action handler;

        private bool isEnabled;

        #endregion Private Fields

        #region Public Constructors

        public RelayCommand(Action handler) {
            this.handler = handler;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Properties

        public bool IsEnabled {
            get {
                return isEnabled;
            }
            set {
                if (value != isEnabled) {
                    isEnabled = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public bool CanExecute(object parameter) {
            return IsEnabled;
        }

        public void Execute(object parameter) {
            handler();
        }

        #endregion Public Methods
    }
}
