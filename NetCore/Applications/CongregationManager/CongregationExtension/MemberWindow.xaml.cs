using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace CongregationExtension {
    public partial class MemberWindow : Window {
        public MemberWindow() {
            InitializeComponent();
            Closing += MemberWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (MemberWindowViewModel.Actions)Enum.Parse(typeof(MemberWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case MemberWindowViewModel.Actions.CloseWindow:
                    Close();
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
    }
}
