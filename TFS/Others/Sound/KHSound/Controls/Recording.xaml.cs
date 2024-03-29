using System;
using System.Linq;
using System.Windows.Controls;
using SharpSounder.Recording;
namespace KHSound.Controls
{
	public partial class Recording : UserControl
	{
		public event RecordingCompleteHandler RecordingComplete;
		public event RecordingCompleteHandler RemoveRecording;
		private int _Current = 0;
		public Recording(MeetingPart meetingPart, string recordingFolder)
		{
			MeetingPart = meetingPart;
			RecordingDirectory = recordingFolder;
			InitializeComponent();
		}
		public int Current
		{
			get { return _Current; }
			set
			{
				_Current = value;
				if(_Current == Sequence && bool.Parse(App.MySettings.GetValue("Personal", "AutoStartNextRecording", "True")))
				{
					Logger.LogMessage(string.Format("Recording {0}", RecordingMedia.FileName));
					RecordingMedia.Start();
				}
			}
		}
		public string FileName { get { return RecordingMedia.FileName; } }
		public bool IsComplete { get { return RecordingMedia != null && RecordingMedia.IsComplete; } }
		public bool IsRecording { get { return RecordingMedia != null && RecordingMedia.IsRecording; } }
		public MeetingPart MeetingPart { get; private set; }
		public int Sequence { get; set; }
		private string RecordingDirectory { get; set; }
		private IRecordable RecordingMedia { get; set; }
		public void SetCurrentNoPlay(int value)
		{
			_Current = value;
		}
		private void MyTitleBar_CloseButtonClicked(object sender, EventArgs e)
		{
			if(RemoveRecording != null)
				RemoveRecording(this, new RecordingCompleteEventArgs(Sequence));
		}
		private void Pause_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = RecordingMedia != null && !RecordingMedia.IsComplete && (Current == Sequence) && RecordingMedia.IsRecording && !RecordingMedia.IsPaused;
		}
		private void Pause_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			try
			{
				RecordingMedia.Pause();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void Record_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = RecordingMedia != null && !RecordingMedia.IsComplete && (Current == Sequence) && (!RecordingMedia.IsRecording || RecordingMedia.IsPaused);
			if(IsRecording)
				MyTitleBar.CloseVisibilty = System.Windows.Visibility.Collapsed;
			else
				MyTitleBar.CloseVisibilty = System.Windows.Visibility.Visible;
		}
		private void Record_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			try
			{
				RecordingMedia.Start();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
		}
		private void Stop_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = RecordingMedia != null && !RecordingMedia.IsComplete && (Current == Sequence) && RecordingMedia.IsRecording;
		}
		private void Stop_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			try
			{
				RecordingMedia.Stop();
			}
			catch(Exception ex)
			{
				Logger.LogException(ex);
				App.DisplayExecption(ex, false);
			}
			MainGrid.Opacity = 0.5;
			if(RecordingComplete != null)
				RecordingComplete(this, new RecordingCompleteEventArgs(Sequence));
		}
		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			RecordingMedia = new MediaRecording(System.IO.Path.Combine(RecordingDirectory, string.Format("{0}.wav", MeetingPart.PartType.ToString())));
			RecordingMedia.LengthUpdated += new EventHandler(RecordingMedia_LengthUpdated);
			RecordingMedia.RecordingComplete += new EventHandler(RecordingMedia_RecordingComplete);
			RecordingMedia.RecordingPaused += new EventHandler(RecordingMedia_RecordingPaused);
			RecordingMedia.RecordingStarted += new EventHandler(RecordingMedia_RecordingStarted);
			if(MeetingPart != null)
			{
				MyTitleBar.Text = MeetingPart.PartType.ToString();
			}
		}
		void RecordingMedia_RecordingStarted(object sender, EventArgs e)
		{
			RecordingLengthTextBlock.Visibility = System.Windows.Visibility.Visible;
			Logger.LogMessage(string.Format("Recording {0}", RecordingMedia.FileName));
		}
		void RecordingMedia_RecordingPaused(object sender, EventArgs e)
		{
			Logger.LogMessage(string.Format("Paused {0}", RecordingMedia.FileName));
		}
		void RecordingMedia_RecordingComplete(object sender, EventArgs e)
		{
			RecordingLengthTextBlock.Visibility = System.Windows.Visibility.Collapsed;
			Logger.LogMessage(string.Format("{0} Complete", RecordingMedia.FileName));
		}
		void RecordingMedia_LengthUpdated(object sender, EventArgs e)
		{
			if(Dispatcher.CheckAccess())
				RecordingLengthTextBlock.Text = RecordingMedia.Length.ToString();
			else
				Dispatcher.Invoke(new EventHandler(RecordingMedia_LengthUpdated), sender, e);
		}
	}
}
