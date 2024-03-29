namespace XPadLib
{
	using GregOsborne.Application.Primitives;
	using System.ComponentModel;

	public class XPadItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private string _Value;
		private string _Name;
		public string Name
		{
			get { return _Name; }
			set
			{
				_Name = value;
				OnPropertyChanged(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName());
			}
		}
		public string Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				OnPropertyChanged(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName());
			}
		}
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
