using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GregOsborne.MVVMFramework;

namespace WpfApplication8
{
	public class MainWindowView:INotifyPropertyChanged
	{
		public MainWindowView()
		{
			SomeData = new List<string>
			{
				"Value 1",
				"Value 2",
				"Value 3"
			};
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler ExitApp;
		private ICommand _ExitCommand = null;
		public DelegateCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand as DelegateCommand;
			}
		}
		private void Exit(object state)
		{
			if (ExitApp != null)
				ExitApp(this, EventArgs.Empty);
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		private List<string> _SomeData;
		public List<string> SomeData
		{
			get { return _SomeData; }
			set
			{
				_SomeData = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SomeData"));
			}
		}
		private string _SelectedData;
		public string SelectedData
		{
			get { return _SelectedData; }
			set
			{
				_SelectedData = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedData"));
			}
		}
	}
}
