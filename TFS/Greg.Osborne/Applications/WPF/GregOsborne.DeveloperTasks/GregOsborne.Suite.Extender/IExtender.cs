namespace GregOsborne.Suite.Extender {
	using System.Windows.Controls;
	using GregOsborne.Suite.Extender.AppSettings;

	public interface IExtender {
		string Id { get; }
		string Title { get; }
		string Description { get; }
		UserControl Control { get; }
		void DropMessage();
		void Initialize();
		string MDL2AssetsChar { get; }
		TextBlock GetIconTextBlock();
		Category SettingsCategory { get; }
		void InitializeSettings(Category parent);
		void UpdateControlProperty<T>(string property, T value);
		string AssemblyFilename { get; set; }
		TabItem TabItem { get; set; }
	}
}
