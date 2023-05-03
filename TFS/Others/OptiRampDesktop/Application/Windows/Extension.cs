using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyApplication.Windows
{
	public static class Extension
	{
		#region Private Fields
		private static Window TimerWindow = null;
		private static bool ToParent = false;
		private static DispatcherTimer WindowTimer = null;
		#endregion

		#region Public Methods

		public static WindowState AsWindowState(this string value)
		{
			var names = new List<string>(Enum.GetNames(typeof(WindowState)));
			if (names.Contains(value))
				return (WindowState)Enum.Parse(typeof(WindowState), value, true);
			else
				return WindowState.Normal;
		}

		public static void CenterOwner(this Window value)
		{
			if (value.Owner != null)
			{
				ToParent = true;
				TimerWindow = value;
				WindowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
				WindowTimer.Tick += new EventHandler(WindowTimer_Tick);
				WindowTimer.Start();
			}
		}

		public static void CenterScreen(this Window value)
		{
			ToParent = false;
			TimerWindow = value;
			WindowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			WindowTimer.Tick += new EventHandler(WindowTimer_Tick);
			WindowTimer.Start();
		}

		public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject
		{
			if (parent == null)
				return null;
			T foundChild = null;
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				T childType = child as T;
				if (childType == null)
				{
					foundChild = FindChildByName<T>(child, childName);
					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						foundChild = (T)child;
						break;
					}
				}
				else
				{
					foundChild = (T)child;
					break;
				}
			}
			return foundChild;
		}

		public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}
					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public static FrameworkElement GetFirstChild<T>(this DependencyObject dp)
		{
			FrameworkElement result = null;
			DependencyObject child = null;
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++)
			{
				child = VisualTreeHelper.GetChild(dp, i);
				if (child.GetType() == typeof(T))
					result = (FrameworkElement)child;
				else
					result = child.GetFirstChild<T>();
				if (result != null) break;
			}
			return result;
		}

		public static void HideControlBox(this System.Windows.Window window)
		{
			IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
			var currentStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);
			NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE) & ~NativeMethods.WS_SYSMENU);
		}

		public static void HideMinimizeAndMaximizeButtons(this System.Windows.Window window)
		{
			IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
			var currentStyle = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_STYLE);
			NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_STYLE, (currentStyle & ~NativeMethods.WS_MAXIMIZEBOX & ~NativeMethods.WS_MINIMIZEBOX));
		}

		public static bool IsInDesignMode(this Window value)
		{
			return DesignerProperties.GetIsInDesignMode(value);
		}

		public static bool IsOnPrimaryMonitor(this System.Windows.Window window)
		{
			IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
			var screen = System.Windows.Forms.Screen.FromHandle(hwnd);
			return screen.DeviceName == System.Windows.Forms.Screen.PrimaryScreen.DeviceName;
		}

		public static double MaxWidthFromList(this List<string> value, FontFamily family, double? size, FontStyle style, FontWeight weight)
		{
			var maxWidth = 0.0;
			foreach (var item in value)
			{
				var thisWidth = item.Measure(family, size, style, weight).Width;
				if (thisWidth > maxWidth)
					maxWidth = thisWidth;
			}
			return maxWidth;
		}

		public static Size Measure(this String value, FontFamily family, double? size, FontStyle style, FontWeight weight)
		{
			return value.Measure(family, size, style, weight, double.PositiveInfinity);
		}

		public static Size Measure(this String value, FontFamily family, double? size, FontStyle style, FontWeight weight, double suggestedWidth)
		{
			Size result = new Size();
			try
			{
				TextBlock l = new TextBlock();
				l.FontFamily = family;
				l.FontSize = size.HasValue ? size.Value : 10.0;
				l.FontStyle = style;
				l.FontWeight = weight;
				l.Text = value;
				if (!double.IsPositiveInfinity(suggestedWidth))
					l.Width = suggestedWidth;
				l.TextWrapping = !double.IsPositiveInfinity(suggestedWidth) ? TextWrapping.Wrap : TextWrapping.NoWrap;
				l.Measure(new Size(suggestedWidth, double.PositiveInfinity));
				result.Height = l.ActualHeight;
				if (!double.IsPositiveInfinity(suggestedWidth) && l.ActualWidth < suggestedWidth)
					result.Width = suggestedWidth;
				else
					result.Width = l.ActualWidth;
			}
			catch
			{
				result.Height = 0;
				result.Width = 0;
			}
			return result;
		}

		#endregion

		#region Private Methods

		private static void WindowTimer_Tick(object sender, EventArgs e)
		{
			((DispatcherTimer)sender).Stop();
			double top = TimerWindow.Top;
			double left = TimerWindow.Left;
			if (ToParent)
			{
				top = TimerWindow.Owner.Top + ((TimerWindow.Owner.Height - TimerWindow.ActualHeight) / 2);
				left = TimerWindow.Owner.Left + ((TimerWindow.Owner.Width - TimerWindow.ActualWidth) / 2);
			}
			else
			{
				if (System.Windows.Forms.Screen.AllScreens.Length == 0 || IsOnPrimaryMonitor(TimerWindow))
				{
					top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - TimerWindow.Height) / 2;
					left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - TimerWindow.Width) / 2;
				}
				else
				{
					for (int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
					{
						if (i == System.Windows.Forms.Screen.AllScreens.Length - 1)
						{
							top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - TimerWindow.Height) / 2;
							left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - TimerWindow.Width) / 2;
						}
						else
						{
							if (TimerWindow.Left > System.Windows.Forms.Screen.AllScreens[i].Bounds.X && TimerWindow.Left < System.Windows.Forms.Screen.AllScreens[i + 1].Bounds.X)
							{
								top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - TimerWindow.Height) / 2;
								left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - TimerWindow.Width) / 2;
							}
						}
					}
				}
			}
			TimerWindow.Top = top < 0 ? 0 : top;
			TimerWindow.Left = left < 0 ? 0 : left;
		}

		#endregion
	}
}