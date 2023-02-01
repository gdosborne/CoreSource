using Azure.Core.Pipeline;
using Common.Applicationn;
using Common.Applicationn.Logging;
using CongregationManager.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CongregationManager.Extensibility {
    public abstract class ExtensionBase : INotifyPropertyChanged {
        public ExtensionBase() { }

        protected ExtensionBase(string name, string glyph) {
            Name = name;
            Glyph = glyph;
        }

        /// <summary>
        /// Initializes the extension
        /// </summary>
        /// <param name="dataDirectory">The data directory</param>
        /// <param name="tempDirectory">The temp directory</param>
        /// <param name="appSettings">The application settings</param>
        /// <param name="logger">The logger</param>
        /// <param name="dataManager">The data manager</param>
        public abstract void Initialize(string dataDirectory, string tempDirectory, Settings appSettings,
            ApplicationLogger logger, DataManager dataManager);

        /// <summary>
        /// Destroy method
        /// </summary>
        public abstract void Destroy();

        /// <summary>
        /// RetrieveResources event
        /// </summary>
        public virtual event RetrieveResourcesHandler RetrieveResources;

        /// <summary>
        /// SaveExtensionData event
        /// </summary>
        public virtual event SaveExtensionDataHandler? SaveExtensionData;

        /// <summary>
        /// AddControlItem event
        /// </summary>
        public virtual event AddControlItemHandler? AddControlItem;

        /// <summary>
        /// RemoveControlItem event
        /// </summary>
        public virtual event RemoveControlItemHandler? RemoveControlItem;

        /// <summary>
        /// PropertyChanged event
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Glyph Property
        private string _Glyph = default;
        /// <summary>Gets/sets the Glyph.</summary>
        /// <value>The Glyph.</value>
        public string Glyph {
            get => _Glyph;
            set {
                _Glyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Panel Property
        private IExtensionPanel _Panel = default;
        /// <summary>Gets/sets the Panel.</summary>
        /// <value>The Panel.</value>
        public IExtensionPanel Panel {
            get => _Panel;
            set {
                _Panel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsEnabled Property
        private bool? _IsEnabled = default;
        /// <summary>Gets/sets the IsEnabled.</summary>
        /// <value>The IsEnabled.</value>
        public bool? IsEnabled {
            get => _IsEnabled;
            set {
                _IsEnabled = value;
                Settings.AddOrUpdateSetting($"{Name} Extension", "IsEnabled", IsEnabled);
                ToggleLoadedControls(IsEnabled.Value);
                OnPropertyChanged();
            }
        }
        #endregion

        public abstract void ToggleLoadedControls(bool value);

        /// <summary>Saves the data</summary>
        public abstract void Save();

        #region DataDirectory Property
        private string _DataDirectory = default;
        /// <summary>Gets/sets the DataDirectory.</summary>
        /// <value>The DataDirectory.</value>
        public string DataDirectory {
            get => _DataDirectory;
            set {
                _DataDirectory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TempDirectory Property
        private string _TempDirectory = default;
        /// <summary>Gets/sets the TempDirectory.</summary>
        /// <value>The TempDirectory.</value>
        public string TempDirectory {
            get => _TempDirectory;
            set {
                _TempDirectory = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Settings Property
        private Settings _Settings = default;
        /// <summary>Gets/sets the Settings.</summary>
        /// <value>The Settings.</value>
        public Settings Settings {
            get => _Settings;
            set {
                _Settings = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Logger Property
        private ApplicationLogger _Logger = default;
        /// <summary>Gets/sets the Logger.</summary>
        /// <value>The Logger.</value>
        public ApplicationLogger Logger {
            get => _Logger;
            set {
                _Logger = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataManager Property
        private DataManager _DataManager = default;
        /// <summary>Gets/sets the DataManager.</summary>
        /// <value>The DataManager.</value>
        public DataManager DataManager {
            get => _DataManager;
            set {
                _DataManager = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Filename Property
        private string _Filename = default;
        /// <summary>Gets/sets the Filename.</summary>
        /// <value>The Filename.</value>
        public string Filename {
            get => _Filename;
            set {
                _Filename = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
