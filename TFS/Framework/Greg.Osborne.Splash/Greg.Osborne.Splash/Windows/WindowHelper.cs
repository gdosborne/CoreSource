using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GregOsborne.Application.Xml.Linq;

namespace Greg.Osborne.Splash.Windows {
    internal static class WindowHelper {
        static WindowHelper() {
            CommonProperties = new Dictionary<string, string>();
        }

        public static Dictionary<string, string> CommonProperties { get; private set; }

        public static string CommonPropertiesFileName { get; private set; } = null;

        public static void LoadCommonProperties() {
            CommonProperties.Clear();
            CommonPropertiesFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CommonProperties.xml");
            var xDoc = CommonPropertiesFileName.GetXDocument();
            if (xDoc == null)
                return;
            var root = xDoc.Root;
            if (root == null)
                return;
            root.Elements().ToList().ForEach(x => {
                if (!x.LocalName().Equals("property", System.StringComparison.OrdinalIgnoreCase) ||
                    !x.Attributes().Any(y => y.LocalName().Equals("name", System.StringComparison.OrdinalIgnoreCase)) ||
                    !x.Attributes().Any(y => y.LocalName().Equals("value", System.StringComparison.OrdinalIgnoreCase)))
                    return;
                var name = x.Attributes().First(y => y.LocalName().Equals("name", System.StringComparison.OrdinalIgnoreCase)).Value;
                var value = x.Attributes().First(y => y.LocalName().Equals("value", System.StringComparison.OrdinalIgnoreCase)).Value;
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    CommonProperties.Add(name, value);
            });
        }
    }
}
