namespace WcfService1 {
    using ServiceData.Logging;
    using ServiceData.Repository;
    using ServiceData.Repository.SqlServer;
    using ServiceDataModel;
    using ServiceDataModel.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DataServiceWcf : IDataServiceWcf {
        private IServiceRepository GetRepository() {
            if (_repository == null)
                _repository = SqlServerRepository.Create(true, TimeSpan.FromHours(1));
            _repository.ReportException += _repository_ReportException;
            return _repository;
        }

        private ILogger GetLogger() {
            if (_logger == null) {
                _logLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataService", "servicelog.log");
                _logger = Logger.Create(_logLocation);
            }
            return _logger;
        }

        private void _repository_ReportException(object sender, ServiceData.Reporting.ReportExceptionEventArgs e) {

        }

        private IServiceRepository _repository = default(SqlServerRepository);
        private ILogger _logger = default(Logger);
        private string _logLocation = default;

        public List<Customer> GetAllCustomers() => GetRepository().GetAllCustomers();
        public Customer GetCustomer(int id) => GetRepository().GetCustomer(id);
        public List<Business> GetAllBusinesses() => GetRepository().GetAllBusinesses();
        public Business GetBusiness(int id) => GetRepository().GetBusiness(id);
    }
}
