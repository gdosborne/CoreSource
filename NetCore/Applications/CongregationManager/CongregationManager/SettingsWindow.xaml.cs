using Common.Media;
using Common.Primitives;
using Common.Windows;
using CongregationManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationManager {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();

            View.Initialize();
            this.Closing += SettingsWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (SettingsWindowViewModel.Actions)Enum.Parse(typeof(SettingsWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case SettingsWindowViewModel.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case SettingsWindowViewModel.Actions.CreateTheme: {
                        var win = new ThemeEditorWindow {
                            Owner = this
                        };
                        win.ShowDialog();
                        break;
                    }
            }
        }

        private void SettingsWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) =>
            this.SaveBounds(App.ApplicationSession.ApplicationSettings, true);

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(App.ApplicationSession.ApplicationSettings, true);
        }

        public SettingsWindowViewModel View => DataContext.As<SettingsWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) =>
            DragMove();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
            sender.As<TextBox>().SelectAll();
    }
}
