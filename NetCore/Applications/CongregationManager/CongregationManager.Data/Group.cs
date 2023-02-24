using Newtonsoft.Json;
using System.Collections.Generic;

namespace CongregationManager.Data {
    [JsonObject("group")]
    public class Group : ItemBase {
        public Group() {
            MemberIDs = new List<int>();
        }

        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        [JsonProperty("name")]
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OverseerMemberID Property
        private int _OverseerMemberID = default;
        /// <summary>Gets/sets the OverseerMemberID.</summary>
        /// <value>The OverseerMemberID.</value>
        [JsonProperty("overseermemberid")]
        public int OverseerMemberID {
            get => _OverseerMemberID;
            set {
                _OverseerMemberID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AssistantMemberID Property
        private int _AssistantMemberID = default;
        /// <summary>Gets/sets the AssistantMemberID.</summary>
        /// <value>The AssistantMemberID.</value>
        public int AssistantMemberID {
            get => _AssistantMemberID;
            set {
                _AssistantMemberID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MemberIDs Property
        private List<int> _MemberIDs = default;
        /// <summary>Gets/sets the MemberIDs.</summary>
        /// <value>The MemberIDs.</value>
        [JsonProperty("memberids")]
        public List<int> MemberIDs {
            get => _MemberIDs;
            set {
                _MemberIDs = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
