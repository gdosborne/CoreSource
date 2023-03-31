using Ookii.Dialogs.Wpf;
using System.Windows;

namespace OzDBCreate.Code {
    internal static class Helpers {
        //public static bool? ShowYesNoDialog(this TaskDialog dialog, Window parent, string windowTitle,
        //        string mainInstruction, string content, bool addCancel = false) {
        //    if (dialog == null) {
        //        return false;
        //    }
        //    var td = new TaskDialog {
        //        WindowTitle = windowTitle,
        //        MainInstruction = mainInstruction,
        //        Content = content,
        //        AllowDialogCancellation = true,
        //        ButtonStyle = TaskDialogButtonStyle.Standard,
        //        CenterParent = true,
        //        MainIcon = TaskDialogIcon.Shield
        //    };
        //    dialog.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
        //    dialog.Buttons.Add(new TaskDialogButton(ButtonType.No));
        //    if (addCancel) {
        //        dialog.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
        //    }
        //    var buttonResult = dialog.ShowDialog(parent);
        //    bool? result = buttonResult.ButtonType == ButtonType.Cancel
        //       ? null
        //       : buttonResult.ButtonType == ButtonType.Yes
        //           ? true
        //           : false;
        //    return result;
        //}
    }
}
