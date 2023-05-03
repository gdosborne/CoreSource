using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GregOsborne.Application.Text;

namespace VersionMaster {
    public class VersionTransformer {
        public VersionTransformer(string assemblyInfoFileName, ProjectData project, SchemaItem schemaItem) {
            AssemblyInfoFileName = assemblyInfoFileName;
            Project = project;
            SchemaItem = schemaItem;
        }

        private string GetTempFileName() {
            var result = default(string);
            while (string.IsNullOrEmpty(result) || (string.IsNullOrEmpty(result) && File.Exists(result))) {
                result = Path.GetTempFileName();
            }
            return result;
        }

        public VersionTransformer Modify() {
            try {
                var attrs = File.GetAttributes(AssemblyInfoFileName);
                if (attrs.HasFlag(FileAttributes.ReadOnly)) {
                    attrs = attrs & ~FileAttributes.ReadOnly;
                    File.SetAttributes(AssemblyInfoFileName, attrs);
                }
            }
            catch { }

            string tempFile = default;
            var attemptedFiles = new List<string>();
            var triggers = new List<VersionTrigger>();
            Enum.GetNames(typeof(Enumerations.VersionParts)).Select(x => (Enumerations.VersionParts)Enum.Parse(typeof(Enumerations.VersionParts), x, true)).ToList().ForEach(partType => {
                triggers.Add(new VersionTrigger {
                    Part = partType,
                    TestText = partType.TriggerText(),
                    ActualText = partType.ActualText(),
                    //Version = partType.VersionFromProject(Project)
                });
            });
            using (var fileReader = new FileStream(AssemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamReader = new StreamReader(fileReader)) {
                var isChangeComplete = false;
                while (!isChangeComplete) {
                    tempFile = GetTempFileName();
                    attemptedFiles.Add(tempFile);
                    try {
                        using (var fileWriter = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                        using (var streamWriter = new StreamWriter(fileWriter)) {
                            var trigger = default(VersionTrigger);
                            var newLine = default(string);
                            while (!streamReader.EndOfStream) {
                                var line = streamReader.ReadLine();
                                trigger = triggers.FirstOrDefault(t => line.StartsWithIgnoreCase(t.TestText));
                                newLine = trigger.Output(line);
                                streamWriter.WriteLine(newLine);
                            }
                        }
                        isChangeComplete = true;
                    }
                    catch { }
                }
            }
            RemakeAssemblyInfoFile(tempFile);
            if (attemptedFiles.Any())
                attemptedFiles.ForEach(x => {
                    try {
                        if (File.Exists(x))
                            File.Delete(x);
                    }
                    catch { }
                });
            return this;
        }

        public static string[] PartNames = new string[] {
            SchemaItem.majorValue,
			SchemaItem.minorValue,
			SchemaItem.buildValue,
			SchemaItem.revisionValue
        };

        private void RemakeAssemblyInfoFile(string tempFile) {
            try {
                var dir = Path.GetDirectoryName(AssemblyInfoFileName);
                var bkUp = Path.Combine(dir, $"{Path.GetFileNameWithoutExtension(AssemblyInfoFileName)}.backup");
                File.Move(AssemblyInfoFileName, bkUp);
                File.Move(tempFile, AssemblyInfoFileName);
                File.Delete(bkUp);
            }
            catch (Exception) { throw; }
        }

        public string AssemblyInfoFileName { get; private set; }
        public ProjectData Project { get; private set; }
        public SchemaItem SchemaItem { get; set; }
    }
}
