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
            set {
                HasNameChange = value != Name;
                _Name = value;
                HasChanges = HasNameChange;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Description Property
        private string _Description = default;
        /// <summary>Gets/sets the Description.</summary>
        /// <value>The Description.</value>
        [JsonProperty]
        public string Description {
            get => _Description;
            set {
                _Description = value;
                HasChanges = true;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasChanges Property
        private bool _HasChanges = default;
        /// <summary>Gets/sets the HasChanges.</summary>
        /// <value>The HasChanges.</value>
        [JsonIgnore]
        public bool HasChanges {
            get => _HasChanges;
            set {
                _HasChanges = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasNameChange Property
        private bool _HasNameChange = default;
        /// <summary>Gets/sets the HasNameChange.</summary>
        /// <value>The HasNameChange.</value>
        [JsonIgnore]
        public bool HasNameChange {
            get => _HasNameChange;
            set {
                _HasNameChange = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public abstract Task<bool> DeleteAsync();
    }
}
