using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.SqlCompact {
    public class DbqSqlCompactQuery : IDbqQuery {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
