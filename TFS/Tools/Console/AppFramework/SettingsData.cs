namespace GregOsborne.Application {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Reflection;
	using GregOsborne.Application.Primitives;

	public sealed class ObservableKeyValuePair : INotifyPropertyChanged {
		private object value;

		public ObservableKeyValuePair(string name) {
			this.Name = name;
			this.Settings = new List<SettingsData>();
		}

		public ObservableKeyValuePair(string name, object value)
			: this(name) => this.Value = value;

		public string Name { get; }
		public List<SettingsData> Settings { get; }

		public object Value {
			get => this.value;
			set {
				this.value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public sealed class SettingsData : INotifyPropertyChanged {
		public SettingsData(string name)
			: this() => this.Name = name;//var keys = Settings.GetKeys(name);

		private SettingsData() {
			this.Items = new ObservableCollection<ObservableKeyValuePair>();
			this.Items.CollectionChanged += this.Items_CollectionChanged;
		}

		public ObservableCollection<ObservableKeyValuePair> Items { get; }
		public string Name { get; }
		public event PropertyChangedEventHandler PropertyChanged;

		private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => ItemAdded?.Invoke(this, EventArgs.Empty);

		public event EventHandler ItemAdded;

		public void AddItem(string name, object value) {
			var kvp = new ObservableKeyValuePair(name, value);
			kvp.PropertyChanged += this.Kvp_PropertyChanged;
			this.Items.Add(kvp);
		}

		private void Kvp_PropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
	}
}