using Common.Applicationn;
using Common.MVVMFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CongregationManager.Data {
    /// <summary>
    /// Congregation object
    /// </summary>
    [JsonObject("congregation")]
    public class Congregation : ItemBase, ICloneable {
        /// <summary>
        /// Initializes an instance of the Congregation object
        /// </summary>
        public Congregation() {
            Groups = new List<Group>();
            Members = new List<Member>();
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

        #region Filename Property
        private string _Filename = default;
        /// <summary>Gets/sets the Filename.</summary>
        /// <value>The Filename.</value>
        [JsonIgnore]
        public string Filename {
            get => _Filename;
            set {
                _Filename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataPath Property
        private string _DataPath = default;
        /// <summary>Gets/sets the DataPath.</summary>
        /// <value>The DataPath.</value>
        [JsonIgnore]
        public string DataPath {
            get => _DataPath;
            set {
                _DataPath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Members Property
        private List<Member> _Members = default;
        /// <summary>Gets/sets the Members.</summary>
        /// <value>The Members.</value>
        [JsonProperty("members")]
        public List<Member> Members {
            get => _Members;
            set {
                _Members = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Groups Property
        private List<Group> _Groups = default;
        /// <summary>Gets/sets the Groups.</summary>
        /// <value>The Groups.</value>
        [JsonProperty("groups")]
        public List<Group> Groups {
            get => _Groups;
            set {
                _Groups = value;
                OnPropertyChanged();
            }
        }
        #endregion

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

        #region City Property
        private string _City = default;
        /// <summary>Gets/sets the City.</summary>
        /// <value>The City.</value>
        [JsonProperty("city")]
        public string City {
            get => _City;
            set {
                _City = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region StateProvence Property
        private string _StateProvence = default;
        /// <summary>Gets/sets the StateProvence.</summary>
        /// <value>The StateProvence.</value>
        [JsonProperty("stateprovence")]
        public string StateProvence {
            get => _StateProvence;
            set {
                _StateProvence = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PostalCode Property
        private string _PostalCode = default;
        /// <summary>Gets/sets the PostalCode.</summary>
        /// <value>The PostalCode.</value>
        [JsonProperty("postalcode")]
        public string PostalCode {
            get => _PostalCode;
            set {
                _PostalCode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Telephone Property
        private string _Telephone = default;
        /// <summary>Gets/sets the Telephone.</summary>
        /// <value>The Telephone.</value>
        [JsonProperty("telephone")]
        public string Telephone {
            get => _Telephone;
            set {
                _Telephone = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MeetingTime Property
        private TimeSpan _MeetingTime = default;
        /// <summary>Gets/sets the MeetingTime.</summary>
        /// <value>The MeetingTime.</value>
        [JsonProperty("meetingtime")]
        public TimeSpan MeetingTime {
            get => _MeetingTime;
            set {
                _MeetingTime = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region MeetingDay Property
        private DayOfWeek _MeetingDay = default;
        /// <summary>Gets/sets the MeetingDay.</summary>
        /// <value>The MeetingDay.</value>
        [JsonProperty("meetingday")]
        public DayOfWeek MeetingDay {
            get => _MeetingDay;
            set {
                _MeetingDay = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsLocal Property
        private bool _IsLocal = default;
        /// <summary>Gets/sets the IsLocal.</summary>
        /// <value>The IsLocal.</value>
        [JsonProperty("islocal")]
        public bool IsLocal {
            get => _IsLocal;
            set {
                _IsLocal = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsNew Property
        private bool _IsNew = default;
        /// <summary>Gets/sets the IsNew.</summary>
        /// <value>The IsNew.</value>
        [JsonIgnore]
        public bool IsNew {
            get => _IsNew;
            set {
                _IsNew = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Saves the congregation
        /// </summary>
        /// <param name="password">The password for data encryption</param>
        public void Save(string password) {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(this, settings);
            try {
#if DEBUG
                File.WriteAllText(Path.Combine(DataPath, Filename), json);
#else

                var enc = default(string);
                using (var crypto = new Crypto(password)) {
                    password = null;
                    enc = crypto.Encrypt<string>(json);
                }
                File.WriteAllText(Path.Combine(DataPath, Filename), enc);
#endif
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Opens the congregation data file
        /// </summary>
        /// <param name="filename">The filename</param>
        /// <param name="password">The password for data encryption</param>
        /// <returns>Congregation if successful, null otherwise</returns>
        public static Congregation? OpenFromFile(string filename, string password) {
            var json = default(string);
            using (var crypto = new Crypto(password)) {
                password = null;

                var data = File.ReadAllText(filename);
#if DEBUG
                json = data;
#else
                json = crypto.Decrypt<string>(data);
#endif                
            }

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            return JsonConvert.DeserializeObject<Congregation>(json, settings);
        }

        /// <summary>
        /// Informs parent that the congregation is to be saved
        /// </summary>
        public event EventHandler SaveThisItem;
        /// <summary>
        /// Informs the parent that the congregation is to be edited
        /// </summary>
        public event EventHandler EditThisItem;

        #region SaveCommand
        private DelegateCommand _SaveCommand = default;
        /// <summary>Gets the Save command.</summary>
        /// <value>The Save command.</value>
        [JsonIgnore]
        public DelegateCommand SaveCommand => _SaveCommand ?? (_SaveCommand = new DelegateCommand(Save, ValidateSaveState));
        private bool ValidateSaveState(object state) => true;
        private void Save(object state) {
            this.SaveThisItem?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region RevertCommand
        private DelegateCommand _RevertCommand = default;
        /// <summary>Gets the Revert command.</summary>
        /// <value>The Revert command.</value>
        [JsonIgnore]
        public DelegateCommand RevertCommand => _RevertCommand ?? (_RevertCommand = new DelegateCommand(Revert, ValidateRevertState));
        private bool ValidateRevertState(object state) => true;
        private void Revert(object state) {
            var props = this.GetType()
                .GetProperties()
                .Where(x => x.PropertyType != typeof(DelegateCommand))
                .Select(x => x.Name);
            props.ToList().ForEach(x => RevertField(x));
        }
        #endregion

        #region EditCommand
        private DelegateCommand _EditCommand = default;
        /// <summary>Gets the Edit command.</summary>
        /// <value>The Edit command.</value>
        [JsonIgnore]
        public DelegateCommand EditCommand => _EditCommand ?? (_EditCommand = new DelegateCommand(Edit, ValidateEditState));
        private bool ValidateEditState(object state) => true;
        private void Edit(object state) {
            this.EditThisItem?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        private void RevertField(string name) {
            var prop = this.GetType().GetProperty(name);
            if (prop == null)
                return;
            prop.SetValue(this, prop.GetValue(Original));
        }

        internal Congregation Original { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        override public string ToString() =>
            Name;

        /// <summary>
        /// Clones the congregation
        /// </summary>
        /// <returns>Clone of the Congregation</returns>
        public object Clone() {
            var result = this.MemberwiseClone();
            return result;
        }
    }
}
