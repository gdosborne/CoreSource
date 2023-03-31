using Common.Application.Theme;
using System;

namespace ApplicationFramework.Windows.Theme {
    public delegate void ThemeChangedHandler(object sender, ThemeChangedEventArgs e);

    public class ThemeChangedEventArgs : EventArgs {
        public ThemeChangedEventArgs(ThemeWatcher.WindowsTheme theme) {
            Theme = theme;
        }
        public ThemeWatcher.WindowsTheme Theme { get; private set; }
    }
}
