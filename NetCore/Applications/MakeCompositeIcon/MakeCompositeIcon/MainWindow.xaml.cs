using Common.Media;
using Common.Linq;
using Common.Primitives;
using Common.Windows.Controls;
using Dsafa.WpfColorPicker;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Universal.Common;
using static Common.Windows.Extension;

namespace MakeCompositeIcon {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            View.Initialize();
            Closing += MainWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
            App.Current.DispatcherUnhandledException += App.ThisApp.App_DispatcherUnhandledException;
        }

        private async void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(MainWindowView.Actions), e.CommandToExecute, out var action)) {
                var act = (MainWindowView.Actions)action;
                switch (act) {
                    case MainWindowView.Actions.ShowSettingType: {
                            View.HideSettings();
                            switch ((string)e.Parameters["Type"]) {
                                case "IconType": View.IconTypeVisibility = Visibility.Visible; break;
                                case "Colors": View.ColorsVisibility = Visibility.Visible; break;
                                case "Fonts": View.FontsVisibility = Visibility.Visible; break;
                                case "FontSizes": View.SizeVisibility = Visibility.Visible; break;
                                case "Characters": View.CharactersVisibility = Visibility.Visible; break;
                            }
                            break;
                        }
                    case MainWindowView.Actions.ShowRecycleBin: {
                            var win = new RecycleBinWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };

                            var result = win.ShowDialog();
                            if (win.View.ItemsForOtherFiles != null && win.View.ItemsForOtherFiles.Any()) {
                                win.View.ItemsForOtherFiles.ForEach(async x => await View.AddOtherFileAsync(x));
                            }
                            View.RefreshFiles();
                            View.UpdateInterface();
                            break;
                        }
                    case MainWindowView.Actions.ShowCharacterWindow: {
                            this.Cursor = Cursors.Wait;
                            var win = new CharacterWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            var result = default(bool?);
                            win.View.IsPrimary = (bool)e.Parameters["IsPrimary"];
                            var chars = win.View.IsPrimary
                                ? App.ThisApp.CachedCharacters[View.SelectedIcon.PrimaryFontFamily.Source]
                                : App.ThisApp.CachedCharacters[View.SelectedIcon.SecondaryFontFamily.Source];
                            chars.ForEach(x => {
                                if (x.Index == 0 || x.Index == 13 || x.Index == 10)
                                    return;
                                x.FontSize = win.View.CharacterSize;
                                win.View.Characters.Add(x);
                            });
                            win.View.Title += $" - {chars[0].FontFamily.Source}";
                            win.View.SelectedCharacter = win.View.IsPrimary ? View.PrimaryGlyph : View.SecondaryGlyph;
                            result = win.ShowDialog();
                            this.Cursor = Cursors.Arrow;
                            if (!result.HasValue || !result.Value)
                                return;
                            if (win.View.IsPrimary) {
                                View.PrimaryGlyph = win.View.SelectedCharacter;
                            }
                            else {
                                View.SecondaryGlyph = win.View.SelectedCharacter;
                            }
                            break;
                        }
                    case MainWindowView.Actions.ViewXaml: {
                            var win = new ViewCodeWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            win.View.Icon = View.SelectedIcon;
                            var result = win.ShowDialog();
                            if (!result.HasValue || !result.Value)
                                return;
                            var outSize = App.ThisApp.MySession.ApplicationSettings.GetValue("Application",
                                "OutputIconSize", 32);
                            Clipboard.SetText(win.View.Icon.GetXaml(outSize));
                            break;
                        }
                    case MainWindowView.Actions.OpenSettings: {
                            var win = new OptionsWindow {
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                                Owner = this
                            };
                            var result = win.ShowDialog();
                            View.GuideVisibility = App.ThisApp.AreGuidesShown ? Visibility.Visible : Visibility.Hidden;
                            break;
                        }
                    case MainWindowView.Actions.Delete: {
                            var answer = default(string);

                            var isAlwaysPromptSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application",
                                "IsAlwaysPromptSelected", true);
                            var isAlwaysDeleteSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application",
                                "IsAlwaysDeleteSelected", false);
                            var isAlwaysRecycleSelected = App.ThisApp.MySession.ApplicationSettings.GetValue("Application",
                                "IsAlwaysRecycleSelected", false);

                            if (!isAlwaysPromptSelected) {
                                answer = isAlwaysDeleteSelected ? "Delete" : "Recycle";
                            }
                            else {
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
                                    WindowTitle = "Delete icon...",
                                    VerificationText = "Always use this selection"
                                };
                                td.Buttons.Add(new TaskDialogButton("Delete"));
                                td.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
                                td.Buttons.Add(new TaskDialogButton("Recycle"));
                                var result = td.ShowDialog(this);
                                if (result.ButtonType == ButtonType.Cancel) {
                                    return;
                                }
                                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                                    "IsAlwaysPromptSelected", !td.IsVerificationChecked);
                                answer = td.IsVerificationChecked ? result.Text : default(string);
                                if (td.IsVerificationChecked) {
                                    App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                                        "IsAlwaysDeleteSelected", answer == "Delete");
                                    App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                                        "IsAlwaysRecycleSelected", answer == "Recycle");
                                }
                            }

                            if (!Path.GetDirectoryName(View.SelectedIcon.FullPath)
                                .Equals(App.ThisApp.FilesDirectory, StringComparison.OrdinalIgnoreCase)) {
                                await View.RemoveOtherIconAsync(View.SelectedIcon);
                            }
                            if (answer == "Delete") {
                                File.Delete(View.SelectedIcon.FullPath);
                            }
                            else {
                                var recycleFilename = Path.Combine(App.ThisApp.RecycleDirectory,
                                    $"{Guid.NewGuid()}.compo");
                                File.Move(View.SelectedIcon.FullPath, recycleFilename);
                            }

                            View.Icons.Remove(View.SelectedIcon);
                            View.PrimaryCharacters.Clear();
                            View.SecondaryCharacters.Clear();
                            View.SelectedIcon = null;
                            break;
                        }
                    case MainWindowView.Actions.FileSave:
                    case MainWindowView.Actions.FileSaveAs: {
                            if (View.SelectedIcon == null)
                                return;
                            if (act == MainWindowView.Actions.FileSaveAs ||
                                (View.SelectedIcon.IsNewIcon && act == MainWindowView.Actions.FileSave)) {
                                var lastDir = App.ThisApp.MySession.ApplicationSettings.GetValue("Application", "LastDirectory",
                                    App.ThisApp.FilesDirectory);
                                var dialog = new VistaSaveFileDialog {
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
                                        if (!Path.GetDirectoryName(dialog.FileName).Equals(App.ThisApp.FilesDirectory, StringComparison.OrdinalIgnoreCase)) {
                                            File.Move(View.SelectedIcon.FullPath, dialog.FileName);
                                            View.SelectedIcon.FullPath = dialog.FileName;
                                            await View.AddOtherFileAsync(dialog.FileName);
                                        }
                                        App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDirectory",
                                            Path.GetDirectoryName(dialog.FileName));
                                    }
                                }
                            }
                            else if (act == MainWindowView.Actions.FileSave)
                                await View.SelectedIcon.Save();
                            break;
                        }
                    case MainWindowView.Actions.FileOpen: {
                            var lastDir = App.ThisApp.MySession.ApplicationSettings.GetValue("Application", "LastDirectory",
                                        App.ThisApp.FilesDirectory);
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
                                var icon = default(CompositeIcon);
                                if (Path.GetDirectoryName(dialog.FileName).Equals(App.ThisApp.FilesDirectory,
                                    StringComparison.OrdinalIgnoreCase)) {
                                    icon = View.Icons.FirstOrDefault(x => x.Filename.Equals(dialog.FileName,
                                        StringComparison.OrdinalIgnoreCase));
                                    if (icon == null) {
                                        icon = CompositeIcon.FromFile(dialog.FileName);
                                        View.Icons.Add(icon);
                                    }
                                }
                                else {
                                    if (!View.OtherIconFiles.Any(x => x.Equals(dialog.FileName,
                                        StringComparison.OrdinalIgnoreCase))) {
                                        icon = await View.AddOtherFileAsync(dialog.FileName);
                                        if (icon != null) {
                                            View.Icons.Add(icon);
                                        }
                                    }
                                    else {
                                        icon = View.Icons.FirstOrDefault(x => x.FullPath.Equals(dialog.FileName,
                                            StringComparison.OrdinalIgnoreCase));
                                    }
                                }
                                if (icon != null)
                                    View.SelectedIcon = icon;
                                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application", "LastDirectory",
                                    Path.GetDirectoryName(dialog.FileName));
                            }
                            break;
                        }
                    case MainWindowView.Actions.FileNew: {
                            var font = Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe MDL2 Assets");
                            var result = CompositeIcon.Create(CompositeIconData.IconTypes.FullOverlay, System.Windows.Media.Brushes.White,
                                font, System.Windows.Media.Brushes.Black, "", 200, "", font, System.Windows.Media.Brushes.Black, 200);
                            result.Filename = "[Unnamed].compo";
                            result.ShortName = Path.GetFileNameWithoutExtension(result.Filename);
                            result.FullPath = result.Filename;
                            result.Filename = Path.GetFileName(result.Filename);

                            View.Icons.Add(result);
                            View.SubscriptVisibility = Visibility.Collapsed;
                            View.SelectedIcon = result;

                            break;
                        }
                    case MainWindowView.Actions.SelectColor: {
                            var isSurface = (bool)e.Parameters["IsSurface"];
                            var isPrimary = (bool)e.Parameters["IsPrimary"];
                            var color = default(System.Windows.Media.Color);
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
                    case MainWindowView.Actions.RenameIcon: {
                            var index = IconListBox.Items.IndexOf(View.SelectedIcon);
                            if (index >= 0) {
                                if (IconListBox.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                                    return;
                                var lbi = IconListBox.ItemContainerGenerator.ContainerFromIndex(index).As<ListBoxItem>();
                                renameTextBox = lbi.FindChildByName<TextBox>("TitleTextBox");
                                if (renameTextBox != null) {
                                    renameTextBox.Visibility = Visibility.Visible;
                                    renameTextBox.Focus();
                                }
                            }
                            break;
                        }
                }
            }
        }

        private TextBox renameTextBox = default;

        private void TitleTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Escape) {
                e.Handled = true;
                View.SelectedIcon.RenameValue = View.SelectedIcon.Filename;
                renameTextBox.Visibility = Visibility.Hidden;
                renameTextBox = null;
            }
            else if (e.Key == System.Windows.Input.Key.Enter) {
                e.Handled = true;
                renameTextBox.Visibility = Visibility.Hidden;
                var oldValue = View.SelectedIcon.FullPath;
                var isRenamed = View.SelectedIcon.Rename(out var reason);
                if (isRenamed) {
                    var icon = View.SelectedIcon;
                    if (!Path.GetDirectoryName(oldValue).Equals(App.ThisApp.FilesDirectory, StringComparison.OrdinalIgnoreCase)) {
                        View.RenameOtherIcon(icon, oldValue);
                    }


                    View.SelectedIcon = null;
                    var orderedList = View.Icons.OrderBy(x => x.ShortName).ToList();
                    View.Icons.Clear();
                    View.Icons.AddRange(orderedList);
                    icon.RenameValue = icon.Filename;
                    View.SelectedIcon = icon;
                    renameTextBox = null;
                    return;
                }
                var td = new TaskDialog {
                    MainIcon = TaskDialogIcon.Error,
                    MainInstruction = "Rename Error",
                    AllowDialogCancellation = true,
                    ButtonStyle = TaskDialogButtonStyle.Standard,
                    Content = $"The rename failed for the following reason:\n\n{reason}",
                    CenterParent = true,
                    MinimizeBox = false,
                    WindowTitle = "Rename error..."
                };
                td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                td.ShowDialog(this);
                View.SelectedIcon.RenameValue = View.SelectedIcon.Filename;
                renameTextBox = null;
            }
        }

        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Left", RestoreBounds.Left);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Top", RestoreBounds.Top);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Width", RestoreBounds.Width);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "Height", RestoreBounds.Height);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "WindowState", WindowState);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), "ListColumnWidth", ListColumn.ActualWidth);
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            MainToolbar.RemoveOverflow();
            ItemToolbar.RemoveOverflow();
            SettingsToolbar.RemoveOverflow();

            if (App.ThisApp.IsUseLastPositionChecked) {
                Left = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Left", double.IsInfinity(Left) ? 0 : Left);
                Top = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Top", double.IsInfinity(Top) ? 0 : Top);
                Width = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Width", Width);
                Height = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "Height", Height);
                WindowState = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "WindowState", WindowState);
                ListColumn.Width = new GridLength(App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(MainWindow), "ListColumnWidth", 250.0));
            }
        }

        internal MainWindowView View => DataContext.As<MainWindowView>();

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => sender.As<TextBox>().SelectAll();

        private void SecondSlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            View.UpdateClipWithSize(nameof(CompositeIcon.SecondarySize));

        private void PrimarySlider_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) =>
            View.UpdateClipWithSize(nameof(CompositeIcon.PrimarySize));

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            sender.As<ListBox>().ScrollIntoView(sender.As<ListBox>().SelectedItem);
        }

        private void ListBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            sender.As<ListBox>().ScrollIntoView(sender.As<ListBox>().SelectedItem);
        }

    }
}
