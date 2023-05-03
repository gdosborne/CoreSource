using System;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Media;
namespace SharpSounder.Media
{
	public class MediaFile : IMediaFile
	{
		public event DurationAvailableHandler DurationAvailable;
		public event EventHandler PlayComplete;
		public event EventHandler PlayPaused;
		public event EventHandler PlayStarted;
		public event EventHandler RemainingChanged;
		private bool DurationSignalled = false;
		private string fileName = null;
		private MediaPlayer player = null;
		private Timer PlayTimer = null;
		public MediaFile()
		{
			IsPaused = false;
			IsPlaying = false;
			PlayTimer = new Timer(1000);
			PlayTimer.Elapsed += new ElapsedEventHandler(PlayTimer_Elapsed);
		}
		public MediaFile(string filePath)
			: this()
		{
			fileName = filePath;
			player = new MediaPlayer();
			player.BufferingStarted += new EventHandler(player_BufferingStarted);
			player.BufferingEnded += new EventHandler(player_BufferingEnded);
			player.MediaOpened += new EventHandler(player_MediaOpened);
			player.MediaEnded += new EventHandler(player_MediaEnded);
			player.MediaFailed += new EventHandler<ExceptionEventArgs>(player_MediaFailed);
		}
		public double AmountComplete
		{
			get
			{
				if(player == null || !IsPlaying || !Duration.HasValue || !Position.HasValue) return 0;
				return Position.Value.TotalSeconds / Duration.Value.TotalSeconds;
			}
		}
		public bool Buffering { get; private set; }
		public string DeleteText
		{
			get { return "Remove"; }
		}
		public TimeSpan? Duration
		{
			get
			{
				if(player.NaturalDuration.HasTimeSpan)
					return player.NaturalDuration.TimeSpan;
				else
					return null;
			}
		}
		public string DurationString
		{
			get { return Duration.HasValue ? Duration.Value.ToString(@"mm\:ss") : "00:00"; }
		}
		public System.IO.FileInfo FileInfo
		{
			get { return new FileInfo(fileName); }
		}
		public string FileName
		{
			get
			{
				return fileName;
			}
			set
			{
				if(IsPlaying)
				{
					player.Stop();
					IsPlaying = false;
					if(PlayComplete != null)
						PlayComplete(this, EventArgs.Empty);
				}
				Opened = false;
				fileName = value;
				if(player == null)
					player = new MediaPlayer();
				player.Open(new Uri(FileName, UriKind.Relative));
			}
		}
		public bool IsPaused { get; private set; }
		public bool IsPlaying { get; private set; }
		public bool Opened { get; private set; }
		public TimeSpan? Position
		{
			get
			{
				if(player == null || !IsPlaying) return null;
				return player.Position;
			}
			set
			{
				if(player == null || !IsPlaying || !value.HasValue) return;
				player.Position = value.Value;
			}
		}
		public TimeSpan? Remaining
		{
			get
			{
				if(player == null || !Duration.HasValue || !Position.HasValue) return null;
				return Duration.Value.Subtract(Position.Value);
			}
		}
		public int Sequence { get; set; }
		public Int64 Size
		{
			get
			{
				if(FileInfo == null)
					return 0;
				else
					return FileInfo.Length;
			}
		}
		public double SizeMB
		{
			get
			{
				return (System.Convert.ToDouble(Size) / System.Convert.ToDouble(1024)) / System.Convert.ToDouble(1024);
			}
		}
		public string SizeMBString
		{
			get { return string.Format("{0:G4} MB", SizeMB); }
		}
		public string Title { get; set; }
		public void Pause()
		{
			if(!player.CanPause)
				throw new ApplicationException("Media cannot be paused at this time");
			player.Pause();
			IsPaused = true;
			PlayTimer.Stop();
			if(PlayPaused != null)
				PlayPaused(this, EventArgs.Empty);
		}
		public void Play()
		{
			if(!IsPaused)
				player.Open(new Uri(fileName, UriKind.Relative));
			player.Play();
			IsPaused = false;
			IsPlaying = true;
			PlayTimer.Start();
			if(PlayStarted != null)
				PlayStarted(this, EventArgs.Empty);
		}
		public void Stop()
		{
			if(IsPlaying)
				player.Stop();
			IsPlaying = false;
			IsPaused = false;
			PlayTimer.Stop();
			if(PlayComplete != null)
				PlayComplete(this, EventArgs.Empty);
		}
		private void player_BufferingEnded(object sender, EventArgs e)
		{
			Buffering = false;
		}
		private void player_BufferingStarted(object sender, EventArgs e)
		{
			Buffering = true;
		}
		private void player_MediaEnded(object sender, EventArgs e)
		{
			IsPlaying = false;
			IsPaused = false;
			if(PlayComplete != null)
				PlayComplete(this, EventArgs.Empty);
		}
		private void player_MediaFailed(object sender, ExceptionEventArgs e)
		{
			throw new ApplicationException("Media playback failure. See inner exception for details", e.ErrorException);
		}
		private void player_MediaOpened(object sender, EventArgs e)
		{
			if(player.NaturalDuration.HasTimeSpan)
			{
				if(DurationAvailable != null)
				{
					DurationAvailable(this, new TimeSpanAvailableEventArgs(player.NaturalDuration.TimeSpan));
					DurationSignalled = true;
				}
			}
			Opened = true;
		}
		private void PlayTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(RemainingChanged != null)
				RemainingChanged(this, EventArgs.Empty);
		}
	}
}
