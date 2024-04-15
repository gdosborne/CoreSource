using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace InnoData {
    public class ColorDefinitions : IDictionary<string, SolidColorBrush> {
        public ColorDefinitions() {
            SolidColorBrushs = [];
        }

        private readonly Dictionary<string, SolidColorBrush> SolidColorBrushs = default;
        public SolidColorBrush this[string key] { get => SolidColorBrushs[key]; set => SolidColorBrushs[key] = value; }
        public ICollection<string> Keys => SolidColorBrushs.Keys;
        public ICollection<SolidColorBrush> Values => SolidColorBrushs.Values;
        public int Count => SolidColorBrushs.Count;
        public bool IsReadOnly => false;
        public void Add(string key, SolidColorBrush value) => SolidColorBrushs.Add(key, value);
        public void Add(KeyValuePair<string, SolidColorBrush> item) => SolidColorBrushs.Add(item.Key, item.Value);
        public void Clear() => SolidColorBrushs.Clear();
        public bool Contains(KeyValuePair<string, SolidColorBrush> item) => SolidColorBrushs.Contains(item);
        public bool ContainsKey(string key) => SolidColorBrushs.ContainsKey(key);
        public void CopyTo(KeyValuePair<string, SolidColorBrush>[] array, int arrayIndex) {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfLessThan(arrayIndex, array.Length);
            SolidColorBrushs.ToArray().CopyTo(array, arrayIndex);
        }
        public IEnumerator<KeyValuePair<string, SolidColorBrush>> GetEnumerator() => SolidColorBrushs.GetEnumerator();
        public bool Remove(string key) => SolidColorBrushs.Remove(key);
        public bool Remove(KeyValuePair<string, SolidColorBrush> item) => SolidColorBrushs.Remove((string)item.Key);
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out SolidColorBrush value) => SolidColorBrushs.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => SolidColorBrushs.GetEnumerator();
    }
}
