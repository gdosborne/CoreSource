using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationExtension;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using System;
using System.Windows;

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
            var action = (CongregationWindowViewModel.Actions)Enum.Parse(typeof(CongregationWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case CongregationWindowViewModel.Actions.CloseWindow: {
                        Close();
                        break;
                    }
                case CongregationWindowViewModel.Actions.AcceptData:
                default: {
                        //var cong = new Congregation {
                        //    Name = View.Name,
                        //    Filename = $"{View.Name}.congregation",
                        //    Address = View.Address,
                        //    City = View.City,
                        //    StateProvence = View.StateProvence,
                        //    PostalCode = View.PostalCode,
                        //    Telephone = View.Telephone,
                        //};
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
    }
}
