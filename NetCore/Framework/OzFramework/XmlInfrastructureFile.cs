/* File="XmlInfrastructureFile"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System.IO;

namespace OzFramework {
    public abstract class XmlInfrastructureFile {
        public enum FileTypes {
            Settings,
            Logs
        }

        public string ApplicationName {
            get; protected set;
        }

        public string ActualFileName {
            get; protected set;
        }

        public bool SettingsFileExists => !ActualFileName.IsNull() && File.Exists(ActualFileName);

        protected static bool DoesFileNameExist(string settingsDirectory, FileTypes type) => File.Exists(GetFileName(settingsDirectory, type));

        protected static string GetFileName(string settingsDirectory, FileTypes type) {
            if (!Directory.Exists(settingsDirectory)) {
                Directory.CreateDirectory(settingsDirectory);
            }

            return Path.Combine(settingsDirectory, $"{type}.xml");
        }
    }
}
