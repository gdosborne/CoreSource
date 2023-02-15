using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension;
using CongregationExtension.ViewModels;
using CongregationManager.Extensibility;
using System;
using System.Linq;
using System.Windows;

namespace CongregationManager {
    public partial class CongregationWindow : Window {
        public CongregationWindow() {
            InitializeComponent();
            Closing += CongregationWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize(App.AppSettings, App.DataManager);
        }

        private void CongregationWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.GroupSelected: {
                        View.Members.ToList().ForEach(x => x.IsEnabled = View.SelectedGroup != null);
                        break;
                    }
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case LocalBase.Actions.AcceptData:
                default: {
                        if (View.SelectedGroup != null) {
                            var memberIDs = View.Congregation.Members
                                .Where(x => x.IsSelected)
                                .Select(x => x.ID)
                                .ToList();
                            View.SelectedGroup.MemberIDs = memberIDs;
                            var otherGroupMembers = App.MembersInOtherGroups(View.Congregation, View.SelectedGroup);
                            if (otherGroupMembers.Any()) {
                                foreach (var member in otherGroupMembers) {
                                    var group = View.Congregation.Groups.FirstOrDefault(x => x.ID != View.SelectedGroup.ID && x.MemberIDs.Contains(member.ID));
                                    if (group == null)
                                        continue;
                                    var msg = $"{member.FullName} is already in {group.Name}.\n\nMove {member.FullName} " +
                                        $"to {View.SelectedGroup.Name}?";
                                    if (App.IsYesInDialogSelected("Switch Groups", msg, "Switch Groups",
                                            Ookii.Dialogs.Wpf.TaskDialogIcon.Shield)) {
                                        group.MemberIDs.Remove(member.ID);
                                    }
                                    else {
                                        View.SelectedGroup.MemberIDs.Remove(member.ID);
                                    }
                                }
                            }
                        }
                        View.DataManager.SaveCongregation(View.Congregation);
                        DialogResult = true;
                        break;
                    }
            }
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        public CongregationWindowViewModel View => DataContext.As<CongregationWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        private void MembersListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (View.SelectedMember != null) {
                var win = new MemberWindow();
                win.View.Member = View.SelectedMember;
                var result = win.ShowDialog();
                if (!result.HasValue || !result.Value)
                    return;
                App.DataManager.SaveCongregation(View.Congregation);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            App.AddEditGroup(View.Congregation, View.SelectedGroup);
        }
    }
}
