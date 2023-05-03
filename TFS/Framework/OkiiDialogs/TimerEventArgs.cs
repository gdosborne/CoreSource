namespace GregOsborne.Dialogs {
	using System;

	public class TimerEventArgs : EventArgs {
		public TimerEventArgs(int tickCount) => this.TickCount = tickCount;

		public bool ResetTickCount { get; set; }

		public int TickCount { get; }
	}
}