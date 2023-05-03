using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionMaster {
    public class VersionReader {
        public VersionReader(string projectFileName) {
            ProjectFileName = projectFileName;
        }

        public string ProjectFileName { get; private set; }
    }
}
