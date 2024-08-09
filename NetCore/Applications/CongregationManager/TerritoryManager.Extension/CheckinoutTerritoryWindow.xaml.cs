using Common.Primitives;
using Common.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public partial class CheckinoutTerritoryWindow : Window {
        public CheckinoutTerritoryWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            View.ExecuteUiAction += View_ExecuteUiAction;
            Closing += CheckoutTerritoryWindow_Closing;
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        private void CheckoutTerritoryWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            this.SaveBounds(View.AppSettings);
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.AcceptData: {
                        DialogResult = true;
                        break;
                    }
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                default:
                    break;
            }
        }

        public CheckinoutTerritoryWindowViewModel View => DataContext.As<CheckinoutTerritoryWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
