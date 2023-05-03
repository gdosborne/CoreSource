namespace SDFManager
{
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using SDFManagerSupport;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Ribbon;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public class MainWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			ChangeIndicatorVisibility = Visibility.Hidden;
			RecentItems = RecentFilesManager.GetMenuItems(RecentItemCommand);
			CurrentScale = Settings.GetValue<double>(App.ApplicationName, "Settings", "Scale", 1.0);
		}
		#endregion Public Constructors

		#region Public Methods
		public bool? AskSave()
		{
			bool? result = null;
			if (ExecuteAction != null)
			{
				var buttons = new GregOsborne.Dialog.ButtonTypes[] { GregOsborne.Dialog.ButtonTypes.Yes, GregOsborne.Dialog.ButtonTypes.No, GregOsborne.Dialog.ButtonTypes.Cancel };
				var p = new Dictionary<string, object>
				{
					{ "additionalinfo",  Definition.FileName },
					{ "imagetype", GregOsborne.Dialog.ImagesTypes.Question },
					{ "text", "The definition file has changed.\n\nWould you like to save the file?" },
					{ "title", "Database definition changed" },
					{ "result", false },
					{ "buttons", buttons }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("ShowMessage", p));
				result = (bool?)p["result"];
			}
			return result;
		}
		public void CreateNewDatabase(string fileName)
		{
			Definition = new DatabaseDefinition(System.IO.Path.GetFileName(fileName), null);
			Definition.Save(fileName);
			RecentFilesManager.AddItem(fileName);
			RecentItems = RecentFilesManager.GetMenuItems(RecentItemCommand);
		}
		public void Initialize(Window window)
		{
			window.Left = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Left", window.Left);
			window.Top = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Top", window.Top);
			window.Width = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Width", window.Width);
			window.Height = Settings.GetValue<double>(App.ApplicationName, "MainWindow", "Height", window.Height);
			window.WindowState = Settings.GetValue<WindowState>(App.ApplicationName, "MainWindow", "State", window.WindowState);
		}
		public void InitView()
		{
			LastFolder = Settings.GetValue<string>(App.ApplicationName, "Settings", "Last Folder", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
			UpdateInterface();
		}
		public void OpenDatabase(string fileName)
		{
			if (Definition != null && Definition.IsChanged)
			{
				var askResult = AskSave();
				switch (askResult)
				{
					case true:
						Save(null);
						break;
					case null:
						return;
				}
			}
			try
			{
				Definition = DatabaseDefinition.Load(fileName);
				RefreshDiagram();
			}
			catch (Exception ex)
			{
			}
		}
		public void Persist(Window window)
		{
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Left", window.RestoreBounds.Left);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Top", window.RestoreBounds.Top);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Width", window.RestoreBounds.Width);
			Settings.SetValue<double>(App.ApplicationName, "MainWindow", "Height", window.RestoreBounds.Height);
			Settings.SetValue<WindowState>(App.ApplicationName, "MainWindow", "State", window.WindowState);
		}
		public void RefreshDiagram()
		{
			if (Definition == null)
				return;
			if (ExecuteAction != null)
				ExecuteAction(this, new ExecuteUIActionEventArgs("ClearCanvas"));
			var temp = new List<TableDefinitionItem>();
			foreach (var t in Definition.Tables)
			{
				var tdi = new TableDefinitionItem
				{
					Title = t.Name,
					Definition = t
				};
				tdi.PreviewMouseLeftButtonDown += tdi_PreviewMouseLeftButtonDown;
				tdi.PreviewMouseDoubleClick += tdi_PreviewMouseDoubleClick;
				if (AddControl != null)
					AddControl(this, new AddControlEventArgs(tdi, new Point(t.LeftPosition, t.TopPosition)));
				temp.Add(tdi);
			}
			var foundKey = false;
			foreach (var t in temp)
			{
				foundKey = false;
				foreach (var f in t.Definition.Fields)
				{
					if (f.ForeignKey != null)
					{
						foreach (var t1 in temp)
						{
							if (f.ForeignKey.TableName.Equals(t1.Definition.Name))
							{
								t.ForeignKeyItem = t1;
								t1.PrimaryKeyItem = t;
								foundKey = true;
								break;
							}
						}
						if (foundKey)
							break;
					}
				}
			}
			foreach (var t in temp.Where(x => x.ForeignKeyItem != null))
			{
				var p = new Polyline
				{
					Stroke = Application.Current.Resources["ForeignKey"].As<SolidColorBrush>(),
					StrokeThickness = 3
				};
				var f = t.Definition.Fields.FirstOrDefault(k => k.ForeignKey != null);
				if (f != null)
					p.ToolTip = string.Format("{0}=>{1}.{2}", f.Name, f.ForeignKey.TableName, f.ForeignKey.FieldName);

				var x = (double)t.GetValue(Canvas.LeftProperty) + t.ActualWidth - 5;
				var y = (double)t.GetValue(Canvas.TopProperty) + (t.ActualHeight / 2);
				var p1 = new Point(0, 0);

				p.Points = new PointCollection
				{
					new Point(x, y),
					new Point(x + 20, y),
					new Point((double)t.ForeignKeyItem.GetValue(Canvas.LeftProperty) - 20, (double)t.ForeignKeyItem.GetValue(Canvas.TopProperty) + t.ForeignKeyItem.ActualHeight / 2),
					new Point((double)t.ForeignKeyItem.GetValue(Canvas.LeftProperty) + 5, (double)t.ForeignKeyItem.GetValue(Canvas.TopProperty) + t.ForeignKeyItem.ActualHeight / 2)
				};
				t.ConnectingLine = p;
				if (AddControl != null)
					AddControl(this, new AddControlEventArgs(p, p1));
			}
		}

		public void Save(object state)
		{
			if (Definition != null)
				Definition.Save(Definition.FileName);
		}
		public void UpdateInterface()
		{
			NewCommand.RaiseCanExecuteChanged();
			OpenCommand.RaiseCanExecuteChanged();
			CloseCommand.RaiseCanExecuteChanged();
			SaveCommand.RaiseCanExecuteChanged();
			ExitCommand.RaiseCanExecuteChanged();
			AddTableCommand.RaiseCanExecuteChanged();
			AboutCommand.RaiseCanExecuteChanged();
			GenerateDatabaseCommand.RaiseCanExecuteChanged();
			OptionsCommand.RaiseCanExecuteChanged();

			ChangeIndicatorVisibility = Definition != null && Definition.IsChanged ? Visibility.Visible : Visibility.Hidden;
		}
		#endregion Public Methods

		#region Private Methods
		private void _Definition_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			UpdateInterface();
		}
		private void About(object state)
		{
		}
		private void AddTable(object state)
		{
			if (ExecuteAction != null)
			{
				var p = new Dictionary<string, object>
				{
					{ "result", false },
					{ "definition" , null }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("ShowAddTable", p));
				var result = (bool?)p["result"];
			}
		}
		private void Close(object state)
		{
			if (Definition.IsChanged)
			{
				switch (AskSave())
				{
					case true:
						Save(null);
						break;
					case null:
						//cancel
						return;
				}
			}
			Definition = null;
		}
		private void DeleteTable(object state)
		{
		}
		private void Exit(object state)
		{
			Application.Current.Shutdown(0);
		}
		private void GenerateDatabase(object state)
		{
			if (Definition != null)
			{
				var dbFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Definition.FileName), System.IO.Path.GetFileNameWithoutExtension(Definition.FileName) + ".sdf");
				SDFDatabase.Create(dbFileName, Definition);
				bool? result = null;
				if (ExecuteAction != null)
				{
					var buttons = new GregOsborne.Dialog.ButtonTypes[] { GregOsborne.Dialog.ButtonTypes.OK };
					var p = new Dictionary<string, object>
					{
						{ "additionalinfo",  null },
						{ "imagetype", GregOsborne.Dialog.ImagesTypes.Information },
						{ "text", string.Format("The database file {0} has been created.", dbFileName) },
						{ "title", "Database created" },
						{ "result", false },
						{ "buttons", buttons }
					};
					ExecuteAction(this, new ExecuteUIActionEventArgs("ShowMessage", p));
					result = (bool?)p["result"];
				}
				//return result;
			}
		}
		private void New(object state)
		{
			if (ExecuteAction != null)
			{
				var p = new Dictionary<string, object>
				{
					{ "result", false },
					{ "filename", string.Empty },
					{ "initialdirectory", LastFolder }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("SaveAsDefinitionFile", p));
				if (!((bool)p["result"]))
					return;
				LastFolder = System.IO.Path.GetDirectoryName((string)p["filename"]);
				CreateNewDatabase((string)p["filename"]);
			}
		}
		private void Open(object state)
		{
			if (ExecuteAction != null)
			{
				var p = new Dictionary<string, object>
				{
					{ "result", false },
					{ "filename", string.Empty },
					{ "initialdirectory", LastFolder }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("OpenDefinitionFile", p));
				if (!((bool)p["result"]))
					return;
				LastFolder = System.IO.Path.GetDirectoryName((string)p["filename"]);
				if (System.IO.File.Exists((string)p["filename"]))
					OpenFile((string)p["filename"]);
			}
		}
		private void OpenFile(string fileName)
		{
			OpenDatabase(fileName);
			RecentFilesManager.AddItem(fileName);
			RecentItems = RecentFilesManager.GetMenuItems(RecentItemCommand);
		}
		private void Options(object state)
		{
			if (ExecuteAction != null)
			{
				var p = new Dictionary<string, object> { };
				ExecuteAction(this, new ExecuteUIActionEventArgs("ShowOptionsDialog", p));
			}
		}
		private void RecentItem(object state)
		{
			if (Definition != null && Definition.IsChanged)
			{
				switch (AskSave())
				{
					case true:
						Save(null);
						break;
					case null:
						return;
				}
			}
			Definition = null;
			OpenFile((string)state);
		}
		private void tdi_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (ExecuteAction != null)
			{
				e.Handled = true;
				var p = new Dictionary<string, object>
				{
					{ "result", false },
					{ "definition" , sender.As<TableDefinitionItem>().Definition }
				};
				ExecuteAction(this, new ExecuteUIActionEventArgs("ShowAddTable", p));
				var result = (bool?)p["result"];
			}
		}
		private void tdi_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			sender.As<TableDefinitionItem>().SelectionRectangleVisibility = Visibility.Visible;
			SelectedTableDefinition = sender.As<TableDefinitionItem>().Definition;
			//e.Handled = true;
		}
		private bool ValidateAboutState(object state)
		{
			return true;
		}
		private bool ValidateAddTableState(object state)
		{
			return Definition != null;
		}
		private bool ValidateCloseState(object state)
		{
			return Definition != null;
		}
		private bool ValidateDeleteTableState(object state)
		{
			return SelectedTableDefinition != null;
		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		private bool ValidateGenerateDatabaseState(object state)
		{
			return Definition != null;
		}
		private bool ValidateNewState(object state)
		{
			return true;
		}
		private bool ValidateOpenState(object state)
		{
			return true;
		}
		private bool ValidateOptionsState(object state)
		{
			return true;
		}
		private bool ValidateRecentItemState(object state)
		{
			return true;
		}
		private bool ValidateSaveState(object state)
		{
			return Definition != null && Definition.IsChanged;
		}
		#endregion Private Methods

		#region Public Events
		public event AddControlHandler AddControl;
		public event ClearCanvasHandler ClearCanvas;
		public event ExecuteUIActionHandler ExecuteAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _AboutCommand = null;
		private DelegateCommand _AddTableCommand = null;
		private Visibility _ChangeIndicatorVisibility;
		private DelegateCommand _CloseCommand = null;
		private double _CurrentScale;
		private DatabaseDefinition _Definition;
		private DelegateCommand _DeleteTableCommand = null;
		private DelegateCommand _ExitCommand = null;
		private DelegateCommand _GenerateDatabaseCommand = null;
		private string _LastFolder;
		private DelegateCommand _NewCommand = null;
		private DelegateCommand _OpenCommand = null;
		private DelegateCommand _OptionsCommand = null;
		private DelegateCommand _RecentItemCommand = null;
		private List<RibbonApplicationMenuItem> _RecentItems;
		private DelegateCommand _SaveCommand = null;
		private TableDefinition _SelectedTableDefinition;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand AboutCommand
		{
			get
			{
				if (_AboutCommand == null)
					_AboutCommand = new DelegateCommand(About, ValidateAboutState);
				return _AboutCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddTableCommand
		{
			get
			{
				if (_AddTableCommand == null)
					_AddTableCommand = new DelegateCommand(AddTable, ValidateAddTableState);
				return _AddTableCommand as DelegateCommand;
			}
		}
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
		public double CurrentScale
		{
			get
			{
				return _CurrentScale;
			}
			set
			{
				_CurrentScale = value;
				Settings.SetValue<double>(App.ApplicationName, "Settings", "Scale", value);
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DatabaseDefinition Definition
		{
			get
			{
				return _Definition;
			}
			set
			{
				if (value == null && ClearCanvas != null)
					ClearCanvas(this, new ClearCanvasEventArgs());
				if (value != null)
					value.PropertyChanged += _Definition_PropertyChanged;
				else if (_Definition != null)
					_Definition.PropertyChanged -= _Definition_PropertyChanged;
				_Definition = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand DeleteTableCommand
		{
			get
			{
				if (_DeleteTableCommand == null)
					_DeleteTableCommand = new DelegateCommand(DeleteTable, ValidateDeleteTableState);
				return _DeleteTableCommand as DelegateCommand;
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
		public DelegateCommand GenerateDatabaseCommand
		{
			get
			{
				if (_GenerateDatabaseCommand == null)
					_GenerateDatabaseCommand = new DelegateCommand(GenerateDatabase, ValidateGenerateDatabaseState);
				return _GenerateDatabaseCommand as DelegateCommand;
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
				Settings.SetValue<string>(App.ApplicationName, "Settings", "Last Folder", value);
				UpdateInterface();
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
		public DelegateCommand OptionsCommand
		{
			get
			{
				if (_OptionsCommand == null)
					_OptionsCommand = new DelegateCommand(Options, ValidateOptionsState);
				return _OptionsCommand as DelegateCommand;
			}
		}
		public DelegateCommand RecentItemCommand
		{
			get
			{
				if (_RecentItemCommand == null)
					_RecentItemCommand = new DelegateCommand(RecentItem, ValidateRecentItemState);
				return _RecentItemCommand as DelegateCommand;
			}
		}
		public List<RibbonApplicationMenuItem> RecentItems
		{
			get
			{
				return _RecentItems;
			}
			set
			{
				_RecentItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
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
		public TableDefinition SelectedTableDefinition
		{
			get
			{
				return _SelectedTableDefinition;
			}
			set
			{
				_SelectedTableDefinition = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties
	}
}
