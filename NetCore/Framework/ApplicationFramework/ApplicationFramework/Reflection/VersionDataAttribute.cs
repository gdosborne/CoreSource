using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Reflection {
    [System.AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class VersionSchemaNameAttribute : Attribute {        
        public VersionSchemaNameAttribute(string data) {
            Data = data;
        }

        public string Data { get; private set; }
    }
}

