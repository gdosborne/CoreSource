using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MyApplication
{
	internal class NativeMethods
	{
		#region Internal Fields
		internal const int GWL_STYLE = -16;
		internal const int WS_MAXIMIZEBOX = 0x10000;
		internal const int WS_MINIMIZEBOX = 0x20000;
		internal const int WS_SYSMENU = 0x80000;
		#endregion

		#region Internal Methods

		[DllImport("user32.dll")]
		extern internal static int GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		extern internal static int SetWindowLong(IntPtr hwnd, int index, int value);

		#endregion
	}
}