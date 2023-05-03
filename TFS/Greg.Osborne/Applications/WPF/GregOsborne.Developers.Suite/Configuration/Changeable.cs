using System.ComponentModel;
using System.Xml.Linq;

namespace GregOsborne.Developers.Suite.Configuration {
	public abstract class Changeable : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		protected void InvokePropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public abstract XElement ToXElement();
	}
}
