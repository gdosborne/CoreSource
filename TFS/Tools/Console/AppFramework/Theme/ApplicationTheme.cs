using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

using GregOsborne.Application.Media;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Xml.Linq;
using Color = System.Windows.Media.Color;
using SystemColors = System.Windows.SystemColors;

namespace GregOsborne.Application.Theme {

    public class ApplicationTheme {
        private const string doubleType = "double";
        private const string nameAttribute = "name";
        private const string solidColorBrushType = "solidcolorbrush";
        private const string typeAttribute = "type";
        private const string valueAttribute = "value";
        private const string viewPropertyName = "View";
        private const string visualElementElement = "visualelement";
        private const string themeElementName = "theme";

        public ApplicationTheme(string themesFileName) {
            this.ThemesFileName = themesFileName;
            this.ActiveCaptionBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ActiveCaption,
                Name = ThemeNames.ActiveCaptionBrush
            };
            this.ActiveCaptionTextBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ActiveCaptionText,
                Name = ThemeNames.ActiveCaptionTextBrush
            };
            this.WindowBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.Window,
                Name = ThemeNames.WindowBrush
            };
            this.WindowTextBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.WindowText,
                Name = ThemeNames.WindowTextBrush
            };
            this.BorderBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.Border,
                Name = ThemeNames.BorderBrush
            };
            this.ControlBorderBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ControlBorder,
                Name = ThemeNames.ControlBorderBrush
            };
            this.HighlightBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.HighlightBrush,
                Name = ThemeNames.HighlightBrush
            };
            this.HighlightTextBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.HighlightTextBrush,
                Name = ThemeNames.HighlightBrush
            };
            this.FontSize = new VisualElement<double> {
                Value = DefaultValues.FontSize,
                Name = ThemeNames.FontSize
            };
            this.TitlebarFontSize = new VisualElement<double> {
                Value = DefaultValues.TitlebarFontSize,
                Name = ThemeNames.TitlebarFontSize
            };
            this.CloseBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.CloseBackgroundBrush,
                Name = ThemeNames.CloseBackgroundBrush
            };
            this.CloseForegroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.CloseForegroundBrush,
                Name = ThemeNames.CloseForegroundBrush
            };
            this.ConsoleBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ConsoleBackgroundBrush,
                Name = ThemeNames.ConsoleBackgroundBrush
            };
            this.ConsoleForegroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ConsoleForegroundBrush,
                Name = ThemeNames.ConsoleForegroundBrush
            };
            this.DataGridAlternatingRowBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.DataGridAlternatingRowBackgroundBrush,
                Name = ThemeNames.DataGridAlternatingRowBackgroundBrush
            };
            this.DataGridHeaderForegroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.DataGridHeaderForegroundBrush,
                Name = ThemeNames.DataGridHeaderForegroundBrush
            };
            this.DataGridHeaderBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.DataGridHeaderBackgroundBrush,
                Name = ThemeNames.DataGridHeaderBackgroundBrush
            };
            this.ToggleBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ToggleBackgroundBrush,
                Name = ThemeNames.ToggleBackgroundBrush
            };
            this.ToggleForegroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ToggleForegroundBrush,
                Name = ThemeNames.ToggleForegroundBrush
            };
            this.ToggleOffBackgroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ToggleOffBackgroundBrush,
                Name = ThemeNames.ToggleOffBackgroundBrush
            };
            this.ToggleOffForegroundBrush = new VisualElement<SolidColorBrush> {
                Value = DefaultValues.ToggleOffForegroundBrush,
                Name = ThemeNames.ToggleOffForegroundBrush
            };
            this.ToggleSize = new VisualElement<double> {
                Value = DefaultValues.ToggleSize,
                Name = ThemeNames.ToggleSize
            };
        }
        public bool HasChanges { get; set; } = default;
        public VisualElement<SolidColorBrush> ActiveCaptionBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ActiveCaptionTextBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> BorderBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ControlBorderBrush { get; set; } = default;
        public VisualElement<double> FontSize { get; set; } = default;
        public VisualElement<double> TitlebarFontSize { get; set; } = default;
        public string Name { get; set; } = default;
        public string ThemesFileName { get; set; } = default;
        public VisualElement<SolidColorBrush> WindowBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> WindowTextBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> HighlightBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> HighlightTextBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> CloseBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> CloseForegroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ConsoleBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ConsoleForegroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> DataGridAlternatingRowBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> DataGridHeaderForegroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> DataGridHeaderBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ToggleBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ToggleForegroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ToggleOffBackgroundBrush { get; set; } = default;
        public VisualElement<SolidColorBrush> ToggleOffForegroundBrush { get; set; } = default;
        public VisualElement<double> ToggleSize { get; set; } = default;

        public static List<ApplicationTheme> Create(string themesFileName) {
            var result = new List<ApplicationTheme>();
            try {
                var doc = XDocument.Load(themesFileName);
                doc.Root.Elements().ToList().ForEach(element => {
                    var theme = new ApplicationTheme(themesFileName);
                    var themeName = GetRandomThemeName(12);
                    if (element.Attribute(nameAttribute).HasValue()) {
                        theme.Name = element.Attribute(nameAttribute).Value;
                    }
                    element.Elements().ToList().ForEach(elem => {
                        if (elem.LocalName().Equals(visualElementElement)) {
                            //name, type, and value are required
                            if (elem.Attribute(nameAttribute).HasValue() && elem.Attribute(typeAttribute).HasValue() && elem.Attribute(valueAttribute).HasValue()) {
                                var name = elem.Attribute(nameAttribute).Value;
                                var type = elem.Attribute(typeAttribute).Value;
                                var value = elem.Attribute(valueAttribute).Value;
                                var prop = theme.GetType().GetProperty(name);
                                if (prop != null) {
                                    switch (type) {
                                        case doubleType: {
                                                var temp = new VisualElement<double> {
                                                    Value = double.Parse(value),
                                                    Name = name,
                                                };
                                                prop.SetValue(theme, temp, null);
                                                break;
                                            }
                                        case solidColorBrushType: {
                                                var temp = new VisualElement<SolidColorBrush> {
                                                    Value = new SolidColorBrush(value.ToColor()),
                                                    Name = name,
                                                };
                                                prop.SetValue(theme, temp, null);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    });
                    if (theme != null) {
                        result.Add(theme);
                    }
                });
            }
            catch (System.Exception ex) {
                //log error
                throw ex;
            }
            return result;
        }

        public void Apply(Window window) {
            var type = window.GetType();
            if (!window.GetType().HasProperty(viewPropertyName, out var windowView)) return;

            IThemedView? view = default;
            try {
                view = (IThemedView)windowView.GetValue(window, null);
            }
            catch {
                var val = windowView.GetValue(window, null);

                var props = val.GetType().GetProperties();
                foreach (var prop in props) {
                    Debug.WriteLine(prop.Name);
                    var propVal = prop.GetValue(val);
                    if (propVal.Is<IThemedView>()) {
                        view = propVal.As<IThemedView>();
                        break;
                    }
                }
            }
            if (view == null) return;

            view.ApplyVisualElement(this.ActiveCaptionBrush);
            view.ApplyVisualElement(this.ActiveCaptionTextBrush);
            view.ApplyVisualElement(this.WindowBrush);
            view.ApplyVisualElement(this.WindowTextBrush);
            view.ApplyVisualElement(this.BorderBrush);
            view.ApplyVisualElement(this.ControlBorderBrush);
            view.ApplyVisualElement(this.HighlightBrush);
            view.ApplyVisualElement(this.HighlightTextBrush);
            view.ApplyVisualElement(this.FontSize);
            view.ApplyVisualElement(this.TitlebarFontSize);
            view.ApplyVisualElement(this.CloseBackgroundBrush);
            view.ApplyVisualElement(this.CloseForegroundBrush);
            view.ApplyVisualElement(this.ConsoleBackgroundBrush);
            view.ApplyVisualElement(this.ConsoleForegroundBrush);
            view.ApplyVisualElement(this.DataGridAlternatingRowBackgroundBrush);
            view.ApplyVisualElement(this.DataGridHeaderForegroundBrush);
            view.ApplyVisualElement(this.DataGridHeaderBackgroundBrush);
            view.ApplyVisualElement(this.ToggleBackgroundBrush);
            view.ApplyVisualElement(this.ToggleForegroundBrush);
            view.ApplyVisualElement(this.ToggleOffBackgroundBrush);
            view.ApplyVisualElement(this.ToggleOffForegroundBrush);
            view.ApplyVisualElement(this.ToggleSize);
        }

        private Color GetBrushColor(string name) {
            if (name == ThemeNames.ActiveCaptionBrush) {
                return this.ActiveCaptionBrush.Value.Color;
            }
            else if (name == ThemeNames.ActiveCaptionTextBrush) {
                return this.ActiveCaptionTextBrush.Value.Color;
            }
            else if (name == ThemeNames.WindowBrush) {
                return this.WindowBrush.Value.Color;
            }
            else if (name == ThemeNames.WindowTextBrush) {
                return this.WindowTextBrush.Value.Color;
            }
            else if (name == ThemeNames.BorderBrush) {
                return this.BorderBrush.Value.Color;
            }
            else if (name == ThemeNames.ControlBorderBrush) {
                return this.ControlBorderBrush.Value.Color;
            }
            else if (name == ThemeNames.HighlightBrush) {
                return this.HighlightBrush.Value.Color;
            }
            else if (name == ThemeNames.HighlightTextBrush) {
                return this.HighlightTextBrush.Value.Color;
            }
            else if (name == ThemeNames.CloseBackgroundBrush) {
                return this.CloseBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.CloseForegroundBrush) {
                return this.CloseForegroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ConsoleBackgroundBrush) {
                return this.ConsoleBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ConsoleForegroundBrush) {
                return this.ConsoleForegroundBrush.Value.Color;
            }
            else if (name == ThemeNames.DataGridAlternatingRowBackgroundBrush) {
                return this.DataGridAlternatingRowBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.DataGridHeaderForegroundBrush) {
                return this.DataGridHeaderForegroundBrush.Value.Color;
            }
            else if (name == ThemeNames.DataGridHeaderBackgroundBrush) {
                return this.DataGridHeaderBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ToggleBackgroundBrush) {
                return this.ToggleBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ToggleForegroundBrush) {
                return this.ToggleForegroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ToggleOffBackgroundBrush) {
                return this.ToggleOffBackgroundBrush.Value.Color;
            }
            else if (name == ThemeNames.ToggleOffForegroundBrush) {
                return this.ToggleOffForegroundBrush.Value.Color;
            }
            return default;
        }

        public void Save(string fileName) {
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException("File does not exist", fileName);
            }
            this.ThemesFileName = fileName;
            this.Save();
        }

        public void Save() {
            if (!this.HasChanges || this.Name.Equals("Default")) {
                return;
            }
            var result = new List<ApplicationTheme>();
            try {
                var doc = !File.Exists(this.ThemesFileName)
                    ? new XDocument(new XElement("theme", new XElement(themeElementName, new XAttribute(nameAttribute, this.Name))))
                    : XDocument.Load(this.ThemesFileName);
                var themeElement = doc.Root.Elements().FirstOrDefault(x => x.LocalName().Equals(themeElementName) && x.Attribute(nameAttribute).Value.Equals(this.Name));
                if (themeElement == null) {
                    themeElement = new XElement(themeElementName,
                        new XAttribute(nameAttribute, this.Name));
                    doc.Root.Add(themeElement);
                }
                var names = new string[] {
                    ThemeNames.ActiveCaptionBrush,
                    ThemeNames.ActiveCaptionTextBrush,
                    ThemeNames.WindowBrush,
                    ThemeNames.WindowTextBrush,
                    ThemeNames.BorderBrush,
                    ThemeNames.ControlBorderBrush,
                    ThemeNames.HighlightBrush,
                    ThemeNames.HighlightTextBrush,
                    ThemeNames.CloseBackgroundBrush,
                    ThemeNames.ConsoleForegroundBrush,
                    ThemeNames.ConsoleBackgroundBrush,
                    ThemeNames.CloseForegroundBrush,
                    ThemeNames.FontSize,
                    ThemeNames.TitlebarFontSize,
                    ThemeNames.DataGridAlternatingRowBackgroundBrush,
                    ThemeNames.DataGridHeaderForegroundBrush,
                    ThemeNames.DataGridHeaderBackgroundBrush,
                    ThemeNames.ToggleBackgroundBrush,
                    ThemeNames.ToggleForegroundBrush,
                    ThemeNames.ToggleOffBackgroundBrush,
                    ThemeNames.ToggleOffForegroundBrush,
                    ThemeNames.ToggleSize,
                };
                names.ToList().ForEach(name => {
                    var value = this.GetBrushColor(name);
                    var element = themeElement.Elements().FirstOrDefault(x => x.LocalName().Equals(visualElementElement) && x.Attribute(nameAttribute).Value.Equals(name));
                    if (element == null) {
                        element = new XElement(visualElementElement);
                        element.Add(new XAttribute(nameAttribute, name));
                        element.Add(new XAttribute(typeAttribute, solidColorBrushType));
                        element.Add(new XAttribute(valueAttribute, value.ToString()));
                        themeElement.Add(element);
                    }
                    else {
                        element.Attribute(valueAttribute).Value = value.ToString();
                    }
                });
                var fontSize = this.FontSize;
                var fontSizeElement = themeElement.Elements().FirstOrDefault(x => x.LocalName().Equals(visualElementElement) && x.Attribute(nameAttribute).Value.Equals(ThemeNames.FontSize));
                if (fontSizeElement == null) {
                    fontSizeElement = new XElement(visualElementElement);
                    fontSizeElement.Add(new XAttribute(nameAttribute, ThemeNames.FontSize));
                    fontSizeElement.Add(new XAttribute(typeAttribute, doubleType));
                    fontSizeElement.Add(new XAttribute(valueAttribute, fontSize.Value.ToString()));
                    themeElement.Add(fontSizeElement);
                }
                else {
                    fontSizeElement.Attribute(valueAttribute).Value = fontSize.Value.ToString();
                }
                doc.Save(this.ThemesFileName);
                this.HasChanges = false;
            }
            catch (System.Exception ex) {
                //log error
                throw ex;
            }
        }

        public override string ToString() => this.Name;

        private static string GetRandomThemeName(int length) {
            var r = new Random();
            var result = new byte[length];
            for (var i = 0; i < length; i++) {
                var val = r.Next('A', 'Z' + 1);
                result[i] = (byte)val;
            }
            return System.Text.Encoding.ASCII.GetString(result);
        }

        public static class DefaultValues {
            public static SolidColorBrush ActiveCaption => SystemColors.ActiveCaptionBrush;
            public static SolidColorBrush ActiveCaptionText => SystemColors.ActiveCaptionTextBrush;
            public static SolidColorBrush Border => SystemColors.ActiveBorderBrush;
            public static SolidColorBrush ControlBorder => SystemColors.ControlDarkBrush;
            public static double FontSize => 12.0;
            public static double TitlebarFontSize => 16.0;
            public static SolidColorBrush Window => SystemColors.WindowBrush;
            public static SolidColorBrush WindowText => SystemColors.WindowTextBrush;
            public static SolidColorBrush HighlightBrush => SystemColors.HighlightBrush;
            public static SolidColorBrush HighlightTextBrush => SystemColors.HighlightTextBrush;
            public static SolidColorBrush CloseForegroundBrush => SystemColors.HighlightBrush;
            public static SolidColorBrush CloseBackgroundBrush => SystemColors.HighlightTextBrush;
            public static SolidColorBrush ConsoleForegroundBrush => System.Windows.Media.Brushes.WhiteSmoke;
            public static SolidColorBrush ConsoleBackgroundBrush => System.Windows.Media.Brushes.Black;
            public static SolidColorBrush DataGridAlternatingRowBackgroundBrush => SystemColors.WindowBrush;
            public static SolidColorBrush DataGridHeaderForegroundBrush => SystemColors.WindowBrush;
            public static SolidColorBrush DataGridHeaderBackgroundBrush => SystemColors.WindowTextBrush;
            public static SolidColorBrush ToggleBackgroundBrush =>System.Windows.Media.Brushes.Transparent;
            public static SolidColorBrush ToggleForegroundBrush => System.Windows.Media.Brushes.Black;
            public static SolidColorBrush ToggleOffBackgroundBrush => SystemColors.ControlBrush;
            public static SolidColorBrush ToggleOffForegroundBrush => SystemColors.ControlLightLightBrush;
            public static double ToggleSize => 35.0;
        }
    }

    public static class ThemeNames {
        public static string ActiveCaption => "ActiveCaption";
        public static string ActiveCaptionBrush => $"{ActiveCaption}{Brush}";
        public static string ActiveCaptionText => "ActiveCaptionText";
        public static string ActiveCaptionTextBrush => $"{ActiveCaptionText}{Brush}";
        public static string Background => "Background";
        public static string Foreground => "ForeGround";
        public static string DataGridAlternatingRowBackgroundBrush => $"DataGridAlternatingRow{Background}{Brush}";
        public static string Border => "Border";
        public static string Brush => "Brush";
        public static string BorderBrush => $"{Border}{Brush}";
        public static string Console => "Console";
        public static string ControlBorder => "ControlBorder";
        public static string ControlBorderBrush => $"{ControlBorder}{Brush}";
        public static string FontSize => "FontSize";
        public static string TitlebarFontSize => "TitlebarFontSize";
        public static string FontSizeValue => $"{FontSize}Value";
        public static string LocalActiveCaptionBrushKey => $"Local{ActiveCaptionBrush}Key";
        public static string LocalActiveCaptionTextBrushKey => $"Local{ActiveCaptionTextBrush}Key";
        public static string LocalBorderBrushKey => $"Local{BorderBrush}Key";
        public static string LocalFontSizeValueKey => $"Local{FontSize}Key";
        public static string LocalWindowBrushKey => $"Local{WindowBrush}Key";
        public static string LocalWindowTextBrushKey => $"Local{WindowTextBrush}Key";
        public static string SaveMainWindowBounds => "SaveMainWindowBounds";
        public static string Visual => "Visual";
        public static string Window => "Window";
        public static string Highlight => "Highlight";
        public static string Close => "Close";
        public static string WindowBrush => $"{Window}{Brush}";
        public static string WindowText => "WindowText";
        public static string WindowTextBrush => $"{WindowText}{Brush}";
        public static string HighlightBrush => $"{Highlight}{Brush}";
        public static string HighlightTextBrush => $"{Highlight}Text{Brush}";
        public static string CloseBackgroundBrush => $"{Close}{Background}{Brush}";
        public static string CloseForegroundBrush => $"{Close}{Foreground}{Brush}";
        public static string ConsoleBackgroundBrush => $"{Console}{Background}{Brush}";
        public static string ConsoleForegroundBrush => $"{Console}{Foreground}{Brush}";
        public static string DataGridHeaderForegroundBrush => $"DataGridHeader{Foreground}{Brush}";
        public static string DataGridHeaderBackgroundBrush => $"DataGridHeader{Background}{Brush}";
        public static string ToggleBackgroundBrush => $"Toggle{Background}{Brush}";
        public static string ToggleForegroundBrush => $"Toggle{Foreground}{Brush}";
        public static string ToggleOffBackgroundBrush => $"ToggleOff{Background}{Brush}";
        public static string ToggleOffForegroundBrush => $"ToggleOff{Foreground}{Brush}";
        public static string ToggleSize => $"ToggleSize";
    }
}
