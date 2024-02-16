namespace OSInstallerBuilder.Windows
{
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerBuilder.Classes.Options;
	using OSInstallerCommands.Classes.Events;
	using OSInstallerCommands.Zip;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using SysIO=System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using GregOsborne.Application.IO;
	using System.Diagnostics;

	public class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			BuildProgressVisibility = Visibility.Collapsed;
			OptionCategories = new OptionList();

			OptionCategories.Add(GetGeneralCategory());
			OptionCategories.Add(GetExtensionsCategory());
		}
		#endregion Public Constructors

		#region Public Methods
		public override void UpdateInterface()
		{
			TabControlEnabled = Manager != null;
			SaveFileCommand.RaiseCanExecuteChanged();
			BuildCommand.RaiseCanExecuteChanged();
			AreButtonsEnabled = true;
		}
		#endregion Public Methods

		#region Private Methods
		private void _Manager_LoadComplete(object sender, EventArgs e)
		{
			FileName = Manager.FileName;
			UpdateInterface();
		}
		private void AddFilesFromDirectory(SysIO.DirectoryInfo d, string id, bool includeSubFolders, ref Dictionary<string, string> items)
		{
			foreach (var item in d.GetFiles())
			{
				items.Add(Guid.NewGuid().ToString(), item.FullName);
			}
			if (!includeSubFolders)
				return;
			foreach (var item in d.GetDirectories())
			{
				AddFilesFromDirectory(item, null, includeSubFolders, ref items);
			}
		}
		private void Build(object state)
		{
			var createCommand = new BuildCommand();
			createCommand.CommandStatusUpdate += createCommand_CommandStatusUpdate;
			var items = new Dictionary<string, string>();
			Manager.Items.ToList().ForEach(x =>
			{
				if (x.ItemType == ItemTypes.Folder)
				{
					var dInfo = new SysIO.DirectoryInfo(x.Path);
					AddFilesFromDirectory(dInfo, null, x.IncludeSubFolders, ref items);
				}
				else
					items.Add(x.Name, x.Path);
			});
			var extension = (string)OptionCategories.GetItem("General", "Installer Settings", "Build", "Build file extension").Value;
			extension = extension.StartsWith(".") ? extension.TrimStart('.') : extension;
			var tempFileName = new SysIO.DirectoryInfo(SysIO.Path.GetDirectoryName(Manager.FileName)).GetTempFile();
			var zipFileName = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Manager.FileName), string.Format("{0}.{1}", SysIO.Path.GetFileNameWithoutExtension(Manager.FileName), extension));
			var parameters = new Dictionary<string, object>
			{
				{ "zipfile", zipFileName },
				{ "itemlist", items },
				{ "tempfilename", tempFileName }
			};
			createCommand.Execute(parameters);
		}
		private DelegateCommand _OutputDirectoryCommand = null;
		public DelegateCommand OutputDirectoryCommand
		{
			get
			{
				if (_OutputDirectoryCommand == null)
					_OutputDirectoryCommand = new DelegateCommand(OutputDirectory, ValidateOutputDirectoryState);
				return _OutputDirectoryCommand as DelegateCommand;
			}
		}
		private void OutputDirectory(object state)
		{
			var zipFolder = SysIO.Path.GetDirectoryName(Manager.FileName);
			Process.Start(zipFolder);
		}
		private bool ValidateOutputDirectoryState(object state)
		{
			return Manager != null;
		}

		private void createCommand_CommandStatusUpdate(object sender, OSInstallerCommands.Classes.Events.CommandStatusUpdateEventArgs e)
		{
			if (CommandStatusUpdate != null)
				CommandStatusUpdate(this, e);
		}
		private string _BuildProgressText;
		public string BuildProgressText
		{
			get { return _BuildProgressText; }
			set
			{
				_BuildProgressText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BuildProgressText"));
			}
		}
		private Button GetButton(string asset, DelegateCommand command, string toolTip, double iconSize)
		{
			var btn = new Button
			{
				Content = GetImage(asset, iconSize),
				Command = command,
				ToolTip = toolTip
			};
			return btn;
		}
		private Category GetExtensionsCategory()
		{
			var result = new Category("Extensions");

			var ext_extPage = new OSInstallerBuilder.Classes.Options.Page("Extensions", result);

			var ext_ext_ItemsGroup = new Group("Items", ext_extPage);

			var extensionListItem = new Item("Extensions", ext_ext_ItemsGroup, typeof(List<string>), new List<string>())
			{
				Sequence = 0,
			};

			ext_ext_ItemsGroup.Items.Add(extensionListItem);
			ext_extPage.Groups.Add(ext_ext_ItemsGroup);
			result.Pages.Add(ext_extPage);

			return result;
		}
		private Category GetGeneralCategory()
		{
			var result = new Category("General");

			var genPage = new OSInstallerBuilder.Classes.Options.Page("General", result);
			var interfacePage = new OSInstallerBuilder.Classes.Options.Page("User Interface", result);
			var installerPage = new OSInstallerBuilder.Classes.Options.Page("Installer Settings", result);

			var foldersGroup = new Group("Folders", genPage);
			var startupGroup = new Group("Startup", genPage);
			var defaultsGroup = new Group("Defaults", interfacePage);
			var buildGroup = new Group("Build", installerPage);

			var logsFolderItem = new Item("Logs folder", foldersGroup, typeof(string), SysIO.Path.Combine(SysIO.Path.Combine(SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), App.ApplicationName), "logs")))
			{
				Sequence = 0,
				StringSubType = Classes.Options.Item.StringSubTypes.Directory
			};
			var savePositionItem = new Item("Save window position", startupGroup, typeof(bool), true)
			{
				Sequence = 1,
			};
			var headerBackgroundItem = new Item("Header background", defaultsGroup, typeof(Color), Colors.Black)
			{
				Sequence = 0,
			};
			var headerForegroundItem = new Item("Header foreground", defaultsGroup, typeof(Color), Colors.White)
			{
				Sequence = 1,
			};
			var winBackgroundItem = new Item("Window background", defaultsGroup, typeof(Color), Colors.LightGray)
			{
				Sequence = 2,
			};
			var winForegroundItem = new Item("Window foreground", defaultsGroup, typeof(Color), Colors.Black)
			{
				Sequence = 3,
			};
			var wizardImageItem = new Item("Wizard image", defaultsGroup, typeof(string), string.Empty)
			{
				Sequence = 4,
				StringSubType = Item.StringSubTypes.File
			};
			var hideFileNamesItem = new Item("Hide file names", buildGroup, typeof(bool), true)
			{
				Sequence = 0,
			};
			var installerExtensionItem = new Item("Installer extension", buildGroup, typeof(string), ".installer")
			{
				Sequence = 1,
				StringSubType = Item.StringSubTypes.None
			};
			var compressedExtensionItem = new Item("Build file extension", buildGroup, typeof(string), ".zip")
			{
				Sequence = 2,
				StringSubType = Item.StringSubTypes.None
			};

			foldersGroup.Items.Add(logsFolderItem);
			startupGroup.Items.Add(savePositionItem);
			defaultsGroup.Items.Add(headerBackgroundItem);
			defaultsGroup.Items.Add(headerForegroundItem);
			defaultsGroup.Items.Add(winBackgroundItem);
			defaultsGroup.Items.Add(winForegroundItem);
			defaultsGroup.Items.Add(wizardImageItem);
			buildGroup.Items.Add(hideFileNamesItem);
			buildGroup.Items.Add(installerExtensionItem);
			buildGroup.Items.Add(compressedExtensionItem);
			genPage.Groups.Add(foldersGroup);
			genPage.Groups.Add(startupGroup);
			interfacePage.Groups.Add(defaultsGroup);
			installerPage.Groups.Add(buildGroup);
			result.Pages.Add(genPage);
			result.Pages.Add(interfacePage);
			result.Pages.Add(installerPage);

			return result;
		}
		private Image GetImage(string asset, double iconSize)
		{
			Style style = Application.Current.FindResource("ControlButtonImage").As<Style>();
			var img = new Image
			{
				Style = style,
				Width = iconSize,
				Height = iconSize,
				Margin = new System.Windows.Thickness(2),
				Source = OSInstallerExtensibility.Classes.Helpers.GetImageSourceFromResource("OSInstallerBuilder", asset)
			};
			return img;
		}
		private void NewFile(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("NewFile"));
		}
		private void OpenFile(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("OpenFile"));
		}
		private void Options(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("ShowOptionsWindow"));
		}
		private void SaveFile(object state)
		{
			Manager.Save();
			UpdateInterface();
		}
		private bool ValidateBuildState(object state)
		{
			return _Manager != null;
		}
		private bool ValidateCommandsState(object state)
		{
			return Manager != null;
		}
		private bool ValidateNewFileState(object state)
		{
			return true;
		}
		private bool ValidateOpenFileState(object state)
		{
			return true;
		}
		private bool ValidateOptionsState(object state)
		{
			return true;
		}
		private bool ValidateSaveFileState(object state)
		{
			return Manager != null && Manager.IsDirty;
		}
		#endregion Private Methods

		#region Public Events
		public event CommandStatusUpdateHandler CommandStatusUpdate;
		public event ExecuteUiActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool _AreButtonsEnabled;
		private DelegateCommand _BuildCommand = null;
		private double _BuildProgressMaximum;
		private double _BuildProgressValue;
		private Visibility _BuildProgressVisibility;
		private string _FileName;
		private bool _IsInitializing;
		private IInstallerManager _Manager;
		private DelegateCommand _NewFileCommand = null;
		private DelegateCommand _OpenFileCommand = null;
		private OptionList _OptionCategories;
		private DelegateCommand _OptionsCommand = null;
		private DelegateCommand _SaveFileCommand = null;
		private bool _TabControlEnabled;
		#endregion Private Fields

		#region Public Properties
		public bool AreButtonsEnabled
		{
			get
			{
				return _AreButtonsEnabled;
			}
			set
			{
				_AreButtonsEnabled = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AreButtonsEnabled"));
			}
		}
		public DelegateCommand BuildCommand
		{
			get
			{
				if (_BuildCommand == null)
					_BuildCommand = new DelegateCommand(Build, ValidateBuildState);
				return _BuildCommand.As<DelegateCommand>();
			}
		}
		public double BuildProgressMaximum
		{
			get
			{
				return _BuildProgressMaximum;
			}
			set
			{
				_BuildProgressMaximum = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BuildProgressMaximum"));
			}
		}
		public double BuildProgressValue
		{
			get
			{
				return _BuildProgressValue;
			}
			set
			{
				_BuildProgressValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BuildProgressValue"));
			}
		}
		public Visibility BuildProgressVisibility
		{
			get
			{
				return _BuildProgressVisibility;
			}
			set
			{
				_BuildProgressVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BuildProgressVisibility"));
			}
		}
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public bool IsInitializing
		{
			get
			{
				return _IsInitializing;
			}
			set
			{
				_IsInitializing = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsInitializing"));
			}
		}
		public IInstallerManager Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				_Manager = value;
				if (_Manager == null)
					return;
				_Manager.LoadComplete += _Manager_LoadComplete;
				FileName = _Manager.FileName;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
			}
		}
		public DelegateCommand NewFileCommand
		{
			get
			{
				if (_NewFileCommand == null)
					_NewFileCommand = new DelegateCommand(NewFile, ValidateNewFileState);
				return _NewFileCommand.As<DelegateCommand>();
			}
		}
		public DelegateCommand OpenFileCommand
		{
			get
			{
				if (_OpenFileCommand == null)
					_OpenFileCommand = new DelegateCommand(OpenFile, ValidateOpenFileState);
				return _OpenFileCommand.As<DelegateCommand>();
			}
		}
		public OptionList OptionCategories
		{
			get
			{
				return _OptionCategories;
			}
			set
			{
				_OptionCategories = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OptionCategories"));
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
		public DelegateCommand SaveFileCommand
		{
			get
			{
				if (_SaveFileCommand == null)
					_SaveFileCommand = new DelegateCommand(SaveFile, ValidateSaveFileState);
				return _SaveFileCommand.As<DelegateCommand>();
			}
		}
		public bool TabControlEnabled
		{
			get
			{
				return _TabControlEnabled;
			}
			set
			{
				_TabControlEnabled = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TabControlEnabled"));
			}
		}
		#endregion Public Properties
	}
}
