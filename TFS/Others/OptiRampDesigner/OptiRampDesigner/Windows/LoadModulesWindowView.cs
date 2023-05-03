namespace OptiRampDesigner.Windows
{
    using GregOsborne.Application.Primitives;
    using MVVMFramework;
    using OptiRampDesignerModel;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;

    public class LoadModulesWindowView : INotifyPropertyChanged
    {
        #region Private Fields

        private DelegateCommand _BrowseForModuleCommand = null;

        private DelegateCommand _CancelCommand = null;

        private bool? _DialogResult;

        private ObservableCollection<IModule> _Modules;

        private DelegateCommand _OKCommand = null;

        private DelegateCommand _RemoveModuleCommand = null;

        private IModule _SelectedModule;

        #endregion Private Fields

        #region Public Events

        public event ExecuteUIActionHandler ExecuteUIAction;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public DelegateCommand BrowseForModuleCommand {
            get {
                if (_BrowseForModuleCommand == null)
                    _BrowseForModuleCommand = new DelegateCommand(BrowseForModule, ValidateBrowseForModuleState);
                return _BrowseForModuleCommand as DelegateCommand;
            }
        }

        public DelegateCommand CancelCommand {
            get {
                if (_CancelCommand == null)
                    _CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
                return _CancelCommand as DelegateCommand;
            }
        }

        public bool? DialogResult {
            get { return _DialogResult; }
            set {
                _DialogResult = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
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

        public DelegateCommand OKCommand {
            get {
                if (_OKCommand == null)
                    _OKCommand = new DelegateCommand(OK, ValidateOKState);
                return _OKCommand as DelegateCommand;
            }
        }

        public DelegateCommand RemoveModuleCommand {
            get {
                if (_RemoveModuleCommand == null)
                    _RemoveModuleCommand = new DelegateCommand(RemoveModule, ValidateRemoveModuleState);
                return _RemoveModuleCommand as DelegateCommand;
            }
        }

        public IModule SelectedModule {
            get { return _SelectedModule; }
            set {
                _SelectedModule = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Initialize(Window window)
        {
            window.Left = App.GetSetting<double>("Load.Modules.Window.Left", window.Left);
            window.Top = App.GetSetting<double>("Load.Modules.Window.Top", window.Top);
            window.Width = App.GetSetting<double>("Load.Modules.Window.Width", window.Width);
            window.Height = App.GetSetting<double>("Load.Modules.Window.Height", window.Height);
        }

        public void InitView()
        {
            Modules = new ObservableCollection<IModule>(App.GetModules());
            UpdateInterface();
        }

        public void Persist(Window window)
        {
            App.SetSetting<double>("Load.Modules.Window.Left", window.RestoreBounds.Left);
            App.SetSetting<double>("Load.Modules.Window.Top", window.RestoreBounds.Top);
            App.SetSetting<double>("Load.Modules.Window.Width", window.RestoreBounds.Width);
            App.SetSetting<double>("Load.Modules.Window.Height", window.RestoreBounds.Height);
        }

        public void UpdateInterface()
        {
            OKCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            BrowseForModuleCommand.RaiseCanExecuteChanged();
            RemoveModuleCommand.RaiseCanExecuteChanged();
        }

        #endregion Public Methods

        #region Private Methods

        private void BrowseForModule(object state)
        {
            if (ExecuteUIAction != null)
            {
                var p = new Dictionary<string, object>
                {
                    { "result", false },
                    { "filename", string.Empty },
                    { "filter", "Modules|*.dll" },
                    { "title", "Select module..." }
                };
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowFileSelectDialog", p));
                if (!(bool)p["result"])
                    return;
                App.SetSetting<string>("Last.Module.Directory", Path.GetDirectoryName((string)p["filename"]));
                if (App.AddModule((string)p["filename"]))
                    Modules = new ObservableCollection<IModule>(App.GetModules());
            }
        }

        private void Cancel(object state)
        {
            DialogResult = false;
        }

        private void OK(object state)
        {
            DialogResult = true;
        }

        private void RemoveModule(object state)
        {
            if (ExecuteUIAction != null)
            {
                var p = new Dictionary<string, object>
                {
                    { "result", false },
                    { "message", "Removing the module removes the functionality from the application.\n\nAre you sure you want to remove the selected module?" }
                };
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowYesNoDialog", p));
                if (!(bool)p["result"])
                    return;
                SelectedModule.Release();
                if (App.RemoveModule(SelectedModule.FullPath))
                    Modules = new ObservableCollection<IModule>(App.GetModules());
                SelectedModule = null;
            }
        }

        private bool ValidateBrowseForModuleState(object state)
        {
            return true;
        }

        private bool ValidateCancelState(object state)
        {
            return true;
        }

        private bool ValidateOKState(object state)
        {
            return true;
        }

        private bool ValidateRemoveModuleState(object state)
        {
            return SelectedModule != null;
        }

        #endregion Private Methods
    }
}