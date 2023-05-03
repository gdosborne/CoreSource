namespace SoundDesk.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IPlayable
	{
		#region Public Methods
		void Pause();
		void Play();
		void Stop();
		void Initialize();
		#endregion Public Methods

		#region Public Events
		event EventHandler PlayComplete;
		event EventHandler PlayPaused;
		event EventHandler PlayStarted;
		#endregion Public Events
	}
}
