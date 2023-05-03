namespace MVVMFramework {
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;

	public class ViewModelBase : INotifyPropertyChanged {
		public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual event ExecuteUIActionHandler ExecuteUIAction;

        public bool SettingProperty { get; set; } = default;
		public virtual void Initialize() { }
		public virtual void UpdateInterface() {
			var cmds = this.GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
			var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
			if (!infos.Any()) {
				return;
			}

			infos.ToList().ForEach(x => {
				this.SettingProperty = true;
				((DelegateCommand)x.GetValue(this)).RaiseCanExecuteChanged();
				this.SettingProperty = false;
			});
		}

		protected void InvokePropertyChanged(string propertyName) {
			this.UpdateInterface();
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void InvokePropertyChanged(ViewModelBase sender, string propertyName) {
			this.UpdateInterface();
			PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
		}
	}
}
