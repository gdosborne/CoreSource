using Newtonsoft.Json;
using OzDB.Management.Validators;
using System;
using System.Threading.Tasks;

namespace OzDB.Management {
    [JsonObject]
    public class OzDBDataField : PropertyChangedBase {
        public override async Task<bool> DeleteAsync() {
            await Task.Yield();
            return true;
        }

        #region DataType Property
        private Type _DataType = default;
        /// <summary>Gets/sets the DataType.</summary>
        /// <value>The DataType.</value>
        [JsonProperty]
        public Type DataType {
            get => _DataType;
            set {
                _DataType = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region StringLength Property
        private int _StringLength = default;
        /// <summary>Gets/sets the StringLength.</summary>
        /// <value>The StringLength.</value>
        [JsonProperty]
        public int StringLength {
            get => _StringLength;
            set {
                _StringLength = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsFixedLength Property
        private bool _IsFixedLength = default;
        /// <summary>Gets/sets the IsFixedLength.</summary>
        /// <value>The IsFixedLength.</value>
        [JsonProperty]
        public bool IsFixedLength {
            get => _IsFixedLength;
            set {
                _IsFixedLength = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Validator Property
        private IValidator _Validator = default;
        /// <summary>Gets/sets the Validator.</summary>
        /// <value>The Validator.</value>
        [JsonProperty]
        public IValidator Validator {
            get => _Validator;
            set {
                _Validator = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AreNullValuesAllowed Property
        private bool _AreNullValuesAllowed = default;
        /// <summary>Gets/sets the AreNullValuesAllowed.</summary>
        /// <value>The AreNullValuesAllowed.</value>
        [JsonProperty]
        public bool AreNullValuesAllowed {
            get => _AreNullValuesAllowed;
            set {
                _AreNullValuesAllowed = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NullReplacementValue Property
        private dynamic _NullReplacementValue = default;
        /// <summary>Gets/sets the NullReplacementValue.</summary>
        /// <value>The NullReplacementValue.</value>
        [JsonProperty]
        public dynamic NullReplacementValue {
            get => _NullReplacementValue;
            set {
                _NullReplacementValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PromptText Property
        private string _PromptText = default;
        /// <summary>Gets/sets the PromptText.</summary>
        /// <value>The PromptText.</value>
        [JsonProperty]
        public string PromptText {
            get => _PromptText;
            set {
                _PromptText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAutoNumberValue Property
        private bool _IsAutoNumberValue = default;
        /// <summary>Gets/sets the IsAutoNumberValue.</summary>
        /// <value>The IsAutoNumberValue.</value>
        [JsonProperty]
        public bool IsAutoNumberValue {
            get => _IsAutoNumberValue;
            set {
                _IsAutoNumberValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AutoNumberSeed Property
        private long _AutoNumberSeed = default;
        /// <summary>Gets/sets the AutoNumberSeed.</summary>
        /// <value>The AutoNumberSeed.</value>
        [JsonProperty]
        public long AutoNumberSeed {
            get => _AutoNumberSeed;
            set {
                _AutoNumberSeed = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AutoNumberIncrement Property
        private long _AutoNumberIncrement = default;
        /// <summary>Gets/sets the AutoNumberIncrement.</summary>
        /// <value>The AutoNumberIncrement.</value>
        [JsonProperty]
        public long AutoNumberIncrement {
            get => _AutoNumberIncrement;
            set {
                _AutoNumberIncrement = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
