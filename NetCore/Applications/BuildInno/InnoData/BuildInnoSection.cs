using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoData {
    public class BuildInnoSection {
        public BuildInnoSection(string name) {
            Name = name;
            Lines = new List<string>();
        }

        public string Name { get; private set; }
        public List<string> Lines { get; private set; }
        public BuildInnoProject Project { get; internal set; }

        public override string ToString() {
            return $"{Name} ({Lines.Count})";
        }
    }
}
