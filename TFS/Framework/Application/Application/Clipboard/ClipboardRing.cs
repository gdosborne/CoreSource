using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Threading;

namespace GregOsborne.Application.Clipboard {
    public static class ClipboardRing {
        private const string NewLineReplacer = "●";

        private static string _clipboardFileName;

        private static DispatcherTimer _readTimer;

        static ClipboardRing() {
            ClipboardEntries = new List<string>();
        }

        public static List<string> ClipboardEntries { get; }

        public static event EventHandler ClipboardRingChanged;

        public static void Clear() {
            ClipboardEntries.Clear();
            System.Windows.Clipboard.Clear();
            SaveEntries();
        }

        public static void InitializeRing() {
            InitializeRing(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "clipboardring.txt"));
        }

        public static void InitializeRing(string clipboardFileName) {
            InitializeRing(clipboardFileName, 1000);
        }

        public static void InitializeRing(string clipboardFileName, int checkClipboardIntervalMs) {
            _clipboardFileName = clipboardFileName;
            PopulateSavedEntries();
            _readTimer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(checkClipboardIntervalMs)};
            _readTimer.Tick += ReadTimer_Tick;
            _readTimer.Start();
        }

        public static void RemoveItemAt(int index) {
            ClipboardEntries.RemoveAt(index);
            System.Windows.Clipboard.Clear();
            SaveEntries();
        }

        private static void PopulateSavedEntries() {
            if (!File.Exists(_clipboardFileName))
                return;
            var lines = IO.File.ReadAllLines(_clipboardFileName, FileShare.None, false);
            foreach (var line in lines) {
                var value = line.Replace(NewLineReplacer, Environment.NewLine);
                ClipboardEntries.Insert(0, value);
            }
        }

        private static void ReadTimer_Tick(object sender, EventArgs e) {
            _readTimer.Stop();
            if (System.Windows.Clipboard.ContainsText()) {
                var text = System.Windows.Clipboard.GetText();
                var exists = false;
                foreach (var item in ClipboardEntries) {
                    exists = item.Equals(text, StringComparison.InvariantCultureIgnoreCase);
                    if (exists)
                        break;
                }
                if (!exists) {
                    ClipboardEntries.Insert(0, text);
                    while (ClipboardEntries.Count > 20)
                        ClipboardEntries.RemoveAt(ClipboardEntries.Count - 1);
                    SaveEntries();
                }
            }
            _readTimer.Start();
        }

        private static void SaveEntries() {
            using (var fs = new FileInfo(_clipboardFileName).Open(FileMode.Create, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs)) {
                for (var i = ClipboardEntries.Count - 1; i >= 0; i--)
                    sw.WriteLine(ClipboardEntries[i].Replace(Environment.NewLine, NewLineReplacer));
            }
            ClipboardRingChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}