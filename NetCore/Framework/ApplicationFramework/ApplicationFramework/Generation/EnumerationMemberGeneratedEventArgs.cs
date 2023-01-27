using System;

namespace Common.Applicationn.Generation {
    public delegate void EnumerationMemberGeneratedHandler(object sender, EnumerationMemberGeneratedEventArgs e);
    public class EnumerationMemberGeneratedEventArgs : EventArgs {
        public EnumerationMemberGeneratedEventArgs(string enumName, string itemName, object itemValue) {
            EnumName = enumName;
            ItemName = itemName;
            ItemValue = itemValue;
        }

        public string EnumName { get; private set; } = default;

        public string ItemName { get; private set; } = default;

        public object ItemValue { get; private set; } = default;
    }
}
