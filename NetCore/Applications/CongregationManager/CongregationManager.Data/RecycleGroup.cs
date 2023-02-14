using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CongregationManager.Data {
    public class RecycleGroup : INotifyPropertyChanged {
        public RecycleGroup(string name) {
            Name = name;
            Items = new ObservableCollection<RecycleItem>();
        }

        public string Name { get; set; }
        public ObservableCollection<RecycleItem> Items { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private RecycleItem _SelectedItem = default;
        public RecycleItem SelectedItem {
            get => _SelectedItem;
            set {
                _SelectedItem = value;
                IsExpanded = SelectedItem != null;
                if (SelectedItem != null)
                    OnPropertyChanged();
            }
        }

        #region IsExpanded Property
        private bool _IsExpanded = default;
        /// <summary>Gets/sets the IsExpanded.</summary>
        /// <value>The IsExpanded.</value>
        public bool IsExpanded {
            get => _IsExpanded;
            set {
                _IsExpanded = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
