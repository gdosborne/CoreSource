using Common.Applicationn;
using Common.Applicationn.Linq;
using Common.Applicationn.Logging;
using CongregationManager.Data;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Windows;
using static Common.Applicationn.Logging.ApplicationLogger;

namespace CongregationExtension {
    internal class App {
        public static void Main(string[] args) { }

        internal static ApplicationLogger logger = default;
        internal static Settings AppSettings = default;
        internal static DataManager DataManager = default;

        internal static void LogMessage(string message, EntryTypes type) {
            logger.LogMessage(new StringBuilder(message), type);
        }

        internal static bool IsYesInDialogSelected(string main, string content, string title, TaskDialogIcon icon) {
            var td = new TaskDialog {
                Width = 250,
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            return result.ButtonType == ButtonType.Yes;
        }

        internal static void OkDialog(string main, string content, string title, TaskDialogIcon icon) {
            var td = new TaskDialog {
                Width = 250,
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

        internal static bool DeleteMember(List<Member> members, Congregation congregation) {
            var msg = default(string);
            var mainAndTitle = "Delete Member";
            if (members == null || members.Count == 0)
                return false;
            if (members.Count == 1) {
                msg = $"You are about to delete {members[0].FullName}. Doing so will make any data " +
                    $"attached to this member invalid.\n\nAre you sure you want to delete {members[0].FullName}?";
            }
            else {
                msg = $"You are about to delete {members.Count} members.\n\n";
                foreach (var member in members) {
                    msg += $"    {member.FullName}\n";
                }
                msg += $"\nDoing so will make any data attached to these members invalid.\n\nAre you " +
                    $"sure you want to delete these members?";
                mainAndTitle += "s";
            }

            if (IsYesInDialogSelected(mainAndTitle, msg, mainAndTitle, TaskDialogIcon.Shield)) {               
                members.ForEach(x => {
                    App.logger.LogMessage($"Deleting {x.FullName} from {congregation.Name}",
                        EntryTypes.Information);
                    congregation.Members.Remove(x);
                });
                DataManager.SaveCongregation(congregation);
                return true;
            }
            return false;
        }

        internal static bool MoveMember(List<Member> members, Congregation congregation, List<Congregation> otherCongregations) {
            var win = new MemberMoverWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            win.View.Congregations.AddRange(otherCongregations);
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return false;

            var msg = default(string);
            var mainAndTitle = "Move Member";
            if (members == null || members.Count == 0)
                return false;
            if (members.Count == 1) {
                msg = $"You are about to move {members[0].FullName} to the {win.View.SelectedCongregation.Name} " +
                    $"Congregation. Only the member information itself is moved. You or the new congregation manager " +
                    $"will be responsible to reestablish the member priveleges.\n\nAre you sure you want to move " +
                    $"{members[0].FullName}?";
            }
            else {
                msg = $"You are about to move {members.Count} members to the {win.View.SelectedCongregation.Name} " +
                    $"Congregation.\n\n";
                foreach (var member in members) {
                    msg += $"    {member.FullName}\n";
                }
                msg += $"\nOnly the member information itself is moved. You or the new congregation manager " +
                    $"will be responsible to reestablish the member priveleges.\n\nAre you sure you want to move these " +
                    $"members?";
                mainAndTitle += "s";
            }

            if (IsYesInDialogSelected(mainAndTitle, msg, mainAndTitle, TaskDialogIcon.Shield)) {
                members.ForEach(x => {
                    App.logger.LogMessage($"Moving {x.FullName} from {congregation.Name} to {win.View.SelectedCongregation.Name}", 
                        EntryTypes.Information);
                    congregation.Members.Remove(x);
                    x.ID = !win.View.SelectedCongregation.Members.Any() 
                        ? 1 : win.View.SelectedCongregation.Members.Max(x => x.ID) + 1;
                    win.View.SelectedCongregation.Members.Add(x);
                });
                DataManager.SaveCongregation(congregation);
                DataManager.SaveCongregation(win.View.SelectedCongregation);
                return true;
            }
            return false;
        }

        internal static void AddNewMember(Congregation congregation) {
            var newId = congregation.Members.Count() == 0
                    ? 1 : congregation.Members.Max(x => x.ID) + 1;
            var win = new MemberWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            var mbr = new Member { IsNew = true };
            win.View.Member = mbr;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value || win.View.Member == null)
                return;

            mbr.Resources = App.DataManager.Resources;
            if (mbr.IsNew) {
                mbr.ID = newId;
            }
            congregation.Members.Add(mbr);
            congregation.Members = congregation.Members
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            DataManager.SaveCongregation(congregation);
            App.LogMessage($"New member ({mbr.FirstName} {mbr.LastName}) added" +
                $" to {congregation.Name}",
                ApplicationLogger.EntryTypes.Information);
        }
    }
}
