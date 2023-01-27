using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    internal static class Extensions {
        public static string ShortenedName(this FileInfo file) {
            var result = file.Name;
            if(result.Contains(".congregation", StringComparison.OrdinalIgnoreCase)) {
                result = result.Replace(".congregation", string.Empty);
            }
            return result;
        }
    }
}
