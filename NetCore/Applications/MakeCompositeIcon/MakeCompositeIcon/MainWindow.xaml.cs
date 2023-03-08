using Common.Application.Primitives;
using Common.Application.Windows.Controls;
using Dsafa.WpfColorPicker;
using System;
using System.Windows;
using System.Windows.Media;

namespace MakeCompositeIcon {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize();
            Closing += MainWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.Actions), e.CommandToExecute, out var action)) {
                switch (action) {
                    case MainWindowView.Actions.FileOpen:
                        break;
                    case MainWindowView.Actions.FileNew:
                        break;
                    case MainWindowView.Actions.SelectColor: {
                            var isSurface = (bool)e.Parameters["IsSurface"];
                            var isPrimary = (bool)e.Parameters["IsPrimary"];
                            var color = isSurface ? View.SurfaceBrush.Color : isPrimary ? View.PrimaryBrush.Color : Colors.Black;
                            var dialog = new ColorPickerDialog(color);
                            dialog.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                            dialog.Owner = this;
                            var result = dialog.ShowDialog();
                            if (result.HasValue && result.Value) {
                                if (isPrimary)
                                    View.PrimaryBrush = new SolidColorBrush(dialog.Color);
                                else if (isSurface)
                                    View.SurfaceBrush = new SolidColorBrush(dialog.Color);

                            }
                            break;
                        }
                }
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Left", RestoreBounds.Left);
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Top", RestoreBounds.Top);
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Width", RestoreBounds.Width);
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Height", RestoreBounds.Height);
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "WindowState", WindowState);
            App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "ListColumnWidth", ListColumn.ActualWidth);

        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            MainToolbar.RemoveOverflow();

            Left = App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Left", double.IsInfinity(Left) ? 0 : Left);
            Top = App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Top", double.IsInfinity(Top) ? 0 : Top);
            Width = App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Width", Width);
            Height = App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Height", Height);
            WindowState = App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "WindowState", WindowState);
            ListColumn.Width = new GridLength(App.Current.As<App>().MySession.ApplicationSettings.GetValue(nameof(MainWindow), "ListColumnWidth", 250.0));
        }

        internal MainWindowView View => DataContext.As<MainWindowView>();
    }
}
