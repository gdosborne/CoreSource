namespace SDFManager
{
	using GregOsborne.Application;
	using GregOsborne.Application.Media;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Controls;
	using System.Windows.Controls.Ribbon;
	using System.Windows.Input;

	internal static class RecentFilesManager
	{
		#region Public Methods
		public static void AddItem(string fileName)
		{
			if (GetRecentFiles().Values.Any(x => x.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
				RemoveItem(fileName);
			RebuildRecentFiles(fileName);
		}
		public static List<RibbonApplicationMenuItem> GetMenuItems(ICommand command)
		{
			var result = new List<RibbonApplicationMenuItem>();
			foreach (var item in GetRecentFiles().OrderBy(x => x.Key))
			{
				var mnuItem = new RibbonApplicationMenuItem
				{
					Header = System.IO.Path.GetFileName(item.Value),
					Command = command,
					CommandParameter = item.Value,
					ToolTip = item.Value,
					ImageSource = Assembly.GetExecutingAssembly().GetImageSourceByName("Resources/Images/256/database.png")
				};
				result.Add(mnuItem);
			}
			return result;
		}
		public static Dictionary<int, string> GetRecentFiles()
		{
			if (_RecentFiles == null)
			{
				_RecentFiles = new Dictionary<int, string>();
				var fileName = string.Empty;
				var num = 0;
				while (num == 0 || !string.IsNullOrEmpty(fileName))
				{
					fileName = GregOsborne.Application.Settings.GetValue<string>(App.ApplicationName, "RecentFiles", num.ToString(), string.Empty);
					if (string.IsNullOrEmpty(fileName))
						break;
					_RecentFiles.Add(num, fileName);
					num++;
				}
			}
			return _RecentFiles;
		}
		public static void RemoveItem(string fileName)
		{
			if (!GetRecentFiles().Values.Any(x => x.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
				return;
			var itemToRemove = _RecentFiles.FirstOrDefault(x => x.Value.Equals(fileName, StringComparison.OrdinalIgnoreCase));
			_RecentFiles.Remove(itemToRemove.Key);
			RebuildRecentFiles(null);
		}
		#endregion Public Methods

		#region Private Methods
		private static void RebuildRecentFiles(string newItem)
		{
			var num = 1;
			var temp = new Dictionary<int, string>();
			if (newItem != null)
				temp.Add(0, newItem);
			foreach (var item in _RecentFiles.OrderBy(x => x.Key))
			{
				temp.Add(num, item.Value);
				num++;
			}
			GregOsborne.Application.Settings.RemoveKey(App.ApplicationName, "RecentFiles");
			foreach (var item in temp)
			{
				GregOsborne.Application.Settings.SetValue<string>(App.ApplicationName, "RecentFiles", item.Key.ToString(), item.Value);
			}
			_RecentFiles = temp;
		}
		#endregion Private Methods

		#region Private Fields
		private static Dictionary<int, string> _RecentFiles = null;
		#endregion Private Fields
	}
}
