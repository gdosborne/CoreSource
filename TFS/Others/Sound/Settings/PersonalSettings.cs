using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
namespace SoundSettings
{
	public class PersonalSettings
	{
		private RegistryKey MainKey = null;
		private readonly string KeyName = @"Software\Kingdom Hall Sound";
		public PersonalSettings()
		{
			MainKey = Registry.CurrentUser.CreateSubKey(KeyName);
		}
		public string GetValue(string sectionKey, string valueKey, string defaultValue)
		{
			
			var result = defaultValue;
			var key = MainKey.CreateSubKey(sectionKey);
			result = (string)key.GetValue(valueKey, defaultValue);
			return result;
		}
		public void SetValue(string sectionKey, string valueKey, object value)
		{
			var key = MainKey.CreateSubKey(sectionKey);
			key.SetValue(valueKey, value.ToString());
		}
	}
}
