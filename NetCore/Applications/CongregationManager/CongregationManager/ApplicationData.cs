using Common.Applicationn.Primitives;
using CongregationManager.Extensibility;
using Controls;
using System.Collections.Generic;
using System.Windows;

namespace CongregationManager {
    internal static class ApplicationData {
        public static List<ExtensionBase> Extensions { get; set; }

        public static FontIcon GetIcon(ResourceDictionary resDictionary, char glyph) =>
            GetIcon(resDictionary, glyph, "MenuItemIcon");

        public static FontIcon GetIcon(ResourceDictionary resDictionary, char glyph, string styleKey) =>
            new FontIcon {
                Glyph = glyph.ToString(),
                Style = GetStyle(resDictionary, styleKey),
            };

        public static Style GetStyle(ResourceDictionary resDictionary, string key) =>
            resDictionary[key].As<Style>();


    }
}
