using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVMFramework {
	public class DataItem : INotifyPropertyChanged {
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
