using OzFramework.IO;
using OzFramework.Primitives;
using OzFramework.Timers;
using OzFramework.Windows;
using OzFramework.Windows.Controls;

using OzMiniDB.Builder.Helper;
using OzMiniDB.Items;

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using Universal.Common;

namespace OzMiniDB.Builder {
    public partial class DatabaseSettingsWindow : Window {


        public DatabaseSettingsWindow() {
            InitializeComponent();
            View.Initialize();

            SourceInitialized += (s, e) => {
                this.HideMinimizeAndMaximizeButtons();
            };

            View.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(DialogResult)) {
                    DialogResult = View.DialogResult;
                } else if (e.PropertyName == nameof(Database)) {
                    var selectedGroupName = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.LastGrpSelected, string.Empty);
                    if (!selectedGroupName.IsNull()) {
                        var selectedItem = View.Groups.FirstOrDefault(x => x.Name == selectedGroupName);
                        if (!selectedItem.IsNull()) {
                            View.SelectedGroup = selectedItem;
                        }
                    }
                } else if (e.PropertyName == nameof(View.SelectedGroup)) {
                    App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.LastGrpSelected, View.SelectedGroup.Name);
                    View.Groups.ForEach(x => x.ArrowVisibility = Visibility.Hidden);
                    if (!View.SelectedGroup.IsNull()) {
                        TimeSpan.FromMilliseconds(100).GetAutoStartStopTimer().Tick += (s, ee) => {
                            View.SelectedGroup.ArrowVisibility = Visibility.Visible;
                            if (SettingsGrid.Children.Count > 0) {
                                ClearGrid();
                            }
                            ResetGrid(View.SelectedGroup);
                        };
                    }
                }
            };

            Closing += (s, e) => {
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Left, RestoreBounds.Left);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Top, RestoreBounds.Top);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Width, RestoreBounds.Width);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.Height, RestoreBounds.Height);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.HasSettings, true);
                App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.WindowState, WindowState);
            };

            SourceInitialized += (s, e) => {
                Left = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Left, RestoreBounds.Left);
                Top = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Top, RestoreBounds.Top);
                Width = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Width, RestoreBounds.Width);
                Height = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.Height, RestoreBounds.Height);
                WindowState = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.WindowState, WindowState);
            };

        }

        public DatabaseSettingsWindowView View => DataContext.As<DatabaseSettingsWindowView>();

        private void ClearGrid() {
            SettingsGrid.Children.Clear();
            SettingsGrid.RowDefinitions.Clear();
        }

        private void ResetGrid(SettingGroup group) {
            var headerStyle = App.FindResource<Style>(App.Current.Resources, App.Constants.StdHeaderStyleName);
            var text = group.Name;
            var headerTb = new TextBlock {
                Style = headerStyle,
                Text = text
            };
            SettingsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            headerTb.SetValue(Grid.RowProperty, SettingsGrid.RowDefinitions.Count - 1);
            SettingsGrid.Children.Add(headerTb);

            group.Values.ForEach(value => {
                var grid = GetGridForValue(value);
                SettingsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
                grid.SetValue(Grid.RowProperty, SettingsGrid.RowDefinitions.Count - 1);
                SettingsGrid.Children.Add(grid);
            });
        }

        private Thickness GetMargin() => GetMargin(10, 0, 0, 0);
        private Thickness GetMargin(double left, double top, double right, double bottom) => new Thickness(left, top, right, bottom);

        private Label GetLabel(string value) {
            var label = new Label {
                Content = value,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = GetMargin()
            };
            return label;
        }

        private TextBox GetTextBox(SettingValue settingValue, double rightOffSet = 0.0) {
            var style = App.FindResource<Style>(App.Current.Resources, App.Constants.StdTextBoxStyleName);
            var result = new TextBox {
                Style = style,
                Margin = GetMargin(10, 0, rightOffSet, 0),
                ToolTip = settingValue.Description,
                Tag = settingValue,
                VerticalAlignment = VerticalAlignment.Center,
                Text = settingValue.Value.Is<FileInfo>() ? settingValue.Value.As<FileInfo>().FullName : (string)settingValue.Value
            };
            result.TextChanged += (s, e) => {
                var tb = s.As<TextBox>();
                if (tb.Tag.As<SettingValue>().Value.Is<FileInfo>()) {
                    tb.Tag.As<SettingValue>().Value = new FileInfo(tb.Text);
                } else {
                    tb.Tag.As<SettingValue>().Value = tb.Text;
                }
            };
            return result;
        }

        private Button GetSelectFileButton(SettingValue settingValue, TextBox connectedTextBox) {
            var tbx = new TextBlock {
                Text = ''.ToString(),
                FontFamily = IconFF,
                FontSize = ButtonIconSize
            };
            var btn = new Button {
                Content = tbx,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 20
            };
            btn.Click += (s, e) => {
                var values = ((FileInfo)settingValue.Value).GetExtendedFileProperties();
                var ext = (string)values.FirstOrDefault(x => x.Name == "System.FileExtension").Value;
                var itemType = (string)values.FirstOrDefault(x => x.Name == "System.ItemTypeText").Value;
                var extFiles = (Extension: ext, Name: itemType);
                var result = Dialogs.SaveFileDialog(this, settingValue.Value.As<FileInfo>().Directory.FullName,
                    $"Select {settingValue.Name}", ((FileInfo)settingValue.Value).FullName, extFiles);
                if (!result.IsNull()) {
                    connectedTextBox.Text = result;
                    settingValue.Value = new FileInfo(result);
                }
            };
            return btn;
        }

        private CheckBox GetCheckBox(SettingValue settingValue) {
            var style = App.FindResource<Style>(App.Current.Resources, App.Constants.StdCheckBoxStyleName);
            var result = new CheckBox {
                Style = style,
                Content = settingValue.Name,
                Margin = new Thickness(10, 10, 0, 0),
                ToolTip = settingValue.Description,
                Tag = settingValue,
                IsChecked = (bool)settingValue.Value
            };
            result.Checked += (s, e) => {
                var cb = s.As<CheckBox>();
                cb.Tag.As<SettingValue>().Value = cb.IsChecked;
            };
            result.Unchecked += (s, e) => {
                var cb = s.As<CheckBox>();
                cb.Tag.As<SettingValue>().Value = cb.IsChecked;
            };

            return result;
        }

        private Grid GetHolderGrid(SettingValue settingValue) {
            var result = new Grid();

            if (settingValue.Value.GetType() == typeof(SysIO.FileInfo)) {
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
            } else if (settingValue.Value.GetType() == typeof(string)) {
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            } else if (settingValue.Value.GetType() == typeof(bool)) {
                result.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            return result;
        }

        private Grid GetGridForValue(SettingValue settingValue) {
            var result = GetHolderGrid(settingValue);
            result.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            var rowNumber = result.RowDefinitions.Count - 1;
            if (settingValue.Value.GetType() == typeof(SysIO.FileInfo)) {
                var label = GetLabel(settingValue.Name.As<string>());
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(label);

                var tb = GetTextBox(settingValue);
                tb.SetValue(Grid.ColumnProperty, 1);
                tb.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(tb);

                var btn = GetSelectFileButton(settingValue, tb);
                btn.SetValue(Grid.ColumnProperty, 2);
                btn.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(btn);
            } else if (settingValue.Value.GetType() == typeof(string)) {
                var label = GetLabel(settingValue.Value.As<string>());
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(label);

                var tb = GetTextBox(settingValue);
                tb.SetValue(Grid.ColumnProperty, 1);
                tb.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(tb);
            } else if (settingValue.Value.GetType() == typeof(bool)) {
                var cb = GetCheckBox(settingValue);
                cb.SetValue(Grid.ColumnProperty, 0);
                cb.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(cb);
            }
            return result;
        }

        private FontFamily IconFF => App.FindResource<FontFamily>(App.Current.Resources, App.Constants.IconFontFamilyName);
        private double ButtonIconSize => App.FindResource<double>(App.Current.Resources, App.Constants.IconFontSizeName);

        private Binding GetElementNameBinding(DependencyProperty property, string elementName = default) =>
            new(elementName.IsNull() ? property.Name : elementName) {
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ElementName = elementName
            };

        private void SetElementBinding(FrameworkElement destination, DependencyProperty property, string elementName = default) {
            destination.SetBinding(property, GetElementNameBinding(property, elementName));
        }

    }
}
