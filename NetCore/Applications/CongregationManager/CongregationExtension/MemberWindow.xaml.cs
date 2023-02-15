using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension.ViewModels;
using CongregationManager.Extensibility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationExtension {
    public partial class MemberWindow : Window {
        public MemberWindow() {
            InitializeComponent();
            Closing += MemberWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize(App.AppSettings, App.DataManager);
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow:
                    DialogResult = false;
                    break;
                case LocalBase.Actions.AddMember:
                    DialogResult = true;
                    break;
                default:
                    break;
            }
        }

        private void MemberWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }

        public MemberWindowViewModel View => DataContext.As<MemberWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void ItemGotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }
    }
}
