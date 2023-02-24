using Common.Application.Media;
using Common.Application.Primitives;
using Common.Application.Windows;
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
                case SettingsWindowViewModel.Actions.ChooseColor: {
                        var color = e.Parameters["Color"].As<string>();
                        var cd = new System.Windows.Forms.ColorDialog {
                            AllowFullOpen = true,
                            AnyColor = true,
                            FullOpen = true,
                            SolidColorOnly = true,
                            Color = color.ToColor().ToColor()
                        };
                        var result = cd.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.Cancel) {
                            e.Parameters["Color"] = null;
                            return;
                        }
                        e.Parameters["Color"] = cd.Color.ToColor().ToHexValue();
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
