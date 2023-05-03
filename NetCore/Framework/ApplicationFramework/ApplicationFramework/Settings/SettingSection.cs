using Common.AppFramework.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Common.AppFramework.Settings {
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
