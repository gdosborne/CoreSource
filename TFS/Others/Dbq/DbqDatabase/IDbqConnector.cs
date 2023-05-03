using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase {
    public interface IDbqConnector {
        IEnumerable<object> ExecuteQuery(IDbqQuery query);
        bool ParseQuery(IDbqQuery query);
        IDbConnection Connection { get; }
    }
}
