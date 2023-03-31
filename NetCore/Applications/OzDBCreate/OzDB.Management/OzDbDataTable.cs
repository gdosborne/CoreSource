using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OzDB.Management {
    public class OzDbDataTable : PropertyChangedBase {
        internal OzDbDataTable() { }

        #region Fields Property
        private List<OzDBDataField> _Fields = default;
        /// <summary>Gets/sets the Fields.</summary>
        /// <value>The Fields.</value>
        [JsonProperty]
        public List<OzDBDataField> Fields {
            get => _Fields;
            set {
                _Fields = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsHidden Property
        private bool _IsHidden = default;
        /// <summary>Gets/sets the IsHidden.</summary>
        /// <value>The IsHidden.</value>
        public bool IsHidden {
            get => _IsHidden;
            set {
                _IsHidden = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public async override Task<bool> DeleteAsync() {
            try {

            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }
    }
}
