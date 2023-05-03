namespace DatabaseStuff.DataContexts {
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TestingContext : IDataContext {
        public IDbConnection Connection => null;

        public List<T> GetAll<T>() => throw new NotImplementedException();
    }
}
