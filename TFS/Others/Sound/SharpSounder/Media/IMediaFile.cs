using System;
using System.IO;
using System.Linq;
namespace SharpSounder.Media
{
	public delegate void DurationAvailableHandler(object sender, TimeSpanAvailableEventArgs e);
	public delegate void PositionHandler(object sender, TimeSpanAvailableEventArgs e);
	public interface IMediaFile : IPlayable
	{
		event DurationAvailableHandler DurationAvailable;
		event EventHandler RemainingChanged;
		double AmountComplete { get; }
		string DeleteText { get; }
		TimeSpan? Duration { get; }
		string DurationString { get; }
		FileInfo FileInfo { get; }
		string FileName { get; set; }
		bool IsPaused { get; }
		bool IsPlaying { get; }
		TimeSpan? Position { get; set; }
		TimeSpan? Remaining { get; }
		int Sequence { get; set; }
		Int64 Size { get; }
		double SizeMB { get; }
		string SizeMBString { get; }
		string Title { get; set; }
	}
	public class TimeSpanAvailableEventArgs : EventArgs
	{
		public TimeSpanAvailableEventArgs(TimeSpan duration)
		{
			Duration = duration;
		}
		public TimeSpan Duration { get; private set; }
	}
}
