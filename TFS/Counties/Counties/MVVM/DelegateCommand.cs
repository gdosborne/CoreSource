using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Counties.MVVM {
	public class DelegateCommand : ICommand {
		#region Public Delegates

		public delegate void BlankHandler();

		#endregion Public Delegates

		#region Private Fields

		private readonly Predicate<object> _canExecute;

		private readonly Action<object> _execute;

		#endregion Private Fields

		#region Public Constructors

		public DelegateCommand(Action<object> execute)
			: this(execute, null) { }

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion Public Constructors

		#region Public Events

		public event EventHandler CanExecuteChanged;

		public event CheckAccessEventHandler CheckAccess;

		#endregion Public Events

		#region Public Methods

		public bool CanExecute(object parameter) {
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object parameter) {
			_execute(parameter);
		}

		public void RaiseCanExecuteChanged() {
			if (CheckAccess != null) {
				var e = new CheckAccessEventArgs();
				CheckAccess(this, e);
				if (e.HasAccess) {
					CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				} else {
					e.Dispatcher.BeginInvoke(new BlankHandler(RaiseCanExecuteChanged), null);
				}
			} else {
				try {
					CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				}
				catch {
					// ignored
				}
			}
		}

		#endregion Public Methods
	}
}
