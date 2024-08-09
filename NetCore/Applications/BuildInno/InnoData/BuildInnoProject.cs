using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using GregOsborne.Application.Media;

namespace InnoData {
    public class BuildInnoProject : INotifyPropertyChanged {
        public BuildInnoProject(string filename, FontFamily fontFamily, double fontSize) {
            Filename = filename;
            Name = System.IO.Path.GetFileNameWithoutExtension(filename);
            Sections = new List<BuildInnoSection>();
            AllSectionNames = new List<string> {
                "Setup", "Types", "Components", "Tasks", "Dirs",
                "Files", "Icons", "INI", "InstallDelete", "Languages",
                "Messages", "CustomMessages", "LangOptions", "Registry",
                "Run", "UninstallDelete", "UninstallRun"
            };
            Colors = [];
            InitializeDefaultColors();
            OpenSetupSourceFile(fontFamily, fontSize);
            Sections.ForEach(section => section.Project = this);
            RemainingSectionNames = new List<string>(AllSectionNames.Except(Sections.Select(x => x.Name)));
            SectionMenuItems = [];

            RemainingSectionNames.ForEach(item => {
                SectionMenuItems.Add(new MenuItem { Header = item });
            });
        }

        private void InitializeDefaultColors() {
            Colors.Add("NormalText", new SolidColorBrush("#FF000000".ToColor()));
            Colors.Add("Preprocessor", new SolidColorBrush("#FFFF0000".ToColor()));
            Colors.Add("SectionName", new SolidColorBrush("#FF0000FF".ToColor()));
            Colors.Add("Comment", new SolidColorBrush("#FF009900".ToColor()));
            Colors.Add("StringValue", new SolidColorBrush("#FFFF7F7F".ToColor()));
        }

        public ObservableCollection<MenuItem> SectionMenuItems { get; private set; }
        public List<string> AllSectionNames { get; private set; }
        public List<string> RemainingSectionNames { get; private set; }
        public string Filename { get; private set; }
        public string Name { get; set; }
        private string data = default;

        public FlowDocument Document { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public List<BuildInnoSection> Sections { get; private set; }
        private void OpenSetupSourceFile(FontFamily fontFamily, double fontSize) {
            if (!File.Exists(Filename)) {
                return;// throw new FileNotFoundException(Filename);
            }
            try {
                using (var fs = new FileStream(Filename, FileMode.Open, FileAccess.Read, FileShare.None)) {
                    using var reader = new StreamReader(fs);
                    data = reader.ReadToEnd();
                }
                ParseData(fontFamily, fontSize);
            }
            catch {
                throw;
            }
        }

        private void ParseData(FontFamily fontFamily, double fontSize) {
            var result = new FlowDocument {
                FontFamily = fontFamily,
                Foreground = Colors["NormalText"],
                FontSize = fontSize,
                PageWidth = 2500
            };
            var para = new Paragraph();
            using (var reader = new StringReader(data)) {
                while (reader.Peek() > -1) {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line)) {
                        var runs = ParseLine(line);
                        runs.ToList().OrderBy(x => x.Key).ToList().ForEach(run => {
                            para.Inlines.Add(run.Value);
                        });
                    }
                    para.Inlines.Add(new LineBreak());
                }
                reader.Close();
            }
            result.Blocks.Add(para);
            Document = result;
        }

        private IDictionary<int, Run> ParseLine(string line) {
            var result = new Dictionary<int, Run>();

            if (line.StartsWith(';')) {
                var defRun = new Run(line);
                defRun.Foreground = Colors["Comment"];
                result.Add(1, defRun);
            }
            else {
                var chars = line.ToCharArray();
                var index = 0;
                var pp = new List<char>();
                var key = default(string);

                for (int i = 0; i < chars.Length; i++) {
                    var c = chars[i];
                    if (c == '#') {
                        if (pp.Count > 0) {
                            var defRun = new Run(new string(pp.ToArray()));
                            defRun.Foreground = Colors[key];
                            result.Add(index, defRun);
                            pp.Clear();
                            index++;
                        }
                        key = "Preprocessor";
                        pp.Add(c);
                        i++;
                        while (i < chars.Length && chars[i] != ' ') {
                            c = chars[i];
                            pp.Add(c);
                            i++;
                        }
                        var run = new Run(new string(chars.ToArray()));
                        run.Foreground = Colors[key];
                        result.Add(index, run);
                        pp.Clear();
                        index++;
                    }
                    else if (c == '\"') {
                        if (pp.Count > 0) {
                            var defRun = new Run(new string(pp.ToArray()));
                            defRun.Foreground = Colors[key];
                            result.Add(index, defRun);
                            pp.Clear();
                            index++;
                        }
                        key = "StringValue";
                        pp.Add(c);
                        i++;
                        while (i < chars.Length && chars[i] != '\"') {
                            c = chars[i];
                            pp.Add(c);
                            i++;
                        }
                        var run = new Run(new string(chars.ToArray()));
                        run.Foreground = Colors[key];
                        result.Add(index, run);
                        pp.Clear();
                        index++;
                    }
                    else {
                        key = "NormalText";
                        pp.Add(c);
                    }
                }
            }
            return result;
        }

        public ColorDefinitions Colors { get; private set; }

    }
}
