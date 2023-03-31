using ApplicationFramework.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace MakeCompositeIcon {
    internal partial class OptionsWindowView : ViewModelBase {
        public OptionsWindowView() {
            Title = "Options [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Options";
            IsUseLastPositionChecked = App.ThisApp.IsUseLastPositionChecked;
            AreGuidesShown = App.ThisApp.AreGuidesShown;
            FontSizes = new ObservableCollection<double>();
            for (int i = 10; i < 40; i++) {
                FontSizes.Add(i);
            }
            SelectedFontSize = (double)App.ThisApp.Resources["RootFontSize"];
            FontFamilies = new ObservableCollection<FontFamily>(
                Fonts.SystemFontFamilies.OrderBy(x => x.Source)
            );
            SelectedFont = App.ThisApp.Resources["AppFontFamily"].As<FontFamily>();
            IsAlwaysPromptSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application", 
                nameof(IsAlwaysPromptSelected), true);
            IsAlwaysDeleteSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application", 
                nameof(IsAlwaysDeleteSelected), false);
            IsAlwaysRecycleSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application", 
                nameof(IsAlwaysRecycleSelected), false);
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsUseLastPositionChecked Property
        private bool _IsUseLastPositionChecked = default;
        /// <summary>Gets/sets the IsUseLastPositionChecked.</summary>
        /// <value>The IsUseLastPositionChecked.</value>
        public bool IsUseLastPositionChecked {
            get => _IsUseLastPositionChecked;
            set {
                _IsUseLastPositionChecked = value;
                App.ThisApp.IsUseLastPositionChecked = IsUseLastPositionChecked;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AreGuidesShown Property
        private bool _AreGuidesShown = default;
        /// <summary>Gets/sets the AreGuidesShown.</summary>
        /// <value>The AreGuidesShown.</value>
        public bool AreGuidesShown {
            get => _AreGuidesShown;
            set {
                _AreGuidesShown = value;
                App.ThisApp.AreGuidesShown = AreGuidesShown;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontSizes Property
        private ObservableCollection<double> _FontSizes = default;
        /// <summary>Gets/sets the FontSizes.</summary>
        /// <value>The FontSizes.</value>
        public ObservableCollection<double> FontSizes {
            get => _FontSizes;
            set {
                _FontSizes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedFontSize Property
        private double _SelectedFontSize = default;
        /// <summary>Gets/sets the SelectedFontSize.</summary>
        /// <value>The SelectedFontSize.</value>
        public double SelectedFontSize {
            get => _SelectedFontSize;
            set {
                _SelectedFontSize = value;
                App.ThisApp.BaseFontSize = SelectedFontSize;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedIcon Property
        private RecycledCompositeIcon _SelectedIcon = default;
        /// <summary>Gets/sets the SelectedIcon.</summary>
        /// <value>The SelectedIcon.</value>
        public RecycledCompositeIcon SelectedIcon {
            get => _SelectedIcon;
            set {
                _SelectedIcon = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontFamilies Property
        private ObservableCollection<FontFamily> _FontFamilies = default;
        /// <summary>Gets/sets the FontFamilies.</summary>
        /// <value>The FontFamilies.</value>
        public ObservableCollection<FontFamily> FontFamilies {
            get => _FontFamilies;
            set {
                _FontFamilies = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedFont Property
        private FontFamily _SelectedFont = default;
        /// <summary>Gets/sets the SelectedFont.</summary>
        /// <value>The SelectedFont.</value>
        public FontFamily SelectedFont {
            get => _SelectedFont;
            set {
                _SelectedFont = value;
                App.ThisApp.BaseFontFamily = SelectedFont;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAlwaysPromptSelected Property
        private bool _IsAlwaysPromptSelected = default;
        /// <summary>Gets/sets the IsAlwaysPromptSelected.</summary>
        /// <value>The IsAlwaysPromptSelected.</value>
        public bool IsAlwaysPromptSelected {
            get => _IsAlwaysPromptSelected;
            set {
                _IsAlwaysPromptSelected = value;
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                    nameof(IsAlwaysPromptSelected), IsAlwaysPromptSelected);
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAlwaysDeleteSelected Property
        private bool _IsAlwaysDeleteSelected = default;
        /// <summary>Gets/sets the IsAlwaysDeleteSelected.</summary>
        /// <value>The IsAlwaysDeleteSelected.</value>
        public bool IsAlwaysDeleteSelected {
            get => _IsAlwaysDeleteSelected;
            set {
                _IsAlwaysDeleteSelected = value;
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                    nameof(IsAlwaysDeleteSelected), IsAlwaysDeleteSelected);
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAlwaysRecycleSelected Property
        private bool _IsAlwaysRecycleSelected = default;
        /// <summary>Gets/sets the IsAlwaysRecycleSelected.</summary>
        /// <value>The IsAlwaysRecycleSelected.</value>
        public bool IsAlwaysRecycleSelected {
            get => _IsAlwaysRecycleSelected;
            set {
                _IsAlwaysRecycleSelected = value;
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                    nameof(IsAlwaysRecycleSelected), IsAlwaysRecycleSelected);
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
