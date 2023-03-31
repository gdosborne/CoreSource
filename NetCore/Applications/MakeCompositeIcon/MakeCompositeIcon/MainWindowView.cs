using ApplicationFramework.Media;
using Common.Application.Linq;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static ApplicationFramework.Media.CompositeIconData;
using static Common.Application.Media.Extensions;

namespace MakeCompositeIcon {
    internal partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = $"{App.ThisApp.ApplicationName} [designer]";
            SelectedIcon = default;
            IsSingleColorSelected = true;
            IsEditorEnabled = false;
            Icons = new ObservableCollection<CompositeIcon>();
            Fonts = new ObservableCollection<System.Windows.Media.FontFamily>();
            IconTypes = new ObservableCollection<IconTypes> {
                CompositeIconData.IconTypes.FullOverlay,
                CompositeIconData.IconTypes.SubscriptedOverlay
            };
            GuideBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
            GuideVisibility = App.ThisApp.AreGuidesShown ? Visibility.Visible : Visibility.Hidden;
            RenameVisibility = Visibility.Hidden;
            HideSettings();
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.ThisApp.ApplicationName;
            RefreshFiles();
            OffsetVisibility = Visibility.Hidden;
            var fonts = System.Windows.Media.Fonts.SystemFontFamilies.OrderBy(x => x.Source);
            Fonts.AddRange(fonts);

