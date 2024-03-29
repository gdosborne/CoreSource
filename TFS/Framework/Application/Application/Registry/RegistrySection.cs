using System.Collections.Generic;

namespace GregOsborne.Application.Registry {
    public class RegistrySection {
        public RegistrySection() {
			this.Values = new Dictionary<string, object>();
			this.Sections = new List<RegistrySection>();
        }

        public string Name { get; set; }

        public IList<RegistrySection> Sections { get; internal set; }

        public IDictionary<string, object> Values { get; internal set; }
    }
}