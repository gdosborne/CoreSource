using OzFramework.Primitives;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OzMiniDB.Builder.Helper {
    public class SettingGroup : INotifyPropertyChanged {
        public SettingGroup() {
            Values = [];
        }
        public List<SettingValue> Values { get; private set; }

        #region IsSelected Property
        private bool _IsSelected = default;
        public bool IsSelected {
            get => _IsSelected;
            set {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsExpanded Property
        private bool _IsExpanded = default;
        public bool IsExpanded {
            get => _IsExpanded;
            set {
                _IsExpanded = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Name Property
        private string _Name = default;
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ArrowVisibility Property
        private Visibility _ArrowVisibility = default;
        public Visibility ArrowVisibility {
            get => _ArrowVisibility;
            set {
                _ArrowVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
