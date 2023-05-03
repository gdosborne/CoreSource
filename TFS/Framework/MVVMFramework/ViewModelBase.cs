﻿using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GregOsborne.Application.Primitives;

namespace GregOsborne.MVVMFramework {
    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase {

        #region Public Events

        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Methods

        public virtual void Initialize() { }

        private bool settingProperty = false;
        public bool SettingProperty { get { return settingProperty; } private set { settingProperty = value; } }
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
            UpdateInterface();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void InvokePropertyChanged(ViewModelBase sender, string propertyName) {
            UpdateInterface();
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

		public event ExecuteUiActionHandler ExecuteUiAction;

		#endregion Protected Methods
	}
}
