namespace GregOsborne.Application {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using GregOsborne.Application.Process;

	public class Memory {
		public Dictionary<DateTime, long> Log { get; } = default;

		private readonly Timer reportingTimer = default;
		private bool isReporting = false;
		private readonly WriteStatusHandler writeHandler = default;

		public Memory(TimeSpan interval, bool startReporting, WriteStatusHandler writeHandler) {
			this.Log = new Dictionary<DateTime, long>();
			this.Initial = System.Diagnostics.Process.GetCurrentProcess().UsedMemory();
			this.Log.Add(DateTime.Now, this.Initial);
			this.reportingTimer = new Timer(this.TimerCallBack, null, 0, interval.Milliseconds);
			this.isReporting = startReporting;
			this.writeHandler = writeHandler;
		}

		~Memory() {
			this.reportingTimer.Dispose();
		}

		private void TimerCallBack(object stateInfo) {
			if (this.isReporting) {
				this.writeHandler?.Invoke(this, new WriteStatusEventArgs($"{ByteSize.FromBytes(Convert.ToDouble(this.Current)).MegaBytes.ToString($"#,0.0 {ByteSize.MegaByteSymbol} memory used")}"));
			}
		}

		public long Initial { get; private set; }

		public long Current {
			get {
				var result = System.Diagnostics.Process.GetCurrentProcess().UsedMemory();
				this.Log.Add(DateTime.Now, result);
				return result;
			}
		}

		public Memory StartReporting() {
			this.isReporting = true;
			return this;
		}

		public Memory StopReporting() {
			this.isReporting = false;
			return this;
		}

		public Memory Reset() {
			this.Log.Clear();
			return this;
		}

		public long FromLog(DateTime targetDate) => this.Log[new DateTime(this.Log.Select(x => x.Key).Min(date => Math.Abs((date - targetDate).Ticks)))];
	}

}
