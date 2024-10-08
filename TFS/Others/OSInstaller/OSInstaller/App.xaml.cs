namespace OSInstaller
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Windows;
	using System.Windows.Interop;
	using System.Xml.Linq;

	public partial class App : Application
	{
		#region Public Methods
		public static TaskDialog GetTaskDialog(string title, string largeText, string message, string additionalText, TaskDialogStandardIcon icon, IntPtr windowHandle, TaskDialogStandardButtons buttons)
		{
			return GetTaskDialog(title, largeText, message, additionalText, icon, windowHandle, buttons, null);
		}
		public static TaskDialog GetTaskDialog(string title, string largeText, string message, string additionalText, TaskDialogStandardIcon icon, IntPtr windowHandle, TaskDialogStandardButtons buttons, System.Drawing.Icon customIcon)
		{
			var td = new TaskDialog
			{
				Cancelable = false,
				Caption = title,
				DetailsExpandedLabel = "Collapse",
				DetailsCollapsedLabel = "Expand",
				DetailsExpanded = false,
				Icon = TaskDialogStandardIcon.Information,
				OwnerWindowHandle = windowHandle,
				StartupLocation = TaskDialogStartupLocation.CenterOwner,
				InstructionText = largeText,
				Text = message,
				DetailsExpandedText = additionalText,
				StandardButtons = buttons
			};
			return td;
		}
		#endregion Public Methods

		#region Protected Methods
		protected override void OnStartup(StartupEventArgs e)
		{
			var exeFileName = Assembly.GetExecutingAssembly().Location;
			if (e.Args.Length == 0)
			{
				GetTaskDialog("Error", string.Format("You must specify an installer file to run.\rError Code: {0}", NO_INSTALLER_FILE_SPECIFIED), exeFileName, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
				App.Current.Shutdown(NO_INSTALLER_FILE_SPECIFIED);
				return;
			}
			else
			{
				var fileName = e.Args[0];
				if (!System.IO.File.Exists(fileName))
				{
					GetTaskDialog("Error", string.Format("The file \"{0}\" does not exist.\rError Code: {1}", fileName, INSTALLER_FILE_NOT_FOUND), exeFileName, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
					App.Current.Shutdown(INSTALLER_FILE_NOT_FOUND);
					return;
				}
				else
				{
					XDocument installerDoc = null;
					try
					{
						installerDoc = XDocument.Load(fileName);
					}
					catch (Exception)
					{
						GetTaskDialog("Error", string.Format("The file \"{0}\" is not a valid installer file.\rError Code: {1}", fileName, INVALID_INSTALLER_FILE), exeFileName, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
						App.Current.Shutdown(INVALID_INSTALLER_FILE);
						return;
					}
					var root = installerDoc.Root;
					if (!root.Name.LocalName.Equals("installation"))
					{
						GetTaskDialog("Error", string.Format("The file \"{0}\" is not a valid installer file.\rError Code: {1}", fileName, INSTALLER_FILE_ROOT_INVALID), exeFileName, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
						App.Current.Shutdown(INSTALLER_FILE_ROOT_INVALID);
						return;
					}
					InstallerFileName = fileName;
				}
			}
			base.OnStartup(e);
		}
		#endregion Protected Methods

		#region Public Fields
		public static int INSTALLER_FILE_LOAD_ERROR = 1005;
		public static int INSTALLER_FILE_NOT_FOUND = 1002;
		public static int INSTALLER_FILE_ROOT_INVALID = 1004;
		public static int INSTALLER_TYPE_LOAD_ERROR = 1006;
		public static int INVALID_INSTALLER_FILE = 1003;
		public static int NO_INSTALLER_FILE_SPECIFIED = 1001;
		#endregion Public Fields

		#region Public Properties
		public static string InstallerFileName { get; set; }
		#endregion Public Properties
	}
}
