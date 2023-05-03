using System.Collections.Generic;
using System.Windows.Media;

namespace GregOsborne.Controls.Configuration {
    public class BrushPalette {
        public BrushPalette() {
            Brushes = new Dictionary<Enumerations.PalettePart, Brush>();
        }
        public static BrushPalette TextPalette = new BrushPalette {
            Brushes = new Dictionary<Enumerations.PalettePart, Brush> {
                {Enumerations.PaletteParts.Background, System.Windows.Media.Brushes.Black},
                {Enumerations.PaletteParts.Foreground, System.Windows.Media.Brushes.White}
            }
        };

        public static BrushPalette XmlPalette = new BrushPalette {
            Brushes = new Dictionary<Enumerations.PalettePart, Brush> {
                {Enumerations.PaletteParts.Background, System.Windows.Media.Brushes.DarkSlateGray},
                {Enumerations.PaletteParts.Foreground, System.Windows.Media.Brushes.White},
                {Enumerations.PaletteParts.StringStartEnd, System.Windows.Media.Brushes.LightGray},
                {Enumerations.PaletteParts.ElementStartEnd, System.Windows.Media.Brushes.Olive},
                {Enumerations.PaletteParts.EqualsSign, System.Windows.Media.Brushes.NavajoWhite},
                {Enumerations.PaletteParts.ElementName, System.Windows.Media.Brushes.GreenYellow},
                {Enumerations.PaletteParts.ElementValue, System.Windows.Media.Brushes.Thistle},
                {Enumerations.PaletteParts.AttributeName, System.Windows.Media.Brushes.Coral},
                {Enumerations.PaletteParts.AttributeValue, System.Windows.Media.Brushes.LightSkyBlue},
                {Enumerations.PaletteParts.CData, System.Windows.Media.Brushes.FloralWhite}
            }
        };
        public static BrushPalette CSharpPalette = new BrushPalette {
            Brushes = new Dictionary<Enumerations.PalettePart, Brush> {
                {Enumerations.PaletteParts.Background, System.Windows.Media.Brushes.DarkSlateGray},
                {Enumerations.PaletteParts.Foreground, System.Windows.Media.Brushes.White},
            }
        };

        public Dictionary<Enumerations.PalettePart, Brush> Brushes { get; private set; }
    }
}