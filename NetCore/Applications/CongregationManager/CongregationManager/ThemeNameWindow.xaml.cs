using Common.Primitives;
using CongregationManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace CongregationManager {
    /// <summary>
    /// Interaction logic for ThemeNameWindow.xaml
    /// </summary>
    public partial class ThemeNameWindow : Window {
        public ThemeNameWindow() {
            InitializeComponent();

            View.Initialize();
            View.ExecuteUiAction += View_ExecuteUiAction;

        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (ThemeNameWindowViewModel.Actions)Enum.Parse(typeof(ThemeNameWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case ThemeNameWindowViewModel.Actions.OK:
                    DialogResult = true;
                    break;
                case ThemeNameWindowViewModel.Actions.Cancel:
                    DialogResult = false;
                    break;
            }
        }

        public ThemeNameWindowViewModel View => DataContext.As<ThemeNameWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

    }
}
