using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Ookii.Dialogs.Wpf;
using GregOsborne.Application.Windows;
using GregOsborne.Application.Windows.Controls;
using System.Windows.Controls.Ribbon;
using GregOsborne.MVVMFramework;
using GregOsborne.Dialogs;
using GregOsborne.Application.Primitives;

namespace FormatCodeFile
{
	public partial class MainWindow : RibbonWindow
	{
		#region Private Fields
		private int countOfFiles = 0;

		private int fileNumber = 0;
		#endregion

		#region Public Constructors

		public MainWindow()
		{
			InitializeComponent();

            this.DataContext = new MainWindowView();

            this.View.PropertyChanged += MainWindowView_PropertyChanged;
            this.View.SelectBaseFolder += MainWindowView_SelectBaseFolder;
            this.View.ShowSettings += MainWindowView_ShowSettings;

            Properties.Settings.Default.Upgrade();
			Properties.Settings.Default.Save();
		}

        public MainWindowView View => this.DataContext.As<MainWindowView>();

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
			//MyToolbar.RemoveOverflow();
		}

		#endregion

		#region Private Methods

		private void AddMessage(string message)
		{
			if (Dispatcher.CheckAccess())
			{
				MessageListBox.Items.Add(message);
				MessageListBox.SelectedIndex = MessageListBox.Items.Count - 1;
				MessageListBox.ScrollIntoView(message);
			}
			else
				Dispatcher.Invoke(new Action<string>(AddMessage), new object[] { message });
		}

		private void CountFiles()
		{
			var fileCount = 0;
			foreach (var folder in View.Folders)
			{
				fileCount = GetFileCount(folder as Classes.Folder);
			}
			countOfFiles = fileCount;
		}

		private IEnumerable<string> FileLines(string fileName, MainWindowView view)
		{
			if (view.SaveUnmodifiedCode)
			{
				var counter = 0;
				var newFileName = string.Format("{0}.backup.{1}", fileName, counter);
				if (view.SaveAllVersions)
				{
					while (System.IO.File.Exists(newFileName))
					{
						counter++;
						newFileName = string.Format("{0}.backup.{1}", fileName, counter);
					}
				}
				System.IO.File.Copy(fileName, newFileName);
			}
			bool mustRemoveBlankLines = view.RemoveBlankLines;
			bool mustRemoveComments = view.RemoveComments;
			bool mustFormatCodeFile = view.FormatCodeFile;
			bool mustRemoveRegions = view.RemoveRegions;
			using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
			using (var sr = new System.IO.StreamReader(fs))
			{
				while (sr.Peek() != -1)
				{
					var line = sr.ReadLine();
					var isHtmlComment = line.Trim().StartsWith("///");
					var removeLine = (string.IsNullOrEmpty(line.Trim()) && mustRemoveBlankLines)
						|| (isHtmlComment && mustFormatCodeFile)
						|| (line.Trim().StartsWith("//") && mustRemoveComments && !isHtmlComment)
						|| ((line.Trim().StartsWith("#region") && mustRemoveRegions) || (line.Trim().StartsWith("#endregion") && mustRemoveRegions));
					if (!removeLine)
					{
						if (mustRemoveComments && (line.Contains("//") && !line.Contains("\"//") && !line.Contains("://")) && !isHtmlComment)
							line = line.Substring(0, line.IndexOf("//")).TrimEnd();
						yield return line;
					}
				}
			}
		}

