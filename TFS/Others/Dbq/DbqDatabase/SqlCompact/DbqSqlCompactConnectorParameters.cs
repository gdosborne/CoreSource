using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.SqlCompact {
    public class DbqSqlCompactConnectorParameters : IDbqConnectorParameters {
        private IDictionary<string, string> _instance = new Dictionary<string, string>();

        public string this[string key] {
            get { return _instance[key]; }
            set { _instance[key] = value; }
        }

        public ICollection<string> Keys {
            get { return _instance.Keys; }
        }

        public ICollection<string> Values {
            get { return _instance.Values; }
        }

        public int Count {
            get { return _instance.Count; }
        }

        public bool IsReadOnly {
            get { return _instance.IsReadOnly; }
        }

        public void Add(string key, string value) {
            _instance.Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item) {
            _instance.Add(item);
        }

        public void Clear() {
            _instance.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item) {
            return _instance.Contains(item);
        }

        public bool ContainsKey(string key) {
            return _instance.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) {
            _instance.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
            return _instance.GetEnumerator();
        }

        public bool Remove(string key) {
            return _instance.Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item) {
            return _instance.Remove(item);
        }

        public bool TryGetValue(string key, out string value) {
            return _instance.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _instance.GetEnumerator();
        }
    }
}
