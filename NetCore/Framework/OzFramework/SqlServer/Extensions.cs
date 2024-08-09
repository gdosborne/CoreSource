/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Xml;

namespace Common.SqlServer {
    public static class Extensions {
        public static T GetValue<T>(this SqlDataReader reader, string fieldName, T defaultValue = default) =>
            (T)(reader.IsDBNull(reader.GetOrdinal(fieldName)) ? defaultValue : reader.GetValue(reader.GetOrdinal(fieldName)));
        
        
    }
}
