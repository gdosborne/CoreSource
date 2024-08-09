/* File="SaveRecentListEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace Common {
    public delegate void SavedRecentListHandler(object sender, SaveRecentListEventArgs e);
    public class SaveRecentListEventArgs {
        public SaveRecentListEventArgs(string[] orderedList) {
            OrderedList = orderedList;
        }

        public string[] OrderedList { get; private set; } = null;

    }

}
