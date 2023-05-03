using GregOsborne.Application.Primitives;
using MVVMFramework;
using OptiRampDesignerModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace OptiRampDesigner.Windows
{
    public class OptionsWindowView : INotifyPropertyChanged
    {
        #region Private Fields

        private bool? _DialogResult;

        private ObservableCollection<IOptionSet> _Options;

        #endregion Private Fields

        #region Public Events

        public event ExecuteUIActionHandler ExecuteUIAction;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public bool? DialogResult {
            get { return _DialogResult; }
            set {
                _DialogResult = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        public ObservableCollection<IOptionSet> Options {
            get { return _Options; }
            set {
                _Options = value;
                RefreshSettings();
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Initialize(Window window)
        {
            window.Left = App.GetSetting<double>("Options.Window.Left", window.Left);
            window.Top = App.GetSetting<double>("Options.Window.Top", window.Top);
            window.Width = App.GetSetting<double>("Options.Window.Width", window.Width);
            window.Height = App.GetSetting<double>("Options.Window.Height", window.Height);
        }

        public void InitView()
        {
            RefreshSettings();
            UpdateInterface();
        }

        public void Persist(Window window)
        {
            App.SetSetting<double>("Options.Window.Left", window.RestoreBounds.Left);
            App.SetSetting<double>("Options.Window.Top", window.RestoreBounds.Top);
            App.SetSetting<double>("Options.Window.Width", window.RestoreBounds.Width);
            App.SetSetting<double>("Options.Window.Height", window.RestoreBounds.Height);
        }

        public void UpdateInterface()
        {
        }

        #endregion Public Methods

        #region Private Methods

        private void RefreshSettings()
        {
            if (ExecuteUIAction != null)
            {
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ClearTree"));
                var p = new Dictionary<string, object>
                {
                    { "optionset", null }
                };
                Options.ToList().ForEach(opt =>
                {
                    p["optionset"] = opt;
                    ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddOptionSet", p));
                });
            }
        }

        #endregion Private Methods
    }
}