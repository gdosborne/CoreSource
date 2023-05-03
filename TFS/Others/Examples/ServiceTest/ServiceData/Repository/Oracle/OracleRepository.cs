using ServiceData.Reporting;
using ServiceDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.Repository.Oracle {
    public class OracleRepository : IServiceRepository {
        public List<Customer> GetAllCustomers() => throw new NotImplementedException();
        public IDbConnection Connection { get; private set; }
        public List<Business> GetAllBusinesses() => throw new NotImplementedException();
        public Customer GetCustomer(int id) => throw new NotImplementedException();
        public Business GetBusiness(int id) => throw new NotImplementedException();
        public bool IsCachingAvailable { get; }
        public TimeSpan CacheRefresh { get; }

        public event ReportExceptionHandler ReportException;
    }
}
