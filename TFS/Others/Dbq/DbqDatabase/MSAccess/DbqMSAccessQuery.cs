using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.MSAccess {
    public class DbqMSAccessQuery : IDbqQuery {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
