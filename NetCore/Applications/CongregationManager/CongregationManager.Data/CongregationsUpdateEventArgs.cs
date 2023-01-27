using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    
    public delegate void CongregationsUpdateHandler(object dender, CongregationsUpdateEventArgs e);

    public class CongregationsUpdateEventArgs : EventArgs {
        public CongregationsUpdateEventArgs(bool isNewItem, string filename) {
            IsNewItem = isNewItem;
            Filename = filename;
        }

        public CongregationsUpdateEventArgs(Congregation congregation) {
            Congregation = congregation;
        }

        public CongregationsUpdateEventArgs(Exception ex) {
            Exception = ex;
        }

        public bool IsNewItem { get; private set; }
        public string Filename { get; private set; }
        public Congregation Congregation { get; private set; }
        public Exception Exception { get; private set; }
    }
}
