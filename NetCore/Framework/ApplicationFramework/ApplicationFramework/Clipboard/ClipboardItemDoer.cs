using System.Collections.Generic;
using System.Linq;

namespace ApplicationFramework.Clipboard {
    public class ClipboardItemDoer<T> {
        public ClipboardItemDoer(T initialValue) {
            _items = new Dictionary<int, T> {
                { 0, initialValue }
            };
            Position = 0;
        }

        private Dictionary<int, T> _items;

        public int Count => _items.Max(x => x.Key);

        public int Position { get; set; }

        public T GetValue() => GetValue(Position);
        public T GetValue(int key) => _items[key];

        public void Push(T item) {
            _items.Add(_items.Max(x => x.Key) + 1, item);
            Position++;
        }
    }
}
