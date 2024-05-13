/* File="VersionDataAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Reflection {
    [System.AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class VersionSchemaNameAttribute : Attribute {        
        public VersionSchemaNameAttribute(string data) {
            Data = data;
        }

        public string Data { get; private set; }
    }
}

