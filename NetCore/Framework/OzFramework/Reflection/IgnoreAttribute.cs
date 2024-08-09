/* File="IgnoreAttribute"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Reflection {
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute {
    }
}
