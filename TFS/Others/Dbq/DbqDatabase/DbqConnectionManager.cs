using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase {
    public class DbqConnectionManager {
        public List<string> ConnectionTypes {
            get {
                return new List<string> {
                    DbqConnectorTypes.MSAccess.ToString(),
                    DbqConnectorTypes.SqlCompact.ToString(),
                    DbqConnectorTypes.SqlServer.ToString()
                };
            }
        }
    }
}
