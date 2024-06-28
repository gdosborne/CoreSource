using OzFramework.Primitives;
using OzFramework.Text;
using static OzFramework.Text.Extension;

using OzMiniDB.EventHandling;

using System.Reflection;
using System.Text;

using Universal.Common;

namespace OzMiniDB {
    internal static class Tags {
        public static string UsingsArea => "@usingsarea";
        public static string Namespace => "@namespace";
        public static string Class => "@classname";
        public static string Implementation => "@implementationname";
        public static string ImplementationArea => "@implementationarea";
        public static string PropertiesArea => "@propertiesarea";
        public static string DataType => "@datatype";
        public static string PropertyName => "@propertyname";

        public static string Tab(int level) => new string(' ', level * 3);

    }

    public sealed class DBGenerator {
        public DBGenerator(string databaseName, string databaseSchemaFilename, string databasePath) {
            DatabaseName = databaseName;
            DatabaseSchemaFilename = databaseSchemaFilename;
            DatabasePath = databasePath;
        }
        public DBGenerator(string databaseName, string databaseSchemaFilename, string databasePath, bool generateTopLevelDBEngine)
            : this(databaseName, databaseSchemaFilename, databasePath) {
            GenerateTopLevelDBEngine = generateTopLevelDBEngine;
        }
        public DBGenerator(string databaseName, string databaseSchemaFilename, string databasePath, bool generateTopLevelDBEngine, bool implementINotifyPropertyChanged)
            : this(databaseName, databaseSchemaFilename, databasePath, generateTopLevelDBEngine) {
            ImplementINotifyPropertyChanged = implementINotifyPropertyChanged;
        }

        public string DatabaseName { get; set; }
        public string DatabaseSchemaFilename { get; set; }
        public string DatabasePath { get; set; }
        public bool GenerateTopLevelDBEngine { get; set; }
        public bool ImplementINotifyPropertyChanged { get; set; }
        public event ActionOccurredHandler ActionOccurred;

        public void Run(Items.Database database) {
            var stringTypes = new List<Items.Field.DBDataType> {
                Items.Field.DBDataType.String,
                Items.Field.DBDataType.FixedString
            };
            var classTemplateFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Templates", "Domain", database.ClassTemplateFilename);
            var stdPropertyTemplateFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Templates", "Domain", database.StandardPropertyTemplateFilename);
            var notTemplateFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Templates", "Domain", database.NotificationPropertyTemplateFilename);

            var databaseName = $"{DatabaseName.Replace(" ", "_")}.{database.ClassesDirectory}";

            database.Tables.ForEach(t => {
                ActionOccurred?.Invoke(this, new ActionOccurredEventArgs(ActionOccurredEventArgs.ActionTypes.ClassAddStart, t.Name, 0));

                var code = t.GetText(database.ImplementPropertyChanged, classTemplateFilename,
                    stdPropertyTemplateFilename, notTemplateFilename, databaseName);

                var fname = $"{t.Name.Replace(" ", "_")}.cs";
                var dirName = Path.Combine(DatabasePath, database.ClassesDirectory);
                if (!Directory.Exists(dirName)) {
                    Directory.CreateDirectory(dirName);
                }
                var filename = Path.Combine(dirName, fname);
                if (File.Exists(filename)) {
                    File.Delete(filename);
                }
                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)) {
                    using var sw = new StreamWriter(fs);
                    sw.Write(code);
                    sw.Flush();
                }
                ActionOccurred?.Invoke(this, new ActionOccurredEventArgs(ActionOccurredEventArgs.ActionTypes.ClassAddEnd, t.Name, 0));
            });
            if (database.GenerateTopLevelDBEngineClass) {
                ActionOccurred?.Invoke(this, new ActionOccurredEventArgs(ActionOccurredEventArgs.ActionTypes.DBEngineCreated, "DBEngine", 0));
            }
        }

        private StringBuilder GetTemplateText(string templatePath) {
            var data = default(string);
            var result = new StringBuilder();
            try {
                using (var reader = File.OpenRead(templatePath)) {
                    data = reader.ReadToEnd();
                }
                if (!data.IsNull()) {
                    var hasStarted = false;
                    using var sr = new StringReader(data);
                    while (sr.Peek() > -1) {
                        var line = sr.ReadLine();
                        if (!line.IsNull()) {
                            if (line.StartsWithIgnoreCase("[start]")) {
                                hasStarted = true;
                                continue;
                            } else if (line.StartsWithIgnoreCase("[end]")) {
                                break;
                            } else if (hasStarted) {
                                result.AppendLine(line);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                result = new StringBuilder();
                result.Append($"@error: {ex.Message}");
            }
            return result;
        }

    }
}
