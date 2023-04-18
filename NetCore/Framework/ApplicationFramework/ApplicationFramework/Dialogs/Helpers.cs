using Ookii.Dialogs.Wpf;
using System.Windows;

namespace Common.OzApplication.Dialogs {
    public static class Helpers {
        public static bool ShowYesNoDialog(this Window window, string title, string content, TaskDialogIcon icon, 
                int width = 300) =>
            ShowYesNoDialog(window, title, title, content, icon, width);

        public static bool ShowYesNoDialog(this Window window, string title, string main, string content, 
                TaskDialogIcon icon, int width = 300) {
            var td = new TaskDialog {
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title,
                Width = width,
                CenterParent = true                
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog(window);
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
