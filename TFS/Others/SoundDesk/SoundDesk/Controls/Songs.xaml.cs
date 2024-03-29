namespace SoundDesk.Controls
{
	using SoundDesk.Entities;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;
	using System.Windows.Media.Imaging;
	using System.Windows.Media;

	public partial class Songs : UserControl
	{
		#region Public Constructors
		public Songs()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onSong1Changed(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Songs)source;
			if (src == null)
				return;
			var value = (int?)e.NewValue;
			var song = src.SongList.FirstOrDefault(x => x.As<Song>().Number == value);
			if (song != null)
			{
				src.Song1Number.Content = song.As<Song>().Number.ToString();
				src.Song1Title.Content = song.As<Song>().Title;
				src.Song1Number.IsEnabled = true;
				src.Song1Title.IsEnabled = true;
				src.Song1Controller.IsEnabled = true;
				src.Song1Sequence.IsEnabled = true;
				src.Song1Deleter.IsEnabled = true;
			}
		}
		private static void onSong2Changed(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Songs)source;
			if (src == null)
				return;
			var value = (int?)e.NewValue;
			var song = src.SongList.FirstOrDefault(x => x.As<Song>().Number == value);
			if (song != null)
			{
				src.Song2Number.Content = song.As<Song>().Number.ToString();
				src.Song2Title.Content = song.As<Song>().Title;
				src.Song2Number.IsEnabled = true;
				src.Song2Title.IsEnabled = true;
				src.Song2Controller.IsEnabled = true;
				src.Song2Sequence.IsEnabled = true;
				src.Song2Deleter.IsEnabled = true;
			}
		}
		private static void onSong3Changed(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Songs)source;
			if (src == null)
				return;
			var value = (int?)e.NewValue;
			var song = src.SongList.FirstOrDefault(x => x.As<Song>().Number == value);
			if (song != null)
			{
				src.Song3Number.Content = song.As<Song>().Number.ToString();
				src.Song3Title.Content = song.As<Song>().Title;
				src.Song3Number.IsEnabled = true;
				src.Song3Title.IsEnabled = true;
				src.Song3Controller.IsEnabled = true;
				src.Song3Sequence.IsEnabled = true;
				src.Song3Deleter.IsEnabled = true;
			}
		}
		private static void onSongListChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (Songs)source;
			if (src == null)
				return;
			var value = (ObservableCollection<IMediaFile>)e.NewValue;
			//implementation code goes here
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty Song1Property = DependencyProperty.Register("Song1", typeof(int?), typeof(Songs), new PropertyMetadata(null, onSong1Changed));
		public static readonly DependencyProperty Song2Property = DependencyProperty.Register("Song2", typeof(int?), typeof(Songs), new PropertyMetadata(null, onSong2Changed));
		public static readonly DependencyProperty Song3Property = DependencyProperty.Register("Song3", typeof(int?), typeof(Songs), new PropertyMetadata(null, onSong3Changed));
		public static readonly DependencyProperty SongListProperty = DependencyProperty.Register("SongList", typeof(ObservableCollection<IMediaFile>), typeof(Songs), new PropertyMetadata(null, onSongListChanged));
		#endregion Public Fields

		#region Public Properties
		public int? Song1
		{
			get { return (int?)GetValue(Song1Property); }
			set { SetValue(Song1Property, value); }
		}

		public int? Song2
		{
			get { return (int?)GetValue(Song2Property); }
			set { SetValue(Song2Property, value); }
		}
		public int? Song3
		{
			get { return (int?)GetValue(Song3Property); }
			set { SetValue(Song3Property, value); }
		}
		public ObservableCollection<IMediaFile> SongList
		{
			get { return (ObservableCollection<IMediaFile>)GetValue(SongListProperty); }
			set { SetValue(SongListProperty, value); }
		}
		#endregion Public Properties

		private void SongBorder_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			SetSongInActive(1);
			SetSongInActive(2);
			SetSongInActive(3);
			SetSongActive(sender == Song1Border ? 1 : sender == Song2Border ? 2 : 3);
		}
		private void SetBorderBackground(int number, Border bdr, bool isActive)
		{
			var backgroundName = isActive ? "ActiveSongBackground" : "InActiveSongBackground";
			var foregroundName = isActive ? "ActiveSongForeground" : "TextColor";

			bdr.Background = App.Current.Resources[backgroundName].As<Brush>();
			var b = App.Current.Resources[foregroundName].As<Brush>();
	
			switch(number)
			{
				case 1:
					Song1Sequence.Foreground = b;
					Song1Title.Foreground = b;
					Song1Number.Foreground = b;
					break;
				case 2:
					Song2Sequence.Foreground = b;
					Song2Title.Foreground = b;
					Song2Number.Foreground = b;
					break;
				case 3:
					Song3Sequence.Foreground = b;
					Song3Title.Foreground = b;
					Song3Number.Foreground = b;
					break;
			}
		}
		private void SetSongActive(int number)
		{
			Border ctrl = null;
			switch (number)
			{
				case 1:
					ctrl = Song1Border;
					break;
				case 2:
					ctrl = Song2Border;
					break;
				case 3:
					ctrl = Song3Border;
					break;
			}
			SetBorderBackground(number, ctrl, true);
		}
		private void SetSongInActive(int number)
		{
			Border ctrl = null;
			switch (number)
			{
				case 1:
					ctrl = Song1Border;
					break;
				case 2:
					ctrl = Song2Border;
					break;
				case 3:
					ctrl = Song3Border;
					break;
			}
			SetBorderBackground(number, ctrl, false);
		}
	}
}
