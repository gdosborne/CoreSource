namespace GregOsborne.Application.Theme {
    using System.Windows.Media;

    public interface IThemedView {
        SolidColorBrush WindowBrush { get; set; }
        SolidColorBrush WindowTextBrush { get; set; }
        SolidColorBrush ActiveCaptionBrush { get; set; }
        SolidColorBrush ActiveCaptionTextBrush { get; set; }
        SolidColorBrush BorderBrush { get; set; }
        SolidColorBrush ControlBorderBrush { get; set; }
        SolidColorBrush DataGridHeaderBackgroundBrush { get; set; }
        SolidColorBrush DataGridHeaderForegroundBrush { get; set; }

        SolidColorBrush ToggleBackgroundBrush { get; set; }
        SolidColorBrush ToggleForegroundBrush { get; set; }
        SolidColorBrush ToggleOffBackgroundBrush { get; set; }
        SolidColorBrush ToggleOffForegroundBrush { get; set; }

        double FontSize { get; set; }
        double TitlebarFontSize { get; set; }
        double ToggleSize { get; set; }

        ApplicationTheme Theme { get; set; }

        void ApplyVisualElement<T>(VisualElement<T> element);
    }
}
