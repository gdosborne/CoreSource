namespace GregOsborne.MVVMFramework {
	using System.ComponentModel;
	using System.Windows;

	public static class Extensions {
		#region Public Methods

		public static T GetView<T>(this FrameworkElement root) {
			if (DesignerProperties.GetIsInDesignMode(root)) {
				return default;
			}

			return (T)root.DataContext;
		}

		#endregion Public Methods
	}
}
