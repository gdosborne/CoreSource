/* File="ThemeChangedEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace OzFramework.Windows.Theme {
    public delegate void ThemeChangedHandler(object sender, ThemeChangedEventArgs e);

    public class ThemeChangedEventArgs : EventArgs {
        public ThemeChangedEventArgs(ThemeWatcher.WindowsTheme theme) {
            Theme = theme;
        }
        public ThemeWatcher.WindowsTheme Theme { get; private set; }
    }
}
