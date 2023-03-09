using ApplicationFramework.Media;
using Common.Application.Primitives;
using Common.Application.Windows.Controls;
using Dsafa.WpfColorPicker;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MakeCompositeIcon {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize();
            Closing += MainWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private async void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.Actions), e.CommandToExecute, out var action)) {
                switch (action) {
                    case MainWindowView.Actions.FileSave: {
                            if (View.SelectedIcon == null)
                                return;
                            if (string.IsNullOrEmpty(View.SelectedIcon.Filename)) {
                                var lastDir = App.Current.As<App>().MySession.ApplicationSettings.GetValue("Application", "LastDirectory",
                                    App.Current.As<App>().FilesDirectory);
                                var dialog = new Ookii.Dialogs.Wpf.VistaSaveFileDialog {
                                    AddExtension = true,
                                    CheckFileExists = false,
                                    CheckPathExists = true,
                                    CreatePrompt = false,
                                    DefaultExt = ",compo",
                                    Filter = "Composite Icons|.compo",
                                    InitialDirectory = lastDir,
                                    OverwritePrompt = true,
                                    RestoreDirectory = true,
                                    Title = "Save composite icon..."
                                };
                                var result = dialog.ShowDialog(this);
                                if (result.HasValue && result.Value) {
                                    if (View.SelectedIcon != null) {
                                        await View.SelectedIcon.Save(dialog.FileName);
                                        App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDirectory",
                                            Path.GetDirectoryName(dialog.FileName));
                                        View.Icons.Add(CompositeIcon.FromFile(dialog.FileName));
                                    }
                                }
                            }
                            else
                                await View.SelectedIcon.Save();
                            break;
                        }
                    case MainWindowView.Actions.FileOpen:
                        break;
                    case MainWindowView.Actions.FileNew: {
                            var icon = CompositeIcon.Create(CompositeIconData.IconTypes.FullOverlay, Brushes.Transparent,
                                Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe Fluent Icons"), Brushes.Black,
                                '', 200, '');
                            View.SelectedIcon = icon;

                            break;
                        }
                    case MainWindowView.Actions.SelectColor: {
                            var isSurface = (bool)e.Parameters["IsSurface"];
                            var isPrimary = (bool)e.Parameters["IsPrimary"];
                            var color = default(Color);
                            if (isSurface) {
                                color = View.SelectedIcon.SurfaceBrush == null
                                    ? Colors.Transparent
                                    : View.SelectedIcon.SurfaceBrush.Color;
                            }
                            else if (isPrimary) {
                                color = View.SelectedIcon.PrimaryBrush.Color;
                            }
                            else {
                                color = View.SelectedIcon.SecondaryBrush == null
                                    ? View.SelectedIcon.PrimaryBrush.Color
                                    : View.SelectedIcon.SecondaryBrush.Color;
                            }
                            var dialog = new ColorPickerDialog(color) {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            var result = dialog.ShowDialog();
                            if (result.HasValue && result.Value) {
                                if (isPrimary)
                                    View.SelectedIcon.PrimaryBrush = new SolidColorBrush(dialog.Color);
                                else if (isSurface)
                                    View.SelectedIcon.SurfaceBrush = new SolidColorBrush(dialog.Color);
                                else
                                    View.SelectedIcon.SecondaryBrush = new SolidColorBrush(dialog.Color);
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

    }
}
