using System;
using System.Linq;
namespace SharpSounder.Media
{
	public interface IPlayable
	{
		event EventHandler PlayComplete;
		event EventHandler PlayPaused;
		event EventHandler PlayStarted;
		void Pause();
		void Play();
		void Stop();
	}
}
