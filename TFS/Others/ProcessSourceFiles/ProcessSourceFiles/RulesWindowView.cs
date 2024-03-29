using System;
using System.ComponentModel;
namespace ProcessSourceFiles
{
	public class RulesWindowView : INotifyPropertyChanged
	{
		public RulesWindowView()
		{
			SectionName = "Settings\\Before Rules";
		}
		public event PropertyChangedEventHandler PropertyChanged;
		private string _SectionName;
		public string SectionName
		{
			get
			{
				return _SectionName;
			}
			set
			{
				_SectionName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SectionName"));
			}
		}
	}
}
