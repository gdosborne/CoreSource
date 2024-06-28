using OzFramework.Primitives;
using OzFramework.Text;

using System.Text;

using Universal.Common;

using static OzFramework.Text.Extension;

namespace OzMiniDB.Items {
    public abstract class Implementable {
        public abstract StringBuilder GetText(bool isImplementNotification, string classTemplateFilename, 
            string standardPropertyTemplateFilename, string notificationPropertyTemplateFilename, string databaseName);

        protected string DataType(Items.Field.DBDataType dataType, bool isRequired) {
            var req = default(string);
            if (!isRequired) {
                req = "?";
            }
            switch (dataType) {
                case Items.Field.DBDataType.String:
                case Items.Field.DBDataType.FixedString:
                case Items.Field.DBDataType.Note:
                    return $"string{req}";
                case Items.Field.DBDataType.WholeNumber:
                    return $"long{req}";
                case Items.Field.DBDataType.DecimalNumber:
                    return $"decimal{req}";
                case Items.Field.DBDataType.Boolean:
                    return $"bool{req}";
                case Items.Field.DBDataType.Guid:
                    return $"Guid{req}";
                case Items.Field.DBDataType.Date:
                case Items.Field.DBDataType.DateTime:
                    return $"DateTime{req}";
                case Items.Field.DBDataType.TimeSpan:
                    return $"TimeSpan{req}";
            }
            return null;
        }

        protected StringBuilder GetTemplateText(string templatePath) {
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
