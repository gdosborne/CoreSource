using Common.Application.Primitives;
using Common.Application.Windows;
using OzDBCreate.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OzDBCreate {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            Closing += SettingsWindow_Closing;

            View.Initialize();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void SettingsWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
            App.SaveWindowBounds(this, true);
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideMinimizeAndMaximizeButtons();
            if (App.RestoreWindowPositions) {
                App.RestoreWindowBounds(this, true);
            }
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {

        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("DialogResult")) {
                DialogResult = View.DialogResult;
            }
            else if (e.PropertyName.Equals("SelectedSection")) {
                SettingsGrid.Children.Clear();
                SettingsGrid.RowDefinitions.Clear();
                View.SelectedSection.Items.ForEach(item => {
                    SettingsGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                    var rowIndex = SettingsGrid.RowDefinitions.Count - 1;
                    var label = new Label() { Content = item.Name };
                    label.SetValue(Grid.RowProperty, rowIndex);
                    label.SetValue(Grid.ColumnProperty, 0);
                    item.Control.SetValue(Grid.RowProperty, rowIndex);
                    item.Control.SetValue(Grid.ColumnProperty, 1);
                    if (item.Control.Is<CheckBox>()) {
                        item.Control.As<CheckBox>().IsChecked = item.Value.CastTo<bool>();
                    }
                    SettingsGrid.Children.Add(label);
                    SettingsGrid.Children.Add(item.Control);
                });
            }
        }

        public SettingsWindowView View => DataContext.As<SettingsWindowView>();
    }
}
