namespace SoundDesk.Entities
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Media;

	public delegate void DurationAvailableHandler(object sender, TimeSpanAvailableEventArgs e);
	public delegate void PositionHandler(object sender, TimeSpanAvailableEventArgs e);
	public interface IMediaFile : IPlayable
	{
		ImageSource Source { get; }
		event DurationAvailableHandler DurationAvailable;
		event EventHandler RemainingChanged;
		double AmountComplete { get; }
		string DeleteText { get; }
		string DurationString { get; }
		bool IsPaused { get; }
		bool IsPlaying { get; }
		TimeSpan? Duration { get; }
		TimeSpan? Position { get; set; }
		TimeSpan? Remaining { get; }
		int Sequence { get; set; }
		string Title { get; set; }
		long? PianoSize { get; }
		long? OrchestralSize { get; }
		double Size { get; }
		double Volume { get; set; }
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
