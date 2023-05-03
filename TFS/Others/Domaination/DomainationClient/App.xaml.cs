using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Configuration;
using DomainationClient.Service;
using Sid.Windows.Controls;
using System.Text;
using System.IO;
namespace DomainationClient
{
	public partial class App : System.Windows.Application
	{
		public static void ChangeTheme(string themeName)
		{
			var result = new ResourceDictionary();
			var uri = string.Format(@"pack://application:,,,/Styles/{0}_Theme.xaml", themeName);
			result.Source = new Uri(uri, UriKind.Absolute);
			for (int i = 0; i < App.Current.Resources.MergedDictionaries.Count; i++)
			{
				if (App.Current.Resources.MergedDictionaries[i].Source.OriginalString.ToLower().Contains("_theme"))
				{
					App.Current.Resources.MergedDictionaries.RemoveAt(i);
					App.Current.Resources.MergedDictionaries.Insert(i, result);
					break;
				}
			}
		}
		public static void SetSetting(string name, object value)
		{
			var cm = new ConfigurationManager(App.ConfigFileName);
			if (App.Settings.ContainsKey(name))
				App.Settings[name] = value;
			else
				App.Settings.Add(name, value);
			cm.SetSetting(name, value);
		}
		public static bool TestServer()
		{
			var serverUrl = (string)App.Settings["ServerUrl"];
			var resultString = "successful";
			var result = false;
			using (var client = References.DomainationClient(serverUrl))
			{
				try
				{
					result = client.Test();
				}
				catch
				{
					resultString = "unsuccessful";
				}
			}
			TaskDialog dialog = new TaskDialog();
			dialog.TaskDialogWindow.Title = "Client Test";
			dialog.HeaderIcon = TaskDialogIcon.Information;
			dialog.TaskDialogButton = TaskDialogButton.Ok;
			dialog.Header = string.Format("Your test to the Domaination Server was {0}.", resultString);
			dialog.Content = string.Format("Url = {0}", serverUrl);
			dialog.Show();
			return result;
		}
		public static void DisplayExceptionDetails(Exception ex, bool isFatal)
		{
			TaskDialog dialog = new TaskDialog();
			dialog.TaskDialogWindow.Title = "Error";
			dialog.HeaderIcon = TaskDialogIcon.Error;
			dialog.TaskDialogButton = TaskDialogButton.Ok;
			dialog.Header = "An error has occurred in the application.";
			dialog.Content = ExceptionText(ex);
			dialog.Show();
			if (isFatal)
				App.Current.Shutdown(1);
		}
		private static string ExceptionText(Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			var tabStop = 0;
			while (ex != null)
			{
				var space = new string(' ', tabStop * 3);
				sb.AppendLine(space + ex.Message);
				if (!string.IsNullOrEmpty(ex.StackTrace))
				{
					using (var sr = new StringReader(ex.StackTrace))
					{
						while (sr.Peek() != -1)
						{
							sb.AppendLine(space + sr.ReadLine());
						}
					}
				}
				ex = ex.InnerException;
				tabStop++;
			}
			return sb.ToString();
		}
		public static byte[] Password { get; private set; }
		public static Dictionary<string, object> Settings { get; private set; }
		public static string ConfigFileName = null;
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var configFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Domaination");
			if (!System.IO.Directory.Exists(configFolder))
				System.IO.Directory.CreateDirectory(configFolder);
			ConfigFileName = System.IO.Path.Combine(configFolder, "settings.xml");
			var cm = new ConfigurationManager(ConfigFileName);
			Settings = cm.GetAll();
			Password = new byte[] { 125, 254, 87, 54, 62, 33, 0, 128 };
		}
	}
}
