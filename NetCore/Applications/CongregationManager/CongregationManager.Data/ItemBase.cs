using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CongregationManager.Data {
    public abstract class ItemBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region ID Property
        private int _ID = default;
        /// <summary>Gets/sets the ID.</summary>
        /// <value>The ID.</value>
        [JsonProperty("id")]
        public int ID {
            get => _ID;
            set {
                _ID = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
