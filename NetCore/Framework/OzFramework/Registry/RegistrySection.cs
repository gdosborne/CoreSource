/* File="RegistrySection"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Collections.Generic;

namespace OzFramework.Registry {
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
