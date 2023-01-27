using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CongregationManager.Extensibility {
    public interface IExtensionPanel {
        string Glyph { get; set; }
        string ExtensionName { get; set; }
        UserControl Control { get; set; }
    }
}
