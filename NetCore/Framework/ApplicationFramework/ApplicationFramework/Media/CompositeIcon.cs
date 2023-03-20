using Common.Application.Media;
using Common.Application.Primitives;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;
using static ApplicationFramework.Media.CompositeIconData;

namespace ApplicationFramework.Media {
    [JsonObject]
    public class CompositeIcon : INotifyPropertyChanged, ICloneable {
        private CompositeIcon() {

        }

        public static CompositeIcon Create(IconTypes iconType, SolidColorBrush surfaceBrush,
                FontFamily primaryFont, SolidColorBrush primaryBrush, char primaryGlyph,
                double primarySize, char secondaryGlyph, FontFamily secondaryFont,
                SolidColorBrush secondaryBrush, double? secondarySize) {
            var result = new CompositeIcon {
                SurfaceBrush = surfaceBrush == null ? Brushes.Transparent : surfaceBrush,
                IconType = iconType,
                PrimaryBrush = primaryBrush,
                SecondaryBrush = secondaryBrush != null ? secondaryBrush : primaryBrush,
                PrimaryFontFamily = primaryFont == null
                    ? Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe Fluent Icons")
                    : primaryFont,
                PrimaryGlyph = primaryGlyph,
                SecondaryGlyph = secondaryGlyph,
                SecondaryFontFamily = secondaryFont,
                PrimarySize = primarySize,
                SecondarySize = !secondarySize.HasValue ? primarySize : secondarySize.Value,
                IsLoadComplete = true
            };
            return result;
        }

        public static CompositeIcon Create(IconTypes iconType, SolidColorBrush surfaceBrush,
            FontFamily primaryFont, SolidColorBrush primaryBrush, char primaryGlyph,
            double primarySize, char secondaryGlyph) {
            return Create(iconType, surfaceBrush, primaryFont, primaryBrush, primaryGlyph, primarySize,
                secondaryGlyph, default, default, default);
        }

        private static CompositeIcon? FromJson(string json) {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            return JsonConvert.DeserializeObject<CompositeIcon>(json, settings);
        }

        public static CompositeIcon FromFile(string filename) {
            var json = File.ReadAllText(filename);

            var result = FromJson(json);
            if (result != null) {
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
                await File.WriteAllTextAsync(FullPath, json);
            }
            catch (Exception) {
                throw;
            }
        }

