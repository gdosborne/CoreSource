using Common.Applicationn;
using Common.MVVMFramework;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CongregationManager.Data {
    [JsonObject("territory")]
    public class Territory : ItemBase {
        public Territory() {
            DoNotCalls = new ObservableCollection<DoNotCall>();
            History = new ObservableCollection<TerritoryHistory>();
        }

        #region CongregationID Property
        private int _CongregationID = default;
        /// <summary>Gets/sets the CongregationID.</summary>
        /// <value>The CongregationID.</value>
        [JsonProperty("congregationid")]
        public int CongregationID {
            get => _CongregationID;
            set {
                _CongregationID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Number Property
        private string _Number = default;
        /// <summary>Gets/sets the Number.</summary>
        /// <value>The Number.</value>
        [JsonProperty("number")]
        public string Number {
            get => _Number;
            set {
                _Number = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DoNotCalls Property
        private ObservableCollection<DoNotCall> _DoNotCalls = default;
        /// <summary>Gets/sets the DoNotCalls.</summary>
        /// <value>The DoNotCalls.</value>
        [JsonProperty("donotcalls")]
        public ObservableCollection<DoNotCall> DoNotCalls {
            get => _DoNotCalls;
            set {
                _DoNotCalls = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Notes Property
        private string _Notes = default;
        /// <summary>Gets/sets the Notes.</summary>
        /// <value>The Notes.</value>
        [JsonProperty("notes")]
        public string Notes {
            get => _Notes;
            set {
                _Notes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region History Property
        private ObservableCollection<TerritoryHistory> _History = default;
        /// <summary>Gets/sets the History.</summary>
        /// <value>The History.</value>
        [JsonProperty("history")]
        public ObservableCollection<TerritoryHistory> History {
            get => _History;
            set {
                _History = value;
                OnPropertyChanged();
            }
        }
        #endregion

        override public string ToString() =>
           $"Territory {Number}";

    }
}
