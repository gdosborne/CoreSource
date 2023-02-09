using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationExtension {
    public partial class GroupWindow : Window {
        public GroupWindow() {
            InitializeComponent();

            View.Initialize();
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
                        if(View.Group == null) {
                            View.Group = new CongregationManager.Data.Group {
                                Name = View.GroupName,
                                OverseerMemberID = View.SelectedOverseer.ID,
                                AssistantMemberID = View.SelectedAssistant.ID,
                                ID = !View.Congregation.Groups.Any()
                                    ? 1 : View.Congregation.Groups.Max(x => x.ID) + 1
                            };
                            View.Congregation.Groups.Add(View.Group);
                        }
                        View.Group.MemberIDs = View.Congregation.Members
                            .Where(x => x.IsSelected)
                            .Select(x => x.ID)
                            .ToList();

                        var otherGroupMembers = App.MembersInOtherGroups(View.Congregation, View.Group);
                        if (otherGroupMembers.Any()) {
                            foreach (var member in otherGroupMembers) {
                                var group = View.Congregation.Groups.FirstOrDefault(x => x.ID != View.Group.ID && x.MemberIDs.Contains(member.ID));
                                if (group == null)
                                    continue;
                                var msg = $"{member.FullName} is already in {group.Name}.\n\nMove {member.FullName} " +
                                    $"to {View.Group.Name}?";
                                if (App.IsYesInDialogSelected("Switch Groups", msg, "Switch Groups",
                                        Ookii.Dialogs.Wpf.TaskDialogIcon.Shield)) {
                                    group.MemberIDs.Remove(member.ID);
                                }
                                else {
                                    View.Group.MemberIDs.Remove(member.ID);
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
