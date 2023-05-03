using Common.AppFramework.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Common.AppFramework.RecentItems {
    /// <summary>
    /// The recent manager.
    /// </summary>
    public class RecentManager {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentManager"/> class.
        /// </summary>
        /// <param name="maxItems">The max items.</param>
        /// <param name="directory">The storage directory</param>
        /// <param name="command">The common command</param>
        public RecentManager(byte maxItems, string directory, ICommand command) {
            MaxItems = maxItems;
            storageDirectory = directory;
            storageFileName = ".recent";
            this.command = command;
            Items = new ObservableCollection<RecentItem>();

            LoadMostRecentItems();
        }

        private ICommand command = default;
        private string storageDirectory = default;
        private readonly string storageFileName = default;
        private bool initialLoad = true;

        /// <summary>
        /// A refresh is required
        /// </summary>
        public event EventHandler RefreshRequired;

        /// <summary>
        /// Loads the most recent items.
        /// </summary>
        private void LoadMostRecentItems() {
            if (!Directory.Exists(storageDirectory)) {
                throw new DirectoryNotFoundException($"Storage directory ({storageDirectory}) not found.");
            }
            var fName = Path.Combine(storageDirectory, storageFileName);
            if (!File.Exists(fName)) {
                using var fsc = new FileStream(fName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fsc);
                sw.WriteLine($"#Most recent file list");
            }
            var temp = new Dictionary<int, string>();
            using var fs = new FileStream(fName, FileMode.Open, FileAccess.Read, FileShare.None);
            using var sr = new StreamReader(fs);
            while (!sr.EndOfStream) {
                var line = sr.ReadLine();
                if (line.StartsWith("#")) {
                    continue;
                }
                var parts = line.Split(',');
                if (parts.Length == 2) {
                    var num = parts[0];
                    var path = parts[1];
                    temp.Add(int.Parse(num), path);
                }
            }
            if (temp.Any()) {
                temp.OrderBy(x => x.Key)
                    .ToList()
                    .ForEach(x => Add(x.Key, x.Value));
            }
            initialLoad = false;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public ObservableCollection<RecentItem> Items { get; private set; }

        /// <summary>
        /// Gets the max items.
        /// </summary>
        public byte MaxItems { get; private set; } = 5;

        /// <summary>
        /// Adds the.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public void Add(int position, string path) {
            if (Items.Any(x => x.Text.Equals(path, StringComparison.OrdinalIgnoreCase))) {
                return;
            }
            if (Items.Any(x => x.Sequence == position)) {
                IncrementAll(position);
            }
            Items.Add(RecentItem.Create(path, command, position));
            Save();
            RefreshRequired?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Increments all above position.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        private void IncrementAll(int startPosition) {
            var reverseItems = Items.Where(x => x.Sequence >= startPosition).OrderByDescending(x => x.Sequence);
            reverseItems.ToList().ForEach(x => x.Sequence++);
        }

        /// <summary>
        /// Closes the sequence gap.
        /// </summary>
        private void CloseSequenceGap() {
            var index = 1;
            Items.OrderBy(x => x.Sequence).ToList().ForEach(x => {
                x.Sequence = index;
                index++;
            });
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="position">The position.</param>
        public void Remove(int position) {
            if (Items.Any(x => x.Sequence == position)) {
                Items.Remove(Items.First(x => x.Sequence == position));
                CloseSequenceGap();
                Save();
                RefreshRequired?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Remove(string path) {
            if (Items.Any(x => x.Text == path)) {
                Items.Remove(Items.First(x => x.Text == path));
                CloseSequenceGap();
                Save();
                RefreshRequired?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Saves the recent list.
        /// </summary>
        public void Save() {
            if (initialLoad) {
                return;
            }
            if (Items.Any()) {
                var fName = Path.Combine(storageDirectory, storageFileName);
                using var fsc = new FileStream(fName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fsc);
                sw.WriteLine($"#Most recent file list");
                var index = 1;
                Items.OrderBy(x => x.Sequence).Select(x => x.Text).ToList().ForEach(x => {
                    sw.WriteLine($"{index},{x}");
                    index++;
                });
            }
        }

        /// <summary>
        /// Moves the designated item to most recent.
        /// </summary>
        /// <param name="path">The path.</param>
        public void MoveToMostRecent(string path) {
            if (!Items.Any(x => x.Text.Equals(path, StringComparison.OrdinalIgnoreCase))) {
                return;
            }
            IncrementAll(1);
            Items.First(x => x.Text == path).Sequence = 1;
            RefreshRequired?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the ordered items.
        /// </summary>
        /// <returns>A list of string.</returns>
        public ObservableCollection<RecentItem> GetOrderedItems() =>
            new ObservableCollection<RecentItem>(Items.OrderBy(x => x.Sequence).Take(MaxItems));

        /// <summary>
        /// Gets all of the ordered items.
        /// </summary>
        /// <returns>A list of string.</returns>
        public ObservableCollection<RecentItem> GetAllOrderedItems() =>
           new ObservableCollection<RecentItem>(Items.OrderBy(x => x.Sequence));

        /// <summary>
        /// Clears the recent list.
        /// </summary>
        public void Clear() {
            Items.Clear();
            Save();
        }
    }
}
