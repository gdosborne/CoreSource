using DbqDatabase.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase.SqlServer {
    public class DbqSqlServerConnector : IDbqConnector {
        private DbqSqlServerConnectorParameters _parameters = null;
        private IDbqQueryParser _parser = null;
        private IDbConnection _connection = null;
        public DbqSqlServerConnector(DbqSqlServerConnectorParameters parameters) {
            _parameters = parameters;
            _parser = new DbqStandardQueryParser();
            if (_parameters.ContainsKey("connectionString"))
                _connection = new SqlConnection(_parameters["connectionString"]);
            else
                throw new MissingConnectionStringException("Parameters do not contain a connection string");
        }
        public IEnumerable<object> ExecuteQuery(IDbqQuery query) {
            throw new NotImplementedException();
        }

        public bool ParseQuery(IDbqQuery query) {
            return _parser.Parse(query);
        }

        public IDbConnection Connection { get { return _connection; } private set { _connection = value; } }
    }
}
