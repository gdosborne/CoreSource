using Common.OzApplication.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using static Common.OzApplication.NativeMethods;

namespace Common.OzApplication.Windows {

    public static class Extension {
        private static Window timerWindow;

        private static bool toParent;

        private static DispatcherTimer windowTimer;

        public static Rect? GetItemBounds(this ListBox listBox, object o) {
            int itemIndex = listBox.Items.IndexOf(o);
            var listBoxItem = GetListBoxItem(listBox, itemIndex);

            if (listBoxItem == null) return null;

            var transform = listBoxItem.TransformToVisual((Visual)listBox.Parent).Transform(new Point());

            var listViewItemBounds = VisualTreeHelper.GetDescendantBounds(listBoxItem);
            listViewItemBounds.Offset(transform.X, transform.Y);

            return listViewItemBounds;
        }

        private static ListBoxItem GetListBoxItem(ListBox listBox, int index) {
            if (listBox.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            if (index == -1) return null;

            return listBox.ItemContainerGenerator.ContainerFromIndex(index).As<ListBoxItem>();
        }

        public static void RemoveChild(this DependencyObject parent, UIElement child) {
            if (parent.Is<Panel>()) {
                parent.As<Panel>().Children.Remove(child);
            }
            else if (parent.Is<Decorator>()) {
                parent.As<Decorator>().Child = null;
            }
            else if (parent.Is<ContentPresenter>()) {
                parent.As<ContentPresenter>().Content = null;
            }
            else if (parent.Is<ContentControl>()) {
                parent.As<ContentControl>().Content = null;
            }
            else if (parent.Is<PageContent>()) {
                var ch = parent.As<PageContent>().Child;
                parent.As<PageContent>().Child = null;
            }
        }

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

        public static T FindParent<T>(this DependencyObject depObj) where T : DependencyObject {
            if (depObj == null) {
                return (T)(object)null;
            }
            if (depObj.GetType() == typeof(T)) {
                return (T)(object)depObj;
            }

            var parent = VisualTreeHelper.GetParent(depObj);
            if(parent == null) {
                return (T)null;
            }
            if (parent.GetType() == typeof(T)) {
                return (T)(object)parent;
            }
            return parent.FindParent<T>();
        }

        public static T FindChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject {
            if (parent == null) {
                return null;
            }

            T foundChild = null;
            try {
                var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                if (childrenCount == 0 && parent is TextBlock) {
                    childrenCount = (parent as TextBlock).Inlines.Count;
                }
                if (childrenCount > 0) {
                    for (var i = 0; i < childrenCount; i++) {
                        var child = default(DependencyObject);
                        if (parent.Is<TextBlock>()) {
                            child = parent.As<TextBlock>().Inlines.Cast<Inline>().ElementAt(i);
                        }
                        else {
                            child = VisualTreeHelper.GetChild(parent, i);
                        }
                        if (child.Is<T>()) {
                            if (child.Is<Inline>() && child.As<Inline>().Name.Equals(childName)) {
                                foundChild = child.As<T>();
                                break;
                            }
                            else if (child.Is<FrameworkElement>() && child.As<FrameworkElement>().Name.Equals(childName)) {
                                foundChild = child.As<T>();
                                break;
                            }
                            else {
                                continue;
                            }
                        }
                        else {
                            foundChild = FindChildByName<T>(child, childName);
                            if (foundChild != null) {
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
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
            var result = default(FrameworkElement);
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++) {
                var child = VisualTreeHelper.GetChild(dp, i);
                if (child is T) {
                    result = (FrameworkElement)child;
                }
                else {
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

        public static double ScreenDPI(this Window window) {
            //var screen = window.GetScreen();
            //return screen.BitsPerPixel;
            using var mc = new ManagementClass("Win32_DesktopMonitor");
            using var moc = mc.GetInstances();
            var PixelsPerXLogicalInch = 0; // dpi for x
            var PixelsPerYLogicalInch = 0; // dpi for y

            foreach (ManagementObject each in moc) {
                PixelsPerXLogicalInch = int.Parse(each.Properties["PixelsPerXLogicalInch"].Value.ToString());
                PixelsPerYLogicalInch = int.Parse(each.Properties["PixelsPerYLogicalInch"].Value.ToString());
            }
            return PixelsPerXLogicalInch;
        }

        public static int PrimaryScreenWidth(this System.Windows.Forms.Screen screen) {
            return System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
        }

        public static int PrimaryScreenHeight(this System.Windows.Forms.Screen screen) {
            return System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
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

        public static Size Measure(this string value, Style style, double suggestedWidth = double.PositiveInfinity) {
            var result = new Size();
            try {
                var l = new TextBlock {
                    Style = style,
                    Text = value
                };
                if (!double.IsPositiveInfinity(suggestedWidth)) {
                    l.Width = suggestedWidth;
                }

                //l.TextWrapping = !double.IsPositiveInfinity(suggestedWidth) ? TextWrapping.Wrap : TextWrapping.NoWrap;
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
            }
            else {
                if (System.Windows.Forms.Screen.AllScreens.Length == 0 || IsOnPrimaryMonitor(timerWindow)) {
                    top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - timerWindow.Height) / 2;
                    left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - timerWindow.Width) / 2;
                }
                else {
                    for (var i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++) {
                        if (i == System.Windows.Forms.Screen.AllScreens.Length - 1) {
                            top = (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Height - timerWindow.Height) / 2;
                            left = System.Windows.Forms.Screen.AllScreens[i].Bounds.X + (System.Windows.Forms.Screen.AllScreens[i].WorkingArea.Width - timerWindow.Width) / 2;
                        }
                        else {
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

        public static void SetBounds(this Window win, AppSettings appSettings, bool includeState = false) {
            if (win == null) {
                throw new ArgumentNullException("Window is missing");
            }
            var name = win.GetType().Name;

            var left = appSettings.GetValue(name, "Left", 0.0);
            var top = appSettings.GetValue(name, "Top", 0.0);
            var width = appSettings.GetValue(name, "Width", win.RestoreBounds.Width);
            var height = appSettings.GetValue(name, "Height", win.RestoreBounds.Height);
            if (left == 0 && win.Owner != null) {
                left = win.Owner.Left + (win.Owner.ActualWidth - width) / 2.0;
                top = win.Owner.Top = (win.Owner.ActualHeight - height) / 2.0;
            }

            var winState = appSettings.GetValue(name, "WindowState", win.WindowState);
            win.Left =
                double.IsNaN(left)
                    || left == double.PositiveInfinity
                    || left == double.NegativeInfinity ? 100 : left;
            win.Top =
                double.IsNaN(top)
                    || top == double.PositiveInfinity
                    || top == double.NegativeInfinity ? 100 : top;
            win.Width =
                double.IsNaN(width)
                    || width == double.PositiveInfinity
                    || width == double.NegativeInfinity ? 800 : width;
            win.Height =
                double.IsNaN(height)
                    || height == double.PositiveInfinity
                    || height == double.NegativeInfinity ? 600 : height;
            if (includeState)
                win.WindowState = winState;
        }

        public static void SaveBounds(this Window win, AppSettings appSettings, bool includeState = false) {
            var name = win.GetType().Name;
            if (win.RestoreBounds != Rect.Empty) {
                appSettings.AddOrUpdateSetting(name, "Left", win.RestoreBounds.Left);
                appSettings.AddOrUpdateSetting(name, "Top", win.RestoreBounds.Top);
                appSettings.AddOrUpdateSetting(name, "Width", win.RestoreBounds.Width);
                appSettings.AddOrUpdateSetting(name, "Height", win.RestoreBounds.Height);
            }
            else {
                appSettings.AddOrUpdateSetting(name, "Left", win.Left);
                appSettings.AddOrUpdateSetting(name, "Top", win.Top);
                appSettings.AddOrUpdateSetting(name, "Width", win.Width);
                appSettings.AddOrUpdateSetting(name, "Height", win.Height);
            }
            if (includeState)
                appSettings.AddOrUpdateSetting(name, "WindowState", win.WindowState);
        }
    }
}
