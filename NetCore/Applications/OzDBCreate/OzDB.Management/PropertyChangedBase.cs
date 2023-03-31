using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OzDB.Management {
    [JsonObject]
    public abstract class PropertyChangedBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        [JsonProperty]
        public string Name {
            get => _Name;
            protected set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Description Property
        private string _Description = default;
        /// <summary>Gets/sets the Description.</summary>
        /// <value>The Description.</value>
        public string Description {
            get => _Description;
            set {
                _Description = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public abstract Task<bool> Save();
        public abstract Task<bool> MoveAsync(string destination);
        public abstract Task<bool> DeleteAsync();
    }
}
