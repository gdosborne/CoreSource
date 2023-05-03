namespace GregOsborne.Developers.Tasks.Ext {
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Suite.Extender;
	using GregOsborne.Suite.Extender.AppSettings;

	public class Extension : IExtender {
		public Extension() {
			this.Control = new DeveloperTasks();
			this.Control.As<DeveloperTasks>().View.MDL2AssetsCharacter = this.MDL2AssetsChar;
		}

		public string Title { get; } = "Developer Task Tracker";
		public string Description => "Adds, modifies and tracks progress of user development tasks";
		public string Id { get; } = "{A859D693-150A-4EE0-95C0-339FEA68267C}";
		public string MDL2AssetsChar => char.ConvertFromUtf32(60127);

		public string AssemblyFilename { get; set; }

		public TabItem TabItem { get; set; }

		public UserControl Control { get; }

		public Category SettingsCategory { get; private set; }

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

		public void Initialize() {
		}

		public void InitializeSettings(Category parent) {
			this.SettingsCategory = new Category(parent, this.Title) {
				Control = new SettingsControl()
			};
			parent.Categories.Add(this.SettingsCategory);
		}

		public void UpdateControlProperty<T>(string property, T value) {
			if (property == "Show Watermarks on Extensions") {
				this.Control.As<DeveloperTasks>().View.WatermarkVisibility = (Visibility)(object)value;
			}
		}
	}
}
