namespace MyMinistry.Utilities
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;

	public class BindableBase : INotifyPropertyChanged
	{
		#region Public Events

		public event ExecuteUIActionHandler ExecuteUIAction;

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Public Events

		#region Protected Methods

		protected void OnExecuteUIAction(string commandToExecute, Dictionary<string, object> parameters)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs(commandToExecute, parameters));
		}

		protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
		{
			if (Equals(storage, value))
				return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		#endregion Protected Methods
	}
}
