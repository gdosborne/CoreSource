using System.Windows;
using System.Windows.Input;

namespace CongregationManager.Extensibility {
    public interface IExtensionPanel {
        char Glyph { get; set; }
        string Title { get; set; }
        FrameworkElement Control { get; set; }
        ICommand SaveCommand { get; }
        ICommand RevertCommand { get; }
    }
}
