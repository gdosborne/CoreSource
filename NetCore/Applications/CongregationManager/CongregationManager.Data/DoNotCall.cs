using Newtonsoft.Json;
using System;

namespace CongregationManager.Data {
    [JsonObject("donotcall")]
    public class DoNotCall : ItemBase {

        #region Address Property
        private string _Address = default;
        /// <summary>Gets/sets the Address.</summary>
        /// <value>The Address.</value>
        [JsonProperty("address")]
        public string Address {
            get => _Address;
            set {
                _Address = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HouseholderName Property
        private string _HouseholderName = default;
        /// <summary>Gets/sets the HouseholderName.</summary>
        /// <value>The HouseholderName.</value>
        [JsonProperty("householdername")]
        public string HouseholderName {
            get => _HouseholderName;
            set {
                _HouseholderName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DateReported Property
        private DateTime? _DateReported = default;
        /// <summary>Gets/sets the DateReported.</summary>
        /// <value>The DateReported.</value>
        [JsonProperty("datereported")]
        public DateTime? DateReported {
            get => _DateReported;
            set {
                _DateReported = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ReportedBy Property
        private Member _ReportedBy = default;
        /// <summary>Gets/sets the ReportedBy.</summary>
        /// <value>The ReportedBy.</value>
        [JsonIgnore]
        public Member ReportedBy {
            get => _ReportedBy;
            set {
                _ReportedBy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ReportedByID Property
        private int _ReportedByID = default;
        /// <summary>Gets/sets the ReportedByID.</summary>
        /// <value>The ChecedOutByID.</value>
        [JsonProperty("reportedbyid")]
        public int ReportedByID {
            get => _ReportedByID;
            set {
                _ReportedByID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DateCreated Property
        private DateTime _DateCreated = default;
        /// <summary>Gets/sets the DateCreated.</summary>
        /// <value>The DateCreated.</value>
        [JsonProperty("datecreated")]
        public DateTime DateCreated {
            get => _DateCreated;
            set {
                _DateCreated = value;
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