            if (!string.IsNullOrEmpty(App.ThisApp.StartupFilename)) {
                var icon = Icons.FirstOrDefault(x => x.FullPath.Equals(App.ThisApp.StartupFilename, System.StringComparison.OrdinalIgnoreCase));
                if (icon == null) {
                    if (File.Exists(App.ThisApp.StartupFilename)) {
                        icon = CompositeIcon.FromFile(App.ThisApp.StartupFilename);
                        Icons.Add(icon);
                    }
                }
                SelectedIcon = icon;
            }
        }

        #region PrimaryCharacters Property
        private ObservableCollection<CharInfo> _PrimaryCharacters = default;
        /// <summary>Gets/sets the PrimaryCharacters.</summary>
        /// <value>The Characters.</value>
        public ObservableCollection<CharInfo> PrimaryCharacters {
            get => _PrimaryCharacters;
            set {
                _PrimaryCharacters = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryCharacters Property
        private ObservableCollection<CharInfo> _SecondaryCharacters = default;
        /// <summary>Gets/sets the SecondaryCharacters.</summary>
        /// <value>The SecondaryCharacters.</value>
        public ObservableCollection<CharInfo> SecondaryCharacters {
            get => _SecondaryCharacters;
            set {
                _SecondaryCharacters = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IconTypes Property
        private ObservableCollection<IconTypes> _IconTypes = default;
        /// <summary>Gets/sets the IconTypes.</summary>
        /// <value>The IconTypes.</value>
        public ObservableCollection<IconTypes> IconTypes {
            get => _IconTypes;
            set {
                _IconTypes = value;
                OnPropertyChanged();
            }
        }
        #endregion

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

        #region MaxOverlayIconSize Property
        private double _MaxOverlayIconSize = default;
        /// <summary>Gets/sets the MaxOverlayIconSize.</summary>
        /// <value>The MaxOverlayIconSize.</value>
        public double MaxOverlayIconSize {
            get => _MaxOverlayIconSize;
            set {
                _MaxOverlayIconSize = value;
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
                Reset();
                _SelectedIcon = value;

                if (SelectedIcon != null) {
                    SelectedIcon.PropertyChanged += SelectedIcon_PropertyChanged;

                    SelectedIcon.IsLoadComplete = false;

                    SelectedIcon.PrimarySize = SelectedIcon.PrimarySize;
                    SelectedIcon.SecondarySize = SelectedIcon.SecondarySize;
                    SelectedIcon.PrimaryBrush = SelectedIcon.PrimaryBrush;
                    SelectedIcon.SecondaryBrush = SelectedIcon.SecondaryBrush;
                    SelectedIcon.IconType = SelectedIcon.IconType;
                    SelectedIcon.SurfaceBrush = SelectedIcon.SurfaceBrush;
                    SelectedIcon.PrimaryFontFamily = SelectedIcon.PrimaryFontFamily;
                    SelectedIcon.SecondaryFontFamily = SelectedIcon.SecondaryFontFamily;

                    PrimaryCharacters ??= new ObservableCollection<CharInfo>();
                    SecondaryCharacters ??= new ObservableCollection<CharInfo>();

                    PrimaryCharacters.Clear();
                    SecondaryCharacters.Clear();

                    if (SelectedIcon.PrimaryFontFamily != null) {
                        if (!App.ThisApp.CachedCharacters.ContainsKey(SelectedIcon.PrimaryFontFamily.Source)) {
                            var chars = GetCharacters(SelectedIcon.PrimaryFontFamily, glyphFontSize);
                            App.ThisApp.CachedCharacters.Add(SelectedIcon.PrimaryFontFamily.Source, chars);
                        }
                        PrimaryCharacters.AddRange(App.ThisApp.CachedCharacters[SelectedIcon.PrimaryFontFamily.Source]);

                    }
                    if (SelectedIcon.SecondaryFontFamily != null) {
                        if (!App.ThisApp.CachedCharacters.ContainsKey(SelectedIcon.SecondaryFontFamily.Source)) {
                            var chars = GetCharacters(SelectedIcon.SecondaryFontFamily, glyphFontSize);
                            App.ThisApp.CachedCharacters.Add(SelectedIcon.SecondaryFontFamily.Source, chars);
                        }
                        SecondaryCharacters.AddRange(App.ThisApp.CachedCharacters[SelectedIcon.SecondaryFontFamily.Source]);
                    }

                    PrimaryGlyph = PrimaryCharacters.FirstOrDefault(x => x.Image.Equals(SelectedIcon.PrimaryGlyph));
                    SecondaryGlyph = SecondaryCharacters.FirstOrDefault(x => x.Image.Equals(SelectedIcon.SecondaryGlyph));

                    IsSingleColorSelected = SelectedIcon.SecondaryBrush == null
                        || (SelectedIcon.PrimaryBrush.Color.ToHexValue() == SelectedIcon.SecondaryBrush.Color.ToHexValue());
                    IsSingleSizeSelected = SelectedIcon.PrimarySize == SelectedIcon.SecondarySize;

                    OffsetVisibility = Visibility.Hidden;
                    if (SelectedIcon.IconType == CompositeIconData.IconTypes.FullOverlay) {
                        OffsetVisibility = Visibility.Visible;
                        RecalcOffsets();
                    }
                    if (SelectedIcon.SurfaceBrush.Color.A > 0)
                        GuideBrush = SelectedIcon.SurfaceBrush.Invert(128);

                    ShowSettingTypeCommand.Execute("Characters");

                    SelectedIcon.IsLoadComplete = true;
                }
                IsEditorEnabled = SelectedIcon != null;
                OnPropertyChanged();
            }
        }

        #region PrimaryGlyph Property
        private CharInfo _PrimaryGlyph = default;
        /// <summary>Gets/sets the PrimaryGlyph.</summary>
        /// <value>The PrimaryGlyph.</value>
        public CharInfo PrimaryGlyph {
            get => _PrimaryGlyph;
            set {
                _PrimaryGlyph = value;
                if (PrimaryGlyph != null && PrimaryGlyph.Index > 0)
                    SelectedIcon.PrimaryGlyph = PrimaryGlyph.Image;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryGlyph Property
        private CharInfo _SecondaryGlyph = default;
        /// <summary>Gets/sets the SecondaryGlyph.</summary>
        /// <value>The SecondaryGlyph.</value>
        public CharInfo SecondaryGlyph {
            get => _SecondaryGlyph;
            set {
                _SecondaryGlyph = value;
                if (SecondaryGlyph != null && SecondaryGlyph.Index > 0)
                    SelectedIcon.SecondaryGlyph = SecondaryGlyph.Image;
                OnPropertyChanged();
            }
        }
        #endregion

        private void RecalcOffsets() {
            OffsetPositive = SelectedIcon.PrimarySize - SelectedIcon.SecondarySize.Value;
            OffsetNegative = -(SelectedIcon.PrimarySize - SelectedIcon.SecondarySize.Value);
            SecondaryMargin = new Thickness(SelectedIcon.SecondaryHorizontalOffset,
                SelectedIcon.SecondaryVerticalOffset, 0, 0);
        }

        private void SelectedIcon_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var icon = sender.As<CompositeIcon>();
            if (e.PropertyName == nameof(CompositeIcon.PrimaryBrush)) {
                if (IsSingleColorSelected) {
                    icon.SecondaryBrush = SelectedIcon.PrimaryBrush;
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.SurfaceBrush)) {
                GuideBrush = icon.SurfaceBrush.Invert(128);
            }
            else if (e.PropertyName == nameof(CompositeIcon.PrimarySize)) {
                if (IsSingleSizeSelected) {
                    icon.SecondarySize = SelectedIcon.PrimarySize;
                }
                RecalcOffsets();
            }
            else if (e.PropertyName == nameof(CompositeIcon.SecondarySize)) {
                RecalcOffsets();
            }
            else if (e.PropertyName == nameof(CompositeIcon.IconType)) {
                CenteredVisibility = icon.IconType == CompositeIconData.IconTypes.FullOverlay ? Visibility.Visible : Visibility.Collapsed;
                SubscriptVisibility = icon.IconType == CompositeIconData.IconTypes.SubscriptedOverlay ? Visibility.Visible : Visibility.Collapsed;
                MaxOverlayIconSize = icon.IconType == CompositeIconData.IconTypes.FullOverlay ? 200 : 100;
                if (icon.SecondarySize > MaxOverlayIconSize) {
                    icon.SecondarySize = MaxOverlayIconSize;
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.PrimaryGlyph)) {
                var glyph = PrimaryCharacters.FirstOrDefault(x => x.Image == icon.PrimaryGlyph);
                if (glyph != null) {
                    if (icon.PrimaryFontFamily.Source != glyph.FontFamily.Source) {
                        icon.PrimaryFontFamily = glyph.FontFamily;
                    }
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.SecondaryGlyph)) {
                var glyph = SecondaryCharacters.FirstOrDefault(x => x.Image == icon.SecondaryGlyph);
                if (glyph != null) {
                    if (icon.SecondaryFontFamily.Source != glyph.FontFamily.Source) {
                        icon.SecondaryFontFamily = glyph.FontFamily;
                    }
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.PrimaryFontFamily)) {
                if (IsSingleFontSelected) {
                    icon.SecondaryFontFamily = icon.PrimaryFontFamily;
                }
                var chars = GetCharacters(icon.PrimaryFontFamily, icon.PrimarySize);
                if (chars != null) {
                    if (PrimaryCharacters != null) {
                        PrimaryCharacters.Clear();
                        PrimaryCharacters.AddRange(chars);
                    }
                    else
                        PrimaryCharacters = new ObservableCollection<CharInfo>(chars);
                    if (!App.ThisApp.CachedCharacters.ContainsKey(icon.PrimaryFontFamily.Source))
                        App.ThisApp.CachedCharacters.Add(icon.PrimaryFontFamily.Source, chars);
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.SecondaryFontFamily)) {
                var chars = GetCharacters(icon.SecondaryFontFamily, icon.SecondarySize.HasValue ? icon.SecondarySize.Value : icon.PrimarySize);
                if (chars != null) {
                    if (SecondaryCharacters != null) {
                        SecondaryCharacters.Clear();
                        SecondaryCharacters.AddRange(chars);
                    }
                    else
                        SecondaryCharacters = new ObservableCollection<CharInfo>(chars);
                    if (!App.ThisApp.CachedCharacters.ContainsKey(icon.SecondaryFontFamily.Source))
                        App.ThisApp.CachedCharacters.Add(icon.SecondaryFontFamily.Source, chars);
                }
            }
            else if (e.PropertyName == nameof(CompositeIcon.SecondaryVerticalOffset)
                || e.PropertyName == nameof(CompositeIcon.SecondaryHorizontalOffset)) {
                SecondaryMargin = new Thickness(SelectedIcon.SecondaryHorizontalOffset,
                           SelectedIcon.SecondaryVerticalOffset, 0, 0);
            }
            if (e.PropertyName == nameof(CompositeIcon.SecondarySize)
                    || e.PropertyName == nameof(CompositeIcon.PrimarySize)
                    || e.PropertyName == nameof(CompositeIcon.IsLoadComplete)) {
                return;
            }
            UpdateInterface();
        }

        private List<CharInfo> GetCharacters(System.Windows.Media.FontFamily fontFamily, double size) =>
            fontFamily.GetCharacters(size, out var isSymbolFont);

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

        #region SubscriptVisibility Property
        private Visibility _SubscriptVisibility = default;
        /// <summary>Gets/sets the SubscriptVisibility.</summary>
        /// <value>The SubscriptVisibility.</value>
        public Visibility SubscriptVisibility {
            get => _SubscriptVisibility;
            set {
                _SubscriptVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CenteredVisibility Property
        private Visibility _CenteredVisibility = default;
        /// <summary>Gets/sets the CenteredVisibility.</summary>
        /// <value>The CenteredVisibility.</value>
        public Visibility CenteredVisibility {
            get => _CenteredVisibility;
            set {
                _CenteredVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OffsetNegative Property
        private double _OffsetNegative = default;
        /// <summary>Gets/sets the OffsetNegative.</summary>
        /// <value>The OffsetNegative.</value>
        public double OffsetNegative {
            get => _OffsetNegative;
            set {
                _OffsetNegative = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OffsetPositive Property
        private double _OffsetPositive = default;
        /// <summary>Gets/sets the OffsetPositive.</summary>
        /// <value>The OffsetPositive.</value>
        public double OffsetPositive {
            get => _OffsetPositive;
            set {
                _OffsetPositive = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OffsetVisibility Property
        private Visibility _OffsetVisibility = default;
        /// <summary>Gets/sets the OffsetVisibility.</summary>
        /// <value>The OffsetVisibility.</value>
        public Visibility OffsetVisibility {
            get => _OffsetVisibility;
            set {
                _OffsetVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryMargin Property
        private Thickness _SecondaryMargin = default;
        /// <summary>Gets/sets the SecondaryMargin.</summary>
        /// <value>The SecondaryMargin.</value>
        public Thickness SecondaryMargin {
            get => _SecondaryMargin;
            set {
                _SecondaryMargin = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region GuideBrush Property
        private SolidColorBrush _GuideBrush = default;
        /// <summary>Gets/sets the GuideBrush.</summary>
        /// <value>The GuideBrush.</value>
        public SolidColorBrush GuideBrush {
            get => _GuideBrush;
            set {
                _GuideBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IconTypeVisibility Property
        private Visibility _IconTypeVisibility = default;
        /// <summary>Gets/sets the IconTypeVisibility.</summary>
        /// <value>The IconTypeVisibility.</value>
        public Visibility IconTypeVisibility {
            get => _IconTypeVisibility;
            set {
                _IconTypeVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ColorsVisibility Property
        private Visibility _ColorsVisibility = default;
        /// <summary>Gets/sets the ColorsVisibility.</summary>
        /// <value>The ColorsVisibility.</value>
        public Visibility ColorsVisibility {
            get => _ColorsVisibility;
            set {
                _ColorsVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontsVisibility Property
        private Visibility _FontsVisibility = default;
        /// <summary>Gets/sets the FontsVisibility.</summary>
        /// <value>The FontsVisibility.</value>
        public Visibility FontsVisibility {
            get => _FontsVisibility;
            set {
                _FontsVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SizeVisibility Property
        private Visibility _SizeVisibility = default;
        /// <summary>Gets/sets the SizeVisibility.</summary>
        /// <value>The SizeVisibility.</value>
        public Visibility SizeVisibility {
            get => _SizeVisibility;
            set {
                _SizeVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CharactersVisibility Property
        private Visibility _CharactersVisibility = default;
        /// <summary>Gets/sets the CharactersVisibility.</summary>
        /// <value>The CharactersVisibility.</value>
        public Visibility CharactersVisibility {
            get => _CharactersVisibility;
            set {
                _CharactersVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region GuideVisibility Property
        private Visibility _GuideVisibility = default;
        /// <summary>Gets/sets the GuideVisibility.</summary>
        /// <value>The GuideVisibility.</value>
        public Visibility GuideVisibility {
            get => _GuideVisibility;
            set {
                _GuideVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region RenameVisibility Property
        private Visibility _RenameVisibility = default;
        /// <summary>Gets/sets the RenameVisibility.</summary>
        /// <value>The RenameVisibility.</value>
        public Visibility RenameVisibility {
            get => _RenameVisibility;
            set {
                _RenameVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OtherIconFiles Property
        private List<string> _OtherIconFiles = default;
        /// <summary>Gets/sets the OtherIconFiles.</summary>
        /// <value>The OtherIconFiles.</value>
        public List<string> OtherIconFiles {
            get => _OtherIconFiles;
            private set {
                _OtherIconFiles = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public void UpdateClipWithSize(string propertyName) {
            if (SelectedIcon == null)
                return;

            var prop = SelectedIcon.GetType().GetProperty(propertyName);
            if (prop != null) {
                var value = prop.GetValue(SelectedIcon, null);
                Clipboard.SetText($"{propertyName}={value}", TextDataFormat.Text);
            }
            UpdateInterface();
        }

        public void Reset() {
            if (SelectedIcon != null) {
                SelectedIcon.IsLoadComplete = false;
            }
            IsSingleColorSelected = false;
            IsEditorEnabled = false;
            IsSingleFontSelected = false;
            IsSingleSizeSelected = false;
            SubscriptVisibility = Visibility.Collapsed;
            CenteredVisibility = Visibility.Collapsed;
        }

        internal void HideSettings() {
            CharactersVisibility = Visibility.Collapsed;
            SizeVisibility = Visibility.Collapsed;
            FontsVisibility = Visibility.Collapsed;
            ColorsVisibility = Visibility.Collapsed;
            IconTypeVisibility = Visibility.Collapsed;
        }

        public void RefreshFiles() {
            Icons.Clear();
            var temp = new List<CompositeIcon>();
            var files = new DirectoryInfo(App.ThisApp.FilesDirectory).GetFiles("*.compo");
            foreach (var file in files.OrderBy(x => x.Name)) {
                var icon = CompositeIcon.FromFile(file.FullName);
                temp.Add(icon);
            }

            OtherIconFiles ??= new List<string>();
            OtherIconFiles.Clear();
            if (File.Exists(App.ThisApp.OtherFilesFilename)) {
                var json = File.ReadAllText(App.ThisApp.OtherFilesFilename, Encoding.BigEndianUnicode);
                var settings = new JsonSerializerSettings {
                    Formatting = Formatting.Indented
                };
                OtherIconFiles = JsonConvert.DeserializeObject<List<string>>(json, settings);
                if (OtherIconFiles.Any()) {
                    OtherIconFiles.ForEach(x => {
                        try {
                            var icon = CompositeIcon.FromFile(x);
                            temp.Add(icon);
                        }
                        catch (Exception e) { App.HandleException(e); }
                    });

                }
            }
            Icons.AddRange(temp.OrderBy(x => x.ShortName));
        }

        internal async Task RenameOtherIcon(CompositeIcon icon, string oldFilename) {
            if (OtherIconFiles.Contains(oldFilename, StringComparison.OrdinalIgnoreCase)) {
                OtherIconFiles[OtherIconFiles.IndexOf(oldFilename, StringComparison.OrdinalIgnoreCase)] = icon.FullPath;
            }

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(OtherIconFiles, settings);
            await File.WriteAllTextAsync(App.ThisApp.OtherFilesFilename, json, Encoding.BigEndianUnicode);
        }

        internal async Task<bool> RemoveOtherIconAsync(CompositeIcon icon) {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            if (OtherIconFiles.Contains(icon.FullPath, StringComparison.OrdinalIgnoreCase)) {
                var result = true;
                while (result) {
                    result = OtherIconFiles.Remove(icon.FullPath);
                    if (result) {
                        var json = JsonConvert.SerializeObject(OtherIconFiles, settings);
                        await File.WriteAllTextAsync(App.ThisApp.OtherFilesFilename, json, Encoding.BigEndianUnicode);
                    }
                }
            }
            return true;
        }

        internal async Task<CompositeIcon> AddOtherFileAsync(string filename) {
            if (!File.Exists(filename))
                return null;
            try {
                var icon = CompositeIcon.FromFile(filename);
                OtherIconFiles?.Add(filename);
                var settings = new JsonSerializerSettings {
                    Formatting = Formatting.Indented
                };
                var json = JsonConvert.SerializeObject(OtherIconFiles, settings);
                await File.WriteAllTextAsync(App.ThisApp.OtherFilesFilename, json, Encoding.BigEndianUnicode);
                return icon;
            }
            catch (Exception ex) { App.HandleException(ex); }
            return null;
        }

        internal double glyphFontSize => (double)App.ThisApp.Resources["GlyphFontSize"];

    }
}
