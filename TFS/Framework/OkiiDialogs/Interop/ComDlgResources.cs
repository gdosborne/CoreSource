namespace GregOsborne.Dialogs.Interop {
	internal static class ComDlgResources {
		public enum ComDlgResourceId {
			OpenButton = 370,
			Open = 384,
			FileNotFound = 391,
			CreatePrompt = 402,
			ReadOnly = 427,
			ConfirmSaveAs = 435
		}
		private static Win32Resources resources = new Win32Resources("comdlg32.dll");
		public static string LoadString(ComDlgResourceId id) => resources.LoadString((uint)id);
		public static string FormatString(ComDlgResourceId id, params string[] args) => resources.FormatString((uint)id, args);
	}
}
