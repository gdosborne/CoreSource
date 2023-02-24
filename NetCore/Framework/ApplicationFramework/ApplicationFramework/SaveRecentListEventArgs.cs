using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application {
    public delegate void SavedRecentListHandler(object sender, SaveRecentListEventArgs e);
    public class SaveRecentListEventArgs {
        public SaveRecentListEventArgs(string[] orderedList) {
            OrderedList = orderedList;
        }

        public string[] OrderedList { get; private set; } = null;

    }
}
