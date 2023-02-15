using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    public delegate void CongregationChangedHandler(object sender, CongregationChangedEventArgs e);
    public class CongregationChangedEventArgs : EventArgs {
        public CongregationChangedEventArgs(Congregation cong) { 
            Congregation = cong;
        }

        public Congregation Congregation { get; private set; } = null;

    }
}
