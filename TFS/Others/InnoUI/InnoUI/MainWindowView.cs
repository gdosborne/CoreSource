namespace InnoUI
{
	using GregOsborne.Application.Primitives;
	using InnoUI.Properties;
	using InnoUILib;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Threading;

	public class MainWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			if (Settings.Default.UpdateRequired)
			{
				Settings.Default.Upgrade();
				Settings.Default.UpdateRequired = false;
				Settings.Default.Save();
			}

			SaveCommand.CheckAccess += command_CheckAccess;
			SaveAsCommand.CheckAccess += command_CheckAccess;
			NewCommand.CheckAccess += command_CheckAccess;
			OpenCommand.CheckAccess += command_CheckAccess;
			ExitCommand.CheckAccess += command_CheckAccess;

			StatusFullNameVisibility = Settings.Default.ShowFullPathInStatusBar ? Visibility.Visible : Visibility.Collapsed;
			StatusNameVisibility = !Settings.Default.ShowFullPathInStatusBar ? Visibility.Visible : Visibility.Collapsed;
			DataVisibility = Visibility.Collapsed;
			ChangeIndicatorVisibility = Visibility.Hidden;
			FontToolbarVisibility = Visibility.Collapsed;

			DirtyIndicatorBrush = GreenBrush;
			DirtyIndicatorToolTip = "File has not changed";

			FontFamilies = new ObservableCollection<FontFamily>();
			foreach (var f in System.Windows.Media.Fonts.SystemFontFamilies.OrderBy(x => x.Source))
			{
				FontFamilies.Add(f);
			}

			SelectedFontFamily = FontFamilies.FirstOrDefault(x => x.Source.Equals("Lucida Console"));
			FontSize = 11;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(Window window)
		{
			window.Left = Settings.Default.Position.X;
			window.Top = Settings.Default.Position.Y;
			window.Width = Settings.Default.Size.Width;
			window.Height = Settings.Default.Size.Height;
			window.WindowState = Settings.Default.State;

			IsShowScriptChecked = false;

		}
		public void InitView()
		{
		}
		public void Persist(Window window)
		{
			Settings.Default.Position = new System.Drawing.Point(Convert.ToInt32(window.RestoreBounds.X), Convert.ToInt32(window.RestoreBounds.Y));
			Settings.Default.Size = new System.Drawing.Size(Convert.ToInt32(window.RestoreBounds.Width), Convert.ToInt32(window.RestoreBounds.Height));
			Settings.Default.State = window.WindowState;
			Settings.Default.Save();
		}
		public void UpdateInterface()
		{
			NewCommand.RaiseCanExecuteChanged();
			OpenCommand.RaiseCanExecuteChanged();
			CloseCommand.RaiseCanExecuteChanged();
			SaveCommand.RaiseCanExecuteChanged();
			SaveAsCommand.RaiseCanExecuteChanged();
			ExitCommand.RaiseCanExecuteChanged();

			DirtyIndicatorBrush = File != null && File.IsDirty ? RedBrush : GreenBrush;
			DirtyIndicatorToolTip = File != null && File.IsDirty ? "File has changed" : "File has not changed";
			DataVisibility = File != null ? Visibility.Visible : Visibility.Collapsed;
			ChangeIndicatorVisibility = File != null ? Visibility.Visible : Visibility.Hidden;
		}
		#endregion Public Methods

		#region Private Methods
		private bool AskSave()
		{
			if (File != null && File.IsDirty)
			{
				if (ExecuteUIAction != null)
				{
					var parameters = new Dictionary<string, object>
					{
						{ "result", true }
					};
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("AskSave", parameters));
					if (!(bool)parameters["result"])
						return true;
					if (!File.Save())
						return true;
				}
			}
			return false;
		}
		private void Close(object state)
		{
			File = null;
		}
		private void command_CheckAccess(object sender, CheckAccessEventArgs e)
		{
			e.HasAccess = Dispatcher.CurrentDispatcher.CheckAccess();
		}
		private void Exit(object state)
		{
			if (!AskSave())
				Application.Current.Shutdown(0);
		}
		private void File_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			UpdateInterface();
		}
		private void New(object state)
		{
		}
		private ObservableCollection<InnoLine> _DefineLines;
		public ObservableCollection<InnoLine> DefineLines
		{
			get { return _DefineLines; }
			set
			{
				_DefineLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private void Open(object state)
		{
			if (!AskSave())
			{
				if (ExecuteUIAction != null)
				{
					var parameters = new Dictionary<string, object>
					{
						{ "cancel", true },
						{ "filename", string.Empty },
						{ "width", 0.0 }
					};
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("OpenFile", parameters));
					if ((bool)parameters["cancel"])
						return;
					FontToolbarVisibility = Visibility.Visible;
					File = new InnoFile((string)parameters["filename"], (double)parameters["width"]);
					//File.FontFamily = SelectedFontFamily;
					//File.FontSize = FontSize;
					DataVisibility = Visibility.Visible;

					DefineLines = new ObservableCollection<InnoLine>(File.Lines.Where(x => x.Is<DefineLine>()));

					File.IsDirty = false;
				}
			}
		}
		private void Save(object state)
		{
		}
		private void SaveAs(object state)
		{
		}
		private bool ValidateCloseState(object state)
		{
			return File != null;
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		private bool ValidateNewState(object state)
		{
			return true;
		}
		private bool ValidateOpenState(object state)
		{
			return true;
		}
		private bool ValidateSaveAsState(object state)
		{
			return File != null;
		}
		private bool ValidateSaveState(object state)
		{
			return File != null && File.IsDirty;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Visibility _ChangeIndicatorVisibility;
		private DelegateCommand _CloseCommand = null;
		private Visibility _DataVisibility;
		private bool? _DialogResult;
		private Brush _DirtyIndicatorBrush;
		private string _DirtyIndicatorToolTip;
		private DelegateCommand _ExitCommand = null;
		private InnoFile _File;
		private ObservableCollection<FontFamily> _FontFamilies;
		private double _FontSize;
		private Visibility _FontToolbarVisibility;
		private bool _IsShowScriptChecked;
		private DelegateCommand _NewCommand = null;
		private DelegateCommand _OpenCommand = null;
		private DelegateCommand _SaveAsCommand = null;
		private DelegateCommand _SaveCommand = null;
		private FontFamily _SelectedFontFamily;
		private Visibility _StatusFullNameVisibility;
		private Visibility _StatusNameVisibility;
		private Brush GreenBrush = new SolidColorBrush(Color.FromArgb(255, 6, 191, 6));
		private Brush RedBrush = new SolidColorBrush(Color.FromArgb(255, 191, 6, 6));
		#endregion Private Fields
		private Visibility _ScriptVisibility;
		public Visibility ScriptVisibility
		{
			get { return _ScriptVisibility; }
			set
			{
				_ScriptVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#region Public Properties
		public Visibility ChangeIndicatorVisibility
		{
			get
			{
				return _ChangeIndicatorVisibility;
			}
			set
			{
				_ChangeIndicatorVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand CloseCommand
		{
			get
			{
				if (_CloseCommand == null)
					_CloseCommand = new DelegateCommand(Close, ValidateCloseState);
				return _CloseCommand as DelegateCommand;
			}
		}
		public Visibility DataVisibility
		{
			get
			{
				return _DataVisibility;
			}
			set
			{
				_DataVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Brush DirtyIndicatorBrush
		{
			get
			{
				return _DirtyIndicatorBrush;
			}
			set
			{
				_DirtyIndicatorBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string DirtyIndicatorToolTip
		{
			get
			{
				return _DirtyIndicatorToolTip;
			}
			set
			{
				_DirtyIndicatorToolTip = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
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
		public InnoFile File
		{
			get
			{
				return _File;
			}
			set
			{
				if (!AskSave())
				{
				}
				if (File != null)
					File.PropertyChanged -= File_PropertyChanged;
				_File = value;
				if (File != null)
					File.PropertyChanged += File_PropertyChanged;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<FontFamily> FontFamilies
		{
			get
			{
				return _FontFamilies;
			}
			set
			{
				_FontFamilies = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double FontSize
		{
			get
			{
				return _FontSize;
			}
			set
			{
				_FontSize = Convert.ToDouble(Convert.ToInt32(value));
				//if (File != null)
				//	File.FontSize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility FontToolbarVisibility
		{
			get
			{
				return _FontToolbarVisibility;
			}
			set
			{
				_FontToolbarVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsShowScriptChecked
		{
			get
			{
				return _IsShowScriptChecked;
			}
			set
			{
				_IsShowScriptChecked = value;
				ScriptVisibility = value ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string LastOpenPath
		{
			get
			{
				return Settings.Default.LastOpenPath;
			}
			set
			{
				Settings.Default.LastOpenPath = value;
				Settings.Default.Save();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand NewCommand
		{
			get
			{
				if (_NewCommand == null)
					_NewCommand = new DelegateCommand(New, ValidateNewState);
				return _NewCommand as DelegateCommand;
			}
		}
		public DelegateCommand OpenCommand
		{
			get
			{
				if (_OpenCommand == null)
					_OpenCommand = new DelegateCommand(Open, ValidateOpenState);
				return _OpenCommand as DelegateCommand;
			}
		}
		public DelegateCommand SaveAsCommand
		{
			get
			{
				if (_SaveAsCommand == null)
					_SaveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState);
				return _SaveAsCommand as DelegateCommand;
			}
		}
		public DelegateCommand SaveCommand
		{
			get
			{
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}
		public FontFamily SelectedFontFamily
		{
			get
			{
				return _SelectedFontFamily;
			}
			set
			{
				_SelectedFontFamily = value;
				//if (File != null)
				//	File.FontFamily = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility StatusFullNameVisibility
		{
			get
			{
				return _StatusFullNameVisibility;
			}
			set
			{
				_StatusFullNameVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility StatusNameVisibility
		{
			get
			{
				return _StatusNameVisibility;
			}
			set
			{
				_StatusNameVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties
	}
}
