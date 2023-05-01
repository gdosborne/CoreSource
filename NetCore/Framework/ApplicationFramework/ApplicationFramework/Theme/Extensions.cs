using Microsoft.Win32;
using System.Globalization;
using System.Management;
using System.Security.Principal;
using System.Windows;

namespace Common.AppFramework.Theme {
    public delegate void ThemeChangedDelegate(WindowsTheme newTheme);

    public enum WindowsTheme {
        Default,
        Light,
        Dark
    }


    public static class Extensions {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegistryValueName = "AppsUseLightTheme";

        private static WindowsTheme currentTheme = WindowsTheme.Default;

        //public static void CopyResourceDictionary(ResourceDictionary rdSource, ResourceDictionary rdTarget) 
        //    => CopyResourceDictionary(rdSource, rdTarget, false);
        //public static void CopyResourceDictionary(ResourceDictionary rdSource, ResourceDictionary rdTarget, bool persist) {
        //    foreach (DictionaryEntry item in rdSource) {
        //        if (rdTarget[item.Key] != null) {
        //            rdTarget[item.Key] = item.Value;
        //        }
        //    }
        //    if (persist) {

        //    }
        //}

        public static void WriteToOutput(this ResourceDictionary rdSource) {
            foreach (var key in rdSource.Keys) {
                System.Diagnostics.Debug.WriteLine($"{key}, {rdSource[key]}");
            }
        }


        public static void WatchTheme(ThemeChangedDelegate themeChanged) {
            var currentUser = WindowsIdentity.GetCurrent();
            var query = string.Format(
                CultureInfo.InvariantCulture,
                @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
                currentUser.User.Value,
                RegistryKeyPath.Replace(@"\", @"\\"),
                RegistryValueName);

            try {
                currentTheme = GetWindowsTheme();
                var watcher = new ManagementEventWatcher(query);
                watcher.EventArrived += (sender, args) => {
                    var newWindowsTheme = GetWindowsTheme();
                    if (newWindowsTheme != currentTheme) {
                        themeChanged?.Invoke(newWindowsTheme);
                    }
                };
                watcher.Start();
            }
            catch (System.Exception) {
                // This can fail on Windows 7
            }
        }

        public static WindowsTheme GetWindowsTheme() {
            using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKeyPath)) {
                var registryValueObject = key?.GetValue(RegistryValueName);
                if (registryValueObject == null) {
                    return WindowsTheme.Light;
                }
                var registryValue = (int)registryValueObject;
                return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
            }
        }

        public static void SetWindowsTheme(WindowsTheme theme) {
            using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKeyPath)) {
                if (theme == WindowsTheme.Default) {
                    key?.DeleteValue(RegistryValueName);
                }
                else {
                    key?.SetValue(RegistryValueName, theme == WindowsTheme.Dark ? 0 : 1);
                }
            }
        }
    }
}
