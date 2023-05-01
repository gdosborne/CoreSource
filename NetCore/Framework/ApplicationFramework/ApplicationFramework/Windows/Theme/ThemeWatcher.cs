using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Management;
using System.Security.Principal;

namespace Common.AppFramework.Windows.Theme {
    public sealed class ThemeWatcher {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegistryValueName = "AppsUseLightTheme";

        public enum WindowsTheme {
            Light,
            Dark
        }

        public event ThemeChangedHandler ThemeChanged;

        public void WatchTheme() {
            var currentUser = WindowsIdentity.GetCurrent();
            var query = string.Format(
                CultureInfo.InvariantCulture,
                @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
                currentUser.User.Value,
                RegistryKeyPath.Replace(@"\", @"\\"),
                RegistryValueName);

            try {
                var watcher = new ManagementEventWatcher(query);
                watcher.EventArrived += (sender, args) => {
                    var newWindowsTheme = GetWindowsTheme();
                    var e = new ThemeChangedEventArgs(newWindowsTheme);
                    ThemeChanged?.Invoke(this, e);
                };

                // Start listening for events
                watcher.Start();
            }
            catch (System.Exception) {
                // This can fail on Windows 7
            }

            WindowsTheme initialTheme = GetWindowsTheme();
        }

        private static WindowsTheme GetWindowsTheme() {
            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            var registryValueObject = key?.GetValue(RegistryValueName);
            if (registryValueObject == null) {
                return WindowsTheme.Light;
            }

            var registryValue = (int)registryValueObject;
            return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
        }
    }
}
