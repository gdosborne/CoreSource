using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GregOsborne.Application.Windows;
using Microsoft.Win32;

namespace GregOsborne.Application.Registry {
    public static class Extensions {
        public static RegistryKey SettingsKey { get; } = null;

        public static IList<RegistrySection> GetAllSections(this RegistryKey value) {
            var names = value.GetSubKeyNames();
            return names.Select(value.GetSection).ToList();
        }

        public static IDictionary<string, object> GetAllValues(this RegistryKey value) {
            var names = value.GetValueNames();
            return names.ToDictionary(item => item, value.GetValue);
        }

        public static RegistryKey GetApplicationKey(string applicationName) {
            return Microsoft.Win32.Registry.CurrentUser.CreateSubKey($"Software\\{applicationName}");
        }

        public static T GetEnumValue<T>(this RegistryKey key, string name, T defaultValue) where T : IConvertible {
            var value = (string) key.GetValue(name, defaultValue.ToString(CultureInfo.InvariantCulture));
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static RegistrySection GetSection(this RegistryKey value, string name) {
            return new RegistrySection {
                Name = name,
                Values = value.OpenSubKey(name).GetAllValues(),
                Sections = value.OpenSubKey(name).GetAllSections()
            };
        }

        public static T GetValue<T>(this RegistryKey key, string name, T defaultValue) {
            return (T) Convert.ChangeType((string) key.GetValue(name, defaultValue.ToString()), typeof(T));
        }

        public static void Save(this WindowSetting value, string key, string name) {
            var registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(key);
            registryKey?.SetValue(name, value.ToString());
        }

        public static void SetValue<T>(this RegistryKey key, string name, T value) {
            key.SetValue(name, value.ToString());
        }

    }
}