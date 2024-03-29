using GregOsborne.Application.Primitives;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GregOsborne.MVVMFramework {
    public class ViewModelBase : INotifyPropertyChanged {
        #region Public Events

        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Methods

        public virtual void Initialize() { }

        private bool _SettingProperty = false;
        public bool SettingProperty { get { return _SettingProperty; } private set { _SettingProperty = value; } }
        public virtual void UpdateInterface() {
            var cmds = this.GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
            var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
            if (!infos.Any()) return;
            infos.ToList().ForEach(x => {
                SettingProperty = true;
                x.GetValue(this).As<DelegateCommand>().RaiseCanExecuteChanged();
                SettingProperty = false;
            });
        }

        #endregion Public Methods

        #region Protected Methods

        protected void InvokePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void InvokePropertyChanged(ViewModelBase sender, string propertyName) {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected Methods
    }
}