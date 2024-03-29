using System;
using System.Collections.ObjectModel;
using System.Linq;
namespace SharpSounder.Media
{
	public class Playlist : IPlayable
	{
		public event EventHandler PlayComplete;
		public event EventHandler PlayPaused;
		public event EventHandler PlayStarted;
		public IMediaFile MediaFile { get; private set; }
		public int CurrentIndex { get; private set; }
		public ObservableCollection<IMediaFile> Items { get; private set; }
		public bool RemoveMediaAtEndOfPlayback { get; set; }
		public bool StopAtEndOfPlayback { get; set; }
		public Playlist()
		{
			CurrentIndex = 0;
			Items = new ObservableCollection<IMediaFile>();
		}
		public void Pause()
		{
			if(MediaFile == null) return;
			MediaFile.Pause();
		}
		public void Play()
		{
			if(Items.Count == 0) return;
			if(MediaFile != null && MediaFile.IsPaused)
				MediaFile.Play();
			else
			{
				if(MediaFile == null)
					MediaFile = Items[0];
				MediaFile.PlayComplete += new EventHandler(mediaFile_PlayComplete);
				MediaFile.PlayPaused += new EventHandler(mediaFile_PlayPaused);
				MediaFile.PlayStarted += new EventHandler(mediaFile_PlayStarted);
				MediaFile.Play();
			}
		}
		public void RemoveCurrent()
		{
			if(MediaFile == null) return;
			if(!Items.Contains(MediaFile)) return;
			MediaFile.Stop();
			Items.Remove(MediaFile);
		}
		public void Stop()
		{
			if(MediaFile == null) return;
			MediaFile.Stop();
			MediaFile.PlayComplete -= mediaFile_PlayComplete;
			MediaFile.PlayPaused -= mediaFile_PlayPaused;
			MediaFile.PlayStarted -= mediaFile_PlayStarted;
		}
		private void ProcessComplete()
		{
			if(RemoveMediaAtEndOfPlayback)
				RemoveCurrent();
			else
				CurrentIndex++;
			if(!StopAtEndOfPlayback)
			{
				if(Items.Count > CurrentIndex)
				{
					MediaFile = Items[CurrentIndex];
					Play();
				}
			}
		}
		private void mediaFile_PlayComplete(object sender, EventArgs e)
		{
			if(PlayComplete != null)
				PlayComplete(this, EventArgs.Empty);
			
			ProcessComplete();
		}
		private void mediaFile_PlayPaused(object sender, EventArgs e)
		{
			if(PlayPaused != null)
				PlayPaused(this, EventArgs.Empty);
		}
		private void mediaFile_PlayStarted(object sender, EventArgs e)
		{
			if(PlayStarted != null)
				PlayStarted(this, EventArgs.Empty);
		}
		public void PlayNext()
		{
			if(MediaFile.IsPlaying)
				MediaFile.Stop();
			if(CurrentIndex < Items.Count - 1)
			{
				CurrentIndex++;
				MediaFile = Items[CurrentIndex];
				Play();
			}
		}
		public void PlayFirst()
		{
		}
		public void PlayPrevious()
		{
		}
		public void PlayLast()
		{
		}
	}
}
