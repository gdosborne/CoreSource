namespace SoundDesk
{
	using Entities;
	//using GregOsborne.Application.Linq;
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Windows;

	public class MainWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			Left = SoundDesk.Properties.Settings.Default.Left;
			Top = SoundDesk.Properties.Settings.Default.Top;
			Width = SoundDesk.Properties.Settings.Default.Width;
			Height = SoundDesk.Properties.Settings.Default.Height;
			LeftColumnWidth = new GridLength(SoundDesk.Properties.Settings.Default.LeftColumnWidth);
			WindowState = SoundDesk.Properties.Settings.Default.WindowState;

			var songFileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Resources", "songs.xml");
			Songs = new ObservableCollection<IMediaFile>(Song.LoadSongs(songFileName));
			Songs.ToList().ForEach(x =>
			{
				x.As<Song>().IsOrchestralPreferred = SoundDesk.Properties.Settings.Default.IsOrchestralSongsPreferred;
				x.Initialize();
			});

			MaxSongNumber = Songs.Cast<Song>().Max(x => x.Number);
			SettingsVisibility = Visibility.Collapsed;
			RandomSongsVisibility = Visibility.Collapsed;
			SkipNextVisibility = Visibility.Collapsed;
			IsOrchestralSongsPreferred = SoundDesk.Properties.Settings.Default.IsOrchestralSongsPreferred;
			IsSongRemovedWhenFinished = SoundDesk.Properties.Settings.Default.IsSongRemovedWhenFinished;
			IsPlayVocalVersion = SoundDesk.Properties.Settings.Default.IsPlayVocalVersion;
			IsCheckForUpdatesOnStart = SoundDesk.Properties.Settings.Default.IsCheckForUpdatesOnStart;

			RandomText = "Pre/Post Meeting";
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(Window window)
		{
			window.Left = Left;
			window.Top = Top;
			window.Width = Width;
			window.Height = Height;
			window.WindowState = WindowState;
		}
		public void InitView()
		{
		}
		public void Persist(Window window)
		{
			Left = window.RestoreBounds.Left;
			Top = window.RestoreBounds.Top;
			Width = window.RestoreBounds.Width;
			Height = window.RestoreBounds.Height;
			WindowState = window.WindowState;
			SaveSettings();
		}
		public void UpdateInterface()
		{
		}
		#endregion Public Methods

		#region Private Methods
		private void CurrentFile_DurationAvailable(object sender, TimeSpanAvailableEventArgs e)
		{
			Duration = e.Duration;
			PosMax = Duration.TotalSeconds;
		}
		private void CurrentFile_PlayComplete(object sender, EventArgs e)
		{
			SelectNextRandomSong();
		}
		private void CurrentFile_PlayPaused(object sender, EventArgs e)
		{
		}
		private void CurrentFile_PlayStarted(object sender, EventArgs e)
		{
		}
		private void CurrentFile_RemainingChanged(object sender, EventArgs e)
		{
			Remaining = sender.As<Song>().Remaining.Value;
			Position = Duration.Subtract(Remaining);
			Pos = Position.TotalSeconds;
		}
		private void RandomSongs(object state)
		{
			SettingsVisibility = Visibility.Collapsed;
			if (!IsPlayingRandomSongs)
			{
				if (Volume == 0)
					Volume = 0.5;
				RandomSongsVisibility = Visibility.Visible;
				RandomText = "Stop Playback";
			}
			else
			{
				RandomSongsVisibility = Visibility.Collapsed;
				RandomText = "Pre/Post Meeting";
				RandomMediaFiles = null;
				Songs.Cast<Song>().ToList().ForEach(x => x.IsVocalPreferred = false);
			}
			IsPlayingRandomSongs = !IsPlayingRandomSongs;
			if (IsPlayingRandomSongs)
			{
				SelectNextRandomSong();
				SkipNextVisibility = Visibility.Visible;
			}
			else if (CurrentFile != null)
			{
				CurrentFile.Stop();
				CurrentFile = null;
			}
		}
		private void SaveSettings()
		{
			SoundDesk.Properties.Settings.Default.Left = Left;
			SoundDesk.Properties.Settings.Default.Top = Top;
			SoundDesk.Properties.Settings.Default.Width = Width;
			SoundDesk.Properties.Settings.Default.Height = Height;
			SoundDesk.Properties.Settings.Default.LeftColumnWidth = LeftColumnWidth.Value;
			SoundDesk.Properties.Settings.Default.WindowState = WindowState;
			SoundDesk.Properties.Settings.Default.Save();
			Songs.Cast<Song>().ToList().ForEach(x => x.IsOrchestralPreferred = SoundDesk.Properties.Settings.Default.IsOrchestralSongsPreferred);
		}
		private void SelectNextRandomSong()
		{
			if (!IsPlayingRandomSongs)
				return;

			Songs.Cast<Song>().ToList().ForEach(x => x.IsVocalPreferred = SoundDesk.Properties.Settings.Default.IsPlayVocalVersion);
			//if (RandomMediaFiles == null || !RandomMediaFiles.Any())
			//	RandomMediaFiles = Songs.Randomize(new Random()).ToList();
			CurrentFile = RandomMediaFiles.First();
			RandomMediaFiles.Remove(CurrentFile);
			CurrentFile.As<Song>().Play(true);
			CurrentFile.Volume = Volume;
		}
		private void SettingsCancel(object state)
		{
			IsOrchestralSongsPreferred = SoundDesk.Properties.Settings.Default.IsOrchestralSongsPreferred;
			IsSongRemovedWhenFinished = SoundDesk.Properties.Settings.Default.IsSongRemovedWhenFinished;
			SettingsVisibility = Visibility.Collapsed;
		}
		private void SettingsOK(object state)
		{
			SaveSettings();
			SettingsVisibility = Visibility.Collapsed;
		}
		private void SkipSong(object state)
		{
			if (CurrentFile != null)
				CurrentFile.Stop();
		}
		private bool ValidateRandomSongsState(object state)
		{
			return true;
		}
		private bool ValidateSettingsCancelState(object state)
		{
			return true;
		}
		private bool ValidateSettingsOKState(object state)
		{
			return true;
		}
		private bool ValidateSkipSongState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IMediaFile _CurrentFile;
		private bool? _DialogResult;
		private TimeSpan _Duration;
		private double _Height;
		private bool _IsCheckForUpdatesOnStart;
		private bool _IsOrchestralSongsPreferred;
		private bool _IsPlayingRandomSongs;
		private bool _IsSongRemovedWhenFinished;
		private double _Left;
		private GridLength _LeftColumnWidth;
		private bool _PlayVocalVersion;
		private double _Pos;
		private TimeSpan _Position;
		private double _PosMax;
		private DelegateCommand _RandomSongsCommand = null;
		private Visibility _RandomSongsVisibility;
		private string _RandomText;
		private TimeSpan _Remaining;
		private DelegateCommand _SettingsCancelCommand = null;
		private DelegateCommand _SettingsOKCommand = null;
		private Visibility _SettingsVisibility;
		private Visibility _SkipNextVisibility;
		private DelegateCommand _SkipSongCommand = null;
		private int? _Song1;
		private IMediaFile _Song1Item;
		private int? _Song2;
		private IMediaFile _Song2Item;
		private int? _Song3;
		private IMediaFile _Song3Item;
		private ObservableCollection<IMediaFile> _Songs;
		private double _Top;
		private double _Volume;
		private double _VolumeTickFrequency;
		private double _Width;
		private WindowState _WindowState;
		private Random SongRandomizer = null;
		#endregion Private Fields

		#region Public Properties
		public IMediaFile CurrentFile
		{
			get
			{
				return _CurrentFile;
			}
			set
			{
				if (_CurrentFile != null)
				{
					_CurrentFile.DurationAvailable -= CurrentFile_DurationAvailable;
					_CurrentFile.PlayComplete -= CurrentFile_PlayComplete;
					_CurrentFile.PlayPaused -= CurrentFile_PlayPaused;
					_CurrentFile.PlayStarted -= CurrentFile_PlayStarted;
					_CurrentFile.RemainingChanged -= CurrentFile_RemainingChanged;
				}
				_CurrentFile = value;
				if (value != null)
				{
					value.DurationAvailable += CurrentFile_DurationAvailable;
					value.PlayComplete += CurrentFile_PlayComplete;
					value.PlayPaused += CurrentFile_PlayPaused;
					value.PlayStarted += CurrentFile_PlayStarted;
					value.RemainingChanged += CurrentFile_RemainingChanged;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public TimeSpan Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double Height
		{
			get
			{
				return _Height;
			}
			set
			{
				_Height = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsCheckForUpdatesOnStart
		{
			get
			{
				return _IsCheckForUpdatesOnStart;
			}
			set
			{
				_IsCheckForUpdatesOnStart = value;
				SoundDesk.Properties.Settings.Default.IsCheckForUpdatesOnStart = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsOrchestralSongsPreferred
		{
			get
			{
				return _IsOrchestralSongsPreferred;
			}
			set
			{
				_IsOrchestralSongsPreferred = value;
				SoundDesk.Properties.Settings.Default.IsOrchestralSongsPreferred = IsOrchestralSongsPreferred;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsPlayingRandomSongs
		{
			get
			{
				return _IsPlayingRandomSongs;
			}
			set
			{
				_IsPlayingRandomSongs = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private int _MaxSongNumber;
		public int MaxSongNumber
		{
			get { return _MaxSongNumber; }
			set
			{
				_MaxSongNumber = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsPlayVocalVersion
		{
			get
			{
				return _PlayVocalVersion;
			}
			set
			{
				_PlayVocalVersion = value;
				SoundDesk.Properties.Settings.Default.IsPlayVocalVersion = IsPlayVocalVersion;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsSongRemovedWhenFinished
		{
			get
			{
				return _IsSongRemovedWhenFinished;
			}
			set
			{
				_IsSongRemovedWhenFinished = value;
				SoundDesk.Properties.Settings.Default.IsSongRemovedWhenFinished = IsSongRemovedWhenFinished;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double Left
		{
			get
			{
				return _Left;
			}
			set
			{
				_Left = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public GridLength LeftColumnWidth
		{
			get
			{
				return _LeftColumnWidth;
			}
			set
			{
				_LeftColumnWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double Pos
		{
			get
			{
				return _Pos;
			}
			set
			{
				_Pos = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public TimeSpan Position
		{
			get
			{
				return _Position;
			}
			set
			{
				_Position = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double PosMax
		{
			get
			{
				return _PosMax;
			}
			set
			{
				_PosMax = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand RandomSongsCommand
		{
			get
			{
				if (_RandomSongsCommand == null)
					_RandomSongsCommand = new DelegateCommand(RandomSongs, ValidateRandomSongsState);
				return _RandomSongsCommand as DelegateCommand;
			}
		}
		public Visibility RandomSongsVisibility
		{
			get
			{
				return _RandomSongsVisibility;
			}
			set
			{
				_RandomSongsVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string RandomText
		{
			get
			{
				return _RandomText;
			}
			set
			{
				_RandomText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public TimeSpan Remaining
		{
			get
			{
				return _Remaining;
			}
			set
			{
				_Remaining = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand SettingsCancelCommand
		{
			get
			{
				if (_SettingsCancelCommand == null)
					_SettingsCancelCommand = new DelegateCommand(SettingsCancel, ValidateSettingsCancelState);
				return _SettingsCancelCommand as DelegateCommand;
			}
		}
		public DelegateCommand SettingsOKCommand
		{
			get
			{
				if (_SettingsOKCommand == null)
					_SettingsOKCommand = new DelegateCommand(SettingsOK, ValidateSettingsOKState);
				return _SettingsOKCommand as DelegateCommand;
			}
		}
		public Visibility SettingsVisibility
		{
			get
			{
				return _SettingsVisibility;
			}
			set
			{
				_SettingsVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility SkipNextVisibility
		{
			get
			{
				return _SkipNextVisibility;
			}
			set
			{
				_SkipNextVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public DelegateCommand SkipSongCommand
		{
			get
			{
				if (_SkipSongCommand == null)
					_SkipSongCommand = new DelegateCommand(SkipSong, ValidateSkipSongState);
				return _SkipSongCommand as DelegateCommand;
			}
		}
		public int? Song1
		{
			get
			{
				return _Song1;
			}
			set
			{
				_Song1 = value;
				var song = Songs.FirstOrDefault(x => x.As<Song>().Number == value);
				if (song == null)
				{
					_Song1 = null;
					Song1Item = null;
					return;
				}
				Song1Item = song;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public IMediaFile Song1Item
		{
			get
			{
				return _Song1Item;
			}
			set
			{
				_Song1Item = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public int? Song2
		{
			get
			{
				return _Song2;
			}
			set
			{
				_Song2 = value;
				var song = Songs.FirstOrDefault(x => x.As<Song>().Number == value);
				if (song == null)
				{
					_Song2 = null;
					Song2Item = null;
					return;
				}
				Song2Item = song;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public IMediaFile Song2Item
		{
			get
			{
				return _Song2Item;
			}
			set
			{
				_Song2Item = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public int? Song3
		{
			get
			{
				return _Song3;
			}
			set
			{
				_Song3 = value;
				var song = Songs.FirstOrDefault(x => x.As<Song>().Number == value);
				if (song == null)
				{
					_Song3 = null;
					Song3Item = null;
					return;
				}
				Song3Item = song;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public IMediaFile Song3Item
		{
			get
			{
				return _Song3Item;
			}
			set
			{
				_Song3Item = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<IMediaFile> Songs
		{
			get
			{
				return _Songs;
			}
			set
			{
				_Songs = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Songs"));
			}
		}
		public double Top
		{
			get
			{
				return _Top;
			}
			set
			{
				_Top = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				_Volume = value;
				if (CurrentFile != null)
					CurrentFile.Volume = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double VolumeTickFrequency
		{
			get
			{
				return _VolumeTickFrequency;
			}
			set
			{
				_VolumeTickFrequency = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double Width
		{
			get
			{
				return _Width;
			}
			set
			{
				_Width = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public WindowState WindowState
		{
			get
			{
				return _WindowState;
			}
			set
			{
				_WindowState = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties

		#region Private Properties
		private List<IMediaFile> RandomMediaFiles { get; set; }
		#endregion Private Properties
	}
}
