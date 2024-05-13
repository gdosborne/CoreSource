/* File="WrapperProperty"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.IO {
    public class WrapperProperty {
        public short ID { get; internal set; }
        public string Name { get; internal set; }
        public string Filename { get; internal set; }
        public string Value { get; set; }
        public override string ToString() => $"{Name}:={Value}";
    }
}
