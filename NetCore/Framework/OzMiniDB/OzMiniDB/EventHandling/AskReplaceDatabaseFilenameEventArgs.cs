using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzMiniDB.EventHandling {
    public delegate void AskReplaceDatabaseFilenameHandler(object sender, AskReplaceDatabaseFilenameEventArgs e);
    public class AskReplaceDatabaseFilenameEventArgs : EventArgs {
        public AskReplaceDatabaseFilenameEventArgs(string filename) {
            Filename = filename;
        }

        public string Filename { get; private set; }
        public bool IsReplaceSet { get; set; }
    }
}
