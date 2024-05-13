/* File="EnumerationMemberGeneratedEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2023 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace CCC.ApplicationFramework.Generation {
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