        public async Task Save(string filename) {
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

        #region FullPath Property
        private string _FullPath = default;
        /// <summary>Gets/sets the FullPath.</summary>
        /// <value>The FullPath.</value>
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
        private char _PrimaryGlyph = default;
        /// <summary>Gets/sets the PrimaryGlyph.</summary>
        /// <value>The PrimaryGlyph.</value>
        [JsonProperty("primaryglyph")]
        public char PrimaryGlyph {
            get => _PrimaryGlyph;
            set {
                _PrimaryGlyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SecondaryGlyph Property
        private char _SecondaryGlyph = default;
        /// <summary>Gets/sets the SecondaryGlyph.</summary>
        /// <value>The SecondaryGlyph.</value>
        [JsonProperty("secondaryglyph")]
        public char SecondaryGlyph {
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

        public string GetXaml(bool isUWPXaml) {
            var widthAndHeight = !SecondarySize.HasValue
                ? PrimarySize + 20.0
                : PrimarySize > SecondarySize.Value
                    ? PrimarySize + 20.0
                    : SecondarySize.Value + 20.0;
            var result = new Grid {
                Background = SurfaceBrush
            };
            result.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(.5, System.Windows.GridUnitType.Star) });
            result.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(.5, System.Windows.GridUnitType.Star) });
            result.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(.5, System.Windows.GridUnitType.Star) });
            result.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(.5, System.Windows.GridUnitType.Star) });
            result.Children.Add(new ContentControl { Name = "primary" });
            result.Children.Add(new ContentControl { Name = "secondary" });

            var primaryChar = $"&#x{((int)PrimaryGlyph).ToString("X2")};";
            var secondaryChar = $"&#x{((int)SecondaryGlyph).ToString("X2")};";

            var primaryIcon = default(string);
            var secondaryIcon = default(string);
            
            if (isUWPXaml) {
                primaryIcon = $"<FontIcon FontFamily=\"{PrimaryFontFamily.Source}\" " +
                    $"FontSize=\"{PrimarySize}\" Glyph=\"{primaryChar}\" " +
                    $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                    $"Grid.RowSpan=\"2\" Foreground=\"{PrimaryBrush.Color.ToHexValue()}\" " +
                    $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>";
            }
            else {
                primaryIcon = $"<TextBlock FontFamily=\"{PrimaryFontFamily.Source}\" " +
                    $"FontSize=\"{PrimarySize}\" Text=\"{primaryChar}\" " +
                    $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                    $"Grid.RowSpan=\"2\" Foreground=\"{PrimaryBrush.Color.ToHexValue()}\" " +
                    $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" " +
                    $"Margin=\"10\"/>";
            }

            var font = default(FontFamily);
            var color = default(Color);
            var size = default(double);

            font = SecondaryFontFamily == null ? PrimaryFontFamily : SecondaryFontFamily;
            color = IsSingleBrushInUse ? PrimaryBrush.Color : SecondaryBrush.Color;
            size = !SecondarySize.HasValue ? PrimarySize : SecondarySize.Value;

            if (isUWPXaml) {
                if (IconType == IconTypes.FullOverlay) {
                    secondaryIcon = $"<FontIcon FontFamily=\"{font.Source}\" " +
                        $"FontSize=\"{size}\" Glyph=\"{secondaryChar}\" " +
                        $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                        $"Grid.RowSpan=\"2\" Foreground=\"{color.ToHexValue()}\" " +
                        $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" " +
                        $"Margin=\"10\"/>";
                }
                else {
                    secondaryIcon = $"<Border Grid.Column=\"1\" Grid.Row=\"1\" Background=\"{SurfaceBrush.Color.ToHexValue()}\" " +
                        $"Padding=\"10\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\">" +
                        $"<FontIcon FontFamily=\"{font.Source}\" " +
                            $"FontSize=\"{size}\" Glyph=\"{secondaryChar}\" " +
                            $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                            $"Grid.RowSpan=\"2\" Foreground=\"{color.ToHexValue()}\" " +
                            $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>" +
                            $"</Border>";
                }
            }
            else {
                if (IconType == IconTypes.FullOverlay) {
                    secondaryIcon = $"<TextBlock FontFamily=\"{font.Source}\" " +
                        $"FontSize=\"{size}\" Text=\"{secondaryChar}\" " +
                        $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                        $"Grid.RowSpan=\"2\" Foreground=\"{color.ToHexValue()}\" " +
                        $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" " +
                        $"Margin=\"10\"/>";
                }
                else {
                    secondaryIcon = $"<Border Grid.Column=\"1\" Grid.Row=\"1\" Background=\"{SurfaceBrush.Color.ToHexValue()}\" " +
                        $"Padding=\"10\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\">" +
                        $"<TextBlock FontFamily=\"{font.Source}\" " +
                        $"FontSize=\"{size}\" Text=\"{secondaryChar}\" " +
                        $"Grid.Column=\"0\" Grid.Row=\"0\" Grid.ColumnSpan=\"2\" " +
                        $"Grid.RowSpan=\"2\" Foreground=\"{color.ToHexValue()}\" " +
                        $"HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\"/>" +
                        $"</Border>";
                }
            }


            var xaml = XamlWriter.Save(result);
            xaml = xaml.Replace(" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"", string.Empty);
            xaml = xaml.Replace("<ContentControl Name=\"primary\" />", primaryIcon);
            xaml = xaml.Replace("<ContentControl Name=\"secondary\" />", secondaryIcon);
            var doc = XDocument.Parse(xaml);


            return doc.ToString();
        }
    }
}
