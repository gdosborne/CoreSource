/* File="JSonSection"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Settings.JSon
{
    [JsonObject]
    public class JSonSection
    {
        public JSonSection(string name)
        {
            Name = name;
            Values = new List<JSonValue>();
        }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public List<JSonValue> Values { get; private set; }
    }
}
