namespace OSInstallerBuilder
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

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
		}
		#endregion Protected Methods

		#region Public Fields
		public static readonly string ApplicationName = "OSInstallerBuilder";
		#endregion Public Fields
	}
}
