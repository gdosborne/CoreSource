using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OokiiDialogs = Ookii.Dialogs.Wpf;

namespace GregOsborne.Application.Dialogs {
    public static class DialogHelpers {
        public static bool ShowYesNoDialog(string title, string message, OokiiDialogs.TaskDialogIcon icon, int dialogWidth = 250) {
            var dlg = new OokiiDialogs.TaskDialog {
                AllowDialogCancellation = true,
                ButtonStyle = OokiiDialogs.TaskDialogButtonStyle.Standard,
                CenterParent = true,
                Content = message,
                WindowTitle = title,
                MainIcon = icon,
                MainInstruction = title,
                MinimizeBox = false,
                Width = dialogWidth
            };
            var yesBtn = new OokiiDialogs.TaskDialogButton(OokiiDialogs.ButtonType.Yes);
            var noBtn = new OokiiDialogs.TaskDialogButton(OokiiDialogs.ButtonType.No);
            dlg.Buttons.Add(yesBtn);
            dlg.Buttons.Add(noBtn);
            var result = dlg.ShowDialog();
            return result == yesBtn;
        }

        public static void ShowOKDialog(string title, string message, OokiiDialogs.TaskDialogIcon icon, int dialogWidth = 250) {
            var dlg = new OokiiDialogs.TaskDialog {
                AllowDialogCancellation = true,
                ButtonStyle = OokiiDialogs.TaskDialogButtonStyle.Standard,
                CenterParent = true,
                Content = message,
                WindowTitle = title,
                MainIcon = icon,
                MainInstruction = title,
                MinimizeBox = false,
                Width = dialogWidth
            };
            var okBtn = new OokiiDialogs.TaskDialogButton(OokiiDialogs.ButtonType.Ok);
            dlg.Buttons.Add(okBtn);
            dlg.ShowDialog();
        }

        public static string ShowOpenFileDialog(string title, string initialDirectory, (string extension, string name)[] filters) {
            var dlg = new OpenFileDialog {
                AddExtension = true,
                InitialDirectory = initialDirectory,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = string.Join('|', filters.Select(f => $"{f.name}|{f.extension}")),
                Multiselect = false,
                OkRequiresInteraction = true,
                RestoreDirectory = true,
                ShowHiddenFiles = true,
                Title = title,
            };
            var result = dlg.ShowDialog();
            if (result == DialogResult.Cancel) return null;
            return dlg.FileName;
        }

    }
}
