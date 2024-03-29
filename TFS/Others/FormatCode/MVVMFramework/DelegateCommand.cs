using System;
using System.Windows.Input;

namespace MVVMFramework
{
	public class DelegateCommand : ICommand
	{
		#region Private Fields
		private readonly Predicate<object> _CanExecute;
		private readonly Action<object> _Execute;
		#endregion

		#region Public Constructors

		public DelegateCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_Execute = execute;
			_CanExecute = canExecute;
		}

		#endregion

		#region Public Events
		public event EventHandler CanExecuteChanged;
		#endregion

		#region Public Methods

		public bool CanExecute(object parameter)
		{
			if (_CanExecute == null)
				return true;
			return _CanExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_Execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
				CanExecuteChanged(this, EventArgs.Empty);
		}

		#endregion
	}
}
