namespace GregOsborne.Application.Windows {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Documents;
	using System.Windows.Interop;
	using System.Windows.Media;
    using System.Windows.Threading;
    using GregOsborne.Application.Primitives;
	using static GregOsborne.Application.NativeMethods;

	public static class Extension {
		private static Window timerWindow;

		private static bool toParent;

		private static DispatcherTimer windowTimer;

		public static WindowState AsWindowState(this string value) {
			var names = new List<string>(Enum.GetNames(typeof(WindowState)));
			if (names.Contains(value)) {
				return (WindowState)Enum.Parse(typeof(WindowState), value, true);
			}

			return WindowState.Normal;
		}

        public static bool HasWindowStyle(this System.Diagnostics.Process p) {
            var hwnd = p.MainWindowHandle;
            uint WS_DISABLED = 0x8000000;
            bool visible = false;
            if (hwnd != IntPtr.Zero) {
                var style = GetWindowLong(hwnd, GwlStyle);
                visible = ((style & WS_DISABLED) != WS_DISABLED);
            }
            return visible;
        }

        public static void CenterOwner(this Window value) {
			if (value.Owner == null) {
				return;
			}

			toParent = true;
			timerWindow = value;
			windowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			windowTimer.Tick += WindowTimer_Tick;
			windowTimer.Start();
		}

		public static void CenterScreen(this Window value) {
			toParent = false;
			timerWindow = value;
			windowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			windowTimer.Tick += WindowTimer_Tick;
			windowTimer.Start();
		}

		public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject {
			if (parent == null) {
				return null;
			}

			T foundChild = null;
			var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			if (childrenCount == 0 && parent is TextBlock) {
				childrenCount = (parent as TextBlock).Inlines.Count;
			}
			if (childrenCount > 0) {
				for (var i = 0; i < childrenCount; i++) {
					var child = default(DependencyObject);
					if (parent.Is<TextBlock>()) {
						child = parent.As<TextBlock>().Inlines.Cast<Inline>().ElementAt(i);
					} else {
						child = VisualTreeHelper.GetChild(parent, i);
					}
					if (child.Is<T>()) {
						if (child.Is<Inline>() && child.As<Inline>().Name.Equals(childName)) {
							foundChild = child.As<T>();
							break;
						} else if (child.Is<FrameworkElement>() && child.As<FrameworkElement>().Name.Equals(childName)) {
							foundChild = child.As<T>();
							break;
						} else {
							continue;
						}
					} else {
						foundChild = FindChildByName<T>(child, childName);
						if (foundChild != null) {
							break;
						}
						//if (!child.Is<T>()) {
						//	foundChild = FindChildByName<T>(child, childName);
						//	if (foundChild != null) {
						//		break;
						//	}
						//} else if (!string.IsNullOrEmpty(childName)) {
						//	if (child.Is<FrameworkElement>() || child.Is<Inline>()) {
						//		if ((child.Is<FrameworkElement>()) || !child.As<FrameworkElement>().Name.Equals(childName)) {
						//			continue;
						//		} else if ((child.Is<Inline>()) || !child.As<Inline>().Name.Equals(childName)) {
						//			continue;
						//		}
						//	}
						//} else if (child.Is<T>()) {
						//	foundChild = child.As<T>();
						//	break;
						//}
					}
				}
			}
			return foundChild;
		}

		public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject {
			if (depObj == null) {
				yield break;
			}

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
				var child = VisualTreeHelper.GetChild(depObj, i);
				if (child is T children) {
					yield return children;
				}

				foreach (var childOfChild in FindVisualChildren<T>(child)) {
					yield return childOfChild;
				}
			}
		}

