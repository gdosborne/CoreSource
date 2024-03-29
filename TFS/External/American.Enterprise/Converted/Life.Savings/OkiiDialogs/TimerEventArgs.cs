using System;

namespace Ookii.Dialogs.Wpf {
    public class TimerEventArgs : EventArgs {
        public TimerEventArgs(int tickCount) {
            TickCount = tickCount;
        }

        public bool ResetTickCount { get; set; }

        public int TickCount { get; }
    }
}