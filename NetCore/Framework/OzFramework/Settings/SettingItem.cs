/* File="SettingItem"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace OzFramework.Settings {
    public class SettingItem : INotifyPropertyChanged {
        public SettingItem(SettingSection section, string name, Type dataType, object value) {
            Section = section;
            Name = name;
            DataType = dataType;
            Value = value;
            Control = GetControl();
        }

        private FrameworkElement GetControl() {
            if (DataType == typeof(bool)) {
                var cb = new CheckBox();
                cb.Checked += Cb_Checked;
                cb.Unchecked += Cb_Unchecked;
                return cb;
            }
            else if (DataType == typeof(DirectoryInfo)) {
                var g = new Grid();
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
                var tb = new TextBox();
                tb.VerticalAlignment = VerticalAlignment.Center;
                var btn = new Button();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.FontFamily = new System.Windows.Media.FontFamily("Segoe Fluent Icons");
                btn.Content = "";
                btn.FontSize = 13;
                tb.SetValue(Grid.ColumnProperty, 0);
                btn.SetValue(Grid.ColumnProperty, 1);
                tb.TextChanged += Tb_TextChanged;
                g.Children.Add(tb);
                g.Children.Add(btn);
                return g;
            }
            return default;
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e) =>
            Value = new DirectoryInfo(sender.As<TextBox>().Text);

        private void Cb_Unchecked(object sender, RoutedEventArgs e) =>
            Value = false;

        private void Cb_Checked(object sender, RoutedEventArgs e) =>
            Value = true;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public SettingSection Section { get; private set; }
        public string Name { get; private set; }
        public Type DataType { get; private set; }
        public FrameworkElement Control { get; private set; }

        #region Value Property
        private object _Value = default;
        public object Value {
            get => _Value;
            set {
                _Value = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
