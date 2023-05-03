using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Counties.MVVM {
	public class ViewModelBase : INotifyPropertyChanged {

		#region Public Events

		public virtual event PropertyChangedEventHandler PropertyChanged;

		#endregion Public Events

		#region Public Methods

		public virtual void Initialize() { }

		private bool _settingProperty = false;
		public bool SettingProperty { get { return _settingProperty; } private set { _settingProperty = value; } }
		public virtual void UpdateInterface() {
			var cmds = this.GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
			var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
			if (!infos.Any()) return;
			infos.ToList().ForEach(x => {
				SettingProperty = true;
				((DelegateCommand)x.GetValue(this)).RaiseCanExecuteChanged();
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

		#endregion Protected Methods
	}
}
