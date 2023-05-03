namespace DatabaseStuff.DataContexts {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SqlServerContext : IDataContext {
        public IDbConnection Connection => throw new NotImplementedException();
        public List<T> GetAll<T>() => throw new NotImplementedException();
    }
}
