/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OzFramework.Timers {
    public static class Extensions {

        public static DispatcherTimer GetTimer(this TimeSpan value, bool isStarted, bool autostop) {
            var result = new DispatcherTimer() { Interval = value };
            if (isStarted) {
                result.Start();
            }
            if (autostop) {
                result.Tick += (s, e) => {
                    s.As<DispatcherTimer>().Stop();
                };
            }
            return result;
        }

        public static DispatcherTimer GetTimer(this double milliseconds, bool isStarted, bool autostop) => 
            TimeSpan.FromMilliseconds(milliseconds).GetTimer(isStarted, autostop);
        public static DispatcherTimer GetAutoStartStopTimer(this TimeSpan value) => GetTimer(value, true, true);
        public static DispatcherTimer GetAutoStartStopTimer(this double milliseconds) => 
            TimeSpan.FromMilliseconds(milliseconds).GetTimer(true, true);
        public static DispatcherTimer GetAutoStartTimer(this TimeSpan value) => GetTimer(value, true, false);
        public static DispatcherTimer GetAutoStartTimer(this double milliseconds) => 
            TimeSpan.FromMilliseconds(milliseconds).GetTimer(true, false);
    }
}
