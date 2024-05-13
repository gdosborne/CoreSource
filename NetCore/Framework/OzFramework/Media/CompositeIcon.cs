/* File="CompositeIcon"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;
using OzFramework.Primitives;
using Newtonsoft.Json;

using static OzFramework.Media.CompositeIconData;

namespace OzFramework.Media {
    [JsonObject]
    public class CompositeIcon : INotifyPropertyChanged, ICloneable {
        internal CompositeIcon() {

        }

        public static CompositeIcon Create(IconTypes iconType, SolidColorBrush surfaceBrush,
                FontFamily primaryFont, SolidColorBrush primaryBrush, string primaryGlyph,
                double primarySize, string secondaryGlyph, FontFamily secondaryFont,
                SolidColorBrush secondaryBrush, double? secondarySize) {
            var result = new CompositeIcon {
                SurfaceBrush = surfaceBrush.IsNull() ? Brushes.Transparent : surfaceBrush,
                IconType = iconType,
                PrimaryBrush = primaryBrush,
                SecondaryBrush = !secondaryBrush.IsNull() ? secondaryBrush : primaryBrush,
                PrimaryFontFamily = primaryFont.IsNull()
                    ? Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe Fluent Icons")
                    : primaryFont,
                PrimaryGlyph = primaryGlyph,
                SecondaryGlyph = secondaryGlyph,
                SecondaryFontFamily = secondaryFont,
                PrimarySize = primarySize,
                SecondarySize = !secondarySize.HasValue ? primarySize : secondarySize.Value,
                IsLoadComplete = true
            };
            result.IsNewIcon = true;
            return result;
        }

        public static CompositeIcon Create(IconTypes iconType, SolidColorBrush surfaceBrush,
            FontFamily primaryFont, SolidColorBrush primaryBrush, string primaryGlyph,
            double primarySize, string secondaryGlyph) {
            return Create(iconType, surfaceBrush, primaryFont, primaryBrush, primaryGlyph, primarySize,
                secondaryGlyph, default, default, default);
        }

        protected static CompositeIcon? FromJson(string json) {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            return JsonConvert.DeserializeObject<CompositeIcon>(json, settings);
        }

        public static CompositeIcon FromFile(string filename) {
            var json = File.ReadAllText(filename, Encoding.BigEndianUnicode);

            var result = FromJson(json);
            if (!result.IsNull()) {
                result.ShortName = Path.GetFileNameWithoutExtension(filename);
                result.FullPath = filename;
                result.Filename = Path.GetFileName(filename);
                result.SecondaryFontFamily ??= result.PrimaryFontFamily;
                if (result.SecondarySize <= 0)
                    result.SecondarySize = result.PrimarySize;
                result.SecondaryBrush ??= result.PrimaryBrush;
                result.IsLoadComplete = true;
            }
            return result;
        }

        public async Task Save() {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(this, settings);
            try {
                await File.WriteAllTextAsync(FullPath, json, Encoding.BigEndianUnicode);
                IsNewIcon = false;
            } catch (System.Exception) {
                throw;
            }
        }

        public async Task Save(string filename) {
            ShortName = Path.GetFileNameWithoutExtension(filename);
            FullPath = filename;
            Filename = Path.GetFileName(filename);
            await Save();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region IsLoadComplete Property
        private bool _IsLoadComplete = default;
        /// <summary>Gets/sets the IsLoadComplete.</summary>
        /// <value>The IsLoadComplete.</value>
        [JsonIgnore]
        public bool IsLoadComplete {
            get => _IsLoadComplete;
            set {
                _IsLoadComplete = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public object Clone() => this.MemberwiseClone();

        #region ShortName Property
        private string _ShortName = default;
        /// <summary>Gets/sets the ShortName.</summary>
        /// <value>The ShortName.</value>
        [JsonIgnore]
        public string ShortName {
            get => _ShortName;
            set {
                _ShortName = value;
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
                RenameValue = Filename;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FullPath Property
        private string _FullPath = default;
        /// <summary>Gets/sets the FullPath.</summary>
        /// <value>The FullPath.</value>
        [JsonProperty("fullpath")]
        public string FullPath {
            get => _FullPath;
            set {
                _FullPath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IconType Property
        private IconTypes _IconType = default;
        /// <summary>Gets/sets the IconType.</summary>
        /// <value>The IconType.</value>
        [JsonProperty("icontype")]
        public IconTypes IconType {
            get => _IconType;
            set {
                _IconType = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrimaryBrush Property
        private SolidColorBrush _PrimaryBrush = default;
        /// <summary>Gets/sets the PrimaryBrush.</summary>
        /// <value>The PrimaryBrush.</value>
        [JsonProperty("primarybrush")]
        public SolidColorBrush PrimaryBrush {
            get => _PrimaryBrush;
            set {
                _PrimaryBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSingleBrushInUse Property
        private bool _IsSingleBrushInUse = default;
        /// <summary>Gets/sets the IsSingleBrushInUse.</summary>
        /// <value>The IsSingleBrushInUse.</value>
        [JsonProperty("issinglebrushinuse")]
        public bool IsSingleBrushInUse {
            get => _IsSingleBrushInUse;
            set {
                _IsSingleBrushInUse = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryBrush Property
        private SolidColorBrush _SecondaryBrush = default;
        /// <summary>Gets/sets the SecondaryBrush.</summary>
        /// <value>The SecondaryBrush.</value>
        [JsonProperty("secondarybrush")]
        public SolidColorBrush SecondaryBrush {
            get => _SecondaryBrush;
            set {
                _SecondaryBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrimaryFontFamily Property
        private FontFamily _PrimaryFontFamily = default;
        /// <summary>Gets/sets the PrimaryFontFamily.</summary>
        /// <value>The PrimaryFontFamily.</value>
        [JsonProperty("primaryfontfamily")]
        public FontFamily PrimaryFontFamily {
            get => _PrimaryFontFamily;
            set {
                _PrimaryFontFamily = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryFontFamily Property
        private FontFamily _SecondaryFontFamily = default;
        /// <summary>Gets/sets the SecondaryFontFamily.</summary>
        /// <value>The SecondaryFontFamily.</value>
        [JsonProperty("secondaryfontfamily")]
        public FontFamily SecondaryFontFamily {
            get => _SecondaryFontFamily;
            set {
                _SecondaryFontFamily = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrimaryGlyph Property
        private string _PrimaryGlyph = default;
        /// <summary>Gets/sets the PrimaryGlyph.</summary>
        /// <value>The PrimaryGlyph.</value>
        [JsonProperty("primaryglyph")]
        public string PrimaryGlyph {
            get => _PrimaryGlyph;
            set {
                _PrimaryGlyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryGlyph Property
        private string _SecondaryGlyph = default;
        /// <summary>Gets/sets the SecondaryGlyph.</summary>
        /// <value>The SecondaryGlyph.</value>
        [JsonProperty("secondaryglyph")]
        public string SecondaryGlyph {
            get => _SecondaryGlyph;
            set {
                _SecondaryGlyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrimarySize Property
        private double _PrimarySize = default;
        /// <summary>Gets/sets the PrimarySize.</summary>
        /// <value>The PrimarySize.</value>
        [JsonProperty("primarysize")]
        public double PrimarySize {
            get => _PrimarySize;
            set {
                //do not call BeforePropertyChanged in sizing because
                // we do not want a change recorded at every pixel change
                // this is called externally when mouse leaves slider  
                _PrimarySize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondarySize Property
        private double? _SecondarySize = default;
        /// <summary>Gets/sets the SecondarySize.</summary>
        /// <value>The SecondarySize.</value>
        [JsonProperty("secondarysize")]
        public double? SecondarySize {
            get => _SecondarySize;
            set {
                //do not call BeforePropertyChanged in sizing because
                // we do not want a change recorded at every pixel change
                // this is called externally when mouse leaves slider  
                _SecondarySize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SurfaceBrush Property
        private SolidColorBrush _SurfaceBrush = default;
        /// <summary>Gets/sets the SurfaceBrush.</summary>
        /// <value>The SurfaceBrush.</value>
        [JsonProperty("surfacebrush")]
        public SolidColorBrush SurfaceBrush {
            get => _SurfaceBrush;
            set {
                _SurfaceBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryVerticalOffset Property
        private double _SecondaryVerticalOffset = default;
        /// <summary>Gets/sets the SecondaryVerticalOffset.</summary>
        /// <value>The VerticalOffset.</value>
        [JsonProperty("secondaryverticaloffset")]
        public double SecondaryVerticalOffset {
            get => _SecondaryVerticalOffset;
            set {
                _SecondaryVerticalOffset = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryHorizontalOffset Property
        private double _SecondaryHorizontalOffset = default;
        /// <summary>Gets/sets the SecondaryHorizontalOffset.</summary>
        /// <value>The SecondaryHorizontalOffset.</value>
        [JsonProperty("secondaryhorizontaloffset")]
        public double SecondaryHorizontalOffset {
            get => _SecondaryHorizontalOffset;
            set {
                _SecondaryHorizontalOffset = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsNewIcon Property
        private bool _IsNewIcon = default;
        /// <summary>Gets/sets the IsNewIcon.</summary>
        /// <value>The IsNewIcon.</value>
        [JsonIgnore]
        public bool IsNewIcon {
            get => _IsNewIcon;
            set {
                _IsNewIcon = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region RenameValue Property
        private string _RenameValue = default;
        /// <summary>Gets/sets the RenameValue.</summary>
        /// <value>The RenameValue.</value>
        [JsonIgnore]
        public string RenameValue {
            get => _RenameValue;
            set {
                _RenameValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region LastDateSaved Property
        private DateTime? _LastSavedDate = default;
        /// <summary>Gets/sets the LastDateSaved.</summary>
        /// <value>The LastDateSaved.</value>
        public DateTime? LastDateSaved {
            get => _LastSavedDate;
            set {
                if (!_LastSavedDate.HasValue) {
                    _LastSavedDate = value;
                    OnPropertyChanged();
                    return;
                }
                throw new ReadOnlyException($"{nameof(LastDateSaved)} is read-only");
            }
        }

        #endregion

        public string GetXaml(int? targetFontSize = default) {
            var multi = 1.0;
            if (targetFontSize.HasValue) {
                multi = targetFontSize.Value / PrimarySize;
            }
            var primaryFS = PrimarySize * multi;
            var secondaryFS = SecondarySize.HasValue ? SecondarySize.Value * multi : primaryFS;

            var widthAndHeight = primaryFS + 20.0;
            var result = new Grid {
                Background = SurfaceBrush
            };
            result.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.5, GridUnitType.Star) });
            result.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.5, GridUnitType.Star) });
            result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });
            result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) });

            var primeIcon = new TextBlock {
                Text = PrimaryGlyph.ToString(),
                FontFamily = PrimaryFontFamily,
                FontSize = primaryFS,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = PrimaryBrush,
                Margin = new Thickness(10)
            };
            primeIcon.SetValue(Grid.ColumnProperty, 0);
            primeIcon.SetValue(Grid.RowProperty, 0);
            primeIcon.SetValue(Grid.ColumnSpanProperty, 2);
            primeIcon.SetValue(Grid.RowSpanProperty, 2);
            result.Children.Add(primeIcon);

            var font = SecondaryFontFamily.IsNull() ? PrimaryFontFamily : SecondaryFontFamily;
            var brush = IsSingleBrushInUse ? PrimaryBrush : SecondaryBrush;

            if (IconType == IconTypes.FullOverlay) {
                var secondIcon = new TextBlock {
                    Text = SecondaryGlyph.ToString(),
                    FontFamily = font,
                    FontSize = secondaryFS,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = brush,
                    Margin = new Thickness(SecondaryHorizontalOffset, SecondaryVerticalOffset, 0, 0)
                };
                secondIcon.SetValue(Grid.ColumnProperty, 0);
                secondIcon.SetValue(Grid.RowProperty, 0);
                secondIcon.SetValue(Grid.ColumnSpanProperty, 2);
                secondIcon.SetValue(Grid.RowSpanProperty, 2);
                result.Children.Add(secondIcon);
            } else {
                var padSize = 10 * multi;
                var border = new Border {
                    Padding = new Thickness(padSize),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = SurfaceBrush
                };

                var secondIcon = new TextBlock {
                    Text = SecondaryGlyph.ToString(),
                    FontFamily = font,
                    FontSize = secondaryFS,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = brush
                };

                border.SetValue(Grid.ColumnProperty, 1);
                border.SetValue(Grid.RowProperty, 1);
                border.SetValue(Grid.ColumnSpanProperty, 1);
                border.SetValue(Grid.RowSpanProperty, 1);

                border.Child = secondIcon;
                result.Children.Add(border);
            }

            var xaml = XamlWriter.Save(result);
            xaml = xaml.Replace(" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"", string.Empty);
            var doc = XDocument.Parse(xaml);

            return doc.ToString();
        }

        public bool Rename(out string reasonError) {
            reasonError = default;
            var dir = Path.GetDirectoryName(FullPath);
            var newFile = new FileInfo(RenameValue);
            if (newFile.Extension.IsNull()) {
                RenameValue += ".compo";
            }
            var newFilename = Path.Combine(dir, RenameValue);
            if (!File.Exists(newFilename)) {
                try {
                    File.Move(FullPath, newFilename);
                    FullPath = newFilename;
                    Filename = Path.GetFileName(FullPath);
                    ShortName = Path.GetFileNameWithoutExtension(FullPath);
                    RenameValue = Filename;
                    return true;
                } catch (System.Exception ex) {
                    reasonError = ex.Message;
                    return false;
                }
            }
            reasonError = $"{newFilename} already exists";
            return false;
        }
    }
}
