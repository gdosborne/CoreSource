namespace GregOsborne.Developers.Docs.ext {
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Suite.Extender;
	using GregOsborne.Suite.Extender.AppSettings;

	public sealed class Extension : IExtender {
		public Extension() {
			this.Control = new DevelopersDocs();
			this.Control.As<DevelopersDocs>().View.MDL2AssetsCharacter = this.MDL2AssetsChar;
		}

		public string Title => "Developer Documentation";
		public string Description => "Documentation/Help System for Developer Suite";
		public string Id => "{ABDFB507-F98C-40A4-B2B7-CEB4B7D366F7}";
		public string MDL2AssetsChar => char.ConvertFromUtf32(57811);

		public UserControl Control { get; private set; }

		public Category SettingsCategory { get; private set; }

		public string AssemblyFilename { get; set; }

		public TabItem TabItem { get; set; }

		public void DropMessage() => throw new NotImplementedException();

		public TextBlock GetIconTextBlock() {
			var result = new TextBlock {
				FontFamily = new FontFamily("Segoe MDL2 Assets"),
				Foreground = Brushes.Black,
				FontSize = 16,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center,
				Text = this.MDL2AssetsChar
			};
			return result;
		}

		public void InitializeSettings(Category parent) {
			this.SettingsCategory = new Category(parent, this.Title) {
				Control = new SettingsControl()
			};
			parent.Categories.Add(this.SettingsCategory);
		}

		public void Initialize() {
		}

		public void UpdateControlProperty<T>(string property, T value) {
			if (property == "Show Watermarks on Extensions") {
				this.Control.As<DevelopersDocs>().View.WatermarkVisibility = (Visibility)(object)value;
			}
		}
	}
}
