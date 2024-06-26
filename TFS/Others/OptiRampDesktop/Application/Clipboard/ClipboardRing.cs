using System;
using System.Collections.Generic;
using System.Windows.Threading;
using MyApplication.IO;

namespace MyApplication.Clipboard
{
	public static class ClipboardRing
	{
		#region Private Fields
		private const string NewLineReplacer = "●";
		private static string ClipboardFileName = null;
		private static DispatcherTimer ReadTimer = null;
		#endregion

		#region Public Constructors

		static ClipboardRing()
		{
			ClipboardEntries = new List<string>();
		}

		#endregion

		#region Public Events
		public static event EventHandler ClipboardRingChanged;
		#endregion

		#region Public Properties
		public static List<string> ClipboardEntries { get; private set; }
		#endregion

		#region Public Methods

		public static void Clear()
		{
			ClipboardEntries.Clear();
			System.Windows.Clipboard.Clear();
			SaveEntries();
		}

		public static void InitializeRing()
		{
			InitializeRing(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "clipboardring.txt"));
		}

		public static void InitializeRing(string clipboardFileName)
		{
			InitializeRing(clipboardFileName, 1000);
		}

		public static void InitializeRing(string clipboardFileName, int checkClipboardIntervalMS)
		{
			ClipboardFileName = clipboardFileName;
			PopulateSavedEntries();
			ReadTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(checkClipboardIntervalMS) };
			ReadTimer.Tick += new EventHandler(ReadTimer_Tick);
			ReadTimer.Start();
		}

		public static void RemoveItemAt(int index)
		{
			ClipboardEntries.RemoveAt(index);
			System.Windows.Clipboard.Clear();
			SaveEntries();
		}

		#endregion

		#region Private Methods

		private static void PopulateSavedEntries()
		{
			if (!System.IO.File.Exists(ClipboardFileName))
				return;
			var lines = File.ReadAllLines(ClipboardFileName, System.IO.FileShare.None, false);
			foreach (var line in lines)
			{
				var value = line.Replace(NewLineReplacer, Environment.NewLine);
				ClipboardEntries.Insert(0, value);
			}
		}

		private static void ReadTimer_Tick(object sender, EventArgs e)
		{
			ReadTimer.Stop();
			if (System.Windows.Clipboard.ContainsText())
			{
				var text = System.Windows.Clipboard.GetText();
				var exists = false;
				foreach (var item in ClipboardEntries)
				{
					exists = item.Equals(text, StringComparison.InvariantCultureIgnoreCase);
					if (exists)
						break;
				}
				if (!exists)
				{
					ClipboardEntries.Insert(0, text);
					while (ClipboardEntries.Count > 20)
					{
						ClipboardEntries.RemoveAt(ClipboardEntries.Count - 1);
					}
					SaveEntries();
				}
			}
			ReadTimer.Start();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		private static void SaveEntries()
		{
			using (var fs = new System.IO.FileInfo(ClipboardFileName).Open(System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
			using (var sw = new System.IO.StreamWriter(fs))
			{
				for (int i = ClipboardEntries.Count - 1; i >= 0; i--)
				{
					sw.WriteLine(ClipboardEntries[i].Replace(Environment.NewLine, NewLineReplacer));
				}
			}
			if (ClipboardRingChanged != null)
				ClipboardRingChanged(null, EventArgs.Empty);
		}

		#endregion
	}
}