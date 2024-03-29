using KHS4.Controls;
using Microsoft.Win32;
using SharpSounder.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace KHS4
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MusicFolderTextBox.Text = App.MySettings.GetValue("Application", "MusicFolder", string.Empty);
			RecordingFolderTextBox.Text = App.MySettings.GetValue("Application", "RecordingFolder", string.Empty);
			AutoConvertOnOff.Value = bool.Parse(App.MySettings.GetValue("Personal", "ConvertImmediately", "false"));
			ClickToJumpOnOff.Value = bool.Parse(App.MySettings.GetValue("Personal", "AllowJumpToPosition", "false"));
			ClickStopRemovesSongOnOff.Value = bool.Parse(App.MySettings.GetValue("Personal", "StopRemovesCurrent", "false"));
			RemoveSongWhenEndOnOff.Value = bool.Parse(App.MySettings.GetValue("Personal", "RemoveAfterPlayback", "false"));
			AutomaticallyStartNextRecordingOnOff.Value = bool.Parse(App.MySettings.GetValue("Personal", "AutoStartNextRecording", "false"));
			Initializing = false;
			if (!string.IsNullOrEmpty(MusicFolderTextBox.Text))
				LoadSongs();
		}

		private bool IsPlaying = false;
		private List<IMediaFile> MediaFiles = null;
		private List<string> MediaErrors = null;
		private bool StopClicked = false;
		private bool Initializing = true;
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			Close();
		}

		private void AutoConvertOnOff_ValueChanged(object sender, EventArgs e)
		{
			if (Initializing)
				return;
			var onoff = sender as OnOff;
			App.MySettings.SetValue("Personal", "ConvertImmediately", onoff.Value.ToString());
		}

		private void ClickToJumpOnOff_ValueChanged(object sender, EventArgs e)
		{
			if (Initializing)
				return;
			var onoff = sender as OnOff;
			App.MySettings.SetValue("Personal", "AllowJumpToPosition", onoff.Value.ToString());
		}

		private void ClickStopRemovesSongOnOff_ValueChanged(object sender, EventArgs e)
		{
			if (Initializing)
				return;
			var onoff = sender as OnOff;
			App.MySettings.SetValue("Personal", "StopRemovesCurrent", onoff.Value.ToString());
		}

		private void RemoveSongWhenEndOnOff_ValueChanged(object sender, EventArgs e)
		{
			if (Initializing)
				return;
			var onoff = sender as OnOff;
			App.MySettings.SetValue("Personal", "RemoveAfterPlayback", onoff.Value.ToString());
		}

		private void AutomaticallyStartNextRecordingOnOff_ValueChanged(object sender, EventArgs e)
		{
			if (Initializing)
				return;
			var onoff = sender as OnOff;
			App.MySettings.SetValue("Personal", "AutoStartNextRecording", onoff.Value.ToString());
		}

		private string SelectFolder(string type, string defaultFolder, string registryKey)
		{
			var fbd = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
			{
				Description = string.Format("Select {0} folder...", type),
				RootFolder = Environment.SpecialFolder.MyComputer,
				ShowNewFolderButton = true,
				UseDescriptionForTitle = true,
				SelectedPath = defaultFolder
			};
			var result = fbd.ShowDialog();
			if (!result.GetValueOrDefault())
				return null;
			return fbd.SelectedPath;
		}
		private void MusicFolderExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var folder = SelectFolder("music", MusicFolderTextBox.Text, "MusicFolder");
			if (string.IsNullOrEmpty(folder))
				return;
			MusicFolderTextBox.Text = folder;
			App.MySettings.SetValue("Application", "MusicFolder", folder);
		}

		private void RecordFolderExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var folder = SelectFolder("recording", RecordingFolderTextBox.Text, "RecordingFolder");
			if (string.IsNullOrEmpty(folder))
				return;
			RecordingFolderTextBox.Text = folder;
			App.MySettings.SetValue("Application", "RecordingFolder", folder);
		}

		private void LoadSongs()
		{
			try
			{
				var songFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "song.xml");
				var doc = XDocument.Load(songFileName);
				MediaFiles = new List<IMediaFile>();
				var songsFolder = App.MySettings.GetValue("Application", "MusicFolder", string.Empty);
				if (string.IsNullOrEmpty(songsFolder) || !Directory.Exists(songsFolder))
					return;

				MediaErrors = new List<string>();
				doc.Root.Elements().ToList().ForEach(x =>
				{
					var fileName = System.IO.Path.Combine(songsFolder, x.Attribute("filename").Value);
					if (!File.Exists(fileName))
					{
						MediaErrors.Add(x.Attribute("filename").Value);
						return;
					}
					MediaFiles.Add(new MediaFile(fileName)
					{
						Sequence = int.Parse(x.Attribute("number").Value),
						Title = x.Attribute("title").Value
					});
				});
				if (MediaErrors.Count > 0)
				{
					Logger.LogMessage(string.Format("The following media files were not found in {1}: {0}", string.Join(", ", MediaErrors), songsFolder));
					using (var dialog = new Ookii.Dialogs.Wpf.TaskDialog
					{
						AllowDialogCancellation = false,
						Content = string.Join(", ", MediaErrors),
						MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Error,
						MainInstruction = "Errors occurred while attempting to load media files.",
						MinimizeBox = false,
						WindowTitle = "Media Errors"
					})
					{
						Ookii.Dialogs.Wpf.TaskDialogButton okButton = new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok);
						dialog.Buttons.Add(okButton);
						dialog.ShowDialog(this);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
				return;
			}
			SongsListBox.ItemsSource = MediaFiles.OrderBy(x => x.Sequence).ToList();
		}

		private void SongsListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var lb = sender as ListBox;
			var mf = (IMediaFile)GetDataFromListBox(lb, e.GetPosition(lb));
			lb.SelectedItem = mf;
			if (mf == null)
				return;
			var obj = new DataObject(mf);
			DragDrop.DoDragDrop(lb, obj, DragDropEffects.Copy);
		}
		private object GetDataFromListBox(ListBox source, Point point)
		{
			UIElement element = source.InputHitTest(point) as UIElement;
			if (element != null)
			{
				object data = DependencyProperty.UnsetValue;
				while (data == DependencyProperty.UnsetValue)
				{
					data = source.ItemContainerGenerator.ItemFromContainer(element);
					if (data == DependencyProperty.UnsetValue)
					{
						element = VisualTreeHelper.GetParent(element) as UIElement;
					}
					if (element == source)
					{
						return null;
					}
				}
				if (data != DependencyProperty.UnsetValue)
				{
					return data;
				}
			}
			return null;
		}

		private void Song1Border_Drop(object sender, DragEventArgs e)
		{
			var mf = e.Data.GetData("SharpSounder.Media.MediaFile") as IMediaFile;
			var bdr = sender as Border;
			bdr.Tag = mf;
			Song1TextBlock.Text = mf.Sequence.ToString();
			Song1ClearImage.Visibility = Visibility.Visible;
			e.Handled = true;
			Logger.LogMessage(string.Format("Added {0}", mf.FileName));
		}

		private void Song2Border_Drop(object sender, DragEventArgs e)
		{
			if (Song1Border.Tag == null)
			{
				Song1Border_Drop(Song1Border, e);
				return;
			}
			var mf = e.Data.GetData("SharpSounder.Media.MediaFile") as IMediaFile;
			var bdr = sender as Border;
			bdr.Tag = mf;
			Song2TextBlock.Text = mf.Sequence.ToString();
			Song2ClearImage.Visibility = Visibility.Visible;
			e.Handled = true;
			Logger.LogMessage(string.Format("Added {0}", mf.FileName));
		}

		private void Song3Border_Drop(object sender, DragEventArgs e)
		{
			if (Song2Border.Tag == null)
			{
				Song2Border_Drop(Song2Border, e);
				return;
			}
			var mf = e.Data.GetData("SharpSounder.Media.MediaFile") as IMediaFile;
			var bdr = sender as Border;
			bdr.Tag = mf;
			Song3TextBlock.Text = mf.Sequence.ToString();
			Song3ClearImage.Visibility = Visibility.Visible;
			e.Handled = true;
			Logger.LogMessage(string.Format("Added {0}", mf.FileName));
		}

		private void SongBorder_MouseEnter(object sender, MouseEventArgs e)
		{
			var bdr = sender as Border;
			if (bdr.Tag != null)
				bdr.Background = new SolidColorBrush(Color.FromArgb(55, 0, 0, 200));
		}

		private void SongBorder_MouseLeave(object sender, MouseEventArgs e)
		{
			var bdr = sender as Border;
			bdr.Background = new SolidColorBrush(Colors.Transparent);
		}

		private bool AskDeleteSong(bool isLastSong, int position, bool hasLaterSongs)
		{
			using (var dialog = new Ookii.Dialogs.Wpf.TaskDialog
			{
				AllowDialogCancellation = false,
				Content = "You will have to drop another song on this playlist position if you delete it. " + (hasLaterSongs ? "Also, deleting a song from this position will move songs from later positions in the playlist up." : string.Empty),
				MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Warning,
				MainInstruction = "Click the OK button to remove the song from the playlist",
				MinimizeBox = false,
				WindowTitle = string.Format("Delete song at position {0}", position)
			})
			{
				Ookii.Dialogs.Wpf.TaskDialogButton okButton = new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok);
				Ookii.Dialogs.Wpf.TaskDialogButton cancelButton = new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Cancel);
				dialog.Buttons.Add(okButton);
				dialog.Buttons.Add(cancelButton);
				Ookii.Dialogs.Wpf.TaskDialogButton button = dialog.ShowDialog(this);
				if (button == cancelButton)
					return false;
				return true;
			}
		}

		private void SetDeleteButtons()
		{
			Song1ClearImage.Visibility = Song1Border.Tag == null ? Visibility.Collapsed : Visibility.Visible;
			Song2ClearImage.Visibility = Song2Border.Tag == null ? Visibility.Collapsed : Visibility.Visible;
			Song3ClearImage.Visibility = Song3Border.Tag == null ? Visibility.Collapsed : Visibility.Visible;
		}

		private void MoveSongUp(Border toBorder, Border fromBorder, TextBlock toTextBlock, TextBlock fromTextBlock)
		{
			toBorder.Tag = null;
			toTextBlock.Text = string.Empty;
			if (fromBorder.Tag != null)
			{
				toBorder.Tag = fromBorder.Tag;
				toTextBlock.Text = (toBorder.Tag as IMediaFile).Sequence.ToString();
				fromBorder.Tag = null;
				fromTextBlock.Text = string.Empty;
			}
		}

		private void Song1ClearImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			if (IsPlaying)
				return;
			if (AskDeleteSong(false, 1, Song2Border.Tag != null))
			{
				var mf = Song1Border.Tag as IMediaFile;
				Logger.LogMessage(string.Format("Moving {0} up", mf.FileName));
				MoveSongUp(Song1Border, Song2Border, Song1TextBlock, Song2TextBlock);
				MoveSongUp(Song2Border, Song3Border, Song2TextBlock, Song3TextBlock);
				SetDeleteButtons();
			}
		}
		private void Song2ClearImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			if (AskDeleteSong(false, 2, Song3Border.Tag != null))
			{
				var mf = Song2Border.Tag as IMediaFile;
				Logger.LogMessage(string.Format("Moving {0} up", mf.FileName));
				MoveSongUp(Song2Border, Song3Border, Song2TextBlock, Song3TextBlock);
				SetDeleteButtons();
			}
		}
		private void Song3ClearImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			if (AskDeleteSong(true, 3, false))
			{
				var mf = Song3Border.Tag as IMediaFile;
				Logger.LogMessage(string.Format("Moving {0} up", mf.FileName));
				Song3Border.Tag = null;
				Song3TextBlock.Text = string.Empty;
			}
			SetDeleteButtons();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.Left < 0)
			{
				this.Left = 0;
				this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
			}
			if (this.Top < 0)
			{
				this.Top = 0;
				this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
			}
		}

		private void Song1Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((sender as Border).Tag == null)
				return;
			var mf = (sender as Border).Tag as IMediaFile;
			mf.PlayComplete += mf_PlayComplete;
			mf.PlayStarted += mf_PlayStarted;
			mf.RemainingChanged += mf_RemainingChanged;
			Logger.LogMessage(string.Format("Playing {0}", mf.FileName));
			mf.Play();
		}

		void mf_RemainingChanged(object sender, EventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				var mf = sender as IMediaFile;
				if (!mf.Duration.HasValue)
					return;
				PlayProgressBar.Maximum = mf.Duration.Value.TotalSeconds;
				if (mf.Position.HasValue)
				{
					PlayProgressBar.Value = mf.Position.Value.TotalSeconds;
					RemainingTextBlock.Text = mf.Duration.Value.Subtract(mf.Position.Value).ToString(@"mm\:ss");
				}
			}
			else
				Dispatcher.Invoke(new EventHandler(mf_RemainingChanged), new object[] { sender, e });
		}

		void mf_PlayStarted(object sender, EventArgs e)
		{
			RemainingTextBlock.Text = "00:00";
			IsPlaying = true;
		}

		private void RemovePlayingSong()
		{
			Song1Border.Tag = null;
			MoveSongUp(Song1Border, Song2Border, Song1TextBlock, Song2TextBlock);
			MoveSongUp(Song2Border, Song3Border, Song2TextBlock, Song3TextBlock);
			SetDeleteButtons();
		}

		void mf_PlayComplete(object sender, EventArgs e)
		{
			CommandManager.InvalidateRequerySuggested();
			RemainingTextBlock.Text = "00:00";
			PlayProgressBar.Value = 0;
			IsPlaying = false;
			if ((StopClicked && ClickStopRemovesSongOnOff.Value) || (!StopClicked && RemoveSongWhenEndOnOff.Value))
				RemovePlayingSong();
			StopClicked = false;
		}

		private void StopPlayingExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var mf = Song1Border.Tag as IMediaFile;
			StopClicked = true;
			mf.Stop();
		}

		private void StopPlayingCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = IsPlaying;
			System.Diagnostics.Debug.WriteLine(IsPlaying.ToString());
		}

		private void PlayProgressBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsPlaying || !ClickToJumpOnOff.Value)
				return;
			try
			{
				var mf = Song1Border.Tag as IMediaFile;
				var pb = sender as ProgressBar;
				var pos = e.GetPosition(pb);
				var pct = pos.X / pb.ActualWidth;
				var playPosition = mf.Duration.Value.TotalSeconds * pct;
				mf.Position = TimeSpan.FromSeconds(playPosition);
				Logger.LogMessage(string.Format("Seeking to position {1} for {0}", mf.FileName, mf.Position.Value.ToString(@"mm\:ss")));
			}
			catch (Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
	}
}
