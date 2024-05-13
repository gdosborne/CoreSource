using OzFramework.Primitives;
using OzFramework.Timers;
using OzFramework.Windows;
using OzFramework.Windows.Controls;

using OzMiniDB.Builder.Helper;
using OzMiniDB.Items;

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
                    if (View.DialogResult) {
                        App.Session.ApplicationSettings.AddOrUpdateSetting(App.Constants.Application, App.Constants.SaveWinSizeAndLoc,
                            View.Groups.First(x => x.Name == App.Constants.UserInterface)
                                .Groups.First(x => x.Name == App.Constants.General)
                                .Values.First(x => x.Name == App.Constants.SaveWinSizeAndLoc).Value);
                    }
                    DialogResult = View.DialogResult;
                } else if (e.PropertyName == nameof(Database)) {
                    var selectedPath = App.Session.ApplicationSettings.GetValue(GetType().Name, App.Constants.LastGrpSelected, string.Empty);
                    if (!selectedPath.IsNull()) {
                        var selectedItem = SelectedGroup(null, selectedPath);
                        if (!selectedItem.IsNull()) {
                            SelectTreeViewItem(selectedItem, null);
                        }
                    }
                };
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

        private void SelectTreeViewItem(SettingGroup settingGroup, TreeViewItem item) {
            if (settingGroup.IsNull()) return;
            if (!item.IsNull()) {
                settingGroup.IsSelected = true;
                //foreach (var tvItem in SettingGroupsTreeView.Items) {

                //}
            }
        }

        private SettingGroup SelectedGroup(SettingGroup parent, string path) {
            var result = default(SettingGroup);
            if (!parent.IsNull() && parent.Path.Equals(path)) return parent;
            if (parent.IsNull()) {
                foreach (var item in View.Groups) {
                    result = SelectedGroup(item, path);
                    if (!result.IsNull()) break;
                }
            }
            if (!parent.IsNull() && !parent.Groups.IsNull()) {
                foreach (var item in parent.Groups) {
                    result = SelectedGroup(item, path);
                    if (!result.IsNull()) break;
                }
            }
            return result;
        }

        public DatabaseSettingsWindowView View => DataContext.As<DatabaseSettingsWindowView>();

        private void TreeView_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var tv = sender.As<TreeView>();
            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed) {

            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            var tvi = sender.As<TreeView>()
                .ItemContainerGenerator
                .ContainerFromItemRecursive(sender.As<TreeView>().SelectedItem);

            var selected = e.NewValue.As<SettingGroup>();
            App.Session.ApplicationSettings.AddOrUpdateSetting(GetType().Name, App.Constants.LastGrpSelected, selected.Path);
            if (selected.Parent.IsNull()) {
                if (selected.Groups.Any(x => x.Name == App.Constants.General))
                    selected = selected.Groups.First(x => x.Name == App.Constants.General);
                else
                    selected = selected.Groups.First();
                tvi.IsExpanded = true;
            }
            if (!selected.IsNull()) {
                TimeSpan.FromMilliseconds(100).GetAutoStartStopTimer().Tick += (s, ee) => {
                    if (SettingsGrid.Children.Count > 0) {
                        ClearGrid();
                    }
                    ResetGrid(selected);
                };
            }
        }

        private void ClearGrid() {
            SettingsGrid.Children.Clear();
            SettingsGrid.RowDefinitions.Clear();
        }

        private void ResetGrid(SettingGroup group) {
            var headerStyle = App.FindResource<Style>(App.Current.Resources, App.Constants.StdHeaderStyleName);
            var text = group.Name;
            if (!group.Parent.IsNull()) {
                text = $"{group.Parent.Name} / {group.Name}";
            }
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
                Margin = GetMargin()
            };
            return label;
        }

        private TextBox GetTextBox(SettingValue settingValue, double rightOffSet = 0.0) {
            var style = App.FindResource<Style>(App.Current.Resources, App.Constants.StdTextBoxStyleName);
            var tb = new TextBox {
                Style = style,
                Margin = GetMargin(10, 0, rightOffSet, 5),
                ToolTip = settingValue.Description
            };
            SetBinding(tb, TextBox.TextProperty, settingValue, App.Constants.Value);
            return tb;
        }

        private Button GetSelectFileButton(SettingValue settingValue) {
            var tbx = new TextBlock {
                Text = ''.ToString(),
                FontFamily = IconFF,
                FontSize = ButtonIconSize
            };
            var btn = new Button {
                Content = tbx,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
            };
            return btn;
        }

        private CheckBox GetChackBox(SettingValue settingValue) {
            var style = App.FindResource<Style>(App.Current.Resources, App.Constants.StdCheckBoxStyleName);
            var result = new CheckBox {
                Style = style,
                Content = settingValue.Name,
                Margin = new Thickness(10, 10, 0, 0),
                ToolTip = settingValue.Description
            };
            SetBinding(result, CheckBox.IsCheckedProperty, settingValue, App.Constants.Value);
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

                var btn = GetSelectFileButton(settingValue);
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
                var cb = GetChackBox(settingValue);
                cb.SetValue(Grid.ColumnProperty, 0);
                cb.SetValue(Grid.RowProperty, rowNumber);
                result.Children.Add(cb);
            }
            return result;
        }

        private FontFamily IconFF => App.FindResource<FontFamily>(App.Current.Resources, App.Constants.IconFontFamilyName);
        private double ButtonIconSize => App.FindResource<double>(App.Current.Resources, App.Constants.IconFontSizeName);

        private Binding GetBinding(DependencyProperty property, object source, string propName = default) =>
            new(propName.IsNull() ? property.Name : propName) {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = source
            };
        private Binding GetElementNameBinding(DependencyProperty property, string elementName = default) =>
            new(elementName.IsNull() ? property.Name : elementName) {
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ElementName = elementName
            };

        private void SetBinding(FrameworkElement destination, DependencyProperty property, object source, string propName = default) {
            destination.SetBinding(property, GetBinding(property, source, propName));
        }

        private void SetElementBinding(FrameworkElement destination, DependencyProperty property, string elementName = default) {
            destination.SetBinding(property, GetElementNameBinding(property, elementName));
        }

    }
}
