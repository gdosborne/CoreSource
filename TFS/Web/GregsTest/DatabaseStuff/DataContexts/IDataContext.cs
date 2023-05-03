namespace DatabaseStuff.DataContexts {
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataContext {
        IDbConnection Connection { get; }
        List<T> GetAll<T>();
    }
}
