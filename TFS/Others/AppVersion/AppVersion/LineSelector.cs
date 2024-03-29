namespace GregOsborne.AppVersion
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class LineSelector : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private bool _IsSelected;
		public bool IsSelected {
			get { return _IsSelected; }
			set {
				_IsSelected = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
			}
		}
		private string _Value;
		public string Value {
			get { return _Value; }
			set {
				_Value = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}
	}
}
