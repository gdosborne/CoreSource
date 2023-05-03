namespace ServiceData.Repository.SqlServer {
    using GregOsborne.Application.Primitives;
    using ServiceData.ModelOverrides;
    using ServiceData.Reporting;
    using ServiceDataModel;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public sealed class SqlServerRepository : IServiceRepository, IDisposable {
        public static SqlServerRepository Create(bool isCachingAvailable, TimeSpan cacheRefresh) {
            return new SqlServerRepository(isCachingAvailable, cacheRefresh);
        }

        private SqlServerRepository(bool isCachingAvailable, TimeSpan cacheRefresh) {
            var connectionString = ConfigurationManager.AppSettings["productionConnectionString"].CastTo<string>();
            Connection = new SqlConnection(connectionString);
            IsCachingAvailable = isCachingAvailable;
            CacheRefresh = cacheRefresh;
        }

        public event ReportExceptionHandler ReportException;

        private DateTime _lastCache = DateTime.MinValue;
        private List<Customer> _CustomersCache = default;
        private List<Business> _BusinessesCache = default;
        public IDbConnection Connection { get; private set; }
        public void Dispose() => Dispose(true);
        private bool IsCacheExpired => DateTime.Now.Subtract(_lastCache) > CacheRefresh;

        private void RebuildCache() {
            _CustomersCache = LocalGetAllCustomers();
            _BusinessesCache = LocalGetAllBusinesses();
            _lastCache = DateTime.Now;
        }

        private List<Customer> LocalGetAllCustomers() {
            var result = new List<Customer>();
            using (var table = GetDataTable("select id, firstname, lastname, businessid from customer")) {
                if (table != null && table.Rows.Count > 0) {
                    var item = LocalCustomer.FromDataRow(table.Rows[0]);
                    item.Business = GetBusiness(table.Rows[0]["businessid"].CastTo<int>());
                    result.Add(item);
                }
            }
            return result;
        }

        private List<Business> LocalGetAllBusinesses() {
            var result = new List<Business>();
            using (var table = GetDataTable("select id, name from buiness")) {
                if (table != null && table.Rows.Count > 0) {
                    var item = LocalBusiness.FromDataRow(table.Rows[0]);
                    result.Add(item);
                }
            }
            return result;
        }

        private DataTable GetDataTable(string sql) {
            var result = default(DataTable);
            try {
                Connection.Open();
                using (var command = new SqlCommand(sql, Connection.As<SqlConnection>())) {
                    using (var reader = command.ExecuteReader()) {
                        result = new DataTable();
                        result.Load(reader);
                    }
                }
            }
            catch (Exception ex) {
                ReportException?.Invoke(this, new ReportExceptionEventArgs(ex));
            }
            finally {
                Connection.Close();
            }
            return result;
        }

        public List<Customer> GetAllCustomers() {
            if (IsCachingAvailable) {
                if (IsCacheExpired)
                    RebuildCache();
                return _CustomersCache;
            }
            return LocalGetAllCustomers();
        }

        public List<Business> GetAllBusinesses() {
            if (IsCachingAvailable) {
                if (IsCacheExpired)
                    RebuildCache();
                return _BusinessesCache;
            }
            return LocalGetAllBusinesses();
        }

        public Customer GetCustomer(int id) {
            var result = default(Customer);
            var isCachedItemMissing = false;
            if (IsCachingAvailable) {
                if (IsCacheExpired)
                    RebuildCache();
                result = _CustomersCache.FirstOrDefault(x => x.Id == id);
                if (result != null)
                    return result;
                isCachedItemMissing = true;
            }
            using (var table = GetDataTable($"select id, firstname, lastname, businessid from customer where id = {id}")) {
                if (table != null && table.Rows.Count > 0) {
                    result = LocalCustomer.FromDataRow(table.Rows[0]);
                    if (isCachedItemMissing && result != null)
                        _CustomersCache.Add(result);
                }
            }
            return result;
        }
        public Business GetBusiness(int id) {
            var result = default(Business);
            var isCachedItemMissing = false;
            if (IsCachingAvailable) {
                if (IsCacheExpired)
                    RebuildCache();
                result = _BusinessesCache.FirstOrDefault(x => x.Id == id);
                if (result != null)
                    return result;
                isCachedItemMissing = true;
            }
            using (var table = GetDataTable($"select id, name from business where id = {id}")) {
                if (table != null && table.Rows.Count > 0) {
                    result = LocalBusiness.FromDataRow(table.Rows[0]);
                    if (isCachedItemMissing && result != null)
                        _BusinessesCache.Add(result);
                }
            }
            return result;
        }

        public bool IsCachingAvailable { get; private set; }
        public TimeSpan CacheRefresh { get; private set; }

        private bool _disposedValue = false;
        private void Dispose(bool disposing) {
            if (!_disposedValue) {
                if (disposing) {
                    if (Connection != null) {
                        Connection.Close();
                        Connection.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }
    }
}
