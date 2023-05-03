using System;
using System.Windows.Input;

namespace GregOsborne.MVVMFramework {
    public class DelegateCommand : ICommand {
        #region Public Delegates

        public delegate void BlankHandler();

        #endregion Public Delegates

        #region Private Fields

        private readonly Predicate<object> canExecute;

        private readonly Action<object> execute;

        #endregion Private Fields

        #region Public Constructors

        public DelegateCommand(Action<object> execute)
            : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged;

        public event CheckAccessEventHandler CheckAccess;

        #endregion Public Events

        #region Public Methods

        public bool CanExecute(object parameter) {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter) {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged() {
            if (CheckAccess != null) {
                var e = new CheckAccessEventArgs();
                CheckAccess(this, e);
                if (e.HasAccess) {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                else {
                    e.Dispatcher.BeginInvoke(new BlankHandler(RaiseCanExecuteChanged), null);
                }
            }
            else {
                try {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
                catch(Exception ex) {
                    // ignored
                }
            }
        }

		#endregion Public Methods

	}
}
