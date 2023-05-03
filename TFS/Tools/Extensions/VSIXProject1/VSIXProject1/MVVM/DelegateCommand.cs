namespace VSIXProject1.MVVM
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        public delegate void BlankHandler();

        private readonly Predicate<object> _canExecute;

        private readonly Action<object> _execute;

        public DelegateCommand(Action<object> execute)
            : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            try {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            catch {
                // ignored
            }
        }
    }
}
