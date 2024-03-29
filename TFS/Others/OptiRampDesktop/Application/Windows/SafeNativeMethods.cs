using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MyApplication.Windows
{
	public static class SafeNativeMethods
	{
		#region Private Fields
		private const int SW_HIDE = 0;

		private const int SW_SHOW = 5;

		private const string VistaStartMenuCaption = "Start";

		private static IntPtr vistaStartMenuWnd = IntPtr.Zero;
		#endregion

		#region Private Delegates

		private delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);

		#endregion

		#region Public Properties
		public static bool Visible
		{
			set { SetVisibility(value); }
		}
		#endregion

		#region Public Methods

		public static void Hide()
		{
			SetVisibility(false);
		}

		public static void Show()
		{
			SetVisibility(true);
		}

		#endregion

		#region Private Methods

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool EnumThreadWindows(int threadId, EnumThreadProc pfnEnum, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

		private static IntPtr GetVistaStartMenuWnd(IntPtr taskBarWnd)
		{
			int procId;
			GetWindowThreadProcessId(taskBarWnd, out procId);
			Process p = Process.GetProcessById(procId);
			if (p != null)
			{
				foreach (ProcessThread t in p.Threads)
				{
					EnumThreadWindows(t.Id, MyEnumThreadWindowsProc, IntPtr.Zero);
				}
			}
			return vistaStartMenuWnd;
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

		private static bool MyEnumThreadWindowsProc(IntPtr hWnd, IntPtr lParam)
		{
			StringBuilder buffer = new StringBuilder(256);
			if (GetWindowText(hWnd, buffer, buffer.Capacity) > 0)
			{
				Console.WriteLine(buffer);
				if (buffer.ToString() == VistaStartMenuCaption)
				{
					vistaStartMenuWnd = hWnd;
					return false;
				}
			}
			return true;
		}

		private static void SetVisibility(bool show)
		{
			IntPtr taskBarWnd = FindWindow("Shell_TrayWnd", null);
			IntPtr startWnd = FindWindowEx(taskBarWnd, IntPtr.Zero, "Button", "Start");
			if (startWnd == IntPtr.Zero)
			{
				startWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, "Start");
			}
			if (startWnd == IntPtr.Zero)
			{
				startWnd = FindWindow("Button", null);
				if (startWnd == IntPtr.Zero)
				{
					startWnd = GetVistaStartMenuWnd(taskBarWnd);
				}
			}
			ShowWindow(taskBarWnd, show ? SW_SHOW : SW_HIDE);
			ShowWindow(startWnd, show ? SW_SHOW : SW_HIDE);
		}

		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

		#endregion
	}
}