		public static FrameworkElement GetFirstChild<T>(this DependencyObject dp) {
			FrameworkElement result = null;
			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++) {
				var child = VisualTreeHelper.GetChild(dp, i);
				if (child is T) {
					result = (FrameworkElement)child;
				} else {
					result = child.GetFirstChild<T>();
				}

				if (result != null) {
					break;
				}
			}
			return result;
		}

		[Flags]
		public enum WindowParts {
			ControlBox = 1,
			MinimizeButton = 2,
			MaximizeButton = 4
		}

		public static void HideWindowControls(this Window window, WindowParts partsToHide) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var currentStyle = GetWindowLong(hwnd, GwlStyle);
			if (partsToHide.HasFlag(WindowParts.ControlBox)) {
				SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsSysmenu);
				currentStyle = GetWindowLong(hwnd, GwlStyle);
			}
			if (partsToHide.HasFlag(WindowParts.MinimizeButton)) {
				SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsMinimizebox);
				currentStyle = GetWindowLong(hwnd, GwlStyle);
			}
			if (partsToHide.HasFlag(WindowParts.MaximizeButton)) {
				SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsMaximizebox);
				currentStyle = GetWindowLong(hwnd, GwlStyle);
			}
		}

		public static void HideControlBox(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var currentStyle = GetWindowLong(hwnd, GwlStyle);
			SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsSysmenu);
		}

		public static void HideMinimizeAndMaximizeButtons(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var currentStyle = GetWindowLong(hwnd, GwlStyle);
			SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsMaximizebox & ~WsMinimizebox);
		}

		public static void HideMinimizeButton(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var currentStyle = GetWindowLong(hwnd, GwlStyle);
			SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsMinimizebox);
		}

		public static void HideMaximizeButton(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var currentStyle = GetWindowLong(hwnd, GwlStyle);
			SetWindowLong(hwnd, GwlStyle, currentStyle & ~WsMaximizebox);
		}

		public static bool IsInDesignMode(this Window value) => DesignerProperties.GetIsInDesignMode(value);

		public static bool IsOnPrimaryMonitor(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			var screen = System.Windows.Forms.Screen.FromHandle(hwnd);
			return screen.DeviceName == System.Windows.Forms.Screen.PrimaryScreen.DeviceName;
		}

		public static int GetScreenIndexForApp(this Window window) {
			var scrn = window.GetScreen();
			for (var i = 0; i < AllScreens().Length; i++) {
				if (scrn.DeviceName == AllScreens()[i].DeviceName) {
					return i;
				}
			}
			return -1;
		}

		public static System.Windows.Forms.Screen[] AllScreens() => System.Windows.Forms.Screen.AllScreens;

		public static System.Windows.Forms.Screen GetScreen(this Window window) {
			var hwnd = new WindowInteropHelper(window).Handle;
			return System.Windows.Forms.Screen.FromHandle(hwnd);
		}

		public static double MaxWidthFromList(this List<string> value, FontFamily family, double? size, FontStyle style, FontWeight weight) {
			var maxWidth = 0.0;
			foreach (var item in value) {
				var thisWidth = item.Measure(family, size, style, weight).Width;
				if (thisWidth > maxWidth) {
					maxWidth = thisWidth;
				}
			}
			return maxWidth;
		}

		public static Size Measure(this string value, FontFamily family, double? size, FontStyle style, FontWeight weight) => value.Measure(family, size, style, weight, double.PositiveInfinity);

		public static Size Measure(this string value, FontFamily family, double? size, FontStyle style, FontWeight weight, double suggestedWidth) {
			var result = new Size();
			try {
				var systemFont = System.Windows.Media.Fonts.SystemFontFamilies.ToList().FirstOrDefault(Xml => Xml.Source == family.Source);
				var l = new TextBlock {
					FontFamily = systemFont,
					FontSize = size ?? 10.0,
					FontStyle = style,
					FontWeight = weight,
					Text = value
				};
				if (!double.IsPositiveInfinity(suggestedWidth)) {
					l.Width = suggestedWidth;
				}

				l.TextWrapping = !double.IsPositiveInfinity(suggestedWidth) ? TextWrapping.Wrap : TextWrapping.NoWrap;
				l.Measure(new Size(suggestedWidth, double.PositiveInfinity));
				var ds = l.DesiredSize;
				result.Height = ds.Height;
				result.Width = ds.Width;
			}
			catch {
				result.Height = 0;
				result.Width = 0;
			}
			return result;
		}

		private static void WindowTimer_Tick(object sender, EventArgs e) {
			((DispatcherTimer)sender).Stop();
			var top = timerWindow.Top;
			var left = timerWindow.Left;
			if (toParent) {
				top = timerWindow.Owner.Top + (timerWindow.Owner.Height - timerWindow.ActualHeight) / 2;
				left = timerWindow.Owner.Left + (timerWindow.Owner.Width - timerWindow.ActualWidth) / 2;
			} else {
				if (System.Windows.Forms.Screen.AllScreens.Length == 0 || IsOnPrimaryMonitor(timerWindow)) {
					top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - timerWindow.Height) / 2;
					left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - timerWindow.Width) / 2;
				} else {
					for (var i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++) {
						if (i == System.Windows.Forms.Screen.AllScreens.Length - 1) {
							top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - timerWindow.Height) / 2;
							left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - timerWindow.Width) / 2;
						} else {
							if (timerWindow.Left > System.Windows.Forms.Screen.AllScreens[i].Bounds.X && timerWindow.Left < System.Windows.Forms.Screen.AllScreens[i + 1].Bounds.X) {
								top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - timerWindow.Height) / 2;
								left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - timerWindow.Width) / 2;
							}
						}
					}
				}
			}
			timerWindow.Top = top < 0 ? 0 : top;
			timerWindow.Left = left < 0 ? 0 : left;
		}

		public static void SavePosition(this Window win, Settings appSettings) {
            appSettings.AddOrUpdateSetting(win.GetType().Name, nameof(win.Left), win.RestoreBounds.Left);
            appSettings.AddOrUpdateSetting(win.GetType().Name, nameof(win.Top), win.RestoreBounds.Top);
            appSettings.AddOrUpdateSetting(win.GetType().Name, nameof(win.Width), win.RestoreBounds.Width);
            appSettings.AddOrUpdateSetting(win.GetType().Name, nameof(win.Height), win.RestoreBounds.Height);
            appSettings.AddOrUpdateSetting(win.GetType().Name, nameof(win.WindowState), win.WindowState);
        }

        public static void SavePosition(this Window win, string applicationName) {
			win.SavePosition(new Settings(applicationName));
		}

        public static void SetPosition(this Window win, Settings appSettings) {
            var left = appSettings.GetValue(win.GetType().Name, nameof(win.Left), win.Left);
            var top = appSettings.GetValue(win.GetType().Name, nameof(win.Top), win.Top);
            var width = appSettings.GetValue(win.GetType().Name, nameof(win.Width), 0.0);
            var height = appSettings.GetValue(win.GetType().Name, nameof(win.Height), 0.0);
            var windowState = appSettings.GetValue(win.GetType().Name, nameof(WindowState), win.WindowState);

            if (width == 0 || double.IsInfinity(width) || double.IsNegativeInfinity(width) || height == 0 || double.IsInfinity(height) || double.IsNegativeInfinity(height)) {
                left = (Screen.PrimaryScreen.WorkingArea.Width - win.Width) / 2;
                top = (Screen.PrimaryScreen.WorkingArea.Height - win.Height) / 2;
                width = win.ActualWidth;
                height = win.ActualHeight;
            }

            win.Width = width;
            win.Height = height;
            win.Left = left;
            win.Top = top;
            win.WindowState = windowState;
        }

        public static void SetPosition(this Window win, string applicationName) {
			win.SetPosition(new Settings(applicationName));
		}
	}
}
