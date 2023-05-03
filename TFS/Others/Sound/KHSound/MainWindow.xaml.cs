using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using SharpSounder.Convert;
using SharpSounder.Media;
namespace KHSound
{
	public delegate Point GetDragDropPosition(IInputElement theElement);
	public partial class MainWindow : Window
	{
		private bool ConversionProcessRunning = false;
		private bool StopConversionProcess = false;
	
		private IMediaFile CurrentMediaFile = null;
		private List<LocalFileInfo> FilesToConvert = null;
		private List<IMediaFile> MediaFiles = null;
		private List<IMediaFile> SelectedMediaFiles = null;
		private bool StopPressed = false;
		private System.IO.FileSystemWatcher WavWatcher = null;
		public MainWindow()
		{
			InitializeComponent();
		}
		public string RecordingDirectory { get; set; }
		public bool WavFilesExist { get; set; }
		private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
		{
			var cb = sender as CheckBox;
			var path = cb.Content as String;
			FilesToConvert.First(x => x.Path == path).Selected = cb.IsChecked.HasValue && cb.IsChecked.Value;
		}
		private void CheckForWavFiles(System.IO.DirectoryInfo dirInfo)
		{
			if(WavFilesExist)
				return;
			if(dirInfo.GetFiles("*.wav").Any())
				WavFilesExist = true;
			else
				dirInfo.GetDirectories().ToList().ForEach(x => CheckForWavFiles(x));
		}
		private void ClearList_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SelectedMediaFiles != null && SelectedMediaFiles.Count > 0 &&
				(CurrentMediaFile == null || (CurrentMediaFile != null && !CurrentMediaFile.IsPlaying));
		}
		private void ClearList_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if(MessageBox.Show("Clear the pending song list?", "Clear songs", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
				return;
			SelectedMediaFiles.Clear();
			SelectedSongsListBox.ItemsSource = null;
			SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
		}
		private void Convert_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = WavFilesExist;
		}
		private void Convert_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SongsBorder.Visibility = System.Windows.Visibility.Collapsed;
			RecordingsBorder.Visibility = System.Windows.Visibility.Collapsed;
			ConvertBorder.Visibility = System.Windows.Visibility.Visible;
			RefreshWavFiles();
		}
		private void CurrentMediaFile_DurationAvailable(object sender, TimeSpanAvailableEventArgs e)
		{
			SelectedSongsListBox.ItemsSource = null;
			SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
		}
		private void CurrentMediaFile_PlayComplete(object sender, EventArgs e)
		{
			PurgeCurrentMediaFile();
		}
		private void CurrentMediaFile_PlayPaused(object sender, EventArgs e)
		{
		}
		private void CurrentMediaFile_PlayStarted(object sender, EventArgs e)
		{
			SelectedSongsListBox.ItemsSource = null;
			SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
			RemainingTextBlock.Text = "Remaining: 00:00";
			RemainingBorder.Visibility = System.Windows.Visibility.Visible;
			PlayProgressBar.Visibility = System.Windows.Visibility.Visible;
		}
		private void CurrentMediaFile_RemainingChanged(object sender, EventArgs e)
		{
			if(Dispatcher.CheckAccess())
			{
				var mf = sender as IMediaFile;
				if(!mf.Duration.HasValue)
					return;
				PlayProgressBar.Maximum = mf.Duration.Value.TotalSeconds;
				if(mf.Position.HasValue)
				{
					PlayProgressBar.Value = mf.Position.Value.TotalSeconds;
					RemainingTextBlock.Text = string.Format("Remaining: {0}", mf.Duration.Value.Subtract(mf.Position.Value).ToString(@"mm\:ss"));
				}
			}
			else
				Dispatcher.Invoke(new EventHandler(CurrentMediaFile_RemainingChanged), new object[] { sender, e });
		}
		private object GetDataFromListBox(ListBox source, Point point)
		{
			UIElement element = source.InputHitTest(point) as UIElement;
			if(element != null)
			{
				object data = DependencyProperty.UnsetValue;
				while(data == DependencyProperty.UnsetValue)
				{
					data = source.ItemContainerGenerator.ItemFromContainer(element);
					if(data == DependencyProperty.UnsetValue)
					{
						element = VisualTreeHelper.GetParent(element) as UIElement;
					}
					if(element == source)
					{
						return null;
					}
				}
				if(data != DependencyProperty.UnsetValue)
				{
					return data;
				}
			}
			return null;
		}
		private List<LocalFileInfo> GetWavFiles(System.IO.DirectoryInfo dirInfo)
		{
			var temp = new List<LocalFileInfo>();
			try
			{
				dirInfo.GetFiles("*.wav").ToList().ForEach(x =>
				{
					temp.Add(new LocalFileInfo { FileInfo = x, Selected = true });
				});
				dirInfo.GetDirectories().ToList().ForEach(x => temp.AddRange(GetWavFiles(x)));
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
			return temp;
		}
		private void LoadSongs()
		{
			try
			{
				var songFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "song.xml");
				var doc = XDocument.Load(songFileName);
				MediaFiles = new List<IMediaFile>();
				doc.Root.Elements().ToList()
					.ForEach(x =>
				{
					MediaFiles.Add(new MediaFile(System.IO.Path.Combine(App.MySettings.GetValue("Application", "MusicFolder", null), x.Attribute("filename").Value))
					{
						Sequence = int.Parse(x.Attribute("number").Value),
						Title = x.Attribute("title").Value
					});
				});
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
			SongsListBox.ItemsSource = MediaFiles.OrderBy(x => x.Sequence).ToList();
		}
		private void MakeRecordingDirectory()
		{
			try
			{
				var monDate = DateTime.Now;
				while(monDate.DayOfWeek != DayOfWeek.Monday)
				{
					monDate = monDate.AddDays(-1);
				}
				var dirName = System.IO.Path.Combine(App.MySettings.GetValue("Application", "RecordingFolder", null), monDate.Year.ToString());
				dirName = System.IO.Path.Combine(dirName, monDate.ToString("MMMM"));
				dirName = System.IO.Path.Combine(dirName, monDate.Day.ToString());
				if(!System.IO.Directory.Exists(dirName))
					System.IO.Directory.CreateDirectory(dirName);
				RecordingDirectory = dirName;
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void MyTitleBar_CloseButtonClicked(object sender, EventArgs e)
		{
			this.Close();
		}
		private void MyTitleBar_MaximizeButtonClicked(object sender, EventArgs e)
		{
			if(this.WindowState == System.Windows.WindowState.Normal)
				this.WindowState = System.Windows.WindowState.Maximized;
			else
				this.WindowState = System.Windows.WindowState.Normal;
		}
		private void MyTitleBar_MinimizeButtonClicked(object sender, EventArgs e)
		{
			this.WindowState = System.Windows.WindowState.Minimized;
		}
		private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SelectedMediaFiles != null && SelectedMediaFiles.Count > 0 && CurrentMediaFile != null && CurrentMediaFile.IsPlaying;
		}
		private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				CurrentMediaFile.Pause();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SelectedMediaFiles != null && SelectedMediaFiles.Count > 0;
		}
		private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				CurrentMediaFile = SelectedSongsListBox.Items[0] as IMediaFile;
				CurrentMediaFile.DurationAvailable += new DurationAvailableHandler(CurrentMediaFile_DurationAvailable);
				CurrentMediaFile.PlayComplete += new EventHandler(CurrentMediaFile_PlayComplete);
				CurrentMediaFile.PlayPaused += new EventHandler(CurrentMediaFile_PlayPaused);
				CurrentMediaFile.PlayStarted += new EventHandler(CurrentMediaFile_PlayStarted);
				CurrentMediaFile.RemainingChanged += new EventHandler(CurrentMediaFile_RemainingChanged);
				Logger.LogMessage(string.Format("Playing {0}", CurrentMediaFile.Title));
				CurrentMediaFile.Play();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void PlayProgressBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(!bool.Parse(App.MySettings.GetValue("Personal", "AllowJumpToPosition", "False")))
				return;
			try
			{
				var pb = sender as ProgressBar;
				var pos = e.GetPosition(pb);
				var pct = pos.X / pb.Width;
				var playPosition = CurrentMediaFile.Duration.Value.TotalSeconds * pct;
				CurrentMediaFile.Position = TimeSpan.FromSeconds(playPosition);
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void PurgeCurrentMediaFile()
		{
			if(CurrentMediaFile == null)
				return;
			try
			{
				CurrentMediaFile.DurationAvailable -= new DurationAvailableHandler(CurrentMediaFile_DurationAvailable);
				CurrentMediaFile.PlayComplete -= new EventHandler(CurrentMediaFile_PlayComplete);
				CurrentMediaFile.PlayPaused -= new EventHandler(CurrentMediaFile_PlayPaused);
				CurrentMediaFile.PlayStarted -= new EventHandler(CurrentMediaFile_PlayStarted);
				if((StopPressed && bool.Parse(App.MySettings.GetValue("Personal", "StopRemovesCurrent", "True")))
					|| (!StopPressed && bool.Parse(App.MySettings.GetValue("Personal", "RemoveAfterPlayback", "True"))))
					SelectedMediaFiles.Remove(CurrentMediaFile);
				CurrentMediaFile = null;
				SelectedSongsListBox.ItemsSource = null;
				SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
				PlayProgressBar.Visibility = System.Windows.Visibility.Collapsed;
				PlayProgressBar.Value = 0;
				RemainingBorder.Visibility = System.Windows.Visibility.Collapsed;
				CommandManager.InvalidateRequerySuggested();
				StopPressed = false;
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void Recordings_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SongsBorder.Visibility = System.Windows.Visibility.Collapsed;
			RecordingsBorder.Visibility = System.Windows.Visibility.Visible;
			ConvertBorder.Visibility = System.Windows.Visibility.Collapsed;
		}
		private void RefreshWavFiles()
		{
			FilesToConvert = GetWavFiles(new System.IO.DirectoryInfo(App.MySettings.GetValue("Application", "RecordingFolder", null)));
			ConversionListBox.ItemsSource = null;
			ConversionListBox.ItemsSource = FilesToConvert;
			CommandManager.InvalidateRequerySuggested();
		}
		private void RemoveSelectedSongsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (CurrentMediaFile!=null && CurrentMediaFile.IsPlaying)
				CurrentMediaFile.Stop();
			else
			{
				var tb = sender as TextBlock;
				var number = (int)tb.Tag;
				SelectedMediaFiles.Remove(SelectedMediaFiles.First(x => x.Sequence == number));
				SelectedSongsListBox.ItemsSource = null;
				SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
			}
		}
		private void SelectedSongsListBox_Drop(object sender, DragEventArgs e)
		{
			var mf = e.Data.GetData("SharpSounder.Media.MediaFile") as IMediaFile;
			if(SelectedMediaFiles == null)
				SelectedMediaFiles = new List<IMediaFile>();
			if(SelectedMediaFiles.Count < 3 && !SelectedMediaFiles.Contains(mf))
			{
				SelectedMediaFiles.Add(mf);
				SelectedSongsListBox.ItemsSource = null;
				SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
				CommandManager.InvalidateRequerySuggested();
			}
			e.Handled = true;
		}
		private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			new ApplicationSettingsWindow(this).ShowDialog();
			if(bool.Parse(App.MySettings.GetValue("Personal", "AllowJumpToPosition", "False")))
				PlayProgressBar.Cursor = Cursors.Hand;
			else
				PlayProgressBar.Cursor = Cursors.Arrow;
			LoadSongs();
		}
		private void Songs_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SongsBorder.Visibility = System.Windows.Visibility.Visible;
			RecordingsBorder.Visibility = System.Windows.Visibility.Collapsed;
			ConvertBorder.Visibility = System.Windows.Visibility.Collapsed;
		}
		private void SongsListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var lb = sender as ListBox;
			var mf = lb.SelectedItem as IMediaFile;
			if(SelectedMediaFiles == null)
				SelectedMediaFiles = new List<IMediaFile>();
			var max = int.Parse(App.MySettings.GetValue("Personal", "MaxNumPlaylist", "3"));
			if(SelectedMediaFiles.Count < max && !SelectedMediaFiles.Contains(mf))
			{
				SelectedMediaFiles.Add(mf);
				SelectedSongsListBox.ItemsSource = null;
				SelectedSongsListBox.ItemsSource = SelectedMediaFiles;
			}
			e.Handled = true;
		}
		private void SongsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var lb = sender as ListBox;
			var mf = (IMediaFile)GetDataFromListBox(lb, e.GetPosition(lb));
			lb.SelectedItem = mf;
			if(mf == null)
				return;
			var obj = new DataObject(mf);
			DragDrop.DoDragDrop(lb, obj, DragDropEffects.Copy);
		}
		private void StartConvert_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = ConversionListBox.Items.Count > 0;
		}
		private void StartConvert_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var lamePath = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "lame"), "lame.exe");
			if(!System.IO.File.Exists(lamePath))
			{
				MessageBox.Show("Cannot find lame executable", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			IConverter converter = new Mp3Converter(lamePath);
			try
			{
				ConversionProcessRunning = true;
				while(FilesToConvert.Where(x => x.Selected).Any())
				{
					if(StopConversionProcess)
						break;
					converter.Convert(FilesToConvert.Where(x => x.Selected).First().FileInfo);
					RefreshWavFiles();
				}
				StopConversionProcess = false;
				ConversionProcessRunning = false;
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = SelectedMediaFiles != null && CurrentMediaFile != null && CurrentMediaFile.IsPlaying;
		}
		private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			StopPressed = true;
			try
			{
				CurrentMediaFile.Stop();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void StopConvert_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = ConversionProcessRunning;
		}
		private void StopConvert_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if(!ConversionProcessRunning)
				return;
			StopConversionProcess = true;
		}
		private void WavWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			if(Dispatcher.CheckAccess())
				WavFilesExist = true;
			else
				Dispatcher.Invoke(new System.IO.FileSystemEventHandler(WavWatcher_Created), new object[] { sender, e });
		}
		private void WavWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{
			if(Dispatcher.CheckAccess())
			{
				WavFilesExist = false;
				CheckForWavFiles(new System.IO.DirectoryInfo(App.MySettings.GetValue("Application", "RecordingFolder", null)));
				CommandManager.InvalidateRequerySuggested();
			}
			else
				Dispatcher.Invoke(new System.IO.FileSystemEventHandler(WavWatcher_Deleted), new object[] { sender, e });
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if(string.IsNullOrEmpty(App.MySettings.GetValue("Application", "MusicFolder", null))
				|| string.IsNullOrEmpty(App.MySettings.GetValue("Application", "RecordingFolder", null)))
			{
				new ApplicationSettingsWindow(this).ShowDialog();
				if(string.IsNullOrEmpty(App.MySettings.GetValue("Application", "MusicFolder", null))
					|| string.IsNullOrEmpty(App.MySettings.GetValue("Application", "RecordingFolder", null)))
					Environment.Exit(99);
			}
			if(bool.Parse(App.MySettings.GetValue("Personal", "AllowJumpToPosition", "False")))
				PlayProgressBar.Cursor = Cursors.Hand;
			LoadSongs();
			MakeRecordingDirectory();
			CheckForWavFiles(new System.IO.DirectoryInfo(App.MySettings.GetValue("Application", "RecordingFolder", null)));
			WavWatcher = new System.IO.FileSystemWatcher(App.MySettings.GetValue("Application", "RecordingFolder", null), "*.wav");
			WavWatcher.IncludeSubdirectories = true;
			WavWatcher.EnableRaisingEvents = true;
			WavWatcher.Created += new System.IO.FileSystemEventHandler(WavWatcher_Created);
			WavWatcher.Deleted += new System.IO.FileSystemEventHandler(WavWatcher_Deleted);
			RecordingsBorder.Visibility = System.Windows.Visibility.Collapsed;
			ConvertBorder.Visibility = System.Windows.Visibility.Collapsed;
		}
	}
}
