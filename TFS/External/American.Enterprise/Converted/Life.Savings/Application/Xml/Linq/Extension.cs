using System.IO;
using System.Xml.Linq;

namespace GregOsborne.Application.Xml.Linq {
    public static class Extension {
        public static XDocument GetXDocument(this string fileName) {
            return !File.Exists(fileName) ? null : XDocument.Load(fileName);
        }
    }
}