namespace OzDB.Application {
	using System;

	public delegate void SettingsActionHandler(object sender, SettingsActionEventArgs e);

	public enum Actions {
		Add, Update, Delete
	}

	public class SettingsActionEventArgs : EventArgs {
		public SettingsActionEventArgs(Actions action, string applicationName, string sectionName, string keyName, object value) {
			this.Action = action;
			this.ApplicationName = applicationName;
			this.SectionName = sectionName;
			this.KeyName = keyName;
			this.Value = value;
		}

		public SettingsActionEventArgs(Actions action, string applicationName, string sectionName, string keyName) {
			this.Action = action;
			this.ApplicationName = applicationName;
			this.SectionName = sectionName;
			this.KeyName = keyName;
			this.Value = null;
		}

		public Actions Action { get; set; }

		public string ApplicationName { get; private set; }

		public string SectionName { get; private set; }

		public string KeyName { get; private set; }

		public object Value { get; private set; }
	}
}
