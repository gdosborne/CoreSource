using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CongregationManager.Data {
    [JsonObject("territory")]
    public class Territory : ItemBase {
        public Territory() {
            DoNotCalls = new List<DoNotCall>();
            History = new List<TerritoryHistory>();
        }

        public enum Statuses {
            Active,
            Suspended
        }

        #region Status Property
        private Statuses _Status = default;
        /// <summary>Gets/sets the Status.</summary>
        /// <value>The Status.</value>
        [JsonProperty("status")]
        public Statuses Status {
            get => _Status;
            set {
                _Status = value;
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
        private List<DoNotCall> _DoNotCalls = default;
        /// <summary>Gets/sets the DoNotCalls.</summary>
        /// <value>The DoNotCalls.</value>
        [JsonProperty("donotcalls")]
        public List<DoNotCall> DoNotCalls {
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
        private List<TerritoryHistory> _History = default;
        /// <summary>Gets/sets the History.</summary>
        /// <value>The History.</value>
        [JsonProperty("history")]
        public List<TerritoryHistory> History {
            get => _History;
            set {
                _History = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ImagePath Property
        private string _ImagePath = default;
        /// <summary>Gets/sets the ImagePath.</summary>
        /// <value>The ImagePath.</value>
        [JsonProperty("imagepath")]
        public string ImagePath {
            get => _ImagePath;
            set {
                _ImagePath = value;
                if (string.IsNullOrEmpty(ImagePath))
                    Image = null;
                else if (File.Exists(ImagePath))
                    Image = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
                OnPropertyChanged();
            }
        }
        #endregion

        #region Image Property
        private ImageSource _Image = default;
        /// <summary>Gets/sets the Image.</summary>
        /// <value>The Image.</value>
        [JsonIgnore]
        public ImageSource Image {
            get => _Image;
            set {
                _Image = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region LastHistory Property
        private TerritoryHistory _LastHistory = default;
        /// <summary>Gets/sets the LastHistory.</summary>
        /// <value>The LastHistory.</value>
        [JsonIgnore]
        public TerritoryHistory LastHistory {
            get => History.OrderByDescending(x => x.CheckOutDate).FirstOrDefault();
            private set {
                _LastHistory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        override public string ToString() =>
           $"Territory {Number}";

    }
}
