/* File="UiActionParameters"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Collections;
using System.Collections.Generic;

namespace Common.MVVMFramework {
    public class UiActionParameters : IDictionary<string, object> {
        public UiActionParameters(string commandToExecute) {
            CommandToExecute = commandToExecute;
            data = new Dictionary<string, object>();
        }

        public string CommandToExecute { get; private set; } = default;

        private readonly IDictionary<string, object> data = default;
        public bool ContainsKey(string key) => data.ContainsKey(key);
        public void Add(string key, object value) {
            if (IsReadOnly) {
                return;
            }

            data.Add(key, value);
        }
        public bool Remove(string key) {
            if (IsReadOnly) {
                return false;
            }

            return data.Remove(key);
        }
        public bool TryGetValue(string key, out object value) => data.TryGetValue(key, out value);

        public object this[string key] {
            get => data[key];
            set {
                if (IsReadOnly) {
                    return;
                }

                data[key] = value;
            }
        }

        public ICollection<string> Keys => data.Keys;
        public ICollection<object> Values => data.Values;

        public void Add(KeyValuePair<string, object> item) {
            if (IsReadOnly) {
                return;
            }

            data.Add(item);
        }
        public void Clear() {
            if (IsReadOnly) {
                return;
            }

            data.Clear();
        }
        public bool Contains(KeyValuePair<string, object> item) => data.Contains(item);
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => data.CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<string, object> item) {
            if (IsReadOnly) {
                return false;
            }

            return data.Remove(item);
        }

        public int Count => data.Count;
        public bool IsReadOnly => data.IsReadOnly;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();
    }
}
