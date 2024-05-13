namespace OzDB.Application.Registry {
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using OzDB.Application.Windows;
	using Microsoft.Win32;

	public static class Extensions {
		public static RegistryKey SettingsKey { get; } = null;

		public static IList<RegistrySection> GetAllSections(this RegistryKey value) => value.GetSubKeyNames().Select(value.GetSection).ToList();
		public static IDictionary<string, object> GetAllValues(this RegistryKey value) => value.GetValueNames().ToDictionary(item => item, value.GetValue);
		public static RegistryKey GetApplicationKey(string applicationName) => Microsoft.Win32.Registry.CurrentUser.CreateSubKey($"Software\\{applicationName}");
		public static T GetEnumValue<T>(this RegistryKey key, string name, T defaultValue) where T : IConvertible => (T)Enum.Parse(typeof(T), (string)key.GetValue(name, defaultValue.ToString(CultureInfo.InvariantCulture)), true);
		public static RegistrySection GetSection(this RegistryKey value, string name) => new RegistrySection {
			Name = name,
			Values = value.OpenSubKey(name).GetAllValues(),
			Sections = value.OpenSubKey(name).GetAllSections()
		};
		public static T GetValue<T>(this RegistryKey key, string name, T defaultValue) => (T)Convert.ChangeType((string)key.GetValue(name, defaultValue.ToString()), typeof(T));
		public static void Save(this WindowSetting value, string key, string name) => Microsoft.Win32.Registry.CurrentUser.CreateSubKey(key)?.SetValue(name, value.ToString());
		public static void SetValue<T>(this RegistryKey key, string name, T value) => key.SetValue(name, value.ToString());
	}
}