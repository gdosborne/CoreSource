using Ookii.Dialogs.Wpf;

namespace ApplicationFramework.Dialogs {
    public static class Helpers {
        public static bool ShowYesNoDialog(string title, string content, TaskDialogIcon icon, int width = 300) =>
            ShowYesNoDialog(title, title, content, icon, width);

        public static bool ShowYesNoDialog(string title, string main, string content, TaskDialogIcon icon, int width = 300) {
            var td = new TaskDialog {
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title,
                Width = width                
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            return result != null && result.ButtonType == ButtonType.Yes;
        }

        public static void ShowOKDialog(string title, string content, TaskDialogIcon icon, int width = 200) =>
            ShowOKDialog(title, title, content, icon, width);

        public static void ShowOKDialog(string title, string main, string content, TaskDialogIcon icon, int width = 200) {
            var td = new TaskDialog {
                Width = width,
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
            td.ShowDialog();
        }
    }
}
