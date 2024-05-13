/* File="SettingSection"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OzFramework.Settings {
    public class SettingSection : INotifyPropertyChanged {
        public SettingSection(string name) {
            Name = name;
            Items = new List<SettingItem>();
        }
        public string Name { get; private set; }
        public List<SettingItem> Items { get; private set; }

        public void AddItem(string name, Type dataType, object value) {
            var item = new SettingItem(this, name, dataType, value);
            item.PropertyChanged += Item_PropertyChanged;
            Items.Add(item);
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e) =>
            PropertyChanged?.Invoke(sender, e);

        public void AddItem(string name, Type dataType) => AddItem(name, dataType, default(object));        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
