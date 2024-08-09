using Common.Primitives;
using Common.Windows;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using Ookii.Dialogs.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Common.Dialogs.Helpers;

namespace CongregationExtension {
    public partial class GroupWindow : Window {
        public GroupWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case LocalBase.Actions.AcceptData: {
                        var createdGroup = default(Group);
                        if (View.Group == null) {
                            createdGroup = new Group {
                                Name = View.GroupName,
                                OverseerMemberID = View.SelectedOverseer.ID,
                                AssistantMemberID = View.SelectedAssistant.ID,
                                ID = !View.Congregation.Groups.Any()
                                    ? 1 : View.Congregation.Groups.Max(x => x.ID) + 1
                            };
                            View.Congregation.Groups.Add(createdGroup);
                        }
                        else
                            createdGroup = View.Group;
                        
                        createdGroup.MemberIDs = View.Members
                            .Where(x => x.IsSelected)
                            .Select(x => x.ID)
                            .ToList();

                        var otherGroupMembers = App.MembersInOtherGroups(View.Congregation, createdGroup);
                        if (otherGroupMembers.Any()) {
                            foreach (var member in otherGroupMembers) {
                                var otherGroup = View.Congregation.Groups.FirstOrDefault(x => x.ID != createdGroup.ID && x.MemberIDs.Contains(member.ID));
                                if (otherGroup == null)
                                    continue;
                                var msg = $"{member.FullName} is already in {otherGroup.Name}.\n\nMove {member.FullName} " +
                                    $"to {createdGroup.Name}?";
                                if (Common.Dialogs.Helpers.ShowYesNoDialog(this, "Switch Groups", msg, TaskDialogIcon.Shield)) {
                                    otherGroup.MemberIDs.Remove(member.ID);
                                }
                                else {
                                    createdGroup.MemberIDs.Remove(member.ID);
                                }
                            }
                        }
                        DialogResult = true;
                        break;
                    }
            }
        }

        public GroupWindowViewModel View => DataContext.As<GroupWindowViewModel>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void GroupName_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }
    }
}
