namespace SoundDesk.Entities
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Threading;
	using System.Xml.Linq;

	public class Song : IMediaFile, INotifyPropertyChanged
	{
		#region Public Constructors
		public Song()
		{
		}
		#endregion Public Constructors

		#region Public Methods
		public static Song FromXElement(XElement element, string pianoPath, string orchestraPath, string newPath, string vocalPath, string pianoFormat, string orchestralFormat, string newFormat, string vocalFormat)
		{
			var number = int.Parse(element.Attribute("number").Value);
			var title = element.Attribute("title").Value;
			var result = new Song
			{
				Number = number,
				Title = title,
				OrchestraFileName = System.IO.Path.Combine(orchestraPath, string.Format(orchestralFormat, number.ToString("000"))),
				PianoFileName = System.IO.Path.Combine(pianoPath, string.Format(pianoFormat, number.ToString("000"))),
				NewFileName = System.IO.Path.Combine(newPath, string.Format(newFormat, number.ToString("000"))),
				VocalFileName = System.IO.Path.Combine(vocalPath, string.Format(vocalFormat, number.ToString("000")))
			};
			result.IsOrchestra = System.IO.File.Exists(result.OrchestraFileName);
			result.IsPiano = System.IO.File.Exists(result.PianoFileName);
			result.IsNew = System.IO.File.Exists(result.NewFileName);
			result.IsVocal = System.IO.File.Exists(result.VocalFileName);
			result.OrchestralSize = GregOsborne.Application.IO.File.Size(result.OrchestraFileName);
			result.PianoSize = GregOsborne.Application.IO.File.Size(result.PianoFileName);
			result.NewSize = GregOsborne.Application.IO.File.Size(result.NewFileName);
			result.VocalSize = GregOsborne.Application.IO.File.Size(result.VocalFileName);
			//result.ImageSource = result.IsOrchestra ?
			//	App.Current.Resources["BassImage"].As<BitmapImage>() :
			//	result.IsPiano ?
			//		App.Current.Resources["PianoImage"].As<BitmapImage>() :
			//		App.Current.Resources["NewImage"].As<BitmapImage>();
			return result;
		}
		public static IList<Song> LoadSongs(string songFileName)
		{
			var data = string.Empty;
			var doc = XDocument.Load(songFileName);
			var result = new List<Song>();
			var pianoFolder = doc.Root.Element("Settings").Element("pianofolder").Value;
			var orchestralFolder = doc.Root.Element("Settings").Element("orchestralfolder").Value;
			var newFolder = doc.Root.Element("Settings").Element("newfolder").Value;
			var vocalFolder = doc.Root.Element("Settings").Element("vocalfolder").Value;
			var musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

			if (pianoFolder.StartsWith("%Music%"))
				pianoFolder = pianoFolder.Replace("%Music%", musicFolder);
			if (orchestralFolder.StartsWith("%Music%"))
				orchestralFolder = orchestralFolder.Replace("%Music%", musicFolder);
			if (newFolder.StartsWith("%Music%"))
				newFolder = newFolder.Replace("%Music%", musicFolder);
			if (vocalFolder.StartsWith("%Music%"))
				vocalFolder = vocalFolder.Replace("%Music%", musicFolder);

			var pianoFormat = doc.Root.Element("Settings").Element("pianofileformat").Value;
			var orchestralFormat = doc.Root.Element("Settings").Element("orchestralfileformat").Value;
			var newFormat = doc.Root.Element("Settings").Element("newfileformat").Value;
			var vocalFormat = doc.Root.Element("Settings").Element("vocalfileformat").Value;

			doc.Root.Elements()
				.Where(x => x.Name.LocalName.Equals("Song"))
				.OrderBy(x => int.Parse(x.Attribute("number").Value))
				.ToList()
				.ForEach(x => result.Add(Song.FromXElement(x, pianoFolder, orchestralFolder, newFolder, vocalFolder, pianoFormat, orchestralFormat, newFormat, vocalFormat)));
			return result;
		}
		public void Initialize()
		{
		}
		public void Pause()
		{
			if (!player.CanPause)
				throw new ApplicationException("Media cannot be paused at this time");
			player.Pause();
			IsPaused = true;
			PlayTimer.Stop();
			if (PlayPaused != null)
				PlayPaused(this, EventArgs.Empty);
		}
		public void Play()
		{
			Play(false);
		}
		public void Play(bool isPlayingRandom)
		{
			if (player != null)
			{
				player.BufferingStarted -= player_BufferingStarted;
				player.BufferingEnded -= player_BufferingEnded;
				player.MediaOpened -= player_MediaOpened;
				player.MediaEnded -= player_MediaEnded;
				player.MediaFailed -= player_MediaFailed;
				player = null;
			}
			player = new MediaPlayer();
			player.BufferingStarted += player_BufferingStarted;
			player.BufferingEnded += player_BufferingEnded;
			player.MediaOpened += player_MediaOpened;
			player.MediaEnded += player_MediaEnded;
			player.MediaFailed += player_MediaFailed;

			if (!IsPlaying && IsPaused && player != null)
				player.Play();
			else if (IsOrchestra || IsPiano || IsNew || IsVocal)
			{
				string fileName = SelectFileToPlay(isPlayingRandom);
				player.Open(new Uri(fileName, UriKind.Relative));
				player.Play();
				IsPaused = false;
				IsPlaying = true;
				if (PlayTimer == null)
				{
					PlayTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
					PlayTimer.Tick += PlayTimer_Tick;
				}
				PlayTimer.Start();
				if (PlayStarted != null)
					PlayStarted(this, EventArgs.Empty);
			}
		}

		private string SelectFileToPlay(bool isPlayingRandom)
		{
			if (IsVocalPreferred && IsVocal && isPlayingRandom)
				return VocalFileName;
			else if (IsNew)
				return NewFileName;
			else if (IsOrchestralPreferred && IsOrchestra)
				return OrchestraFileName;
			else
				return PianoFileName;
		}

		public void Stop()
		{
			IsPlaying = false;
			IsPaused = false;
			if (PlayTimer != null)
				PlayTimer.Stop();
			if (player != null)
			{
				player.Stop();
				player.BufferingStarted -= player_BufferingStarted;
				player.BufferingEnded -= player_BufferingEnded;
				player.MediaOpened -= player_MediaOpened;
				player.MediaEnded -= player_MediaEnded;
				player.MediaFailed -= player_MediaFailed;
				player = null;
			}
			if (PlayComplete != null)
				PlayComplete(this, EventArgs.Empty);
		}
		#endregion Public Methods

		#region Private Methods
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
			if (player != null)
			{
				player.BufferingStarted -= player_BufferingStarted;
				player.BufferingEnded -= player_BufferingEnded;
				player.MediaOpened -= player_MediaOpened;
				player.MediaEnded -= player_MediaEnded;
				player.MediaFailed -= player_MediaFailed;
				player = null;
			}
			IsPlaying = false;
			IsPaused = false;
			if (PlayComplete != null)
				PlayComplete(this, EventArgs.Empty);
		}
		private void player_MediaFailed(object sender, ExceptionEventArgs e)
		{
			throw new ApplicationException("Media playback failure. See inner exception for details", e.ErrorException);
		}
		private void player_MediaOpened(object sender, EventArgs e)
		{
			if (player != null && player.NaturalDuration.HasTimeSpan)
			{
				if (DurationAvailable != null)
					DurationAvailable(this, new TimeSpanAvailableEventArgs(player.NaturalDuration.TimeSpan));
				DurationSignalled = true;
			}
			Opened = true;
		}
		private void PlayTimer_Tick(object sender, EventArgs e)
		{
			if (RemainingChanged != null)
				RemainingChanged(this, EventArgs.Empty);
		}
		#endregion Private Methods

		#region Public Events
		public event DurationAvailableHandler DurationAvailable;
		public event EventHandler PlayComplete;
		public event EventHandler PlayPaused;
		public event EventHandler PlayStarted;
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler RemainingChanged;
		#endregion Public Events

		#region Private Fields
		private static readonly double mb = (1024 * 1024);
		private bool DurationSignalled = false;
		private MediaPlayer player = null;
		private DispatcherTimer PlayTimer = null;
		#endregion Private Fields

		#region Public Enums
		public enum PlayTypes { Piano, Orchestral }
		#endregion Public Enums

		#region Public Properties
		public double AmountComplete
		{
			get
			{
				if (player == null || !IsPlaying || !Duration.HasValue || !Position.HasValue) return 0;
				return Position.Value.TotalSeconds / Duration.Value.TotalSeconds;
			}
		}
		public bool Buffering { get; private set; }
		public string DeleteText { get { return "Remove"; } }
		public TimeSpan? Duration
		{
			get
			{
				if (player.NaturalDuration.HasTimeSpan)
					return player.NaturalDuration.TimeSpan;
				else
					return null;
			}
		}
		public string DurationString { get; private set; }
		//public ImageSource ImageSource { get; set; }
		public bool IsNew { get; set; }
		public bool IsOrchestra { get; set; }
		public bool IsOrchestralPreferred { get; set; }
		public bool IsVocalPreferred { get; set; }
		public bool IsPaused { get; private set; }
		public bool IsPiano { get; set; }
		public bool IsPlaying { get; private set; }
		public bool IsVocal { get; set; }
		public string NewFileName { get; set; }
		public long? NewSize { get; set; }
		public int Number { get; set; }
		public bool Opened { get; private set; }
		public string OrchestraFileName { get; set; }
		public long? OrchestralSize { get; private set; }
		public string PianoFileName { get; set; }
		public long? PianoSize { get; private set; }
		public TimeSpan? Position
		{
			get
			{
				if (player == null || !IsPlaying) return null;
				return player.Position;
			}
			set
			{
				if (player == null || !IsPlaying || !value.HasValue) return;
				player.Position = value.Value;
			}
		}
		public TimeSpan? Remaining
		{
			get
			{
				if (player == null || !Duration.HasValue || Position == null || !Position.HasValue) return TimeSpan.FromSeconds(0);
				return Duration.Value.Subtract(Position.Value);
			}
		}
		public int Sequence { get; set; }
		public double Size { get; private set; }
		public ImageSource Source
		{
			get
			{
				if (IsVocal)
					return App.Current.Resources["VocalImage"].As<BitmapImage>();
				else if (IsOrchestra || IsNew)
					return App.Current.Resources["BassImage"].As<BitmapImage>();
				else
					return App.Current.Resources["PianoImage"].As<BitmapImage>();
			}
		}
		public string Title { get; set; }
		public string VocalFileName { get; set; }
		public long? VocalSize { get; private set; }
		public double Volume
		{
			get { return player == null ? 0.5 : player.Volume; }
			set { if (player != null) player.Volume = value; }
		}
		#endregion Public Properties
	}
}
