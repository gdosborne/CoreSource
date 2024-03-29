using System;
using System.Collections.Generic;
using MyApplication.Windows;
using Microsoft.Win32;

namespace MyApplication.Registry
{
	public static class Extensions
	{
		#region Private Fields
		private static RegistryKey SettingsKey = null;
		#endregion

		#region Public Methods

		public static List<RegistrySection> GetAllSections(this RegistryKey value)
		{
			var result = new List<RegistrySection>();
			var names = value.GetSubKeyNames();
			foreach (var item in names)
			{
				result.Add(value.GetSection(item));
			}
			return result;
		}

		public static Dictionary<string, object> GetAllValues(this RegistryKey value)
		{
			var result = new Dictionary<string, object>();
			var names = value.GetValueNames();
			foreach (var item in names)
			{
				result.Add(item, value.GetValue(item));
			}
			return result;
		}

		public static RegistryKey GetApplicationKey(string applicationName)
		{
			return Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Format("Software\\{0}", applicationName));
		}

		public static T GetEnumValue<T>(this RegistryKey key, string name, T defaultValue) where T : IConvertible
		{
			var value = (string)key.GetValue(name, defaultValue.ToString());
			return (T)Enum.Parse(typeof(T), value, true);
		}

		public static RegistrySection GetSection(this RegistryKey value, string name)
		{
			var result = new RegistrySection
			{
				Name = name,
				Values = value.OpenSubKey(name).GetAllValues(),
				Sections = value.OpenSubKey(name).GetAllSections()
			};
			return result;
		}

		public static T GetValue<T>(this RegistryKey key, string name, T defaultValue)
		{
			return (T)Convert.ChangeType((string)key.GetValue(name, defaultValue.ToString()), typeof(T));
		}

		public static void Save(this WindowSetting value, string key, string name)
		{
			Microsoft.Win32.Registry.CurrentUser.CreateSubKey(key).SetValue(name, value.ToString());
		}

		public static void SetValue<T>(this RegistryKey key, string name, T value)
		{
			key.SetValue(name, value.ToString());
		}

		#endregion
	}
}