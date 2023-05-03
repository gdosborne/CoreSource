using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using GregOsborne.MVVMFramework;

namespace FormatCodeFile
{
	public class SettingsWindowView : INotifyPropertyChanged
	{
		private ICommand _CancelCommand = null;
		private bool _FormatCodeFile;
		private ICommand _OKCommand = null;
		private bool _RemoveBlankLines;
		private bool _RemoveComments;
		private bool _RemoveRegions;
		private bool? _Result;
		private bool _SaveAllVersions;
		private bool _SaveUnmodifiedCode;
		public event PropertyChangedEventHandler PropertyChanged;
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public bool FormatCodeFile
		{
			get { return _FormatCodeFile; }
			set
			{
				_FormatCodeFile = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FormatCodeFile"));
			}
		}
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public bool RemoveBlankLines
		{
			get { return _RemoveBlankLines; }
			set
			{
				_RemoveBlankLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveBlankLines"));
			}
		}
		public bool RemoveComments
		{
			get { return _RemoveComments; }
			set
			{
				_RemoveComments = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveComments"));
			}
		}
		public bool RemoveRegions
		{
			get { return _RemoveRegions; }
			set
			{
				_RemoveRegions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveRegions"));
			}
		}
		public bool? Result
		{
			get { return _Result; }
			set
			{
				_Result = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Result"));
			}
		}
		public bool SaveAllVersions
		{
			get { return _SaveAllVersions; }
			set
			{
				_SaveAllVersions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SaveAllVersions"));
			}
		}
		public bool SaveUnmodifiedCode
		{
			get { return _SaveUnmodifiedCode; }
			set
			{
				_SaveUnmodifiedCode = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SaveUnmodifiedCode"));
			}
		}
		private void Cancel(object state)
		{
			Result = false;
		}
		private void OK(object state)
		{
			Result = true;
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return true;
		}
	}
}
