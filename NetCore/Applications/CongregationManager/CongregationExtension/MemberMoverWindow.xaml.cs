using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Windows;

namespace CongregationExtension {
    public partial class MemberMoverWindow : Window {
        public MemberMoverWindow() {
            InitializeComponent();

            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case LocalBase.Actions.AcceptData: {
                        DialogResult = true;
                        break;
                    }
            }
        }

        public MemberMoverWindowViewModel View => DataContext.As<MemberMoverWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }
    }
}