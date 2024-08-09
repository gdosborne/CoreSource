/* File="Shell32Wrapper"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using Common.Primitives;
using Shell32;
using System.Collections.Generic;
using System.Linq;
using SysIO = System.IO;

namespace Common.IO {
    public class Shell32Wrapper {
        public Shell32Wrapper(string filename) {
            Filename = filename;
        }

        public string Filename { get; private set; }

        private List<WrapperProperty> localProperties = default;
        public List<WrapperProperty> GetAllPropertyValues() {
            if (!localProperties.IsNull()) return localProperties;
            var result = new List<WrapperProperty>();
            var shell = new Shell();
            var folder = shell.NameSpace(SysIO.Path.GetDirectoryName(Filename));

            var folderItem = folder.ParseName(SysIO.Path.GetFileName(Filename));

            for (short i = 0; i < short.MaxValue; i++) {
                var header = folder.GetDetailsOf(null, i);
                if (header.IsNull())
                    continue;
                var value = folder.GetDetailsOf(folderItem, i);
                var wp = new WrapperProperty {
                    ID = i,
                    Filename = Filename,
                    Name = header,
                    Value = value
                };
                result.Add(wp);
            }
            localProperties = result.OrderBy(x => x.Name).ToList();
            return localProperties;
        }

        public string GetPropertyValue(string filename, string valueName) {
            var prop = GetAllPropertyValues().FirstOrDefault(x => x.Name == valueName);
            if (!prop.IsNull()) return prop.Value;
            return null;
        }

        //public void SetPropertyValue(string filename, string valueName, string value) {
        //    using (ShellFile shellFile = ShellFile.FromFilePath(filename)) {
        //        //shellFile.Properties.GetProperty(valueName)
        //        //shellFile.Properties.GetProperty(valueName).ValueAsObject = value;
        //        var writer = shellFile.Properties.GetPropertyWriter();
        //        writer.WriteProperty(valueName, value);
        //    }
        //    //var prop = GetAllPropertyValues().FirstOrDefault(x => x.Name == valueName);
        //    //if (prop.IsNull()) return;
        //    //var shell = new Shell();
        //    //var folder = shell.NameSpace(SysIO.Path.GetDirectoryName(Filename));
        //    //var folderItem = folder.ParseName(SysIO.Path.GetFileName(Filename));

        //    //folder.SetDetailsOf(folderItem, prop.ID, value);
        //}
    }
}
