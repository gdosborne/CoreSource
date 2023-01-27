using Common.Applicationn.Primitives;
using CongregationManager.Extensibility;
using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CongregationManager {
    internal static class ApplicationData {
        public static List<ExtensionBase> Extensions { get; set; }

        public static FontIcon GetIcon(ResourceDictionary resDictionary, string glyph) =>
            GetIcon(resDictionary, glyph, "MenuItemIcon");

        public static FontIcon GetIcon(ResourceDictionary resDictionary, string glyph, string styleKey) =>
            new FontIcon {
                Glyph = glyph,
                Style = GetStyle(resDictionary, styleKey),
            };

        public static Style GetStyle(ResourceDictionary resDictionary, string key) =>
            resDictionary[key].As<Style>();


    }
}
