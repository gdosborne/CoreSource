using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.SqlServer {
    public class DbqSqlServerQuery:IDbqQuery {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
