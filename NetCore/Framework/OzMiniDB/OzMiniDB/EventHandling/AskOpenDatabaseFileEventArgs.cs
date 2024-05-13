using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzMiniDB.EventHandling {
    public delegate void AskOpenDatabaseFileEventHandler(object sender, AskOpenDatabaseFileEventArgs e);
    public class AskOpenDatabaseFileEventArgs : EventArgs {
        public string Filename { get; set; }
        public bool IsCancel { get; set; }
    }
}
