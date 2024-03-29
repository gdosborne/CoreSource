namespace MyMinistry.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Input;

	public class DelegateCommand : ICommand
	{
		public DelegateCommand(Action<object> execute)
			: this(execute, null)
		{
		}
		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_Execute = execute;
			_CanExecute = canExecute;
		}

		public string Name { get; set; }
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

		public event EventHandler CanExecuteChanged;
		private readonly Predicate<object> _CanExecute;
		private readonly Action<object> _Execute;

		public string Description { get; set; }
	}
}
