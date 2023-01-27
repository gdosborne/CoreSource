using CongregationManager.Extensibility;
using System.Windows.Controls;

namespace CongregationExtension {
    public class ExtensionPanel : IExtensionPanel {
        public ExtensionPanel(string extensionName, string glyph, UserControl control) {
            ExtensionName = extensionName;
            Glyph = glyph;
            Control = control;
        }

        public string? Glyph { get; set; }
        public string? ExtensionName { get; set; }
        public UserControl? Control { get; set; }
    }
}
