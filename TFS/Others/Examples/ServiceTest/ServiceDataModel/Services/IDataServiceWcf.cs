namespace ServiceDataModel.Services {
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDataServiceWcf {

        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        Customer GetCustomer(int id);

        [OperationContract]
        List<Business> GetAllBusinesses();

        [OperationContract]
        Business GetBusiness(int id);
    }
}
