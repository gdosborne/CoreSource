using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GregOsborne.MVVMFramework;
namespace FormatCodeFile
{
	public class MainWindowView : INotifyPropertyChanged
	{
		private IEnumerable<Classes.FileSystemBase> _Folders;
		private bool _FormatCodeFile;
		private bool _RemoveBlankLines;
		private bool _RemoveComments;
		private bool _RemoveRegions;
		private bool _SaveAllVersions;
		private bool _SaveUnmodifiedCode;
		private string _SelectedFolder;
		private ICommand _SelectFolderCommand = null;
		private ICommand _SettingsCommand = null;
		public MainWindowView()
		{
			RemoveBlankLines = Properties.Settings.Default.RemoveBlankLines;
			RemoveComments = Properties.Settings.Default.RemoveComments;
			FormatCodeFile = Properties.Settings.Default.FormatCodeFile;
			RemoveRegions = Properties.Settings.Default.RemoveRegions;
			SaveUnmodifiedCode = Properties.Settings.Default.SaveUnmodifiedCode;
			SaveAllVersions = Properties.Settings.Default.SaveAllVersions;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler SelectBaseFolder;
		public event EventHandler ShowSettings;
		public IEnumerable<Classes.FileSystemBase> Folders
		{
			get { return _Folders; }
			set
			{
				_Folders = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Folders"));
			}
		}
		public bool FormatCodeFile
		{
			get { return _FormatCodeFile; }
			set
			{
				_FormatCodeFile = value;
				Properties.Settings.Default.FormatCodeFile = value;
				Properties.Settings.Default.Save();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FormatCodeFile"));
			}
		}
		public bool RemoveBlankLines
		{
			get { return _RemoveBlankLines; }
			set
			{
				_RemoveBlankLines = value;
				Properties.Settings.Default.RemoveBlankLines = value;
				Properties.Settings.Default.Save();
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
				Properties.Settings.Default.RemoveComments = value;
				Properties.Settings.Default.Save();
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
				Properties.Settings.Default.RemoveRegions = value;
				Properties.Settings.Default.Save();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveRegions"));
			}
		}
		public bool SaveAllVersions
		{
			get { return _SaveAllVersions; }
			set
			{
				_SaveAllVersions = value;
				Properties.Settings.Default.SaveAllVersions = value;
				Properties.Settings.Default.Save();
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
				Properties.Settings.Default.SaveUnmodifiedCode = value;
				Properties.Settings.Default.Save();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SaveUnmodifiedCode"));
			}
		}
		public string SelectedFolder
		{
			get { return _SelectedFolder; }
			set
			{
				_SelectedFolder = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedFolder"));
				Properties.Settings.Default.LastDirectory = value;
				var folders = new List<Classes.FileSystemBase>();
				var topFolder = new DirectoryInfo(value);
				var folder = new Classes.Folder(topFolder.Name, topFolder.FullName);
				LoadSubFolders(topFolder, ref folder);
				folders.Add(folder);
				Folders = folders;
			}
		}
		private DelegateCommand _ExitCommand = null;
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
			App.Current.Shutdown();
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}

		public DelegateCommand SelectFolderCommand
		{
			get
			{
				if (_SelectFolderCommand == null)
					_SelectFolderCommand = new DelegateCommand(SelectFolder, ValidateSelectFolderState);
				return _SelectFolderCommand as DelegateCommand;
			}
		}
		public DelegateCommand SettingsCommand
		{
			get
			{
				if (_SettingsCommand == null)
					_SettingsCommand = new DelegateCommand(Settings, ValidateSettingsState);
				return _SettingsCommand as DelegateCommand;
			}
		}
		private void LoadSubFolders(DirectoryInfo dInfo, ref Classes.Folder parent)
		{
			if (dInfo == null || parent == null)
				return;
			const string csFiles = "*.cs";
			const string ignore1 = ".g.cs";
			const string ignore2 = ".g.i.cs";
			if (dInfo.GetFiles(csFiles).Any())
			{
				foreach (var x in dInfo.GetFiles(csFiles))
				{
					if (x.Name.EndsWith(ignore1, StringComparison.OrdinalIgnoreCase) || x.Name.EndsWith(ignore2, StringComparison.OrdinalIgnoreCase))
						continue;
					(parent.Files as List<Classes.FileSystemBase>).Add(new Classes.File(x.Name, x.FullName));
				}
			}
			if (dInfo.GetDirectories().Any())
			{
				foreach (var x in dInfo.GetDirectories())
				{
					var folder = new Classes.Folder(x.Name, x.FullName);
					LoadSubFolders(x, ref folder);
					(parent.Folders as List<Classes.FileSystemBase>).Add(folder);
				}
			}
		}
		private void SelectFolder(object state)
		{
			if (SelectBaseFolder != null)
				SelectBaseFolder(this, EventArgs.Empty);
		}
		private void Settings(object state)
		{
			if (ShowSettings != null)
				ShowSettings(this, EventArgs.Empty);
		}
		private bool ValidateSelectFolderState(object state)
		{
			return true;
		}
		private bool ValidateSettingsState(object state)
		{
			return true;
		}
	}
}
