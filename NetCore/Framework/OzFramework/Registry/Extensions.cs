/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using w32 = Microsoft.Win32;

namespace OzFramework.Registry {
    public static class Extensions {
        public static RegistryKey SettingsKey { get; } = null;

        public static IList<RegistrySection> GetAllSections(this RegistryKey value) => value.GetSubKeyNames().Select(value.GetSection).ToList();
        public static IDictionary<string, object> GetAllValues(this RegistryKey value) => value?.GetValueNames().ToDictionary(item => item, value.GetValue);
        public static RegistryKey GetApplicationKey(string applicationName) => w32.Registry.CurrentUser.CreateSubKey($"Software\\{applicationName}");
        public static T GetEnumValue<T>(this RegistryKey key, string name, T defaultValue) where T : IConvertible => (T)Enum.Parse(typeof(T), (string)key.GetValue(name, defaultValue.ToString(CultureInfo.InvariantCulture)), true);
        public static RegistrySection GetSection(this RegistryKey value, string name) => new RegistrySection {
            Name = name,
            Values = value.OpenSubKey(name).GetAllValues(),
            Sections = value.OpenSubKey(name).GetAllSections()
        };
        public static T GetValue<T>(this RegistryKey key, string name, T defaultValue) => (T)Convert.ChangeType((string)key.GetValue(name, defaultValue.ToString()), typeof(T));
        public static void SetValue<T>(this RegistryKey key, string name, T value) => key.SetValue(name, value.ToString());
    }
}
