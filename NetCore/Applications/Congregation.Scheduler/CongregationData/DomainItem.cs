using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CongregationData {
    public abstract class DomainItem : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged ([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region ID Property
        private Guid _ID = default;
        public Guid ID {
            get => _ID;
            set {
                _ID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public abstract XElement ToXElement ();
    }
}
