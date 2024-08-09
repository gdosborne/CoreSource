/* File="JSonValue"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Common.Settings.JSon {
    [JsonObject]
    public class JSonValue {
        internal JSonValue() { }
        public JSonValue(string name) {
            Name = name;
        }
        public JSonValue(string name, object value)
            : this(name) {
            Value = value;
        }
        [JsonProperty()]
        public string Name { get; private set; }
        [JsonProperty()]
        public object Value { get; set; }
    }
}
