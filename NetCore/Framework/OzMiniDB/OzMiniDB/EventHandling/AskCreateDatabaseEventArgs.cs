using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzMiniDB.EventHandling {
    public delegate void AskCreateDatabaseHandler(object sender, AskCreateDatabaseEventArgs e);
    public class AskCreateDatabaseEventArgs : EventArgs {
        public string Filename { get; set; }
        public bool IsCreateSet { get; set; }
    }
}
