using System;
using System.Windows.Input;

namespace MyApplication.Commands
{
	public class RelayCommand : ICommand
	{
		#region Private Fields
		private bool isEnabled;
		private Action theHandler;
		#endregion

		#region Public Constructors

		public RelayCommand(Action handler)
		{
			theHandler = handler;
		}

		#endregion

		#region Public Events
		public event EventHandler CanExecuteChanged;
		#endregion

		#region Public Properties
		public bool IsEnabled
		{
			get { return isEnabled; }
			set
			{
				if (value != isEnabled)
				{
					isEnabled = value;
					if (CanExecuteChanged != null)
					{
						CanExecuteChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		public object Parameter { get; private set; }
		#endregion

		#region Public Methods

		public bool CanExecute(object parameter)
		{
			return IsEnabled;
		}

		public void Execute(object parameter)
		{
			Parameter = parameter;
			theHandler();
		}

		#endregion
	}
}