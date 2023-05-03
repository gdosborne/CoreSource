using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GregOsborne.Application.Primitives;
using GregOsborne.Suite.Extender;
using GregOsborne.Suite.Extender.AppSettings;

namespace GregOsborne.Developers.Batch.ext {
	public sealed class Extension : IExtender {
		public Extension() {
			this.Control = new DevelopersBatch();
			this.Control.As<DevelopersBatch>().View.MDL2AssetsCharacter = this.MDL2AssetsChar;
		}

		public string Id { get; } = "{A8F6AA6F-7B0A-40CE-BF0E-A7835C375D24}";
		public string Title { get; } = "Developers Build Information";
		public string Description { get; } = "Information for Batch Jobs";
		public string MDL2AssetsChar { get; } = char.ConvertFromUtf32(60454);

		public UserControl Control { get; private set; }

		public void DropMessage() => throw new NotImplementedException();
		public void Initialize() {
		}

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

		public Category SettingsCategory { get; private set; }

		public void InitializeSettings(Category parent) {
			this.SettingsCategory = new Category(parent, this.Title) {
				Control = new SettingsControl()
			};
			parent.Categories.Add(this.SettingsCategory);
		}
		public void UpdateControlProperty<T>(string property, T value) {
			if (property == "Show Watermarks on Extensions") {
				this.Control.As<DevelopersBatch>().View.WatermarkVisibility = (Visibility)(object)value;
			}
		}

		public string AssemblyFilename { get; set; }
		public TabItem TabItem { get; set; }
	}
}
