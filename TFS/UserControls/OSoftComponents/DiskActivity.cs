namespace OSoftComponents
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Threading;
    using System.Linq;
    using System.Threading.Tasks;

    public delegate void ActivityHandler(object sender, ActivityEventArgs e);

    public class ActivityEventArgs : EventArgs
    {
        #region Public Constructors
        public ActivityEventArgs(double value)
        {
            Value = value;
            IsOn = value > 0.0;
        }
        #endregion Public Constructors

        #region Public Properties
        public bool IsOn { get; private set; }
        public double Value { get; private set; }
        #endregion Public Properties
    }

    public class DiskActivity : IDisposable
    {
        #region Public Constructors
        public DiskActivity()
        {
            Start();
            RecordActivity = true;
        }

        public void Start()
        {
            if (counter == null)
                counter = new PerformanceCounter("PhysicalDisk", "Disk Reads/sec", "_total", true);

            if (pTimer == null)
            {
                pTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
                pTimer.Tick += pTimer_Tick;
            }
            pTimer.Start();

            //while (true) { }
        }

        public void Stop()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() =>
            {
                ActivitySensed?.Invoke(this, new ActivityEventArgs(0.0));
            }));
            pTimer.Stop();
            pTimer.Tick -= pTimer_Tick;
        }

        public DiskActivity(bool recordActivity)
            : this()
        {
            RecordActivity = recordActivity;
        }
        public DiskActivity(string instanceName)
            : this()
        {
            SetupCounter(instanceName);
        }
        public DiskActivity(string instanceName, bool recordActivity)
            : this(recordActivity)
        {
            SetupCounter(instanceName);
        }
        public DiskActivity(DispatcherTimer timer, string instanceName)
            : this(instanceName)
        {
            timer.Tick += pTimer_Tick;
        }
        public DiskActivity(DispatcherTimer timer, string instanceName, bool recordActivity)
            : this(timer, instanceName)
        {
            RecordActivity = recordActivity;
        }
        #endregion Public Constructors

        #region Public Methods
        public void Dispose()
        {
            if (counter != null)
                counter.Dispose();
            if (pTimer != null)
                pTimer.Stop();
        }
        public void SetupCounter(string instanceName)
        {
            try
            {
                string categoryName = "PhysicalDisk";
                string counterName = "% Disk Time";
                if (string.IsNullOrEmpty(instanceName))
                {
                    var cat = new PerformanceCounterCategory(categoryName);
                    InstanceNames = cat.GetInstanceNames();
                    return;
                }
                counter = new PerformanceCounter(categoryName, counterName, instanceName, true);
                ActivityIsEnabled = true;
                if (pTimer != null)
                    pTimer.Start();
            }
            catch (System.Exception ex)
            {
                ActivityException = ex;
            }
        }
        #endregion Public Methods

        #region Private Methods
        private void pTimer_Tick(object sender, EventArgs e)
        {
            var val = counter.NextValue();

            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() =>
            {
                ActivitySensed?.Invoke(this, new ActivityEventArgs(val));
            }));

            if (RecordActivity)
            {
                var current = DateTime.Now.Subtract(sinceLastRecordWritten);
                if (current.TotalMilliseconds > 1000)
                {
                    averagedValues.Add(!intermediateValues.Any() ? 0.0 : intermediateValues.Average());
                    intermediateValues.Clear();
                    sinceLastRecordWritten = DateTime.Now;
                }
                else
                    intermediateValues.Add(val);
            }

        }
        #endregion Private Methods

        #region Public Events
        public event ActivityHandler ActivitySensed;
        #endregion Public Events

        #region Public Fields
        public bool ActivityIsEnabled = false;
        #endregion Public Fields

        #region Private Fields
        #region private static
        private static DateTime sinceLastRecordWritten = DateTime.Now;
        private static List<double> intermediateValues = new List<double>();
        private static List<double> averagedValues = new List<double>();
        #endregion
        private PerformanceCounter counter = null;
        private DispatcherTimer pTimer = null;
        #endregion Private Fields

        #region Public Properties
        public Exception ActivityException { get; set; }
        public IEnumerable<string> InstanceNames { get; private set; }
        public bool RecordActivity { get; set; }
        #endregion Public Properties
    }
}
