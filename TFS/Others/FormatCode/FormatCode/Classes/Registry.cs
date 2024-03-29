using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FormatCode.Classes
{
	public static class Registry
	{
		static Registry()
		{
			SettingsKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\FormatCode").CreateSubKey(@"Settings");
		}
		private static Microsoft.Win32.RegistryKey SettingsKey = null;
		public static T GetValue<T>(string name, T defaultValue)
		{
			return (T)Convert.ChangeType((string)SettingsKey.GetValue(name, defaultValue.ToString()), typeof(T));
		}
		public static void SetValue<T>(string name, T value)
		{
			SettingsKey.SetValue(name, value.ToString());
		}
	}
}
