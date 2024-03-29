using System;
using System.Linq;
namespace SharpSounder.Recording
{
	public interface IRecordable
	{
		event EventHandler LengthUpdated;
		event EventHandler RecordingComplete;
		event EventHandler RecordingPaused;
		event EventHandler RecordingStarted;
		string FileName { get; set; }
		bool IsComplete { get; }
		bool IsPaused { get; }
		bool IsRecording { get; }
		TimeSpan Length { get; }
		void Pause();
		void Start();
		void Stop();
	}
}
