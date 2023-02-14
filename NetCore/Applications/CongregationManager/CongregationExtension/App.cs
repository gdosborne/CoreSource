﻿using Common.Applicationn;
using Common.Applicationn.Linq;
using Common.Applicationn.Logging;
using CongregationManager;
using CongregationManager.Data;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using static Common.Applicationn.Logging.ApplicationLogger;

namespace CongregationExtension {
    internal class App {
        public static void Main(string[] args) { }

        internal static ApplicationLogger logger { get; set; } = default;
        internal static Settings AppSettings { get; set; } = default;
        internal static DataManager DataManager { get; set; } = default;

        internal static void LogMessage(string message, EntryTypes type) {
            logger.LogMessage(new StringBuilder(message), type);
        }

        internal static IEnumerable<Member> MembersInOtherGroups(Congregation congregation, Group currentGroup) {
            var otherGroups = congregation.Groups
                .Where(x => x.ID != currentGroup.ID).ToList();
            var idsInOtherGroups = otherGroups
                .SelectMany(x => x.MemberIDs);
            var membershipInOtherGroups = idsInOtherGroups.Intersect(currentGroup.MemberIDs);
            return congregation.Members.Where(x => membershipInOtherGroups.Contains(x.ID));
        }

        internal static bool IsYesInDialogSelected(string main, string content, string title, TaskDialogIcon icon, int? width = null) {
            var td = new TaskDialog {
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title
            };
            if (width.HasValue)
                td.Width = width.Value;
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

        internal static void ShowRecycleBin() {
            var items = DataManager.RecycleBinItems();
            var win = new RecycleBinWindow();
            items.ToList().ForEach(x => win.View.AddGroup(x));
            var result = win.ShowDialog();
        }

        internal static Group AddEditGroup(Congregation congregation, Group group) {
            var win = new GroupWindow();
            win.View.Congregation = congregation;
            win.View.Group = group;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value) {
                return null;
            }

            try {
                group = win.View.Group;
                DataManager.SaveCongregation(congregation);
            }
            catch (Exception ex) {
                logger.LogMessage(ex.Message, EntryTypes.Error);
                return null;
            }
            return group;
        }

        internal static void AddCongregation() {
            var win = new CongregationWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            win.View.Congregation = new Congregation {
                IsNew = true,
                MeetingDay = System.DayOfWeek.Sunday,
                MeetingTime = new System.TimeSpan(10, 0, 0)
            };
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;
            try {
                DataManager.SaveCongregation(win.View.Congregation);
            }
            catch (Exception ex) {
                logger.LogMessage(ex.Message, EntryTypes.Error);
            }
        }

        internal static bool DeleteCongregation(Congregation cong) {
            var msg = $"You are about to delete the {cong.Name} Congregation. Doing so will " +
                $"remove it from this and all other extensions that are  using it.\n\nAre you " +
                $"sure want to delete the {cong.Name} Congregation?";
            var result = false;
            try {
                if (IsYesInDialogSelected("Delete Congregation", msg, "Delete Congregation", TaskDialogIcon.Shield, 300)) {
                    result = DataManager.DeleteCongregation(cong);                    
                }
            }
            catch (Exception ex) {
                logger.LogMessage(ex.Message, EntryTypes.Error);
                result = false;
            }
            return result;
        }

        internal static bool DeleteMember(Member member, Congregation congregation) {
            if (member == null || congregation == null)
                return false;
            var mainAndTitle = "Delete Member";
            var msg = $"You are about to delete {member.FullName}. Doing so will make any data " +
                $"attached to this member invalid.\n\nAre you sure you want to delete {member.FullName}?";

            if (IsYesInDialogSelected(mainAndTitle, msg, mainAndTitle, TaskDialogIcon.Shield)) {
                App.logger.LogMessage($"Deleting {member.FullName} from {congregation.Name}",
                    EntryTypes.Information);
                congregation.Members.Remove(member);
                try {
                    DataManager.SaveCongregation(congregation);
                }
                catch (Exception ex) {
                    logger.LogMessage(ex.Message, EntryTypes.Error);
                    return false;
                }
                return true;
            }
            return false;
        }

        internal static bool MoveMember(Member member, Congregation congregation, List<Congregation> otherCongregations) {
            if (member == null || congregation == null)
                return false;

            var win = new MemberMoverWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            win.View.Congregations.AddRange(otherCongregations);
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return false;

            var msg = default(string);
            var mainAndTitle = "Move Member";
            msg = $"You are about to move {member.FullName} to the {win.View.SelectedCongregation.Name} " +
                $"Congregation. Only the member information itself is moved. You or the new congregation manager " +
                $"will be responsible to reestablish the member priveleges.\n\nAre you sure you want to move " +
                $"{member.FullName}?";

            if (IsYesInDialogSelected(mainAndTitle, msg, mainAndTitle, TaskDialogIcon.Shield)) {

                App.logger.LogMessage($"Moving {member.FullName} from {congregation.Name} to {win.View.SelectedCongregation.Name}",
                    EntryTypes.Information);
                congregation.Members.Remove(member);
                member.ID = !win.View.SelectedCongregation.Members.Any()
                    ? 1 : win.View.SelectedCongregation.Members.Max(x => x.ID) + 1;
                win.View.SelectedCongregation.Members.Add(member);

                try {
                    DataManager.SaveCongregation(congregation);
                    DataManager.SaveCongregation(win.View.SelectedCongregation);
                }
                catch (Exception ex) {
                    logger.LogMessage(ex.Message, EntryTypes.Error);
                    return false;
                }
                return true;
            }
            return false;
        }

        internal static Member AddNewMember(Congregation congregation) {
            var newId = congregation.Members.Count() == 0
                    ? 1 : congregation.Members.Max(x => x.ID) + 1;
            var win = new MemberWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            var mbr = new Member { IsNew = true };
            win.View.Member = mbr;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value || win.View.Member == null)
                return null;

            mbr.Resources = App.DataManager.Resources;
            if (mbr.IsNew) {
                mbr.ID = newId;
            }
            congregation.Members.Add(mbr);
            //var sorted = congregation.Members.OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName).ToList();
            //if (sorted != null && sorted.Any())
            //    congregation.Members = sorted;

            try {
                DataManager.SaveCongregation(congregation);
                App.LogMessage($"New member ({mbr.FirstName} {mbr.LastName}) added" +
                    $" to {congregation.Name}",
                    ApplicationLogger.EntryTypes.Information);
            }
            catch (Exception ex) {
                logger.LogMessage(ex.Message, EntryTypes.Error);
                return null;
            }
            return mbr;
        }

        internal static void EditMember(Member member, Congregation congregation) {
            if (member == null || congregation == null)
                return;

            var win = new MemberWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };
            win.View.Member = member;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value || win.View.Member == null)
                return;

            member.Resources = App.DataManager.Resources;
            try {
                DataManager.SaveCongregation(congregation);
                App.LogMessage($"New member ({member.FullName}) added" +
                    $" to {congregation.Name}",
                    ApplicationLogger.EntryTypes.Information);
            }
            catch (Exception ex) {
                logger.LogMessage(ex.Message, EntryTypes.Error);
            }
        }
    }
}
