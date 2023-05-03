namespace Counties.MVVM {
	using System.Collections;
	using System.Collections.Generic;

	public class UiActionParameters : IDictionary<string, object> {
		public UiActionParameters(string commandToExecute) {
			this.CommandToExecute = commandToExecute;
			this.data = new Dictionary<string, object>();
		}

		public string CommandToExecute { get; private set; } = default(string);

		private readonly IDictionary<string, object> data = default(IDictionary<string, object>);
		public bool ContainsKey(string key) => this.data.ContainsKey(key);
		public void Add(string key, object value) {
			if (this.IsReadOnly) {
				return;
			}

			this.data.Add(key, value);
		}
		public bool Remove(string key) {
			if (this.IsReadOnly) {
				return false;
			}

			return this.data.Remove(key);
		}
		public bool TryGetValue(string key, out object value) => this.data.TryGetValue(key, out value);

		public object this[string key] {
			get => this.data[key];
			set {
				if (this.IsReadOnly) {
					return;
				}

				this.data[key] = value;
			}
		}

		public ICollection<string> Keys => this.data.Keys;
		public ICollection<object> Values => this.data.Values;

		public void Add(KeyValuePair<string, object> item) {
			if (this.IsReadOnly) {
				return;
			}

			this.data.Add(item);
		}
		public void Clear() {
			if (this.IsReadOnly) {
				return;
			}

			this.data.Clear();
		}
		public bool Contains(KeyValuePair<string, object> item) => this.data.Contains(item);
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => this.data.CopyTo(array, arrayIndex);
		public bool Remove(KeyValuePair<string, object> item) {
			if (this.IsReadOnly) {
				return false;
			}

			return this.data.Remove(item);
		}

		public int Count => this.data.Count;
		public bool IsReadOnly => this.data.IsReadOnly;

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this.data.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.data.GetEnumerator();
	}
}
