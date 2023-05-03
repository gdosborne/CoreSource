using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Events {
    public delegate void WarnToRefreshHandler(object sender, WarningToRefreshEventArgs e);
    public class WarningToRefreshEventArgs : EventArgs {
        public bool? Answer { get; set; }
    }
}
