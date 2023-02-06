using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationManager.Extensibility {
    public interface IExtensionPanel {
        char Glyph { get; set; }
        string Title { get; set; }
        UserControl Control { get; set; }
        ICommand SaveCommand { get; }
        ICommand RevertCommand { get; }
    }
}
