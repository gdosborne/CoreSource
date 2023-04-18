using System.Collections.Generic;

namespace Common.OzApplication.Registry {
    public class RegistrySection {
        public RegistrySection() {
            Values = new Dictionary<string, object>();
            Sections = new List<RegistrySection>();
        }

        public string Name { get; set; }

        public IList<RegistrySection> Sections { get; internal set; }

        public IDictionary<string, object> Values { get; internal set; }
    }
}