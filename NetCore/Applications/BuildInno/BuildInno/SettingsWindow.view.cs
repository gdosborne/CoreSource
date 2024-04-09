using GregOsborne.MVVMFramework;

namespace BuildInno {
    public partial class SettingsWindowView : ViewModelBase {
        public SettingsWindowView() {
            Title = "Settings [designer]";
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Settings";
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsLightThemeChecked Property
        private bool _IsLightThemeChecked = default;
        public bool IsLightThemeChecked {
            get => _IsLightThemeChecked;
            set {
                _IsLightThemeChecked = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsDarkThemeChecked Property
        private bool _IsDarkThemeChecked = default;
        public bool IsDarkThemeChecked {
            get => _IsDarkThemeChecked;
            set {
                _IsDarkThemeChecked = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsWindowPositionSaved Property
        private bool _IsWindowPositionSaved = default;
        public bool IsWindowPositionSaved {
            get => _IsWindowPositionSaved;
            set {
                _IsWindowPositionSaved = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
