using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager {
    public partial class CongregationWindow : Window {
        public CongregationWindow() {
            InitializeComponent();
            Closing += CongregationWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();
        }

        private void CongregationWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        Close();
                        break;
                    }
                case LocalBase.Actions.AcceptData:
                default: {
                        View.DataManager.SaveCongregation(View.Congregation);
                        Close();
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
            if (View.SelectedMembers.Any()) {
                if(View.SelectedMembers.Count > 1) {
                    App.OkDialog("Single edit only", "You have multiple members selected in the list. You may " +
                        "only edit one member at a time.", "Single edit only", 
                        Ookii.Dialogs.Wpf.TaskDialogIcon.Information);
                    return;
                }
                var win = new MemberWindow();
                win.View.Member = View.SelectedMembers[0]; ;
                var result = win.ShowDialog();
                if (!result.HasValue || !result.Value)
                    return;
                App.DataManager.SaveCongregation(View.Congregation);
            }
        }

        private void MemberListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            foreach (var item in e.AddedItems) {
                View.SelectedMembers.Add(item.As<Member>());
            }
            foreach (var item in e.RemovedItems) {
                View.SelectedMembers.Remove(item.As<Member>());
            }
            e.Handled = false;
        }
    }
}
