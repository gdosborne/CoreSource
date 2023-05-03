namespace GregOsborne.MVVMFramework {
	using System.ComponentModel;

	public class NotifyClassBase : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		protected void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
