namespace ProcessSourceFiles
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;

	public class FindSourceFilesWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public FindSourceFilesWindowView()
		{
			FileNames = new List<string>();
		}
		#endregion Public Constructors

		#region Private Methods
		private void Cancel(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("CloseWindow", new System.Collections.Generic.Dictionary<string, object> { { "result", false } }));
		}
		private void GetTopFolder(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("SelectTopFolder"));
		}
		private void OK(object state)
		{
			var dInfo = new DirectoryInfo(TopFolder);
			ProcessDirectoryInfo(dInfo);
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("CloseWindow", new System.Collections.Generic.Dictionary<string, object> { { "result", true } }));
		}
		private void ProcessDirectoryInfo(DirectoryInfo dInfo)
		{
			dInfo.GetFiles("*.cs").ToList().ForEach(fileInfo =>
			{
				FileNames.Add(fileInfo.FullName);
			});
			if (IncludeSubFolders)
			{
				dInfo.GetDirectories().ToList().ForEach(di => ProcessDirectoryInfo(di));
			}
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateGetTopFolderState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return !string.IsNullOrEmpty(TopFolder);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private List<string> _FileNames;
		private DelegateCommand _GetTopFolderCommand = null;
		private bool _IncludeSubFolders;
		private DelegateCommand _OKCommand = null;
		private string _TopFolder;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public List<string> FileNames
		{
			get
			{
				return _FileNames;
			}
			set
			{
				_FileNames = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileNames"));
			}
		}
		public DelegateCommand GetTopFolderCommand
		{
			get
			{
				if (_GetTopFolderCommand == null)
					_GetTopFolderCommand = new DelegateCommand(GetTopFolder, ValidateGetTopFolderState);
				return _GetTopFolderCommand as DelegateCommand;
			}
		}
		public bool IncludeSubFolders
		{
			get
			{
				return _IncludeSubFolders;
			}
			set
			{
				_IncludeSubFolders = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IncludeSubFolders"));
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
		public string TopFolder
		{
			get
			{
				return _TopFolder;
			}
			set
			{
				_TopFolder = value;
				OKCommand.RaiseCanExecuteChanged();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TopFolder"));
			}
		}
		#endregion Public Properties
	}
}
