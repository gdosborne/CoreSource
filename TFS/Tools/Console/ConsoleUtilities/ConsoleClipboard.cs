namespace DCI.ConsoleUtilities {
	using System;
	using System.Runtime.InteropServices;

	public static class ConsoleClipboard {
		[DllImport("user32.dll")]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll")]
		internal static extern bool CloseClipboard();

		[DllImport("user32.dll")]
		internal static extern bool SetClipboardData(uint uFormat, IntPtr data);
		public static void Copy(string data) {
			OpenClipboard(IntPtr.Zero);
			var ptr = Marshal.StringToHGlobalUni(data);
			SetClipboardData(13, ptr);
			CloseClipboard();
			Marshal.FreeHGlobal(ptr);
		}
	}
}
