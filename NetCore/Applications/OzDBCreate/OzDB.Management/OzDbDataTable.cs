//using Common.Application.Linq;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OzDB.Management {
    public class OzDbDataTable : PropertyChangedBase {
        internal OzDbDataTable() {
            Fields = new ObservableCollection<OzDBDataField>();
        }

        #region Fields Property
        private ObservableCollection<OzDBDataField> _Fields = default;
        /// <summary>Gets/sets the Fields.</summary>
        /// <value>The Fields.</value>
        [JsonProperty]
        public ObservableCollection<OzDBDataField> Fields {
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

        public static OzDbDataTable Create(string name, string description, bool isHidden) =>
            Create(name, description, isHidden, null);

        public static OzDbDataTable Create(string name, string description, bool isHidden, List<OzDBDataField> fields) {
            var result = new OzDbDataTable {
                Name = name,
                Description= description,
                IsHidden = isHidden,
                Fields = new ObservableCollection<OzDBDataField>()
            };
            if(fields != null ) {
                //result.Fields.AddRange(fields);
            }
            return result;
        }
            

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
