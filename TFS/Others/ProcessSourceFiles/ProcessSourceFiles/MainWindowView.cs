using MVVMFramework;
using GregOsborne.Application;
using ProcessSourceFiles.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;
namespace ProcessSourceFiles
{
	public class MainWindowView : INotifyPropertyChanged
	{
		public MainWindowView()
		{
			FileNames = new ObservableCollection<ProcessFile>();
			FileErrors = new ObservableCollection<string>();
			SectionName = "Settings";
			ProgressVisibility = Visibility.Collapsed;
			FileErrorVisibility = Visibility.Collapsed;
			if (App.ProcessingParameters == null)
				return;
			try
			{
				RemoveHtmlComments = App.ProcessingParameters.RemoveHtmlComments;
				RemoveRegions = App.ProcessingParameters.RemoveRegions;
				RemoveFullLineComments = App.ProcessingParameters.RemoveFullLineComments;
				RemoveConsecutiveBlankLines = App.ProcessingParameters.RemoveConsecutiveBlankLines;
				RemoveAllBlankLines = App.ProcessingParameters.RemoveAllBlankLines;
				LastFolder = Settings.GetValue<string>(App.ApplicationName, SectionName, "LastFolder", string.Empty);
				UsingPosition = App.ProcessingParameters.UsingPosition;
				InsideUsings = App.ProcessingParameters.UsingPosition == ProcessFile.UsingPositions.InsideNamespace;
				OutsideUsings = App.ProcessingParameters.UsingPosition == ProcessFile.UsingPositions.OutsideNamespace;
			}
			catch { }
			_InitializingView = false;
		}
		private DispatcherTimer errorClearTimer = null;
		private ObservableCollection<string> _FileErrors;
		public ObservableCollection<string> FileErrors
		{
			get { return _FileErrors; }
			set
			{
				_FileErrors = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileErrors"));
			}
		}
		private Visibility _FileErrorVisibility;
		public Visibility FileErrorVisibility
		{
			get { return _FileErrorVisibility; }
			set
			{
				_FileErrorVisibility = value;
				if (value == Visibility.Collapsed && FileErrors != null)
				{
					if (errorClearTimer != null)
					{
						errorClearTimer.Tick -= errorClearTimer_Tick;
						errorClearTimer = null;
					}
					FileErrors.Clear();
				}
				else
				{
					if (errorClearTimer == null)
					{
						errorClearTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
						errorClearTimer.Tick += errorClearTimer_Tick;
						errorClearTimer.Start();
					}
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileErrorVisibility"));
			}
		}
		void errorClearTimer_Tick(object sender, EventArgs e)
		{
			errorClearTimer.Stop();
			FileErrorVisibility = Visibility.Collapsed;
		}
		private void AddCsFile(string fileName)
		{
			var skipFile = false;
			ProcessFile.disallowedExtensions.ForEach(x => skipFile = skipFile || Path.GetFileName(fileName).EndsWith(x, StringComparison.OrdinalIgnoreCase));
			ProcessFile.disallowedFilePrefixes.ForEach(x => skipFile = skipFile || Path.GetFileName(fileName).StartsWith(x, StringComparison.OrdinalIgnoreCase));
			ProcessFile.disallowedFileNames.ForEach(x => skipFile = skipFile || Path.GetFileName(fileName).Equals(x, StringComparison.OrdinalIgnoreCase));
			if (skipFile)
			{
				FileErrors.Add(string.Format("File not allowed: {0}", fileName));
				return;
			}
			var f = new ProcessFile
			{
				FileName = System.IO.Path.GetFileName(fileName),
				FullPath = fileName,
				IsSelected = true
			};
			f.PropertyChanged += f_PropertyChanged;
			FileNames.Add(f);
			FileCount = FileNames.Select(x => x.IsSelected).Count();
			ProcessCommand.RaiseCanExecuteChanged();
			ClearFilesCommand.RaiseCanExecuteChanged();
			SelectAllCommand.RaiseCanExecuteChanged();
			SelectNoneCommand.RaiseCanExecuteChanged();
		}
		public void OpenSourceFile(string fileName)
		{
			var fInfo = new FileInfo(fileName);
			if (fInfo.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
			{
				AddCsFile(fileName);
			}
			else if (fInfo.Extension.Equals(".csproj", StringComparison.OrdinalIgnoreCase))
			{
				var relativePath = Path.GetDirectoryName(fileName);
				var doc = XDocument.Load(fileName);
				doc.Root.Elements().ToList().ForEach(itemGroupElement =>
				{
					if (itemGroupElement.Name.LocalName.Equals("itemgroup", StringComparison.OrdinalIgnoreCase))
					{
						itemGroupElement.Elements().ToList().ForEach(itemElement =>
						{
							if (itemElement.Name.LocalName.Equals("compile", StringComparison.OrdinalIgnoreCase))
							{
								var singleFileName = Path.Combine(relativePath, itemElement.Attribute("Include").Value);
								if (File.Exists(singleFileName))
									AddCsFile(singleFileName);
							}
						});
					}
				});
			}
		}
		private void ClearFiles(object state)
		{
			FileNames.Clear();
			SelectedFile = null;
			FileCount = FileNames.Count;
			ClearFilesCommand.RaiseCanExecuteChanged();
			SelectAllCommand.RaiseCanExecuteChanged();
			SelectNoneCommand.RaiseCanExecuteChanged();
		}
		private void Exit(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ExitApplication"));
		}
		private void f_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsSelected")
				FileCount = FileNames.Where(x => x.IsSelected).Count();
			else if (e.PropertyName == "ModifiedData" && SelectedFile != null)
				ModifiedTextData = SelectedFile.ModifiedData;
			ProcessCommand.RaiseCanExecuteChanged();
		}
		private void Find(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("DisplayFindWindow"));
		}
		private void OpenFile(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("OpenSourceFile"));
		}
		private void OverwriteAllFiles(object state)
		{
			if (!FileNames.Any())
				return;
			FileNames.Where(x => x.IsSelected && !string.IsNullOrEmpty(x.ModifiedData)).ToList().ForEach(x =>
			{
				x.Save();
			});
		}
		private void OverwriteAllFilesCommand_CheckAccess(object sender, CheckAccessEventArgs e)
		{
			if (CheckAccess != null)
				CheckAccess(this, e);
		}
		private void OverwriteFile(object state)
		{
			if (SelectedFile == null)
				return;
			SelectedFile.Save();
		}
		private void Process(object state)
		{
			var filesToProcess = FileNames.Where(x => x.IsSelected).ToList();
			ProgressVisibility = Visibility.Visible;
			ProgressValue = 0;
			ProgressMaximum = filesToProcess.Count;
			OverwriteAllFilesCommand.CheckAccess += OverwriteAllFilesCommand_CheckAccess;
			var t = new ProcessThread();
			t.Complete += t_Complete;
			t.ReportProgress += t_ReportProgress;
			var thread = new Thread(new ParameterizedThreadStart(t.Start));
			thread.Start(new ProcessParameters
			{
				Files = filesToProcess,
				RemoveAllBlankLines = RemoveAllBlankLines,
				RemoveConsecutiveBlankLines = RemoveConsecutiveBlankLines,
				RemoveFullLineComments = RemoveFullLineComments,
				RemoveHtmlComments = RemoveHtmlComments,
				RemoveRegions = RemoveRegions,
				UsingPosition = UsingPosition
			});
		}
		private void Rules(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowRulesWindow"));
		}
		private void SelectAll(object state)
		{
			FileNames.ToList().ForEach(x => x.IsSelected = true);
			ProcessCommand.RaiseCanExecuteChanged();
		}
		private void SelectNone(object state)
		{
			FileNames.ToList().ForEach(x => x.IsSelected = false);
			ProcessCommand.RaiseCanExecuteChanged();
		}
		private void t_Complete(object sender, EventArgs e)
		{
			if (CheckAccess != null)
			{
				var ex = new CheckAccessEventArgs();
				CheckAccess(this, ex);
				if (ex.HasAccess)
				{
					ProgressVisibility = Visibility.Collapsed;
					OverwriteAllFilesCommand.RaiseCanExecuteChanged();
					OverwriteAllFilesCommand.CheckAccess -= OverwriteAllFilesCommand_CheckAccess;
				}
				else
					ex.Dispatcher.BeginInvoke(new EventHandler(t_Complete), new object[] { sender, e });
			}
		}
		private void t_ReportProgress(object sender, ReportProgressEventArgs e)
		{
			if (CheckAccess != null)
			{
				var ex = new CheckAccessEventArgs();
				CheckAccess(this, ex);
				if (ex.HasAccess)
				{
					ProgressValue = e.Value;
					ProgressFileName = e.FileName;
				}
				else
					ex.Dispatcher.BeginInvoke(new ReportProgressEventHandler(t_ReportProgress), new object[] { sender, e });
			}
		}
		private bool ValidateClearFilesState(object state)
		{
			return FileNames.Any();
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		private bool ValidateFindState(object state)
		{
			return true;
		}
		private bool ValidateOpenFileState(object state)
		{
			return true;
		}
		private bool ValidateOverwriteAllFilesState(object state)
		{
			return FileNames.Any(x => !string.IsNullOrEmpty(x.ModifiedData));
		}
		private bool ValidateOverwriteFileState(object state)
		{
			return SelectedFile != null && !string.IsNullOrEmpty(SelectedFile.ModifiedData);
		}
		private bool ValidateProcessState(object state)
		{
			return FileNames.Any(x => x.IsSelected);
		}
		private bool ValidateRulesState(object state)
		{
			return true;
		}
		private bool ValidateSelectAllState(object state)
		{
			return FileNames.Any();
		}
		private bool ValidateSelectNoneState(object state)
		{
			return FileNames.Any();
		}
		public event CheckAccessEventHandler CheckAccess;
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		private DelegateCommand _ClearFilesCommand = null;
		private DelegateCommand _ExitCommand = null;
		private int _FileCount;
		private ObservableCollection<ProcessFile> _FileNames;
		private DelegateCommand _FindCommand = null;
		private bool _InitializingView = true;
		private bool _InsideUsings;
		private string _LastFolder;
		private string _ModifiedTextData;
		private DelegateCommand _OpenFileCommand = null;
		private bool _OutsideUsings;
		private DelegateCommand _OverwriteAllFilesCommand = null;
		private DelegateCommand _OverwriteFileCommand = null;
		private DelegateCommand _ProcessCommand = null;
		private string _ProgressFileName;
		private double _ProgressMaximum;
		private int _ProgressValue;
		private Visibility _ProgressVisibility;
		private DelegateCommand _RulesCommand = null;
		private string _SectionName;
		private DelegateCommand _SelectAllCommand = null;
		private ProcessFile _SelectedFile;
		private DelegateCommand _SelectNoneCommand = null;
		private string _TextData;
		private ProcessSourceFiles.Classes.ProcessFile.UsingPositions _UsingPosition;
		public DelegateCommand ClearFilesCommand
		{
			get
			{
				if (_ClearFilesCommand == null)
					_ClearFilesCommand = new DelegateCommand(ClearFiles, ValidateClearFilesState);
				return _ClearFilesCommand as DelegateCommand;
			}
		}
		public DelegateCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand as DelegateCommand;
			}
		}
		public int FileCount
		{
			get
			{
				return _FileCount;
			}
			set
			{
				_FileCount = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileCount"));
			}
		}
		public ObservableCollection<ProcessFile> FileNames
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
		public DelegateCommand FindCommand
		{
			get
			{
				if (_FindCommand == null)
					_FindCommand = new DelegateCommand(Find, ValidateFindState);
				return _FindCommand as DelegateCommand;
			}
		}
		public bool InsideUsings
		{
			get
			{
				return _InsideUsings;
			}
			set
			{
				_InsideUsings = value;
				UsingPosition = InsideUsings ? ProcessSourceFiles.Classes.ProcessFile.UsingPositions.InsideNamespace : ProcessFile.UsingPositions.OutsideNamespace;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("InsideUsings"));
			}
		}
		public string LastFolder
		{
			get
			{
				return _LastFolder;
			}
			set
			{
				_LastFolder = value;
				if (!_InitializingView)
					Settings.SetValue<string>(App.ApplicationName, SectionName, "LastFolder", value);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastFolder"));
			}
		}
		public string ModifiedTextData
		{
			get
			{
				return _ModifiedTextData;
			}
			set
			{
				_ModifiedTextData = value;
				OverwriteFileCommand.RaiseCanExecuteChanged();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ModifiedTextData"));
			}
		}
		public DelegateCommand OpenFileCommand
		{
			get
			{
				if (_OpenFileCommand == null)
					_OpenFileCommand = new DelegateCommand(OpenFile, ValidateOpenFileState);
				return _OpenFileCommand as DelegateCommand;
			}
		}
		public bool OutsideUsings
		{
			get
			{
				return _OutsideUsings;
			}
			set
			{
				_OutsideUsings = value;
				UsingPosition = InsideUsings ? ProcessSourceFiles.Classes.ProcessFile.UsingPositions.InsideNamespace : ProcessFile.UsingPositions.OutsideNamespace;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OutsideUsings"));
			}
		}
		public DelegateCommand OverwriteAllFilesCommand
		{
			get
			{
				if (_OverwriteAllFilesCommand == null)
					_OverwriteAllFilesCommand = new DelegateCommand(OverwriteAllFiles, ValidateOverwriteAllFilesState);
				return _OverwriteAllFilesCommand as DelegateCommand;
			}
		}
		public DelegateCommand OverwriteFileCommand
		{
			get
			{
				if (_OverwriteFileCommand == null)
					_OverwriteFileCommand = new DelegateCommand(OverwriteFile, ValidateOverwriteFileState);
				return _OverwriteFileCommand as DelegateCommand;
			}
		}
		public DelegateCommand ProcessCommand
		{
			get
			{
				if (_ProcessCommand == null)
					_ProcessCommand = new DelegateCommand(Process, ValidateProcessState);
				return _ProcessCommand as DelegateCommand;
			}
		}
		public string ProgressFileName
		{
			get
			{
				return _ProgressFileName;
			}
			set
			{
				_ProgressFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressFileName"));
			}
		}
		public double ProgressMaximum
		{
			get
			{
				return _ProgressMaximum;
			}
			set
			{
				_ProgressMaximum = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressMaximum"));
			}
		}
		public int ProgressValue
		{
			get
			{
				return _ProgressValue;
			}
			set
			{
				_ProgressValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressValue"));
			}
		}
		public Visibility ProgressVisibility
		{
			get
			{
				return _ProgressVisibility;
			}
			set
			{
				_ProgressVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressVisibility"));
			}
		}
		public bool RemoveAllBlankLines
		{
			get
			{
				return App.ProcessingParameters.RemoveAllBlankLines;
			}
			set
			{
				App.ProcessingParameters.RemoveAllBlankLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveAllBlankLines"));
			}
		}
		public bool RemoveConsecutiveBlankLines
		{
			get
			{
				return App.ProcessingParameters.RemoveConsecutiveBlankLines;
			}
			set
			{
				App.ProcessingParameters.RemoveConsecutiveBlankLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveConsecutiveBlankLines"));
			}
		}
		public bool RemoveFullLineComments
		{
			get
			{
				return App.ProcessingParameters.RemoveFullLineComments;
			}
			set
			{
				App.ProcessingParameters.RemoveFullLineComments = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveFullLineComments"));
			}
		}
		public bool RemoveHtmlComments
		{
			get
			{
				return App.ProcessingParameters.RemoveHtmlComments;
			}
			set
			{
				App.ProcessingParameters.RemoveHtmlComments = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveHtmlComments"));
			}
		}
		public bool RemoveRegions
		{
			get
			{
				return App.ProcessingParameters.RemoveRegions;
			}
			set
			{
				App.ProcessingParameters.RemoveRegions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RemoveRegions"));
			}
		}
		public DelegateCommand RulesCommand
		{
			get
			{
				if (_RulesCommand == null)
					_RulesCommand = new DelegateCommand(Rules, ValidateRulesState);
				return _RulesCommand as DelegateCommand;
			}
		}
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
		public DelegateCommand SelectAllCommand
		{
			get
			{
				if (_SelectAllCommand == null)
					_SelectAllCommand = new DelegateCommand(SelectAll, ValidateSelectAllState);
				return _SelectAllCommand as DelegateCommand;
			}
		}
		public ProcessFile SelectedFile
		{
			get
			{
				return _SelectedFile;
			}
			set
			{
				_SelectedFile = value;
				TextData = value == null ? string.Empty : value.Data;
				ModifiedTextData = value == null ? string.Empty : value.ModifiedData;
				OverwriteFileCommand.RaiseCanExecuteChanged();
				OverwriteAllFilesCommand.RaiseCanExecuteChanged();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedFile"));
			}
		}
		public DelegateCommand SelectNoneCommand
		{
			get
			{
				if (_SelectNoneCommand == null)
					_SelectNoneCommand = new DelegateCommand(SelectNone, ValidateSelectNoneState);
				return _SelectNoneCommand as DelegateCommand;
			}
		}
		public string TextData
		{
			get
			{
				return _TextData;
			}
			set
			{
				_TextData = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TextData"));
			}
		}
		public ProcessSourceFiles.Classes.ProcessFile.UsingPositions UsingPosition
		{
			get
			{
				return _UsingPosition;
			}
			set
			{
				_UsingPosition = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UsingPosition"));
			}
		}
	}
}
