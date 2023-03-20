using ApplicationFramework.Media;
using Common.Application.Primitives;
using Common.Application.Windows.Controls;
using Dsafa.WpfColorPicker;
using Ookii.Dialogs.Wpf;
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
            App.Current.DispatcherUnhandledException += App.Current.As<App>().App_DispatcherUnhandledException;
        }

        private async void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.Actions), e.CommandToExecute, out var action)) {
                switch (action) {
                    case MainWindowView.Actions.ViewXaml: {
                            var win = new ViewCodeWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            win.View.Icon = View.SelectedIcon;
                            var result = win.ShowDialog();
                            if (!result.HasValue || !result.Value)
                                return;
                            Clipboard.SetText(win.View.Icon.GetXaml(win.View.IsUWPChecked));
                            break;
                        }
                    case MainWindowView.Actions.OpenSettings: {
                            var win = new OptionsWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            var result = win.ShowDialog();
                            break;
                        }
                    case MainWindowView.Actions.Delete: {
                            var td = new TaskDialog {
                                MainIcon = TaskDialogIcon.Shield,
                                MainInstruction = $"Delete {View.SelectedIcon.Filename}?",
                                AllowDialogCancellation = true,
                                ButtonStyle = TaskDialogButtonStyle.Standard,
                                Content = $"If you delete {View.SelectedIcon.Filename} it will no longer" +
                                    $"be available. You can also choose to recycle the file by placing it" +
                                    $"in the Recycle Bin.",
                                CenterParent = true,
                                MinimizeBox = false,
                                WindowTitle = "Delete icon..."
                            };
                            td.Buttons.Add(new TaskDialogButton("Delete"));
                            td.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
                            td.Buttons.Add(new TaskDialogButton("Recycle"));
                            var result = td.ShowDialog(this);
                            if (result.ButtonType == ButtonType.Cancel) {
                                return;
                            }
                            else if (result.Text == "Delete") {
                                File.Delete(View.SelectedIcon.FullPath); 
                            }
                            else {
                                var recycleFilename = Path.Combine(App.Current.As<App>().RecycleDirectory, 
                                    $"{Guid.NewGuid()}_{View.SelectedIcon.Filename}");
                                File.Move(View.SelectedIcon.FullPath, recycleFilename);
                            }
                            View.Icons.Remove(View.SelectedIcon);
                            View.SelectedIcon = null;
                            break;
                        }
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
                                    DefaultExt = ".compo",
                                    Filter = "Composite Icons|*.compo",
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
                    case MainWindowView.Actions.FileOpen: {
                            var lastDir = App.Current.As<App>().MySession.ApplicationSettings.GetValue("Application", "LastDirectory",
                                        App.Current.As<App>().FilesDirectory);
                            var dialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog {
                                AddExtension = false,
                                CheckFileExists = true,
                                CheckPathExists = true,
                                DefaultExt = ".compo",
                                Filter = "Composite Icons|*.compo",
                                InitialDirectory = lastDir,
                                RestoreDirectory = true,
                                Title = "Open composite icon..."
                            };
                            var result = dialog.ShowDialog(this);
                            if (result.HasValue && result.Value) {
                                var icon = View.Icons.FirstOrDefault(x => x.Filename.Equals(dialog.FileName, StringComparison.OrdinalIgnoreCase));
                                if (icon == null) {
                                    App.Current.As<App>().MySession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDirectory",
                                        Path.GetDirectoryName(dialog.FileName));
                                    icon = CompositeIcon.FromFile(dialog.FileName);
                                    View.Icons.Add(icon);
                                }
                                View.SelectedIcon = icon;
                            }
                            break;
                        }
                    case MainWindowView.Actions.FileNew: {
                            var icon = CompositeIcon.Create(CompositeIconData.IconTypes.FullOverlay, Brushes.White,
                                Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe Fluent Icons"), Brushes.Black,
                                '', 200, '', Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe Fluent Icons"),
                                Brushes.Black, 200);
                            View.SubscriptVisibility = Visibility.Collapsed;
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

        private void SecondSlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            View.UpdateClipWithSize(nameof(CompositeIcon.SecondarySize));
        }

        private void PrimarySlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            View.UpdateClipWithSize(nameof(CompositeIcon.PrimarySize));
        }
    }
}
