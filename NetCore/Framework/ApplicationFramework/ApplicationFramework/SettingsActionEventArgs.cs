using System;

namespace Common.Applicationn {
    public delegate void SettingsActionHandler(object sender, SettingsActionEventArgs e);

    public enum Actions {
        Add, Update, Delete
    }

    public class SettingsActionEventArgs : EventArgs {
        public SettingsActionEventArgs(Actions action, string applicationName, string sectionName, string keyName, object value) {
            Action = action;
            ApplicationName = applicationName;
            SectionName = sectionName;
            KeyName = keyName;
            Value = value;
        }

        public SettingsActionEventArgs(Actions action, string applicationName, string sectionName, string keyName) {
            Action = action;
            ApplicationName = applicationName;
            SectionName = sectionName;
            KeyName = keyName;
            Value = null;
        }

        public Actions Action { get; set; }

        public string ApplicationName { get; private set; }

        public string SectionName { get; private set; }

        public string KeyName { get; private set; }

        public object Value { get; private set; }
    }
}
