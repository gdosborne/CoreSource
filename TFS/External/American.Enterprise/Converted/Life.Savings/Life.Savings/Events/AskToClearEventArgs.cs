using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Events {
    public delegate void AskToClearHandler(object sender, AskToClearEventArgs e);
    public class AskToClearEventArgs : EventArgs {
        public bool? Answer { get; set; }
    }
}