		private int GetFileCount(Classes.Folder folder)
		{
			var result = 0;
			if (!folder.Files.Any() && !folder.Folders.Any())
				return result;
			if (folder.Files.Any())
				result += folder.Files.Count();
			if (folder.Folders.Any())
			{
				foreach (var subFolder in folder.Folders)
				{
					result += GetFileCount(subFolder as Classes.Folder);
				}
			}
			return result;
		}

		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Folders")
			{
				CountFiles();

				var worker = new BackgroundWorker
				{
					WorkerReportsProgress = true,
					WorkerSupportsCancellation = true
				};
				worker.ProgressChanged += worker_ProgressChanged;
				worker.RunWorkerCompleted += worker_RunWorkerCompleted;
				worker.DoWork += worker_DoWork;
				worker.RunWorkerAsync(View);
			}
		}

		private void MainWindowView_SelectBaseFolder(object sender, EventArgs e)
		{
			var dlg = new VistaFolderBrowserDialog()
			{
				Description = "Select folder...",
				RootFolder = Environment.SpecialFolder.MyComputer,
				SelectedPath = string.IsNullOrEmpty(Properties.Settings.Default.LastDirectory)
						? Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
						: Properties.Settings.Default.LastDirectory,
				UseDescriptionForTitle = true
			};
			var result = dlg.ShowDialog(App.Current.MainWindow);
			if (!result.GetValueOrDefault())
				return;

			View.SelectedFolder = dlg.SelectedPath;
		}

		private void MainWindowView_ShowSettings(object sender, EventArgs e)
		{
			var settingsWin = new SettingsWindow { Owner = this };
			settingsWin.View.RemoveBlankLines = View.RemoveBlankLines;
			settingsWin.View.RemoveComments = View.RemoveComments;
			settingsWin.View.FormatCodeFile = View.FormatCodeFile;
			settingsWin.View.RemoveRegions = View.RemoveRegions;
			settingsWin.View.SaveUnmodifiedCode = View.SaveUnmodifiedCode;
			settingsWin.View.SaveAllVersions = View.SaveAllVersions;

			if (!settingsWin.ShowDialog().GetValueOrDefault())
				return;

			View.RemoveBlankLines = settingsWin.View.RemoveBlankLines;
			View.RemoveComments = settingsWin.View.RemoveComments;
			View.FormatCodeFile = settingsWin.View.FormatCodeFile;
			View.RemoveRegions = settingsWin.View.RemoveRegions;
			View.SaveAllVersions = settingsWin.View.SaveAllVersions;
			View.SaveUnmodifiedCode = settingsWin.View.SaveUnmodifiedCode;
		}

		private void ProcessFile(string fileName, BackgroundWorker worker, MainWindowView view)
		{
			fileNumber++;
			worker.ReportProgress((int)(((double)fileNumber / (double)countOfFiles) * 100));
			AddMessage(string.Format("Processing {1} of {2} ({0})", fileName, fileNumber.ToString("#,0"), countOfFiles.ToString("#,0")));
			System.Threading.Thread.Sleep(50);
			var newData = new StringBuilder();
			foreach (var line in FileLines(fileName, view))
			{
				newData.AppendLine(line);
			}
			System.IO.File.Delete(fileName);
			using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write))
			{
				using (var sw = new System.IO.StreamWriter(fs, Encoding.Unicode))
				{
					sw.Write(newData);
					sw.Close();
				}
			}
		}

		private void ProcessFolder(Classes.Folder folder, BackgroundWorker worker, MainWindowView view)
		{
			foreach (var subFolder in folder.Folders)
			{
				ProcessFolder(subFolder as Classes.Folder, worker, view);
			}
			foreach (var file in folder.Files)
			{
				ProcessFile((file as Classes.File).FullPath, worker, view);
			}
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			MainWindowView view = e.Argument as MainWindowView;
			var folders = view.Folders;
			var worker = sender as BackgroundWorker;
			foreach (var folder in folders)
			{
				ProcessFolder(folder as Classes.Folder, worker, view);
			}
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (Dispatcher.CheckAccess())
				MyProgressBar.Value = e.ProgressPercentage;
			else
				Dispatcher.Invoke(new ProgressChangedEventHandler(worker_ProgressChanged), new object[] { sender, e });
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var taskDlg = new TaskDialog
			{
				AllowDialogCancellation = true,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				CenterParent = true,
				Content = string.Format("{0} files processed", countOfFiles),
				MainInstruction = "Document format complete",
				MinimizeBox = false,
				WindowTitle = "Complete",
				MainIcon = TaskDialogIcon.Information
			};
			taskDlg.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
			taskDlg.ShowDialog(this);
		}

		#endregion
	}
}
