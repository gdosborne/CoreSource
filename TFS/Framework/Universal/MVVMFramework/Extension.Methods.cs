namespace MVVMFramework {
	using System.Runtime.CompilerServices;
	using Windows.UI.Xaml.Controls;

	public static class ExtensionMethods {
		public static T GetView<T>(this Page page) {
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
				return default;
			}
			return (T)page.DataContext;
		}

		public static T GetView<T>(this ContentDialog dialog) {
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
				return default;
			}
			return (T)dialog.DataContext;
		}
	}
	public static class Reflection {
		public static string GetPropertyName([CallerMemberName] string caller = null) => caller;
	}
}
