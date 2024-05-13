/* File="SettingsData"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace OzFramework {
    public sealed class ObservableKeyValuePair : INotifyPropertyChanged {
        private object value;

        public ObservableKeyValuePair(string name) {
            Name = name;
            Settings = new List<SettingsData>();
        }

        public ObservableKeyValuePair(string name, object value)
            : this(name) => Value = value;

        public string Name { get; }
        public List<SettingsData> Settings { get; }

        public object Value {
            get => value;
            set {
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public sealed class SettingsData : INotifyPropertyChanged {
        public SettingsData(string name)
            : this() => Name = name;//var keys = Settings.GetKeys(name);

        private SettingsData() {
            Items = new ObservableCollection<ObservableKeyValuePair>();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        public ObservableCollection<ObservableKeyValuePair> Items { get; }
        public string Name { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => ItemAdded?.Invoke(this, EventArgs.Empty);

        public event EventHandler ItemAdded;

        public void AddItem(string name, object value) {
            var kvp = new ObservableKeyValuePair(name, value);
            kvp.PropertyChanged += Kvp_PropertyChanged;
            Items.Add(kvp);
        }

        private void Kvp_PropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    }
}
