using System;

namespace CongregationManager.Data {
    public delegate void ChangeNotificationHandler(object sender, ChangeNotificationEventArgs e);

    public enum ModificationTypes {
        Added,
        Modified,
        Deleted
    }

    public class ChangeNotificationEventArgs : EventArgs {
        public ChangeNotificationEventArgs(ItemBase item, ModificationTypes modType) {
            Item = item;
            ModType = modType;
        }

        public ItemBase Item { get; private set; }
        public ModificationTypes ModType { get; private set; }
        public bool Cancel { get; set; }
    }
}
