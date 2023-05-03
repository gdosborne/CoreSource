namespace GregOsborne.Developers.Suite.Utilities {
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Suite.Extender.AppSettings;

	internal static class Shared {
		public static string ShowatermarksOnExtensions => "Show Watermarks on Extensions";

		public static void UpdateExtensionSettings(Category generalCategory, Category extensionsCategory) {
			var extManager = App.Current.As<App>().ExtensionManager;
			var wmOn = ((BoolSettingValue)generalCategory.SettingValues.First(x => x.Name == ShowatermarksOnExtensions)).CurrentValue;
			while (extManager.HasNextExtension) {
				var ext = extManager.GetNextExtension();
				ext.UpdateControlProperty(ShowatermarksOnExtensions, wmOn ? Visibility.Visible : Visibility.Hidden);
			}
		}
	}
}
