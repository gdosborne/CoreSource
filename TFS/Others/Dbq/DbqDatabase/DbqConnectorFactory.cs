using DbqDatabase.MSAccess;
using DbqDatabase.SqlCompact;
using DbqDatabase.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase {
    public enum DbqConnectorTypes {
        SqlCompact,
        SqlServer,
        MSAccess
    }
    public static class DbqConnectorFactory {
        public static IDbqConnector GetConnector(DbqConnectorTypes type, IDbqConnectorParameters parameters) {
            IDbqConnector result = null;
            switch (type) {
                case DbqConnectorTypes.SqlCompact:
                    result = new DbqSqlCompactConnector((DbqSqlCompactConnectorParameters)parameters);
                    break;
                case DbqConnectorTypes.SqlServer:
                    result = new DbqSqlServerConnector((DbqSqlServerConnectorParameters)parameters);
                    break;
                case DbqConnectorTypes.MSAccess:
                    result = new DbqMSAccessConnector((DbqMSAccessConnectorParameters)parameters);
                    break;
            }
            return result;
        }
    }
}
