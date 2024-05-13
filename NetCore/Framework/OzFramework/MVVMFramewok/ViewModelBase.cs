/* File="ViewModelBase"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.MVVMFramework {
    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase {

        #region IgnoreChanges Property
        private bool _IgnoreChanges = false;
        public bool IgnoreChanges {
            get => _IgnoreChanges;
            set {
                _IgnoreChanges = value;
            }
        }
        #endregion

        #region Public Events
        public virtual event EventHandler InitializationComplete;
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUiActionHandler ExecuteUiAction;
        #endregion Public Events

        #region Public Methods
        public virtual void Initialize() { }

        private bool settingProperty = false;
        public bool SettingProperty { get { return settingProperty; } private set { settingProperty = value; } }
        private bool isUpdateInProgress { get; set; }
        public virtual void UpdateInterface() {
            if (isUpdateInProgress)
                return;
            isUpdateInProgress = true;
            var cmds = GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
            var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
            if (!infos.Any()) {
                isUpdateInProgress = false;
                return;
            }

            infos.ToList().ForEach(x => {
                SettingProperty = true;
                (x.GetValue(this) as DelegateCommand).RaiseCanExecuteChanged();
                SettingProperty = false;
            });
            isUpdateInProgress = false;
        }
        private static string lastName = default;
        private static int count = 0;
        #endregion Public Methods

        #region Protected Methods
        protected void InvokePropertyChanged([CallerMemberName] string propertyName = null) {
            UpdateInterface();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void InvokePropertyChanged(ViewModelBase sender, [CallerMemberName] string propertyName = null) {
            UpdateInterface();
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            InvokePropertyChanged(propertyName);

        protected void ExecuteAction(string actionName) => 
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(actionName));
        protected void ExecuteAction(string actionName, Dictionary<string, object> parameters) =>
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(actionName, parameters));
        protected void ExecuteAction(UiActionParameters parameters) =>
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(parameters.CommandToExecute, parameters));
        protected void ExecuteAction(string actionName, params KeyValuePair<string, object>[] parameters) =>
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(actionName, parameters));
        #endregion Protected Methods

        #region Title Property
        private string _Title = default;
        /// <summary>Gets/sets the Title.</summary>
        /// <value>The Title.</value>
        public string Title {
            get => _Title;
            set {
                _Title = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
