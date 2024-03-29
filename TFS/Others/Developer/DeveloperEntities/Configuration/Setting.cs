namespace SNC.OptiRamp.Application.DeveloperEntities.Configuration {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Setting : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		private Type _SettingType;
		public Type SettingType {
			get {
				return _SettingType;
			}
			set {
				_SettingType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SettingType"));
			}
		}
		private object _Value;
		public object Value {
			get {
				return _Value;
			}
			set {
				_Value = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}
		private string _Name;
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
	}
}
