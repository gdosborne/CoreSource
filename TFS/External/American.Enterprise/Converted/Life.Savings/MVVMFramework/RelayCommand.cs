namespace GregOsborne.MVVMFramework {
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        #region Private Fields

        private readonly Action _handler;

        private bool _isEnabled;

        #endregion Private Fields

        #region Public Constructors

        public RelayCommand(Action handler)
        {
            _handler = handler;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Properties

        public bool IsEnabled {
            get {
                return _isEnabled;
            }
            set {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public void Execute(object parameter)
        {
            _handler();
        }

        #endregion Public Methods
    }
}
