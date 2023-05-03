namespace EnableVersioning {
    using System;
    using System.IO;
    using System.Text;
    using System.Windows;
    using GregOsborne.Application;
    using GregOsborne.Application.Exception;
    using GregOsborne.Dialogs;
    using static GregOsborne.Application.Logging.ApplicationLogger;

    public partial class App : System.Windows.Application {

		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) => WriteToLog(e.Exception);

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => WriteToLog((Exception)e.ExceptionObject);

		protected override void OnExit(ExitEventArgs e) => WriteToLog(EntryTypes.Information, "Application end");

		protected override void OnStartup(StartupEventArgs e) {
			Session = new Session(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName), 
                ApplicationName, StorageTypes.FlatFile, StorageOptions.None, smtpServer: EMailer.DefaultSMTPServer);
            Session.Logger.LogDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName, "Logs");

            WriteToLog(EntryTypes.Information, "Application start");
			//DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			//AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
		}

		public static string ApplicationName => "Enable UpdateVersion for Project";

		public static Session Session { get; private set; } = default;

		public static void WriteToLog(EntryTypes entryType, string message) {
			Session.Logger.LogMessage(message, entryType);
			if (entryType == EntryTypes.Error) {
				var td = new TaskDialog {
					WindowTitle = $"Error in {ApplicationName}",
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Warning,
					Content = message,
					Width = 400
				};
				td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
				td.ShowDialog(Current.MainWindow);
				Environment.Exit(0);
			}
		}

		public static void WriteToLog(EntryTypes entryType, StringBuilder message) => WriteToLog(entryType, message.ToString());

		public static void WriteToLog(Exception ex) => WriteToLog(EntryTypes.Error, $"{ex.Message}{Environment.NewLine}{ex.ToStringRecurse()}");
	}
}