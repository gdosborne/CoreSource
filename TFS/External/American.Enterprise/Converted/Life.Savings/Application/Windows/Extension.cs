using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using static GregOsborne.Application.NativeMethods;

namespace GregOsborne.Application.Windows {
    public static class Extension {
        private static Window _timerWindow;

        private static bool _toParent;

        private static DispatcherTimer _windowTimer;

        public static WindowState AsWindowState(this string value) {
            var names = new List<string>(Enum.GetNames(typeof(WindowState)));
            if (names.Contains(value))
                return (WindowState)Enum.Parse(typeof(WindowState), value, true);
            return WindowState.Normal;
        }

        public static void CenterOwner(this Window value) {
            if (value.Owner == null) return;
            _toParent = true;
            _timerWindow = value;
            _windowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _windowTimer.Tick += WindowTimer_Tick;
            _windowTimer.Start();
        }

        public static void CenterScreen(this Window value) {
            _toParent = false;
            _timerWindow = value;
            _windowTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _windowTimer.Tick += WindowTimer_Tick;
            _windowTimer.Start();
        }

        public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject {
            if (parent == null)
                return null;
            T foundChild = null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                var childType = child as T;
                if (childType == null) {
                    foundChild = FindChildByName<T>(child, childName);
                    if (foundChild != null) break;
                } else if (!string.IsNullOrEmpty(childName))
                    if (!(child is FrameworkElement frameworkElement) || frameworkElement.Name != childName) continue;
                foundChild = (T)child;
                break;
            }
            return foundChild;
        }

        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject {
            if (depObj == null) yield break;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T children)
                    yield return children;
                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public static FrameworkElement GetFirstChild<T>(this DependencyObject dp) {
            FrameworkElement result = null;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++) {
                var child = VisualTreeHelper.GetChild(dp, i);
                if (child is T)
                    result = (FrameworkElement)child;
                else
                    result = child.GetFirstChild<T>();
                if (result != null) break;
            }
            return result;
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

        public static bool IsInDesignMode(this Window value) {
            return DesignerProperties.GetIsInDesignMode(value);
        }

        public static bool IsOnPrimaryMonitor(this Window window) {
            var hwnd = new WindowInteropHelper(window).Handle;
            var screen = System.Windows.Forms.Screen.FromHandle(hwnd);
            return screen.DeviceName == System.Windows.Forms.Screen.PrimaryScreen.DeviceName;
        }

        public static System.Windows.Forms.Screen[] AllScreens() {
            return System.Windows.Forms.Screen.AllScreens;
        }

        public static System.Windows.Forms.Screen GetScreen(this Window window) {
            var hwnd = new WindowInteropHelper(window).Handle;
            return System.Windows.Forms.Screen.FromHandle(hwnd);
        }

        public static double MaxWidthFromList(this List<string> value, FontFamily family, double? size, FontStyle style, FontWeight weight) {
            var maxWidth = 0.0;
            foreach (var item in value) {
                var thisWidth = item.Measure(family, size, style, weight).Width;
                if (thisWidth > maxWidth)
                    maxWidth = thisWidth;
            }
            return maxWidth;
        }

        public static Size Measure(this string value, FontFamily family, double? size, FontStyle style, FontWeight weight) {
            return value.Measure(family, size, style, weight, double.PositiveInfinity);
        }

        public static Size Measure(this string value, FontFamily family, double? size, FontStyle style, FontWeight weight, double suggestedWidth) {
            var result = new Size();
            try {
                var l = new TextBlock {
                    FontFamily = family,
                    FontSize = size ?? 10.0,
                    FontStyle = style,
                    FontWeight = weight,
                    Text = value
                };
                if (!double.IsPositiveInfinity(suggestedWidth))
                    l.Width = suggestedWidth;
                l.TextWrapping = !double.IsPositiveInfinity(suggestedWidth) ? TextWrapping.Wrap : TextWrapping.NoWrap;
                l.Measure(new Size(suggestedWidth, double.PositiveInfinity));
                var ds = l.DesiredSize;
                result.Height = ds.Height;
                result.Width = ds.Width;
            } catch (System.Exception ex) {
                result.Height = 0;
                result.Width = 0;
            }
            return result;
        }

        public static double Measure(this string s, string typeFaceName, double emSize) {
            var ft = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(typeFaceName), emSize, new SolidColorBrush(Colors.Black));
            return ft.Width;
        }

        private static void WindowTimer_Tick(object sender, EventArgs e) {
            ((DispatcherTimer)sender).Stop();
            var top = _timerWindow.Top;
            var left = _timerWindow.Left;
            if (_toParent) {
                top = _timerWindow.Owner.Top + (_timerWindow.Owner.Height - _timerWindow.ActualHeight) / 2;
                left = _timerWindow.Owner.Left + (_timerWindow.Owner.Width - _timerWindow.ActualWidth) / 2;
            } else {
                if (System.Windows.Forms.Screen.AllScreens.Length == 0 || IsOnPrimaryMonitor(_timerWindow)) {
                    top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - _timerWindow.Height) / 2;
                    left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - _timerWindow.Width) / 2;
                } else {
                    for (var i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
                        if (i == System.Windows.Forms.Screen.AllScreens.Length - 1) {
                            top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - _timerWindow.Height) / 2;
                            left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - _timerWindow.Width) / 2;
                        } else {
                            if (_timerWindow.Left > System.Windows.Forms.Screen.AllScreens[i].Bounds.X && _timerWindow.Left < System.Windows.Forms.Screen.AllScreens[i + 1].Bounds.X) {
                                top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - _timerWindow.Height) / 2;
                                left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - _timerWindow.Width) / 2;
                            }
                        }
                }
            }
            _timerWindow.Top = top < 0 ? 0 : top;
            _timerWindow.Left = left < 0 ? 0 : left;
        }

        public static void SetBounds(this Window win, double left, double top, double width, double height) {
            win.Left = left;
            win.Top = top;
            win.Width = width;
            win.Height = height;
        }

        public static void SetBounds(this Window win, Point location, Size size) {
            win.SetBounds(location.X, location.Y, size.Width, size.Height);
        }
    }
}