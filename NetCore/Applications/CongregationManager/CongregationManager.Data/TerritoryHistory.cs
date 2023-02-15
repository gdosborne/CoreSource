using Newtonsoft.Json;
using System;

namespace CongregationManager.Data {
    [JsonObject("territoryhistory")]
    public class TerritoryHistory : ItemBase {

        #region CheckOutDate Property
        private DateTime _CheckOutDate = default;
        /// <summary>Gets/sets the CheckOutDate.</summary>
        /// <value>The CheckOutDate.</value>
        [JsonProperty("checkoutdate")]
        public DateTime CheckOutDate {
            get => _CheckOutDate;
            set {
                _CheckOutDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CheckInDate Property
        private DateTime? _CheckInDate = default;
        /// <summary>Gets/sets the CheckInDate.</summary>
        /// <value>The CheckInDate.</value>
        [JsonProperty("checkindate")]
        public DateTime? CheckInDate {
            get => _CheckInDate;
            set {
                _CheckInDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CheckedOutByID Property
        private int _CheckedOutByID = default;
        /// <summary>Gets/sets the CheckedOutByID.</summary>
        /// <value>The ChecedOutByID.</value>
        [JsonProperty("checkedoutbyid")]
        public int CheckedOutByID {
            get => _CheckedOutByID;
            set {
                _CheckedOutByID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CheckedOutBy Property
        private Member _CheckedOutBy = default;
        /// <summary>Gets/sets the CheckedOutBy.</summary>
        /// <value>The CheckedOutBy.</value>
        [JsonIgnore]
        public Member CheckedOutBy {
            get => _CheckedOutBy;
            set {
                _CheckedOutBy = value;
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

    }
}
