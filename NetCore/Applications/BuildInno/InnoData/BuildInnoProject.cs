using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace InnoData {
    public class BuildInnoProject {
        public BuildInnoProject(string filename) {
            Filename = filename;
            Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            Sections = new List<BuildInnoSection>();
            AllSectionNames = new List<string> {
                "Setup", "Types", "Components", "Tasks", "Dirs",
                "Files", "Icons", "INI", "InstallDelete", "Languages",
                "Messages", "CustomMessages", "LangOptions", "Registry",
                "Run", "UninstallDelete", "UninstallRun"
            };
            OpenSetupSourceFile();
            Sections.ForEach(section => section.Project = this);
            RemainingSectionNames = new List<string>(AllSectionNames.Except(Sections.Select(x => x.Name)));
            SectionMenuItems = [];

            RemainingSectionNames.ForEach(item => {
                SectionMenuItems.Add(new MenuItem { Header = item });
            });
        }
        public ObservableCollection<MenuItem> SectionMenuItems { get; private set; }
        public bool HasChanges { get; private set; }
        public List<string> AllSectionNames { get; private set; }
        public List<string> RemainingSectionNames { get; private set; }
        public string Filename { get; private set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public List<BuildInnoSection> Sections { get; private set; }
        private void OpenSetupSourceFile() {
            if (!File.Exists(Filename)) {
                throw new FileNotFoundException(Filename);
            }
            try {
                using (var fs = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.None)) {
                    using var reader = new StreamReader(fs);
                    Data = reader.ReadToEnd();
                }
                using var sr = new StringReader(Data);
                var bis = new BuildInnoSection("#define");
                bis.Project = this;
                while (sr.Peek() > -1) {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        bis.Lines.Add(line);
                    else {
                        if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]")) {
                            var name = line.Trim(' ', '[', ']');
                            if (AllSectionNames.Contains(name)) {
                                bis = new BuildInnoSection(name);
                                Sections.Add(bis);
                            }
                            else
                                throw new ApplicationException($"Section name not found: ({name})");
                        }
                        else {
                            bis.Lines.Add(line);
                        }
                    }
                }
                Sections.Add(bis);
            }
            catch {
                throw;
            }
        }
    }
}
