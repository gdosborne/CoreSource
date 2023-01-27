using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Extensibility {
    public delegate void RemoveControlItemHandler(object sender, RemoveControlItemEventArgs e);
    public class RemoveControlItemEventArgs : EventArgs {
        public RemoveControlItemEventArgs(object[] controls) { 
            Controls = controls;
        }

        public object[] Controls { get; private set; }
    }
}
