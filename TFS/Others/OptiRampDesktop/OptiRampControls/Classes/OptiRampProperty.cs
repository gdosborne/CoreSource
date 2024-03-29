using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OptiRampControls.Classes
{
	public class OptiRampProperty : INotifyPropertyChanged
	{
		#region Private Fields
		private bool _IsChangable;
		private string _Name;
		private object _Value;
		#endregion

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public bool IsChangable
		{
			get { return _IsChangable; }
			set
			{
				_IsChangable = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsChangable"));
			}
		}
		public string Name
		{
			get { return _Name; }
			set
			{
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		public object Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}
		#endregion
	}
}