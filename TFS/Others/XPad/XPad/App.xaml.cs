namespace XPad
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows;

	public partial class App : Application
	{
		public static T GetSetting<T>(string appName, string key, string name, T defaultValue)
		{
			return GregOsborne.Application.Settings.GetValue<T>(appName, key, name, defaultValue);
		}
		public static void SetSetting<T>(string appName, string key, string name, T value)
		{
			GregOsborne.Application.Settings.SetValue<T>(appName, key, name, value);
		}
	}
}
