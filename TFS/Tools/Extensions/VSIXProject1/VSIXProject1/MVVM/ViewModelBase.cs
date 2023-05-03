namespace VSIXProject1.MVVM
{
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void InvokePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            UpdateInterface();
        }

        public bool SettingProperty { get; private set; } = false;

        public virtual void UpdateInterface()
        {
            var cmds = this.GetType().GetProperties().Where(x => x.PropertyType == typeof(DelegateCommand));
            var infos = cmds as PropertyInfo[] ?? cmds.ToArray();
            if (!infos.Any())
                return;
            infos.ToList().ForEach(x => {
                SettingProperty = true;
                (x.GetValue(this) as DelegateCommand).RaiseCanExecuteChanged();
                SettingProperty = false;
            });
        }
    }
}
