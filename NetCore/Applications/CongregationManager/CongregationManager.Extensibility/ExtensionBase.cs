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

        /// <summary>Gets the Name.</summary>
        /// <value>The Name.</value>
        public string? Name { get; protected set; } = default;

        /// <summary>Gets the Glyph.</summary>
        /// <value>The Glyph.</value>
        public string? Glyph { get; protected set; } = default;

        /// <summary>Gets the UserControl.</summary>
        /// <value>The UserControl.</value>
        public IExtensionPanel? Panel { get; protected set; } = default;

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

        /// <summary>
        /// The data directory
        /// </summary>
        public string? DataDirectory { get; protected set; }

        /// <summary>
        /// The temp directory
        /// </summary>
        public string? TempDirectory { get; protected set; }

        /// <summary>
        /// The settings
        /// </summary>
        public Settings? Settings { get; protected set; }

        /// <summary>
        /// The logger
        /// </summary>
        public ApplicationLogger? Logger { get; protected set; }

        /// <summary>
        /// The data manager
        /// </summary>
        public DataManager? DataManager { get; set; }

        /// <summary>
        /// The file name
        /// </summary>
        public string? Filename { get; set; }

    }
}
