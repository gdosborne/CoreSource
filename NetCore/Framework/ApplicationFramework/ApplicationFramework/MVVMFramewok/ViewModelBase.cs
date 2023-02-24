using Common.Application.Primitives;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common.MVVMFramework {
    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase {

        #region Public Events
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUiActionHandler ExecuteUiAction;
        #endregion Public Events

        #region Public Methods
        public virtual void Initialize() { }

        private bool settingProperty = false;
        public bool SettingProperty { get { return settingProperty; } private set { settingProperty = value; } }
        public virtual void UpdateInterface() {
            var cmds = GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
            var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
            if (!infos.Any()) {
                return;
            }

            infos.ToList().ForEach(x => {
                SettingProperty = true;
                x.GetValue(this).As<DelegateCommand>().RaiseCanExecuteChanged();
                SettingProperty = false;
            });
        }
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
        protected void ExecuteAction(string actionName, UiActionParameters parameters) =>
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(actionName, parameters));
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
