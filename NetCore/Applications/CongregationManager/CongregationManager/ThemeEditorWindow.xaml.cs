using Common.Application.IO;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.Application.Windows;
using CongregationManager.Data;
using CongregationManager.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CongregationManager {
    public partial class ThemeEditorWindow : Window {
        public ThemeEditorWindow() {
            InitializeComponent();

            View.Initialize();
            this.Closing += SettingsWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (ThemeEditorWindowViewModel.Actions)Enum.Parse(typeof(ThemeEditorWindowViewModel.Actions), e.CommandToExecute);
            switch (action) {
                case ThemeEditorWindowViewModel.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case ThemeEditorWindowViewModel.Actions.ChooseColor: {
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
                case ThemeEditorWindowViewModel.Actions.CreateTheme: {
                        var win = new ThemeNameWindow {
                            Owner = this
                        };
                        var result = win.ShowDialog();
                        if (!result.HasValue || !result.Value)
                            return;
                        var name = win.View.ThemeName;
                        var theme = new ApplicationTheme {
                            Name = name
                        };
                        View.Colors.ToList().ForEach(x => {
                            theme.Values.Add(x.Key, x.ColorValue.ToHexValue());
                        });
                        var fileName = System.IO.Path.Combine(App.ThemeFolder, $"{name}.apptheme");
                        theme.Save(fileName);
                        View.Themes.Add(theme);
                        View.SelectedTheme = theme;
                        break;
                    }
            }
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(App.ApplicationSession.ApplicationSettings, true);
        }

        private void SettingsWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) =>
            this.SaveBounds(App.ApplicationSession.ApplicationSettings, true);

        public ThemeEditorWindowViewModel View => DataContext.As<ThemeEditorWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            DragMove();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
            sender.As<TextBox>().SelectAll();
    }
}
