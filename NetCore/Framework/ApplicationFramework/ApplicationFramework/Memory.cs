using Common.AppFramework.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Common.AppFramework {
    public class Memory {
        public Dictionary<DateTime, long> Log { get; } = default;

        private readonly Timer reportingTimer = default;
        private bool isReporting = false;
        private readonly WriteStatusHandler writeHandler = default;

        public Memory(TimeSpan interval, bool startReporting, WriteStatusHandler writeHandler) {
            Log = new Dictionary<DateTime, long>();
            Initial = System.Diagnostics.Process.GetCurrentProcess().UsedMemory();
            Log.Add(DateTime.Now, Initial);
            reportingTimer = new Timer(TimerCallBack, null, 0, interval.Milliseconds);
            isReporting = startReporting;
            this.writeHandler = writeHandler;
        }

        ~Memory() {
            reportingTimer.Dispose();
        }

        private void TimerCallBack(object stateInfo) {
            if (isReporting) {
                writeHandler?.Invoke(this, new WriteStatusEventArgs($"{ByteSize.FromBytes(Convert.ToDouble(Current)).MegaBytes.ToString($"#,0.0 {ByteSize.MegaByteSymbol} memory used")}"));
            }
        }

        public long Initial { get; private set; }

        public long Current {
            get {
                var result = System.Diagnostics.Process.GetCurrentProcess().UsedMemory();
                Log.Add(DateTime.Now, result);
                return result;
            }
        }

        public Memory StartReporting() {
            isReporting = true;
            return this;
        }

        public Memory StopReporting() {
            isReporting = false;
            return this;
        }

        public Memory Reset() {
            Log.Clear();
            return this;
        }

        public long FromLog(DateTime targetDate) => Log[new DateTime(Log.Select(x => x.Key).Min(date => Math.Abs((date - targetDate).Ticks)))];
    }

}
