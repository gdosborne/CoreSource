using System;
using System.Windows;

namespace GregOsborne.Application.Windows {
    public class WindowSetting {
        public WindowSetting() {
        }

        public WindowSetting(double left, double top, double width, double height)
            : this() {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public WindowSetting(double left, double top, double width, double height, WindowState windowState)
            : this() {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            WindowState = windowState;
        }

        public double Height { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Width { get; set; }

        public WindowState WindowState { get; set; }

        public static WindowSetting FromWindow(Window win) {
            return new WindowSetting {
                Left = win.RestoreBounds.Left,
                Top = win.RestoreBounds.Top,
                Width = win.RestoreBounds.Width,
                Height = win.RestoreBounds.Height,
                WindowState = win.WindowState
            };
        }

        public static WindowSetting Parse(string value) {
            var val = value.Replace("{", string.Empty).Replace("}", string.Empty);
            var parts = val.Split(',');
            var result = new WindowSetting {
                Left = double.Parse(parts[0]),
                Top = double.Parse(parts[1]),
                Width = double.Parse(parts[2]),
                Height = double.Parse(parts[3])
            };
            if (parts.Length > 4)
                result.WindowState = (WindowState) Enum.Parse(typeof(WindowState), parts[4]);
            return result;
        }

        public static bool TryParse(string value, out WindowSetting test) {
            test = null;
            try {
                test = Parse(value);
                return true;
            }
            catch {
                return false;
            }
        }

        public void SetWindow(Window win) {
            win.Left = Left < 0 ? 0 : Left;
            win.Top = Top < 0 ? 0 : Top;
            win.Width = Width;
            win.Height = Height;
            win.WindowState = WindowState;
        }

        public override string ToString() {
            return $"{{{Left},{Top},{Width},{Height},{WindowState}}}";
        }
    }
}