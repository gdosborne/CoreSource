namespace SNC.OptiRamp.Application.DeveloperEntities.Configuration {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public abstract class ConfigBase  :INotifyPropertyChanged {
		private string _Title;
		public string Title {
			get {
				return _Title;
			}
			set {
				_Title = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}

		public virtual event PropertyChangedEventHandler PropertyChanged;
	}
}
