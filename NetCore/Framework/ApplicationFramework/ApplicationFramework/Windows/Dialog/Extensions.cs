namespace Framewok.Application.Windows.Dialog {
    using Ookii.Dialogs.Wpf;
    using System.Windows;

    public static class Extensions {
		public static TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle, string headerText, string instructionText, TaskDialogIcon icon, params TaskDialogButton[] buttons) => owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, null, null, out var dummy, buttons);

		public static TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle, string headerText, string instructionText, TaskDialogIcon icon, string expandedText, params TaskDialogButton[] buttons) => owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, expandedText, null, out var dummy, buttons);

		public static TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle, string headerText, string instructionText, TaskDialogIcon icon, string expandedText, string verificationText, params TaskDialogButton[] buttons) => owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, expandedText, verificationText, out var dummy, buttons);

		public static TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle, string headerText, string instructionText, TaskDialogIcon icon, string expandedText, string verificationText, out bool isVerificationChecked, params TaskDialogButton[] buttons) {
			isVerificationChecked = false;
			var td = new TaskDialog {
				WindowTitle = windowTitle,
				Content = instructionText,
				MainInstruction = headerText,
				ExpandedInformation = expandedText,
				MainIcon = icon,
				VerificationText = verificationText,

				AllowDialogCancellation = true,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				CenterParent = false,
				MinimizeBox = false,

			};
			foreach (var btn in buttons) {
				td.Buttons.Add(btn);
			}
			var result = td.ShowDialog(owner);
			isVerificationChecked = td.IsVerificationChecked;
			return result;
		}
	}
}
