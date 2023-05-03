namespace AppSystem.ApplicationHelpers {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class SettingsSection {
        public SettingsSection(string sectionName) {
            this.Name = sectionName;
            this.Values = new List<SettingsKey>();
        }

        public string Name { get; private set; } = null;

        public List<SettingsKey> Values { get; private set; } = null;

        public void Save() {
            var sectionFile = Settings.GetStorageFile(this.Name);
            var data = new StringBuilder();
            data.AppendLine($"{Settings.CommentIdentifier}updated {DateTime.Now}");
            this.Values.ForEach(x => {
                data.Append(x.PersistValue(Settings.BracketLeft, Settings.BracketRight, Settings.TypeLeft, Settings.TypeRight));
                data.AppendLine();
            });
            using (var writer = new StreamWriter(sectionFile.OpenStreamForWriteAsync().Result)) {
                writer.Write(data.ToString());
            }
        }
    }
}
