using SoundSettings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace KHS4
{
	public partial class App : Application
	{
		private static PersonalSettings _MySettings = null;
		public static PersonalSettings MySettings
		{
			get
			{
				if (_MySettings == null)
					_MySettings = new PersonalSettings();
				return _MySettings;
			}
		}
		public static void DisplayExecption(Exception ex, bool isFatal)
		{
			using (var dialog = new Ookii.Dialogs.Wpf.TaskDialog
			{
				AllowDialogCancellation = false,
				Content = ex.Message,
				MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Error,
				MainInstruction = ex.StackTrace,
				MinimizeBox = false,
				WindowTitle = "Application Error"
			})
			{
				Ookii.Dialogs.Wpf.TaskDialogButton okButton = new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok);
				dialog.Buttons.Add(okButton);
				dialog.ShowDialog();
			}
			if (isFatal)
				Environment.Exit(9999);
		}
	}
}
