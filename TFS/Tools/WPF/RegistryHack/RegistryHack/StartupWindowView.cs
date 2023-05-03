using GregOsborne.MVVMFramework;

namespace RegistryHack
{
    internal class StartupWindowView : ViewModelBase
    {
        private string _VersionText;

        public StartupWindowView()
        {
            VersionText = $"Version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string VersionText {
            get {
                return _VersionText;
            }
            set {
                _VersionText = value;
                InvokePropertyChanged(nameof(VersionText));
            }
        }
    }
}