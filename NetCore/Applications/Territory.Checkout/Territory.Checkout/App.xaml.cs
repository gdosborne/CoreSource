namespace Territory.Checkout {
	using Common.OzApplication;
	using Framewok.Application.Windows.Dialog;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.IO;
	using System.Linq;
	using System.Windows;

	public partial class App : Application {
		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);
			MakeDirectories();
			AppSession = new Session(AppDirectory.FullName, AppName,
				Common.OzApplication.Logging.ApplicationLogger.StorageTypes.FlatFile,
				Common.OzApplication.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
			AppSettings = AppSession.ApplicationSettings;
			UpdateSettings();
		}

		private void MakeDirectories() {
			var baseDir = new DirectoryInfo(Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				AppName));
			if (!baseDir.Exists) baseDir.Create();
			AppDirectory = baseDir;
			var tempDir = AppDirectory.GetDirectories().FirstOrDefault(x => x.Name == "temp");
			if (tempDir == null) tempDir = AppDirectory.CreateSubdirectory("temp");
			if (!tempDir.Exists) tempDir.Create();
			TempDirectory = tempDir;
		}

		private void UpdateSettings() {
			var fontSize = AppSettings.GetValue("Application", "Font.Size", 13.0);

			Resources["TextFontSize"] = fontSize;
			Resources["TitleHeaderFontSize"] = fontSize * 1.5;
			Resources["GlyphFontSize"] = fontSize * 1.25;

		}

		public static string AppName { get; private set; } = "Territory Checkout";
		public static DirectoryInfo AppDirectory { get; private set; }
		public static DirectoryInfo TempDirectory { get; private set; }
		public static Session AppSession { get; private set; }
		public static AppSettings AppSettings { get; private set; }
		public static int NumberOfDaysNeedsWorked { 
			get => AppSettings.GetValue("Application", "DaysForNeedsWorked", 180);
			set => AppSettings.AddOrUpdateSetting("Application", "DaysForNeedsWorked", value);
		}

		public static void DisplayOKDialog(Window window, string title, string content, 
				string mainText) =>
			window.ShowTaskDialog(title, mainText, content,
				TaskDialogIcon.Information, 
				new TaskDialogButton(ButtonType.Ok));

		public static bool DisplayYesNoDialog(Window window, string title, string content,
				string mainText) =>
			window.ShowTaskDialog(title, mainText, content,
				TaskDialogIcon.Shield,
				new TaskDialogButton(ButtonType.Yes),
				new TaskDialogButton(ButtonType.No)).ButtonType == ButtonType.Yes;
	}
}
