namespace GregOsborne.Application.Theme {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using System.Xml.Linq;

	using GregOsborne.Application.Media;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Xml.Linq;

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
			this.FontSize = new VisualElement<double> {
				Value = DefaultValues.FontSize,
				Name = ThemeNames.FontSize// Value
			};
		}
		public bool HasChanges { get; set; } = default;
		public VisualElement<SolidColorBrush> ActiveCaptionBrush { get; set; } = default;

		public VisualElement<SolidColorBrush> ActiveCaptionTextBrush { get; set; } = default;

		public VisualElement<SolidColorBrush> BorderBrush { get; set; } = default;

		public VisualElement<SolidColorBrush> ControlBorderBrush { get; set; } = default;

		public VisualElement<double> FontSize { get; set; } = default;

		public string Name { get; set; } = default;

		public string ThemesFileName { get; set; } = default;

		public VisualElement<SolidColorBrush> WindowBrush { get; set; } = default;

		public VisualElement<SolidColorBrush> WindowTextBrush { get; set; } = default;

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
			catch (Exception ex) {
				//log error
				throw ex;
			}
			return result;
		}

		public void Apply(Window window) {
			if (!window.GetType().HasProperty(viewPropertyName, out var themeProp)) {
				return;
			}

			var view = (IThemedView)themeProp.GetValue(window, null);
			if (view == null) {
				return;
			}

			view.ApplyVisualElement(this.ActiveCaptionBrush);
			view.ApplyVisualElement(this.ActiveCaptionTextBrush);
			view.ApplyVisualElement(this.WindowBrush);
			view.ApplyVisualElement(this.WindowTextBrush);
			view.ApplyVisualElement(this.BorderBrush);
			view.ApplyVisualElement(this.ControlBorderBrush);
			view.ApplyVisualElement(this.FontSize);
		}

		private Color GetBrushColor(string name) {
			if (name == ThemeNames.ActiveCaptionBrush) {
				return this.ActiveCaptionBrush.Value.Color;
			} else if (name == ThemeNames.ActiveCaptionTextBrush) {
				return this.ActiveCaptionTextBrush.Value.Color;
			} else if (name == ThemeNames.WindowBrush) {
				return this.WindowBrush.Value.Color;
			} else if (name == ThemeNames.WindowTextBrush) {
				return this.WindowTextBrush.Value.Color;
			} else if (name == ThemeNames.BorderBrush) {
				return this.BorderBrush.Value.Color;
			} else if (name == ThemeNames.ControlBorderBrush) {
				return this.ControlBorderBrush.Value.Color;
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
				var doc = XDocument.Load(this.ThemesFileName);
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
					ThemeNames.ControlBorderBrush
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
					} else {
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
				} else {
					fontSizeElement.Attribute(valueAttribute).Value = fontSize.Value.ToString();
				}
				doc.Save(this.ThemesFileName);
				this.HasChanges = false;
			}
			catch (Exception ex) {
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

			public static SolidColorBrush Window => SystemColors.WindowBrush;

			public static SolidColorBrush WindowText => SystemColors.WindowTextBrush;
		}
	}

	public static class ThemeNames {
		public static string ActiveCaption => "ActiveCaption";

		public static string ActiveCaptionBrush => $"{ActiveCaption}Brush";

		public static string ActiveCaptionText => "ActiveCaptionText";

		public static string ActiveCaptionTextBrush => $"{ActiveCaptionText}Brush";

		public static string Border => "Border";

		public static string BorderBrush => $"{Border}Brush";

		public static string ControlBorder => "ControlBorder";

		public static string ControlBorderBrush => $"{ControlBorder}Brush";

		public static string FontSize => "FontSize";

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

		public static string WindowBrush => $"{Window}Brush";

		public static string WindowText => "WindowText";

		public static string WindowTextBrush => $"{WindowText}Brush";
	}
}
