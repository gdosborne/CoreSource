namespace GregOsborne.Application.Theme {
	using System;
	using System.IO;
	using System.Linq;
	using System.Windows;

	public static class Extensions {
		public static void AppendToTheme(this ResourceDictionary value) => value.AppendToTheme(true);
		public static void AppendToTheme(this ResourceDictionary value, bool isReplaceIfExists) {
			if (value == null) {
				throw new ArgumentNullException(nameof(value));
			}

			if ((Application.Current.Resources.MergedDictionaries.Count == 0 || value.Source == null) && isReplaceIfExists) {
				Application.Current.Resources.MergedDictionaries.Add(value);
				return;
			}
			var current = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => Path.GetFileName(x.Source.OriginalString).Equals(Path.GetFileName(value.Source.OriginalString), StringComparison.OrdinalIgnoreCase));
			if (current == null || (current != null && isReplaceIfExists)) {
				Application.Current.Resources.MergedDictionaries.Add(value);
			}
		}

		public static void ApplyToTheme(this ResourceDictionary value) => value.ApplyToTheme("default.styles.xaml");
		public static void ApplyToTheme(this ResourceDictionary value, string fileNameToReplace) => value.ApplyToTheme(fileNameToReplace, true);
		public static void ApplyToTheme(this ResourceDictionary value, string fileNameToReplace, bool isAddIfNotFound) {
			if (value == null) {
				throw new ArgumentNullException(nameof(value));
			}

			if ((Application.Current.Resources.MergedDictionaries.Count == 0 || string.IsNullOrEmpty(fileNameToReplace)) && isAddIfNotFound) {
				Application.Current.Resources.MergedDictionaries.Add(value);
				return;
			}
			var compareTo = value.Source == null ? fileNameToReplace : Path.GetFileName(value.Source.OriginalString);
			var current = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => Path.GetFileName(x.Source.OriginalString).Equals(compareTo, StringComparison.OrdinalIgnoreCase));
			if (current == null) {
				if (isAddIfNotFound) {
					Application.Current.Resources.MergedDictionaries.Add(value);
				}
				return;
			}
			var index = Application.Current.Resources.MergedDictionaries.IndexOf(current);
			Application.Current.Resources.MergedDictionaries.RemoveAt(index);
			Application.Current.Resources.MergedDictionaries.Insert(index, value);
		}
	}
}
