using ApplicationFramework.Media;
using Common.Application.Linq;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MakeCompositeIcon {
    internal partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = $"{App.Current.As<App>().ApplicationName} [designer]";
            SelectedIcon = default;
            IsSingleColorSelected = true;
            IsEditorEnabled = false;
            Icons = new ObservableCollection<CompositeIcon>();
            Fonts = new ObservableCollection<System.Windows.Media.FontFamily>();
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.Current.As<App>().ApplicationName;
            var files = new DirectoryInfo(App.Current.As<App>().FilesDirectory).GetFiles("*.compo");
            foreach (var file in files.OrderBy(x => x.Name)) {
                var icon = CompositeIcon.FromFile(file.FullName);
                Icons.Add(icon);
            }

            var fonts = System.Windows.Media.Fonts.SystemFontFamilies.OrderBy(x => x.Source);
            Fonts.AddRange(fonts);
        }

        #region IsSingleColorSelected Property
        private bool _IsSingleColorSelected = default;
        /// <summary>Gets/sets the IsSingleColorSelected.</summary>
        /// <value>The IsSingleColorSelected.</value>
        public bool IsSingleColorSelected {
            get => _IsSingleColorSelected;
            set {
                _IsSingleColorSelected = value;
                SecondaryBrushVisibility = IsSingleColorSelected ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryBrushVisibility Property
        private Visibility _SecondaryBrushVisibility = default;
        /// <summary>Gets/sets the SecondaryBrushVisibility.</summary>
        /// <value>The SecondaryBrushVisibility.</value>
        public Visibility SecondaryBrushVisibility {
            get => _SecondaryBrushVisibility;
            set {
                _SecondaryBrushVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsEditorEnabled Property
        private bool _IsEditorEnabled = default;
        /// <summary>Gets/sets the IsEditorEnabled.</summary>
        /// <value>The IsEditorEnabled.</value>
        public bool IsEditorEnabled {
            get => _IsEditorEnabled;
            set {
                _IsEditorEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsOverlaySelected Property
        private bool _IsOverlaySelected = default;
        /// <summary>Gets/sets the IsOverlaySelected.</summary>
        /// <value>The IsOverlaySelected.</value>
        public bool IsOverlaySelected {
            get => _IsOverlaySelected;
            set {
                _IsOverlaySelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSubscriptSelected Property
        private bool _IsSubscriptSelected = default;
        /// <summary>Gets/sets the IsSubscriptSelected.</summary>
        /// <value>The IsSubscriptSelected.</value>
        public bool IsSubscriptSelected {
            get => _IsSubscriptSelected;
            set {
                _IsSubscriptSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSingleSizeSelected Property
        private bool _IsSingleSizeSelected = default;
        /// <summary>Gets/sets the IsSingleSizeSelected.</summary>
        /// <value>The IsSingleSizeSelected.</value>
        public bool IsSingleSizeSelected {
            get => _IsSingleSizeSelected;
            set {
                _IsSingleSizeSelected = value;
                if (IsSingleSizeSelected) {
                    //SecondarySize = PrimarySize;
                    SecondSizeVisibility = Visibility.Collapsed;
                }
                else {
                    SecondSizeVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondSizeVisibility Property
        private Visibility _SecondSizeVisibility = default;
        /// <summary>Gets/sets the SecondSizeVisibility.</summary>
        /// <value>The SecondSizeVisibility.</value>
        public Visibility SecondSizeVisibility {
            get => _SecondSizeVisibility;
            set {
                _SecondSizeVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Icons Property
        private ObservableCollection<CompositeIcon> _Icons = default;
        /// <summary>Gets/sets the Icons.</summary>
        /// <value>The Icons.</value>
        public ObservableCollection<CompositeIcon> Icons {
            get => _Icons;
            set {
                _Icons = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSingleFontSelected Property
        private bool _IsSingleFontSelected = default;
        /// <summary>Gets/sets the IsSingleFontSelected.</summary>
        /// <value>The IsSingleFontSelected.</value>
        public bool IsSingleFontSelected {
            get => _IsSingleFontSelected;
            set {
                _IsSingleFontSelected = value;
                SecondaryFontVisibility = IsSingleFontSelected ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryFontVisibility Property
        private Visibility _SecondaryFontVisibility = default;
        /// <summary>Gets/sets the SecondaryFontVisibility.</summary>
        /// <value>The SecondaryFontVisibility.</value>
        public Visibility SecondaryFontVisibility {
            get => _SecondaryFontVisibility;
            set {
                _SecondaryFontVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedIcon Property
        private CompositeIcon _SelectedIcon = default;
        /// <summary>Gets/sets the SelectedIcon.</summary>
        /// <value>The SelectedIcon.</value>
        public CompositeIcon SelectedIcon {
            get => _SelectedIcon;
            set {
                if (SelectedIcon != null) {
                    SelectedIcon.PropertyChanged -= SelectedIcon_PropertyChanged;
                }
                _SelectedIcon = value;
                if (SelectedIcon != null) {
                    IsOverlaySelected = SelectedIcon.IconType == CompositeIconData.IconTypes.FullOverlay;
                    IsSubscriptSelected = SelectedIcon.IconType == CompositeIconData.IconTypes.SubscriptedOverlay;
                    IsSingleColorSelected = SelectedIcon.SecondaryBrush == null
                        || (SelectedIcon.PrimaryBrush.Color.ToHexValue() == SelectedIcon.SecondaryBrush.Color.ToHexValue());
                    IsSingleSizeSelected = SelectedIcon.PrimarySize == SelectedIcon.SecondarySize;
                    SelectedIcon.PropertyChanged += SelectedIcon_PropertyChanged;
                }
                IsEditorEnabled = SelectedIcon != null;
                OnPropertyChanged();
            }
        }

        private void SelectedIcon_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var icon = sender.As<CompositeIcon>();
            if (e.PropertyName == nameof(CompositeIcon.PrimaryBrush)) {
                if (IsSingleColorSelected) {
                    icon.SecondaryBrush = SelectedIcon.PrimaryBrush;
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.PrimarySize)) {
                if (IsSingleSizeSelected) {
                    icon.SecondarySize = SelectedIcon.PrimarySize;
                }
            }
        }
        #endregion

        #region Fonts Property
        private ObservableCollection<System.Windows.Media.FontFamily> _Fonts = default;
        /// <summary>Gets/sets the Fonts.</summary>
        /// <value>The Fonts.</value>
        public ObservableCollection<System.Windows.Media.FontFamily> Fonts {
            get => _Fonts;
            set {
                _Fonts = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private bool isSettingExpanded = false;

        #region IsIconTypeExpanded Property
        private bool _IsIconTypeExpanded = default;
        /// <summary>Gets/sets the IsIconTypeExpanded.</summary>
        /// <value>The IsIconTypeExpanded.</value>
        public bool IsIconTypeExpanded {
            get => _IsIconTypeExpanded;
            set {
                _IsIconTypeExpanded = value;
                OnPropertyChanged();

                if (!value)
                    return;
                IsGlyphExpanded = false;
                IsSizeExpanded = false;
                IsColorExpanded = false;
                IsFontExpanded = false;
            }
        }
        #endregion

        #region IsColorExpanded Property
        private bool _IsColorExpanded = default;
        /// <summary>Gets/sets the IsColorExpanded.</summary>
        /// <value>The IsColorExpanded.</value>
        public bool IsColorExpanded {
            get => _IsColorExpanded;
            set {
                _IsColorExpanded = value;
                OnPropertyChanged();

                if (!value)
                    return;
                IsGlyphExpanded = false;
                IsIconTypeExpanded = false;
                IsSizeExpanded = false;
                IsFontExpanded = false;
            }
        }
        #endregion

        #region IsFontExpanded Property
        private bool _IsFontExpanded = default;
        /// <summary>Gets/sets the IsFontExpanded.</summary>
        /// <value>The IsFontExpanded.</value>
        public bool IsFontExpanded {
            get => _IsFontExpanded;
            set {
                _IsFontExpanded = value;
                OnPropertyChanged();

                if (!value)
                    return;
                IsGlyphExpanded = false;
                IsIconTypeExpanded = false;
                IsSizeExpanded = false;
                IsColorExpanded = false;
            }
        }
        #endregion

        #region IsSizeExpanded Property
        private bool _IsSizeExpanded = default;
        /// <summary>Gets/sets the IsSizeExpanded.</summary>
        /// <value>The IsSizeExpanded.</value>
        public bool IsSizeExpanded {
            get => _IsSizeExpanded;
            set {
                _IsSizeExpanded = value;
                OnPropertyChanged();

                if (!value)
                    return;
                IsGlyphExpanded = false;
                IsIconTypeExpanded = false;
                IsColorExpanded = false;
                IsFontExpanded = false;
            }
        }
        #endregion

        #region IsGlyphExpanded Property
        private bool _IsGlyphExpanded = default;
        /// <summary>Gets/sets the IsGlyphExpanded.</summary>
        /// <value>The IsGlyphExpanded.</value>
        public bool IsGlyphExpanded {
            get => _IsGlyphExpanded;
            set {
                _IsGlyphExpanded = value;
                OnPropertyChanged();

                if (!value)
                    return;
                IsIconTypeExpanded = false;
                IsSizeExpanded = false;
                IsColorExpanded = false;
                IsFontExpanded = false;
            }
        }
        #endregion
    }
}
