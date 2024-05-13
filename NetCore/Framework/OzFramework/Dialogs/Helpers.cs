/* File="Helpers"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using SysIO = System.IO;
using System.Linq;
using System.Resources;
using System.Windows;
using OD = Ookii.Dialogs.Wpf;
using System.ComponentModel;
using Universal.Common;
using OzFramework.Text;

namespace OzFramework.Dialogs {
    public static class Helpers {
        public static OD.TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle,
                string headerText, string instructionText, OD.TaskDialogIcon icon, params OD.TaskDialogButton[] buttons) =>
            owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, null, null, false, out var dummy, buttons);

        public static OD.TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle,
                string headerText, string instructionText, OD.TaskDialogIcon icon, string expandedText, params OD.TaskDialogButton[] buttons) =>
            owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, expandedText, null, false, out var dummy, buttons);

        public static OD.TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle,
                string headerText, string instructionText, OD.TaskDialogIcon icon, string expandedText, string verificationText, params OD.TaskDialogButton[] buttons) =>
            owner.ShowTaskDialog(windowTitle, headerText, instructionText, icon, expandedText, verificationText, false, out var dummy, buttons);

        public static OD.TaskDialogButton ShowTaskDialog(this Window owner, string windowTitle, string headerText,
                string instructionText, OD.TaskDialogIcon icon, string expandedText, string verificationText, bool initialCheckValue,
                out bool isVerificationChecked, params OD.TaskDialogButton[] buttons) {
            isVerificationChecked = false;
            var td = new OD.TaskDialog {
                WindowTitle = windowTitle,
                Content = instructionText,
                MainInstruction = headerText,
                ExpandedInformation = expandedText,
                MainIcon = icon,
                VerificationText = verificationText,
                IsVerificationChecked = initialCheckValue,
                Width = 225,
                AllowDialogCancellation = true,
                ButtonStyle = OD.TaskDialogButtonStyle.Standard,
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

        public static bool ShowYesNoDialog(this Window window, string title, string content, OD.TaskDialogIcon icon) =>
            ShowYesNoDialog(window, title, content, icon, 300);

        public static bool ShowYesNoDialog(this Window window, string title, string content, OD.TaskDialogIcon icon,
                    int width) => ShowYesNoDialog(window, title, content, icon, width, default);

        public static bool ShowYesNoDialog(this Window window, string title, string content, OD.TaskDialogIcon icon,
                int width, string footerText) => ShowYesNoDialog(window, title, title, content, icon, width, footerText,
                    default, out var isChecked);

        public static bool ShowYesNoDialog(this Window window, string title, string main, string content,
                OD.TaskDialogIcon icon, int width, string footerText,
                string verificationText, out bool isVerificationChecked) {
            isVerificationChecked = false;
            var td = new OD.TaskDialog {
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = OD.TaskDialogButtonStyle.Standard,
                WindowTitle = title,
                Width = width,
                CenterParent = true,
                Footer = footerText,
                IsVerificationChecked = isVerificationChecked,
                VerificationText = verificationText                
            };
            td.Buttons.Add(new OD.TaskDialogButton { ButtonType = OD.ButtonType.Yes });
            td.Buttons.Add(new OD.TaskDialogButton { ButtonType = OD.ButtonType.No });
            var result = td.ShowDialog(window);
            if (result.IsNull())
                return false;
            isVerificationChecked = td.IsVerificationChecked;
            return !result.IsNull() && result.ButtonType == OD.ButtonType.Yes;
        }

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon) => window.ShowYesNoDialogNew(title, main, content, icon, 200);

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width) => window.ShowYesNoDialogNew(title, main, content, icon, width, default);

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText) => window.ShowYesNoDialogNew(title, main, content,
                icon, width, footerText, default, out var isVerificationChecked);

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText, string verificationText,
            out bool isVerificationChecked) {
            isVerificationChecked = false;

            return window.ShowYesNoDialogNew(title, main, content, icon, width, footerText, verificationText,
                default(string), default(string), out isVerificationChecked);
        }

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText, string yesDescription, string noDescription) =>
            window.ShowYesNoDialogNew(title, main, content, icon, width, footerText, default(string), default(string),
                default(string), out _);

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText, string yesDescription, string noDescription,
            string verificationText, out bool isVerificationChecked) {

            isVerificationChecked = false;
            return window.ShowYesNoDialogNew(title, main, content, icon, width, footerText, yesDescription,
                noDescription, default(string), default(string), default(string),
                out isVerificationChecked);
        }

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText, string yesDescription, string noDescription,
            string additionalButtonCaption, string additionalButtonDescription) {

            return window.ShowYesNoDialogNew(title, main, content, icon, width, footerText, yesDescription,
                noDescription, additionalButtonCaption, additionalButtonDescription, default(string),
                out var isVerificationChecked);
        }

        public static bool? ShowYesNoDialogNew(this Window window, string title, string main, string content,
            OD.TaskDialogIcon icon, int width, string footerText, string yesDescription, string noDescription,
            string additionalButtonCaption, string additionalButtonDescription, string verificationText,
            out bool isVerificationChecked) {

            isVerificationChecked = false;

            using var dialog = new OD.TaskDialog {
                WindowTitle = title,
                MainInstruction = main,
                Content = content,
                Width = width,
                AllowDialogCancellation = true,
                CenterParent = true,
                ButtonStyle = OD.TaskDialogButtonStyle.CommandLinks,
                Footer = footerText,
                IsVerificationChecked = isVerificationChecked,
                VerificationText = verificationText
            };

            var hasOtherButton = !additionalButtonCaption.IsNull() && !additionalButtonDescription.IsNull();

            var yesButton = new OD.TaskDialogButton { Text = "Yes", CommandLinkNote = yesDescription };
            dialog.Buttons.Add(yesButton);

            var noButton = new OD.TaskDialogButton { Text = "No", CommandLinkNote = noDescription };
            dialog.Buttons.Add(noButton);

            var otherButton = default(OD.TaskDialogButton);
            if (hasOtherButton) {
                otherButton = new OD.TaskDialogButton { Text = additionalButtonCaption, CommandLinkNote = additionalButtonDescription };
                dialog.Buttons.Add(otherButton);
            }

            var result = dialog.ShowDialog();
            isVerificationChecked = dialog.IsVerificationChecked;
            if (result == yesButton) {
                return true;
            } else {
                if (hasOtherButton && result == otherButton) {
                    return null;
                }
            }
            return false;

        }

        public static OD.TaskDialogButton ShowCustomDialog(this Window window, string title, string main, string content,
                OD.TaskDialogIcon icon, int width, params OD.TaskDialogButton[] buttons) {
            var td = new OD.TaskDialog {
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = OD.TaskDialogButtonStyle.CommandLinks,
                WindowTitle = title,
                Width = width < 200 ? 200 : width,
                CenterParent = true
            };
            foreach (var btn in buttons) {
                td.Buttons.Add(btn);
            }
            var result = td.ShowDialog(window);
            return result;
        }

        public static void ShowOKDialog(string title, string content, OD.TaskDialogIcon icon, int width = 200) =>
            ShowOKDialog(title, title, content, icon, width);

        public static void ShowOKDialog(string title, string main, string content, OD.TaskDialogIcon icon, int width = 200) {
            var td = new OD.TaskDialog {
                Width = width,
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = OD.TaskDialogButtonStyle.Standard,
                WindowTitle = title,
                CenterParent = true
            };
            td.Buttons.Add(new OD.TaskDialogButton(OD.ButtonType.Ok));
            td.ShowDialog();
        }

        public static void ShowOKDialogWithConfirm(string title, string main, string content, OD.TaskDialogIcon icon, out bool isConfirmed, string confirmText = default) {
            isConfirmed = false;
            if (confirmText.IsNull()) {
                ShowOKDialog(title, main, content, icon);
                return;
            }
            var td = new OD.TaskDialog {
                //Width = width,
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = OD.TaskDialogButtonStyle.Standard,
                WindowTitle = title,
                CenterParent = true,
                VerificationText = confirmText
            };
            td.Buttons.Add(new OD.TaskDialogButton(OD.ButtonType.Ok));
            td.ShowDialog();
            isConfirmed = td.IsVerificationChecked;
        }

        public static string SelectFolderDialog(this Window win, string folder, string title) {
            var dlg = new OD.VistaFolderBrowserDialog {
                Description = title,
                Multiselect = false,
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = folder,
                ShowNewFolderButton = false,
                UseDescriptionForTitle = true
            };
            var result = dlg.ShowDialog(win);
            if (!result.HasValue || !result.Value) {
                return folder;
            }
            return dlg.SelectedPath;
        }

        public static string SelectFileDialog(this Window win, string folder, string title, params (string Extension, string Name)[] filters) {
            if (!SysIO.Directory.Exists(folder)) {
                throw new SysIO.DirectoryNotFoundException($"Cannot find {folder}");
            }

            var filter = default(string);
            filters.ForEach(f => {
                if (!string.IsNullOrWhiteSpace(filter)) filter += "|";
                if (f.Extension.StartsWith("*")) {
                    filter += $"{f.Name}|{f.Extension}";
                } else {
                    filter += $"{f.Name}|*{f.Extension}";
                }
            });

            var dlg = new OD.VistaOpenFileDialog {
                Title = title,
                Multiselect = false,
                InitialDirectory = folder,
                AddExtension = true,
                Filter = filter,
                CheckPathExists = true,
                CheckFileExists = true,
                RestoreDirectory = true,
            };
            var result = dlg.ShowDialog(win);
            if (!result.HasValue || !result.Value) {
                return default;
            }
            return dlg.FileName;
        }
        public static string SaveFileDialog(this Window win, string folder, string title, string initalFilename, params (string Extension, string Name)[] filters) {
            if (!SysIO.Directory.Exists(folder)) {
                throw new SysIO.DirectoryNotFoundException($"Cannot find {folder}");
            }

            var filter = default(string);
            filters.ForEach(f => {
                if (!string.IsNullOrWhiteSpace(filter)) filter += "|";
                if (f.Extension.StartsWith("*")) {
                    filter += $"{f.Name}|{f.Extension}";
                } else {
                    filter += $"{f.Name}|*{f.Extension}";
                }
            });

            //var filter = string.Join('|', filters.Select(x => $"{x.Name}|*.{x.Extension}"));
            var dlg = new OD.VistaSaveFileDialog {
                Title = title,
                InitialDirectory = folder,
                FileName = initalFilename,
                AddExtension = true,
                Filter = filter,
                CheckPathExists = true,
                CheckFileExists = false,
                CreatePrompt = false,
                OverwritePrompt = true,
                RestoreDirectory = true
            };
            var result = dlg.ShowDialog(win);
            if (!result.HasValue || !result.Value) {
                return default;
            }
            return dlg.FileName;
        }

    }
}
