namespace OptiRampDesigner.Windows
{
    using GregOsborne.Application.Primitives;
    using MVVMFramework;
    using OptiRampDesignerModel;
    using OptiRampDesignerModel.Events;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;

    public class MainWindowView : INotifyPropertyChanged
    {
        #region Private Fields

        private DelegateCommand _ApplicationSettingsCommand = null;

        private DelegateCommand _HelpCommand = null;

        private DelegateCommand _LoadModulesCommand = null;

        private DelegateCommand _LogCommand = null;

        private ObservableCollection<IModule> _Modules;

        private DelegateCommand _NewProjectCommand = null;

        private DelegateCommand _OpenProjectCommand = null;

        private ObservableCollection<IOptionSet> _Options;

        private IDesignerProject _Project;

        private DelegateCommand _SaveAsCommand = null;

        private DelegateCommand _SaveCommand = null;

        #endregion Private Fields

        #region Public Events

        public event ExecuteUIActionHandler ExecuteUIAction;

        public event InitializationCompleteHandler ModuleInitializationComplete;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public DelegateCommand ApplicationSettingsCommand {
            get {
                if (_ApplicationSettingsCommand == null)
                    _ApplicationSettingsCommand = new DelegateCommand(ApplicationSettings, ValidateApplicationSettingsState);
                return _ApplicationSettingsCommand as DelegateCommand;
            }
        }

        public DelegateCommand HelpCommand {
            get {
                if (_HelpCommand == null)
                    _HelpCommand = new DelegateCommand(Help, ValidateHelpState);
                return _HelpCommand as DelegateCommand;
            }
        }

        public DelegateCommand LoadModulesCommand {
            get {
                if (_LoadModulesCommand == null)
                    _LoadModulesCommand = new DelegateCommand(LoadModules, ValidateLoadModulesState);
                return _LoadModulesCommand as DelegateCommand;
            }
        }

        public DelegateCommand LogCommand {
            get {
                if (_LogCommand == null)
                    _LogCommand = new DelegateCommand(Log, ValidateLogState);
                return _LogCommand as DelegateCommand;
            }
        }

        public ObservableCollection<IModule> Modules {
            get { return _Modules; }
            set {
                _Modules = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        public DelegateCommand NewProjectCommand {
            get {
                if (_NewProjectCommand == null)
                    _NewProjectCommand = new DelegateCommand(NewProject, ValidateNewProjectState);
                return _NewProjectCommand as DelegateCommand;
            }
        }

        public DelegateCommand OpenProjectCommand {
            get {
                if (_OpenProjectCommand == null)
                    _OpenProjectCommand = new DelegateCommand(OpenProject, ValidateOpenProjectState);
                return _OpenProjectCommand as DelegateCommand;
            }
        }

        public ObservableCollection<IOptionSet> Options {
            get { return _Options; }
            set {
                _Options = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        public IDesignerProject Project {
            get { return _Project; }
            set {
                _Project = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        public DelegateCommand SaveAsCommand {
            get {
                if (_SaveAsCommand == null)
                    _SaveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState);
                return _SaveAsCommand as DelegateCommand;
            }
        }

        public DelegateCommand SaveCommand {
            get {
                if (_SaveCommand == null)
                    _SaveCommand = new DelegateCommand(Save, ValidateSaveState);
                return _SaveCommand as DelegateCommand;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Initialize(Window window)
        {
            window.Left = App.GetSetting<double>("Main.Window.Left", window.Left);
            window.Top = App.GetSetting<double>("Main.Window.Top", window.Top);
            window.Width = App.GetSetting<double>("Main.Window.Width", window.Width);
            window.Height = App.GetSetting<double>("Main.Window.Height", window.Height);
            window.WindowState = App.GetSetting<WindowState>("Main.Window.WindowState", window.WindowState);
        }

        public void InitView()
        {
            Options = new ObservableCollection<IOptionSet>();
            Options.Add(App.ApplicationOptions);
            Modules = new ObservableCollection<IModule>(App.GetModules());
            foreach (var mod in Modules)
            {
                mod.InitializationComplete += mod_InitializationComplete;
                mod.Initialize();
                Options.Add(mod.Options);
            }
        }

        public void Persist(Window window)
        {
            if (ExecuteUIAction != null)
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("BeginRelease"));
            foreach (var mod in Modules)
            {
                mod.Release();
            }
            App.SetSetting<double>("Main.Window.Left", window.RestoreBounds.Left);
            App.SetSetting<double>("Main.Window.Top", window.RestoreBounds.Top);
            App.SetSetting<double>("Main.Window.Width", window.RestoreBounds.Width);
            App.SetSetting<double>("Main.Window.Height", window.RestoreBounds.Height);
            App.SetSetting<WindowState>("Main.Window.WindowState", window.WindowState);
        }

        public void UpdateInterface()
        {
            NewProjectCommand.RaiseCanExecuteChanged();
            OpenProjectCommand.RaiseCanExecuteChanged();
            ApplicationSettingsCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
            LoadModulesCommand.RaiseCanExecuteChanged();
        }

        #endregion Public Methods

        #region Private Methods

        private void ApplicationSettings(object state)
        {
            if (ExecuteUIAction != null)
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowOptionsDialog"));
        }

        private void Help(object state)
        {
        }

        private void LoadModules(object state)
        {
            if (ExecuteUIAction != null)
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowLoadModulesDialog"));
        }

        private void Log(object state)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = App.Log.LogFileName
                }
            }.Start();
        }

        private void mod_InitializationComplete(object sender, InitializationCompleteEventArgs e)
        {
            if (ModuleInitializationComplete != null)
                ModuleInitializationComplete(this, e);
        }

        private void NewProject(object state)
        {
        }

        private void OpenProject(object state)
        {
        }

        private void Save(object state)
        {
        }

        private void SaveAs(object state)
        {
        }

        private bool ValidateApplicationSettingsState(object state)
        {
            return true;
        }

        private bool ValidateHelpState(object state)
        {
            return false;
        }

        private bool ValidateLoadModulesState(object state)
        {
            return true;
        }

        private bool ValidateLogState(object state)
        {
            return true;
        }

        private bool ValidateNewProjectState(object state)
        {
            return Project == null || (Project != null && !Project.IsChanged);
        }

        private bool ValidateOpenProjectState(object state)
        {
            return Project == null;
        }

        private bool ValidateSaveAsState(object state)
        {
            return Project != null;
        }

        private bool ValidateSaveState(object state)
        {
            return Project != null && Project.IsChanged;
        }

        #endregion Private Methods
    }
}