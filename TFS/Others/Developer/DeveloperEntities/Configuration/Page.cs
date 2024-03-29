namespace SNC.OptiRamp.Application.DeveloperEntities.Configuration {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Page : ConfigBase {
		public Page() {
			Settings = new ObservableCollection<Setting>();
		}
		private ObservableCollection<Setting> _Settings;
		public ObservableCollection<Setting> Settings {
			get {
				return _Settings;
			}
			set {
				_Settings = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Settings"));
			}
		}
		public override event PropertyChangedEventHandler PropertyChanged;
	}
}
