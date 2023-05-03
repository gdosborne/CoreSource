namespace ServiceData.Repository {
    using ServiceData.Reporting;
    using ServiceDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IServiceRepository {
        event ReportExceptionHandler ReportException;

        IDbConnection Connection { get; }
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        List<Business> GetAllBusinesses();
        Business GetBusiness(int id);
        bool IsCachingAvailable { get; }
        TimeSpan CacheRefresh { get; }
    }
}
