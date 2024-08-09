using Common;
using Common.Logging;
using Common.Primitives;
using CongregationManager.Data;
using Controls.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CongregationManager.Extensibility {
    public abstract class ExtensionBase : INotifyPropertyChanged {
        public ExtensionBase() { }

        protected ExtensionBase(string name, char glyph, string glyphStyleName) {
            Name = name;
            Glyph = glyph;
            GlyphStyleName = glyphStyleName;

            AddedControls ??= new Dictionary<ExtensionBase, List<object>>();
            if (!AddedControls.ContainsKey(this))
                AddedControls.Add(this, new List<object>());
        }

        /// <summary>
        /// Initializes the extension
        /// </summary>
        /// <param name="dataDirectory">The data directory</param>
        /// <param name="tempDirectory">The temp directory</param>
        /// <param name="appSettings">The application settings</param>
        /// <param name="logger">The logger</param>
        /// <param name="dataManager">The data manager</param>
        public abstract void Initialize(string dataDirectory, string tempDirectory, AppSettings appSettings,
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
        /// PropertyChanged event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected static Dictionary<ExtensionBase, List<object>> AddedControls { get; set; }

        public void SetUIControls(ToolBar toolbar, Menu menu, ResourceDictionary resources) {            
            Toolbar = toolbar;
            Menu = menu;
            Resources = resources;
        }

        protected ToolBar Toolbar { get; private set; }
        
        protected Menu Menu { get; private set; }
        
        protected ResourceDictionary Resources { get; private set; }

        protected MenuItem AddTopLevelMenuItem(string text) {
            var result = new MenuItem { Header = text };
            Menu.Items.Add(result);
            AddedControls[this].Add(result);
            return result;
        }

        protected void AddMenuItem(string text, MenuItem parent, string iconName, ICommand command, string fontFamilyResourceName) {
            var result = new MenuItem {
                Header = text,
                Icon = new FontIcon {
                    FontFamily = Resources[fontFamilyResourceName].As<FontFamily>(),
                    FontSize = Resources["StandardFontSize"].CastTo<double>(),
                    Glyph = Resources[iconName].CastTo<string>(),
                    Background = Resources["WindowBackground"].As<SolidColorBrush>()
                },
                Command = command,
            };
            if (parent != null)
                parent.Items.Add(result);
            AddedControls[this].Add(result);
        }

        protected void AddToolbarLabel(string text) {
            var result = new TextBlock {
                Text = text,
                Style = Resources["ToolbarLabel"].As<Style>()
            };
            Toolbar.Items.Add(result);
            AddedControls[this].Add(result);
        }

        protected void AddToolbarButton(string text, string iconName, ICommand command, 
                string fontFamilyResourceName) {
            var result = new Button {
                ToolTip = text,
                Content = new FontIcon {
                    FontFamily = Resources[fontFamilyResourceName].As<FontFamily>(),
                    FontSize = Resources["StandardFontSize"].CastTo<double>(),
                    Glyph = Resources[iconName].CastTo<string>(),
                    Style = Resources["ToolbarIcon"].As<Style>()
                },
                Command = command
            };
            Toolbar.Items.Add(result);
            AddedControls[this].Add(result);
        }

        protected void AddMenuSeparator(MenuItem parent) {
            var result = new Separator();
            if (parent != null)
                parent.Items.Add(result);
            AddedControls[this].Add(result);
        }

        protected void AddToolbarSeparator() {
            var result = new Separator();
            Toolbar.Items.Add(result);
            AddedControls[this].Add(result);
        }

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
        private char _Glyph = default;
        /// <summary>Gets/sets the Glyph.</summary>
        /// <value>The Glyph.</value>
        public char Glyph {
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
                Settings.AddOrUpdateSetting($"{Name} Extension", "IsEnabled", 
                    IsEnabled.HasValue ? true : IsEnabled.Value);
                ToggleLoadedControls(IsEnabled.Value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region GlyphStyleName Property
        private string _GlyphStyleName = default;
        /// <summary>Gets/sets the GlyphStyleName.</summary>
        /// <value>The GlyphStyleName.</value>
        public string GlyphStyleName {
            get => _GlyphStyleName;
            set {
                _GlyphStyleName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TabItem Property
        private TabItem _TabItem = default;
        /// <summary>Gets/sets the TabItem.</summary>
        /// <value>The TabItem.</value>
        public TabItem TabItem {
            get => _TabItem;
            set {
                _TabItem = value;
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
        private AppSettings _Settings = default;
        /// <summary>Gets/sets the Settings.</summary>
        /// <value>The Settings.</value>
        public AppSettings Settings {
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
