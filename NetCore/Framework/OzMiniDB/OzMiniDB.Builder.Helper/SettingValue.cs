using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzMiniDB.Builder.Helper {
    public class SettingValue {
        public SettingValue(string name, object value) {
            Name = name;
            Value = value;
        }
        public SettingValue(string name, object value, string description)
            : this (name, value) {
            Description = description;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public string Description { get; set; }
    }
}
